using Microsoft.EntityFrameworkCore;


namespace DotNetProject
{
    public class WorldDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public WorldDbContext(DbContextOptions<WorldDbContext> options)
              : base(options)
        {
        }
    }
}
