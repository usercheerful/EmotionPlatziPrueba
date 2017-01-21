using EmotionPlatzi.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmotionPlatzi.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //El campo en el ViewBag se puede inventar. Gracias a q c# es multiparadigma, permite ser tipado
            //y a su vez dinamico
            ViewBag.WelcomeMessage = "Hola Mundo";
            ViewBag.ValorEntero = 1;
            return View();
        }

        public ActionResult IndexAlternativo()
        {
            var modelo = new Home();
            modelo.WelcomeMessage = "Hola mundo desde el modelo";
            return View(modelo);
        }

        public ActionResult IndexSinLayout()
        {
            var modelo = new Home();
            modelo.WelcomeMessage = "Hola mundo desde el modelo";
            return View(modelo);
        }

        public ActionResult IndexOtro()
        {
            return View();
        }

    }
}