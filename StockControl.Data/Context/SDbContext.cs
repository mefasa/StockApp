using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockApp.Context
{
    public class SDbContext : DbContext
    {
        public SDbContext(DbContextOptions options) : base(options)
        {

        }
        public SDbContext():base()
        {

        }
        public DbSet<Product> Products{ get; set; }
        public DbSet<Order> Orders{ get; set; }
        public DbSet<Customer> Customers{ get; set; }
    }
}
