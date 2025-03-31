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
    public async Task<IActionResult> ProcessEdi([FromForm] string ediContent)
    {
        try
        {
            var segments = await _ediParser.ParseEdiAsync(ediContent);
            // await _ediRepository.SaveSegmentsAsync(segments);
            EdiSegmentContext esc = new();
            foreach (var segment in segments)
            {
                
                Console.WriteLine($"Segment: {segment.Name}");
                string[] elementNames = EdiParser.ElementNames.ContainsKey(segment.Name) ? EdiParser.ElementNames[segment.Name] : null;
                for (int i = 0; i < segment.Elements.Count; i++)
                {
                    string elementName = elementNames != null && i < elementNames.Length ? elementNames[i] : $"Element {i + 1}";
                    Console.WriteLine($" - {elementName}: {segment.Elements[i].Value}");
                }
                esc.Add(segment);
                esc.SaveChanges();
                
            }

            return Ok(new { Message = $"Processed {segments.Count} segments" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "Failed to process EDI", Details = ex.Message });
        }
    }
}