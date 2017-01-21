using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace EmotionPlatzi.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            //Serializacion-> Proceso donde convertimos un objeto en un formato
            //que lo podamos transportar

            //SI ENCUENTRA REFERENCIAS CIRCULARES QUE LAS IGNORE
            //PARA EL CASO DE EMOPICTURE TIENE UNA REFERENCIA CON EMOFACES, ASU VEZ EL EMOFACE TIENE REFERENCIAS A EMPOPICTURE 
            //config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            //VER LAS DIFERENCIAS DEL CODIGO COMENTADO DE ARRIBA CON LAS DOS LINEAS DE CODIGO CONFIG DE ABAJO, CON LA APLICACION JaSON, DE LOS JSON QUE DEVUELVE LA API

            //======
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;

            

        }
    }
}
