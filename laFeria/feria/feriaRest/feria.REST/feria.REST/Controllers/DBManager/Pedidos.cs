using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace feria.REST.Controllers.DBManager
{
    /*
     * Clase para manejar los pedidos de un productor
     */
    public class Pedidos
    {
        public List<Pedido> pedidos;//Lista con cada pedido individual
        int cedula;//Cedula del productor

        /*
         * Constructor que inicia la lista y asigna la cedula
         */
        public Pedidos(int cedula) {
            pedidos = new List<Pedido>();
            this.cedula = cedula;
        }
    }

    /*
     * Clase para cada pedido individual hecho por un cliente
     */
    public class Pedido {
        public int idPedido;//identidicador del predido
        public String usuario;//Usuario que hizo el pedido
        public List<String> direccion;//Direccion de entrega
        public List<Articulo> productos;//Lista de articulos del mismo productor que compro el usuario
        public String factura;//Factura para prueba de pago

        /*
         * Constructor de la clase, inicializa todas las variables
         */
        public Pedido(int id, String usuario, List<String> direccion, String factura) {
            this.idPedido = id;
            this.usuario = usuario;
            this.direccion = direccion;
            this.factura = factura;
            productos = new List<Articulo>();
        }
    }
}