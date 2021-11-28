using Negocio.Utilidades;
using SegundoParcialTAP.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SegundoParcialTAP.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Login()
        {
            return View();
        }
                
        [HttpPost]
        public ActionResult Ingresar(Negocio.Usuario Usuario)
        {
            try
            {
                Negocio.Usuario UsuarioLogueado = Negocio.Usuario.Obtener(Usuario.Email, Seguridad.Encriptar(Usuario.Clave));
                Session["Usuario"] = UsuarioLogueado;
                
                if (UsuarioLogueado != null)
                    return RedirectToAction("Index", "Home");
                else
                    return RedirectToAction("Error", "Home", new { @Mensaje = "El usuario que quiere ingresar es incorrecto" });
            }
            catch
            {
                return View();
            }
        }

        

        public ActionResult Index()
        {
            try
            {
                using (var db = new PrimerParcialEntities1())
                {
                    return View(db.USUARIO.ToList());
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public ActionResult Agregar()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Agregar(USUARIO usuario)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new PrimerParcialEntities1())
                {
                    db.USUARIO.Add(usuario);
                    db.SaveChanges();
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al agregar al Usuario " + ex.Message);
                return View();
            }
        }

        public ActionResult Editar(int id)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var db = new PrimerParcialEntities1())
                {
                    USUARIO usuario = db.USUARIO.Find(id);
                    return View(usuario);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al modificar el Usuario " + ex.Message);
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(USUARIO usuario)
        {
            try
            {
                using (var db = new PrimerParcialEntities1())
                {
                    USUARIO us = db.USUARIO.Find(usuario.IDUSUARIO);
                    us.NOMBRE = usuario.NOMBRE;
                    us.APELLIDO = usuario.APELLIDO;
                    us.DNI = usuario.DNI;
                    us.DIRECCION = usuario.DIRECCION;
                    us.EMAIL = usuario.EMAIL;
                    us.CLAVE = usuario.CLAVE;
                    us.FECHANACIMIENTO = usuario.FECHANACIMIENTO;
                    us.EDAD = usuario.EDAD;
                    us.TIPOUSUARIO = usuario.TIPOUSUARIO;
                    db.SaveChanges();

                    return RedirectToAction("index");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult Detalles(int id)
        {
            using (var db = new PrimerParcialEntities1())
            {
                USUARIO usuario = db.USUARIO.Find(id);
                return View(usuario);
            }
        }

        public ActionResult Eliminar(int id)
        {
            using (var db = new PrimerParcialEntities1())
            {
                USUARIO usuario = db.USUARIO.Find(id);
                db.USUARIO.Remove(usuario);
                db.SaveChanges();

                return RedirectToAction("index");
            }
        }
    }
}
