using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class NewRentalController : ApiController
    {
        private ApplicationDbContext _context;
        public NewRentalController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            if (newRental.MovieIds.Count == 0)
                return BadRequest("No Movie Have Been Given");

            var customer = _context.Customers.
                FirstOrDefault(c => c.id == newRental.CustomerId);

            if (customer == null)
                return BadRequest("Invalid Customer");

            var movies = _context.Movies.Where(m => newRental.MovieIds.Contains(m.id)).ToList() ;

            if (movies.Count != newRental.MovieIds.Count)
                return BadRequest("One or Movie are Invalid");

            foreach (var movie in movies)
            {
                if (movie.NumberAvaliable == 0)
                    return BadRequest("Movie not avaliable");

                movie.NumberAvaliable--;
                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now

                };

                _context.Rentals.Add(rental);
            }
            _context.SaveChanges();
            
            return Ok();
        }
    }
}
