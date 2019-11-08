using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompiladorClase.Transversal;

namespace compilador.Transversal
{
    class TablaSimbolos
    {
        private static TablaSimbolos instancia = new TablaSimbolos();

        private Dictionary<String, List<ComponenteLexico>> tabla = new Dictionary<string, List<ComponenteLexico>>();

        private TablaSimbolos()
        {
        }

        public static TablaSimbolos ObtenerTablaSimbolos()
        {
            return instancia;
        }

        public List<ComponenteLexico> ObtenerListaSimbolos(String lexema)
        {
            if (!tabla.ContainsKey(lexema))
            {
                tabla.Add(lexema, new List<ComponenteLexico>());
            }
            return tabla[lexema];
        }

        public void Agregar(ComponenteLexico componente)
        {
            if(componente != null && componente.Tipo.Equals(TipoComponenteLexico.COMPONENTE_LEXICO))
            {
                ObtenerListaSimbolos(componente.Lexema).Add(componente);
            }
        }

        public List<ComponenteLexico> ObtenerSimbolos()
        {
            return tabla
                .Values
                .SelectMany(error => error)
                .ToList();
        }

        public void Limpiar()
        {
            tabla.Clear();
        }
    }



}
