using Antlr.Runtime.Tree;
using feria.REST.Controllers.DBManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace feria.REST.Controllers
{
    public class SolicitudController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Solicitud> Get()
        {
            return DataBaseLoader.LoadSolicitudes();
        }

        // GET api/<controller>/5
        public Solicitud Get(int id)
        {
            return DataBaseLoader.LoadSolicitud(id);
        }

        // POST 
        //api/Solicitud?id=000&info=nombre-appellido-apellido2-provincia-canton-distrito-fecha-1-2-lugaresN-lugarQ
        public bool Post(int id, string info)
        {
            if (DataBaseLoader.GetAllCedulasProductores().Contains(id)) {
                return false;
            }
            int numeroSolicitud = DataBaseLoader.LoadLastSolicitudId(id)+1;
            if (numeroSolicitud == -1) {return false;}
            if (DataBaseLoader.LoadProductor(id) != null)
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
            Solicitud solicitud = new Solicitud(numeroSolicitud, productor);
            DataBaseWriter.AddSolicitud(solicitud);
            DataBaseWriter.AddSolicitudId(solicitud.id,productor.cedula);
            return true;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5

        public bool Delete(int id)
        {
            return DataBaseWriter.DeleteSolicitud(id);
        }
    }
}