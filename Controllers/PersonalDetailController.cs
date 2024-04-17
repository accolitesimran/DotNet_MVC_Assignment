using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Service;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication1.Controllers
{
    public class PersonalDetailController : Controller
    {
        private readonly IUserService _userService;
        public PersonalDetailController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string email)
        {
            var authenticatedEmail= @User.FindFirstValue(ClaimTypes.Name);
            if(authenticatedEmail != email) {
                return RedirectToAction("LoginIndex", "Account");
            }
            var user=await _userService.GetUserByEmailAsync(email);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(Account obj,string email)
        {
            var authenticatedEmail = @User.FindFirstValue(ClaimTypes.Name);
            if (authenticatedEmail != email)
            {
                return RedirectToAction("LoginIndex", "Account");
            }
            if (email != obj.email)
            {
                return NotFound();
            }
            var result = await _userService.UpdateProfileAsync(obj, email);

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction("Index","Dashboard");
        }
    }
}
