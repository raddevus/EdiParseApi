// Data/EdiContext.cs
using Microsoft.EntityFrameworkCore;

public class EdiContext : DbContext
{
    public DbSet<EdiSegment> EdiSegments { get; set; }

    public EdiContext(DbContextOptions<EdiContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EdiSegment>()
            .Property(e => e.Elements)
            .HasConversion(
                v => JsonSerializer.Serialize(v, null),
                v => JsonSerializer.Deserialize<string[]>(v, null));
    }
}