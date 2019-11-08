using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Transversal
{

    class Error
    {

        public TipoError Tipo { get; }
        public String Lexema { get; }
        public String Causa { get; }
        public String Falla { get; }
        public String Solucion { get; }
        public int NumeroLinea { get; }

        public int PosicionInicial { get; }
        public int PosicionFinal { get; }

        public static Error Crear(TipoError tipo, string lexema, string causa, string falla, string solucion, int numeroLinea, int posicionInicial, int posicionFinal)
        {
            return new Error( tipo, lexema, causa,  falla,  solucion,  numeroLinea,  posicionInicial, posicionFinal);
        }

        private Error(TipoError tipo, string lexema, string causa, string falla, string solucion, int numeroLinea, int posicionInicial, int posicionFinal)
        {
            Tipo = tipo;
            Lexema = lexema;
            Causa = causa;
            Falla = falla;
            Solucion = solucion;
            NumeroLinea = numeroLinea;
            PosicionInicial = posicionInicial;
            PosicionFinal = posicionFinal;
        }

        public static Error CrearErrorLexico(string lexema, string causa, string falla, string solucion, int numeroLinea, int posicionInicial, int posicionFinal)
        {
            return Error.Crear(TipoError.LEXICO, lexema, causa, falla, solucion, numeroLinea, posicionInicial, posicionFinal);
        }

        public static Error CrearErrorSemantico(string lexema, string causa, string falla, string solucion, int numeroLinea, int posicionInicial, int posicionFinal)
        {
            return Error.Crear(TipoError.SEMANTICO, lexema, causa, falla, solucion, numeroLinea, posicionInicial, posicionFinal);
        }

        public static Error CrearErrorSintactico(string lexema, string causa, string falla, string solucion, int numeroLinea, int posicionInicial, int posicionFinal)
        {
            return Error.Crear(TipoError.SINTACTICO, lexema, causa, falla, solucion, numeroLinea, posicionInicial, posicionFinal);
        }

    }
}
