// Interfaces/IEdiRepository.cs
public interface IEdiRepository
{
    Task SaveSegmentsAsync(List<EdiSegment> segments);
}