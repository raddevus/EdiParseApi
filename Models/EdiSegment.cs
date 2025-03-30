// Models/EdiSegment.cs
public class EdiSegment
{
    public int Id { get; set; }
    public string SegmentIdentifier { get; set; }
    public string[] Elements { get; set; }
    public DateTime ProcessedDate { get; set; }
}