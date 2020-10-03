using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace feria.REST.Controllers.DBManager
{
    public class Producto
    {
        public String nombre;
        public String categoria;
        public Image image;
        public int precio;
        public String modoVenta;
        public int disponible = 0;
        
        public Producto(String nombre, String categoria, int precio, String modoVenta) {
            this.nombre = nombre;
            this.categoria = categoria;
            //this.image = imagen;
            this.precio = precio;
            this.modoVenta = modoVenta;
        }

        public void AumentarStock(int cantidad) {
            disponible += cantidad;
        }

        public void ActualizarImagen(Image imagen) {
            image = imagen;
        }

        public int VerificarDisponible() {
            return disponible;
        }


    }
}