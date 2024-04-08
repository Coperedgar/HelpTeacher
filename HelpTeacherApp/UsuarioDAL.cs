using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace HelpTeacherApp
{
    class UsuarioDAL
    {
        public static bool HayUsuariosRegistrados()
        {
            bool hayUsuarios = false;
            string connectionString = Properties.Settings.Default.HelpTeacherConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Usuarios";
                SqlCommand command = new SqlCommand(query, connection);

                // Ejecutar la consulta y obtener el número de usuarios
                int numeroUsuarios = (int)command.ExecuteScalar();

                // Verificar si hay algún usuario registrado
                hayUsuarios = numeroUsuarios > 0;
            }

            return hayUsuarios;
        }

        public static int CrearCuentas(string pUsuaurio, string pContrasenia, string pRutaImagen)
        {
            int resultado = 0;
            SqlConnection Conn = new SqlConnection(Properties.Settings.Default.HelpTeacherConnectionString); //Variable de conectividad hacia la base de datos
            Conn.Open();
            SqlCommand Comando = new SqlCommand(string.Format("Insert Into Usuarios (Nombre, Contraseña, Imagen) values ('{0}', PwdEncrypt('{1}'), '{2}' )", pUsuaurio, pContrasenia, pRutaImagen), Conn);
            resultado = Comando.ExecuteNonQuery();
            Conn.Close();
            return resultado;
        }
        

        public static int Autentificar(string pUsuaurio, string pContrasenia)
        {
            int resultado = -1;
            SqlConnection Conexion = new SqlConnection(Properties.Settings.Default.HelpTeacherConnectionString); //Variable de conectividad hacia la base de datos
            Conexion.Open();
            SqlCommand Comando = new SqlCommand(string.Format(
                "Select * From Usuarios Where Nombre = '{0}'and PwdCompare('{1}', Contraseña) = 1", pUsuaurio, pContrasenia), Conexion);
            SqlDataReader reader = Comando.ExecuteReader();
            while (reader.Read())
            {
                resultado = 50;
            }
            Conexion.Close();
            return resultado;
        }
    }
}
