using Library_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_System.Data;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> opts) : base(opts)
    {
        
    }

    public DbSet<BookModel> Books { get; set; }
}
