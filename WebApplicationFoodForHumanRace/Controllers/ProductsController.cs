using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.IO;
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
    public class ProductsController : ApiController
    {
        private ADO db = new ADO();


        // GET: api/TodoItems/5
        [HttpGet]
        public IHttpActionResult GetProductFromId(int id)
        {
            var product = db.Product.Find(id);
            return Ok(new GetProductResponse { Id = product.Id,Name = product.Name ,Description = product.Description, Price = product.Price, Image = product.Image });
        }

        // GET: api/Products
        public IQueryable<Product> GetProduct()
        {
            
            return db.Product;
        }
        /*// GET product
        public  IQueryable<Product> GetProductFromId11(int id)
        {
            var product = db.Product.Find(id);
            return (IQueryable<Product>)Ok(product);
        }*/

        //Get

        [ActionName("GetProducts")]
        [ResponseType(typeof(List<Product>))]
        public IHttpActionResult GetProducts()
        {
            User user = db.User.FirstOrDefault(u=> u.Id == 1);

            //return Ok(db.Product.ToList().ConvertAll(p => new ProductResponse(p)));

            var product = db.Product.ToList<Product>().Select(p => new GetProductResponse { Id = p.Id, Description = p.Description, Name = p.Name, Price = p.Price, Image = p.Image });

           /* MemoryStream ms = new MemoryStream(product.FirstOrDefault().Image);
            var imag = Image.FromStream(ms);*/

            

            return Ok(product.ToArray());
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {

            /*Product product = db.Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }*/


            var product = db.Product.ToList<Product>().Select(p => new GetProductResponse { Id = p.Id, Description = p.Description, Name = p.Name, Price = p.Price, Image = p.Image });

            

            return Ok(product.Where(p=> p.Id == id));
        }

        [HttpPut]
        public void EditProduct(int id, [FromBody] Product product)
        {
            if (id == product.Id)
            {
                db.Entry(product).State = EntityState.Modified;

                db.SaveChanges();
            }
        }


        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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
        //Post
        [ActionName("OrderNew")]
        [ResponseType(typeof(AddOrder))]
        public IHttpActionResult AuthSignIn(AddOrder order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var products = new ManuProduct();
            //var NewOrder = new Order() { Name = order.Name, Date = DateTime.Now, Description = order.Description, Status = db.Status.FirstOrDefault(s => s.Name == "Новый") };

           // db.Order.Add(NewOrder);
           // db.SaveChanges();
            //var product = new Product() { Id = products.Id, Price = products.Price, Description = products.Description, Name = products.Name, Image = products.Image };

            return Ok(/*new OrderResponse() { Id = NewOrder.Id }*/);




        }

        //POST
        [ActionName("POSTProducts")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult ProductResponse(List<Product> products/*, User user*/)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            /*User user = new User();
            user = db.User.FirstOrDefault(u=> u.Id == 1);
            products = db.Product.ToList();
            var NewOrder = new Order() {Name = products.Name, Date = DateTime.Now, Status = db.Status.FirstOrDefault(s => s.Name == "Новый"), User = user};
            db.Order.Add(NewOrder);
            db.SaveChanges();
            var OrdrerAndProducts = new OrderAndProduct() { Order = NewOrder, Product = products};
            db.OrderAndProduct.Add(OrdrerAndProducts);
            db.SaveChanges();       
            return Ok(new OrderResponse() { Id = NewOrder.Id });*/
            //var orderAndProduct = new List<OrderAndProduct>();

            products = new List<Product>()
            {
                db.Product.FirstOrDefault(p=> p.Id == 1),
                db.Product.FirstOrDefault(p=> p.Id == 2),
                db.Product.FirstOrDefault(p=> p.Id == 3),
            };
            User user = new User();
            user = db.User.FirstOrDefault(u => u.Id == 1);
            var NewOrder = new Order() { Name = user.Login+"Count"+products.Count, Date = DateTime.Now, Status = db.Status.FirstOrDefault(s => s.Name == "Новый"), User = user };
            db.Order.Add(NewOrder);
            db.SaveChanges();
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
        //POST
        [ActionName("AddProduct")]
        [ResponseType(typeof(ProductResponse))]
        public IHttpActionResult ProductRespons(ProductResponse products, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var NewOrder = new Order() { Name = products.Name, Date = DateTime.Now, Status = db.Status.FirstOrDefault(s => s.Name == "Новый"), User = user };
            db.Order.Add(NewOrder);
            db.SaveChanges();
            //var OrdrerAndProducts = new List<OrderAndProduct>(db.Product.ToList().Select(pu => new OrderAndProduct() { Order = NewOrder, Product = pu }));
            //db.OrderAndProduct.Add(OrderAndProducts);

            return Ok(new OrderResponse() { Id = NewOrder.Id });




        }

        //POST WEB

        [ActionName("AddProductWeb")]
        [ResponseType(typeof(ProductResponse))]
        public IHttpActionResult AddProductWeb(ProductResponse products)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var NewProduct = new Product() { Name = products.Name, Description = products.Description, Price = products.Price, Image = products.Image };

            db.Product.Add(NewProduct);
            db.SaveChanges();
            

            return Ok();




        }

        // POST: api/Products
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Product.Add(product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Product.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Product.Count(e => e.Id == id) > 0;
        }
    }
}