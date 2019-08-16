using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // Get api/customers
        public IHttpActionResult GetCustomers()
        {
            var CustomerDtos= _context.Customers
                .Include(c=>c.MemberShipType)
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(CustomerDtos);
        }


        // Get api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer= _context.Customers
                .Include(c => c.MemberShipType)
                .FirstOrDefault(c => c.id == id);

            if (customer == null)
                return NotFound();       
            return Ok(Mapper.Map<Customer, CustomerDto>(customer));

        }

        //POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer (CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
           var customer= Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            customerDto.id = customer.id;
            return Created(new Uri(Request.RequestUri + "/" +customer.id),customerDto );
        }

        //Put /api/customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id,CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDB = _context.Customers.Single(c => c.id == customerDto.id);

            if (customerInDB == null)
                return NotFound();

            Mapper.Map(customerDto,customerInDB);
           
            _context.SaveChanges();

            return Ok();

        }

        //DELETE /api/customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerDB = _context.Customers.FirstOrDefault(c => c.id == id);

            if (customerDB == null)
                return NotFound();

            _context.Customers.Remove(customerDB);
            _context.SaveChanges();

            return Ok();
        }

    }
}
