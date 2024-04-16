
namespace HelpTeacherApp.Inventario
{
    partial class InventarioTareas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventarioTareas));
            this.DgvTareas = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtId = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.BtnExportar = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.CmbGradoT = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.TxtNombreT = new System.Windows.Forms.TextBox();
            this.CmbGrupoT = new System.Windows.Forms.ComboBox();
            this.CmbTrimestreT = new System.Windows.Forms.ComboBox();
            this.BtnCancelarTarea = new System.Windows.Forms.Button();
            this.BtnBuscarTarea = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.TxtTareaT = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DgvTareas)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnExportar)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.SuspendLayout();
            // 
            // DgvTareas
            // 
            this.DgvTareas.AllowUserToAddRows = false;
            this.DgvTareas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvTareas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvTareas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvTareas.Location = new System.Drawing.Point(3, 166);
            this.DgvTareas.Name = "DgvTareas";
            this.DgvTareas.Size = new System.Drawing.Size(794, 281);
            this.DgvTareas.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Elephant", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(267, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 88);
            this.label1.TabIndex = 1;
            this.label1.Text = "TAREAS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtId
            // 
            this.TxtId.Location = new System.Drawing.Point(3, 3);
            this.TxtId.Name = "TxtId";
            this.TxtId.Size = new System.Drawing.Size(100, 20);
            this.TxtId.TabIndex = 2;
            this.TxtId.Visible = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.TxtId, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnExportar, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(794, 108);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // BtnExportar
            // 
            this.BtnExportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnExportar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnExportar.Image = ((System.Drawing.Image)(resources.GetObject("BtnExportar.Image")));
            this.BtnExportar.Location = new System.Drawing.Point(531, 3);
            this.BtnExportar.Name = "BtnExportar";
            this.BtnExportar.Size = new System.Drawing.Size(260, 82);
            this.BtnExportar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.BtnExportar.TabIndex = 3;
            this.BtnExportar.TabStop = false;
            this.BtnExportar.Click += new System.EventHandler(this.BtnExportar_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel10, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.DgvTareas, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.45487F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.90799F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.63714F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 7;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.44274F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.73552F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.73552F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.00299F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.38753F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.34785F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.34785F));
            this.tableLayoutPanel10.Controls.Add(this.CmbGradoT, 0, 1);
            this.tableLayoutPanel10.Controls.Add(this.label16, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.label17, 3, 0);
            this.tableLayoutPanel10.Controls.Add(this.label18, 2, 0);
            this.tableLayoutPanel10.Controls.Add(this.label19, 1, 0);
            this.tableLayoutPanel10.Controls.Add(this.TxtNombreT, 0, 1);
            this.tableLayoutPanel10.Controls.Add(this.CmbGrupoT, 1, 1);
            this.tableLayoutPanel10.Controls.Add(this.CmbTrimestreT, 2, 1);
            this.tableLayoutPanel10.Controls.Add(this.BtnCancelarTarea, 6, 1);
            this.tableLayoutPanel10.Controls.Add(this.BtnBuscarTarea, 5, 1);
            this.tableLayoutPanel10.Controls.Add(this.label10, 4, 0);
            this.tableLayoutPanel10.Controls.Add(this.TxtTareaT, 4, 1);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(3, 117);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 2;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(794, 43);
            this.tableLayoutPanel10.TabIndex = 4;
            // 
            // CmbGradoT
            // 
            this.CmbGradoT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CmbGradoT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbGradoT.FormattingEnabled = true;
            this.CmbGradoT.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.CmbGradoT.Location = new System.Drawing.Point(165, 24);
            this.CmbGradoT.Name = "CmbGradoT";
            this.CmbGradoT.Size = new System.Drawing.Size(87, 21);
            this.CmbGradoT.TabIndex = 22;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Location = new System.Drawing.Point(3, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(156, 21);
            this.label16.TabIndex = 16;
            this.label16.Text = "Nombre";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Location = new System.Drawing.Point(351, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(121, 21);
            this.label17.TabIndex = 15;
            this.label17.Text = "Trimestre";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Location = new System.Drawing.Point(258, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(87, 21);
            this.label18.TabIndex = 14;
            this.label18.Text = "Grupo";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Location = new System.Drawing.Point(165, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(87, 21);
            this.label19.TabIndex = 13;
            this.label19.Text = "Grado";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtNombreT
            // 
            this.TxtNombreT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtNombreT.Location = new System.Drawing.Point(3, 24);
            this.TxtNombreT.Name = "TxtNombreT";
            this.TxtNombreT.Size = new System.Drawing.Size(156, 20);
            this.TxtNombreT.TabIndex = 5;
            // 
            // CmbGrupoT
            // 
            this.CmbGrupoT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CmbGrupoT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbGrupoT.FormattingEnabled = true;
            this.CmbGrupoT.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F"});
            this.CmbGrupoT.Location = new System.Drawing.Point(258, 24);
            this.CmbGrupoT.Name = "CmbGrupoT";
            this.CmbGrupoT.Size = new System.Drawing.Size(87, 21);
            this.CmbGrupoT.TabIndex = 7;
            // 
            // CmbTrimestreT
            // 
            this.CmbTrimestreT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CmbTrimestreT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbTrimestreT.FormattingEnabled = true;
            this.CmbTrimestreT.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.CmbTrimestreT.Location = new System.Drawing.Point(351, 24);
            this.CmbTrimestreT.Name = "CmbTrimestreT";
            this.CmbTrimestreT.Size = new System.Drawing.Size(121, 21);
            this.CmbTrimestreT.TabIndex = 3;
            // 
            // BtnCancelarTarea
            // 
            this.BtnCancelarTarea.BackColor = System.Drawing.Color.Red;
            this.BtnCancelarTarea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnCancelarTarea.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancelarTarea.ForeColor = System.Drawing.Color.White;
            this.BtnCancelarTarea.Location = new System.Drawing.Point(706, 24);
            this.BtnCancelarTarea.Name = "BtnCancelarTarea";
            this.BtnCancelarTarea.Size = new System.Drawing.Size(85, 16);
            this.BtnCancelarTarea.TabIndex = 9;
            this.BtnCancelarTarea.Text = "Cancelar";
            this.BtnCancelarTarea.UseVisualStyleBackColor = false;
            this.BtnCancelarTarea.Click += new System.EventHandler(this.BtnCancelarTarea_Click);
            // 
            // BtnBuscarTarea
            // 
            this.BtnBuscarTarea.BackColor = System.Drawing.Color.Blue;
            this.BtnBuscarTarea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnBuscarTarea.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBuscarTarea.ForeColor = System.Drawing.Color.White;
            this.BtnBuscarTarea.Location = new System.Drawing.Point(616, 24);
            this.BtnBuscarTarea.Name = "BtnBuscarTarea";
            this.BtnBuscarTarea.Size = new System.Drawing.Size(84, 16);
            this.BtnBuscarTarea.TabIndex = 10;
            this.BtnBuscarTarea.Text = "Buscar";
            this.BtnBuscarTarea.UseVisualStyleBackColor = false;
            this.BtnBuscarTarea.Click += new System.EventHandler(this.BtnBuscarTarea_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(478, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(132, 21);
            this.label10.TabIndex = 20;
            this.label10.Text = "Tarea";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtTareaT
            // 
            this.TxtTareaT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtTareaT.Location = new System.Drawing.Point(478, 24);
            this.TxtTareaT.Name = "TxtTareaT";
            this.TxtTareaT.Size = new System.Drawing.Size(132, 20);
            this.TxtTareaT.TabIndex = 21;
            // 
            // InventarioTareas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "InventarioTareas";
            this.Text = "InventarioTareas";
            this.Load += new System.EventHandler(this.InventarioTareas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvTareas)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnExportar)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox BtnExportar;
        private System.Windows.Forms.DataGridView DgvTareas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtId;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.ComboBox CmbGradoT;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox TxtNombreT;
        private System.Windows.Forms.ComboBox CmbGrupoT;
        private System.Windows.Forms.ComboBox CmbTrimestreT;
        private System.Windows.Forms.Button BtnCancelarTarea;
        private System.Windows.Forms.Button BtnBuscarTarea;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox TxtTareaT;
    }
}