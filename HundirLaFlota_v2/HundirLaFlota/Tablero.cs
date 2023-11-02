using System;
using log4net;
using Microsoft.Win32;

namespace HundirLaFlota
{
    class Tablero
    {
        private const int FILAS = 10;
        private const int COLUMNAS = 10;
        private const int BARCOS = 5;

        private Casilla[,] casillas = new Casilla[FILAS, COLUMNAS];
        private Barco[] barcos = new Barco[BARCOS];

        private static readonly ILog Log = Logs.GetLogger();

        //Constructor de Tablero       
        public Tablero()
        {

            for (int i = 0; i < FILAS; i++)
            {
                for (int j = 0; j < COLUMNAS; j++)
                {
                    casillas[i, j] = new Casilla(i,j);
                }
            }
            
            barcos[0] = new Lancha();
            barcos[1] = new Crucero();
            barcos[2] = new Submarino();
            barcos[3] = new Buque();
            barcos[4] = new Portaaviones();
        }

        private bool ComprobarBarco(int barco, int fila, int columna, char orientacion)
        {
                        
            bool ocupado = false;
            int contadorV = -1;
            int contadorH = -1;

            //Para la orientación horizontal:
            if (orientacion == 'H')
            {

                //Comprobamos que el barco no se salga del tablero
                if (columna + barco+1 > COLUMNAS)
                {
                    Log.Error("El barco se sale del tablero");
                    return false;
                }

                //Comprobamos que no haya barcos en esas posiciones
                for (int i = columna; i <= columna + barco; i++)
                {
                    if (casillas[fila, i].GetEstado() != 'O')
                    {
                        contadorH++;
                    }  
                }

                //Si no existen barcos en esa posición, se coloca
                for (int i = columna; i <= columna + barco; i++)
                {
                    if (contadorH == barco)
                    {
                        casillas[fila, i].SetBarco();
                        barcos[barco].CasillasBarco[i - columna] = casillas[fila, i];
                    }
                    else
                    {
                        ocupado = true;
                    }
                }
            }

            //Para la orientación vertical:
            if (orientacion == 'V')
            {

                //Comprobamos que el barco no se salga del tablero
                if (fila + barco+1 > FILAS)
                {
                    Log.Error("El barco se sale del tablero");
                    return false;
                }

                //Comprobamos que no haya barcos en esas posiciones
                for (int i = fila; i <= fila + barco; i++)
                {
                    if (casillas[i, columna].GetEstado() != 'O')
                    {
                        contadorV++;
                    }
                }

                //Si no existen barcos en esa posición, se coloca
                for (int i = fila; i <= fila + barco; i++)
                {                   
                    if (contadorV == barco)
                    {
                        casillas[i, columna].SetBarco();
                        barcos[barco].CasillasBarco[i - fila] = casillas[i, columna];
                    }
                    else
                    {
                        ocupado = true;
                    }
                }
            }
            if (ocupado)
            {
                Log.Error("El barco no se puede colocar en esa posición porque ocupa el mismo espacio que otro/s barco/s.");
                return false;
            }

            Log.Info("El barco ha sido colocado");
            //Console.WriteLine("El barco ha sido colocado.");
            //Console.WriteLine("");
            return true;
        }

        public void ColocarBarcos()
        {
            int barco;
            int fila;
            int columna;
            char orientacion;
            string[] tiposDeBarcos = { "Lancha", "Crucero", "Submarino", "Buque", "Portaaviones" };
            bool colocado;

            for (int i = 0; i < tiposDeBarcos.Length; i++)
            {
                Console.WriteLine("Colocando barco: " + tiposDeBarcos[i].ToLower());
                barco = i;

                //Validamos que se introduzca un carácter válido en todos los campos a introducir:

                do
                {
                    Console.Write("Introduce la fila donde lo quieres colocar: ");
                } while (!int.TryParse(Console.ReadLine(), out fila) || fila < 0 || fila > 10);

                do
                {
                    Console.Write("Introduce la columna donde lo quieres colocar: ");
                } while (!int.TryParse(Console.ReadLine(), out columna) || columna < 0 || columna > 10);

                do
                {
                    Console.Write("Introduce la orientación que quieres que tenga el barco (H para horizontal, V para vertical): ");
                    orientacion = Convert.ToChar(Console.ReadLine().ToUpper());
                } while (orientacion != 'H' && orientacion != 'V');

                colocado = ComprobarBarco(barco, fila, columna, orientacion) ? true : false;

                if (!colocado)
                {
                    Log.Error("El barco no se ha podido colocar en la ubicación indicada.");
                    //Console.WriteLine("El barco no se ha podido colocar en la ubicación indicada.");
                    i--;
                }

                MostrarTablero();
            }
        }


