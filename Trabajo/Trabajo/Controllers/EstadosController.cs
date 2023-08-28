using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trabajo.Models;

namespace Trabajo.Controllers
{
    public class EstadosController : Controller
    {
        // GET: Estados
        ExamenEntities1 examen = new ExamenEntities1();

        public ActionResult Estados()
        {
            List<catEstados> data = examen.catEstados.ToList();
            ViewBag.data = data;
            return View();
        }

        [HttpPost]
        public ActionResult Estados(catEstados cEs)
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
                    examen.catEstados.Add(cEs);
                    examen.SaveChanges();
                    TempData["msj"] = "Guardado";
                    break;

                case "Modificar":
                    if (modificacion.Equals("si"))
                    {
                        catEstados temp = examen.catEstados.FirstOrDefault(x => x.idEstado == cEs.idEstado);
                        temp.descripcion = cEs.descripcion;
                        examen.SaveChanges();
                        TempData["msj"] = "Modificado";
                    }
                    break;

                case "Eliminar":
                    if (eliminacion.Equals("si"))
                    {
                        examen.catEstados.Remove(examen.catEstados.FirstOrDefault(x => x.idEstado == cEs.idEstado));
                        examen.SaveChanges();
                        TempData["msj"] = "Eliminado";

                    }
                    break;
            }
            return RedirectToAction("Estados");
        }

    }
}