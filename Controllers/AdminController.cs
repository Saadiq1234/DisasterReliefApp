using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DisasterReliefApp.Data;

namespace DisasterReliefApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context) { _context = context; }

        public async Task<IActionResult> Dashboard()
        {
            var data = new
            {
                TotalDisasters = await _context.Disasters.CountAsync(),
                TotalDonations = await _context.Donations.CountAsync(),
                TotalVolunteers = await _context.Volunteers.CountAsync(),
                TotalTasks = await _context.TaskAssignments.CountAsync()
            };
            ViewBag.Stats = data;
            return View();
        }

        public async Task<IActionResult> Reports()
        {
            var reports = await _context.Disasters.Include(d => d.ReportedBy).ToListAsync();
            return View(reports);
        }
    }
}
