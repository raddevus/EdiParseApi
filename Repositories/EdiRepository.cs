// Repositories/EdiRepository.cs
public class EdiRepository : IEdiRepository
{
    private readonly EdiContext _context;

    public EdiRepository(EdiContext context)
    {
        _context = context;
    }

    public async Task SaveSegmentsAsync(List<EdiSegment> segments)
    {
        await _context.EdiSegments.AddRangeAsync(segments);
        await _context.SaveChangesAsync();
    }
}