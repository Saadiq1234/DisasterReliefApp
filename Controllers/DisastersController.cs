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
    public class DisastersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<ApplicationUser> _um;

        public DisastersController(ApplicationDbContext context, IWebHostEnvironment env, UserManager<ApplicationUser> um)
        {
            _context = context; _env = env; _um = um;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string? location, SeverityLevel? severity)
        {
            var q = _context.Disasters.AsQueryable();
            if (!string.IsNullOrWhiteSpace(location)) q = q.Where(d => d.Location.Contains(location));
            if (severity.HasValue) q = q.Where(d => d.Severity == severity);
            var list = await q.OrderByDescending(d => d.CreatedAt).ToListAsync();
            return View(list);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Disaster vm, IFormFile? evidence)
        {
            if (!ModelState.IsValid) return View(vm);
            var user = await _um.GetUserAsync(User);
            string? url = null;
            if (evidence != null && evidence.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(evidence.FileName)}";
                var path = Path.Combine(_env.WebRootPath, "uploads", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(path) ?? _env.WebRootPath);
                using (var fs = System.IO.File.Create(path)) await evidence.CopyToAsync(fs);
                url = $"/uploads/{fileName}";
            }

            vm.ReportedById = user.Id;
            vm.EvidenceUrl = url;
            vm.CreatedAt = DateTime.UtcNow;
            _context.Disasters.Add(vm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var d = await _context.Disasters.Include(x => x.ReportedBy).FirstOrDefaultAsync(x => x.Id == id);
            if (d == null) return NotFound();
            return View(d);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var d = await _context.Disasters.FindAsync(id);
            if (d == null) return NotFound();
            _context.Disasters.Remove(d);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
