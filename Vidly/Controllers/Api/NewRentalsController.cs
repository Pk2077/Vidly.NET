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
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateRentals(NewRentalDto newRental)
        {
            var Customer = _context.customers.Single(c => c.Id == newRental.CustomerId);  
            var Movies = _context.movies.Where(m=>newRental.MovieIds.Contains(m.Id)).ToList();

            foreach (var movie in Movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie Not Available");
                movie.NumberAvailable--;
                var rental = new Rental
                {
                    Customer = Customer,
                    Movie=movie,
                    DateRented=DateTime.Now,
                };
                _context.Rentals.Add(rental);
            }
            _context.SaveChanges();

            return Ok();
        }
    }
}
