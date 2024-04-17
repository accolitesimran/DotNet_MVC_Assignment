using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    public class ChangePasswordController : Controller
    {
        private readonly IUserService _userService;
        public ChangePasswordController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ChangePassword(ChangePassword obj)
        {
            var email = @User.FindFirstValue(ClaimTypes.Name);
            var result = await _userService.ChangePasswordAsync(email, obj.oldPassword, obj.newPassword);

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction("Index", "Dashboard");

        }
    }
}
