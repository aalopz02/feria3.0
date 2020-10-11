using Antlr.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace feria.REST.Controllers.DBManager
{
    public class Categoria
    {
        public int id;
        public String nombre;

        public Categoria(int id, string nombre) {
            this.id = id;
            this.nombre = nombre;
        }


    }
}