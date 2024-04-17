using WebApplication1.Models;

namespace WebApplication1.Service
{
    public interface IUrlService
    {
        Task<Account> GetUserByEmailAsync(string email);
        Task<bool> AddUrlAsync(Url url, string email);
        Task<bool> DeleteUrlAsync(int id, string email);
        Task<Url> GetUrlByIdAsync(int id);
        Task<bool> UpdateUrlAsync(Url url);
    }
}
