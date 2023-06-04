using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplicationFoodForHumanRace.Models;
using WebApplicationFoodForHumanRace.Models.Json;

namespace WebApplicationFoodForHumanRace.Controllers
{
    public class OrderAndProductsController : ApiController
    {
        private ADO db = new ADO();


        //Get
        [ActionName("GetProductsOrderFromOrderId")]
        [ResponseType(typeof(List<Product>))]
        public IHttpActionResult GetProductsOrderFromOrderId(int id)
        {
            Order order = db.Order.FirstOrDefault(o=> o.Id == id);

            var productsOrder = db.OrderAndProduct.ToList<OrderAndProduct>().Where(o=> o.IdOrder == order.Id);
            var products = new List<GetProductResponse>();
            foreach (var item in productsOrder)
            {
                var product = db.Product.FirstOrDefault(o => o.Id == item.IdProduct);

                var productResponse = new GetProductResponse
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Image = product.Image,
                    Quantity = item.Quantity
                };

                products.Add(productResponse);
            }


            return Ok(products.ToArray());
        }

        // GET: api/OrderAndProducts
        public IQueryable<OrderAndProduct> GetOrderAndProduct()
        {
            return db.OrderAndProduct;
        }

        // GET: api/OrderAndProducts/5
        [ResponseType(typeof(OrderAndProduct))]
        public IHttpActionResult GetOrderAndProduct(int id)
        {
            OrderAndProduct orderAndProduct = db.OrderAndProduct.Find(id);
            if (orderAndProduct == null)
            {
                return NotFound();
            }

            return Ok(orderAndProduct);
        }

        // PUT: api/OrderAndProducts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrderAndProduct(int id, OrderAndProduct orderAndProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderAndProduct.Id)
            {
                return BadRequest();
            }

            db.Entry(orderAndProduct).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderAndProductExists(id))
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

        // POST: api/OrderAndProducts
        [ResponseType(typeof(OrderAndProduct))]
        public IHttpActionResult PostOrderAndProduct(OrderAndProduct orderAndProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrderAndProduct.Add(orderAndProduct);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OrderAndProductExists(orderAndProduct.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = orderAndProduct.Id }, orderAndProduct);
        }

        // DELETE: api/OrderAndProducts/5
        [ResponseType(typeof(OrderAndProduct))]
        public IHttpActionResult DeleteOrderAndProduct(int id)
        {
            OrderAndProduct orderAndProduct = db.OrderAndProduct.Find(id);
            if (orderAndProduct == null)
            {
                return NotFound();
            }

            db.OrderAndProduct.Remove(orderAndProduct);
            db.SaveChanges();

            return Ok(orderAndProduct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderAndProductExists(int id)
        {
            return db.OrderAndProduct.Count(e => e.Id == id) > 0;
        }
    }
}