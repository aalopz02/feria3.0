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
        public String cantidad;
        public String usuario;
        public List<String> direccion;
        public List<Articulo> producto;
        String factura;

        public Pedido(int id, String cantidad, String usuario, List<String> direccion, String factura) {
            this.idPedido = id;
            this.cantidad = cantidad;
            this.usuario = usuario;
            this.direccion = direccion;
            this.factura = factura;
            producto = new List<Articulo>();
        }
    }
}