using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompiladorClase.Transversal;

namespace compilador.Transversal
{
    class TablaLiterales
    {
        private static TablaLiterales instancia = new TablaLiterales();

        private Dictionary<String, List<ComponenteLexico>> tabla = new Dictionary<string, List<ComponenteLexico>>();

        private TablaLiterales()
        {
        }

        public static TablaLiterales ObtenerTablaLiterales()
        {
            return instancia;
        }

        public List<ComponenteLexico> ObtenerListaLiterales(String lexema)
        {
            if (!tabla.ContainsKey(lexema))
            {
                tabla.Add(lexema, new List<ComponenteLexico>());
            }
            return tabla[lexema];
        }

        public void Agregar(ComponenteLexico componente)
        {
            if(componente != null && componente.Tipo.Equals(TipoComponenteLexico.LITERAL))
            {
                Console.WriteLine("Agregando componente a tabla de Literales " + componente.Lexema);
                ObtenerListaLiterales(componente.Lexema).Add(componente);
            }
        }

        public List<ComponenteLexico> ObtenerLiterales()
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
