using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trabajo.Models;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using static iTextSharp.text.pdf.AcroFields;

namespace Trabajo.Controllers
{
    public class TransaccionesController : Controller
    {
        // GET: Transacciones
        ExamenEntities1 examen = new ExamenEntities1();

        public ActionResult Transacciones()
        {
            List<tblTransacciones> data = examen.tblTransacciones.ToList();
            ViewBag.data = data;

            List<catOrigen> origenData = examen.catOrigen.ToList();
            ViewBag.origenData = origenData;

            List<tblClientes> clientesData = examen.tblClientes.ToList();
            ViewBag.clientesData = clientesData;

            List<catProductos> productosData = examen.catProductos.ToList();
            ViewBag.productosData = productosData;

            List<catEstadoProcesos> EProceso = examen.catEstadoProcesos.ToList();
            ViewBag.EProceso = EProceso;

            List<catTipoProcesos> tProcesosData = examen.catTipoProcesos.ToList();
            ViewBag.tProcesosData = tProcesosData;

            //Para cargar el dropdown
            ViewBag.comboTProc = examen.catTipoProcesos.Select(x => new SelectListItem
            {
                Text = x.descripcion,
                Value = x.idTipoProceso.ToString()
            });

            ViewBag.comboProd = examen.catProductos.Select(x => new SelectListItem
            {
                Text = x.descripcion,
                Value = x.idTipoproducto.ToString()
            });

            ViewBag.comboEProc = examen.catEstadoProcesos.Select(x => new SelectListItem
            {
                Text = x.descripcion,
                Value = x.idEstadoProceso.ToString()
            });


            ViewBag.comboOrigen = examen.catOrigen.Select(x => new SelectListItem
            {
                Text = x.descripcion,
                Value = x.idOrigen.ToString()
            });

            ViewBag.comboClientes = examen.tblClientes.Select(x => new SelectListItem
            {
                Text = x.PrimerNombre + " " + x.PrimerApellido,
                Value = x.idClientes.ToString()
            });

            return View();
        }

        [HttpPost]
        public ActionResult Transacciones( tblTransacciones trans)
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
                    trans.fechaTransaccion = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                    examen.tblTransacciones.Add(trans);
                    examen.SaveChanges();
                    TempData["msj"] = "Guardado";
                    break;

                case "Modificar":
                    if (modificacion.Equals("si"))
                    {

                        tblTransacciones temp = examen.tblTransacciones.FirstOrDefault(x => x.idTransacciones == trans.idTransacciones);
                        temp.monto = trans.monto;
                        temp.idClientes = trans.idClientes;
                        temp.idTipoProducto = trans.idTipoProducto;
                        temp.idTipoProceso = trans.idTipoProceso;
                        temp.idEstadoProceso = trans.idEstadoProceso;
                        temp.idOrigen = trans.idOrigen;

                        examen.SaveChanges();
                        TempData["msj"] = "Modificado";
                    }
                    break;

                case "Eliminar":
                    if (eliminacion.Equals("si"))
                    {
                        examen.tblTransacciones.Remove(examen.tblTransacciones.FirstOrDefault(x => x.idTransacciones == trans.idTransacciones));
                        examen.SaveChanges();
                        TempData["msj"] = "Eliminado";

                    }
                    break;
            }
            return RedirectToAction("Transacciones");
        }
        public ActionResult GenerarPDF(string id, string des,int tipoP, int cliente, int estadoProc, int tipoProd, int origen)
        {
            Document doc = new Document();
            string nCliente = "";
            string nTipoP = "";
            string nEstadoProc = "";
            string nTipoProd = "";
            string nOrigen = "";

            string filePath = Server.MapPath("~/archivos/transaccion_"+id+".pdf");
            List<tblClientes> dataC = examen.tblClientes.ToList();

            foreach (tblClientes t in dataC)
            {
                if (t.idClientes == cliente){
                    nCliente = t.PrimerNombre.ToString() + t.PrimerApellido;
                }
            }

            List<catEstadoProcesos> dataEP = examen.catEstadoProcesos.ToList();
            foreach (catEstadoProcesos dEP in dataEP)
            {
                if (dEP.idEstadoProceso == estadoProc)
                {
                    nEstadoProc = dEP.descripcion;
                }
            }

            List<catProductos> dataP = examen.catProductos.ToList();
            foreach (catProductos cP in dataP)
            {
                if (cP.idTipoproducto == tipoProd)
                {
                    nTipoProd = cP.descripcion;
                }
            }

            List<catOrigen> dataOr = examen.catOrigen.ToList();
            foreach (catOrigen tOr in dataOr)
            {
                if (tOr.idOrigen == origen)
                {
                    nOrigen = tOr.descripcion;
                }
            }

            List<catTipoProcesos> dataProc = examen.catTipoProcesos.ToList();
            foreach (catTipoProcesos tProc in dataProc)
            {
                if (tProc.idTipoProceso == tipoP)
                {
                    nTipoP = tProc.descripcion;
                }
            }

            // Creamos un flujo de salida para el archivo PDF
            FileStream fs = new FileStream(filePath, FileMode.Create);
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);

            // Abrimos el documento para escribir contenido
            doc.Open();

            // Agregamos contenido al PDF
            Paragraph paragraph = new Paragraph("" +
            "Identificador transaccion: " + id +
            "\n"+
            "Monto transaccion: " + des +
            "\n" +
            "Tipo de producto: " + nTipoProd +
            "\n" +
            "Nombre cliente: " + nCliente +
            "\n" +
            "Tipo de proceso: " + nTipoP +
            "\n" +
            "Estado del proceso: " + nEstadoProc +
            "\n" +
            "Origen: " + nOrigen);
            doc.Add(paragraph);

            // Cerramos el documento y el flujo de salida
            doc.Close();
            fs.Close();
            return RedirectToAction("Transacciones");
        }

    }
}