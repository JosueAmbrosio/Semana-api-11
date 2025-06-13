using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Semana_api_11.Data;
using Semana_api_11.Models;

namespace Semana_api_11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly SchoolContext _context;

        public CustomersController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer()
        {
            return await _context.Customer.Where(c => c.IsActive).ToListAsync();
        }


        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customer.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            customer.IsActive = false;

            _context.Entry(customer).Property(c => c.IsActive).IsModified = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Customers/deactivate/5
        [HttpPost("deactivate/{id}")]
        public async Task<IActionResult> DeactivateCustomer(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            customer.IsActive = false;

            _context.Entry(customer).Property(c => c.IsActive).IsModified = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Customers/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Customer>>> SearchCustomers(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Debe proporcionar un nombre o apellido para buscar.");
            }

            name = name.ToLower();

            var customers = await _context.Customer
                .Where(c =>
                    c.IsActive &&
                    (
                        c.FirstName.ToLower().Contains(name) ||
                        c.LastName.ToLower().Contains(name)
                    ))
                .ToListAsync();

            if (!customers.Any())
            {
                return NotFound("No se encontraron clientes con ese nombre o apellido.");
            }

            return Ok(customers);
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.CustomerId == id);
        }
    }
}
