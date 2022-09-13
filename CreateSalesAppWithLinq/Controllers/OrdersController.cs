using CreateSalesAppWithLinq.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLinq.Controllers
{
    public class OrdersController
    {

        public readonly AppDbContext _context = null!;

        public OrdersController(AppDbContext context) { _context = context; }


        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order?> GetByPK(int Id)
        {
            return await _context.Orders.FindAsync();
        }

        public async Task Update(int Id, Order order)
        {
            if(Id != order.Id)
            {
                throw new ArgumentException("The Order Id does not match the database");
            }
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Order> Insert(Order order)
        {
            if(order.Id != 0)
            {
                throw new ArgumentException("The order Id must be set to 0");
            }
            _context.Orders.Add(order);
           await _context.SaveChangesAsync();
            return order;
        }

        public async Task Delete(int Id, Order order)
        {
            Order? order1 = await GetByPK(order.Id);
            if(order1 == null)
            {
                throw new Exception("The order does not exist");
            }
            _context.Orders.Remove(order1);
            await _context.SaveChangesAsync();
        }
    }
}
