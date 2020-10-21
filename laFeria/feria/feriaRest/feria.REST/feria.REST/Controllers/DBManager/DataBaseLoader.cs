using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using System.IO;

namespace feria.REST.Controllers.DBManager
{
    /*
     * Clase para leer los archivos xml con los datos
     */
    public class DataBaseLoader
    {
       /*
        * Variables con las direcciones a las carpetas 
        */
        static readonly String url_mist = "D:\\proyects\\feria\\feriaDatabase\\Mist\\";
        static readonly String url_productores = "D:\\proyects\\feria\\feriaDatabase\\productores\\";
        static readonly String url_solicitud = "D:\\proyects\\feria\\feriaDatabase\\Mist\\Solicitudes\\";
        static readonly String url_clientes = "D:\\proyects\\feria\\feriaDatabase\\clientes\\";

        /*
         * Metodo que devuelve el xml con los pedidos de un productor buscandolo con cedula
         * Si no existe el xml lo crea y lo inicializa
         */
        public static XmlDocument LoadPedidosXml(int cedula)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(url_productores + "pedidos//" + cedula.ToString() + "_doc.xml");
            }
            catch (FileNotFoundException)
            {
                byte[] info = new UTF8Encoding(true).GetBytes("<Pedidos></Pedidos>");
                using (var myFile = File.Create(url_productores + "pedidos//" + cedula.ToString() + "_doc.xml"))
                {
                    myFile.Write(info, 0, info.Length);
                }
                xmlDoc.Load(url_productores + "pedidos//" + cedula.ToString() + "_doc.xml");
            }
            return xmlDoc;
        }

        /*
         * Metodo para obtener los pedidos de un productor buscandolo por cedula, devuelve un objeto Pedidos
         */
        internal static Pedidos GetPedidos(int cedula) {
            Pedidos pedidos = new Pedidos(cedula);
            XmlDocument xmlDoc = LoadPedidosXml(cedula);
            XmlNode rootNode = xmlDoc.ChildNodes[0];

            foreach (XmlNode pedidoXml in rootNode.ChildNodes)
            {
                Pedido pedido = new Pedido(int.Parse(pedidoXml.Attributes["IdPedido"].Value),
                                           pedidoXml.Attributes["Usuario"].Value,
                                           pedidoXml.Attributes["Direccion"].Value.Split(',').ToList(),
                                           pedidoXml.Attributes["Factura"].Value);
                foreach (XmlNode articuloxml in pedidoXml.ChildNodes[0]) {
                    pedido.productos.Add(new Articulo(articuloxml.Attributes["Producto"].Value,
                                                      cedula,
                                                      int.Parse(articuloxml.Attributes["Precio"].Value),
                                                      int.Parse(articuloxml.Attributes["Cantidad"].Value),
                                                      articuloxml.Attributes["ModoVenta"].Value)
                                                      );
                }
                
                pedidos.pedidos.Add(pedido);

            }
            return pedidos;
        }

        /*
         * Metodo para obtener el carrito de un usuario por nombre de usuario, devuelve un objeto Carrito
         */
        internal static Carrito LoadUserCart(string user)
        {
            Carrito cart = new Carrito(user);
            XmlDocument xmlDocument = LoadUserCartXml(user);
            XmlNodeList nodeList = xmlDocument.DocumentElement.ChildNodes;
            foreach (XmlNode xmlArticulo in nodeList)
            {
                Articulo articulo = new Articulo(xmlArticulo.Attributes["NombreProducto"].Value,
                                                    int.Parse(xmlArticulo.Attributes["CedulaProductor"].Value),
                                                    int.Parse(xmlArticulo.Attributes["Precio"].Value),
                                                    int.Parse(xmlArticulo.Attributes["Cantidad"].Value),
                                                    xmlArticulo.Attributes["ModoVenta"].Value);
                cart.listado.Add(articulo);
            }
            return cart;
        }

        /*
         * Metodo para obtener la lista de Productores de toda la base de datos
         */
        public static IEnumerable<Productor> LoadAllProductores() {
            List<Productor> listaAll = new List<Productor>();
            List<int> listaCedulas = GetAllCedulasProductores().ToList();
            foreach (int cedula in listaCedulas) {
                listaAll.Add(LoadProductor(cedula));
            }
            return listaAll;
        }

        /*
         * Metodo para obtener el xml que contiene la lista con las cedulas de los productores existentes
         */
        public static XmlDocument LoadProductoresList()
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(url_mist + "ProductoresList_doc.xml");
            }
            catch (FileNotFoundException)
            {
                byte[] info = new UTF8Encoding(true).GetBytes("<Productores></Productores>");
                using (var myFile = File.Create(url_mist + "ProductoresList_doc.xml"))
                {
                    myFile.Write(info, 0, info.Length);
                }
                xmlDoc.Load(url_mist + "ProductoresList_doc.xml");
            }
            return xmlDoc;
        }

        /*
         * Metodo que devuelve una lista con todos los ids de los pedidos que tiene un productor, busca por cedula
         */
        public static List<int> LoadIdsPedidosProductor(int cedula) {
            List<int> idList = new List<int>();
            XmlDocument xmlDoc = LoadIdsPedidosProductorXml(cedula);
            XmlNodeList xmlNodeList = xmlDoc.DocumentElement.ChildNodes;
            foreach (XmlNode node in xmlNodeList) {
                idList.Add(int.Parse(node.Attributes["Id"].Value));
            }
            return idList;
        }

        /*
         * Metodo para obtener el xml con las lista de pedidos de un productor, buscandolo por cedula
         */
        public static XmlDocument LoadIdsPedidosProductorXml(int cedula)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(url_productores + "pedidos//" + cedula.ToString() + "_ids.xml");
            }
            catch (FileNotFoundException)
            {
                byte[] info = new UTF8Encoding(true).GetBytes("<IDPedidos></IDPedidos>");
                using (var myFile = File.Create(url_productores + "pedidos//" + cedula.ToString() + "_ids.xml"))
                {
                    myFile.Write(info, 0, info.Length);
                }
                xmlDoc.Load(url_productores + "pedidos//" + cedula.ToString() + "_ids.xml");
            }
            return xmlDoc;
        }

        /*
         * Metodo para obtener el xml con el carrito de un usuario
         */
        internal static XmlDocument LoadUserCartXml(string user)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(url_clientes + "carritos\\" + user + "_doc.xml");
            }
            catch (FileNotFoundException)
            {
                byte[] info = new UTF8Encoding(true).GetBytes("<Carrito></Carrito>");
                using (var myFile = File.Create(url_clientes + "carritos//" + user + "_doc.xml"))
                {
                    myFile.Write(info, 0, info.Length);
                }
                xmlDoc.Load(url_clientes + "carritos//" + user + "_doc.xml");
            }
            return xmlDoc;
        }

        /*
         * Metodo para obtener la lista con todas las cedulas de los productores
         */
        public static IEnumerable<int> GetAllCedulasProductores() {
            List<int> list = new List<int>();
            XmlDocument xmlDoc = LoadProductoresList();
            XmlNodeList nodeList = xmlDoc.DocumentElement.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                list.Add(int.Parse(node.Attributes["Cedula"].Value));
            }
            return list;
        }

        /*
         * Metodo para obtener el xml con la lista de usuarios que existen el la base de datos
         * Si no existe lo crea y inicializa
         */
        public static XmlDocument LoadClientesListXml()
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(url_mist + "ClientesList_doc.xml");
            }
            catch (FileNotFoundException)
            {
                byte[] info = new UTF8Encoding(true).GetBytes("<Clientes></Clientes>");
                using (var myFile = File.Create(url_mist + "ClientesList_doc.xml"))
                {
                    myFile.Write(info, 0, info.Length);
                }
                xmlDoc.Load(url_mist + "ClientesList_doc.xml");
            }
            return xmlDoc;
        }

        /*
         * Metodo para obtener la lista de usuarios existentes en la base de datos
         */
        public static IEnumerable<String> GetAllUsers()
        {
            List<String> list = new List<String>();
            XmlDocument xmlDoc = LoadClientesListXml();
            XmlNodeList nodeList = xmlDoc.DocumentElement.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                list.Add(node.Attributes["User"].Value);
            }
            return list;
        }

        /*
         * Metodo para obtener la lista con todas las solicitudes de los productores
         */
        internal static IEnumerable<Solicitud> LoadSolicitudes()
        {
            List<Solicitud> list = new List<Solicitud>();
            XmlDocument xmlDoc = LoadSolicitudXml();
            XmlNodeList nodeList = xmlDoc.DocumentElement.ChildNodes;
            List<int> ids = new List<int>();
            List<int> cedulasProductor = new List<int>();
            foreach (XmlNode node in nodeList)
            {
                cedulasProductor.Add(int.Parse(node.Attributes["Cedula"].Value));
                ids.Add(int.Parse(node.Attributes["Id"].Value));
            }
            for (int i = 0; i < cedulasProductor.Count; i++) {
                Productor productor = LoadProductor(url_solicitud + ids[i] + "_doc.xml");
                list.Add(new Solicitud(ids[i], productor));
            }
            return list;
        }

        /*
         * Metodo para obtener un objeto Solicitud basado en un identificador de la solicitud
         */
        internal static Solicitud LoadSolicitud(int id)
        {
            XmlDocument xmlDoc = LoadSolicitudXml();
            XmlNodeList nodeList = xmlDoc.DocumentElement.ChildNodes;
            int cedulaProductor;
            foreach (XmlNode node in nodeList)
            {
                if (int.Parse(node.Attributes["Id"].Value) == id)
                {
                    cedulaProductor = int.Parse(node.Attributes["Cedula"].Value);
                }
            }
            Productor productor = LoadProductor(url_solicitud + id.ToString() + "_doc.xml");
            return new Solicitud(id, productor);
        }

        /*
         * Metodo para obtener la lista de categorias presente
         */
        internal static IEnumerable<Categoria> LoadCategorias()
        {
            List<Categoria> list = new List<Categoria>();
            XmlDocument xmlDoc = LoadCategoriasXml();
            XmlNodeList nodeList = xmlDoc.DocumentElement.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                list.Add(new Categoria(int.Parse(node.Attributes["Id"].Value), node.Attributes["Nombre"].Value));
            }
            return list;
        }

        /*
         * Metodo para obtener el ultimo id para una solitud y crear uno nuevo con el consecutivo siguiente
         */
        internal static int LoadLastSolicitudId(int id)
        {
            XmlDocument xmlDocument = LoadSolicitudXml();
            XmlNodeList nodeList = xmlDocument.DocumentElement.ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                if (int.Parse(node.Attributes["Cedula"].Value) == id) {
                    return -1;
                }
            }
            int last = 0;
            if (nodeList.Count == 0) {
                return last;
            } else
                last = int.Parse(nodeList.Item(nodeList.Count - 1).Attributes["Id"].Value);
            return last;
        }

        /*
         * Metodo para obtener un objeto Categoria, usando el id de la categoria
         */
        internal static Categoria LoadCategoria(int id)
        {
            XmlDocument xmlDoc = LoadCategoriasXml();
            XmlNodeList nodeList = xmlDoc.DocumentElement.ChildNodes;
            foreach (XmlNode node in nodeList) {
                if (node.Attributes["Id"].Value == id.ToString()) {
                    return new Categoria(int.Parse(node.Attributes["Id"].Value), node.Attributes["Nombre"].Value);
                }
            }
            return null;
        }

        /*
         * Metodo para obtener el xml que contiene las categorias
         */
        public static XmlDocument LoadCategoriasXml() {
            XmlDocument xmlDoc = new XmlDocument();
            try {
                xmlDoc.Load(url_mist + "Categorias_doc.xml");
            } catch (FileNotFoundException) {
                byte[] info = new UTF8Encoding(true).GetBytes("<Categoria></Categoria>");
                using (var myFile = File.Create(url_mist + "Categorias_doc.xml"))
                {
                    myFile.Write(info, 0, info.Length);
                }
                xmlDoc.Load(url_mist + "Categorias_doc.xml");
            }
            
            return xmlDoc;
        }

        /*
         * Metodo para obtener el xml con un productor buscandolo por cedula
         */
        public static XmlDocument LoadProductorXml(int cedula)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try {
                xmlDoc.Load(url_productores + cedula.ToString() + "_doc.xml");
                return xmlDoc;
            } catch (FileNotFoundException) {
                return null;
            }
        }

        /*
         * Metodo para obtener un objeto Productor, buscandolo por cedula
         */
        public static Productor LoadProductor(int cedula) {
            XmlDocument xmlDoc = LoadProductorXml(cedula);
            if (xmlDoc == null) { return null; }
            XmlNodeList nodeList = xmlDoc.DocumentElement.ChildNodes;
            XmlNode nodeInformacion = nodeList.Item(0);
            XmlNode nodeNombre = nodeList.Item(1);
            XmlNode nodeDireccion = nodeList.Item(2);
            XmlNode nodeLugares = nodeList.Item(3);
            List<String> nombreFull = new List<string> {nodeNombre.Attributes["Nombre"].Value,
                                                        nodeNombre.Attributes["PrimerApellido"].Value,
                                                        nodeNombre.Attributes["SegundoApellido"].Value};
            List<String> direccion = new List<string> {nodeDireccion.Attributes["Provincia"].Value,
                                                       nodeDireccion.Attributes["Canton"].Value,
                                                       nodeDireccion.Attributes["Distrito"].Value};
            List<String> entrega = new List<string>();
            foreach (XmlAttribute lugarEntrega in nodeLugares.Attributes) {
                entrega.Add(lugarEntrega.Value.ToString());
            }
            Productor productor = new Productor(cedula,
                                                nombreFull,
                                                direccion,
                                                nodeInformacion.Attributes["FechaNacimiento"].Value,
                                                nodeInformacion.Attributes["Telefono"].Value,
                                                nodeInformacion.Attributes["Sinpe"].Value,
                                                entrega);
            List<Producto> inventario = new List<Producto>();
            XmlNode listProductos = xmlDoc.DocumentElement.LastChild;
            foreach (XmlNode nodeProducto in listProductos.ChildNodes)
            {
                Producto producto = new Producto(nodeProducto.Attributes["Nombre"].Value,
                                                 nodeProducto.Attributes["Categoria"].Value,
                                                 int.Parse(nodeProducto.Attributes["Precio"].Value),
                                                 nodeProducto.Attributes["ModoVenta"].Value,
                                                 nodeProducto.Attributes["Imagen"].Value)
                {
                    disponible = int.Parse(nodeProducto.Attributes["Disponibles"].Value)
                };
                inventario.Add(producto);
            }
            productor.SetCatalogo(inventario);
            return productor;
        }

        /*
         * Metodo para obtener un objeto productor usando la ruta de archivo
         */
        public static Productor LoadProductor(String path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(path);
            }
            catch (FileNotFoundException) { return null; }
            XmlNodeList nodeList = xmlDoc.DocumentElement.ChildNodes;
            XmlNode nodeInformacion = nodeList.Item(0);
            XmlNode nodeNombre = nodeList.Item(1);
            XmlNode nodeDireccion = nodeList.Item(2);
            XmlNode nodeLugares = nodeList.Item(3);
            List<String> nombreFull = new List<string> {nodeNombre.Attributes["Nombre"].Value,
                                                        nodeNombre.Attributes["PrimerApellido"].Value,
                                                        nodeNombre.Attributes["SegundoApellido"].Value};
            List<String> direccion = new List<string> {nodeDireccion.Attributes["Provincia"].Value,
                                                       nodeDireccion.Attributes["Canton"].Value,
                                                       nodeDireccion.Attributes["Distrito"].Value};
            List<String> entrega = new List<string>();
            foreach (XmlAttribute lugarEntrega in nodeLugares.Attributes)
            {
                entrega.Add(lugarEntrega.Value.ToString());
            }
            Productor productor = new Productor(int.Parse(nodeInformacion.Attributes["Cedula"].Value),
                                                nombreFull,
                                                direccion,
                                                nodeInformacion.Attributes["FechaNacimiento"].Value,
                                                nodeInformacion.Attributes["Telefono"].Value,
                                                nodeInformacion.Attributes["Sinpe"].Value,
                                                entrega);
            List<Producto> inventario = new List<Producto>();
            XmlNode listProductos = xmlDoc.DocumentElement.LastChild;
            foreach (XmlNode nodeProducto in listProductos.ChildNodes)
            {
                Producto producto = new Producto(nodeProducto.Attributes["Nombre"].Value,
                                                 nodeProducto.Attributes["Categoria"].Value,
                                                 int.Parse(nodeProducto.Attributes["Precio"].Value),
                                                 nodeProducto.Attributes["ModoVenta"].Value,
                                                 nodeProducto.Attributes["Imagen"].Value)
                {
                    disponible = int.Parse(nodeProducto.Attributes["Disponibles"].Value)
                };
                inventario.Add(producto);
            }
            productor.SetCatalogo(inventario);

            return productor;
        }

        /*
         * Metodo para cargar una lista de Productos que ofrezca un productor, buscandolo por cedula
         */
        public static List<Producto> LoadProductorInventory(int cedula) {
            List<Producto> inventario = new List<Producto>();
            XmlDocument xmlDoc = LoadProductorXml(cedula);
            if (xmlDoc == null) { return null; }
            XmlNode listProductos = xmlDoc.DocumentElement.LastChild;
            foreach (XmlNode nodeProducto in listProductos.ChildNodes) {
                Producto producto = new Producto(nodeProducto.Attributes["Nombre"].Value,
                                                 nodeProducto.Attributes["Categoria"].Value,
                                                 int.Parse(nodeProducto.Attributes["Precio"].Value),
                                                 nodeProducto.Attributes["ModoVenta"].Value,
                                                 nodeProducto.Attributes["Imagen"].Value)
                {
                    disponible = int.Parse(nodeProducto.Attributes["Disponibles"].Value)
                };
                inventario.Add(producto);
            }
            return inventario;
        }

        /*
         * Metodo para cargar el xml de un cliente buscandolo por usuario
         */
        public static XmlDocument LoadClienteXml(String usuario)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try {
                xmlDoc.Load(url_clientes + usuario + "_doc.xml");
                return xmlDoc;
            } catch (FileNotFoundException)
            {
                return null;
            }
        }

        /*
         * Metodo para obtener un objeto Cliente que coincida con un usuario y una clave
         */
        public static Cliente CheckLogIn(String usuario, String clave) {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(url_clientes + usuario + "_doc.xml");
            }
            catch (FileNotFoundException) {
                return null;
            }
            
            if (xmlDoc == null)
            {
                return null;
            }
            else 
            {
                XmlNodeList nodeList = xmlDoc.DocumentElement.ChildNodes;
                XmlNode nodeLogIn = nodeList.Item(2);
                String claveDB = nodeLogIn.Attributes["Password"].Value;
                if (claveDB.Equals(clave)) {
                    return LoadCliente(usuario);
                } else {
                    return null;
                }
            }
        }

        /*
         * Metodo para cargar un objeto Cliente buscandolo por usuario
         */
        public static Cliente LoadCliente(String usuario) {
            XmlDocument xmlDoc = LoadClienteXml(usuario);
            if (xmlDoc == null) { return null; }
            XmlNodeList nodeList = xmlDoc.DocumentElement.ChildNodes;
            XmlNode nodeInformacion = nodeList.Item(0);
            XmlNode nodeNombre = nodeList.Item(1);
            XmlNode nodeLogIn = nodeList.Item(2);
            XmlNode nodeDireccion = nodeList.Item(3);
            List<String> nombreFull = new List<string> {nodeNombre.Attributes["Nombre"].Value,
                                                        nodeNombre.Attributes["PrimerApellido"].Value,
                                                        nodeNombre.Attributes["SegundoApellido"].Value};
            List<String> direccion = new List<string> {nodeDireccion.Attributes["Provincia"].Value,
                                                       nodeDireccion.Attributes["Canton"].Value,
                                                       nodeDireccion.Attributes["Distrito"].Value};
            Cliente cliente = new Cliente(int.Parse(nodeInformacion.Attributes["Cedula"].Value),
                                          nombreFull,direccion,
                                          nodeInformacion.Attributes["FechaNacimiento"].Value,
                                          int.Parse(nodeInformacion.Attributes["Telefono"].Value),
                                          nodeLogIn.Attributes["Usuario"].Value,
                                          nodeLogIn.Attributes["Password"].Value
                );

            return cliente;
        }

        /*
         * Metodo paa cargar el xml que contiene los id con las solicitudes, sino existe lo crea e inicializa
         */
        internal static XmlDocument LoadSolicitudXml()
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(url_solicitud + "Solicitudes_doc.xml");
            }
            catch (FileNotFoundException)
            {
                byte[] info = new UTF8Encoding(true).GetBytes("<Solicitudes></Solicitudes>");
                using (var myFile = File.Create(url_solicitud + "Solicitudes_doc.xml"))
                {
                    myFile.Write(info, 0, info.Length);
                }
                xmlDoc.Load(url_solicitud + "Solicitudes_doc.xml");
            }

            return xmlDoc;
        }
    }

}