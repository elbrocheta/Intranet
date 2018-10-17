using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("WebApi/PruebaAcceso")]
    public class PruebaAccesoController : ApiController
    {

        [AllowAnonymous]
        [Route("GetAllowAnonymous")]
        [HttpGet]
        public String GetAllowAnonymous()
        {
            return "Método público";
        }

       
        [Route("GetNotAllowAnonymous")]
        [HttpGet]
        public String GetNotAllowAnonymous()
        {
            return "Método PRIVADO";
        }
    }
}