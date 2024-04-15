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
    public partial class ControlExamenes : Form
    {
        SqlConnection conexion = new SqlConnection(Properties.Settings.Default.HelpTeacherConnectionString);
        public ControlExamenes()
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
        private void ControlExamenes_Load(object sender, EventArgs e)
        {
            ConsultarAlumnos();
            ConsultarExamen();
            DgvExamenes.Columns[0].Visible = false;
        }
        private void ConsultarAlumnos()
        {
            ConsultarDatos("SELECT NumeroLista As [#Lista], Nombre, ApellidoPaterno As [Apellido Paterno], ApellidoMaterno As[Apellido Materno], Grado, Grupo, Generacion as Generación FROM Alumnos ", DgvAlumnos);
        }
        private void ConsultarExamen()
        {
            ConsultarDatos("SELECT IdExamen, Nombre, Grado, Grupo, Generacion as Generación, Trimestre, Calificacion FROM Examenes ", DgvExamenes);
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
            NumCalificacion.Value = 0;
            BtnActualizar.Enabled = false;
            BtnEliminar.Enabled = false;
        }
        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                

                // Verificar si alguno de los campos está vacío
                if (CmbTrimestre.SelectedIndex ==-1)
                {
                    MessageBox.Show("Todos los campos son obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Detener la ejecución del método si hay campos vacíos
                }

                conexion.Open();
                SqlCommand comando = new SqlCommand("INSERT INTO Examenes VALUES(@Nombre, @Grado, @Grupo, @Generacion, @Trimestre, @Calificacion)", conexion);

                // Asignar valores a los parámetro
                comando.Parameters.AddWithValue("@Nombre", TxtNombreR.Text);
                comando.Parameters.AddWithValue("@Grado", TxtGrado.Text);
                comando.Parameters.AddWithValue("@Grupo", TxtGrupo.Text);
                comando.Parameters.AddWithValue("@Generacion", TxtGeneracionR.Text);
                comando.Parameters.AddWithValue("@Trimestre", CmbTrimestre.Text);
                comando.Parameters.AddWithValue("@Calificacion", NumCalificacion.Value);

                comando.ExecuteNonQuery(); //Ejecuta el comando en la base de datos
                MessageBox.Show("Registro insertado con éxito!!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConsultarExamen();
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

        private void DgvExamenes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                TxtId.Text = DgvExamenes.SelectedCells[0].Value.ToString();
                TxtNombreR.Text = DgvExamenes.SelectedCells[1].Value.ToString();
                TxtGrado.Text = DgvExamenes.SelectedCells[2].Value.ToString();
                TxtGrupo.Text = DgvExamenes.SelectedCells[3].Value.ToString();
                TxtGeneracionR.Text = DgvExamenes.SelectedCells[4].Value.ToString();
                CmbTrimestre.Text = DgvExamenes.SelectedCells[5].Value.ToString();
                NumCalificacion.Value = Convert.ToDecimal(DgvExamenes.SelectedCells[6].Value.ToString());
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
                // Verificar si ya existe un alumno con los mismos datos excepto el ID del alumno que se está actualizando


                conexion.Open();
                string query = "UPDATE Examenes SET Trimestre = @Trimestre, Calificacion = @Calificacion WHERE IdExamen = @IdExamen";
                SqlCommand comando = new SqlCommand(query, conexion);

                // Parámetros para evitar SQL Injection y problemas de formato
                comando.Parameters.AddWithValue("@Trimestre", CmbTrimestre.Text); // Nuevo valor de NumeroLista
                comando.Parameters.AddWithValue("@Calificacion", NumCalificacion.Value);
                comando.Parameters.AddWithValue("@IdExamen", TxtId.Text);


                comando.ExecuteNonQuery(); //Ejecuta el comando en la base de datos
                conexion.Close();

                MessageBox.Show("Datos actualizados con éxito!!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                ConsultarExamen();
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
                SqlCommand comando = new SqlCommand("DELETE FROM Examenes WHERE IdExamen=" + TxtId.Text + "", conexion);
                comando.ExecuteNonQuery(); //Ejecuta el comando en la base de datos
                MessageBox.Show("Registro eliminado con éxito!!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conexion.Close();
                ConsultarExamen();
                Limpiar();
            }
            else
            {
                MessageBox.Show("Error al eliminar!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Limpiar();
            }
        }

        private void BtnBuscarE_Click(object sender, EventArgs e)
        {
            ConsultarDatos("SELECT *FROM Examenes where Nombre like '%" + TxtNombreE.Text + "%' and Grado like '%" + CmbGradoE.Text + "%' and Grupo like '%" + CmbGrupoE.Text + "%' and Trimestre like '%" + CmbTrimestreE.Text + "%' and Generacion like '%" + TxtGeneracionE.Text + "%'", DgvExamenes);
        }

        private void BtnCancelarE_Click(object sender, EventArgs e)
        {
            TxtNombreE.Clear();
            CmbGradoE.SelectedIndex = -1;
            CmbGrupoE.SelectedIndex = -1;
            CmbTrimestreE.SelectedIndex = -1;
            TxtGeneracionE.Clear();
            ConsultarExamen();

        }
    }
}
