using Microsoft.AspNetCore.Mvc;
using DisasterReliefApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace DisasterReliefApp.Controllers
{
    public class VolunteersController : Controller
    {
        [AllowAnonymous] // <-- this is important
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Volunteer vm)
        {
            if (!ModelState.IsValid) return View(vm);

            // Save volunteer to database
            // _context.Volunteers.Add(vm);
            // _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
