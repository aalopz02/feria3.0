using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace feria.REST.Controllers.DBManager
{
    /*
     * Clase para manejar un cliente/usuario
     */
    public class Cliente
    {
        public int cedula;//Cedula del usuario
        public String nombre;//nombre del usuario
        public String apellido1;//apellido del usuario
        public String apellido2;//segundo apellido del usuario
        public List<String> direccion;//Lista para manejar la direccion, en el orden [provincia,canton,distrito]
        public String fechaNacimiento;//fecha nacimiento de usuario
        public int telefono;//telefono del usuario
        private String user;//usuario
        private String password;//contrasenna para inicio de sesion

        /*
         * Constructor para inicializar cliente, inicializa todas las variables, nombreFull : [nombre,apellido1,apellido2]
         */
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

        /*
         * Metodo para obtener la clave del usuario
         */
        public String GetPassWord() 
        {
            return this.password;
        }

        /*
         * Metodo para obtener el usuario
         */
        public String GetLogIn()
        {
            return this.user;
        }

    }
}