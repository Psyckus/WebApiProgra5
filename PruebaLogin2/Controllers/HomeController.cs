using Capa_Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PruebaLogin2.Controllers
{
    public class HomeController : Controller
    {
        private string connectionString = Conexion.cn;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Correcto()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string usuario, string clave)
        {
            string claveEncriptada = ConvertirSha256(clave);

            string resultado = VerificarInicioSesion(usuario, claveEncriptada);

            if (resultado == "Inicio de sesión exitoso")
            {
                string sessionId = HttpContext.Session.SessionID;

                // Crear una nueva cookie con el identificador de sesión
                HttpCookie cookie = new HttpCookie("SessionId", sessionId);

                // Establecer la duración de la cookie (opcional)
                cookie.Expires = DateTime.Now.AddMonths(1); // Por ejem01plo, 1 mes de duración

                // Agregar la cookie a la respuesta
                Response.Cookies.Add(cookie);

                // Resto del código del método Index


                return RedirectToAction("Correcto");
            }
            else
            {
                ViewBag.Error = resultado;
                return View("Index");
            }
        }

        private string VerificarInicioSesion(string usuario, string clave)
        {
            string mensajeError = " Usuario o contraseña incorrectos";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("VerificarInicioSesion", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar los parámetros al comando
                    command.Parameters.AddWithValue("@p_usuario", usuario);
                    command.Parameters.AddWithValue("@p_clave", clave);
                    command.Parameters.Add("@p_mensaje", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();

                    // Obtener el mensaje de salida del procedimiento almacenado
                    string resultado = command.Parameters["@p_mensaje"].Value.ToString();

                    return resultado;
                }
            }

            return mensajeError;
        }
        public static string ConvertirSha256(string texto)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(texto);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

    }
}