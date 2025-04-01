// Interfaces/IEdiParser.cs
public interface IEdiParser
{
    Task<List<EdiSegment>> ParseEdiAsync(string ediContent, bool onlyParseIsa=false);
}