using feria.REST.Controllers.DBManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace feria.REST.Controllers
{
    /*
     * Controlador para carritos
     */
    public class CartController : ApiController
    {
        //funcion POST para crear un pedido a cada productor que corresponda para cada producto del carrito de un usuario
        //api/Cart?nombreUser=aalopz&factura=x
        public void POST(String nombreUser, String factura)
        {
            IEnumerable<Articulo> articulos = Get(nombreUser);
            List<int> cedulas = new List<int>();
            List<List<Articulo>> sortedArticulos = new List<List<Articulo>>();

            foreach (Articulo articulo in articulos)
            {
                if (cedulas.Contains(articulo.cedulaProductor))
                {
                    sortedArticulos[cedulas.IndexOf(articulo.cedulaProductor)].Add(articulo);
                }
                else
                {
                    cedulas.Add(articulo.cedulaProductor);
                    sortedArticulos.Add(new List<Articulo>());
                    sortedArticulos[cedulas.IndexOf(articulo.cedulaProductor)].Add(articulo);
                }
            }
            for (int i = 0; i < cedulas.Count; i++)
            {
                DataBaseWriter.AddIdToPedidosList(cedulas[i]);
                int idPedido = DataBaseLoader.LoadIdsPedidosProductor(cedulas[i]).Last();
                Pedido pedido = new Pedido(idPedido, nombreUser, DataBaseLoader.LoadCliente(nombreUser).direccion, factura)
                {
                    productos = sortedArticulos[i]
                };
                DataBaseWriter.AddPedidoAProductor(cedulas[i], pedido);
            }

        }

        //GET de todos los articulos que un cliente tiene en el carrito
        //api/Cart?nombreUser=aalopz
        public IEnumerable<Articulo> Get(String nombreUser)
        {
            Carrito cart = DataBaseLoader.LoadUserCart(nombreUser);
            return cart.listado;
        }

        //PUT para annadir un producto al carrito de un usuario
        //api/Cart?user=aalopz&nombreProducto=nombreProducto&cantidad=000&cedulaProductor=1
        public Boolean PUT(String user, String nombreProducto, int cantidad, int cedulaProductor)
        {
            Producto producto = null;
            foreach (Producto producto1 in DataBaseLoader.LoadProductorInventory(cedulaProductor))
            {
                if (producto1.nombre.Equals(nombreProducto))
                {
                    if (producto1.disponible >= cantidad)
                    {
                        producto = producto1;
                    }
                    else {
                        return false;
                    }
                }
            }
            try {
                Articulo articulo = new Articulo(nombreProducto, cedulaProductor, producto.precio, cantidad, producto.modoVenta);
                return DataBaseWriter.AddProductoToCarrito(user, articulo);
            } catch (NullReferenceException) {
                return false;
            }
            
        }

        //PUT para annadir un producto al carrito de un usuario
        //api/Cart?user=aalopz&nombreProducto=nombreProducto&cedulaProductor=1
        public Boolean PUT(String user, String nombreProducto, int cedulaProductor)
        {
            Producto producto = null;
            foreach (Producto producto1 in DataBaseLoader.LoadProductorInventory(cedulaProductor))
            {
                if (producto1.nombre.Equals(nombreProducto))
                {
                    if (producto1.disponible >= 1)
                    {
                        producto = producto1;

                    }
                    else {
                        return false;
                    }
                }
            }
            try
            {
                Articulo articulo = new Articulo(nombreProducto, cedulaProductor, producto.precio, -1, producto.modoVenta);
                return DataBaseWriter.AddProductoToCarrito(user, articulo);
            }
            catch (NullReferenceException)
            {
                return false;
            }

        }

        //DELETE para quitar un producto al carrito de un usuario
        //api/Cart?user=aalopz&nombreProducto=nombreProducto&cedulaProductor=1
        public Boolean DELETE(String user, String nombreProducto,int cedulaProductor)
        {
            return DataBaseWriter.DeleteProductoCarrito(user, nombreProducto, cedulaProductor);

        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {

        }
    }
}