        public void GenerarBarcosOponente()
        {
            Random r = new Random();
            int barco, index, fila, columna;
            char[] orientacion = { 'H', 'V' };
            string[] tiposDeBarcos = { "Lancha", "Crucero", "Submarino", "Buque", "Portaaviones" };
            bool creado;

            for (int i = 0; i < tiposDeBarcos.Length; i++)
            {

                barco = i;
                fila = r.Next(0, 9);
                columna = r.Next(0, 9);
                index = r.Next(orientacion.Length);
                creado = ComprobarBarco(barco, fila, columna, orientacion[index]) ? true : false;

                if (!creado)
                {
                    Log.Warn("El barco del rival no se ha podido generar");
                    i--;
                }

                Log.Info("Barco del rival generado.");
            }
        }

        
        //Método para mostrar el tablero del jugador
        public void MostrarTablero()
        {
            Console.WriteLine("");
            Console.WriteLine("0 1 2 3 4 5 6 7 8 9");

            for (int i = 0; i < FILAS; i++)
            {
                for (int j = 0; j < COLUMNAS; j++)
                {
                    char casillaEstado = casillas[i, j].GetEstado();

                    if (casillaEstado == 'X')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                    else if (casillaEstado == 'O')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (casillaEstado == '=')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                    }
                    Console.Write(casillaEstado + " ");
                    Console.ResetColor();
                }

                Console.WriteLine("" + i);
            }
            Console.WriteLine("");
        }

        
        //Método para mostrar el tablero del oponente (Sin mostrar sus barcos)
        public void TableroOponente(int fila, int columna, char estado)
        {
            Console.WriteLine("");
            Console.WriteLine("0 1 2 3 4 5 6 7 8 9");
            for (int i = 0; i < FILAS; i++)
            {

                for (int j = 0; j < COLUMNAS; j++)
                {
                    if (i == fila && j == columna)
                    {
                        if (estado == '=')
                        {
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                        }
                        else if (estado == 'X')
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                        }
                        casillas[fila, columna].SetEstado(estado);
                    }

                    Console.Write(casillas[i, j].GetEstado() + " ");
                    Console.ResetColor();
                }

                Console.WriteLine("" + i);
            }
            Console.WriteLine("");
        }
        

        //Método para establecer los turnos:
        public string Turnos(int fila, int columna, int turno)
        {
            string mensaje = "Agua.";
            int contador = 0;

            for (int i = 0; i < barcos.Length; i++)
            {
                for (int j = 0; j < barcos[i].CasillasBarco.Length; j++)
                {
                    if (barcos[i].CasillasBarco[j] == casillas[fila, columna])
                    {
                        casillas[fila, columna].SetTocado();
                        mensaje = "Tocado.";
                        if (barcos[i].BarcoHundido())
                        {
                            mensaje = "Tocado y hundido.";
                        }

                    }
                }   
            }

            //Comprobamos cuantos barcos se han hundido
            for (int i = 0; i < barcos.Length; i++)
            {
                if (barcos[i].BarcoHundido())
                {
                    contador++;
                }
            }

            //Si el total de barcos hundidos es de 5, tenemos una victoria (puede ser del jugador o del ordenador)
            if (contador == 5)
            {
                mensaje = "Victoria";
            }

            //Mostramos el tablero del oponente con las casillas bombardeadas
            if (turno == 0)
            {
                Console.WriteLine("0 1 2 3 4 5 6 7 8 9");
            }

            for (int i = 0; i < FILAS; i++)
            {

                for (int j = 0; j < COLUMNAS; j++)
                {
                    if (casillas[fila, columna].GetEstado() == '.')
                    {
                        casillas[fila, columna].SetEstado('=');
                        mensaje = "Agua.";
                    }

                    if (turno == 0)
                    {
                        char casillaEstado = casillas[i, j].GetEstado();

                        if (casillaEstado == 'X')
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else if (casillaEstado == '=')
                        {
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                        }
                        else if (casillaEstado == 'O')
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }

                        Console.Write(casillaEstado + " ");
                        Console.ResetColor();
                    }
                }
                if (turno == 0)
                {
                    Console.WriteLine("" + i);
                }
            }
            

            return mensaje;
        }
    }
}
