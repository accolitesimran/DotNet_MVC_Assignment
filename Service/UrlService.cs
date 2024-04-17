using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Service
{
    public class UrlService : IUrlService
    {
        private readonly IUrlRepository _urlRepository;

        public UrlService(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }
        public async Task<bool> AddUrlAsync(Url url, string email)
        {
            var user = await _urlRepository.GetUserByEmailAsync(email);

            if (user == null)
            {
                return false;
            }

            user.profiles.Add(url);
            url.account = user;
            await _urlRepository.AddUrlAsync(url);
            return true;
        }

        public async Task<bool> DeleteUrlAsync(int id, string email)
        {
            var user = await _urlRepository.GetUserByEmailAsync(email);

            if (user == null)
            {
                return false;
            }

            var url = await _urlRepository.GetUrlByIdAsync(id);

            if (url == null)
            {
                return false;
            }

            user.profiles.Remove(url);
            await _urlRepository.RemoveUrlAsync(url);
            return true;
        }

        public async Task<Url> GetUrlByIdAsync(int id)
        {
            Url url= await _urlRepository.GetUrlByIdAsync(id);
            return url;
        }

        public async Task<Account> GetUserByEmailAsync(string email)
        {
            return await _urlRepository.GetUserByEmailAsync(email);
        }

        public async Task<bool> UpdateUrlAsync(Url url)
        {
            await _urlRepository.UpdateUrlAsync(url);
            return true;
        }
    }
}
