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
    public partial class FormUsers : Form
    {
        ConexionSQLN cn = new ConexionSQLN();
        public FormUsers()
        {
            InitializeComponent();
            dataGridView1.DataSource = cn.ConsultaDT();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // nom, apel, dni, tel, user, pass
            cn.InsertarUsuario(txtNombre.Text, txtApellidos.Text, txtDni.Text, txtTelefono.Text, txtUsuario.Text, txtPass.Text);
            dataGridView1.DataSource = cn.ConsultaDT();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            cn.ModificarUsuario(txtNombre.Text, txtApellidos.Text, txtDni.Text, txtTelefono.Text, txtUsuario.Text, txtPass.Text);
            dataGridView1.DataSource = cn.ConsultaDT();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            cn.EliminarUsuario(txtDni.Text);
            dataGridView1.DataSource = cn.ConsultaDT();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
