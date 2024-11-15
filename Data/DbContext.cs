using Microsoft.EntityFrameworkCore;
using System.Net;

public class AppDbContext : DbContext
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

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "a", Email = "a@a.a", Password = "a" },
            new User { Id = 2, Username = "b", Email = "b@b.b", Password = "b" },
            new User { Id = 3, Username = "c", Email = "c@c.c", Password = "c" }
        );

        modelBuilder.Entity<ContentType>().HasData(
            new ContentType { Id = 1, Type = FileType.PlainText },
            new ContentType { Id = 2, Type = FileType.Image }
        );

        modelBuilder.Entity<Folder>().HasData(
            new Folder { Id = 1, Name = "a", UserId = 1, ParentFolderId = null },
            new Folder { Id = 2, Name = "b", UserId = 2, ParentFolderId = null },
            new Folder { Id = 3, Name = "c", UserId = 3, ParentFolderId = null },
            new Folder { Id = 4, Name = "Homework", UserId = 1, ParentFolderId = 1 },
            new Folder { Id = 8, Name = "Totally_legal_movies:)", UserId = 1, ParentFolderId = 1 },
            new Folder { Id = 9, Name = "great_tits", UserId = 1, ParentFolderId = 4 },
            new Folder { Id = 5, Name = "Homework", UserId = 2, ParentFolderId = 2 },
            new Folder { Id = 6, Name = "Homework", UserId = 3, ParentFolderId = 3 },
            new Folder { Id = 7, Name = "Photos", UserId = 3, ParentFolderId = 3 }
        );

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var imageFileName = "Narvik-by-vinter-1440x900.jpg";
        var relativeImagePath = Path.Combine("uploads", imageFileName);
        var localImagePath = Path.Combine(uploadsFolder, imageFileName);
        var imageUrl = "https://narvikgaarden.no/wp-content/uploads/2021/03/Narvik-by-vinter-1440x900.jpg";

        if (!File.Exists(localImagePath))
        {
            using var client = new WebClient();
            client.DownloadFile(imageUrl, localImagePath);
        }

        modelBuilder.Entity<Document>().HasData(
            new Document { Id = 1, Title = "importante shit", Content = "Hallo,\nDette er en test", ContentTypeId = 1, CreatedDate = DateTime.Now, UserId = 1, ParentFolderId = 1 },
            new Document { Id = 2, Title = "Narvik Winter Image", Content = relativeImagePath, ContentTypeId = 2, CreatedDate = DateTime.Now, UserId = 1, ParentFolderId = 7 }
        );
    }
}
