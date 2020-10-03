using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace feria.REST.Controllers.DBManager
{
    public class DataBaseLoader
    {
        static String url_productores = "D:\\proyects\\feria\\laFeria\\feria\\feriaRest\\feriaDatabase\\productores\\";
        static String url_clientes = "D:\\proyects\\feria\\laFeria\\feria\\feriaRest\\feriaDatabase\\clientes\\";

        public static XmlDocument LoadProductorXml(int cedula)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(url_productores + cedula.ToString() + "_doc.xml");
            return xmlDoc;
        }

        public static Productor LoadProductor(int cedula) {
            XmlDocument xmlDoc = LoadProductorXml(cedula);
            XmlNodeList nodeList = xmlDoc.DocumentElement.ChildNodes;
            XmlNode nodeInformacion = nodeList.Item(0);
            XmlNode nodeNombre = nodeList.Item(1);
            XmlNode nodeDireccion = nodeList.Item(2);
            XmlNode nodeLugares = nodeList.Item(3);
            //XmlNode nodeProductos = xmlDoc.DocumentElement.GetAttributeNode("Productos");
            List<String> nombreFull = new List<string> {nodeNombre.Attributes["Nombre"].Value,
                                                        nodeNombre.Attributes["PrimerApellido"].Value,
                                                        nodeNombre.Attributes["SegundoApellido"].Value};
            List<String> direccion = new List<string> {nodeDireccion.Attributes["Provincia"].Value,
                                                       nodeDireccion.Attributes["Canton"].Value,
                                                       nodeDireccion.Attributes["Distrito"].Value};
            List<String> entrega = new List<string>();
            foreach (XmlAttribute lugarEntrega in nodeLugares.Attributes) {
                int index = 0;
                entrega.Add(lugarEntrega.ToString());
                Console.WriteLine(lugarEntrega.ToString());
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
        
    }

}