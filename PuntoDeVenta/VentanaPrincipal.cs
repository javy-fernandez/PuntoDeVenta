using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocios;
using Entidades;

namespace PuntoDeVenta
{
    public partial class VentanaPrincipal : Form
    {
        private DataTable dt;
        ConexionSQLN cn = new ConexionSQLN();
        private double subtotal = 0;
        private double total = 0;
        private double desc = 0;

        public VentanaPrincipal()
        {
            InitializeComponent();

            txtDescuento.Text = txtMenuDescuento.Text;
            double cosa = double.Parse(txtMenuImpuesto.Text);// * 100;
            txtImpuesto.Text = cosa.ToString();
            
            // DataGridView con Productos
            dt = new DataTable();
            dt.Columns.Add("Codigo");
            dt.Columns.Add("Producto");
            dt.Columns.Add("Precio");
            dt.Columns.Add("Cantidad");
            dt.Columns.Add("Descuento");
            dt.Columns.Add("Precio Total");

            dgvFacturacion.DataSource = dt;

            txtFactura.Text = cn.consultaFactura();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMenuUsuarios_Click(object sender, EventArgs e)
        {
            FormUsers v1 = new FormUsers();
            this.Hide();
            v1.ShowDialog();
            this.Show();
        }

        private void btnMenuInventario_Click(object sender, EventArgs e)
        {
            FormInventario v1 = new FormInventario();
            this.Hide();
            v1.ShowDialog();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


        private void ImpVentaYDescChanged(object sender, EventArgs e)
        {
            txtDescuento.Text = txtMenuDescuento.Text;
            txtImpuesto.Text = txtMenuImpuesto.Text;
            /*if(txtMenuDescuento.Text == "")
            {
                txtMenuDescuento.Text = "0";
            }*/
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var resultado = cn.ConsultaInventario(txtCodigoProducto.Text);
            DataRow row = dt.NewRow();

            row["Codigo"] = txtCodigoProducto.Text;
            row["Producto"] = resultado.Item1;
            row["Precio"] = resultado.Item2;
            row["Cantidad"] = txtCantidad.Text;
            row["Descuento"] = txtDescuento.Text;
            row["Precio Total"] = Int32.Parse(txtCantidad.Text) * double.Parse(resultado.Item2);

            dt.Rows.Add(row);

            subtotal = subtotal + (Int32.Parse(txtCantidad.Text) * double.Parse(resultado.Item2));

            double imp = double.Parse(txtImpuesto.Text) / 100;
            double desMenu = double.Parse(txtMenuDescuento.Text) / 100;
            if (desc == 0)
            {
                total = subtotal + (subtotal * imp);
                total = total - total * (desMenu);
                if (desMenu == 0)
                {
                    lblSubTotal.Text = (subtotal).ToString();
                }
                else
                {
                    lblSubTotal.Text = (subtotal * desMenu).ToString();
                }
            }
            else
            {
                total = subtotal + (subtotal * imp);
                total = total - (total * (desc) / 100);
                lblSubTotal.Text = ((subtotal * desc)/10).ToString();
            }


            
            lblTotal.Text = total.ToString();

            txtCantidad.Text = "";
            txtCodigoProducto.Text = "";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var resultado2 = cn.ConsultaCliente(txtCodCliente.Text);
            txtClienteInfo.Text = resultado2.Item1 + " DESC: " + resultado2.Item2;
            desc = resultado2.Item2;
            txtDescuento.Text = desc.ToString();
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            
            List<Factura> listFact = new List<Factura>();
            
            string cliente = txtClienteInfo.Text; // ???????????

            foreach (DataRow row in dt.Rows)
            {
                Factura fact = new Factura();

                fact.Codigo = row["Codigo"].ToString();
                fact.Producto = row["Producto"].ToString();
                fact.Precio = row["Precio"].ToString();
                fact.Cantidad = row["Cantidad"].ToString();
                fact.Descuento = row["Descuento"].ToString();
                fact.PrecioTotal = row["Precio Total"].ToString();
                fact.Subtotal = subtotal.ToString();
                fact.Cliente = cliente.ToString();
                fact.Clientedesc = desc.ToString();
                fact.Total = total.ToString();
                fact.NFactura = txtFactura.Text;

                listFact.Add(fact);


            }

            cn.InsertarFactura(listFact);
            txtFactura.Text = cn.consultaFactura();


        }
    }
}
