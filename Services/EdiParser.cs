// Services/EdiParser.cs
public class EdiParser : IEdiParser
{
    public async Task<List<EdiSegment>> ParseEdiAsync(string ediContent)
    {
        var segments = new List<EdiSegment>();
        
        if (string.IsNullOrWhiteSpace(ediContent))
            return segments;

        var lines = ediContent.Split(new[] { '\u2705', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        
        foreach (var line in lines)
        {
            var elements = line.Split('*');
            if (elements.Length > 0)
            {
                segments.Add(new EdiSegment
                {
                    SegmentIdentifier = elements[0],
                    Elements = elements.Skip(1).ToArray(),
                    ProcessedDate = DateTime.UtcNow
                });
            }
        }

        return await Task.FromResult(segments);
    }
}