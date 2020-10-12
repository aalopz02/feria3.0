using Antlr.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace feria.REST.Controllers.DBManager
{
    public class DataBaseWriter
    {
        static readonly String url_productores = "D:\\proyects\\feria\\feriaDatabase\\productores\\";
        static readonly String url_clientes = "D:\\proyects\\feria\\feriaDatabase\\clientes\\";
        static readonly String url_mist = "D:\\proyects\\feria\\feriaDatabase\\Mist\\";

        public static XmlDocument CrearNuevoProductor(Productor productor)
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

            return xmlDoc;

        }

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

        internal static bool ModifyCat(int id, string value)
        {
            XmlDocument catDoc = DataBaseLoader.LoadCategoriasXml();
            List<Categoria> list = new List<Categoria>();
            XmlNodeList nodeList = catDoc.DocumentElement.ChildNodes;
            String anterior = "";
            File.Delete(url_mist + "Categorias_doc.xml");
            int index = 0;
            foreach (XmlNode node in nodeList)
            {
                if (id == int.Parse(node.Attributes["Id"].Value))
                {
                    anterior = node.Attributes["Nombre"].Value;
                    list.Add(new Categoria(int.Parse(node.Attributes["Id"].Value), node.Attributes["Nombre"].Value));
                }
                else { 
                    list.Add(new Categoria(int.Parse(node.Attributes["Id"].Value), value)); 
                }
                AddCategoria(list[index]);
                index++;
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

        internal static bool DeleteCliente(string user)
        {
            try
            {
                File.Delete(url_clientes + user + "_doc.xml");
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

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

        internal static bool DeleteProductor(int id)
        {
            try {
                File.Delete(url_productores + id.ToString() + "_doc.xml");
                return true;
            } catch (ArgumentException) {
                return false;
            }
            
        }

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

        public static void AddCategoria(Categoria categoria)
        {
            XmlDocument xmlDoc = DataBaseLoader.LoadCategoriasXml();

            XmlNode rootNode = xmlDoc.ChildNodes[0];
            xmlDoc.AppendChild(rootNode);

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
    }
}