using Microsoft.EntityFrameworkCore;


public class AppDbContext  : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User>? User { get; set; }
    public DbSet<Document>? Document { get; set; }
    public DbSet<Folder>? Folder { get; set; }
    public DbSet<ContentType>? ContentType { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }
}