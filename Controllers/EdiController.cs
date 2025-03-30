// Controllers/EdiController.cs
[Route("api/[controller]")]
[ApiController]
public class EdiController : ControllerBase
{
    private readonly IEdiParser _ediParser;
    private readonly IEdiRepository _ediRepository;

    public EdiController(IEdiParser ediParser, IEdiRepository ediRepository)
    {
        _ediParser = ediParser;
        _ediRepository = ediRepository;
    }

    [HttpPost("process")]
    public async Task<IActionResult> ProcessEdi([FromBody] string ediContent)
    {
        try
        {
            var segments = await _ediParser.ParseEdiAsync(ediContent);
            await _ediRepository.SaveSegmentsAsync(segments);
            return Ok(new { Message = $"Processed {segments.Count} segments" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "Failed to process EDI", Details = ex.Message });
        }
    }
}