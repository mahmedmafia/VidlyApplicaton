using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    [Authorize(Roles =RoleName.CanManageMovies)]
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

        public ActionResult New()
        {
            var genrelist = _context.Genres.ToList();
            var viewmodel = new MovieFormViewModel
            {
                
                Genre = genrelist
            };
            return View("MovieForm",viewmodel);

        }
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.Single(m => m.id == id);
            if(movie==null)
                 return HttpNotFound();

            var viewmodel = new MovieFormViewModel(movie)
            {
                Genre = _context.Genres.ToList(),
               
            };
            
            return View("MovieForm", viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewmodel = new MovieFormViewModel(movie)
                {
                    Genre = _context.Genres.ToList()
                 
                };
                
                return View("MovieForm",viewmodel);
            }
            if(movie.id==0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }else
            {
                var MovieInDb = _context.Movies.Single(m => m.id == movie.id);
                MovieInDb.Name = movie.Name;
                MovieInDb.GenreId = movie.GenreId;
                MovieInDb.ReleaseDate = movie.ReleaseDate;    
                MovieInDb.Count = movie.Count;
            }
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        // GET: Movies
        [AllowAnonymous]
        public ActionResult Index()
        {
            //var Movies = _context.Movies.Include(c =>c.Genre).ToList();
            if(User.IsInRole(RoleName.CanManageMovies))
            {
                return View("Index");
            }
            return View("ReadOnlyIndex");
           
        }
        public ActionResult Details(int id)
        {

            var movie = _context.Movies.Include(c => c.Genre).FirstOrDefault(moviex => moviex.id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }
    }
}