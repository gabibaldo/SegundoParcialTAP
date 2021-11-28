using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Usuario
    {
        #region PROPIEDADES

        public int? IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int DNI { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public string TipoUsuario { get; set; }

        #endregion


        #region CONSTRUCTORES

        public Usuario()
        { }

        public Usuario(
            string nombre, 
            string apellido, 
            string direccion, 
            int dni,
            string email, 
            string clave, 
            DateTime fechanacimiento, 
            int edad, 
            string tipousuario)
        {
            Nombre = nombre;
            Apellido = apellido;
            DNI = dni;
            Direccion = direccion;
            Email = email;
            Clave = clave;
            FechaNacimiento = fechanacimiento;
            Edad = edad;
            TipoUsuario = tipousuario;
        }

        #endregion


        #region METODOS PUBLICOS

        public static List<Usuario> Listar()
        {

            DataTable dt = new DataTable();
            dt = Datos.Usuarios.Listar();
            List<Usuario> listaUsuarios = new List<Usuario>();

            foreach (DataRow item in dt.Rows)
            {
                listaUsuarios.Add(ArmarDatos(item));
            }

            return listaUsuarios;
        }

        public static Usuario Obtener(int idUsuario)
        {
            DataTable dt = new DataTable();
            dt = Datos.Usuarios.Obtener(idUsuario);

            return ArmarDatos(dt.Rows[0]);
        }

        public static Usuario Obtener(string email, string clave)
        {
            DataTable dt = new DataTable();
            dt = Datos.Usuarios.Obtener(email, clave);

            if (dt.Rows.Count > 0)
                return ArmarDatos(dt.Rows[0]);
            else
                return null;
        }

        public static Usuario Obtener(string email)
        {
            DataTable dt = new DataTable();
            dt = Datos.Usuarios.Obtener(email, "");

            if (dt.Rows.Count > 0)
                return ArmarDatos(dt.Rows[0]);
            else
                return null;
        }

        public static void Eliminar(int idUsuario)
        {
            Datos.Usuarios.Eliminar(idUsuario);
        }

        public int Grabar()
        {
            try
            {
                if (Validar(out string error))
                {
                    if (IdUsuario == null)
                    {
                        return Insertar();
                    }
                    else
                    {
                        return Modificar();
                    }
                }
                else
                    throw new Exception(error);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        #endregion


        #region METODOS PRIVADOS

        private int Insertar()
        {
            return Datos.Usuarios.Insertar(Nombre, Apellido, DNI, Direccion, Email, Utilidades.Seguridad.Encriptar(Clave), FechaNacimiento, Edad, TipoUsuario);
        }

        private int Modificar()
        {
            Datos.Usuarios.Modificar(IdUsuario.Value, Nombre, Apellido, DNI, Direccion, Email, Clave, FechaNacimiento, Edad, TipoUsuario);
            return IdUsuario.Value;
        }

        private bool Validar(out string error)
        {
            error = "";

            if (string.IsNullOrEmpty(Nombre))
                error += "El campo nombre se encuentra vacio; ";

            if (string.IsNullOrEmpty(Apellido))
                error += "El campo apellido se encuentra vacio; ";

            if(DNI > 0)
                error += "El campo dni se encuentra vacio; ";

            if (string.IsNullOrEmpty(Direccion))
                error += "El campo direccion se encuentra vacia; ";

            if (string.IsNullOrEmpty(Email))
                error += "El campo email se encuentra vacia; ";

            if (string.IsNullOrEmpty(Clave))
                error += "El campo clave se encuentra vacia; ";

            if (string.IsNullOrEmpty(error))
                return true;
            else
                return false;
        }

        private static Usuario ArmarDatos(DataRow item)
        {
            Usuario Usuario = new Usuario();

            Usuario.IdUsuario = Convert.ToInt32(item["IdUsuario"]);
            Usuario.Nombre = item["Nombre"].ToString();
            Usuario.Apellido = item["Apellido"].ToString();
            Usuario.DNI = Convert.ToInt32(item["DNI"]);
            Usuario.Direccion = item["Direccion"].ToString();
            Usuario.Email = item["Email"].ToString();
            Usuario.Clave = item["Clave"].ToString();
            //Usuario.FechaNacimiento = ;
            Usuario.Edad = Convert.ToInt32(item["Edad"]);
            Usuario.TipoUsuario = item["TipoUsuario"].ToString();

            return Usuario;
        }

        #endregion
    }
}
