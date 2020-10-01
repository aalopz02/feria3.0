using feria.REST.Controllers.DBManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
//https://www.tutorialsteacher.com/webapi/parameter-binding-in-web-api
//https://csharp.net-tutorials.com/xml/reading-xml-with-the-xmldocument-class/
namespace feria.REST.Controllers
{
    public class ProductorController : ApiController
    {
        // Post api/Productor?id=000&info=nombre-appellido-apellido2-provincia-canton-distrito-fecha-1-2-lugaresN-lugarQ
        public void GET(int id, string info)
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
            DataBaseWriter.CrearNuevoProductor(productor).ToString();
        }
    }
}