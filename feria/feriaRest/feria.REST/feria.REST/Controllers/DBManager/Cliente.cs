using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace feria.REST.Controllers.DBManager
{
    public class Cliente
    {
        public int cedula;
        public String nombre;
        public String apellido1;
        public String apellido2;
        public List<String> direccion;
        public String fechaNacimiento;
        public int telefono;
        private String user;
        private String password;
        
        public Cliente(int cedula, List<String> nombreFull, List<String> direccion, String fechaNacimiento, int telefono, String usuario, String password) {
            this.cedula = cedula;
            this.nombre = nombreFull[0];
            this.apellido1 = nombreFull[1];
            this.apellido2 = nombreFull[2];
            this.direccion = direccion;
            this.password = password;
            this.fechaNacimiento = fechaNacimiento;
            this.telefono = telefono;
            this.user = usuario;

        }

        public Cliente() { }




    }
}