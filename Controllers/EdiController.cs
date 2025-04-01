// Controllers/EdiController.cs
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
            
            foreach (var segment in segments)
            {
                for (int i = 0; i < segment.Elements.Count; i++)
                {
                    string elementName = "ISA";
                    Console.WriteLine($" - {segment.Elements[i].Name}: {segment.Elements[i].Value}");
                }
            }
            
            return Ok(new { Message = $"Processed {segments.Count} segments" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "Failed to process EDI", Details = ex.Message });
        }
    }

}