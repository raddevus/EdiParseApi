using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<EdiElement> EdiElements { get; set; }
    public DbSet<EdiSegment> EdiSegments { get; set; }
}
