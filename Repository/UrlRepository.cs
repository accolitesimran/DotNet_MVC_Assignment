using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class UrlRepository : IUrlRepository
    {
        private readonly ApplicationDbContext _db;
        public UrlRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task AddUrlAsync(Url url)
        {
            _db.Urls.Add(url);
            await _db.SaveChangesAsync();
        }

        public async Task<Url> GetUrlByIdAsync(int id)
        {
            Url url= await _db.Urls.FindAsync(id);
            return url;
        }

        public async Task<Account> GetUserByEmailAsync(string email)
        {
            return await _db.Accounts.Include(a => a.address).Include(a => a.profiles).FirstOrDefaultAsync(x => x.email == email);
        }

        public async Task RemoveUrlAsync(Url url)
        {
            _db.Urls.Remove(url);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateUrlAsync(Url url)
        {
            _db.Urls.Update(url);
            await _db.SaveChangesAsync();
        }
    }
}
