using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetMovies(string query = null)
        {
            
            var moviesQuery = _context.movies
                 .Include(m => m.Genre)
                 .Where(m=>m.NumberAvailable>0);
            if(!string.IsNullOrWhiteSpace(query))
                moviesQuery= moviesQuery.Where(m=>m.Name.Contains(query));

            var movieDto = moviesQuery
                 .ToList()
                 .Select(Mapper.Map<Movie, MovieDto>);
            return Ok(movieDto);
        }
        public IHttpActionResult GetMovie(int id)
        {
            return Ok(Mapper.Map<Movie, MovieDto>(_context.movies.SingleOrDefault(m => m.Id == id)));
        }

        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;


            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var movieInDb = _context.movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
                return NotFound();

            Mapper.Map(movieDto, movieInDb);
            _context.SaveChanges();

            return Ok(movieDto);
        }
        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = _context.movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb == null)
                return NotFound();

            _context.movies.Remove(movieInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
