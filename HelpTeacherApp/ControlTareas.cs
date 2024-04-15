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

namespace HelpTeacherApp
{
    public partial class ControlTareas : Form
    {
        SqlConnection conexion = new SqlConnection(Properties.Settings.Default.HelpTeacherConnectionString);
        public ControlTareas()
        {
            InitializeComponent();
        }

        private void ControlTareas_Load(object sender, EventArgs e)
        {
            ConsultarAlumnos();
            ConsultarTarea();
            DgvTareas.Columns[0].Visible = false;
        }
        private void ConsultarDatos(String sql, DataGridView DB)
        {
            SqlDataAdapter cargador = new SqlDataAdapter(sql, conexion);
            DataSet datos = new DataSet();
            cargador.Fill(datos, "f");
            DB.DataSource = datos.Tables["f"];
        }
        private void ConsultarAlumnos()
        {
            ConsultarDatos("SELECT NumeroLista As [#Lista], Nombre, ApellidoPaterno As [Apellido Paterno], ApellidoMaterno As[Apellido Materno], Grado, Grupo, Generacion as Generación FROM Alumnos ", DgvAlumnos);
        }
        private void ConsultarTarea()
        {
            ConsultarDatos("SELECT IdTarea, Nombre, Grado, Grupo, Generacion as Generación, Trimestre, Fecha, NombreTarea, Calificacion FROM Tareas ", DgvTareas);
        }
        private void BtnBuscarAlumno_Click(object sender, EventArgs e)
        {
            ConsultarDatos("SELECT NumeroLista As [#Lista], Nombre, ApellidoPaterno As [Apellido Paterno], ApellidoMaterno As[Apellido Materno], Grado, Grupo, Generacion as Generación FROM Alumnos where Nombre like '%" + TxtNombre.Text + "%' and Grado like '%" + CmbGrado.Text + "%' and Grupo like '%" + CmbGrupo.Text + "%' and Generacion like '%" + TxtGeneracion.Text + "%'", DgvAlumnos);
        }

