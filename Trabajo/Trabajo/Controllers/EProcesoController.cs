using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trabajo.Models;

namespace Trabajo.Controllers
{
    public class EProcesoController : Controller
    {
        // GET: EProceso
        ExamenEntities1 examen = new ExamenEntities1();

        public ActionResult EProceso()
        {
            List<catEstadoProcesos> data = examen.catEstadoProcesos.ToList();
            ViewBag.data = data;

            List<catEstados> estadosData = examen.catEstados.ToList();
            ViewBag.estadosData = estadosData;


            //Para cargar el dropdown
            ViewBag.combo = examen.catEstados.Select(x => new SelectListItem
            {
                Text = x.descripcion,
                Value = x.idEstado.ToString()
            });

            ViewBag.comboEstado = examen.catEstados.Select(x => new SelectListItem
            {
                Text = x.descripcion,
                Value = x.idEstado.ToString()
            });
            return View();
        }

        [HttpPost]
        public ActionResult EProceso(catEstadoProcesos cProc)
        {
            cProc.fechaingreso = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
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
                    examen.catEstadoProcesos.Add(cProc);
                    examen.SaveChanges();
                    TempData["msj"] = "Guardado";
                    break;

                case "Modificar":
                    if (modificacion.Equals("si"))
                    {
                        catEstadoProcesos temp = examen.catEstadoProcesos.FirstOrDefault(x => x.idEstadoProceso == cProc.idEstadoProceso);
                        temp.descripcion = cProc.descripcion;
                        temp.idEstado = cProc.idEstado;
                        examen.SaveChanges();
                        TempData["msj"] = "Modificado";
                    }
                    break;

                case "Eliminar":
                    if (eliminacion.Equals("si"))
                    {
                        examen.catEstadoProcesos.Remove(examen.catEstadoProcesos.FirstOrDefault(x => x.idEstadoProceso == cProc.idEstadoProceso));
                        examen.SaveChanges();
                        TempData["msj"] = "Eliminado";

                    }
                    break;
            }
            return RedirectToAction("EProceso");
        }

    }
}