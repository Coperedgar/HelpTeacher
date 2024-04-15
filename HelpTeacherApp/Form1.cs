using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HelpTeacherApp
{
    public partial class Form1 : Form
    {
        private Form tiempoForm = null;

        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            // Abre el formulario "Tiempo" por defecto al cargar el formulario principal
            if (tiempoForm == null)
            {
                tiempoForm = new Tiempo();
                AbrirForm1(tiempoForm);
            }
        }

        private void BtnSlide_Click(object sender, EventArgs e)
        {
            if (MenuVertical.Width == 250)
            {
                MenuVertical.Width = 70;
            }
            else
                MenuVertical.Width = 250;
        }

        private void AbrirForm1(Form form)
        {
            // Si el formulario "Tiempo" no está abierto, lo abre; de lo contrario, abre el formulario pasado como argumento
            if (tiempoForm == null || tiempoForm.IsDisposed)
            {
                tiempoForm = form;
            }
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Clear();
            this.panelContenedor.Controls.Add(form);
            this.panelContenedor.Tag = form;
            form.Show();
        }

        private void BtnAlumnos_Click(object sender, EventArgs e)
        {
            AbrirForm1(new Alumnos());
        }

        private void BtnPaseLista_Click(object sender, EventArgs e)
        {
            AbrirForm1(new ControlPaseLista());
        }

        private void label1_Click(object sender, EventArgs e)
        {
            AbrirForm1(new Tiempo());
        }

        private void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login log = new Login();
            log.Show();
            
        }

        private void BtnEvidencias_Click(object sender, EventArgs e)
        {
            AbrirForm1(new ControlEvidencias());
        }

        private void BtnExamenes_Click(object sender, EventArgs e)
        {
            AbrirForm1(new ControlExamenes());
        }

        private void BtnTrabajos_Click(object sender, EventArgs e)
        {
            AbrirForm1(new ControlTrabajos());
        }

        private void BtnTareas_Click(object sender, EventArgs e)
        {
            AbrirForm1(new ControlTareas());
        }

        private void BtnPracticas_Click(object sender, EventArgs e)
        {
            AbrirForm1(new ControlPracticas());
        }

        private void BtnInventario_Click(object sender, EventArgs e)
        {
            this.Close();
            Inventario.Inventario log = new Inventario.Inventario();
            log.Show();
        }
    }


}
