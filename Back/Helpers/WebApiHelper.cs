using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Back.Helpers
{
    public class WebApiHelper
    {

        #region  URL

        private static String URL_WEBAPI = ConfigurationManager.AppSettings["URL_API"];    

        #endregion

        #region Controllers

        private static String URL_WEBAPI_SECURITY = URL_WEBAPI + "/WebApi/Security/";
        private static String URL_WEBAPI_MENU = "/WebApi/Menu/";
        private static String URL_WEBAPI_MODULOS = "/WebApi/Modulos/";
        private static String URL_WEBAPI_MEN_GRUPOS = "/WebApi/MenuGrupos/";

        #endregion

        #region ENDPOINT

        public static String ENDPOINT_LOGIN = URL_WEBAPI_SECURITY + "Login";

        public static readonly String ENDPOINT_MENU_GETALL = URL_WEBAPI + URL_WEBAPI_MENU + "GetAll";
        public static readonly String ENDPOINT_MENU_GETONE = URL_WEBAPI + URL_WEBAPI_MENU + "GetOne?id=";

        public static readonly String ENDPOINT_MODULOS_GETALL = URL_WEBAPI + URL_WEBAPI_MODULOS + "GetAll";

        public static readonly String ENDPOINT_MENU_GRUPOS_GETALL = URL_WEBAPI + URL_WEBAPI_MEN_GRUPOS + "GetAll";
        public static readonly String ENDPOINT_MENU_GRUPOS_GETONE = URL_WEBAPI + URL_WEBAPI_MEN_GRUPOS + "GetOne?id=";
        public static readonly String ENDPOINT_MENU_GRUPOS_UPDATE = URL_WEBAPI + URL_WEBAPI_MEN_GRUPOS + "Update";
        public static readonly String ENDPOINT_MENU_GRUPOS_CREATE = URL_WEBAPI + URL_WEBAPI_MEN_GRUPOS + "Create";
        public static readonly String ENDPOINT_MENU_GRUPOS_DELETE = URL_WEBAPI + URL_WEBAPI_MEN_GRUPOS + "Delete?id=";

        #endregion

        public static HttpClient p_APESAD_HttpClient()
        {
            HttpClient _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return _client;
        }

        public static HttpClient p_APESAD_HttpClient(String token)
        {
            HttpClient _client = p_APESAD_HttpClient();           
            
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

            return _client;
        }

        public static HttpResponseMessage p_AEPSAD_Request(String url)
        {
            try
            {
                HttpClient _client = WebApiHelper.p_APESAD_HttpClient(SessionHelper.p_AEPSAD_get_usuario().Token);

                HttpResponseMessage _response = _client.GetAsync(url).Result;

                return _response;
            }
            catch (Exception ex)
            {
                ErrorHelper.p_AEPSAD_Log(ex);

                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            }
        }
    
    }
}