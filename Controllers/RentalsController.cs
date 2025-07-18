using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    
    public class RentalsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public RentalsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> New(NewRentalViewModel viewModel)
        {
            if (string.IsNullOrWhiteSpace(viewModel.CustomerName) || string.IsNullOrWhiteSpace(viewModel.MovieName))
            {
                ViewBag.Error = "Please enter both Customer and Movie name.";
                return View(viewModel);
            }

            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Name != null && c.Name.ToLower() == viewModel.CustomerName.ToLower());
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Name != null && m.Name.ToLower() == viewModel.MovieName.ToLower());

            if (customer == null || movie == null)
            {
                ViewBag.Error = "Invalid customer or movie name.";
                return View(viewModel);
            }

            var rental = new Rental
            {
                CustomerId = customer.Id,
                MovieId = movie.id,
                DateRented = DateTime.Now
            };

            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();

            ViewBag.Success = "Movie rented successfully!";
            return View(new NewRentalViewModel());
        }
    }

}

