using CSharp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CSharp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderContext _orderContext;

        public OrderController(OrderContext orderContext)
        {
            _orderContext = orderContext;
            _orderContext.Database.EnsureCreated();
        }

        // GET: api/<OrderController>
        [HttpGet]
        public ActionResult GetAllOrders()
        {
            var orders = _orderContext.Orders;
            foreach (var order in orders)
            {
                var items = _orderContext.Items.Where(i => i.OrderId == order.Id).ToList();
                order.Items = items;
            }
            return Ok(orders);
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public ActionResult<List<Order>> GetOrders(int id)
        {
            var orders = _orderContext.Orders.Where(o => o.CustomerId == id);
            if (orders.Count() != 0)
            {
                foreach (var order in orders)
                {
                    var items = _orderContext.Items.Where(i => i.OrderId == order.Id).ToList();
                    if (items != null)
                    {
                        order.Items = items;
                    }
                }
                return Ok(orders.ToList());
            }
            return NotFound();
        }

        // POST api/<OrderController>
        [HttpPost()]
        public ActionResult PostOrder([FromBody] Order order)
        {
            if (!_orderContext.Orders.Any(o => o.Id == order.Id))
            {
                _orderContext.Orders.Add(order);
                return CreatedAtAction("Get", new { id = order.Id });
            }
            return NotFound();
        }

        // PUT api/<OrderController>
        [HttpPut()]
        public ActionResult PutOrder([FromBody] Order order)
        {
            if (_orderContext.Orders.Any(o => o.Id == order.Id))
            {
                _orderContext.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _orderContext.SaveChanges();
                return Ok();
            }
            return NotFound();

        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(int id)
        {
            var order = _orderContext.Orders.Find(id);
            if (order != null)
            {
                _orderContext.Orders.Remove(order);
                _orderContext.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
    }
}
