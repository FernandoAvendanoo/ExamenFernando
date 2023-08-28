using Antlr.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trabajo.Models;

namespace Trabajo.Controllers
{
    public class CatSexoController : Controller
    {

        ExamenEntities1 examen = new ExamenEntities1();
        // GET: CatSexo
        public ActionResult CatSexo()
        {
            List<catSexo> data = examen.catSexo.ToList();
            ViewBag.data = data;
            return View();
        }

        [HttpPost]
        public ActionResult CatSexo(catSexo cas)
        {
            if (TempData["msj"] !=  null)
            {
                ViewBag.msj = TempData["msj"];
            }
            string action = Request.Form["boton"].ToString();
            string eliminacion = Request.Form["eliminacion"].ToString();
            string modificacion = Request.Form["modificacion"].ToString();

            switch (action)
            {
                case "Guardar":
                    examen.catSexo.Add(cas);
                    examen.SaveChanges();
                    TempData["msj"] = "Guardado";
                    break;

                case "Modificar":
                    if (modificacion.Equals("si"))
                    {
                        catSexo temp = examen.catSexo.FirstOrDefault(x=> x.idSexo == cas.idSexo);
                        temp.descripcion = cas.descripcion;
                        examen.SaveChanges();
                        TempData["msj"] = "Modificado";
                    }
                    break;

                case "Eliminar":
                    if (eliminacion.Equals("si"))
                    {
                        examen.catSexo.Remove(examen.catSexo.FirstOrDefault(x => x.idSexo == cas.idSexo));
                        examen.SaveChanges();
                        TempData["msj"] = "Eliminado";

                    }
                    break;
            }
            return RedirectToAction("CatSexo");
        }
    }
}