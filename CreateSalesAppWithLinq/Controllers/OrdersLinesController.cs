using CreateSalesAppWithLinq.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLinq.Controllers
{
    public class OrdersLinesController
    {

        public readonly AppDbContext _context = null!;
        public OrdersLinesController(AppDbContext context) { _context = context; }

        public async Task<IEnumerable<OrderLine>> GetAll()
        {
            return await _context.OrdersLines.ToListAsync();
        }

        public async Task<OrderLine?> GetByPK(int Id)
        {
            return await _context.OrdersLines.FindAsync(Id);

        }

        public async Task Update(int Id, OrderLine orderLine)
        {
            if(Id != orderLine.Id)
            {
                throw new ArgumentException("The orderline Id does not match");
            }
            _context.Entry(orderLine).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<OrderLine?> Insert(OrderLine orderline)
        {
           
            _context.OrdersLines.Add(orderline);
            await _context.SaveChangesAsync();
            return orderline;
        }

        public async Task Delete(int Id, OrderLine orderline)
        {
            OrderLine? orderLine = await GetByPK(orderline.Id);
            if(orderLine == null)
            {
                throw new Exception("OrderLine is not found");
            }
            _context.OrdersLines.Remove(orderLine);
            await _context.SaveChangesAsync();
        }
    }
}
