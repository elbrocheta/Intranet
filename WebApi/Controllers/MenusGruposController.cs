using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("WebApi/MenuGrupos")]
    public class MenusGruposController : ApiController
    {
        private PruebasIntranet2019Entities db = new PruebasIntranet2019Entities();

        [Route("GetAll")]
        [HttpGet]
        public IQueryable<vBack_GruposMenuList> GetAll()
        {          
            return db.vBack_GruposMenuList.OrderByDescending(x => x.Orden);
        }

        [Route("GetOne")]
        [HttpGet]
        public IQueryable<MenuGrupo> GetOne(int id)
        {
            return db.MenuGrupo.Where(x => x.Id == id);
        }


        [Route("Update")]
        [HttpPut]
        public IHttpActionResult Update(MenuGrupo model)
        {
            try
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();

        }

        [Route("Create")]
        [HttpPost]
        public IHttpActionResult Create(MenuGrupo model)
        {
            try
            {
                db.Entry(model).State = EntityState.Added;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [Route("Delete")]
        [HttpDelete]
        public IHttpActionResult Delete(int id) {


            return Ok();
        }
    }
}
