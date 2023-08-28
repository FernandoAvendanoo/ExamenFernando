using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trabajo.Models;

namespace Trabajo.Controllers
{
    public class ProcesosController : Controller
    {
        ExamenEntities1 examen = new ExamenEntities1();

        // GET: Procesos
        public ActionResult Procesos()
        {

            List<catEstados> estadosData = examen.catEstados.ToList();
            ViewBag.estadosData = estadosData;


            List<catTipoProcesos> data = examen.catTipoProcesos.ToList();
            ViewBag.data = data;

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
        public ActionResult Procesos(catTipoProcesos cProc)
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
                    examen.catTipoProcesos.Add(cProc);
                    examen.SaveChanges();
                    TempData["msj"] = "Guardado";
                    break;

                case "Modificar":
                    if (modificacion.Equals("si"))
                    {
                        catTipoProcesos temp = examen.catTipoProcesos.FirstOrDefault(x => x.idTipoProceso == cProc.idTipoProceso);
                        temp.descripcion = cProc.descripcion;
                        temp.idEstado = cProc.idEstado;
                        examen.SaveChanges();
                        TempData["msj"] = "Modificado";
                    }
                    break;

                case "Eliminar":
                    if (eliminacion.Equals("si"))
                    {
                        examen.catTipoProcesos.Remove(examen.catTipoProcesos.FirstOrDefault(x => x.idTipoProceso == cProc.idTipoProceso));
                        examen.SaveChanges();
                        TempData["msj"] = "Eliminado";

                    }
                    break;
            }
            return RedirectToAction("Procesos");
        }

    }
}