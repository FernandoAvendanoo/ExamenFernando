using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Trabajo.Models;

namespace Trabajo.Controllers
{
    public class ClientesController : Controller
    {
        ExamenEntities1 examen = new ExamenEntities1();

        // GET: Clientes
        public ActionResult Clientes()
        {

            List<catSexo> sexoData = examen.catSexo.ToList();
            ViewBag.sexoData = sexoData;

            List<catEstados> estadosData = examen.catEstados.ToList();
            ViewBag.estadosData = estadosData;

            List<catOrigen> origenData = examen.catOrigen.ToList();
            ViewBag.origenData = origenData;

            List<tblClientes> data = examen.tblClientes.ToList();
            ViewBag.data = data;

            //Para cargar el dropdown
            ViewBag.comboEstado = examen.catEstados.Select(x => new SelectListItem
            {
                Text = x.descripcion,
                Value = x.idEstado.ToString()

            });

            ViewBag.comboOrigen = examen.catOrigen.Select(x => new SelectListItem
            {
                Text = x.descripcion,
                Value = x.idOrigen.ToString()

            });

            ViewBag.comboSexo = examen.catSexo.Select(x => new SelectListItem
            {
                Text = x.descripcion,
                Value = x.idSexo.ToString()

            });

            return View();
        }

        [HttpPost]
        public ActionResult Clientes(tblClientes clien, HttpPostedFileBase imagen)
        {
            clien.fechaingreso = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));

            if (TempData["msj"] != null)
            {
                ViewBag.msj = TempData["msj"];
            }
            string action = Request.Form["boton"].ToString();
            string eliminacion = Request.Form["eliminacion"].ToString();
            string modificacion = Request.Form["modificacion"].ToString();

            if (imagen != null && imagen.ContentLength > 0)
            {
                // Procesa la imagen aquí (por ejemplo, guarda en una ubicación específica)
                byte[] var = getMD5(imagen.FileName);
                clien.Fotografia = var;
                string rutaImagen = Server.MapPath("~/archivos/") + Path.GetFileName(imagen.FileName);
                imagen.SaveAs(rutaImagen);
                // También puedes guardar la ruta de la imagen en la base de datos si es necesario
            }

            switch (action)
            {
                case "Guardar":
                    examen.tblClientes.Add(clien);
                    examen.SaveChanges();
                    TempData["msj"] = "Guardado";
                    break;

                case "Modificar":
                    if (modificacion.Equals("si"))
                    {
                        tblClientes temp = examen.tblClientes.FirstOrDefault(x => x.idClientes == clien.idClientes);
                        temp.PrimerNombre = clien.PrimerNombre;
                        temp.SegundoNombre = clien.SegundoNombre;
                        temp.PrimerApellido = clien.PrimerApellido;
                        temp.SegundoApellido = clien.SegundoApellido;
                        temp.TercerApellido = clien.TercerApellido;
                        temp.idSexo = clien.idSexo;
                        temp.Fotografia = clien.Fotografia;
                        temp.idEstado = clien.idEstado;
                        temp.idOrigen = clien.idOrigen;

                        examen.SaveChanges();
                        TempData["msj"] = "Modificado";
                    }
                    break;

                case "Eliminar":
                    if (eliminacion.Equals("si"))
                    {
                        examen.tblClientes.Remove(examen.tblClientes.FirstOrDefault(x => x.idClientes == clien.idClientes));
                        examen.SaveChanges();
                        TempData["msj"] = "Eliminado";

                    }
                    break;
            }
            return RedirectToAction("Clientes");
        }

        protected byte[] getMD5(string str)
        {
            
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);

            return stream;
        }

    }
}