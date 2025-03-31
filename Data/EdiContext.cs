// Data/EdiContext.cs
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

public class EdiContext : DbContext
{
    public DbSet<EdiSegment> EdiSegments { get; set; }

    public EdiContext(DbContextOptions<EdiContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<EdiSegment>()
        //     .Property(e => e.Elements)
        //     .HasConversion(
        //         v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null)
        //         v => JsonSerializer.Deserialize<List<EdiSegment>(v, (JsonSerializerOptions)null));
                
    }
}