using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace HelpTeacherApp
{
    public partial class Login : Form
    {
        private string imagenUsuarioBase64;
        public Login()
        {
            InitializeComponent();
        }
        
        SqlConnection conexion = new SqlConnection(Properties.Settings.Default.HelpTeacherConnectionString); //Variable de conectividad hacia la base de datos
        private void Login_Load(object sender, EventArgs e)
        {
            // Ocultar el panel de registro al cargar el formulario
            PnlRegistro.Visible = false;

            // Comprobar si hay usuarios registrados al cargar el formulario
            bool hayUsuariosRegistrados = UsuarioDAL.HayUsuariosRegistrados();

            // Mostrar el panel de registro solo si no hay usuarios registrados
            PnlRegistro.Visible = !hayUsuariosRegistrados;
            PnlLogin.Visible = hayUsuariosRegistrados;

            // Consultar y mostrar el nombre de usuario en TxtNombre
            ConsultarDatosUsuario();
        }

        private void ConsultarDatosUsuario()
        {
            try
            {
                // Abrir la conexión a la base de datos
                conexion.Open();

                // Consulta SQL para obtener el nombre de usuario y la ruta de la imagen de perfil
                string sql = "SELECT Nombre, Imagen FROM Usuarios";

                // Ejecutar la consulta y obtener los datos del usuario
                SqlCommand cmd = new SqlCommand(sql, conexion);
                SqlDataReader reader = cmd.ExecuteReader();

                // Si hay resultados, asignar el nombre de usuario y la imagen de perfil
                if (reader.Read())
                {
                    string nombreUsuario = reader["Nombre"].ToString();
                    string rutaImagen = reader["Imagen"].ToString();

                    // Mostrar el nombre de usuario en el control TxtNombre
                    TxtNombre.Text = nombreUsuario;

                    // Si la ruta de la imagen no está vacía, cargar la imagen en el PictureBox
                    if (!string.IsNullOrEmpty(rutaImagen))
                    {
                        // Cargar la imagen desde la ruta obtenida y mostrarla en el PictureBox
                        PtbUsuario.Image = Image.FromFile(rutaImagen);
                    }
                }

                // Cerrar el lector y la conexión
                reader.Close();
                conexion.Close();
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra
                MessageBox.Show("Error al consultar los datos del usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            if (TxtContrasenia.Text == TxtConfirmar.Text)
            {
                if (UsuarioDAL.CrearCuentas(TxtUsuario.Text, TxtContrasenia.Text, imagenUsuarioBase64) > 0)
                {
                    MessageBox.Show("Cuenta creada con éxito!!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PnlRegistro.Visible = false;
                    ConsultarDatosUsuario();
                    PnlLogin.Visible = true;
                    TxtContra.Focus();
                }
                else
                    MessageBox.Show("No se pudo crear la cuenta!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Las contrseñas no coinciden!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TxtContrasenia.Clear();
                TxtConfirmar.Clear();
                TxtContrasenia.Focus();
            }
                
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnEntrar_Click(object sender, EventArgs e)
        {
            if(UsuarioDAL.Autentificar(TxtNombre.Text, TxtContra.Text) > 0)
            {
                this.Hide();
                Form1 f = new Form1();
                f.ShowDialog();
                

            }
            else
            {
                MessageBox.Show("La contraseña es incorrecta intentalo de nuevo!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TxtContra.Clear();
                TxtContra.Focus();
            }


        }

        private void BtnFoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen (*.jpg, *.jpeg, *.png, *.gif)|*.jpg;*.jpeg;*.png;*.gif|Todos los archivos (*.*)|*.*";
            openFileDialog.Title = "Seleccionar imagen";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Obtener la ruta de la imagen seleccionada
                string rutaImagen = openFileDialog.FileName;

                // Mostrar la imagen en el PictureBox
                PtbUser.Image = Image.FromFile(rutaImagen);

                // Guardar la ruta de la imagen en una variable para usarla posteriormente
                imagenUsuarioBase64 = rutaImagen; // Asegúrate de declarar "imagenUsuarioBase64" como una variable a nivel de clase
            }
        }

        private void TxtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            // Verificar si la tecla presionada es Enter
            if (e.KeyCode == Keys.Enter)
            {
                // Establecer el foco en el TextBox TxtContraseña
                TxtContrasenia.Focus();

                // Indicar que hemos manejado el evento, para evitar que se procese nuevamente
                e.Handled = true;
                e.SuppressKeyPress = true; // Esto evita que se genere el sonido de Windows al presionar Enter
            }
        }

        private void TxtContrasenia_KeyDown(object sender, KeyEventArgs e)
        {
            // Verificar si la tecla presionada es Enter
            if (e.KeyCode == Keys.Enter)
            {
                // Establecer el foco en el TextBox TxtContraseña
                TxtConfirmar.Focus();

                // Indicar que hemos manejado el evento, para evitar que se procese nuevamente
                e.Handled = true;
                e.SuppressKeyPress = true; // Esto evita que se genere el sonido de Windows al presionar Enter
            }
        }

        private void TxtConfirmar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (TxtContrasenia.Text == TxtConfirmar.Text)
                {
                    if (UsuarioDAL.CrearCuentas(TxtUsuario.Text, TxtContrasenia.Text, imagenUsuarioBase64) > 0)
                    {
                        MessageBox.Show("Cuenta creada con éxito!!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        PnlRegistro.Visible = false;
                        PnlLogin.Visible = true;
                    }
                    

                }
                else
                {
                    MessageBox.Show("Las contrseñas no coinciden!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TxtContrasenia.Clear();
                    TxtConfirmar.Clear();
                    TxtContrasenia.Focus();
                }
                // Indicar que hemos manejado el evento, para evitar que se procese nuevamente
                e.Handled = true;
                e.SuppressKeyPress = true; // Esto evita que se genere el sonido de Windows al presionar Enter
            }
        }

        private void TxtContra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (UsuarioDAL.Autentificar(TxtNombre.Text, TxtContra.Text) > 0)
                {
                    this.Hide();
                    Form1 f = new Form1();
                    f.ShowDialog();


                }
                else
                {
                    MessageBox.Show("La contraseña es incorrecta intentalo de nuevo!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TxtContra.Clear();
                }

                // Indicar que hemos manejado el evento, para evitar que se procese nuevamente
                e.Handled = true;
                e.SuppressKeyPress = true; // Esto evita que se genere el sonido de Windows al presionar Enter
            }
        }
    }
}
