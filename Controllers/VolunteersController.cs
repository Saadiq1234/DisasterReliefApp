using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DisasterReliefApp.Data;
using DisasterReliefApp.Models;
using Microsoft.AspNetCore.Identity;
using DisasterReliefApp.Models; // For ApplicationUser


namespace DisasterReliefApp.Controllers
{
    [Authorize]
    public class VolunteersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _um;

        public VolunteersController(ApplicationDbContext context, UserManager<ApplicationUser> um)
        {
            _context = context; _um = um;
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Volunteer vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var user = await _um.GetUserAsync(User);
            vm.UserId = user.Id;
            vm.RegisteredAt = DateTime.UtcNow;
            _context.Volunteers.Add(vm);
            await _context.SaveChangesAsync();
            await _um.AddToRoleAsync(user, "Volunteer");
            return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> MyVolunteers()
        {
            var list = await _context.Volunteers.Include(v => v.User).ToListAsync();
            return View(list);
        }
    }
}
