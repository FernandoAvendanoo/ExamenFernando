using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trabajo.Models;

namespace Trabajo.Controllers
{
    public class OrigenController : Controller
    {
        ExamenEntities1 examen = new ExamenEntities1();

        // GET: Origen
        public ActionResult Origen()
        {
            List<catOrigen> data = examen.catOrigen.ToList();
            ViewBag.data = data;
            return View();
        }

        [HttpPost]
        public ActionResult Origen(catOrigen corigen)
        {
            if (TempData["msj"] != null)
            {
                ViewBag.msj = TempData["msj"];
            }
            string action = Request.Form["boton"].ToString();
            string eliminacion = Request.Form["eliminacion"].ToString();
            string modificacion = Request.Form["modificacion"].ToString();

            switch (action)
            {
                case "Guardar":
                    examen.catOrigen.Add(corigen);
                    examen.SaveChanges();
                    TempData["msj"] = "Guardado";
                    break;

                case "Modificar":
                    if (modificacion.Equals("si"))
                    {
                        catOrigen temp = examen.catOrigen.FirstOrDefault(x => x.idOrigen == corigen.idOrigen);
                        temp.descripcion = corigen.descripcion;
                        examen.SaveChanges();
                        TempData["msj"] = "Modificado";
                    }
                    break;

                case "Eliminar":
                    if (eliminacion.Equals("si"))
                    {
                        examen.catOrigen.Remove(examen.catOrigen.FirstOrDefault(x => x.idOrigen == corigen.idOrigen));
                        examen.SaveChanges();
                        TempData["msj"] = "Eliminado";

                    }
                    break;
            }
            return RedirectToAction("Origen");
        }
    }
}