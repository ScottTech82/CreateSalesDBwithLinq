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

        private readonly AppDbContext _context = null!;
        private OrdersController ordCtrl;

        public OrdersLinesController(AppDbContext context) { _context = context; ordCtrl = new(_context); }

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
            await CalcOrderTotal(orderLine.OrderId);
        }

        public async Task<OrderLine?> Insert(OrderLine orderline)
        {
           
            _context.OrdersLines.Add(orderline);
            await _context.SaveChangesAsync();
            await CalcOrderTotal(orderline.OrderId);

            return orderline;
        }

        public async Task Delete(int Id)
        {
            var orderLine = await GetByPK(Id);
            if(orderLine == null)
            {
                throw new Exception("OrderLine is not found");
            }
            _context.OrdersLines.Remove(orderLine);
            await _context.SaveChangesAsync();
            await CalcOrderTotal(orderLine.OrderId);
        }



        //take quantity & unit price of a product and calculate as the order Total.
        //then placed the method after save changes in other methods insert, update, delete.
        private async Task CalcOrderTotal(int orderId)
        {
            
            var orderx = await ordCtrl.GetByPK(orderId);
            if(orderx is null)
            {
                throw new Exception("Order not found");
            }

            orderx.Total = (from ol in _context.OrdersLines
                            join p in _context.Products on ol.ProductId equals p.Id
                            where ol.OrderId == orderId
                            select new { LineTotal = ol.Quantity * p.Price }).Sum(o => o.LineTotal);

            await ordCtrl.Update(orderId, orderx);
        }

    }
}
