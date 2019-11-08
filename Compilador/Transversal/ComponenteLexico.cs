using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using compilador.Transversal;

namespace CompiladorClase.Transversal
{
    class ComponenteLexico
    {
        public String Lexema { get; }
        public String Categoria { get; }
        public int NumeroLinea { get; }
        public int PosicionInicial { get; }
        public int PosicionFinal { get; }

        public TipoComponenteLexico Tipo { get; set; }
        private ComponenteLexico(String lexema, String categoria, int numeroLinea,int posicionInicial, int posicionFinal, TipoComponenteLexico tipo)
        {
            Lexema = lexema;
            Categoria = categoria;
            NumeroLinea = numeroLinea;
            PosicionInicial = posicionInicial;
            PosicionFinal = posicionFinal;
            Tipo = tipo;
        }
        public static ComponenteLexico Crear(String lexema, String categoria, int numeroLinea, int posicionInicial, int posicionFinal, TipoComponenteLexico tipo)
        {
            return new ComponenteLexico(lexema, categoria, numeroLinea, posicionInicial, posicionFinal, tipo);
        }

        public static ComponenteLexico CrearDummy(String lexema, String categoria, int numeroLinea, int posicionInicial, int posicionFinal)
        {
            return new ComponenteLexico(lexema, categoria, numeroLinea, posicionInicial, posicionFinal, TipoComponenteLexico.DUMMY);
        }

        public static ComponenteLexico CrearLiteral(String lexema, String categoria, int numeroLinea, int posicionInicial, int posicionFinal)
        {
            Console.WriteLine("Creando componete ENTERO " + lexema);
            return new ComponenteLexico(lexema, categoria, numeroLinea, posicionInicial, posicionFinal, TipoComponenteLexico.LITERAL);
        }

        public static ComponenteLexico CrearComponenteLexico(String lexema, String categoria, int numeroLinea, int posicionInicial, int posicionFinal)
        {
            return new ComponenteLexico(lexema, categoria, numeroLinea, posicionInicial, posicionFinal, TipoComponenteLexico.COMPONENTE_LEXICO);
        }

        public static ComponenteLexico CrearPalabraReservada(String lexema, String categoria, int numeroLinea, int posicionInicial, int posicionFinal)
        {
            return new ComponenteLexico(lexema, categoria, numeroLinea, posicionInicial, posicionFinal, TipoComponenteLexico.PALABRA_RESERVADA);
        }


    }
}
