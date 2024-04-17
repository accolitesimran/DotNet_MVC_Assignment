using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class UserRepository : IUserRepository
    {
        ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public int AddUser(Account obj)
        {
            int result;
            if (!_db.Accounts.Any(x => x.email == obj.email))
            {
                _db.Accounts.Add(obj);
                result = _db.SaveChanges();
            }
            else
                throw new Exception("User already exist");
            return result;
        }

        public async Task<Account> GetUserByEmailAsync(string email)
        {
            return await _db.Accounts.Include(a=>a.address).Include(a=>a.profiles).FirstOrDefaultAsync(u => u.email == email);
        }

        public async Task UpdateUserAsync(Account user)
        {
            _db.Update(user);
            await _db.SaveChangesAsync();
        }
    }
}
