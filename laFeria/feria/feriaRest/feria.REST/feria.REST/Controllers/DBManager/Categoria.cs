using Antlr.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace feria.REST.Controllers.DBManager
{
    /*
     * Clase para manejar la categoria
     */
    public class Categoria
    {
        public int id;//Id de la categoria
        public String nombre;//Nombre de la categoria

        /*
         * Constructor de categoria, inicializa los dos valores de la clase
         */
        public Categoria(int id, string nombre) {
            this.id = id;
            this.nombre = nombre;
        }


    }
}