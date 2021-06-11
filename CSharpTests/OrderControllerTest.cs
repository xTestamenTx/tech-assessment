using CSharp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpTests
{
    public class OrderControllerTest
    {
        private DbContextOptionsBuilder<OrderContext> dbContextOptionsBuilder;

        protected DbContextOptions<OrderContext> Options { get; }
        public OrderControllerTest(DbContextOptions<OrderContext> options)
        {
            Options = options;
            Seed();
        }

        private void Seed()
        {
            using (var orderContext = new OrderContext(Options))
            {
                orderContext.Database.EnsureCreated();
            }
        }
    }
}
