using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using IntranetData;


namespace Front.Helpers
{
    public class WebApiHelper
    {
        private static readonly String URL_WEBAPI = ConfigurationManager.AppSettings["URL_API"];

        private static String URL_WEBAPI_NOTICIAS = "/WebApi/Noticias/";
        private static String URL_WEBAPI_MENU = "/WebApi/Menu/";
        private static String URL_WEBAPI_SECURITY = "/WebApi/Security/";


        public static readonly String ENDPOINT_NOTICIAS_GETALL = URL_WEBAPI + URL_WEBAPI_NOTICIAS + "GetAll";
        public static readonly String ENDPOINT_NOTICIAS_GETINDEX = URL_WEBAPI + URL_WEBAPI_NOTICIAS + "GetAllIndex";
        public static readonly String ENDPOINT_NOTICIAS_GETBYTYPE = URL_WEBAPI + URL_WEBAPI_NOTICIAS + "GetAllByType?id=";

        public static readonly String ENDPOINT_MENU_GETALL = URL_WEBAPI + URL_WEBAPI_MENU + "GetAll";
        public static readonly String ENDPOINT_MENU_GETBYGRUPO = URL_WEBAPI + URL_WEBAPI_MENU + "GetByGrupo?id=";
        public static readonly String ENDPOINT_MENU_GETBYMODULO = URL_WEBAPI + URL_WEBAPI_MENU + "GetByModulo?id=";



        public static HttpClient p_APESAD_HttpClient()
        {
            HttpClient _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return _client;
        }
    }
}