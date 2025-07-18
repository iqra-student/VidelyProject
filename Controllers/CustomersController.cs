using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;


namespace WebApplication3.Controllers
{
  
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        int pageSize = 5;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchString, int page = 1)
        {
           

            var customersQuery = _context.Customers
                .Include(c => c.MembershipType)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                customersQuery = customersQuery.Where(c => c.Name != null && c.Name.Contains(searchString));
            }

            var totalCustomers = await customersQuery.CountAsync();

            var customers = await customersQuery
                .OrderBy(c => c.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCustomers / pageSize);
            ViewBag.SearchString = searchString;

            return View(customers);
        }

        public IActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            ViewBag.MembershipTypes = new SelectList(membershipTypes, "Id", "Name");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var membershipTypes = await _context.MembershipTypes.ToListAsync();
                ViewBag.MembershipTypes = new SelectList(membershipTypes, "Id", "Name");
                return View("New", customer);
            }

            if (customer.BirthDate == null || CalculateAge(customer.BirthDate.Value) < 18)
            {
                ModelState.AddModelError("BirthDate", "Customer must be at least 18 years old to register.");
                var membershipTypes = await _context.MembershipTypes.ToListAsync();
                ViewBag.MembershipTypes = new SelectList(membershipTypes, "Id", "Name");
                return View("New", customer);
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age;
        }



        public async Task<IActionResult> Search(string searchString)
        {
            var customersQuery = _context.Customers.Include(c => c.MembershipType).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                customersQuery = customersQuery
                    .Where(c => c.Name != null && c.Name.ToLower().Contains(searchString));
            }

            var filteredCustomers = await customersQuery
                .OrderBy(c => c.Name)
                .ToListAsync();

            return PartialView("_CustomerTablePartial", filteredCustomers);
        }



    }
}

