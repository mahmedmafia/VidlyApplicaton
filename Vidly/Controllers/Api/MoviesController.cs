using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using AutoMapper;
using System.Data.Entity;
namespace Vidly.Controllers.Api
{
    [Authorize(Roles =RoleName.CanManageMovies)]
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        //Get api/Movies

        public IHttpActionResult GetMovies()
        {
           var movieDtos= _context.Movies.Include(m=> m.Genre).ToList().
                Select(Mapper.Map<Movie,MovieDto>);
            return Ok(movieDtos);
        }

        //Get api/Movies/id

        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.ToList().SingleOrDefault(mov => mov.id == id);
            if (movie == null) return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        //Post api/Movies/id
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
           
            if (!ModelState.IsValid)
                return BadRequest();

            movieDto.DateAdded = DateTime.Now;
            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            movieDto.id = movie.id;
            _context.SaveChanges();
            return Created(new Uri(Request.RequestUri + "/" + movie.id), movieDto);
        }

        //PUT api/Movies/id
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id,MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDB = _context.Movies.SingleOrDefault(mov => mov.id == id);

            if (movieInDB == null) return NotFound();

            Mapper.Map(movieDto, movieInDB);
            _context.SaveChanges();

            return Ok();

        }
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDB = _context.Movies.SingleOrDefault(mov => mov.id == id);

            if (movieInDB == null) return NotFound();

            _context.Movies.Remove(movieInDB);
            _context.SaveChanges();

            return Ok();
        }
    }
}
