// Models/EdiSegment.cs
using System.ComponentModel.DataAnnotations.Schema;
public class EdiSegment
{
    public int Id { get; set; }
    public int DocumentId{get;set;}
    public string Name{get;set;}

    [NotMapped]
    public List<EdiElement> Elements { get; set; } = new();

}