        private void BtnCancelarB_Click(object sender, EventArgs e)
        {
            TxtNombre.Clear();
            // Limpiar ComboBox
            CmbGrado.SelectedIndex = -1;
            CmbGrupo.SelectedIndex = -1;
            TxtGeneracion.Clear();
            ConsultarAlumnos();
        }
        private void Limpiar()
        {
            TxtNombreR.Clear();
            TxtGrado.Clear();
            TxtGrupo.Clear();
            TxtGeneracionR.Clear();
            CmbTrimestre.SelectedIndex = -1;
            TxtNombreTarea.Clear();
            DtpFecha.Value = DateTime.Now;
            NumCalificacion.Value = 0;
            BtnActualizar.Enabled = false;
            BtnEliminar.Enabled = false;
        }
        private void DgvAlumnos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                TxtId.Text = DgvAlumnos.SelectedCells[0].Value.ToString();
                TxtNombreR.Text = DgvAlumnos.SelectedCells[1].Value.ToString();
                TxtGrado.Text = DgvAlumnos.SelectedCells[4].Value.ToString();
                TxtGrupo.Text = DgvAlumnos.SelectedCells[5].Value.ToString();
                TxtGeneracionR.Text = DgvAlumnos.SelectedCells[6].Value.ToString();

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

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {


                // Verificar si alguno de los campos está vacío
                if (CmbTrimestre.SelectedIndex == -1 ||
                    string.IsNullOrWhiteSpace(TxtNombreTarea.Text))
                {
                    MessageBox.Show("Todos los campos son obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Detener la ejecución del método si hay campos vacíos
                }

                conexion.Open();
                SqlCommand comando = new SqlCommand("INSERT INTO Tareas VALUES(@Nombre, @Grado, @Grupo, @Generacion, @Trimestre, @Fecha, @NombreTarea, @Calificacion)", conexion);

                // Asignar valores a los parámetro
                comando.Parameters.AddWithValue("@Nombre", TxtNombreR.Text);
                comando.Parameters.AddWithValue("@Grado", TxtGrado.Text);
                comando.Parameters.AddWithValue("@Grupo", TxtGrupo.Text);
                comando.Parameters.AddWithValue("@Generacion", TxtGeneracionR.Text);
                comando.Parameters.AddWithValue("@Trimestre", CmbTrimestre.Text);
                comando.Parameters.AddWithValue("@Fecha", DtpFecha.Value.ToString("yyyy-MM-dd"));
                comando.Parameters.AddWithValue("@NombreTarea", TxtNombreTarea.Text);
                comando.Parameters.AddWithValue("@Calificacion", NumCalificacion.Value);

                comando.ExecuteNonQuery(); //Ejecuta el comando en la base de datos
                MessageBox.Show("Registro insertado con éxito!!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConsultarTarea();
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

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            try
            {

                conexion.Open();
                string query = "UPDATE Tareas SET Trimestre = @Trimestre, Calificacion = @Calificacion, Fecha = @Fecha, NombreTarea = @NombreTarea WHERE IdTarea = @IdTarea";
                SqlCommand comando = new SqlCommand(query, conexion);

                // Parámetros para evitar SQL Injection y problemas de formato
                comando.Parameters.AddWithValue("@NombreTarea", TxtNombreTarea.Text);
                comando.Parameters.AddWithValue("@Trimestre", CmbTrimestre.Text); // Nuevo valor de NumeroLista
                comando.Parameters.AddWithValue("@Calificacion", NumCalificacion.Value);
                comando.Parameters.AddWithValue("@IdTarea", TxtId.Text);
                comando.Parameters.AddWithValue("@Fecha", DtpFecha.Value.ToString("yyyy-MM-dd"));


                comando.ExecuteNonQuery(); //Ejecuta el comando en la base de datos
                conexion.Close();

                MessageBox.Show("Datos actualizados con éxito!!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                ConsultarTarea();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el registro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Limpiar();
            }
        }

        private void DgvTareas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                TxtId.Text = DgvTareas.SelectedCells[0].Value.ToString();
                TxtNombreR.Text = DgvTareas.SelectedCells[1].Value.ToString();
                TxtGrado.Text = DgvTareas.SelectedCells[2].Value.ToString();
                TxtGrupo.Text = DgvTareas.SelectedCells[3].Value.ToString();
                TxtGeneracionR.Text = DgvTareas.SelectedCells[4].Value.ToString();
                CmbTrimestre.Text = DgvTareas.SelectedCells[5].Value.ToString();
                DateTime DtpFecha = DateTime.Parse(DgvTareas.SelectedCells[6].Value.ToString());
                TxtNombreTarea.Text = DgvTareas.SelectedCells[7].Value.ToString();
                NumCalificacion.Value = Convert.ToDecimal(DgvTareas.SelectedCells[8].Value.ToString());
                BtnEliminar.Enabled = true;
                BtnActualizar.Enabled = true;

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

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult resultado;
            resultado = MessageBox.Show("¿Segur@ que desea eliminar el registro seleccionado?", "Confirmar!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("DELETE FROM Tareas WHERE IdTarea=" + TxtId.Text + "", conexion);
                comando.ExecuteNonQuery(); //Ejecuta el comando en la base de datos
                MessageBox.Show("Registro eliminado con éxito!!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conexion.Close();
                ConsultarTarea();
                Limpiar();
            }
            else
            {
                MessageBox.Show("Error al eliminar!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Limpiar();
            }
        }


        private void BtnBuscarTarea_Click(object sender, EventArgs e)
        {
            ConsultarDatos("SELECT *FROM Tareas where Nombre like '%" + TxtNombreT.Text + "%' and Grado like '%" + CmbGradoT.Text + "%' and Grupo like '%" + CmbGrupoT.Text + "%' and Trimestre like '%" + CmbTrimestreT.Text + "%' and NombreTarea like '%" + TxtTareaT.Text + "%'", DgvTareas);
        }

        private void BtnCancelarTarea_Click(object sender, EventArgs e)
        {
            TxtNombreT.Clear();
            CmbGradoT.SelectedIndex = -1;
            CmbGrupoT.SelectedIndex = -1;
            CmbTrimestreT.SelectedIndex = -1;
            TxtTareaT.Clear();
            ConsultarTarea();
        }
    }
}
