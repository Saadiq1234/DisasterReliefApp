using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DisasterReliefApp.Models;

namespace DisasterReliefApp.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _um;
        private readonly SignInManager<ApplicationUser> _sm;

        public ProfileController(UserManager<ApplicationUser> um, SignInManager<ApplicationUser> sm)
        {
            _um = um; _sm = sm;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _um.GetUserAsync(User);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateName(string fullName)
        {
            var user = await _um.GetUserAsync(User);
            user.FullName = fullName;
            await _um.UpdateAsync(user);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount()
        {
            var user = await _um.GetUserAsync(User);
            await _um.DeleteAsync(user);
            await _sm.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
