using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Trabajo.Controllers
{
    public class APIEnviarController : ApiController
    {
        [HttpPost]
        [Route("api/enviar/transaccion")]  // Ruta de la API

        public IHttpActionResult EnviarTransaccion([FromBody] Transaccion transaccion)
        {
            try
            {
                // Validar la entrada
                if (transaccion == null)
                {
                    return BadRequest("Datos de transacción inválidos.");
                }

                // Conexión a la base de datos
                string connectionString = ConfigurationManager.ConnectionStrings["BDDDirecta"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Crear y configurar el comando para el procedimiento almacenado
                    SqlCommand command = new SqlCommand("ITransaccion", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Agregar parámetros al procedimiento almacenado
                    command.Parameters.AddWithValue("@Parametro2", transaccion.Propiedad2);
                    command.Parameters.AddWithValue("@Parametro3", transaccion.Propiedad3);
                    command.Parameters.AddWithValue("@Parametro4", transaccion.Propiedad4);
                    command.Parameters.AddWithValue("@Parametro5", transaccion.Propiedad5);
                    command.Parameters.AddWithValue("@Parametro6", transaccion.Propiedad6);
                    command.Parameters.AddWithValue("@Parametro7", transaccion.Propiedad7);
                    command.Parameters.AddWithValue("@Parametro8", transaccion.Propiedad8);

                    // Agregar más parámetros según tus necesidades

                    // Ejecutar el procedimiento almacenado
                    command.ExecuteNonQuery();
                }

                return Ok("Transacción enviada y almacenada correctamente.");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }

    public class Transaccion
    {
        public string Propiedad1 { get; set; }
        public string Propiedad2 { get; set; }

        public string Propiedad3 { get; set; }

        public string Propiedad4 { get; set; }

        public string Propiedad5 { get; set; }

        public string Propiedad6 { get; set; }

        public string Propiedad7 { get; set; }

        public string Propiedad8 { get; set; }


        // Agregar más propiedades según tus necesidades
    }

}
