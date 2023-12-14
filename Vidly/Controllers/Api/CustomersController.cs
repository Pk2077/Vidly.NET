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
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult GetCustomers(string query=null)
        {
            var customersQuery = _context.customers
                .Include(c => c.MembershipType);

            if(!string.IsNullOrWhiteSpace(query))
                customersQuery=customersQuery.Where(c=>c.Name.Contains(query));

            var customerDtos = customersQuery
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);
            return Ok(customerDtos);
        }
        public IHttpActionResult GetCustomer(int Id)
        {
            var customer = _context.customers.SingleOrDefault(c => c.Id == Id);
            if (customer == null)
                return NotFound();
            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;


            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int Id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var CustomerInDb = _context.customers.SingleOrDefault(c => c.Id == Id);
            if (CustomerInDb == null)
            {
                return NotFound();
            }

            Mapper.Map(customerDto, CustomerInDb);
            _context.SaveChanges();

            return Ok(customerDto);
        }

        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int Id)
        {
            var CustomerInDb = _context.customers.SingleOrDefault(c => c.Id == Id);
            if (CustomerInDb == null)
                return NotFound();

            _context.customers.Remove(CustomerInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
