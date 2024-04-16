using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HelpTeacherApp.Inventario
{
    public partial class Inventario : Form
    {
        public Inventario()
        {
            InitializeComponent();
        }
        SqlConnection conexion = new SqlConnection(Properties.Settings.Default.HelpTeacherConnectionString);
        private void Inventario_Load(object sender, EventArgs e)
        {
            MostrarConteoRegistros("Alumnos", LblAlumnosCount);
            MostrarConteoRegistros("PaseLista", LblPaseListaCount);
            MostrarConteoRegistros("Trabajos", LblTrabajosCount);
            MostrarConteoRegistros("Tareas", LblTareasCount);
            MostrarConteoRegistros("Practicas", LblPracticasCount);
            MostrarConteoRegistros("Examenes", LblExamenesCount);
            MostrarConteoRegistros("Evidencias", LblEvidenciasCount);
            
        }
        private void AbrirForm(Form form)
        {
            // Si el formulario "Tiempo" no está abierto, lo abre; de lo contrario, abre el formulario pasado como argumento
            
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Clear();
            this.panelContenedor.Controls.Add(form);
            this.panelContenedor.Tag = form;
            form.Show();
        }
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 log = new Form1();
            log.Show();
        }

        private void BtnCerrarSesion_Click_1(object sender, EventArgs e)
        {
            this.Close();
            Login log = new Login();
            log.Show();
        }

        private void BtnSlider_Click(object sender, EventArgs e)
        {
            if (MenuVertical.Width == 250)
            {
                MenuVertical.Width = 70;
            }
            else
                MenuVertical.Width = 250;
        }
        private void MostrarConteoRegistros(string tabla, Label label)
        {
            try
            {
                {
                    // Consulta SQL para contar los registros en la tabla especificada
                    string query = $"SELECT COUNT(*) FROM {tabla}";

                    SqlCommand command = new SqlCommand(query, conexion);

                    conexion.Open();

                    // Ejecutar la consulta y obtener el resultado
                    int count = (int)command.ExecuteScalar();

                    // Mostrar el resultado en la etiqueta correspondiente
                    label.Text = count.ToString();
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error que pueda ocurrir durante la ejecución de la consulta
                MessageBox.Show($"Error al contar los registros de {tabla}: {ex.Message}");
                conexion.Close();
            }
        }

        private void BtnAlumnos_Click(object sender, EventArgs e)
        {
            AbrirForm(new InventarioAlumnos());
        }

        private void BtnPaseLista_Click(object sender, EventArgs e)
        {
            AbrirForm(new InventarioPaseLista());
        }

        private void BtnTrabajos_Click(object sender, EventArgs e)
        {
            AbrirForm(new InventarioTrabajos());
        }

        private void BtnTareas_Click(object sender, EventArgs e)
        {
            AbrirForm(new InventarioTareas());
        }

        private void BtnPracticas_Click(object sender, EventArgs e)
        {
            AbrirForm(new InventarioPracticas());
        }

        private void BtnExamenes_Click(object sender, EventArgs e)
        {
            AbrirForm(new InventarioExamenes());
        }

        private void BtnEvidencias_Click(object sender, EventArgs e)
        {
            AbrirForm(new InventarioEvidencias());
        }
    }
}
