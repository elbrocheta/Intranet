using System.Net;
using System.Web.Http;
using WebApi.Helpers;
using WebApi.Models;
using System.DirectoryServices;
using System;
using System.DirectoryServices.AccountManagement;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("WebApi/Security")]
    public class LoginController : ApiController
    {

        [Route("Login")]
        [HttpPost]
        public IHttpActionResult Login(Usuario _user)
        {
            if (_user == null || _user.Login.Trim().Length == 0 || _user.Password.Trim().Length == 0)
                throw new HttpResponseException(HttpStatusCode.BadRequest);           

            try
            {
                //Nos logamos en el AD
                DirectoryEntry entry = new DirectoryEntry(ConfigHelper.AD_URL_ACTIVE_DIRECTORY)
                {
                    Username = _user.Login,
                    Password = _user.Password
                };

                //Filtramos los datos del usuario
                DirectorySearcher ds = new DirectorySearcher(entry)
                {
                    SearchScope = SearchScope.Subtree,
                    Filter = "(&(objectClass=User) (samAccountName=" + _user.Login + "))"
                };

                //Cargamos las propiedades que necesitamos
                ds.PropertiesToLoad.Add("objectguid");
                ds.PropertiesToLoad.Add("sn");
                ds.PropertiesToLoad.Add("givenName");
                ds.PropertiesToLoad.Add("mail");

                SearchResult result = ds.FindOne();

                //Creamos el Token
                var token = TokenGenerator.GenerateTokenJwt(_user.Login);

                //Creamos el objeto del usuario
                _user.Id = BitConverter.ToString((byte[])result.Properties["objectguid"][0]).Replace("-", string.Empty);
                _user.Token = token;
                _user.Nombre = result.Properties["givenName"][0].ToString();
                _user.Apellidos = result.Properties["sn"][0].ToString();
                _user.Password = string.Empty;
                _user.Email = result.Properties["mail"][0].ToString();
                _user.Groups = new List<UsuarioGrupos>();

                //Recuperamos los grupos a los que pertenece el usuario
                using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain))
                {
                    // find a user
                    UserPrincipal user = UserPrincipal.FindByIdentity(ctx, _user.Login);

                    if (user != null)
                    {
                        // get the user's groups
                        var groups = user.GetAuthorizationGroups();                        

                        foreach (GroupPrincipal group in groups)
                        {
                            _user.Groups.Add(new UsuarioGrupos()
                            {
                                Id = group.Guid.ToString(),
                                Nombre = group.Name
                            });
                        }
                    }
                }

                return Ok(_user);

            }
            catch (DirectoryServicesCOMException cex)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
             
        [Route("Logout")]
        [HttpGet]
        public IHttpActionResult Logout()
        {
            return Ok("Logout OK");
        }

    }

}