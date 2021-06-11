using CSharp.Controllers;
using CSharp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace CSharpTests
{
    public class OrderTests : OrderControllerTest
    {

        public OrderTests() : base(new DbContextOptionsBuilder<OrderContext>().UseInMemoryDatabase("TestOrders").Options)
        {

        }
        

        [Fact]
        public void CreateValidOrderTest()
        {
            using (var context = new OrderContext(Options))
            {
                var controller = new OrderController(context);
                var testOrder = new Order
                {
                    Id = 3,
                    CustomerId = 3,
                    Total = 25.98M,
                    Items = new List<Item>
                {
                    new Item { Id = 1, Name = "Health Potion", Price = 12.99M, OrderId = 3 },
                    new Item { Id = 2, Name = "Mana Potion", Price = 12.99M, OrderId = 3 },
                }
                };
                var result = controller.PostOrder(testOrder);

                Assert.IsType<CreatedAtActionResult>(result);
            }
        }

        [Fact]
        public void CreateInValidOrderTest()
        {
            using (var context = new OrderContext(Options))
            {
                var controller = new OrderController(context);
                var testOrder = new Order
                {
                    Id = 1,
                    CustomerId = 3,
                    Total = 25.98M,
                    Items = new List<Item>
                {
                    new Item { Id = 1, Name = "Health Potion", Price = 12.99M, OrderId = 3 },
                    new Item { Id = 2, Name = "Mana Potion", Price = 12.99M, OrderId = 3 },
                }
                };
                var result = controller.PostOrder(testOrder);

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public void ListOrderByCustomerOkResultTest()
        {
            using (var context = new OrderContext(Options))
            {
                var controller = new OrderController(context);
                var result = controller.GetOrders(1).Result as OkObjectResult;
                var orders = Assert.IsType<List<Order>>(result.Value);
                Assert.IsType<OkObjectResult>(result);
                Assert.Single(orders);
            }
        }

        [Fact]
        public void ListOrderByCustomerNotFoundResultTest()
        {
            using (var context = new OrderContext(Options))
            {
                var controller = new OrderController(context);
                var result = controller.GetOrders(47);
                Assert.IsType<NotFoundResult>(result.Result);
            }
        }

        [Fact]
        public void UpdateOrderOkResultTest()
        {
            using (var context = new OrderContext(Options))
            {
                var controller = new OrderController(context);
                var testOrder = new Order
                {
                    Id = 1,
                    CustomerId = 1,
                    Total = 12.99M,
                    Items = new List<Item>
                {
                    new Item { Id = 1, Name = "Health Potion", Price = 12.99M, OrderId = 1 }
                }
                };
                var result = controller.PutOrder(testOrder);
                Assert.IsType<OkResult>(result);
            }
        }

        [Fact]
        public void UpdateOrderNotFoundResultTest()
        {
            using (var context = new OrderContext(Options))
            {
                var controller = new OrderController(context);
                var testOrder = new Order
                {
                    Id = 100,
                    CustomerId = 1,
                    Total = 12.99M,
                    Items = new List<Item>
                {
                    new Item { Id = 1, Name = "Health Potion", Price = 12.99M, OrderId = 1 }
                }
                };
                var result = controller.PutOrder(testOrder);
                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public void DeleteOrder()
        {
        }
    }
}
