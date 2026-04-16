using Microsoft.EntityFrameworkCore;

namespace server.Models;

public class PftContext : DbContext
{
    public PftContext(DbContextOptions<PftContext> options)
        : base(options) { }

    // https://www.linkedin.com/pulse/standardize-datetime-handling-utc-dot-net-core-9-osama-nasir-djdbf/
    // Automatically convert all DateTime fields to UTC before saving them in the database.
    public override int SaveChanges()
    {
        ConvertDatesToUtc();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ConvertDatesToUtc();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void ConvertDatesToUtc()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            foreach (var property in entry.Properties)
            {
                if (property.Metadata.ClrType == typeof(DateTime) && property.CurrentValue != null)
                {
                    property.CurrentValue = DateTime
                        .SpecifyKind((DateTime)property.CurrentValue, DateTimeKind.Utc)
                        .ToUniversalTime();
                }
                // useful for Datetime?
                // else if (
                //     property.Metadata.ClrType == typeof(DateTime?)
                //     && property.CurrentValue != null
                // )
                // {
                //     var dateTime = (DateTime)property.CurrentValue!;
                //     property.CurrentValue = DateTime
                //         .SpecifyKind(dateTime, DateTimeKind.Utc)
                //         .ToUniversalTime();
                // }
            }
        }
    }

    public DbSet<Category> Categories { get; set; } = null!;

    public DbSet<Type> Types { get; set; } = null!;

    public DbSet<Entry> Entries { get; set; } = null!;
}
