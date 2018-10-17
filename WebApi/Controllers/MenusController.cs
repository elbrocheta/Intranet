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
               
        [Route("GetAll")]
        [HttpGet]
        public IQueryable<Menu> GetAll()
        {
            return db.Menu.OrderBy(x => x.Orden);
        }

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
    }
}