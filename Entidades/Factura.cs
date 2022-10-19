using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Factura
    {

        private string codigo = "";
        private string producto = "";
        private string precio = "";
        private string cantidad = "";
        private string descuento = "";
        private string precioTotal = "";
        private string subtotal = ""; // no está en la BD
        private string cliente = "";
        private string clientedesc = "";
        private string total = "";
        private string nFactura = "";

        public string Codigo { get => codigo; set => codigo = value; }
        public string Producto { get => producto; set => producto = value; }
        public string Precio { get => precio; set => precio = value; }
        public string Cantidad { get => cantidad; set => cantidad = value; }
        public string Descuento { get => descuento; set => descuento = value; }
        public string PrecioTotal { get => precioTotal; set => precioTotal = value; }
        public string Subtotal { get => subtotal; set => subtotal = value; }
        public string Cliente { get => cliente; set => cliente = value; }
        public string Clientedesc { get => clientedesc; set => clientedesc = value; }
        public string Total { get => total; set => total = value; }
        public string NFactura { get => nFactura; set => nFactura = value; }
    }


}
