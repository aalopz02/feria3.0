using Antlr.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace feria.REST.Controllers.DBManager
{
    /*
     * Clase para manejar el carrito de un cliente
     */
    public class Carrito
    {
        public String usuario;//Usuario de quien pertenece el carrito
        public String factura;//Factura para confirmar el pedido
        public List<Articulo> listado;//Lista de articulos que contienen cada producto

        /*
         * Constructor de carrito, recibe el usuario e inicializa variable listado 
         */
        public Carrito(String usuario) {
            this.usuario = usuario;
            listado = new List<Articulo>();
        }

    }

    /*
     * Clase para manejar cada articulo del carrito
     */
    public class Articulo {
        public String Producto;//Nombre del producto
        public int cantidad;//Cantidad
        public int precio;//Precio del producto
        public int cedulaProductor;//Cedula de productor que vende el producto
        public String modoVenta;//Modo venta del producto

        /*
         * Constructor del articulo, inicializa todos los valores de la clase
         */
        public Articulo(String producto, int cedulaProductor, int precio, int cantidad, String modoVenta) {
            this.Producto = producto;
            this.cedulaProductor = cedulaProductor;
            this.precio = precio;
            this.cantidad = cantidad;
            this.modoVenta = modoVenta;
        }
    }
}