using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models; // Replace with your actual namespace

namespace WebApplication3.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.Role == "Admin" && model.Email == "admin@vidly.com" && model.Password == "admin123")
            {
                HttpContext.Session.SetString("UserEmail", model.Email);
                HttpContext.Session.SetString("Role", "Admin");

                return RedirectToAction("Random", "Movies");
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

            if (user != null)
            {
                HttpContext.Session.SetString("UserEmail", user.Email!);
                HttpContext.Session.SetString("Role", "Customer");

                return RedirectToAction("SimpleMovies", "Movies");
            }

            TempData["Error"] = "Account not found! Please register first.";
            return View(model);

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            // Additionally clear the session cookie to prevent browser from reusing old session
            Response.Cookies.Delete(".AspNetCore.Session");
            return RedirectToAction("Login");
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Registration successful! Please log in.";
                return RedirectToAction("Login");
            }

            return View(user);
        }

    }
}
