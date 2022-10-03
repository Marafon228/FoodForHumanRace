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
    public class EnterprisesController : ApiController
    {
        private ADO db = new ADO();

        // GET: api/Enterprises
        /*public IQueryable<Enterprise> GetEnterprise()
        {
            return db.Enterprise;
        }*/


        //GET
        [ActionName("GetEnterpris")]
        [ResponseType(typeof(List<Enterprise>))]
        public IHttpActionResult GetEnterpris()
        {

            var enterprise = db.Enterprise.ToList<Enterprise>().Select(e => new GetEnterpriseResponse { Id = e.Id, Name = e.Name, IdType = e.IdType, Address = e.Address, Latitude = e.Latitude, Longitude = e.Longitude });

            return Ok(enterprise.ToArray());
        }

        // GET: api/Enterprises/5
        /*[ResponseType(typeof(Enterprise))]
        public IHttpActionResult GetEnterprise(int id)
        {
            Enterprise enterprise = db.Enterprise.Find(id);
            if (enterprise == null)
            {
                return NotFound();
            }

            return Ok(enterprise);
        }*/

        // PUT: api/Enterprises/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEnterprise(int id, Enterprise enterprise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != enterprise.Id)
            {
                return BadRequest();
            }

            db.Entry(enterprise).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnterpriseExists(id))
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

        // POST: api/Enterprises
        [ResponseType(typeof(Enterprise))]
        public IHttpActionResult PostEnterprise(Enterprise enterprise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Enterprise.Add(enterprise);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = enterprise.Id }, enterprise);
        }

        // DELETE: api/Enterprises/5
        [ResponseType(typeof(Enterprise))]
        public IHttpActionResult DeleteEnterprise(int id)
        {
            Enterprise enterprise = db.Enterprise.Find(id);
            if (enterprise == null)
            {
                return NotFound();
            }

            db.Enterprise.Remove(enterprise);
            db.SaveChanges();

            return Ok(enterprise);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EnterpriseExists(int id)
        {
            return db.Enterprise.Count(e => e.Id == id) > 0;
        }
    }
}