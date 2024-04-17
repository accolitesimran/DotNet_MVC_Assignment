using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface IUserRepository
    {
        public int AddUser(Account obj);
        Task<Account> GetUserByEmailAsync(string email);
        Task UpdateUserAsync(Account user);
    }
}
