using Microsoft.EntityFrameworkCore;

namespace server.Models;

public class PftContext : DbContext
{
    public PftContext(DbContextOptions<PftContext> options)
        : base(options) { }

    public DbSet<Category> Categories { get; set; } = null!;

    public DbSet<Type> Types { get; set; } = null!;

    public DbSet<Entry> Entries { get; set; } = null!;
}
