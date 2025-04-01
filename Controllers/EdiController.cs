// Controllers/EdiController.cs
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class EdiController : ControllerBase
{
    private readonly IEdiParser _ediParser;
    //private readonly IEdiRepository _ediRepository;

    public EdiController(IEdiParser ediParser)//, IEdiRepository ediRepository)
    {
        _ediParser = ediParser;
      //  _ediRepository = ediRepository;
    }

    [HttpPost("process")]
    public async Task<IActionResult> ProcessEdi([FromForm] string filename, [FromForm] string ediContent)
    {
        try
        {
            var segments = await _ediParser.ParseEdiAsync(ediContent);
            
            EdiDocumentContext edc = new();
            var ediDoc = new EdiDocument(filename);
            edc.Add(ediDoc);
            edc.SaveChanges();


            EdiSegmentContext esc = new();
            foreach (var segment in segments)
            {
                segment.DocumentId = ediDoc.Id;
                Console.WriteLine($"Segment: {segment.Name}");
                string[] elementNames = EdiParser.ElementNames.ContainsKey(segment.Name) ? EdiParser.ElementNames[segment.Name] : null;
                EdiElementContext eec = new ();
                esc.Add(segment);
                esc.SaveChanges();
                for (int i = 0; i < segment.Elements.Count; i++)
                {
                    string elementName = elementNames != null && i < elementNames.Length ? elementNames[i] : $"Element {i + 1}";
                    EdiElement ediEl = new();
                    ediEl.Name = elementName;
                    ediEl.Value = segment.Elements[i].Value;
                    ediEl.SegmentId = segment.Id;
                    eec.Add(ediEl);
                    eec.SaveChanges();
                    Console.WriteLine($" - {elementName}: {segment.Elements[i].Value}");
                }
            }
            
            return Ok(new { Message = $"Processed {segments.Count} segments" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "Failed to process EDI", Details = ex.Message });
        }
    }

    [HttpPost("parseisa")]
    public async Task<IActionResult> ParseIsa([FromForm] string filename, [FromForm] string ediContent)
    {
        try
        {
            var segments = await _ediParser.ParseEdiAsync(ediContent,true);
            EdiDocumentContext edc = new();
            var ediDoc = new EdiDocument(filename);
            edc.Add(ediDoc);
            edc.SaveChanges();
            IsaHeader resultHeader = null;
            if (segments.Count > 0)
            {
                var segment = segments[0];
                segment.DocumentId = ediDoc.Id;
                resultHeader = StoreIsaHeader(segment);
                for (int i = 0; i < segment.Elements.Count; i++)
                {
                    string elementName = "ISA";
                    Console.WriteLine($" - {segment.Elements[i].Name}: {segment.Elements[i].Value}");
                }
            }
            
            if (resultHeader != null){
                return Ok(JsonSerializer.Serialize(resultHeader));
            }
            return Ok(new { Message = $"Invalid ISA? Failed to parse one of ISA items!!!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "Failed to parse ISA Header", Details = ex.Message });
        }
    }

    private IsaHeader StoreIsaHeader(EdiSegment isaHeader){
        
        try{
            IsaHeader header = new();
            header.DocumentId = isaHeader.DocumentId;
            header.AuthInfoQualifier = isaHeader.Elements[0].Value.Trim();
            header.AuthInfo = isaHeader.Elements[1].Value.Trim();
            header.SecurityInfoQualifier = isaHeader.Elements[2].Value.Trim();
            header.SecurityInfo = isaHeader.Elements[3].Value.Trim();
            header.SenderQualifier = isaHeader.Elements[4].Value.Trim();
            header.SenderId = isaHeader.Elements[5].Value.Trim();
            header.ReceiverQualifier = isaHeader.Elements[6].Value.Trim();
            header.ReceiverId = isaHeader.Elements[7].Value.Trim();        
            header.IsaDate = isaHeader.Elements[8].Value.Trim();
            header.IsaTime = isaHeader.Elements[9].Value.Trim();
            header.RepetitionSeparator = Convert.ToChar(isaHeader.Elements[10].Value.Trim());
            header.ControlVersionNumber = isaHeader.Elements[11].Value.Trim();
            header.ControlNumber = isaHeader.Elements[12].Value.Trim();
            header.AckRequested = Convert.ToBoolean(Convert.ToInt16(isaHeader.Elements[13].Value.Trim()));
            header.UsageIndicator = Convert.ToChar(isaHeader.Elements[14].Value.Trim());
            header.ComponentElementSeparator = Convert.ToChar(isaHeader.Elements[15].Value.Trim());

            IsaHeaderContext ihc = new();
            
            ihc.Add(header);
            ihc.SaveChanges();
            return header;
        }
        catch (Exception ex){
            Console.WriteLine($"Failed! {ex.Message}");
            return null;
        }
    }

}