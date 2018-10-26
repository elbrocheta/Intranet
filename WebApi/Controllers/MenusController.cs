using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("WebApi/Menu")]
    public class MenusController : ApiController
    {
        private PruebasIntranet2019Entities db = new PruebasIntranet2019Entities();

        #region AllowAnonymous

        [AllowAnonymous]
        [Route("GetByModulo")]
        [ResponseType(typeof(vFront_Menu))]
        [HttpGet]
        public IQueryable<vFront_Menu> GetByModulo(int id)
        {
            IQueryable<vFront_Menu> _list = db.vFront_Menu.Where(x => x.ModuloId == id);

            return _list;
        }

        [AllowAnonymous]
        [Route("GetByGrupo")]
        [ResponseType(typeof(Menu))]
        [HttpGet]
        public IQueryable<Menu> GetByGrupo(int id)
        {
            IQueryable<Menu> _list = db.Menu.Where(x => x.MenuGrupoId == id).OrderBy(x => x.Orden);

            return _list;
        }

        #endregion

        [Route("GetAll")]
        [HttpGet]
        public IQueryable<vBack_Menu> GetAll()
        {
            try
            {
                return db.vBack_Menu.OrderBy(x => x.Orden);
            }
            catch (Exception ex) {
                return null;
            }
        }

        [Route("GetOne")]
        [HttpGet]
        public IQueryable<Menu> GetOne(int id)
        {
            return db.Menu.Where(x => x.Id == id);
        }
           
        [Route("Update")]
        [HttpPut]
        public IHttpActionResult Update(Menu model)
        {
            try
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();

        }

        [Route("Create")]
        [HttpPost]
        public IHttpActionResult Create(Menu model)
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
        public IHttpActionResult Delete(int id)
        {

            try
            {
                List<Menu> _item = db.Menu.Where(x => x.Id == id).ToList();

                if (_item == null || _item.Count <= 0)
                    return NotFound();

                db.Menu.Remove(_item[0]);
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}