using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using WebApplication1.Models;
using WebApplication1.Models.Response;

namespace WebApplication1.Service
{
    public interface IUserService
    {
        ResponseTemplate<bool> RegisterUser(Account obj);
        Task<bool> IsAuthenticatedAsync(string email, string password);
        Task<bool> IsUserRegisteredAsync(string email);
        Task LogoutAsync();
        Task<bool> ChangePasswordAsync(string email, string oldPassword, string newPassword);
        Task<Account> GetUserByEmailAsync(string email);
        Task<bool> UpdateProfileAsync(Account updatedUser, string email);
    }
}

