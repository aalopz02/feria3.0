using feria.REST.Controllers.DBManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
//https://www.tutorialsteacher.com/webapi/parameter-binding-in-web-api
//https://csharp.net-tutorials.com/xml/reading-xml-with-the-xmldocument-class/
//https://www.tutorialsteacher.com/webapi/test-web-api
namespace feria.REST.Controllers
{
    public class ProductorController : ApiController
    {
        // GET
        //api/Productor?cedula=000
        public List<Producto> GetProductos(int cedula) {
            return DataBaseLoader.LoadProductorInventory(cedula);
        }

        // GET
        //api/Productor?id=000
        public Productor Get(int id)
        {
            return DataBaseLoader.LoadProductor(id);
        }

        // Post
         //api/Productor?id=000&info=nombre-appellido-apellido2-provincia-canton-distrito-fecha-1-2-lugaresN-lugarQ
        public void Post(int id, string info)
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

        //api/Productor?id=1223456&infoproducto=nombreProducto-categoria-22-kilo-adadadad
        public void AddProductor(int id, string infoproducto)
        {
            String[] valores = infoproducto.Split('-');
            Productor productor = DataBaseLoader.LoadProductor(id);
            Producto producto = new Producto(valores[0], valores[1], int.Parse(valores[2]), valores[3],valores[4]);
            productor.AddProducto(producto);
        }

        //api/Productor?id=000&info=nombre-appellido-apellido2-provincia-canton-distrito-fecha-1-2-lugaresN-lugarQ
        public void PATCH(int id, String info) {
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