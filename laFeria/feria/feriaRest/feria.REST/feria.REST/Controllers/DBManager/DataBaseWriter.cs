using Antlr.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace feria.REST.Controllers.DBManager
{
    /*
     * Clase para manejar la escritura y modificacion de los archivos de la base de datos 
     */
    public class DataBaseWriter
    {
        static readonly String url_productores = "D:\\proyects\\feria\\feriaDatabase\\productores\\";
        static readonly String url_clientes = "D:\\proyects\\feria\\feriaDatabase\\clientes\\";
        static readonly String url_mist = "D:\\proyects\\feria\\feriaDatabase\\Mist\\";
        static readonly String url_solicitud = "D:\\proyects\\feria\\feriaDatabase\\Mist\\Solicitudes\\";

        /*
         * Metodo para agregar un identificador de pedido a un productor 
         */
        internal static void AddIdToPedidosList(int cedula) {
            XmlDocument xmlDoc = DataBaseLoader.LoadIdsPedidosProductorXml(cedula);
            List<int> idList = DataBaseLoader.LoadIdsPedidosProductor(cedula);
            int pedidoNuevo = 0;
            if (idList.Count != 0) {
                pedidoNuevo = idList.Last();
            }
            XmlNode rootNode = xmlDoc.ChildNodes[0];

            XmlNode nodeId = xmlDoc.CreateElement("IdPedido");
            XmlAttribute atributo;
            atributo = xmlDoc.CreateAttribute("Id");
            atributo.Value = (pedidoNuevo + 1).ToString();
            nodeId.Attributes.Append(atributo);
            rootNode.AppendChild(nodeId);

            xmlDoc.Save(url_productores + "pedidos//" + cedula.ToString() + "_ids.xml");
        }

        /*
         * Metodo para agregar un Pedido a un productor
         */
        internal static void AddPedidoAProductor(int cedula, Pedido pedido) {
            XmlDocument xmlDoc = DataBaseLoader.LoadPedidosXml(cedula);
            XmlNode rootNode = xmlDoc.ChildNodes[0];

            XmlNode nodePedido = xmlDoc.CreateElement("Pedido");

            XmlAttribute atributo;
            atributo = xmlDoc.CreateAttribute("IdPedido");
            atributo.Value = pedido.idPedido.ToString();
            nodePedido.Attributes.Append(atributo);

            atributo = xmlDoc.CreateAttribute("Usuario");
            atributo.Value = pedido.usuario;
            nodePedido.Attributes.Append(atributo);

            atributo = xmlDoc.CreateAttribute("Direccion");
            String direccion = pedido.direccion[0] + "," + pedido.direccion[1] + "," + pedido.direccion[2];
            atributo.Value = direccion;
            nodePedido.Attributes.Append(atributo);

            atributo = xmlDoc.CreateAttribute("Factura");
            atributo.Value = pedido.factura; ;
            nodePedido.Attributes.Append(atributo);

            XmlNode nodeArticulos = xmlDoc.CreateElement("Articulos");
            foreach (Articulo articulo in pedido.productos) {

                XmlNode nodeArticulo = xmlDoc.CreateElement("Articulo");

                atributo = xmlDoc.CreateAttribute("Producto");
                atributo.Value = articulo.Producto;
                nodeArticulo.Attributes.Append(atributo);

                atributo = xmlDoc.CreateAttribute("Cantidad");
                atributo.Value = articulo.cantidad.ToString();
                nodeArticulo.Attributes.Append(atributo);

                atributo = xmlDoc.CreateAttribute("Precio");
                atributo.Value = articulo.precio.ToString();
                nodeArticulo.Attributes.Append(atributo);

                atributo = xmlDoc.CreateAttribute("ModoVenta");
                atributo.Value = articulo.modoVenta;
                nodeArticulo.Attributes.Append(atributo);

                nodeArticulos.AppendChild(nodeArticulo);
                
            }
            nodePedido.AppendChild(nodeArticulos);
            rootNode.AppendChild(nodePedido);
            xmlDoc.Save(url_productores + "pedidos//" + cedula.ToString() + "_doc.xml");
        }

        /*
         * Metodo para agregar un usuario nuevo a la lista de usuarios
         */
        public static void AddUserNameToList(String usuario)
        {
            XmlDocument xmlDoc = DataBaseLoader.LoadClientesListXml();

            XmlNode rootNode = xmlDoc.ChildNodes[0];

            XmlNode nodeUser = xmlDoc.CreateElement("Usuario");
            XmlAttribute atributo;
            atributo = xmlDoc.CreateAttribute("User");
            atributo.Value = usuario;
            nodeUser.Attributes.Append(atributo);
            rootNode.AppendChild(nodeUser);

            xmlDoc.Save(url_mist + "ClientesList_doc.xml");
        }

        /*
         * Metodo para agregar un Articulo al carrito de un usuario
         */
        internal static Boolean AddProductoToCarrito(string user, Articulo articulo)
        {
            XmlDocument xmlDoc = DataBaseLoader.LoadUserCartXml(user);
            XmlNode rootNode = xmlDoc.ChildNodes[0];
            xmlDoc.AppendChild(rootNode);
            XmlNode nodeProducto = xmlDoc.CreateElement("Producto");
            XmlAttribute atributo;
            atributo = xmlDoc.CreateAttribute("NombreProducto");
            atributo.Value = articulo.Producto;
            nodeProducto.Attributes.Append(atributo);
            atributo = xmlDoc.CreateAttribute("CedulaProductor");
            atributo.Value = articulo.cedulaProductor.ToString();
            nodeProducto.Attributes.Append(atributo);
            atributo = xmlDoc.CreateAttribute("Precio");
            atributo.Value = articulo.precio.ToString();
            nodeProducto.Attributes.Append(atributo);
            atributo = xmlDoc.CreateAttribute("Cantidad");
            atributo.Value = articulo.cantidad.ToString();
            nodeProducto.Attributes.Append(atributo);
            atributo = xmlDoc.CreateAttribute("ModoVenta");
            atributo.Value = articulo.modoVenta;
            nodeProducto.Attributes.Append(atributo);

            rootNode.AppendChild(nodeProducto);
            xmlDoc.Save(url_clientes + "carritos//" + user + "_doc.xml");

            return true;
        }

        /*
         * Metodo para agregar una nueva cedula de un productor a la lista de cedulas
         */
        public static void AddCedulaProductor(int cedula) {
            XmlDocument xmlDoc = DataBaseLoader.LoadProductoresList();

            XmlNode rootNode = xmlDoc.ChildNodes[0];

            XmlNode nodeProductor = xmlDoc.CreateElement("Productor");
            XmlAttribute atributo;
            atributo = xmlDoc.CreateAttribute("Cedula");
            atributo.Value = cedula.ToString();
            nodeProductor.Attributes.Append(atributo);
            rootNode.AppendChild(nodeProductor);

            xmlDoc.Save(url_mist + "ProductoresList_doc.xml");
        }

        /*
         * Metodo para crear un nuevo archivo xml con un productor nuevo
         */
        public static void CrearNuevoProductor(Productor productor)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("Productor");
            xmlDoc.AppendChild(rootNode);

            XmlNode nodeProductor = xmlDoc.CreateElement("Informacion");
            XmlAttribute atributo;

            atributo = xmlDoc.CreateAttribute("Cedula");
            atributo.Value = productor.cedula.ToString();
            nodeProductor.Attributes.Append(atributo);
            atributo = xmlDoc.CreateAttribute("FechaNacimiento");
            atributo.Value = productor.fechaNacimiento;
            nodeProductor.Attributes.Append(atributo);
            atributo = xmlDoc.CreateAttribute("Telefono");
            atributo.Value = productor.telefono.ToString();
            nodeProductor.Attributes.Append(atributo);
            atributo = xmlDoc.CreateAttribute("Sinpe");
            atributo.Value = productor.sinpe.ToString();
            nodeProductor.Attributes.Append(atributo);
            rootNode.AppendChild(nodeProductor);

            nodeProductor = xmlDoc.CreateElement("Nombre");
            atributo = xmlDoc.CreateAttribute("Nombre");
            atributo.Value = productor.nombre;
            nodeProductor.Attributes.Append(atributo);
            atributo = xmlDoc.CreateAttribute("PrimerApellido");
            atributo.Value = productor.apellido1;
            nodeProductor.Attributes.Append(atributo);
            atributo = xmlDoc.CreateAttribute("SegundoApellido");
            atributo.Value = productor.apellido2;
            nodeProductor.Attributes.Append(atributo);
            rootNode.AppendChild(nodeProductor);

            nodeProductor = xmlDoc.CreateElement("Direccion");
            atributo = xmlDoc.CreateAttribute("Provincia");
            atributo.Value = productor.direccion[0];
            nodeProductor.Attributes.Append(atributo);
            atributo = xmlDoc.CreateAttribute("Canton");
            atributo.Value = productor.direccion[1];
            nodeProductor.Attributes.Append(atributo);
            atributo = xmlDoc.CreateAttribute("Distrito");
            atributo.Value = productor.direccion[2];
            nodeProductor.Attributes.Append(atributo);
            rootNode.AppendChild(nodeProductor);

            nodeProductor = xmlDoc.CreateElement("LugaresDisponibles");
            int indice = 0;
            foreach (String lugar in productor.direccionesEntrega)
            {
                atributo = xmlDoc.CreateAttribute("Lugar" + indice.ToString());
                atributo.Value = lugar;
                nodeProductor.Attributes.Append(atributo);
                indice += 1;
            }
            rootNode.AppendChild(nodeProductor);

            nodeProductor = xmlDoc.CreateElement("Productos");

            rootNode.AppendChild(nodeProductor);

            xmlDoc.Save(url_productores + productor.cedula.ToString() + "_doc.xml");

        }

        /*
         * Metodo para borrar una solicitud de union de un productor de la lista de solicitudes, lo busca por id de solicitud
         */
        internal static bool DeleteSolicitud(int id)
        {
            XmlDocument xmlDoc = DataBaseLoader.LoadSolicitudXml();
            XmlNodeList nodeList = xmlDoc.DocumentElement.ChildNodes;
            List<int> ids = new List<int>();
            List<int> cedulasProductores = new List<int>();
            foreach (XmlNode node in nodeList)
            {
                if (int.Parse(node.Attributes["Id"].Value) != id)
                {
                    cedulasProductores.Add(int.Parse(node.Attributes["Cedula"].Value));
                    ids.Add(int.Parse(node.Attributes["Id"].Value));
                }
                
            }
            File.Delete(url_solicitud + "Solicitudes_doc.xml");
            File.Delete(url_solicitud + id.ToString() + "_doc.xml");
            for (int i = 0; i < cedulasProductores.Count; i++)
            {
                AddSolicitudId(ids[i],cedulasProductores[i]);
            }
            return true;
        }

        /*
         * Metodo para borrar una categoria de la lista de categorias
         * Devuelve false si algun producto depende de la categoria que se quiere borrar
         * True, cuando se puede borrar sin problemas
         */
        internal static bool DeleteCat(int id)
        {
            XmlDocument catDoc = DataBaseLoader.LoadCategoriasXml();
            List<Categoria> list = new List<Categoria>();
            XmlNodeList nodeList = catDoc.DocumentElement.ChildNodes;
            String anterior = "";
            foreach (XmlNode node in nodeList)
            {
                if (id != int.Parse(node.Attributes["Id"].Value))
                {
                    list.Add(new Categoria(int.Parse(node.Attributes["Id"].Value), node.Attributes["Nombre"].Value));
                }
                else {
                    anterior = node.Attributes["Nombre"].Value;
                }
            }
            foreach (string file in Directory.EnumerateFiles(url_productores, "*_doc.xml"))
            {
                Productor productor = DataBaseLoader.LoadProductor(file);
                foreach (Producto producto in productor.catalogo)
                {
                    if (producto.categoria == anterior)
                    {
                        return false;
                    }
                }
            }
            File.Delete(url_mist + "Categorias_doc.xml");
            foreach (Categoria cat in list) {
                AddCategoria(cat);
            }
            return true;
        }

        /*
         * Metodo para modificar una categoria, la busca por id y le asigna el nuevo nombre
         * Busca los porductos que tengan ese nomrbe de categoria y se los actualiza
         */
        internal static Boolean ModifyCat(int id, string value)
        {
            XmlDocument catDoc = DataBaseLoader.LoadCategoriasXml();
            List<Categoria> list = new List<Categoria>();
            XmlNodeList nodeList = catDoc.DocumentElement.ChildNodes;
            String anterior = "";
            File.Delete(url_mist + "Categorias_doc.xml");
            foreach (XmlNode node in nodeList)
            {
                Categoria aux = null;
                if (id == int.Parse(node.Attributes["Id"].Value))
                {
                    anterior = node.Attributes["Nombre"].Value;
                    aux = new Categoria(int.Parse(node.Attributes["Id"].Value), value);
                }
                else {
                    aux = new Categoria(int.Parse(node.Attributes["Id"].Value), node.Attributes["Nombre"].Value); 
                }
                list.Add(aux);
                AddCategoria(aux);
            }
            foreach (string file in Directory.EnumerateFiles(url_productores, "*_doc.xml"))
            {
                Productor productor = DataBaseLoader.LoadProductor(file);
                CrearNuevoProductor(productor);
                foreach (Producto producto in productor.catalogo) {
                    if (producto.categoria == anterior) {
                        producto.categoria = value;
                    }
                    AddProducto(productor.cedula,producto);
                }
            }
            return true;
        }

        /*
         * Metodo para eliminar un cliente, si no existe devuelve false, caso contrario true
         */
        internal static bool DeleteCliente(string user)
        {
            try
            {
                File.Delete(url_clientes + user + "_doc.xml");
                List<String> listUsers = DataBaseLoader.GetAllUsers().ToList();
                listUsers.Remove(user);
                File.Delete(url_mist + "ClientesList_doc.xml");
                foreach (String userName in listUsers)
                {
                    AddUserNameToList(userName);
                }
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        /*
         * Metodo para agregar un producto a un productor, buscandolo por cedula
         */
        public static void AddProducto(int cedula, Producto producto)
        {
            XmlDocument xmlDoc = DataBaseLoader.LoadProductorXml(cedula);

            XmlNode productos = xmlDoc.LastChild.LastChild;
            XmlNode nodeProducto = xmlDoc.CreateElement("Producto");

            XmlAttribute atributo;

            atributo = xmlDoc.CreateAttribute("Nombre");
            atributo.Value = producto.nombre;
            nodeProducto.Attributes.Append(atributo);

            atributo = xmlDoc.CreateAttribute("Categoria");
            atributo.Value = producto.categoria;
            nodeProducto.Attributes.Append(atributo);

            atributo = xmlDoc.CreateAttribute("Imagen");
            atributo.Value = producto.image;
            nodeProducto.Attributes.Append(atributo);

            atributo = xmlDoc.CreateAttribute("Precio");
            atributo.Value = producto.precio.ToString();
            nodeProducto.Attributes.Append(atributo);

            atributo = xmlDoc.CreateAttribute("ModoVenta");
            atributo.Value = producto.modoVenta;
            nodeProducto.Attributes.Append(atributo);

            atributo = xmlDoc.CreateAttribute("Disponibles");
            atributo.Value = producto.disponible.ToString();
            nodeProducto.Attributes.Append(atributo);

            productos.AppendChild(nodeProducto);
            xmlDoc.Save(url_productores + cedula.ToString() + "_doc.xml");
        }

        /*
         * Metodo para modificar un producto de un productor
         */
        public static void ModifyProducto(String path, Producto producto)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNode productos = xmlDoc.LastChild.LastChild;
            XmlNode nodeProducto = xmlDoc.CreateElement("Producto");

            XmlAttribute atributo;

            atributo = xmlDoc.CreateAttribute("Nombre");
            atributo.Value = producto.nombre;
            nodeProducto.Attributes.Append(atributo);

            atributo = xmlDoc.CreateAttribute("Categoria");
            atributo.Value = producto.categoria;
            nodeProducto.Attributes.Append(atributo);

            atributo = xmlDoc.CreateAttribute("Imagen");
            atributo.Value = producto.image;
            nodeProducto.Attributes.Append(atributo);

            atributo = xmlDoc.CreateAttribute("Precio");
            atributo.Value = producto.precio.ToString();
            nodeProducto.Attributes.Append(atributo);

            atributo = xmlDoc.CreateAttribute("ModoVenta");
            atributo.Value = producto.modoVenta;
            nodeProducto.Attributes.Append(atributo);

            atributo = xmlDoc.CreateAttribute("Disponibles");
            atributo.Value = producto.disponible.ToString();
            nodeProducto.Attributes.Append(atributo);

            productos.AppendChild(nodeProducto);
            xmlDoc.Save(path);
        }

        /*
         * Metodo para eliminar un productor
         * Devuelve true si se puede borrar, false si no existe
         */
        internal static bool DeleteProductor(int id)
        {
            try {
                File.Delete(url_productores + id.ToString() + "_doc.xml");
                List<int> listCedulas = DataBaseLoader.GetAllCedulasProductores().ToList();
                listCedulas.Remove(id);
                File.Delete(url_mist + "ProductoresList_doc.xml");
                foreach (int cedula in listCedulas) {
                    AddCedulaProductor(cedula);
                }
                return true;
            } catch (ArgumentException) {
                return false;
            }
            
        }

        /*
         * Metodo para agregar un nuevo xml con un usuario
         */
        public static void AddUsuario(Cliente cliente)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("Cliente");
            xmlDoc.AppendChild(rootNode);

            XmlNode nodeInfo = xmlDoc.CreateElement("Informacion");
            XmlAttribute atributo;
            atributo = xmlDoc.CreateAttribute("Cedula");
            atributo.Value = cliente.cedula.ToString();
            nodeInfo.Attributes.Append(atributo);
            atributo = xmlDoc.CreateAttribute("FechaNacimiento");
            atributo.Value = cliente.fechaNacimiento;
            nodeInfo.Attributes.Append(atributo);
            atributo = xmlDoc.CreateAttribute("Telefono");
            atributo.Value = cliente.telefono.ToString();
            nodeInfo.Attributes.Append(atributo);
            rootNode.AppendChild(nodeInfo);

            XmlNode nodeNombre = xmlDoc.CreateElement("Nombre");
            atributo = xmlDoc.CreateAttribute("Nombre");
            atributo.Value = cliente.nombre;
            nodeNombre.Attributes.Append(atributo);
            atributo = xmlDoc.CreateAttribute("PrimerApellido");
            atributo.Value = cliente.apellido1;
            nodeNombre.Attributes.Append(atributo);

            atributo = xmlDoc.CreateAttribute("SegundoApellido");
            atributo.Value = cliente.apellido2;
            nodeNombre.Attributes.Append(atributo);
            rootNode.AppendChild(nodeNombre);

            XmlNode nodeLogIn = xmlDoc.CreateElement("LogIn");
            atributo = xmlDoc.CreateAttribute("Usuario");
            atributo.Value = cliente.GetLogIn();
            nodeLogIn.Attributes.Append(atributo);
            atributo = xmlDoc.CreateAttribute("Password");
            atributo.Value = cliente.GetPassWord();
            nodeLogIn.Attributes.Append(atributo);
            rootNode.AppendChild(nodeLogIn);

            XmlNode nodeDireccion = xmlDoc.CreateElement("Direccion");
            atributo = xmlDoc.CreateAttribute("Provincia");
            atributo.Value = cliente.direccion[0];
            nodeDireccion.Attributes.Append(atributo);
            atributo = xmlDoc.CreateAttribute("Canton");
            atributo.Value = cliente.direccion[1];
            nodeDireccion.Attributes.Append(atributo);
            atributo = xmlDoc.CreateAttribute("Distrito");
            atributo.Value = cliente.direccion[2];
            nodeDireccion.Attributes.Append(atributo);
            rootNode.AppendChild(nodeDireccion);

            xmlDoc.Save(url_clientes + cliente.GetLogIn() + "_doc.xml");
        }

        /*
         * Metodo para agregar una categoria nueva a la lista de categorias
         */
        public static void AddCategoria(Categoria categoria)
        {
            XmlDocument xmlDoc = DataBaseLoader.LoadCategoriasXml();

            XmlNode rootNode = xmlDoc.ChildNodes[0];

            XmlNode nodeCategoria = xmlDoc.CreateElement("Categoria");
            XmlAttribute atributo;
            atributo = xmlDoc.CreateAttribute("Id");
            atributo.Value = categoria.id.ToString();
            nodeCategoria.Attributes.Append(atributo);

            atributo = xmlDoc.CreateAttribute("Nombre");
            atributo.Value = categoria.nombre;
            nodeCategoria.Attributes.Append(atributo);

            rootNode.AppendChild(nodeCategoria);

            xmlDoc.Save(url_mist + "Categorias_doc.xml");
        }

        /*
         * Metodo para agregar un xml de solicitud con la informacion de un productor
         */
        public static void AddSolicitud(Solicitud solicitud) {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("Productor");
            xmlDoc.AppendChild(rootNode);
            XmlNode nodeProductor = xmlDoc.CreateElement("Informacion");
            XmlAttribute atributo;
            Productor productor = solicitud.productor;
            atributo = xmlDoc.CreateAttribute("Cedula");
            atributo.Value = productor.cedula.ToString();
            nodeProductor.Attributes.Append(atributo);
            atributo = xmlDoc.CreateAttribute("FechaNacimiento");
            atributo.Value = productor.fechaNacimiento;
            nodeProductor.Attributes.Append(atributo);
            atributo = xmlDoc.CreateAttribute("Telefono");
            atributo.Value = productor.telefono.ToString();
            nodeProductor.Attributes.Append(atributo);
            atributo = xmlDoc.CreateAttribute("Sinpe");
            atributo.Value = productor.sinpe.ToString();
            nodeProductor.Attributes.Append(atributo);
            rootNode.AppendChild(nodeProductor);

            nodeProductor = xmlDoc.CreateElement("Nombre");
            atributo = xmlDoc.CreateAttribute("Nombre");
            atributo.Value = productor.nombre;
            nodeProductor.Attributes.Append(atributo);
            atributo = xmlDoc.CreateAttribute("PrimerApellido");
            atributo.Value = productor.apellido1;
            nodeProductor.Attributes.Append(atributo);
            atributo = xmlDoc.CreateAttribute("SegundoApellido");
            atributo.Value = productor.apellido2;
            nodeProductor.Attributes.Append(atributo);
            rootNode.AppendChild(nodeProductor);

            nodeProductor = xmlDoc.CreateElement("Direccion");
            atributo = xmlDoc.CreateAttribute("Provincia");
            atributo.Value = productor.direccion[0];
            nodeProductor.Attributes.Append(atributo);
            atributo = xmlDoc.CreateAttribute("Canton");
            atributo.Value = productor.direccion[1];
            nodeProductor.Attributes.Append(atributo);
            atributo = xmlDoc.CreateAttribute("Distrito");
            atributo.Value = productor.direccion[2];
            nodeProductor.Attributes.Append(atributo);
            rootNode.AppendChild(nodeProductor);

            nodeProductor = xmlDoc.CreateElement("LugaresDisponibles");
            int indice = 0;
            foreach (String lugar in productor.direccionesEntrega)
            {
                atributo = xmlDoc.CreateAttribute("Lugar" + indice.ToString());
                atributo.Value = lugar;
                nodeProductor.Attributes.Append(atributo);
                indice += 1;
            }
            rootNode.AppendChild(nodeProductor);

            nodeProductor = xmlDoc.CreateElement("Productos");

            rootNode.AppendChild(nodeProductor);

            xmlDoc.Save(url_solicitud + solicitud.id + "_doc.xml");
        }

        /*
         * Metodo para agregar un identificador con su respectiva cedula, para la lista de solicitudes de productores
         */
        public static void AddSolicitudId(int id, int cedula) {
            XmlDocument xmlDoc = DataBaseLoader.LoadSolicitudXml();

            XmlNode rootNode = xmlDoc.ChildNodes[0];
            xmlDoc.AppendChild(rootNode);

            XmlNode nodeCategoria = xmlDoc.CreateElement("Categoria");
            XmlAttribute atributo;
            atributo = xmlDoc.CreateAttribute("Id");
            atributo.Value = id.ToString();
            nodeCategoria.Attributes.Append(atributo);
            atributo = xmlDoc.CreateAttribute("Cedula");
            atributo.Value = cedula.ToString();
            nodeCategoria.Attributes.Append(atributo);

            rootNode.AppendChild(nodeCategoria);

            xmlDoc.Save(url_solicitud + "Solicitudes_doc.xml");
        }

    }

}