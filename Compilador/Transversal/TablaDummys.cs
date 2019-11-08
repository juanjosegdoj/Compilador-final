using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompiladorClase.Transversal;

namespace compilador.Transversal
{
    class TablaDummys
    {
        private static TablaDummys instancia = new TablaDummys();

        private Dictionary<String, List<ComponenteLexico>> tabla = new Dictionary<string, List<ComponenteLexico>>();

        private TablaDummys()
        {

        }

        public static TablaDummys ObtenerTablaDummys()
        {
            return instancia;
        }

        public List<ComponenteLexico> ObtenerListaDummys(String lexema)
        {
            if (!tabla.ContainsKey(lexema))
            {
                tabla.Add(lexema, new List<ComponenteLexico>());
            }
            return tabla[lexema];
        }

        public void Agregar(ComponenteLexico componente)
        {
            if (componente != null && componente.Tipo.Equals(TipoComponenteLexico.DUMMY))
            {
                ObtenerListaDummys(componente.Lexema).Add(componente);
            }
        }

        public List<ComponenteLexico> ObtenerDummys()
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
