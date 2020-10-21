using feria.REST.Controllers.DBManager;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace feria.REST.Controllers
{
    public class ClienteController : ApiController
    {

        //GET de todos los productores que tienen disponible como lugar de entrega el distrito de un usuario 
        ///api/Cliente?usuario=aalopz
        public IEnumerable<Productor> GetProductores(String usuario) {
            Cliente cliente = DataBaseLoader.LoadCliente(usuario);
            return new ProductorController().GetByDistrito(cliente.direccion[2]);
        }

        // GET para obtener un usuario buscandolo por su nombre
        //api/Cliente?user=aalopz
        public Cliente Get(String user)
        {
            return DataBaseLoader.LoadCliente(user);
        }

        //Get para hacer el inicio de sesion, retorna false si no coincide 
        //api/Cliente?user=aalopz&password=clave
        public Boolean Get(String user, String password)
        {
            Cliente cliente = DataBaseLoader.CheckLogIn(user, password);
            if (cliente == null) {
                return false;
            }
            return true;
        }

        //Post de un nuevo cliente
        // https://localhost:44303/api/Cliente?cedula=12345&info=nombre-appellido-apellido2-provincia-canton-distrito-fecha-8888-aalopz-clave
        public Boolean Post(int cedula, string info) 
        {
            String[] valores = info.Split('-');
            if (DataBaseLoader.GetAllUsers().Contains(valores[8]))
            {
                return false;
            }
            List<String> listaNombre = new List<string>
            {
                valores[0],
                valores[1],
                valores[2]
            };
            List<String> listaDireccion = new List<string>
            {
                valores[3],
                valores[4],
                valores[5]
            };

            Cliente cliente = new Cliente(cedula,listaNombre,listaDireccion,valores[6],int.Parse(valores[7]),valores[8],valores[9]);
            DataBaseWriter.AddUsuario(cliente);
            DataBaseWriter.AddUserNameToList(cliente.GetLogIn());
            return true;
        }

        //PUT para modificar un cliente
        public Boolean PUT(int cedula, string info) {
            String[] valores = info.Split('-');
            if (!DataBaseLoader.GetAllUsers().Contains(valores[8]))
            {
                return false;
            }
            List<String> listaNombre = new List<string>
            {
                valores[0],
                valores[1],
                valores[2]
            };
            List<String> listaDireccion = new List<string>
            {
                valores[3],
                valores[4],
                valores[5]
            };
            Cliente cliente = new Cliente(cedula, listaNombre, listaDireccion, valores[6], int.Parse(valores[7]), valores[8], valores[9]);
            DataBaseWriter.AddUsuario(cliente);
            return true;
        }

        //DELETE de un usuario
        public Boolean DELETE(String user)
        {
            return DataBaseWriter.DeleteCliente(user);
        }
    }
}