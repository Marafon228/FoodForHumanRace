using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplicationFoodForHumanRace.Models;
using WebApplicationFoodForHumanRace.Models.Json;

namespace WebApplicationFoodForHumanRace.Controllers
{
    public class OrdersController : ApiController
    {
        private ADO db = new ADO();

        // GET: api/Orders
        public IQueryable<Order> GetOrder()
        {
            return db.Order;
        }
        //Get
        [ActionName("GetOrders")]
        [ResponseType(typeof(List<Order>))]
        public IHttpActionResult GetProducts()
        {
            User user = db.User.FirstOrDefault(u => u.Id == 1);

            //return Ok(db.Product.ToList().ConvertAll(p => new ProductResponse(p)));

            var order = db.Order.ToList<Order>().Select(p => new GetOrder { Id = p.Id, Description = p.Description, Name = p.Name, Count = p.Count, Date = p.Date, OverPrice = p.OverPrice });

            return Ok(order.ToArray());
        }


        // GET: api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetOrder(int id)
        {
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Id)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        //POST
        [ActionName("AddsProduct")]
        [ResponseType(typeof(AddOrder))]
        public IHttpActionResult ProductResponse(AddOrder order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            /*products = new List<Product>()
            {
                db.Product.FirstOrDefault(p=> p.Id == 1),
                db.Product.FirstOrDefault(p=> p.Id == 2),
                db.Product.FirstOrDefault(p=> p.Id == 3),
            };*/
            var count = order.ManuProducts;
            User user = new User();
            user = db.User.FirstOrDefault(u => u.Login == order.LoginUser);
            var NewOrder = new Order() { Name = user.Login + "Count" + order.ManuProducts.Count(),
                Description = order.Description,
                Date = DateTime.Now, Status = db.Status.FirstOrDefault(s => s.Name == "Новый"),
                User = user, Count = order.ManuProducts.Count(), OverPrice = order.ManuProducts.Sum(o=> o.Price) };
            db.Order.Add(NewOrder);
            db.SaveChanges();
            var products = order.ManuProducts;
            foreach (var item in products)
            {
                var product = db.Product.FirstOrDefault(p => p.Name == item.Name);
                if (product != null)
                {
                    db.OrderAndProduct.Add(new OrderAndProduct() { Order = NewOrder, Product = product });
                }
            }
            db.SaveChanges();

            return Ok();


        }

        // POST: api/Orders
        [ResponseType(typeof(Order))]
        public IHttpActionResult PostOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Order.Add(order);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Order.Remove(order);
            db.SaveChanges();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Order.Count(e => e.Id == id) > 0;
        }
    }
}