using Microsoft.EntityFrameworkCore;

namespace WebApiAutores
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet <Autor> Autores { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}