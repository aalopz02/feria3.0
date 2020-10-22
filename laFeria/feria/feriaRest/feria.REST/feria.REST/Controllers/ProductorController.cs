using feria.REST.Controllers.DBManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace feria.REST.Controllers
{
    public class ProductorController : ApiController
    {

        //GET de los pedidos de un productor
        //api/Productor?cedulaPedidos=1234
        public Pedidos GetPedidos(int cedulaPedidos)
        {
            return DataBaseLoader.GetPedidos(cedulaPedidos);
        }


        //GET de los productores que tienen un distrito como lugar disponible para entrega
        //api/Productor?distrito=nombre
        public IEnumerable<Productor> GetByDistrito(String distrito) {
            IEnumerable<Productor> allProductores = Get();
            List<Productor> selectedProductores = new List<Productor>();
            foreach (Productor productor in allProductores) {
                if (productor.direccionesEntrega.Contains(distrito)) {
                    selectedProductores.Add(productor);
                }
            }
            return selectedProductores;
         }

        // GET de todos los produtores de la base de datos
        //api/Productor?
        public IEnumerable<Productor> Get()
        {
            return DataBaseLoader.LoadAllProductores();
        }

        // GET de la lista de productos que ofrece un productor
        //api/Productor?cedula=000
        public List<Producto> GetProductos(int cedula)
        {
            return DataBaseLoader.LoadProductorInventory(cedula);
        }

        // GET de un productor 
        //api/Productor?id=000
        public Productor Get(int id)
        {
            return DataBaseLoader.LoadProductor(id);
        }

        // Post de un nuevo productor
        //api/Productor?id=000&info=nombre-appellido-apellido2-provincia-canton-distrito-fecha-1-2-lugaresN-lugarQ
        public Boolean Post(int id, string info)
        {
            String[] valores = info.Split('-');
            List<String> listaNombre = new List<string>
            {
                valores[0],
                valores[1],
                valores[2]
            };
            List<String> listaDireccion = new List<string>
            {
                valores[3],
                valores[4],
                valores[5]
            };
            List<String> listaLugaresEntrega = new List<string>();
            for (int i = 9; i < valores.Length; i++)
            {
                listaLugaresEntrega.Add(valores[i]);
            }
            Productor productor = new Productor(id, listaNombre, listaDireccion, valores[6],
                                                 valores[7], valores[8], listaLugaresEntrega);
            DataBaseWriter.CrearNuevoProductor(productor);
            DataBaseWriter.AddCedulaProductor(id);
            return true;
        }

        //funcion para agregar un producto a un productor
        //api/Productor?id=cedulaProductor&infoproducto=nombreProducto-categoria-precio-modoVenta-cantidad-img
        public Boolean AddProducto(int id, string infoproducto)
        {
            String[] valores = infoproducto.Split('-');
            Productor productor = DataBaseLoader.LoadProductor(id);
            if (productor==null) { return false; }
            List<Categoria> listaCat = DataBaseLoader.LoadCategorias().ToList();
            foreach (Categoria cat in listaCat)
            {//String nombre, String categoria, int precio, String modoVenta, String imagen)
                if (cat.nombre == valores[1])
                {
                    Producto producto = new Producto(valores[0], valores[1], int.Parse(valores[2]), valores[3], valores[5]);
                    producto.AumentarStock(int.Parse(valores[4]));
                    productor.AddProducto(producto);
                    return true;
                }
            }

            return false;
        }

        //PATCH para modificar un productor
        //api/Productor?id=000&info=nombre-appellido-apellido2-provincia-canton-distrito-fecha-1-2-lugaresN-lugarQ
        public Boolean PATCH(int id, String info)
        {
            if (DataBaseLoader.LoadProductor(id) == null)
            {
                return false;
            }
            String[] valores = info.Split('-');
            List<String> listaNombre = new List<string>
            {
                valores[0],
                valores[1],
                valores[2]
            };
            List<String> listaDireccion = new List<string>
            {
                valores[3],
                valores[4],
                valores[5]
            };
            List<String> listaLugaresEntrega = new List<string>();
            for (int i = 9; i < valores.Length; i++)
            {
                listaLugaresEntrega.Add(valores[i]);
            }
            Productor productor = new Productor(id, listaNombre, listaDireccion, valores[6],
                                                 valores[7], valores[8], listaLugaresEntrega);
            DataBaseWriter.CrearNuevoProductor(productor);
            return true;
        }

        //DELETE de un productor
        //api/Productor?id=000
        public Boolean DELETE(int id)
        {
            return DataBaseWriter.DeleteProductor(id);
        }

    }
}