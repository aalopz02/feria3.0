using feria.REST.Controllers.DBManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Web;
using System.Web.Http;

namespace feria.REST.Controllers
{
    public class ValuesController : ApiController
    {

        // GET api/values
        public IEnumerable<string> Get()
        {
            
            return new string[] { "ok" };
        }

        // GET api/values/
        public string Get(String id)
        {
            return id;
        }

        // POST
        public void Post([FromBody] string value)
        {
            
        }

        // PUT api/values/5 api/values/cedula/nombre-appellido-apellido2-provincia-canton-distrito-fecha-telefono-sinpe-lugaresN
        public void Put(int id, [FromBody] string value)
        {
           
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            
        }
    }
}
