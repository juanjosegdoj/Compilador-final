using System;

namespace Compilador.Transversal
{
    public class Linea
    {
        public int Numero { get; set; }
        public String Contenido { get; set; }

        private Linea(int numero, string contenido)
        {
            this.Numero = numero;
            this.Contenido = contenido;
        }
        public static Linea Crear(int numero, String contenido)
        {
            return new Linea(numero, contenido);
        }
    }
}