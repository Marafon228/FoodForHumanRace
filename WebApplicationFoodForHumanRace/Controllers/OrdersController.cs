using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebApplicationFoodForHumanRace.Models;
using WebApplicationFoodForHumanRace.Models.Json;

namespace WebApplicationFoodForHumanRace.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OrdersController : ApiController
    {
        private ADO db = new ADO();



        // GET: api/TodoItems/5
        [HttpGet]
        public IHttpActionResult GetOrderFromId(int id)
        {
            var order = db.Order.Find(id);
            return Ok(new GetOrderAllRowResponse { Id = order.Id, Name = order.Name, Description = order.Description, Date = order.Date, IdUser = order.IdUser, IdStatus = order.IdStatus, OverPrice = order.OverPrice, Count = order.Count });
        }

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

            var order = db.Order.ToList<Order>().Where(o=> o.Status.Name == "Новый").Select(p => new GetOrder { Id = p.Id, Description = p.Description, Name = p.Name, Count = p.Count, Date = p.Date, OverPrice = p.OverPrice, Staff = p.Staff, IdStatus = p.IdStatus, IdUser = p.IdUser });

            return Ok(order.ToArray());
        }

        //Get
        [ActionName("GetOrdersFromUserId")]
        [ResponseType(typeof(List<Order>))]
        public IHttpActionResult GetOrdersFromUserId(int id)
        {
            User user = db.User.FirstOrDefault(u => u.Id == id);

            //return Ok(db.Product.ToList().ConvertAll(p => new ProductResponse(p)));

            var order = db.Order.ToList<Order>().
                                                  Where(o=> o.IdUser == user.Id).
                                                  Select(p => new GetOrderResponse { Id = p.Id, Description = p.Description, Name = p.Name, Count = p.Count, Date = p.Date, OverPrice = p.OverPrice, Status = p.Status.Name });

            return Ok(order.ToArray());
        }

        //Get
        [ActionName("GetOrdersFromStaffId")]
        [ResponseType(typeof(List<Order>))]
        public IHttpActionResult GetOrdersFromStaffId(int id)
        {
            try
            {
                User user = db.User.FirstOrDefault(u => u.Id == id);

                //return Ok(db.Product.ToList().ConvertAll(p => new ProductResponse(p)));

                var order = db.Order.ToList<Order>().
                                                      Where(o => o.Staff == user.Id).
                                                      Select(p => new GetOrderResponse { Id = p.Id, Description = p.Description, Name = p.Name, Count = p.Count, Date = p.Date, OverPrice = p.OverPrice, Status = p.Status.Name });

                return Ok(order.ToArray());
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }           
            
        }


        //Get
        [ActionName("GetOrdersFromUserIdForEmployee")]
        [ResponseType(typeof(List<Order>))]
        public IHttpActionResult GetOrdersFromUserIdForEmployee()
        {
            

            var order = db.Order.ToList<Order>().
                                                  Where(o => o.IdStatus == 1).
                                                  Select(p => new GetOrderResponse { Id = p.Id, Description = p.Description, Name = p.Name, Count = p.Count, Date = p.Date, OverPrice = p.OverPrice, Status = p.Status.Name });

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

        [HttpPut]
        public void EditOrderForStatus(int id, [FromBody] Order order)
        {
            
            if (id == order.Id)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
            }
        }



        [HttpPut]
        public void EditOrder(int id, [FromBody] Order order)
        {

            

            if (id == order.Id)
            {
                db.Entry(order).State = EntityState.Modified;              
                db.SaveChanges();
            }
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
        //POST Не работает используй нижний 
        [ActionName("AddsProductAdds")]
        [ResponseType(typeof(AddOrder))]
        public IHttpActionResult ProductResponses(AddOrder order)
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
            var NewOrder = new Order()
            {
                Name = user.Login + "Count" + order.ManuProducts.Count(),
                Description = order.Description,
                Date = DateTime.Now,
                Status = db.Status.FirstOrDefault(s => s.Name == "Новый"),
                User = user,
                Count = order.ManuProducts.Count(),
                OverPrice = order.ManuProducts.Sum(o => o.Price)
            };
            db.Order.Add(NewOrder);
            db.SaveChanges();
            var products = order.ManuProducts;
            foreach (var item in products)
            {
                var product = db.Product.FirstOrDefault(p => p.Name == item.Name);
                if (product != null)
                {
                    db.OrderAndProduct.Add(new OrderAndProduct() { Order = NewOrder, Product = product, Quantity = item.Quantity });
                }
            }
            db.SaveChanges();

            return Ok();


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
            var count = order.ManuProducts;
            User user = new User();
            user = db.User.FirstOrDefault(u => u.Login == order.LoginUser);
            var NewOrder = new Order() { Name = "User Login:" + user.Login + " " + "Count:" + order.ManuProducts.Count(),
                Description = "Описание " + order.Description + " Адрес " + user.Adress,
                Date = DateTime.Now, Status = db.Status.FirstOrDefault(s => s.Name == "Новый"),
                User = user,
                Count = order.ManuProducts.Count(),
                OverPrice = order.ManuProducts.Sum(o => o.Price),
                Staff = 0
            };
            db.Order.Add(NewOrder);
            db.SaveChanges();
            var products = order.ManuProducts;
            foreach (var item in products)
            {
                var product = db.Product.FirstOrDefault(p => p.Name == item.Name);
                if (product != null)
                {
                    db.OrderAndProduct.Add(new OrderAndProduct() { Order = NewOrder, Product = product, Quantity = item.Quantity });
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