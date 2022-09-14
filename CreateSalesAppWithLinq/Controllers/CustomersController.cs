using CreateSalesAppWithLinq.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLinq.Controllers
{
    public class CustomersController
    {

        private readonly AppDbContext _context = null!;

        public CustomersController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetByPk(int Id)
        {
            return await _context.Customers.FindAsync(Id);
        }





        public async Task Update(int Id, Customer customer)
        {
            if(Id != customer.Id)
            {
                throw new ArgumentException("The Id does not match the database");
            }

            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }

        public async Task<Customer> Insert(Customer customer)
        {
            if(customer.Id != 0)
            {
                throw new ArgumentException("Adding a new Customer requires the ID to be 0");
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task Delete(int Id)
        {
            Customer? customer = await GetByPk(Id);
            if(customer is null)
            {
                throw new Exception("Customer not found");
            }
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();   
        }




    }
}
