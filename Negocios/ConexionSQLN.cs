using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocios
{
    public class ConexionSQLN
    {
        ConexionSQL cn = new ConexionSQL();

        public int conSQL (string user, string pass)
        {


            return cn.consultaLogin(user, pass);
        }


        public DataTable ConsultaDT()
        {
            return cn.ConsultaUsuariosDG();
        }

        public int InsertarUsuario(string nom, string apel, string dni, string tel, string user, string pass)
        {
            return cn.InsertarUsuario(nom, apel, dni, tel, user, pass);
        }

        public int ModificarUsuario(string nom, string apel, string dni, string tel, string user, string pass)
        {
            return cn.ModificarUsuario(nom, apel, dni, tel, user, pass);
        }

        public int EliminarUsuario(string dni)
        {
            return cn.EliminarUsuario(dni);
        }

        public string consultaFactura()
        {
            return cn.ConsultaFactura();
        }
        public Tuple<string, string> ConsultaInventario(string codigo)
        {
            return cn.ConsultaInventario(codigo);
        }
        public Tuple<string, double> ConsultaCliente(string codigo)
        {
            return cn.ConsultaCliente(codigo);
        }

        public void InsertarFactura(List<Factura> F)
        {
            cn.InsertarFactura(F);
        }
    }
}
