using System;
using System.Windows.Forms;
using CompiladorClase.AnalisisLexico;
using CompiladorClase.Transversal;
using WindowsFormsApp1.Transversal;

namespace CompiladorClase.AnalisisSintactico
{
    public class AnalizadorSintactico
    {

        private ComponenteLexico componente;
        private AnalizadorLexico anaLex = new AnalizadorLexico();


        public void Analizar()
        {
            try
            {
                Avanzar();
                Programa();

                if (ManejadorErrores.ObtenerManejadorErrores().HayErrores())
                {
                    MessageBox.Show("El programa se encuentra mal escrito. Por favor verifique la consola de errores...");
                }
                else if ("@EOF@".Equals(componente.Categoria))
                {
                    MessageBox.Show("El programa se encuentra bien escrito...Farid el mejor!!!!!");

                }
                else
                {
                    MessageBox.Show("Faltaron componentes por evaluar. Por favor revisar, debido a que el análisis terminó en el compoente: " + componente.Lexema);
                }

            }
            catch (Exception excepcion)
            {
                MessageBox.Show(excepcion.Message);
            }
        }

        //<programa> := <inicioPrograma><cuerpoPrograma><finPrograma>
        private void Programa()
        {
            InicioPrograma();
            CuerpoPrograma();
            FinPrograma();
        }

        //<inicioProgram> := <lectura><restoInicio> | <escritura><restoInicio>
        private void InicioPrograma()
        {
            if ("LECTURA".Equals(componente.Categoria))
            {
                Avanzar();
                if ("IDENTIFICADOR".Equals(componente.Categoria))
                {
                    Avanzar();
                    RestoInicio();
                }
                else
                {
                    ManejadorErrores.ObtenerManejadorErrores().Agregar(Error.CrearErrorSintactico(componente.Lexema,
                          "Se esperaba un IDENTIFICADOR y se recibió: " + componente.Categoria,
                          "Componente no esperado en la ubicación actual de la expresión",
                          "Asegúrese que en la posición deseada aparezca un IDENTIFICADOR",
                          componente.NumeroLinea,
                          componente.PosicionInicial,
                          componente.PosicionFinal
                          ));

                    throw new Exception("Se ha presentado un error tipo Stopper que no permite continuar con el análisis sintáctico, verifique la consola de errores");
                }

            }
            else if("ESCRITURA".Equals(componente.Categoria))
            {
                Avanzar();
                CuerpoEscritura();
                RestoFinPrograma();
                RestoInicio();
            }

        }

        // <restoInicio> := <inicioPrograma> | epsilon
        private void RestoInicio()
        {
            InicioPrograma();
        }

        //<finPrograma> := <escritura><restoFin>
        private void FinPrograma()
        {
            if ("ESCRITURA".Equals(componente.Categoria))
            {
                Avanzar();
                CuerpoEscritura();
                RestoFinPrograma();
                FinPrograma();
            }
            else if(!"@EOF@".Equals(componente.Categoria))
            {
                ManejadorErrores.ObtenerManejadorErrores().Agregar(Error.CrearErrorSintactico(componente.Lexema,
                            "Se esperaba una ESCRITURA y se recibió: " + componente.Categoria,
                            "Componente no esperado en la ubicación actual de la expresión",
                            "Asegúrese de que después sólo se realicen ESCRITURAS",
                            componente.NumeroLinea,
                            componente.PosicionInicial,
                            componente.PosicionFinal
                            ));

                throw new Exception("Se ha presentado un error tipo Stopper que no permite continuar con el análisis sintáctico, verifique la consola de errores");
            }

        }

        private void RestoFinPrograma()
        {
            if ("CONCATENACION".Equals(componente.Categoria))
            {
                Avanzar();
                CuerpoEscritura();
                RestoFinPrograma();
            }
        }


        //<restoFin> := <finPrograma> | epsilon
        private void RestoFin()
        {
            RestoFinPrograma();
            
        }

        //<escritura> := > <cuerpoEscritura>


        //<cuerpoEscritura> := CADENA <restoEscritura> | ENTERO <restoEscritura> | DECIMAL <restoEscritura> | IDENTIFICADOR <restoEscritura>
        private void CuerpoEscritura()
        {
            if ("CADENA".Equals(componente.Categoria) || "ENTERO".Equals(componente.Categoria) || "DECIMAL".Equals(componente.Categoria) || "IDENTIFICADOR".Equals(componente.Categoria))
            {
                Avanzar();
                RestoEscritura();
            }
            else
            {
                ManejadorErrores.ObtenerManejadorErrores().Agregar(Error.CrearErrorSintactico(componente.Lexema,
                         "Se esperaba una CADENA, ENTERO, DECIMAL, IDENTIFICADOR y se recibió: " + componente.Categoria,
                         "Componente no esperado en la ubicación actual de la expresión",
                         "Asegúrese que en la posición deseada aparezca un IDENTIFICADOR",
                         componente.NumeroLinea,
                         componente.PosicionInicial,
                         componente.PosicionFinal
                         ));

                throw new Exception("Se ha presentado un error tipo Stopper que no permite continuar con el análisis sintáctico, verifique la consola de errores");

            }
        }

