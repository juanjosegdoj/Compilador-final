using compilador.Transversal;
using Compilador.Transversal;
using CompiladorClase.AnalisisLexico;
using CompiladorClase.AnalisisSintactico;
using CompiladorClase.Transversal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Transversal;

namespace Compilador
{
    public partial class FormCompilador : Form
    {
        private Archivo archivo;
        public FormCompilador()
        {
            InitializeComponent();
            archivo = Archivo.ObtenerInstancia();
        }

        private void BtnCargarArchivo_Click(object sender, EventArgs e)
        {
            if (archivo.obtenerLineas().Count != 0)
            {
                if (MessageBox.Show("Se cargara un nuevo archivo y el anterior se eliminara\n¿Deseas cargar el nuevo archivo?",
                    "Cargar Archivo", MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
                {
                    archivo.LimpiarLineas();
                    archivo.CargarArchivo();
                    textBoxEditor.Clear();
                    LlenarTextBoxEditor();
                }
            }
            else
            {
                archivo.CargarArchivo();
                textBoxEditor.Clear();
            }
            LlenarTextBoxEditor();
        }

        private void BtnCompilar_Click(object sender, EventArgs e)
        {
            TablaSimbolos.ObtenerTablaSimbolos().Limpiar();
            TablaDummys.ObtenerTablaDummys().Limpiar();
            TablaLiterales.ObtenerTablaLiterales().Limpiar();
            ManejadorErrores.ObtenerManejadorErrores().Limpiar();

            textBoxReader.Clear();
            LlenarTextBoxReader();

            AnalizadorSintactico anaSin = new AnalizadorSintactico();
            anaSin.Analizar();

            tablaSimbolos.DataSource = TablaSimbolos.ObtenerTablaSimbolos().ObtenerSimbolos();
            tablaLiterales.DataSource = TablaLiterales.ObtenerTablaLiterales().ObtenerLiterales();
            tablaDummys.DataSource = TablaDummys.ObtenerTablaDummys().ObtenerDummys();
            tablaErrores.DataSource = ManejadorErrores.ObtenerManejadorErrores().ObtenerErrores();
        }

        private void BtnResetear_Click(object sender, EventArgs e)
        {

            archivo.LimpiarLineas();
            textBoxReader.Clear();

            TablaSimbolos.ObtenerTablaSimbolos().Limpiar();
            TablaDummys.ObtenerTablaDummys().Limpiar();
            TablaLiterales.ObtenerTablaLiterales().Limpiar();
            ManejadorErrores.ObtenerManejadorErrores().Limpiar();

            tablaSimbolos.DataSource = TablaSimbolos.ObtenerTablaSimbolos().ObtenerSimbolos();
            tablaLiterales.DataSource = TablaLiterales.ObtenerTablaLiterales().ObtenerLiterales();
            tablaDummys.DataSource = TablaDummys.ObtenerTablaDummys().ObtenerDummys();
            tablaErrores.DataSource = ManejadorErrores.ObtenerManejadorErrores().ObtenerErrores();

        }   
        private void LlenarTextBoxReader()
        {
            archivo.BuildArchivo(textBoxEditor.Lines);
            textBoxReader.Clear();

            if (archivo.obtenerLineas().Count != 0)
            {
                foreach (Linea linea in archivo.obtenerLineas())
                {
                    textBoxReader.AppendText(linea.Numero + "- > "+ linea.Contenido + Environment.NewLine);
                }
            }
        }
        private void LlenarTextBoxEditor()
        {
            textBoxEditor.Clear();
            if (archivo.obtenerLineas().Count != 0)
            {
                foreach (Linea linea in archivo.obtenerLineas())
                {
                    textBoxEditor.AppendText(linea.Contenido + Environment.NewLine);
                }
            }
        }

        private void FormCompilador_Load(object sender, EventArgs e)
        {

        }

        private void TextBoxEditor_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBoxReader_TextChanged(object sender, EventArgs e)
        {

        }

        private void tablaSimbolos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tablaLiterales_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tablaDummys_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void tablaErrores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tablaPalabrasReservadas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
