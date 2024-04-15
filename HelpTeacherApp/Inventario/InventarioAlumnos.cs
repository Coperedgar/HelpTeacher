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
using OfficeOpenXml;

namespace HelpTeacherApp.Inventario
{
    public partial class InventarioAlumnos : Form
    {
        public InventarioAlumnos()
        {
            InitializeComponent();
        }

        private void InventarioAlumnos_Load(object sender, EventArgs e)
        {

        }
        private void ExportarDatosAExcel()
        {
            // Cadena de conexión a tu base de datos SQL Server
            string connectionString = "Properties.Settings.Default.HelpTeacherConnectionString";


            // Consulta SQL para seleccionar todos los datos de la tabla Alumnos
            string query = "SELECT * FROM Alumnos";

            // Nombre del archivo Excel donde se exportarán los datos
            string fileName = "Alumnos.xlsx";

            // Intenta conectar a la base de datos y recuperar los datos
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Crear un nuevo archivo Excel
                    using (ExcelPackage excelPackage = new ExcelPackage())
                    {
                        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Alumnos");

                        // Escribir los datos en el archivo Excel
                        worksheet.Cells.LoadFromDataTable(dataTable, true);

                        // Guardar el archivo Excel en el disco
                        excelPackage.SaveAs(new System.IO.FileInfo(fileName));
                    }
                }

                MessageBox.Show("Datos exportados exitosamente a Excel.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar datos a Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExportar_Click(object sender, EventArgs e)
        {
            ExportarDatosAExcel();
        }
    }
}