        //<restoEscritura> := # <cuerpoEscritura> | epsilon
        private void RestoEscritura()
        {
            if ("CONCATENADOR".Equals(componente.Categoria))
            {
                CuerpoEscritura();
            }
        }


        //<cuerpoPrograma> := IDENTIFICADOR = <operacion>
        private void CuerpoPrograma()
        {
            if ("IDENTIFICADOR".Equals(componente.Categoria))
            {
                Avanzar();
                if ("ASIGNACION".Equals(componente.Categoria))
                {
                    Avanzar();
                    Operacion();
                }
                else
                {
                    ManejadorErrores.ObtenerManejadorErrores().Agregar(Error.CrearErrorSintactico(componente.Lexema,
                       "Se esperaba un = y se recibió: " + componente.Lexema,
                       "Componente no esperado en la ubicación actual de la expresión",
                       "Asegúrese que en la posición deseada aparezca un paréntesis que cierra",
                       componente.NumeroLinea,
                       componente.PosicionInicial,
                       componente.PosicionFinal
                       ));
                }
            }
            else
            {
                ManejadorErrores.ObtenerManejadorErrores().Agregar(Error.CrearErrorSintactico(componente.Lexema,
                   "Se esperaba un IDENTIFICADOR y se recibió: " + componente.Lexema,
                   "Componente no esperado en la ubicación actual de la expresión",
                   "Asegúrese que en la posición deseada aparezca un paréntesis que cierra",
                   componente.NumeroLinea,
                   componente.PosicionInicial,
                   componente.PosicionFinal
                   ));
            }
        }


        //<operacion> :=  <multiplicacionDivision><RestoMultiplicacionDivision>
        private void Operacion()
        {
            MultiplicacionDivision();
            RestoMultiplicacionDivision();
        }

        //<restoMultiplicacionDivision> := +<operacion> | -<operacion> | epsilon
        private void RestoMultiplicacionDivision()
        {
            if ("+".Equals(componente.Lexema))
            {
                Avanzar();
                Operacion();
            }
            else if ("-".Equals(componente.Lexema))
            {
                Avanzar();
                Operacion();
            }
        }

        //<multiplicacionDivision> := <openrando><restoOperando>
        private void MultiplicacionDivision()
        {
            Operando();
            RestoOperando();
        }

        //<operando> := IDENTIFICADOR | ENTERO | DECIMAL | (<operacion>)
        private void Operando()
        {
            if ("IDENTIFICADOR".Equals(componente.Categoria))
            {
                Avanzar();
            }
            else if ("ENTERO".Equals(componente.Categoria))
            {
                Avanzar();
            }
            else if ("DECIMAL".Equals(componente.Categoria))
            {
                Avanzar();
            }
            else if ("(".Equals(componente.Lexema))
            {
                Avanzar();
                Operacion();

                if (")".Equals(componente.Lexema))
                {
                    Avanzar();
                }
                else
                {
                    ManejadorErrores.ObtenerManejadorErrores().Agregar(Error.CrearErrorSintactico(componente.Lexema,
                       "Se esperaba un paréntesis que cierra y se recibió: " + componente.Lexema,
                       "Componente no esperado en la ubicación actual de la expresión",
                       "Asegúrese que en la posición deseada aparezca un paréntesis que cierra",
                       componente.NumeroLinea,
                       componente.PosicionInicial,
                       componente.PosicionFinal
                       ));
                }
            }
            else
            {
                ManejadorErrores.ObtenerManejadorErrores().Agregar(Error.CrearErrorSintactico(componente.Lexema,
                      "Se esperaba un IDENTIFICADOR ó ENTERO ó DECIMAL ó ( y se recibió: " + componente.Lexema,
                      "Componente no esperado en la ubicación actual de la expresión",
                      "Asegúrese que en la posición deseada aparezca un IDENTIFICADOR ó ENTERO ó DECIMAL ó (",
                      componente.NumeroLinea,
                      componente.PosicionInicial,
                      componente.PosicionFinal
                      ));

                throw new Exception("Se ha presentado un error tipo Stopper que no permite continuar con el análisis sintáctico, verifique la consola de errores");
            }
        }

        //<restoOperando> := * <multiplicacionDivision> | / <multiplicacionDivision> | % <multiplicacionDivision> | epsilon
        private void RestoOperando()
        {
            if ("*".Equals(componente.Lexema))
            {
                Avanzar();
                MultiplicacionDivision();
            }
            else if ("/".Equals(componente.Lexema))
            {
                Avanzar();
                MultiplicacionDivision();
            }
            else if ("%".Equals(componente.Lexema))
            {
                Avanzar();
                MultiplicacionDivision();
            }
        }

        private void Avanzar()
        {
            componente = anaLex.DevolverComponente();
        }
    }
}
