using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DisasterReliefApp.Data;
using DisasterReliefApp.Models;
using Microsoft.AspNetCore.Identity;
using DisasterReliefApp.Models; // For ApplicationUser

// Alias to avoid conflict with System.Threading.Tasks.TaskStatus
using DRTaskStatus = DisasterReliefApp.Models.TaskStatus;

namespace DisasterReliefApp.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _um;

        public TasksController(ApplicationDbContext context, UserManager<ApplicationUser> um)
        {
            _context = context;
            _um = um;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var list = await _context.TaskAssignments
                                     .Include(t => t.AssignedVolunteer)
                                     .OrderByDescending(t => t.ScheduledAt)
                                     .ToListAsync();
            return View(list);
        }

        [Authorize(Roles = "Admin,Coordinator")]
        public IActionResult Create() => View();

        [HttpPost]
        [Authorize(Roles = "Admin,Coordinator")]
        public async Task<IActionResult> Create(TaskAssignment vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var user = await _um.GetUserAsync(User);
            vm.CreatedById = user.Id;
            _context.TaskAssignments.Add(vm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Volunteer")]
        public async Task<IActionResult> SignUp(int id)
        {
            var user = await _um.GetUserAsync(User);
            var volunteer = await _context.Volunteers.FirstOrDefaultAsync(v => v.UserId == user.Id);
            if (volunteer == null) return RedirectToAction("Register", "Volunteers");

            var task = await _context.TaskAssignments.FindAsync(id);

            // Use the alias to avoid ambiguity
            task.Status = DRTaskStatus.Assigned;
            task.AssignedVolunteerId = volunteer.Id;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
