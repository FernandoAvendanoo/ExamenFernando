using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Trabajo.Models;

namespace Trabajo.Controllers
{
    public class APIConsultaController : ApiController
    {
        ExamenEntities1 examen = new ExamenEntities1();

        // GET: api/APIConsulta/{id}
        [HttpGet]
        [Route("api/APIConsulta/{id}")]
        public IHttpActionResult GetTransaccion(int id)
        {
            try
            {
                var transaccion = examen.tblTransacciones.Find(id);

                if (transaccion == null)
                {
                    return NotFound(); // Devuelve 404 si no se encuentra la transacción
                }

                // Mapea la transacción a un objeto anónimo o DTO si es necesario
                var origen = examen.catOrigen.Find(transaccion.idOrigen);

                var transaccionJson = new
                {
                    Id = transaccion.idTransacciones,
                    Fecha = transaccion.fechaTransaccion,
                    monto = transaccion.monto,
                    cliente = transaccion.idClientes,
                    tipoProducto = transaccion.idTipoProducto,
                    tipoProceso = transaccion.idTipoProceso,
                    estadoProceso = transaccion.idEstadoProceso,
                    origen = origen.descripcion
                    // ... otras propiedades que quieras mostrar en JSON
                };

                return Ok(transaccionJson); // Devuelve la transacción en formato JSON
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); // Devuelve 500 en caso de error interno
            }
        }
    }
}

