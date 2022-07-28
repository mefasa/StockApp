using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StockApp.Context;
using System;
using System.Linq;

namespace StockControl.Data.Context
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<SDbContext>>()))
            {
                
                if (context.Customers.Any() && context.Products.Any())
                {
                    return;   
                }

                context.Customers.AddRange(
                    new Customer
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Mehmet Fatih Savran",
                        Contact = "1111111111",
                        BirthDate = DateTime.Parse("1996-1-21"),
                        City = "Ankara"
                    },

                    new Customer
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Leyla Yılmaz",
                        Contact = "11111111111",
                        BirthDate = DateTime.Parse("1986-2-23"),
                        City = "Konya"
                    },

                    new Customer
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Ali Kaya",
                        Contact = "2222222222",
                        BirthDate = DateTime.Parse("1993-5-14"),
                        City = "Antalya"
                    }
                );;
                context.Products.AddRange(
                    new Product
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Tire",
                        Piece = 50,
                        Price = 425
                    },
                    new Product
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Light",
                        Piece = 72,
                        Price = 5
                    },
                    new Product
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Accumulator",
                        Piece = 34,
                        Price = 15
                    },
                    new Product
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Brake",
                        Piece = 25,
                        Price = 20
                    },
                    new Product
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Fuel filter",
                        Piece = 75,
                        Price = 20
                    }

                );
                context.SaveChanges();
            }
        }
    }
   
}
