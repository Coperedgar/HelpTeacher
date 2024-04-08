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
    public partial class Alumnos : Form
    {
        SqlConnection conexion = new SqlConnection(Properties.Settings.Default.HelpTeacherConnectionString); //Variable de conectividad hacia la base de datos
        
       
        public Alumnos()
        {
            InitializeComponent();
            
            
        }
        private void ConsultarAlumnos()
        {
            ConsultarDatos("SELECT Id, NumeroLista As [#Lista], Nombre, ApellidoPaterno As [Apellido Paterno], ApellidoMaterno As[Apellido Materno], Grado, Grupo, Generacion as Generación FROM Alumnos ", DgvAlumnos);
        }
        private void Alumnos_Load(object sender, EventArgs e)
        {
            TxtNombreR.Focus();
            ConsultarAlumnos();
            DgvAlumnos.Columns[0].Visible = false;
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


        private void Alumnos_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void TxtNombre_Enter(object sender, EventArgs e)
        {

        }




        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar si ya existe un alumno con el mismo nombre y número de lista
                if (ExisteAlumno())
                {
                    MessageBox.Show("Ya existe un alumno con los mismos datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Salir del método sin registrar el alumno
                }

                conexion.Open();
                SqlCommand comando = new SqlCommand("INSERT INTO Alumnos VALUES(@NumeroLista, @Nombre, @ApellidoPaterno, @ApellidoMaterno, @Grado, @Grupo, @Generacion)", conexion);

                // Asignar valores a los parámetros
                comando.Parameters.AddWithValue("@NumeroLista", NumLista.Value);
                comando.Parameters.AddWithValue("@Nombre", TxtNombreR.Text);
                comando.Parameters.AddWithValue("@ApellidoPaterno", TxtApellidoPR.Text);
                comando.Parameters.AddWithValue("@ApellidoMaterno", TxtApellidoMR.Text);
                comando.Parameters.AddWithValue("@Grado", CmbGradoR.Text);
                comando.Parameters.AddWithValue("@Grupo", CmbGrupoR.Text);
                comando.Parameters.AddWithValue("@Generacion", TxtGeneracionR.Text);

                comando.ExecuteNonQuery(); //Ejecuta el comando en la base de datos
                MessageBox.Show("Registro insertado con éxito!!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConsultarAlumnos();
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

        private bool ExisteAlumno()
        {
            try
            {
                // Abrir la conexión
                conexion.Open();

                // Consultar si ya existe un alumno con el mismo nombre y número de lista
                SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM Alumnos WHERE Nombre = @Nombre AND NumeroLista = @NumeroLista AND Generacion = @Generacion AND Grado = @Grado AND Grupo = @Grupo", conexion);
                comando.Parameters.AddWithValue("@Nombre", TxtNombreR.Text);
                comando.Parameters.AddWithValue("@NumeroLista", NumLista.Value);
                comando.Parameters.AddWithValue("@Generacion", TxtGeneracionR.Text);
                comando.Parameters.AddWithValue("@Grado", CmbGradoR.Text);
                comando.Parameters.AddWithValue("@Grupo", CmbGrupoR.Text);

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



        private void Limpiar()
        {
            // Limpiar TextBox
            TxtNombreR.Clear();
            TxtApellidoPR.Clear();
            TxtApellidoMR.Clear();
            TxtId.Clear();
            


      

            //Numeric Up Down en 0
            NumLista.Value = 1;

            //Foco en el Nombre
            TxtNombreR.Focus();
            BtnActualizar.Enabled = false;
            BtnEliminar.Enabled = false;
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void DgvAlumnos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                TxtId.Text = DgvAlumnos.SelectedCells[0].Value.ToString();
                NumLista.Value = Convert.ToDecimal(DgvAlumnos.SelectedCells[1].Value.ToString());
                TxtNombreR.Text = DgvAlumnos.SelectedCells[2].Value.ToString();
                TxtApellidoPR.Text = DgvAlumnos.SelectedCells[3].Value.ToString();
                TxtApellidoMR.Text = DgvAlumnos.SelectedCells[4].Value.ToString();
                CmbGradoR.Text = DgvAlumnos.SelectedCells[5].Value.ToString();
                CmbGrupoR.Text = DgvAlumnos.SelectedCells[6].Value.ToString();
                TxtGeneracionR.Text = DgvAlumnos.SelectedCells[7].Value.ToString();
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
                if (ExisteAlumnoExceptoActual(Convert.ToInt32(TxtId.Text)))
                {
                    MessageBox.Show("Ya existe un alumno con los mismos datos, por favor, introduzca datos diferentes.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // No continuar con la actualización si ya existe un alumno con los mismos datos
                }

                conexion.Open();
                string query = "UPDATE Alumnos SET NumeroLista = @NuevoNumeroLista, Nombre = @Nombre, ApellidoPaterno = @ApellidoPaterno, ApellidoMaterno = @ApellidoMaterno, Grado = @Grado, Grupo = @Grupo, Generacion = @Generacion WHERE Id = @Id";
                SqlCommand comando = new SqlCommand(query, conexion);

                // Parámetros para evitar SQL Injection y problemas de formato
                comando.Parameters.AddWithValue("@NuevoNumeroLista", NumLista.Value.ToString()); // Nuevo valor de NumeroLista
                comando.Parameters.AddWithValue("@Nombre", TxtNombreR.Text);
                comando.Parameters.AddWithValue("@ApellidoPaterno", TxtApellidoPR.Text);
                comando.Parameters.AddWithValue("@ApellidoMaterno", TxtApellidoMR.Text);
                comando.Parameters.AddWithValue("@Grado", CmbGradoR.Text);
                comando.Parameters.AddWithValue("@Grupo", CmbGrupoR.Text);
                comando.Parameters.AddWithValue("@Generacion", TxtGeneracionR.Text);
                comando.Parameters.AddWithValue("@Id", TxtId.Text);

                comando.ExecuteNonQuery(); //Ejecuta el comando en la base de datos
                conexion.Close();

                MessageBox.Show("Datos actualizados con éxito!!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar();
                ConsultarAlumnos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el registro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Limpiar();
            }
        }

        private bool ExisteAlumnoExceptoActual(int idAlumnoActual)
        {
            try
            {
                conexion.Open();

                // Consultar si ya existe un alumno con los mismos datos excepto el ID del alumno actual
                SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM Alumnos WHERE Nombre = @Nombre AND NumeroLista = @NumeroLista AND Generacion = @Generacion AND Grado = @Grado AND Grupo = @Grupo", conexion);
                comando.Parameters.AddWithValue("@Nombre", TxtNombreR.Text);
                comando.Parameters.AddWithValue("@NumeroLista", NumLista.Value);
                comando.Parameters.AddWithValue("@Generacion", TxtGeneracionR.Text);
                comando.Parameters.AddWithValue("@Grado", CmbGradoR.Text);
                comando.Parameters.AddWithValue("@Grupo", CmbGrupoR.Text);
                int count = (int)comando.ExecuteScalar();

                return count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar la existencia del alumno: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
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
                SqlCommand comando = new SqlCommand("DELETE FROM Alumnos WHERE Id=" + TxtId.Text + "", conexion);
                comando.ExecuteNonQuery(); //Ejecuta el comando en la base de datos
                MessageBox.Show("Usuario eliminado con éxito!!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conexion.Close();
                ConsultarAlumnos();
                Limpiar();
            }
            else
            {
                MessageBox.Show("Error al eliminar!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Limpiar();
            }

        }

        private void TxtNombreR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Establecer el foco en el TextBox TxtApellidoPR
                TxtApellidoPR.Focus();

                // Indicar que hemos manejado el evento, para evitar que se procese nuevamente
                e.Handled = true;
                e.SuppressKeyPress = true; // Esto evita que se genere el sonido de Windows al presionar Enter
            }
        }

        private void TxtNombreR_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter presionado no es una letra
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Space)
            {
                // Cancelar la entrada del carácter si no es una letra, una tecla de retroceso o un espacio
                e.Handled = true;
            }
        }

        private void TxtApellidoPR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Establecer el foco en el TextBox TxtApellidoPR
                TxtApellidoMR.Focus();

                // Indicar que hemos manejado el evento, para evitar que se procese nuevamente
                e.Handled = true;
                e.SuppressKeyPress = true; // Esto evita que se genere el sonido de Windows al presionar Enter
            }
        }

        private void TxtApellidoPR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Space)
            {
                // Cancelar la entrada del carácter si no es una letra, una tecla de retroceso o un espacio
                e.Handled = true;
            }
        }

        private void TxtApellidoMR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Desplegar el ComboBox CmbGradoR
                CmbGradoR.Focus();
                CmbGradoR.DroppedDown = true;
                // Indicar que hemos manejado el evento, para evitar que se procese nuevamente
                e.Handled = true;
                e.SuppressKeyPress = true; // Esto evita que se genere el sonido de Windows al presionar Enter
            }
            
        }

        private void TxtApellidoMR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Space)
            {
                // Cancelar la entrada del carácter si no es una letra, una tecla de retroceso o un espacio
                e.Handled = true;
            }
        }

        private void CmbGradoR_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            //CmbGrupoR.DroppedDown = true;
            //CmbGrupoR.Focus();
        }

        private void CmbGradoR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CmbGradoR.SelectedItem != null)
            {
                CmbGrupoR.DroppedDown = true;
                CmbGrupoR.Focus();
            }
        }

        private void CmbGrupoR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CmbGrupoR.SelectedItem != null)
            {
                TxtGeneracionR.Focus();
            }
        }

        private void TxtNombre_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TxtNombre_TextChanged(object sender, EventArgs e)
        {
            
            //ConsultarDatos("SELECT * FROM Alumnos WHERE Nombre LIKE '%" + TxtNombre.Text + "%'", DgvAlumnos);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            ConsultarDatos("SELECT Id, NumeroLista As [#Lista], Nombre, ApellidoPaterno As [Apellido Paterno], ApellidoMaterno As[Apellido Materno], Grado, Grupo, Generacion as Generación FROM Alumnos where Nombre like '%" + TxtNombre.Text + "%' and Grado like '%" + CmbGrado.Text + "%' and Grupo like '%"+ CmbGrupo.Text +"%' and Generacion like '%"+ TxtGeneracion.Text +"%'", DgvAlumnos);
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

        private void tableLayoutPanel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TxtGeneracionR_TextChanged(object sender, EventArgs e)
        {

        }

        private void CmbGrupoR_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TxtApellidoMR_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtApellidoPR_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void NumLista_ValueChanged(object sender, EventArgs e)
        {

        }

        private void TxtNombreR_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void DgvAlumnos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TxtGeneracion_TextChanged(object sender, EventArgs e)
        {

        }

        private void CmbGrado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CmbGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TxtNombre_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void TxtId_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
