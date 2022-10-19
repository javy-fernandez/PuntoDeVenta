using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Entidades;
using System.Windows.Forms;

namespace Datos
{
    public class ConexionSQL
    {

        static string conexionstring = "Server= localhost; database= PuntoDeVenta;" +
            "integrated security= true";
        SqlConnection con = new SqlConnection(conexionstring);


        public int consultaLogin(string Usuario, string Contrasena)
        {
            int count;
            con.Open();
            string Query = "Select Count(*) From Persona where usuario= '"+ Usuario +"'" +
                " and contrasena= '"+ Contrasena +"'";
            SqlCommand cmd = new SqlCommand(Query, con);
            count = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return count;
        }

        public int InsertarUsuario(string nom, string apel, string dni, string tel, string user, string pass)
        {
            int flag = 0;
            con.Open();
            string query = "insert into Persona values ('"+nom+"','"+apel+"','"+dni+"','"+tel+"','"+user+"','"+pass+"')";
            SqlCommand cmd = new SqlCommand (query, con);
            flag = cmd.ExecuteNonQuery();
            con.Close();
            return flag;
        }

        public int ModificarUsuario(string nom, string apel, string dni, string tel, string user, string pass)
        {
            int flag = 0;
            con.Open();
            string query = "Update Persona set nombre ='" + nom + "', apellidos = '"+ apel + 
                "', telefono = '" + tel + "', usuario ='" + user + "', contrasena='" + pass + "' where dni = '" + dni + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            flag = cmd.ExecuteNonQuery();
            con.Close();
            return flag;
        }

        public int EliminarUsuario(string dni)
        {
            int flag = 0;
            con.Open();
            string query = "Delete from Persona where dni = '" + dni + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            flag = cmd.ExecuteNonQuery();
            con.Close();
            return flag;
        }

        public DataTable ConsultaUsuariosDG()
        {
            try
            {
                string query = "select * from Persona";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter data = new SqlDataAdapter(cmd);
                DataTable tabla = new DataTable();
                data.Fill(tabla);

                return tabla;
            }
            catch (Exception ex)
            {
                MessageBox.Show("¡BBDD no encontrada!");
                return null;
            }
            
        }

        public string ConsultaFactura()
        {
            con.Open();
            string resultado = "NULL";
            string query = "Select(Select distinct top 1 NumFactura from Facturacion order by NumFactura desc) + 1 as NumFactura";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reg = cmd.ExecuteReader();

            if (reg.Read())
            {
                resultado = reg["NumFactura"].ToString();
            }
            con.Close();
            return resultado;
            
        }

        public Tuple<string, string> ConsultaInventario(string codigo)
        {
            con.Open();
            string resultado1 = "NULL";
            string resultado2 = "NULL";
            string query = "Select * from Inventario where Codigo = '"+codigo+"'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reg = cmd.ExecuteReader();

            if (reg.Read())
            {
                resultado1 = reg["Producto"].ToString();
                resultado2 = reg["Precio"].ToString();
            }
            con.Close();
            return Tuple.Create(resultado1, resultado2);
        }

        public Tuple<string, double> ConsultaCliente(string codigo)
        {
            con.Open();
            string resultado1 = "NULL";
            double resultado2 = 0;
            string query = "Select [NombreCliente] + ' ' + [ApellidoCliente] as Nombre , [Descuento] from [Clientes] where [CodigoCliente] = '"+codigo+"'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reg = cmd.ExecuteReader();

            if (reg.Read())
            {
                resultado1 = reg["Nombre"].ToString();
                resultado2 = double.Parse(reg["Descuento"].ToString());
            }
            con.Close();
            return Tuple.Create(resultado1, resultado2);
        }

        public void InsertarFactura(List<Factura> F)
        {
            con.Open();

            foreach (Factura fact in F)
            {
                string query = "insert into Facturacion (Codigo,Producto,PrecioUnidad,Cantidad,Descuento" +
                    ",Cliente,DescuentoCliente,MontoTotal, NumFactura ) " +
                    " values ('" + fact.Codigo + "', '" + fact.Producto + "'," +
                    "" + Convert.ToDouble(fact.Precio) + "," + Convert.ToInt32(fact.Cantidad) + "," + Convert.ToDouble(fact.Descuento) +
                    "'" + fact.Cliente + "'," + Convert.ToDouble(fact.Clientedesc) + "," +
                    "" + Convert.ToDouble(fact.Total) + "," + Convert.ToInt32(fact.NFactura) + ")";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
            }

            con.Close();
        }
    }
}
