using CreateSalesAppWithLinq.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLinq.Controllers
{
    public class ProductsController
    {
        private readonly AppDbContext _context = null!;
        public ProductsController(AppDbContext context) { _context = context; }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByPK(int Id)
        {
            return await _context.Products.FindAsync(Id);
        }

        public async Task Update(int Id, Product product)
        {
            if(Id != product.Id)
            {
                throw new ArgumentException("The product Id does not match the database");
            }
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Product> Insert(Product product)
        {
            if(product.Id != 0)
            {
                throw new ArgumentException("The product Id must be set to 0");
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task Delete(int Id)
        {
            Product? product = await GetByPK(Id);
            if(product == null)
            {
                throw new Exception("The product does not exist");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }





    }


}
