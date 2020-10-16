using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace feria.REST.Controllers.DBManager
{
    public class Pedidos
    {
        public List<Pedido> pedidos;
        int cedula;

        public Pedidos(int cedula) {
            pedidos = new List<Pedido>();
            this.cedula = cedula;
        }
    }

    public class Pedido {
        public int idPedido;
        public String usuario;
        public List<String> direccion;
        public List<Articulo> productos;
        public String factura;

        public Pedido(int id, String usuario, List<String> direccion, String factura) {
            this.idPedido = id;
            this.usuario = usuario;
            this.direccion = direccion;
            this.factura = factura;
            productos = new List<Articulo>();
        }
    }
}