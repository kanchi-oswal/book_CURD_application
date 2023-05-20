using Book_app.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_app.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }
        public DbSet<Book> Books { get; set; }
    }
}
