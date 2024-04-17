using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Response;
using WebApplication1.Repository;

namespace WebApplication1.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }



        public ResponseTemplate<bool> RegisterUser(Account user)
        {

            ResponseTemplate<bool> responseTemplate = new ResponseTemplate<bool>();
            try
            {
                var result = _userRepository.AddUser(user);
                responseTemplate.Data = result > 0;
            }
            catch (Exception ex)
            {
                responseTemplate.Errors = new List<Error> { new Error { Code = "RUEX", Message = ex.Message } };
            }
            return responseTemplate;
        }
        public async Task<bool> IsAuthenticatedAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            return user != null && user.password == password;
        }

        public async Task<bool> IsUserRegisteredAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            return user != null;
        }

        public async Task LogoutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<bool> ChangePasswordAsync(string email, string oldPassword, string newPassword)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                return false;
            }

            if (user.password != oldPassword)
            {
                return false;
            }

            user.password = newPassword;
            await _userRepository.UpdateUserAsync(user);
            return true; 
        }

        public async Task<Account> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task<bool> UpdateProfileAsync(Account updatedUser, string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            if (user == null || user.email != updatedUser.email)
            {
                return false;
            }
            updatedUser.address.account = user;
            updatedUser.address.accountId = user.Id;
            user.phoneNo = updatedUser.phoneNo;
            user.address = updatedUser.address;
            user.email = updatedUser.email;
            user.firstName = updatedUser.firstName;
            user.lastName = updatedUser.lastName;

            await _userRepository.UpdateUserAsync(user);
            return true;
        }
    }
}
