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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            bool hayUsuariosRegistrados = UsuarioDAL.HayUsuariosRegistrados();

            // Mostrar el panel de registro solo si no hay usuarios registrados
            PnlRegistro.Visible = !hayUsuariosRegistrados;
            PnlLogin.Visible = hayUsuariosRegistrados;

        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            if(TxtContrasenia.Text == TxtConfirmar.Text)
            {
                if(UsuarioDAL.CrearCuentas(TxtUsuario.Text, TxtContrasenia.Text) > 0)
                {
                    MessageBox.Show("Cuenta creada con éxito!!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PnlRegistro.Visible = false;
                    PnlLogin.Visible = true;
                    

                }
                

                else
                    MessageBox.Show("No se pudo crear la cuenta!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnEntrar_Click(object sender, EventArgs e)
        {
            if(UsuarioDAL.Autentificar(TxtNombre.Text, TxtContra.Text) > 0)
            {
                this.Hide();
                Form1 f = new Form1();
                f.ShowDialog();
                

            }
            else
                MessageBox.Show("No se pudo crear la cuenta!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
