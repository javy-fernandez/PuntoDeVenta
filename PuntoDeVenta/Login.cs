using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocios;

namespace PuntoDeVenta
{
    public partial class Login : Form
    {
        ConexionSQLN cn = new ConexionSQLN();
        public Login()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            
            if (cn.conSQL(txtUsuario.Text, txtPassword.Text) == 1)
            {
                //MessageBox.Show("El usuario ha sido encontrado");
                txtUsuario.Text = "";
                txtPassword.Text = "";
                //this.Hide();
                VentanaPrincipal v1 = new VentanaPrincipal();
                v1.Show();
            } else
            {
                MessageBox.Show("¡Usuario no encontrado!");
                txtUsuario.Text = "";
                txtPassword.Text = "";
            }
        }
    }
}
