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
        // GET de todas las categorias que existen el la base de datos
        //api/Cat?
        public IEnumerable<Categoria> Get()
        {
            return DataBaseLoader.LoadCategorias();
        }

        // GET de una categoria en especifico, buscando por id
        //api/Cat?id=5
        public Categoria Get(int id)
        {
            return DataBaseLoader.LoadCategoria(id);
        }

        // POST para crear una nueva categoria
        //api/Cat?id=12&nombre=papapapa
        public Boolean Post(int id, string nombre)
        {
            if (DataBaseLoader.LoadCategoria(id) != null) {
                return false;
            }
            Categoria categoria = new Categoria(id,nombre);
            DataBaseWriter.AddCategoria(categoria);
            return true;
        }

        // PUT para modificar el nombre de una categoria
        //api/Cat?id=0&value=nuevo
        public Boolean Put(int id, string value)
        {
            if (DataBaseLoader.LoadCategoria(id) == null)
            {
                return false;
            }
            return DataBaseWriter.ModifyCat(id,value);
        }

        // DELETE de una categoria
        //api/Cat?id=5
        public Boolean Delete(int id)
        {
            return DataBaseWriter.DeleteCat(id);
        }
    }
}