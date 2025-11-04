using Microsoft.AspNetCore.Mvc;

namespace DisasterReliefApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Temporary fake login for now — replace with Identity later
            if (username == "admin" && password == "1234")
            {
                return RedirectToAction("Dashboard", "Admin");
            }

            ViewBag.Error = "Invalid login credentials";
            return View();
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string username, string password)
        {
            // Temporary stub — you can wire up real registration later
            ViewBag.Message = "Registration successful (placeholder)";
            return RedirectToAction("Login");
        }

        // GET: /Account/AccessDenied
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
