using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace feria.REST.Controllers.DBManager
{
    /*
     * Clase para manejar un producto
     */
    public class Producto
    {
        public String nombre;// nombre del producto
        public String categoria; //nombre de la categoria a la que pertenece el producto
        public String image; // imagen codificada del producto
        public int precio; // precio del producto
        public String modoVenta; // modo de venta del producto
        public int disponible = 0; // cantidad del producto disponible
        
        /*
         * Constructor que inicia la clase 
         */
        public Producto(String nombre, String categoria, int precio, String modoVenta, String imagen) {
            this.nombre = nombre;
            this.categoria = categoria;
            this.image = imagen;
            this.precio = precio;
            this.modoVenta = modoVenta;
        }

        /*
         * Funcion para aumentar la cantidad disponible por una cantidad
         */
        public void AumentarStock(int cantidad) {
            disponible += cantidad;
        }

        /*
         * Funcion para canbiar la imagen de un producto
         */
        public void ActualizarImagen(String imagen) {
            image = imagen;
        }

    }
}