using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compilador.Transversal
{
    public class Archivo
    {

        public const String FINAL_ARCHIVO = "@EOF@";

        List<Linea> lineas = new List<Linea>();
        private static Archivo instancia=new Archivo();
        private Archivo(){ }
        public static Archivo ObtenerInstancia()
        {
                return instancia; 
        }
        public void AgregarLinea(String contenido)
        {
            if (contenido == null)
            {
                contenido = "";
            }
            lineas.Add(Linea.Crear(ObtenerNumeroProximaLinea(), contenido));
        }

        public void LimpiarLineas()
        {
            if (lineas.Count!=0)
            {
                lineas.Clear();
            }  
        }

        public string ObtenerContenidoLinea(int numeroLinea)
        {
            return (ExisteLinea(numeroLinea) ? lineas.ElementAt(numeroLinea - 1).Contenido : Linea.Crear(ObtenerNumeroProximaLinea(), FINAL_ARCHIVO).Contenido);
        }
        private int ObtenerNumeroProximaLinea()
        {
            int lineaProxima;
            if (lineas.Count == 0)
            {
                lineaProxima = 1;
            }
            else
            {
                lineaProxima = lineas.Count + 1;
            }
            return lineaProxima;
        }
        public Linea ObtenerLinea(int numeroLinea)
        {
            return (ExisteLinea(numeroLinea) ? lineas.ElementAt(numeroLinea - 1) : Linea.Crear(ObtenerNumeroProximaLinea(), "@EOF@"));
        }
        private Boolean ExisteLinea(int numero)
        {
            return (numero - 1 >= 0 && numero - 1 < lineas.Count);
        }
        public List<Linea> obtenerLineas()
        {
            return lineas;
        }
        public void CargarArchivo()
        {
            OpenFileDialog openFileDialog;
            string filePath= string.Empty;
            String contenidoLinea;
            using (openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                int contador = 1;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        while ((contenidoLinea = reader.ReadLine()) != null)
                        {
                            lineas.Add(Linea.Crear(contador , contenidoLinea));
                            contador++;
                        }
                    }
                }
            }
        }

        public void BuildArchivo(String [] texto)
        {
            instancia.LimpiarLineas();
            foreach (String contenidoLinea in texto)
            {
                instancia.AgregarLinea(contenidoLinea);
            }
        }
    }
}
