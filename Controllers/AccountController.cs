using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Service;
using Azure.Core;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {

        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("Account/loggedIn")]
        public IActionResult Create(Account obj)
        {
            var response=_userService.RegisterUser(obj);
            if (response?.Data == true)
            {
                return View();
            }
            else
            {
                foreach (var e in response.Errors)
                    ModelState.AddModelError("", e.Message);
                return RedirectToAction("LoginIndex");
            }
            
        }

        [HttpGet("Account/signUp")]
        public IActionResult SignUpIndex()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(Login user)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(user.email) || string.IsNullOrEmpty(user.password))
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View();
                }
                else if (!await _userService.IsUserRegisteredAsync(user.email))
                {
                    return RedirectToAction("SignUpIndex", "Account");
                }
                else
                {
                    bool isAuthenticated = await _userService.IsAuthenticatedAsync(user.email, user.password);
                    if (isAuthenticated)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Name,user.email)
                        };
                        var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            AllowRefresh = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(6),
                            IsPersistent = true,
                            IssuedUtc = DateTimeOffset.UtcNow
                        };
                        await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Email or password entered is wrong");
                    }
                }
            }
            return View("LoginIndex");
        }

        [HttpGet("Account/login")]
        public IActionResult LoginIndex()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
    }
