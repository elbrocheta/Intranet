using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;
using static IntranetData.Enums;

namespace WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("WebApi/Noticias")]
    public class NoticiasController : ApiController
    {
        private PruebasIntranet2019Entities db = new PruebasIntranet2019Entities();

        [AllowAnonymous]
        [Route("GetAll")]
        [HttpGet]
        public IQueryable<Noticias> GetNoticias()
        {       
            return db.Noticias;
        }

        [AllowAnonymous]
        [Route("GetAllIndex")]
        [HttpGet]
        public IQueryable<Noticias> GetNoticiasIndex()
        {
            db.Configuration.LazyLoadingEnabled = false;

            return db.Noticias.Where(x => x.MostrarEnInicio == 1);
        }

        [AllowAnonymous]
        [Route("GetAllByType")]
        [HttpGet]
        // GET: api/Noticias/5
        [ResponseType(typeof(Noticias))]
        public IHttpActionResult GetNoticias(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;

            IQueryable<Noticias> noticias = db.Noticias.Where(x => x.NoticiaTipo == id);

            if (noticias == null)
            {
                return NotFound();
            }

            return Ok(noticias);
        }

        // PUT: api/Noticias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNoticias(int id, Noticias noticias)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != noticias.Id)
            {
                return BadRequest();
            }

            db.Entry(noticias).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoticiasExists(id))
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

        // POST: api/Noticias
        [ResponseType(typeof(Noticias))]
        public IHttpActionResult PostNoticias(Noticias noticias)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Noticias.Add(noticias);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = noticias.Id }, noticias);
        }

        // DELETE: api/Noticias/5
        [ResponseType(typeof(Noticias))]
        public IHttpActionResult DeleteNoticias(int id)
        {
            Noticias noticias = db.Noticias.Find(id);
            if (noticias == null)
            {
                return NotFound();
            }

            db.Noticias.Remove(noticias);
            db.SaveChanges();

            return Ok(noticias);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NoticiasExists(int id)
        {
            return db.Noticias.Count(e => e.Id == id) > 0;
        }
    }
}