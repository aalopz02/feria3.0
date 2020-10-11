using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace feria.REST.Controllers.DBManager
{
    public class DataBaseLoader
    {   
        static readonly String url_mist = "D:\\proyects\\feria\\feriaDatabase\\Mist\\";
        static readonly String url_productores = "D:\\proyects\\feria\\feriaDatabase\\productores\\";
        static readonly String url_clientes = "D:\\proyects\\feria\\feriaDatabase\\clientes\\";

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

        public static XmlDocument LoadCategoriasXml() {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(url_mist + "Categorias_doc.xml");
            return xmlDoc;
        }

        public static XmlDocument LoadProductorXml(int cedula)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try {
                xmlDoc.Load(url_productores + cedula.ToString() + "_doc.xml");
                return xmlDoc;
            } catch (System.ArgumentException) {
                return null;
            }
        }

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
            productor.SetCatalogo(LoadProductorInventory(cedula));
            return productor;
        }

        public static Productor LoadProductor(String path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            foreach(Producto producto in LoadProductorInventory(cedula))
            {

            }

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
            foreach (XmlAttribute lugarEntrega in nodeLugares.Attributes)
            {
                entrega.Add(lugarEntrega.Value.ToString());
            }
            Productor productor = new Productor(cedula,
                                                nombreFull,
                                                direccion,
                                                nodeInformacion.Attributes["FechaNacimiento"].Value,
                                                nodeInformacion.Attributes["Telefono"].Value,
                                                nodeInformacion.Attributes["Sinpe"].Value,
                                                entrega);
            
            return productor;
        }

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

        public static XmlDocument LoadClienteXml(String cedula)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try {
                xmlDoc.Load(url_clientes + cedula + "_doc.xml");
                return xmlDoc;
            } catch (System.ArgumentException)
            {
                return null;
            }
        }

        public static Cliente CheckLogIn(String usuario, String clave) {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(url_clientes + usuario + "_doc.xml");
            }
            catch (System.ArgumentException) {
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
    }

}