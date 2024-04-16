﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using SpreadsheetLight;

namespace HelpTeacherApp.Inventario
{
    public partial class InventarioTrabajos : Form
    {
        SqlConnection conexion = new SqlConnection(Properties.Settings.Default.HelpTeacherConnectionString);
        public InventarioTrabajos()
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
        private void ConsultarTrabajos()
        {
            ConsultarDatos("SELECT IdTrabajo, Nombre, Grado, Grupo, Generacion as Generación, Trimestre, Fecha, NombreTrabajo, Calificacion FROM Trabajos ", DgvTrabajos);
        }
        private void InventarioTrabajos_Load(object sender, EventArgs e)
        {
            ConsultarTrabajos();
        }

        private void BtnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                // Mostrar el cuadro de diálogo SaveFileDialog
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Archivos de Excel (*.xlsx)|*.xlsx";
                saveFileDialog.Title = "Guardar archivo de Excel";
                saveFileDialog.FileName = "prueba1.xlsx"; // Nombre predeterminado del archivo

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Obtener la ruta seleccionada por el usuario
                    string filePath = saveFileDialog.FileName;

                    // Crear el documento de Excel y realizar la exportación
                    SLDocument sl = new SLDocument();
                    SLStyle style = new SLStyle();
                    style.Font.FontSize = 12;
                    style.Font.Bold = true;
                    int IC = 1;
                    foreach (DataGridViewColumn column in DgvTrabajos.Columns)
                    {
                        sl.SetCellValue(1, IC, column.HeaderText.ToString());
                        sl.SetCellStyle(1, IC, style);
                        IC++;
                    }

                    int IR = 2;
                    foreach (DataGridViewRow row in DgvTrabajos.Rows)
                    {
                        sl.SetCellValue(IR, 1, row.Cells[0].Value.ToString());
                        sl.SetCellValue(IR, 2, row.Cells[1].Value.ToString());
                        sl.SetCellValue(IR, 3, row.Cells[2].Value.ToString());
                        sl.SetCellValue(IR, 4, row.Cells[3].Value.ToString());
                        sl.SetCellValue(IR, 5, row.Cells[4].Value.ToString());
                        sl.SetCellValue(IR, 6, row.Cells[5].Value.ToString());
                        sl.SetCellValue(IR, 7, row.Cells[6].Value.ToString());
                        sl.SetCellValue(IR, 8, row.Cells[7].Value.ToString());
                        sl.SetCellValue(IR, 9, row.Cells[8].Value.ToString());
                        IR++;
                    }

                    // Guardar el documento de Excel en la ruta seleccionada por el usuario
                    sl.SaveAs(filePath);

                    MessageBox.Show("La exportación se ha realizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar datos a Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
