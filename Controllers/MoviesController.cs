using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApplication3.Models;



namespace WebApplication3.Controllers
{
    
    public class MoviesController : Controller
    {


        private readonly ApplicationDbContext _context;
        private const int PageSize = 5;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Random(string searchString, int page = 1)
        {

            
            int pageSize = PageSize;

            var moviesQuery = _context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                moviesQuery = moviesQuery
                    .Where(m => m.Name != null && m.Name != null && m.Name.ToLower().StartsWith(searchString));
            }

            var pagedMovies = await moviesQuery
                .OrderBy(m => m.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.SearchString = searchString;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)moviesQuery.Count() / pageSize);

            return View(pagedMovies);
        }



        public IActionResult Details(int id)
        {

            var movie = _context.Movies.FirstOrDefault(m => m.id == id);

            if (movie == null)
                return NotFound();


            return View(movie);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return View(movie);
            }

            _context.Movies.Add(movie);
            _context.SaveChanges();
            return RedirectToAction("Random");
        }


        public IActionResult Edit(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.id == id);
            if (movie == null)
                return NotFound();

            return View(movie);
        }
        

       
        public IActionResult Delete(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.id == id);
            if (movie == null)
                return NotFound();

            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return RedirectToAction("Random");
        }
        [HttpPost]
        public IActionResult Edit(Movie updatedMovie)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.id == updatedMovie.id);
            if (movie == null)
                return NotFound();

            movie.Name = updatedMovie.Name;
            movie.Genre = updatedMovie.Genre;
            _context.SaveChanges();

            return RedirectToAction("Random");
        }




        public async Task<IActionResult> LiveSearch(string searchString, int page = 1)
        {
            var moviesQuery = _context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                moviesQuery = moviesQuery
                    .Where(m => m.Name != null && m.Name.ToLower().Contains(searchString));
            }

            var pagedMovies = await moviesQuery
                .OrderBy(m => m.Name)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            ViewBag.SearchString = searchString;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)moviesQuery.Count() / PageSize);

            return PartialView("_MoviesTable", pagedMovies);
        }

        public async Task<IActionResult> SimpleMovies(string searchString, int page = 1)
        {
            int pageSize = 5;

            var moviesQuery = _context.Movies.AsQueryable(); // ✅ Removed Include

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                moviesQuery = moviesQuery
                    .Where(m => m.Name.ToLower().Contains(searchString));
            }

            var totalMovies = await moviesQuery.CountAsync();

            var movies = await moviesQuery
                .OrderBy(m => m.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.SearchString = searchString;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalMovies / pageSize);

            return View(movies);
        }



    }
}
