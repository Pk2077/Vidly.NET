using Vidly.Models;
using Vidly.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            if(User.IsInRole(RoleName.CanManageMovies))
            return View("List");
            return View("ReadOnlyList");
        }

        [Route("Movie/Details/{id}")]
        public ActionResult Details(int id)
        {
            var movie = _context.movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }

        [Route("Movies/random")]
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "sk" };
            var customers = new List<Customer>
            {
                new Customer {Name = "Pk1"},
                new Customer {Name = "Pk2"},
            };
            var viewModel = new RandomMovieViewModel { Movie = movie, Customers = customers };
            return View(viewModel);
        }

        [Route("Movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new MoviesViewModel
            {
                Genres = genres
            };

            return View("MovieForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie model)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MoviesViewModel(model)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }

            if (model.Id == 0)
                _context.movies.Add(model);
            else
            {
                var movieInDb = _context.movies.Single(m => m.Id == model.Id);
                movieInDb.Name = model.Name;
                movieInDb.GenreId = model.GenreId;
                movieInDb.Genre = model.Genre;
                movieInDb.ReleaseDate = model.ReleaseDate;
                movieInDb.NumberInStock = model.NumberInStock;
            }
            _context.SaveChanges();


            return RedirectToAction("Index", "Movies");
        }
        public ActionResult Edit(int Id)
        {
            var movie = _context.movies.SingleOrDefault(m => m.Id == Id);
            if (movie == null)
                return HttpNotFound();
            var viewModel = new MoviesViewModel(movie)
            {
                Genres = _context.Genres.ToList(),
            };
            return View("MovieForm", viewModel);
        }
    }
}