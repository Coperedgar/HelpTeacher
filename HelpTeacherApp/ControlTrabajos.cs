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
    public partial class ControlTrabajos : Form
    {
        SqlConnection conexion = new SqlConnection(Properties.Settings.Default.HelpTeacherConnectionString);
        public ControlTrabajos()
        {
            InitializeComponent();
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
        private void ControlTrabajos_Load(object sender, EventArgs e)
        {
            ConsultarAlumnos();
            ConsultarTrabajos();
            DgvTrabajos.Columns[0].Visible = false;
        }
        private void ConsultarTrabajos()
        {
            ConsultarDatos("SELECT IdTrabajo, Nombre, Grado, Grupo, Generacion as Generación, Trimestre, Fecha, NombreTrabajo, Calificacion FROM Trabajos ", DgvTrabajos);
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
            DtpFecha.Value = DateTime.Now;
            TxtNombreTrabajo.Clear();
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
                    string.IsNullOrWhiteSpace(TxtNombreTrabajo.Text))
                {
                    MessageBox.Show("Todos los campos son obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Detener la ejecución del método si hay campos vacíos
                }

                conexion.Open();
                SqlCommand comando = new SqlCommand("INSERT INTO Trabajos VALUES(@Nombre, @Grado, @Grupo, @Generacion, @Trimestre, @Fecha, @NombreTrabajo, @Calificacion)", conexion);

                // Asignar valores a los parámetro
                comando.Parameters.AddWithValue("@Nombre", TxtNombreR.Text);
                comando.Parameters.AddWithValue("@Grado", TxtGrado.Text);
                comando.Parameters.AddWithValue("@Grupo", TxtGrupo.Text);
                comando.Parameters.AddWithValue("@Generacion", TxtGeneracionR.Text);
                comando.Parameters.AddWithValue("@Trimestre", CmbTrimestre.Text);
                comando.Parameters.AddWithValue("@Fecha", DtpFecha.Value.ToString("yyyy-MM-dd"));
                comando.Parameters.AddWithValue("@NombreTrabajo", TxtNombreTrabajo.Text);
                comando.Parameters.AddWithValue("@Calificacion", NumCalificacion.Value);

                comando.ExecuteNonQuery(); //Ejecuta el comando en la base de datos
                MessageBox.Show("Registro insertado con éxito!!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConsultarTrabajos();
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
        private void DgvTrabajos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                TxtId.Text = DgvTrabajos.SelectedCells[0].Value.ToString();
                TxtNombreR.Text = DgvTrabajos.SelectedCells[1].Value.ToString();
                TxtGrado.Text = DgvTrabajos.SelectedCells[2].Value.ToString();
                TxtGrupo.Text = DgvTrabajos.SelectedCells[3].Value.ToString();
                TxtGeneracionR.Text = DgvTrabajos.SelectedCells[4].Value.ToString();
                CmbTrimestre.Text = DgvTrabajos.SelectedCells[5].Value.ToString();
                DateTime DtpFecha = DateTime.Parse(DgvTrabajos.SelectedCells[6].Value.ToString());
                TxtNombreTrabajo.Text = DgvTrabajos.SelectedCells[7].Value.ToString();
                NumCalificacion.Value = Convert.ToDecimal(DgvTrabajos.SelectedCells[8].Value.ToString());
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
        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            try
            {

                conexion.Open();
                string query = "UPDATE Trabajos SET Trimestre = @Trimestre, Calificacion = @Calificacion, Fecha = @Fecha, NombreTrabajo = @NombreTrabajo WHERE IdTrabajo = @IdTrabajo";
                SqlCommand comando = new SqlCommand(query, conexion);

                // Parámetros para evitar SQL Injection y problemas de formato
                comando.Parameters.AddWithValue("@NombreTrabajo", TxtNombreTrabajo.Text);
                comando.Parameters.AddWithValue("@Trimestre", CmbTrimestre.Text); // Nuevo valor de NumeroLista
                comando.Parameters.AddWithValue("@Calificacion", NumCalificacion.Value);
                comando.Parameters.AddWithValue("@IdTrabajo", TxtId.Text);
                comando.Parameters.AddWithValue("@Fecha", DtpFecha.Value.ToString("yyyy-MM-dd"));


                comando.ExecuteNonQuery(); //Ejecuta el comando en la base de datos
                conexion.Close();

                MessageBox.Show("Datos actualizados con éxito!!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                ConsultarTrabajos();
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
                SqlCommand comando = new SqlCommand("DELETE FROM Trabajos WHERE IdTrabajo=" + TxtId.Text + "", conexion);
                comando.ExecuteNonQuery(); //Ejecuta el comando en la base de datos
                MessageBox.Show("Registro eliminado con éxito!!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conexion.Close();
                ConsultarTrabajos();
                Limpiar();
            }
            else
            {
                MessageBox.Show("Error al eliminar!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Limpiar();
            }
        }

        private void BtnBuscarPLista_Click(object sender, EventArgs e)
        {
            ConsultarDatos("SELECT *FROM Trabajos where Nombre like '%" + TxtNombreT.Text + "%' and Grado like '%" + CmbGradoT.Text + "%' and Grupo like '%" + CmbGrupoT.Text + "%' and Trimestre like '%" + CmbTrimestreT.Text + "%' and NombreTrabajo like '%" + TxtTrabajoT.Text + "%'", DgvTrabajos);
        }

        private void BtnCancelarPLista_Click(object sender, EventArgs e)
        {
            TxtNombreT.Clear();
            CmbGradoT.SelectedIndex = -1;
            CmbGrupoT.SelectedIndex = -1;
            CmbTrimestreT.SelectedIndex = -1;
            TxtTrabajoT.Clear();
            ConsultarTrabajos();
        }
    }
}
