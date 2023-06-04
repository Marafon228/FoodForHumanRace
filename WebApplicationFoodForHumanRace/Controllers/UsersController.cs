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
    public class UsersController : ApiController
    {
        private ADO db = new ADO();

        // GET: api/Users
        public IQueryable<User> GetUser()
        {
            return db.User;
        }

        // GET: api/Users/5
        [ActionName("Gettsuser")]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.User.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET: api/Users/5
        [ActionName("GetAllUser")]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetAllUser()
        {
            var user = db.User.ToList<User>().Select(u => new GetAllUserResponse { Id = u.Id, Login = u.Login, FirsName = u.FirsName, MiddleName = u.MidleName, 
            LastName = u.LastName, Adress = u.Adress, Email = u.Email, Password = u.Password, Phone = u.Phone, IdRole = u.IdRole });
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.ToArray());
        }

        // GET: api/TodoItems/5
        [HttpGet]
        public IHttpActionResult GetEnterpriseFromUserId(int id)
        {

            var UserEnter = db.UsersAndEnterprise.ToList<UsersAndEnterprise>().Where(u => u.IdUser == id).ToArray();


            //var EntrerpriseIncludeProduct = db.Enterprise.Include(enterpr => enterpr.);
            /*var tp = db.TypesOfProducts.ToList<TypesOfProducts>().Where(e => e.IdEnterprise == id).ToArray();
            var newProduct = new List<Product>();
            var product = db.Product.ToList<Product>();
            foreach (var pr in product)
            {
                for (int i = 0; i < tp.Length; i++)
                {
                    if (pr.TypesOfProducts.FirstOrDefault() == null)
                    {
                        break;
                    }
                    else
                    {
                        if (pr.TypesOfProducts.FirstOrDefault().IdProduct == tp[i].IdProduct)
                        {
                            newProduct.Add(pr);
                        }
                    }
                }

            }*/
            var IdEnterArray = new List<EnterpriseIdResponse>();

            foreach (var en in UserEnter)
            {
                IdEnterArray.Add(new EnterpriseIdResponse() { IdEnterprise = en.IdEnterprise, NameEnterprise = en.Enterprise.Name});           
            }
            
            
            
           
            return Ok(IdEnterArray);
        }


        [HttpPut]
        public void UpdateUser(int id, [FromBody] User user)
        {

            if (id == user.Id)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                Ok();
            }
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
        [ActionName("SignIn")]
        [ResponseType(typeof(AuthSignIn))]
        public IHttpActionResult AuthSignIn(AuthSignIn user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            
            User userNew = db.User.Where(u=> u.Login == user.Login && u.Password == user.Password).FirstOrDefault();

            //user = db.User.FirstOrDefault(u => u.Login == u.Login && u.Password == u.Password);
            //User users = db.User.Where(u=> u.Login == user.Login && u.Password == user.Password).FirstOrDefault();
            //new User() { Role = db.Role.FirstOrDefault(r => r.Name == "Fisherman"), UserLogin = user.UserLogin, UserPassword = user.UserPassword  };

            //{ Role = db.Role.FirstOrDefault(r => r.Name == "Fisher"), };
            //db.User.Add(userNew);
            //db.SaveChanges();

            //var users = new User() {Id = user.Id, Login = user.Login, Password = user.Password, FirsName = user.FirsName , Adress = user.Adress, Email = user.Email, LastName = user.LastName, MidleName = user.MidleName, Phone = user.Phone  };

            //user = ADO.Instance.User.FirstOrDefault();


            if (userNew == null)
            {
                return Unauthorized();//403 не авторизован
            }
            else
            {
                return Ok(new AuthSignInResponse() { Id = userNew.Id, Login = userNew.Login, Password = userNew.Password, FirsName = userNew.FirsName, MidleName = userNew.MidleName, LastName = userNew.LastName, Role = userNew.Role.Name, Adress = userNew.Adress, Email = userNew.Email, Phone = userNew.Phone });
            }




        }


        //POST: api/User
        [ActionName("RegisterUser")]
        [ResponseType(typeof(RegisterUser))]
        public IHttpActionResult RegisterUser(RegisterUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userNew = new User { Role = db.Role.FirstOrDefault(r => r.Name == "Клиент"), Login = user.Login, Password = user.Password, FirsName = user.FirsName, MidleName = user.MidleName, LastName = user.LastName, Adress = user.Adress, Email = user.Email, Phone = user.Phone };
            db.User.Add(userNew);
            db.SaveChanges();

            return Ok(/*new RegisterUserResponse() { Id = userNew.Id, Login = userNew.Login, Password = userNew.Password }*/);




        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.User.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.User.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.User.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.User.Count(e => e.Id == id) > 0;
        }
    }
}