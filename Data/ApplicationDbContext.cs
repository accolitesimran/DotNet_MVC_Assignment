using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
                
        }
        public DbSet<WebApplication1.Models.Account> Accounts { get; set; }
        public DbSet<WebApplication1.Models.Address> Addresses { get; set; }
        public DbSet<WebApplication1.Models.Url> Urls { get; set; }
        public DbSet<WebApplication1.Models.ChangePassword> ChangePassword { get; set; } = default!;

    }
}
