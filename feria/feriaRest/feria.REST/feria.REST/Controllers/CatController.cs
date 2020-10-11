using feria.REST.Controllers.DBManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace feria.REST.Controllers
{
    public class CatController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Categoria> Get()
        {
            return DataBaseLoader.LoadCategorias();
        }

        // GET api/<controller>/5
        public Categoria Get(int id)
        {
            return DataBaseLoader.LoadCategoria(id);
        }

        // POST api/<controller>?id=000&nombre=papapapa
        public void Post(int id, string nombre)
        {
            Categoria categoria = new Categoria(id,nombre);
            DataBaseWriter.AddCategoria(categoria);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
            
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}