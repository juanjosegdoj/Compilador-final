using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompiladorClase.Transversal;

namespace compilador.Transversal
{
    class TablaMaestra
    {
        private static TablaMaestra instancia = new TablaMaestra();

        private TablaMaestra()
        {
        }

        public static TablaMaestra ObtenerTablaMaestra()
        {
            return instancia;
        }

        public void Sincronizar(ComponenteLexico componente)
        {
            if (componente != null)
            {
                switch (componente.Tipo)
                {
                    
                   case TipoComponenteLexico.DUMMY:
                        Console.WriteLine("Sincronizando componente DUMMY " + componente.Lexema);
                        TablaDummys.ObtenerTablaDummys().Agregar(componente);
                        break;
                   case TipoComponenteLexico.LITERAL:
                        Console.WriteLine("Sincronizando componente LITERAL "+ componente.Lexema);
                        TablaLiterales.ObtenerTablaLiterales().Agregar(componente);
                        break;
                   case TipoComponenteLexico.COMPONENTE_LEXICO:
                        Console.WriteLine("Sincronizando componente COMPONENTE_LEXICO " + componente.Lexema);
                        TablaSimbolos.ObtenerTablaSimbolos().Agregar(componente);
                        break;

                }
            }
        }
    }
}
