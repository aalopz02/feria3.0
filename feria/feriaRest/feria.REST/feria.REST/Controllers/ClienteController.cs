using feria.REST.Controllers.DBManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace feria.REST.Controllers
{
    public class ClienteController : ApiController
    {
        //Post
        // /api/Cliente?cedula=12345&info=nombre-appellido-apellido2-provincia-canton-distrito-fecha-8888-aalopz-clave
        public void Get(int cedula, string info) 
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
            Cliente cliente = new Cliente(cedula,listaNombre,listaDireccion,valores[6],int.Parse(valores[7]),valores[8],valores[9]);
            DataBaseWriter.AddUsuario(cliente);
        }
    }
}