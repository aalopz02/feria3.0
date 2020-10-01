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

        public static XmlDocument LoadProductor(int cedula)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(url_productores + cedula.ToString() + "_doc.xml");
             return xmlDoc;
        }

        
    }

}