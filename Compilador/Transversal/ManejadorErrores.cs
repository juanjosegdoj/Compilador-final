using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Transversal
{
    class ManejadorErrores
    {
        private static ManejadorErrores instancia = new ManejadorErrores();
        private Dictionary<TipoError, List<Error>> mapaErrores = new Dictionary<TipoError, List<Error>>();

        private ManejadorErrores()
        {
            mapaErrores.Add(TipoError.LEXICO, new List<Error>());
            mapaErrores.Add(TipoError.SINTACTICO, new List<Error>());
            mapaErrores.Add(TipoError.SEMANTICO, new List<Error>());
        }

        public static ManejadorErrores ObtenerManejadorErrores()
        {
            return instancia;
        }

        public void Limpiar(TipoError error)
        {
            mapaErrores[error].Clear();
        }

        public void Limpiar()
        {
            mapaErrores[TipoError.LEXICO].Clear();
            mapaErrores[TipoError.SINTACTICO].Clear();
            mapaErrores[TipoError.SEMANTICO].Clear();
        }

        public List<Error> ObtenerErrores(TipoError error)
        {
            return mapaErrores[error];
        }

        public List<Error> ObtenerErrores()
        {
            return mapaErrores
                .Values
                .SelectMany(error => error)
                .ToList();
        }
        public void Agregar(Error error)
        {
            if (error != null)
            {
                mapaErrores[error.Tipo].Add(error);
            }
        }

        public Boolean HayErrores(TipoError tipo)
        {
            return mapaErrores[tipo].Count > 0;
        }

        public Boolean HayErrores()
        {
            return HayErrores(TipoError.LEXICO) || HayErrores(TipoError.SINTACTICO) || HayErrores(TipoError.SEMANTICO);
        }
    }
}
