using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSalesAppWithLinq.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderLine> OrdersLines { get; set; }


        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            string connectionString = @"server=localhost\sqlexpress01;database=SalesDatabase;trusted_connection=true;";
            if(!builder.IsConfigured)
            {
                builder.UseSqlServer(connectionString);
            }
        }


        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {

        }

    }
}
