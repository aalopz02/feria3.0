using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace feria.REST.Controllers.DBManager
{
    /*
     * Clase para manejar una solicitud de union de un productor
     */
    public class Solicitud
    {
        public Productor productor;// Clase con el productor que se quiere agregar
        public int id; //Id de la solicitud

        /*
         * Contructor de la clase, inicializa ambas variables
         */
        public Solicitud(int id, Productor productor)
        {
            this.id = id;
            this.productor = productor;
        }

    }
}