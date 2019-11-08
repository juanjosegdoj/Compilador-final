using compilador.Transversal;
using Compilador.Transversal;
using CompiladorClase.Transversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Transversal;

namespace CompiladorClase.AnalisisLexico
{
    class AnalizadorLexico
    {
        private int numeroLineactual = 0;
        private Linea lineaActual;
        private int puntero;
        private String caracterActual;

        public AnalizadorLexico()
        {
            CargarNuevaLinea();
        }
        private void CargarNuevaLinea()
        {
            numeroLineactual += 1;

            lineaActual = Archivo.ObtenerInstancia().ObtenerLinea(numeroLineactual);
            puntero = 0;
            if ("@EOFQ@".Equals(lineaActual.Contenido))
            {
                numeroLineactual = lineaActual.Numero;
            }
        }
        private void LeerSiguienteCaracter()
        {
            puntero += 1;
            if ("@EOF@".Equals(lineaActual.Contenido)){
                caracterActual = lineaActual.Contenido;
            }
            else if (puntero - 1 >= lineaActual.Contenido.Length)
            {
                caracterActual = "@FL@";
            }
            else
            {
                caracterActual = lineaActual.Contenido.Substring(puntero - 1, 1);
            }
        }
        private void DevolverPuntero()
        {
            puntero -= 1;
        }
        public ComponenteLexico DevolverComponente()
        {
            ComponenteLexico componente=null;
            int estadoActual =  0;
            int nroBS = 0;
            bool continuarAnalisis = true;
            string lexema = "";

            while (continuarAnalisis)
            {
                if (estadoActual == 0)
                {
                    LeerSiguienteCaracter();

                    while (" ".Equals(caracterActual))
                    {
                        LeerSiguienteCaracter();
                    }
                    if (Char.IsLetter(caracterActual.ToCharArray()[0]))
                    {
                        estadoActual = 4;
                        lexema += caracterActual;
                    }
                    else if (Char.IsDigit(caracterActual.ToCharArray()[0]))
                    {
                        estadoActual = 1;
                        lexema += caracterActual;
                    }
                    else if ("+".Equals(caracterActual))
                    {
                        estadoActual = 5;
                        lexema += caracterActual;
                    }
                    else if ("-".Equals(caracterActual))
                    {
                        estadoActual = 8;
                        lexema += caracterActual;
                    }
                    else if ("*".Equals(caracterActual))
                    {
                        estadoActual = 7;
                        lexema += caracterActual;
                    }
                    else if ("/".Equals(caracterActual))
                    {
                        estadoActual = 6;
                        lexema += caracterActual;
                    }
                    else if ("%".Equals(caracterActual))
                    {
                        estadoActual = 9;
                        lexema += caracterActual;
                    }
                    else if ("(".Equals(caracterActual))
                    {
                        estadoActual = 10;
                        lexema += caracterActual;
                    }
                    else if (")".Equals(caracterActual))
                    {
                        estadoActual = 11;
                        lexema += caracterActual;
                    }
                    else if ("#".Equals(caracterActual))
                    {
                        estadoActual = 22;
                        lexema += caracterActual;
                    }
                    else if ("@EOF@".Equals(caracterActual))
                    {
                        estadoActual = 12;
                        lexema += caracterActual;
                    }
                    else if ("=".Equals(caracterActual))
                    {
                        estadoActual = 19;
                        lexema += caracterActual;
                    }
                    else if ("<".Equals(caracterActual))
                    {
                        estadoActual = 20;
                        lexema += caracterActual;
                    }
                    else if (">".Equals(caracterActual))
                    {
                        estadoActual = 21;
                        lexema += caracterActual;
                    }
                    else if ("'".Equals(caracterActual))
                    {
                        estadoActual = 23;
                        lexema += caracterActual;
                        Console.WriteLine("Entro ad estaod comillads");
                    }
                    else if ("@FL@".Equals(caracterActual))
                    {
                        estadoActual = 13;
                    }
                    else
                    {
                        estadoActual = 18;
                    }
                }
                else if (estadoActual == 1)
                {
                    LeerSiguienteCaracter();
                    if (Char.IsDigit(caracterActual.ToCharArray()[0]))
                    {
                        estadoActual = 1;
                        lexema += caracterActual;
                    }
                    else if (".".Equals(caracterActual))
                    {
                        estadoActual = 2;
                        lexema += caracterActual;
                    }
                    else
                    {
                        estadoActual = 14;
                    }
                }
                else if (estadoActual == 2)
                {
                    LeerSiguienteCaracter();
                    if (Char.IsDigit(caracterActual.ToCharArray()[0]))
                    {
                        estadoActual = 3;
                        lexema += caracterActual;
                    }
                    else
                    {
                        estadoActual = 17;
                        lexema += caracterActual;
                    }
                }
                else if (estadoActual == 3)
                {
                    LeerSiguienteCaracter();
                    if (Char.IsDigit(caracterActual.ToCharArray()[0]))
                    {
                        estadoActual = 3;
                    }
                    else
                    {
                        estadoActual = 15;
                    }
                }
                else if (estadoActual == 4)
                {
                    LeerSiguienteCaracter();

                    if (Char.IsLetter(caracterActual.ToCharArray()[0]))
                    {
                        estadoActual = 4;
                        lexema += caracterActual;
                    }
                    else
                    {
                        estadoActual = 16;
                    }
                }
                else if (estadoActual == 5)
                {
                    continuarAnalisis = false;
                    componente = ComponenteLexico.CrearComponenteLexico(lexema, "SUMA", lineaActual.Numero, puntero, puntero);
                }
                else if (estadoActual == 6)
                {
                    continuarAnalisis = false;
                    componente = ComponenteLexico.CrearComponenteLexico(lexema, "DIVISION", lineaActual.Numero, puntero, puntero);
                }
                else if (estadoActual == 7)
                {
                    continuarAnalisis = false;
                    componente = ComponenteLexico.CrearComponenteLexico(lexema, "MULTIPLICACION", lineaActual.Numero, puntero, puntero);
                }
                else if (estadoActual == 8)
                {
                    LeerSiguienteCaracter();
                    if ("-".Equals(caracterActual))
                    {
                        estadoActual = 36;
                        lexema += caracterActual;
                    }
                    else
                    {
                        estadoActual = 33;
                    }
                }
                else if (estadoActual == 9)
                {
                    continuarAnalisis = false;
                    componente = ComponenteLexico.CrearComponenteLexico(lexema, "MODULO", lineaActual.Numero, puntero, puntero);
                }
                else if (estadoActual == 10)
                {
                    continuarAnalisis = false;
                    componente = ComponenteLexico.CrearComponenteLexico(lexema, "PARENTESIS ABRE", lineaActual.Numero, puntero, puntero);

                }
                else if (estadoActual == 11)
                {
                    continuarAnalisis = false;
                    componente = ComponenteLexico.CrearComponenteLexico(lexema, "PARENTESIS CIERRA", lineaActual.Numero, puntero, puntero);

                }
                else if (estadoActual == 12)
                {
                    continuarAnalisis = false;
                    componente = ComponenteLexico.CrearComponenteLexico(lexema, "@EOF@", lineaActual.Numero, puntero, puntero);

                }
                else if (estadoActual == 13)
                {
                    CargarNuevaLinea();
                    estadoActual = 0;

                }
                else if (estadoActual == 14)
                {
                    continuarAnalisis = false;
                    DevolverPuntero();
                    componente = ComponenteLexico.CrearLiteral(lexema, "ENTERO", lineaActual.Numero, puntero - lexema.Length + 1, puntero);
                }
                else if (estadoActual == 15)
                {
                    continuarAnalisis = false;
                    DevolverPuntero();
                    componente = ComponenteLexico.CrearLiteral(lexema, "DECIMAL", lineaActual.Numero, puntero - lexema.Length + 1, puntero);

                }
                else if (estadoActual == 16)
                {
                    continuarAnalisis = false;
                    DevolverPuntero();
                    componente = ComponenteLexico.CrearComponenteLexico(lexema, "IDENTIFICADOR", lineaActual.Numero, puntero - lexema.Length + 1, puntero);
                }
                else if (estadoActual == 17)
                {
                    DevolverPuntero();
                    continuarAnalisis = false;

                    ManejadorErrores.ObtenerManejadorErrores().Agregar(Error.CrearErrorLexico(lexema,
                        "Se esperaba un número y se recibió: " + caracterActual,
                        "Numero decimal no valido",
                        "Asegurese de que el número decimal sea válido",
                        lineaActual.Numero,
                        puntero - lexema.Length,
                        puntero
                        ));


                    componente = ComponenteLexico.CrearDummy(lexema, "DECIMAL", lineaActual.Numero, puntero - lexema.Length +1, puntero);
                }
                else if (estadoActual == 18)
                {
                    ManejadorErrores.ObtenerManejadorErrores().Agregar(Error.CrearErrorLexico(caracterActual,
                        "Caracter no aceptado por el lenguaje: " + caracterActual,
                        "No es posible continuar con el analisis por caracter no valido",
                        "Asegurese de utilizar caracteres aceptados por el lenguaje",
                        lineaActual.Numero,
                        puntero - lexema.Length,
                        puntero
                        ));

                    throw new Exception("Se ha presentado un error tipo Stopper que no permite continuar con el análisis, verifique la consola de errores");
                }
                else if (estadoActual == 19)
                {
                    continuarAnalisis = false;
                    componente = ComponenteLexico.CrearComponenteLexico(lexema, "ASIGNACION", lineaActual.Numero, puntero, puntero);
                }
                else if (estadoActual == 20)
                {
                    continuarAnalisis = false;
                    componente = ComponenteLexico.CrearComponenteLexico(lexema, "LECTURA", lineaActual.Numero, puntero - lexema.Length, puntero);
                }
                else if (estadoActual == 21)
                {
                    continuarAnalisis = false;
                    componente = ComponenteLexico.CrearComponenteLexico(lexema, "ESCRITURA", lineaActual.Numero, puntero - lexema.Length, puntero);
                }
                else if (estadoActual == 22)
                {
                    continuarAnalisis = false;
                    componente = ComponenteLexico.CrearComponenteLexico(lexema, "CONCATENACION", lineaActual.Numero, puntero, puntero);
                }
                else if (estadoActual == 23)
                {
                    LeerSiguienteCaracter();
                    if ("\\".Equals(caracterActual))
                    {
                        estadoActual = 24;
                        Console.WriteLine("Leido un BS");
                        nroBS += 1;
                    }
                    else if ("'".Equals(caracterActual))
                    {
                        estadoActual = 25;
                        lexema += caracterActual;
                    }
                    else if ("@FL@".Equals(caracterActual))
                    {
                        estadoActual = 26;
                        //lexema += caracterActual;
                    }
                    else 
                    {
                        estadoActual = 23;
                        lexema += caracterActual;
                    }
                }
                else if (estadoActual == 24)
                {
                    LeerSiguienteCaracter();
                    if ("@FL@".Equals(caracterActual))
                    {
                        estadoActual = 26;
                    }else
                    {
                        estadoActual = 23;
                        lexema += caracterActual;
                    }
                }
                else if (estadoActual == 25)
                {
                    continuarAnalisis = false;
                    //DevolverPuntero();
                    componente = ComponenteLexico.CrearLiteral(lexema, "CADENA", lineaActual.Numero, puntero - lexema.Length, puntero);
                }
                else if (estadoActual == 26)
                {
                    continuarAnalisis = false;
                    ManejadorErrores.ObtenerManejadorErrores().Agregar(Error.CrearErrorLexico(lexema,
                    "Se esperaba una comilla ['] y se recibió: " + caracterActual,
                    "Cadena no valida",
                    "Asegurese de que la estructura de cerrar la cadena con un ['].",
                    lineaActual.Numero,
                    puntero - lexema.Length - nroBS,
                    puntero
                    ));

                    componente = ComponenteLexico.CrearDummy(lexema, "CADENA_INVALIDA", lineaActual.Numero, puntero - lexema.Length - nroBS, puntero);
                }
              
                else if (estadoActual == 33)
                {
                    continuarAnalisis = false;
                    DevolverPuntero();
                    componente = ComponenteLexico.CrearComponenteLexico(lexema, "RESTA", lineaActual.Numero, puntero - lexema.Length, puntero);
                }

                
                else if (estadoActual == 36)
                {
                    LeerSiguienteCaracter();
                    if ("@FL@".Equals(caracterActual))
                    {
                        estadoActual = 13;
                        lexema = "";
                    }
                    else
                    {
                        estadoActual = 36;
                        lexema += caracterActual;
                    }
                }
            }

            if(caracterActual != "@EOF@")
            {
                TablaMaestra.ObtenerTablaMaestra().Sincronizar(componente);
            }
            return componente;
        }
    }
}
