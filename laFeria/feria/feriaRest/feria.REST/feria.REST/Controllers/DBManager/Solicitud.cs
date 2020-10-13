using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace feria.REST.Controllers.DBManager
{
    public class Solicitud
    {
        public Productor productor;
        public int id;

        public Solicitud(int id, Productor productor)
        {
            this.id = id;
            this.productor = productor;
        }

    }
}