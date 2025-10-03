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
    public class DonationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _um;

        public DonationsController(ApplicationDbContext context, UserManager<ApplicationUser> um)
        {
            _context = context; _um = um;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                var all = await _context.Donations.Include(d => d.Donor).OrderByDescending(d => d.CreatedAt).ToListAsync();
                return View(all);
            }
            var userId = _um.GetUserId(User);
            var mine = await _context.Donations.Where(d => d.DonorId == userId).OrderByDescending(d => d.CreatedAt).ToListAsync();
            return View(mine);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Donation vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var user = await _um.GetUserAsync(User);
            vm.DonorId = user.Id;
            vm.CreatedAt = DateTime.UtcNow;
            _context.Donations.Add(vm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Coordinator")]
        public async Task<IActionResult> MarkDistributed(int id)
        {
            var donation = await _context.Donations.FindAsync(id);
            if (donation == null) return NotFound();
            donation.Status = DonationStatus.Distributed;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
