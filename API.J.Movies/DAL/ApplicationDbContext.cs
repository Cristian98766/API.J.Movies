using API.J.Movies.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace API.J.Movies.DAL
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
    }
}
