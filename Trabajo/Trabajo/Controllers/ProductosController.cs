using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trabajo.Models;

namespace Trabajo.Controllers
{
    public class ProductosController : Controller
    {
        ExamenEntities1 examen = new ExamenEntities1();
        // GET: Productos
        public ActionResult Productos()
        {
            List<catProductos> data = examen.catProductos.ToList();
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
        public ActionResult Productos(catProductos cProd)
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
                    examen.catProductos.Add(cProd);
                    examen.SaveChanges();
                    TempData["msj"] = "Guardado";
                    break;

                case "Modificar":
                    if (modificacion.Equals("si"))
                    {
                        catProductos temp = examen.catProductos.FirstOrDefault(x => x.idTipoproducto == cProd.idTipoproducto);
                        temp.descripcion = cProd.descripcion;
                        temp.idEstado = cProd.idEstado;
                        examen.SaveChanges();
                        TempData["msj"] = "Modificado";
                    }
                    break;

                case "Eliminar":
                    if (eliminacion.Equals("si"))
                    {
                        examen.catProductos.Remove(examen.catProductos.FirstOrDefault(x => x.idTipoproducto == cProd.idTipoproducto));
                        examen.SaveChanges();
                        TempData["msj"] = "Eliminado";

                    }
                    break;
            }
            return RedirectToAction("Productos");
        }

    }
}