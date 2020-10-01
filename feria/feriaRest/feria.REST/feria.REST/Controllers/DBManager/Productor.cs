using System;
using System.Collections.Generic;

namespace feria.REST.Controllers.DBManager
{
    public class Productor
    {
        public int cedula;
        public String nombre;
        public String apellido1;
        public String apellido2;
        public List<String> direccion;
        public String fechaNacimiento;
        public int telefono;
        public int sinpe;
        public List<String> direccionesEntrega;
        public List<Producto> catalogo;

        public Productor(int cedula, List<String> nombreFull, List<String> direccion, String fechaNacimiento, String telefono, String sinpe, List<String> entrega)
        {
            this.cedula = cedula;
            this.nombre = nombreFull[0];
            this.apellido1 = nombreFull[1];
            this.apellido2 = nombreFull[2];
            this.direccion = direccion;
            this.direccionesEntrega = entrega;
            this.fechaNacimiento = fechaNacimiento;
            this.telefono = int.Parse(telefono);
            this.sinpe = int.Parse(sinpe);

        }

        public void AddProducto(Producto producto) {
            DataBaseWriter.AddProducto(cedula,producto);
        }
    }
}