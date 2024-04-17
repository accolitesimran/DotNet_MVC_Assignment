using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly IUrlService _urlService;
        public ProfilesController(IUrlService urlService)
        {
            _urlService = urlService;
        }
        public IActionResult Add()
        {
            var authenticatedEmail = @User.FindFirstValue(ClaimTypes.Name);
            if (authenticatedEmail == null)
            {
                return RedirectToAction("LoginIndex", "Account");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Url obj)
        {
            var authenticatedEmail = @User.FindFirstValue(ClaimTypes.Name);
            if(authenticatedEmail==null)
            {
                return RedirectToAction("LoginIndex", "Account");
            }
            if (ModelState.IsValid)
            {
                var success = await _urlService.AddUrlAsync(obj, authenticatedEmail);
                if (!success)
                {
                    return BadRequest();
                }
            } 
            return RedirectToAction("Index","Dashboard");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var authenticatedEmail = @User.FindFirstValue(ClaimTypes.Name);
            if (authenticatedEmail == null)
            {
                return RedirectToAction("LoginIndex", "Account");
            }
            Url url = await _urlService.GetUrlByIdAsync(id);
            return View(url);
        }
        public async Task<IActionResult> EditUrl(int id,Url updatedUrl)
        {
            var authenticatedEmail = @User.FindFirstValue(ClaimTypes.Name);
            if (authenticatedEmail == null)
            {
                return RedirectToAction("LoginIndex", "Account");
            }
            Url oldUrl = await _urlService.GetUrlByIdAsync(id);
            if (oldUrl == null)
            {
                return NotFound();
            }
            oldUrl.name = updatedUrl.name;
            oldUrl.url = updatedUrl.url;
            bool result = await _urlService.UpdateUrlAsync(oldUrl);

            if (!result)
            {
                return BadRequest();
            }
            return RedirectToAction("Index", "Dashboard");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var authenticatedEmail = @User.FindFirstValue(ClaimTypes.Name);
            if (authenticatedEmail == null)
            {
                return RedirectToAction("LoginIndex", "Account");
            }
            var result = await _urlService.DeleteUrlAsync(id, authenticatedEmail);

            if (!result)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Dashboard");
        }
    }
}
