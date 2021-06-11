using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CSharp.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, FirstName = "Bob", LastName = "Bobberson", Address = "123 Main St New York, NY 36498"},
                new Customer { Id = 2, FirstName = "Adam", LastName = "Stone", Address = "234 South Drive New York, NY 36498" },
                new Customer { Id = 3, FirstName = "Sarah", LastName = "Lee", Address = "79 Street Circle New York, NY 36498" }
                );

            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, CustomerId = 1, Total = 25.98M },
                new Order { Id = 2, CustomerId = 2, Total = 29.98M }
                );

            modelBuilder.Entity<Item>().HasData(
                new Item { Id = 1, Name = "Health Potion", Price = 12.99M, OrderId = 1},
                new Item { Id = 2, Name = "Mana Potion", Price = 12.99M, OrderId = 1},
                new Item { Id = 3, Name = "Cape", Price = 13.99M, OrderId = 2},
                new Item { Id = 4, Name = "Helmet", Price = 15.99M, OrderId =2}
                );


        }
    }
}
