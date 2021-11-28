using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SegundoParcialTAP.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Login()
        {
            return View();
        }


        public ActionResult RecuperarClave(Negocio.Usuario Usuario)
        {
            try
            {
                Negocio.Usuario UsuarioLogueado = Negocio.Usuario.Obtener(Usuario.Email);
                Session["Usuario"] = UsuarioLogueado;

                if (UsuarioLogueado != null)
                {
                    Negocio.Utilidades.Email.Enviar(
                       ConfigurationManager.AppSettings["UsuarioCorreo"].ToString(),
                       UsuarioLogueado.Email,
                       "Esba",
                       UsuarioLogueado.Nombre,
                       ConfigurationManager.AppSettings["UsuarioCorreo"].ToString(),
                       ConfigurationManager.AppSettings["Clave"].ToString(),
                       "Recuperar Contraseña",
                       UsuarioLogueado.Email + " Tu clave es: " + Negocio.Utilidades.Seguridad.DesEncriptar(UsuarioLogueado.Clave),
                       false,
                       ConfigurationManager.AppSettings["Host"].ToString(),
                       Convert.ToInt32(ConfigurationManager.AppSettings["Puerto"].ToString()),
                       Convert.ToBoolean(ConfigurationManager.AppSettings["UsaSSL"].ToString()));

                    return RedirectToAction("Index", "Home");
                }
                else
                    return RedirectToAction("Error", "Home", new { @Mensaje = "El usuario que quiere ingresar es incorrecto" });
            }
            catch
            {
                return View();
            }
        }
    }
}