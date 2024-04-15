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
    public partial class ControlPaseLista : Form
    {
        SqlConnection conexion = new SqlConnection(Properties.Settings.Default.HelpTeacherConnectionString); //Variable de conectividad hacia la base de datos
        public ControlPaseLista()
        {
            InitializeComponent();
        }

        private void ControlPaseLista_Load(object sender, EventArgs e)
        {
            ConsultarLista();
            ConsultarAlumnos();
            DgvPaseLista.Columns[0].Visible = false;
            BtnActualizar.Enabled = false;
            BtnEliminar.Enabled = false;
            
            
        }
        private void ConsultarDatos(String sql, DataGridView DB)
        {
            SqlDataAdapter cargador = new SqlDataAdapter(sql, conexion);
            DataSet datos = new DataSet();
            cargador.Fill(datos, "f");
            DB.DataSource = datos.Tables["f"];
        }
        private void ConsultarLista()
        {
            ConsultarDatos("SELECT IdLista, Fecha, Nombre, NumeroLista As [#Lista], Grado, Grupo, Generacion as Generación, Asistencia, Motivo FROM PaseLista ", DgvPaseLista);
        }
        private void ConsultarAlumnos()
        {
            ConsultarDatos("SELECT NumeroLista As [#Lista], Nombre, ApellidoPaterno As [Apellido Paterno], ApellidoMaterno As[Apellido Materno], Grado, Grupo, Generacion as Generación FROM Alumnos ", DgvAlumnos);
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckAsistencia.Checked == true)
            {
                LblMotivo.Visible = false;
                TxtMotivo.Visible = false;
                TxtCheked.Text = "Si";
            }
            else
            {
                
                LblMotivo.Visible = true;
                TxtMotivo.Visible = true;
                TxtCheked.Text = "NO";
                TxtMotivo.Focus();
            }
            //LblMotivo.Visible = CheckAsistencia.Checked;
            //TxtMotivo.Visible = CheckAsistencia.Checked;
        }
        private void Limpiar()
        {
            TxtId.Clear();
            DtpFecha.Value = DateTime.Now;
            TxtNombreR.Clear();
            TxtLista.Clear();
            TxtGrado.Clear();
            TxtGrupo.Clear();
            TxtGeneracionR.Clear();
            TxtMotivo.Clear();
            BtnActualizar.Enabled = false;
            BtnEliminar.Enabled = false;
        }
        private void DgvAlumnos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //TxtId.Text = DgvAlumnos.SelectedCells[0].Value.ToString();
                TxtLista.Text = DgvAlumnos.SelectedCells[0].Value.ToString();
                TxtNombreR.Text = DgvAlumnos.SelectedCells[1].Value.ToString();
                TxtGrado.Text = DgvAlumnos.SelectedCells[4].Value.ToString();
                TxtGrupo.Text = DgvAlumnos.SelectedCells[5].Value.ToString();
                TxtGeneracionR.Text = DgvAlumnos.SelectedCells[6].Value.ToString();
                BtnActualizar.Enabled = false;
                BtnEliminar.Enabled = false;
            }
            catch(Exception ex)
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

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar si ya existe un alumno con el mismo nombre y número de lista
                if (ExistePaseLista())
                {
                    MessageBox.Show("El pase de lista ya fue realizado el día de hoy.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Salir del método sin registrar el alumno
                }

                // Verificar si alguno de los campos está vacío
                if (string.IsNullOrWhiteSpace(TxtNombreR.Text) ||
                    string.IsNullOrWhiteSpace(TxtLista.Text) ||
                    string.IsNullOrWhiteSpace(TxtGrado.Text) ||
                    string.IsNullOrWhiteSpace(TxtGrupo.Text) ||
                    string.IsNullOrWhiteSpace(TxtGeneracionR.Text) ||
                    string.IsNullOrWhiteSpace(TxtCheked.Text))
                {
                    MessageBox.Show("Todos los campos son obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Detener la ejecución del método si hay campos vacíos
                }

                conexion.Open();
                SqlCommand comando = new SqlCommand("INSERT INTO PaseLista VALUES(@Fecha, @Nombre, @NumeroLista, @Grado, @Grupo, @Generacion, @Asistencia, @Motivo)", conexion);

                // Asignar valores a los parámetros
                comando.Parameters.AddWithValue("@Fecha", DtpFecha.Value.ToString("yyyy-MM-dd"));
                comando.Parameters.AddWithValue("@Nombre", TxtNombreR.Text);
                comando.Parameters.AddWithValue("@NumeroLista", TxtLista.Text);
                comando.Parameters.AddWithValue("@Grado", TxtGrado.Text);
                comando.Parameters.AddWithValue("@Grupo", TxtGrupo.Text);
                comando.Parameters.AddWithValue("@Generacion", TxtGeneracionR.Text);
                comando.Parameters.AddWithValue("@Asistencia", TxtCheked.Text);
                comando.Parameters.AddWithValue("@Motivo", TxtMotivo.Text);

                comando.ExecuteNonQuery(); //Ejecuta el comando en la base de datos
                MessageBox.Show("Registro insertado con éxito!!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConsultarLista();
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

        private bool ExistePaseLista()
        {
            try
            {
                // Abrir la conexión
                conexion.Open();

                // Consultar si ya existe un alumno con el mismo nombre y número de lista
                SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM PaseLista WHERE Nombre = @Nombre AND NumeroLista = @NumeroLista AND Fecha = @Fecha AND Grado = @Grado AND Grupo = @Grupo", conexion);
                comando.Parameters.AddWithValue("@Nombre", TxtNombreR.Text);
                comando.Parameters.AddWithValue("@NumeroLista", TxtLista.Text);
                comando.Parameters.AddWithValue("@Fecha", DtpFecha.Value.ToString("yyyy-MM-dd"));
                comando.Parameters.AddWithValue("@Grado", TxtGrado.Text);
                comando.Parameters.AddWithValue("@Grupo", TxtGrupo.Text);

                // Ejecutar la consulta y obtener el resultado
                int count = (int)comando.ExecuteScalar();

                // Si count es mayor que cero, significa que ya existe un alumno con los mismos datos
                return count > 0;
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir
                MessageBox.Show("Error al verificar la existencia del alumno: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Retornar false en caso de error
            }
            finally
            {
                // Cerrar la conexión después de usarla
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
        }

        private void DgvPaseLista_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                TxtId.Text = DgvPaseLista.SelectedCells[0].Value.ToString();
                DateTime DtpFecha = DateTime.Parse(DgvPaseLista.SelectedCells[1].Value.ToString());
                TxtNombreR.Text = DgvPaseLista.SelectedCells[2].Value.ToString();
                TxtLista.Text = DgvPaseLista.SelectedCells[3].Value.ToString();
                TxtGrado.Text = DgvPaseLista.SelectedCells[4].Value.ToString();
                TxtGrupo.Text = DgvPaseLista.SelectedCells[5].Value.ToString();
                TxtGeneracionR.Text = DgvPaseLista.SelectedCells[6].Value.ToString();
                TxtCheked.Text = DgvPaseLista.SelectedCells[7].Value.ToString();
                TxtMotivo.Text = DgvPaseLista.SelectedCells[8].Value.ToString();
                if (TxtCheked.Text == "NO")
                {
                    CheckAsistencia.Checked = false;
                }
                else
                {
                    CheckAsistencia.Checked = true;
                }
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
                string query = "UPDATE PaseLista SET Asistencia = @Asistencia, Motivo = @Motivo WHERE IdLista = @IdLista";
                SqlCommand comando = new SqlCommand(query, conexion);

                // Parámetros para evitar SQL Injection y problemas de formato
                comando.Parameters.AddWithValue("@Asistencia", TxtCheked.Text); // Nuevo valor de NumeroLista
                comando.Parameters.AddWithValue("@Motivo", TxtMotivo.Text);
                comando.Parameters.AddWithValue("@IdLista", TxtId.Text);
             

                comando.ExecuteNonQuery(); //Ejecuta el comando en la base de datos
                conexion.Close();

                MessageBox.Show("Datos actualizados con éxito!!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                ConsultarLista();
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
                SqlCommand comando = new SqlCommand("DELETE FROM PaseLista WHERE IdLista=" + TxtId.Text + "", conexion);
                comando.ExecuteNonQuery(); //Ejecuta el comando en la base de datos
                MessageBox.Show("Registro eliminado con éxito!!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conexion.Close();
                ConsultarLista();
                Limpiar();
            }
            else
            {
                MessageBox.Show("Error al eliminar!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Limpiar();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConsultarDatos("SELECT IdLista, Fecha, Nombre, NumeroLista As [#Lista], Grado, Grupo, Generacion as Generación, Asistencia, Motivo FROM PaseLista WHERE Nombre LIKE '%" + TxtNombreList.Text + "%' AND Grado LIKE '%" + cmbGradoList.Text + "%' AND Grupo LIKE '%" + CmbGrupoList.Text + "%' AND CONVERT(date, Fecha) = '" + DtpFechaLista.Value.ToString("yyyy-MM-dd") + "'", DgvPaseLista);

        }

        private void BtnCancelarPLista_Click(object sender, EventArgs e)
        {
            TxtNombreList.Clear();
            // Limpiar ComboBox
            cmbGradoList.SelectedIndex = -1;
            CmbGrupoList.SelectedIndex = -1;
            DtpFechaLista.Value = DateTime.Now;
            ConsultarLista();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void TxtId_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtCheked_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void TxtGeneracionR_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtGrupo_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtGrado_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtLista_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtNombreR_TextChanged(object sender, EventArgs e)
        {

        }

        private void DtpFecha_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TxtMotivo_TextChanged(object sender, EventArgs e)
        {

        }

        private void LblMotivo_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DgvPaseLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void TxtNombreList_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbGradoList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CmbGrupoList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DtpFechaLista_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DgvAlumnos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void TxtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void CmbGrado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CmbGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TxtGeneracion_TextChanged(object sender, EventArgs e)
        {

        }
    }
    
}

