// Models/EdiSegment.cs
public class EdiSegment
{
    public int Id { get; set; }
    public string Name{get;set;}
    public List<EdiElement> Elements { get; set; }
}