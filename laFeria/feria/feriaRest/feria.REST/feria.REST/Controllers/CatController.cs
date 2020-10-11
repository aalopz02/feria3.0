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

        // GET api/<controller>?id=5
        public Categoria Get(int id)
        {
            return DataBaseLoader.LoadCategoria(id);
        }

        // POST api/<controller>?id=12&nombre=papapapa
        public Boolean Post(int id, string nombre)
        {
            if (DataBaseLoader.LoadCategoria(id) != null) {
                return false;
            }
            Categoria categoria = new Categoria(id,nombre);
            DataBaseWriter.AddCategoria(categoria);
            return true;
        }

        // PUT api/<controller>?id=0&value=nuevo
        public Boolean Put(int id, string value)
        {
            return DataBaseWriter.ModifyCat(id,value);
        }

        // DELETE api/<controller>?id=5
        public Boolean Delete(int id)
        {
            return DataBaseWriter.DeleteCat(id);
        }
    }
}