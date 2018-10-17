using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("WebApi/Modulos")]
    public class ModulosController : ApiController
    {
        private PruebasIntranet2019Entities db = new PruebasIntranet2019Entities();

        [AllowAnonymous]
        [Route("GetAll")]
        [HttpGet]
        public IQueryable<Modulos> GetAll()
        {
            return db.Modulos.OrderBy( x => x.Orden);
        }

        [Route("GetOne")]
        [ResponseType(typeof(Modulos))]
        [HttpGet]
        public IHttpActionResult GetOne(int id)
        {
            Modulos modulos = db.Modulos.Find(id);
            if (modulos == null)
            {
                return NotFound();
            }

            return Ok(modulos);
        }

        // PUT: api/Modulos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutModulos(int id, Modulos modulos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != modulos.Id)
            {
                return BadRequest();
            }

            db.Entry(modulos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModulosExists(id))
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

        // POST: api/Modulos
        [ResponseType(typeof(Modulos))]
        public IHttpActionResult PostModulos(Modulos modulos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Modulos.Add(modulos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = modulos.Id }, modulos);
        }

        // DELETE: api/Modulos/5
        [ResponseType(typeof(Modulos))]
        public IHttpActionResult DeleteModulos(int id)
        {
            Modulos modulos = db.Modulos.Find(id);
            if (modulos == null)
            {
                return NotFound();
            }

            db.Modulos.Remove(modulos);
            db.SaveChanges();

            return Ok(modulos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ModulosExists(int id)
        {
            return db.Modulos.Count(e => e.Id == id) > 0;
        }
    }
}