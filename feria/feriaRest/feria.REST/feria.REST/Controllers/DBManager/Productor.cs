using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace feria.REST.Controllers.DBManager
{
    public class Productor
    {
        private int cedula;
        private String nombre;
        private String apellido1;
        private String apellido2;
        private List<String> direccion;
        private String fechaNacimiento;
        private int telefono;
        private int sinpe;
        private List<String> direccionesEntrega;

        public Productor(int cedula, List<String> nombreFull, List<String> direccion, String fechaNacimiento, int telefono, int sinpe, List<String> entrega)
        {
            this.cedula = cedula;
            this.nombre = nombreFull[0];
            this.apellido1 = nombreFull[1];
            this.apellido2 = nombreFull[2];
            this.direccion = direccion;
            this.direccionesEntrega = entrega;
            this.fechaNacimiento = fechaNacimiento;
            this.telefono = telefono;
            this.sinpe = sinpe;

        }
    }
}