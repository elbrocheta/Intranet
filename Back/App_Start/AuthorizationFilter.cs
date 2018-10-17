using Back.Helpers;
using System.Web;
using System.Web.Mvc;

namespace Back.App_Start
{
    public class AuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                // Don't check for authorization as AllowAnonymous filter is applied to the action or controller  
                return;
            }

            // Check for authorization  
            if (HttpContext.Current.Session[SessionHelper.SESSION_NAME_USER] == null &&
                HttpContext.Current.Session[SessionHelper.SESSION_NAME_TOKEN] == null)
            {                
                filterContext.Result = new RedirectResult(ConfigHelper.URL_ERROR + "?" + ErrorHelper.ERROR_VARIABLE + "=" + ErrorHelper.ERROR_ACCESS_KEY);
            }
        }
    }

}