using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HelpTeacherApp
{
    public partial class ControlEvidencias : Form
    {
        SqlConnection conexion = new SqlConnection(Properties.Settings.Default.HelpTeacherConnectionString);
        private string imagenevidencia;
        public ControlEvidencias()
        {
            InitializeComponent();
        }

        private void ControlEvidencias_Load(object sender, EventArgs e)
        {
            TxtNota.Focus();
            BtnActualizar.Enabled = false;
            BtnEliminar.Enabled = false;
            ConsultarEvidencias();
            DgvEvidencias.Columns[0].Visible = false;
            DgvEvidencias.Columns[5].Visible = false;

        }
        private void ConsultarDatos(String sql, DataGridView DB)
        {
            SqlDataAdapter cargador = new SqlDataAdapter(sql, conexion);
            DataSet datos = new DataSet();
            cargador.Fill(datos, "f");
            DB.DataSource = datos.Tables["f"];
        }
        private void ConsultarEvidencias()
        {
            ConsultarDatos("SELECT IdEvidencias, Fecha, Grado, Grupo, Nota, Imagen FROM Evidencias ", DgvEvidencias);
        }
        private void BtnSubirImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen (*.jpg, *.jpeg, *.png, *.gif)|*.jpg;*.jpeg;*.png;*.gif|Todos los archivos (*.*)|*.*";
            openFileDialog.Title = "Seleccionar imagen";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Obtener la ruta de la imagen seleccionada
                string rutaImagen = openFileDialog.FileName;

                // Mostrar la imagen en el PictureBox
                PtbEvidencia.Image = Image.FromFile(rutaImagen);

                // Guardar la ruta de la imagen en una variable para usarla posteriormente
                imagenevidencia = rutaImagen; // Asegúrate de declarar "imagenUsuarioBase64" como una variable a nivel de clase
                TxtNota.Focus();
            }
        }
        private void Limpiar()
        {
            TxtNota.Clear();
            CmbGradoE.SelectedIndex = -1;
            CmbGrupoE.SelectedIndex = -1;
            PtbEvidencia.Image = Properties.Resources.imagenes;
            BtnActualizar.Enabled = false;
            BtnEliminar.Enabled = false;
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                
                // Verificar si alguno de los campos está vacío
                if (string.IsNullOrWhiteSpace(TxtNota.Text) ||
                    CmbGradoE.SelectedIndex == -1 ||
                    CmbGrupoE.SelectedIndex == -1)
                {
                    MessageBox.Show("Todos los campos son obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Detener la ejecución del método si hay campos vacíos
                }

                conexion.Open();
                SqlCommand comando = new SqlCommand("INSERT INTO Evidencias VALUES(@Fecha, @Grado, @Grupo, @Nota, @Imagen)", conexion);

                // Asignar valores a los parámetros
                comando.Parameters.AddWithValue("@Fecha", DtpFecha.Value.ToString("yyyy-MM-dd"));
                comando.Parameters.AddWithValue("@Grado", CmbGradoE.Text);
                comando.Parameters.AddWithValue("@Grupo", CmbGrupoE.Text);
                comando.Parameters.AddWithValue("@Nota", TxtNota.Text);
                comando.Parameters.AddWithValue("@Imagen", imagenevidencia);

                comando.ExecuteNonQuery(); //Ejecuta el comando en la base de datos
                MessageBox.Show("Registro insertado con éxito!!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConsultarEvidencias();
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar registro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Limpiar();
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        private void DgvEvidencias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                TxtId.Text = DgvEvidencias.SelectedCells[0].Value.ToString();
                DateTime DtpFecha = DateTime.Parse(DgvEvidencias.SelectedCells[1].Value.ToString());
                CmbGradoE.Text = DgvEvidencias.SelectedCells[2].Value.ToString();
                CmbGrupoE.Text = DgvEvidencias.SelectedCells[3].Value.ToString();
                TxtNota.Text = DgvEvidencias.SelectedCells[4].Value.ToString();
                PtbEvidencia.ImageLocation = DgvEvidencias.SelectedCells[5].Value.ToString();
                
                BtnActualizar.Enabled = true;
                BtnEliminar.Enabled = true;
            }
            catch
            {
                Limpiar();
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar si ya existe un alumno con los mismos datos excepto el ID del alumno que se está actualizando


                conexion.Open();
                string query = "UPDATE Evidencias SET Grado = @Grado, Grupo = @Grupo, Nota = @Nota, Imagen = @Imagen WHERE IdEvidencias = @IdEvidencia";
                SqlCommand comando = new SqlCommand(query, conexion);

                // Parámetros para evitar SQL Injection y problemas de formato
                comando.Parameters.AddWithValue("@Grado", CmbGradoE.Text); // Nuevo valor de NumeroLista
                comando.Parameters.AddWithValue("@Grupo", CmbGrupoE.Text);
                comando.Parameters.AddWithValue("@Nota", TxtNota.Text);
                comando.Parameters.AddWithValue("@Imagen", imagenevidencia);
                comando.Parameters.AddWithValue("@IdEvidencia", TxtId.Text);


                comando.ExecuteNonQuery(); //Ejecuta el comando en la base de datos
                conexion.Close();

                MessageBox.Show("Datos actualizados con éxito!!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                ConsultarEvidencias();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el registro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Limpiar();
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult resultado;
            resultado = MessageBox.Show("¿Segur@ que desea eliminar el registro seleccionado?", "Confirmar!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("DELETE FROM Evidencias WHERE IdEvidencias=" + TxtId.Text + "", conexion);
                comando.ExecuteNonQuery(); //Ejecuta el comando en la base de datos
                MessageBox.Show("Registro eliminado con éxito!!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conexion.Close();
                ConsultarEvidencias();
                Limpiar();
            }
            else
            {
                MessageBox.Show("Error al eliminar!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Limpiar();
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
           
            ConsultarDatos("SELECT *FROM Evidencias where Grado like '%" + CmbGrado.Text + "%' and Grupo like '%" + CmbGrupo.Text + "%' AND CONVERT(date, Fecha) = '" + DtpFechaEvidencia.Value.ToString("yyyy-MM-dd") + "'", DgvEvidencias);
        }

        private void BtnCancelarB_Click(object sender, EventArgs e)
        {
            CmbGrado.SelectedIndex = -1;
            CmbGrupo.SelectedIndex = -1;
            DtpFechaEvidencia.Value = DateTime.Now;
            ConsultarEvidencias();

        }
    }
}
