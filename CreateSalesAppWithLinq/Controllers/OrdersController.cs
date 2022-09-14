using CreateSalesAppWithLinq.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLinq.Controllers
{
    public class OrdersController
    {

        private readonly AppDbContext _context = null!;

        public OrdersController(AppDbContext context) { _context = context; }


        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order?> GetByPK(int Id)
        {
            return await _context.Orders.FindAsync(Id);
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

        public async Task Delete(int Id)
        {
            Order? order1 = await GetByPK(Id);
            if(order1 == null)
            {
                throw new Exception("The order does not exist");
            }
            _context.Orders.Remove(order1);
            await _context.SaveChangesAsync();
        }

        //***Adding our own Methods***


            //Get all orders by customerId
        public async Task<IEnumerable<Order>> GetOrdersByCustomer(int customerId)
        {
                //Method syntax for async
            //return await _context.Orders.Where(o => o.CustomerId == customerId).ToListAsync();

                //Query syntax for async 
            var fred = from o in _context.Orders
                       where o.CustomerId == customerId
                       select o;
            
            return await fred.ToListAsync();

        }
            //Get all orders by customer code
        public async Task<IEnumerable<Order>> GetOrdersByCustomerCode(string code)
        {

            var fred = from o in _context.Orders
                       join c in _context.Customers 
                        on o.CustomerId equals c.Id
                    where c.Code == code
                    select o;
            return await fred.ToListAsync();
                       
        }

        //list of all orders with a specific product Id
        public async Task<IEnumerable<Order>> GetOrdersByProductId(int productId)
        {


            //my attempt
            var fred = from o in _context.Orders
                       join ol in _context.OrdersLines on o.Id equals ol.OrderId
                       join p in _context.Products on ol.ProductId equals p.Id
                       where p.Id == productId
                       select o;
                    
            return await fred.ToListAsync();

        }

        //read an order, pass instance to Method, method will set status field to Close as Update.
        public async Task SetStatusToClosed(int Id, Order order)
        {
            order.Status = "CLOSED";
            await Update(Id, order);  //instead of re-doing the update, call the Update.
         

        }

        //update status to in process, only if the total order is > 0
        public async Task SetStatusToInProcess(int Id, Order order)
        {
            if(order.Total != 0)
            {
                order.Status = "InProcess";
                await Update(Id, order);
            }
            return; //if total is 0, do not update
        }


        
       
        



    }
}
