using Antlr.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace feria.REST.Controllers.DBManager
{
    public class Carrito
    {
        public String usuario;
        public String factura;
        public List<Articulo> listado;

        public Carrito(String usuario) {
            this.usuario = usuario;
            listado = new List<Articulo>();
        }

    }

    public class Articulo {
        public String Producto;
        public int cantidad;
        public int precio;
        public int cedulaProductor;
        public String modoVenta;

        public Articulo(String producto, int cedulaProductor, int precio, int cantidad, String modoVenta) {
            this.Producto = producto;
            this.cedulaProductor = cedulaProductor;
            this.precio = precio;
            this.cantidad = cantidad;
            this.modoVenta = modoVenta;
        }
    }
}