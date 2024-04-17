using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IUserService _userService;
        public DashboardController(IUserService userService)
        { 
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var authenticatedEmail = @User.FindFirstValue(ClaimTypes.Name);
            var user = await _userService.GetUserByEmailAsync(authenticatedEmail);
            if (authenticatedEmail == null)
            {
                return RedirectToAction("LoginIndex", "Account");
            }
            return View(user);
        }

    }
}
