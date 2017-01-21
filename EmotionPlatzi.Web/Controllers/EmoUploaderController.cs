using EmotionPlatzi.Web.Models;
using EmotionPlatzi.Web.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmotionPlatzi.Web.Controllers
{
    public class EmoUploaderController : Controller
    {

        EmotionHelper emoHelper;
        string key;
        EmotionPlatziWebContext db = new EmotionPlatziWebContext();

        //======== REALIZAMOS ESTE BLOQUE PARA EL NOMBRE DE LA CARPETA DONDE SE GUARDAN LAS IMAGENES(Images) 
        //YA QUE HAY QUE CONSIDERAR QUE CUANDO SE ESTA EN PRODUCCION EL NOMBRE DE ESTA CARPETA PUEDE CAMBIAR

        //VERIFICAR WEB.CONFIG, SE LE AGREGO UNA ETIQUETA DE PARAMETRO
        //SE LE AGREGO TAMBIEN PARA EL KEY

        string serverFolderPath;

        public EmoUploaderController()
        {
            

            serverFolderPath = ConfigurationManager.AppSettings["UPLOAD_DIR"];
            
            
            key = ConfigurationManager.AppSettings["EMOTION_KEY"];
            emoHelper = new EmotionHelper(key);
        }
        //=============



        // GET: EmoUploader
        public ActionResult Index()
        {
            return View();
        }

        //Se ejecuta este metodo cuando Enviamos el archivo que cargamos en el fomulario
        [HttpPost]
        public async Task<ActionResult> Index(HttpPostedFileBase file) // <- cambio el nombre porq una linea de codigo ejecuta un metodo asincrono, ---anteriormente estaba asi: public ActionResult Index(HttpPostedFileBase file)
        {
            //verificamos si archivo existe
            if (file!=null && file.ContentLength>0) //otra forma if(file?.ContentLenght>0)
            {
                var pictureName = Guid.NewGuid().ToString(); //generamos un nombre aleatorio
                pictureName += Path.GetExtension(file.FileName); //obtenemos la extension del archivo que enviamos y concatenamos al nombre aleatorio

                //Mapeamos una ruta del servidor a una local
                var route = Server.MapPath(serverFolderPath); //Devuelve una ruta real
                
                //Creamos una ruta completa en el servidor
                route = route + "/" + pictureName;

                //guardamos el archivo en disco
                file.SaveAs(route);

                //para ejecutar un metodo asincrono se le antepone await y tambien cambia el nombre del metodo 
                //se le agrega async Task<ActionResult>
                var emoPicture=await emoHelper.DetectAndExtractFacesAsync(file.InputStream);

                //Agregamos los datos que faltaban(Name y Path) al objeto emoPicture
                emoPicture.Name = file.FileName;
                emoPicture.Path= $"{serverFolderPath}/{pictureName}";

                //Agregamos la fotografia que procesamos
                db.EmoPictures.Add(emoPicture);
                //Guardamos
                await db.SaveChangesAsync();

                return RedirectToAction("Details","EmoPictures",new {Id=emoPicture.Id }); //(Nombre de Accion, Nombre de Controlador, Pasamos parametros)
            }

            return View();
        }

    }
}