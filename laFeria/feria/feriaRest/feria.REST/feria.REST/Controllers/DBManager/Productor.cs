using System;
using System.Collections.Generic;

namespace feria.REST.Controllers.DBManager
{
    /*
     * Clase para manejar un productor 
     */
    public class Productor
    {
        public int cedula; // Cedula del productor
        public String nombre; // nombre del productor
        public String apellido1; // apellido del productor
        public String apellido2; // segundo apellido del productor
        public List<String> direccion; // lista con la direccion del productor, de la forma [provincia,canton,distrito]
        public String fechaNacimiento; //fecha de nacimiento del productor
        public int telefono; // telefono del productor
        public int sinpe; // numero de sinpe para pagos
        public List<String> direccionesEntrega; // lista con las direcciones donde el productor puede hacer entregas
        public List<Producto> catalogo; // lista de Productos que el productor vende

        /*
         * Constructor de la clase
         */
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

        /*
         * Inicializa la lista de productos disponibles 
         */
        public void SetCatalogo(List<Producto> catalogo) {
            this.catalogo = catalogo;
        }
        
        /*
         * Funcion que agrega un producto al productor
         */
        public void AddProducto(Producto producto) {
            DataBaseWriter.AddProducto(cedula,producto);
        }
    }
}