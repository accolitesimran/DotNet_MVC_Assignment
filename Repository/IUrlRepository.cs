using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface IUrlRepository
    {
        Task<Account> GetUserByEmailAsync(string email);
        Task AddUrlAsync(Url url);
        Task<Url> GetUrlByIdAsync(int id);
        Task RemoveUrlAsync(Url url);
        Task UpdateUrlAsync(Url url);
    }
}
