using System;
using log4net;
using log4net.Config;

namespace HundirLaFlota
{
    class Juego
    {
        static void Main()
        {

            XmlConfigurator.Configure();
            Console.WriteLine("Bienvenido a Hundir la flota!");
            Console.WriteLine("Pulsa intro para empezar, o cualquier otra tecla para salir.");

            ConsoleKeyInfo intro;
            intro = Console.ReadKey(true);

            if (intro.KeyChar == 13) {
                bool victoria = false;
                Random r = new Random();
                int fila, columna;
                int[] turno = { 0, 1 }; // 0 corresponde al turno del oponente, 1 al del jugador
                //char[] valor = { '=', 'X' };
                string mensaje = "";
                string continuar = "Pulsa intro para continuar";

                Tablero tableroJugador = new Tablero();
                Tablero tableroOrdenador = new Tablero();
                Tablero tableroOponente = new Tablero();

                //El jugador coloca sus barcos:
                tableroJugador.ColocarBarcos();
                Console.WriteLine("Tu tablero:");
                tableroJugador.LogTableroJugador();

                Console.WriteLine("Generando barcos del oponente...");
                tableroOrdenador.GenerarBarcosOponente();
                Console.WriteLine("Proceso finalizado. Pulsa intro para comenzar!");
                Console.WriteLine(intro.KeyChar);
                if (intro.KeyChar == 13)
                {
                    while (!victoria)
                    {
                        Console.Clear();
                        Console.WriteLine("Turno del jugador.");
                        tableroOponente.LogTableroJugador();
                        Console.Write("Introduce fila (0 - 9): ");
                        try
                        {
                            fila = Convert.ToInt32(Console.ReadLine());
                            while (fila > 10 || fila < 0)
                            {
                                Console.Write("Introduce una fila correcta (0 - 9): ");
                                fila = Convert.ToInt32(Console.ReadLine());
                            }

                            Console.Write("Introduce columna (0 - 9): ");
                            columna = Convert.ToInt32(Console.ReadLine());
                            while (columna > 10 || columna < 0)
                            {
                                Console.Write("Introduce una columna correcta (0 - 9): ");
                                columna = Convert.ToInt32(Console.ReadLine());
                            }

                            mensaje = tableroOrdenador.Turnos(fila, columna, turno[1]);

                            tableroOponente.LogTableroOponente();

                            /*

                            if (mensaje == "Agua.")
                            {
                                Console.WriteLine(mensaje);
                                tableroOponente.LogTableroOponente(fila, columna, valor[0]);

                            }
                            else if (mensaje == "Tocado.")
                            {
                                Console.WriteLine(mensaje);
                                tableroOponente.TableroOponente(fila, columna, valor[1]);

                            }
                            else if (mensaje == "Tocado y hundido.")
                            {
                                Console.WriteLine(mensaje);
                                tableroOponente.TableroOponente(fila, columna, valor[1]);
                            }

                            */

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("error: " + e);
                        }

                        if (mensaje == "Victoria")
                        {
                            victoria = true;
                            Console.WriteLine("Victoria del jugador, ¡enhorabuena!");
                            Console.WriteLine("");
                            Console.WriteLine("Fin del juego.");
                        }

                        Console.WriteLine(continuar);
                        intro = Console.ReadKey(true);
                        Console.WriteLine(intro.KeyChar);

                        while (intro.KeyChar != 13)
                        {
                            Console.WriteLine(continuar);
                            intro = Console.ReadKey(true);
                        }

                        Console.Clear();
                        Console.WriteLine("Turno del ordenador: ");
                        fila = r.Next(0, 10);
                        columna = r.Next(0, 10);                        Console.WriteLine("");

                        mensaje = tableroJugador.Turnos(fila, columna, turno[0]);

                        Console.WriteLine("");

                        if (mensaje == "Victoria")
                        {
                            victoria = true;
                            Console.WriteLine("Victoria del oponente!");
                            Console.WriteLine("");
                            Console.WriteLine("Fin del juego.");
                        }

                        Console.WriteLine("Jugada del ordenador: Fila " + fila + ", Columna " + columna + ": " + mensaje);

                        Console.WriteLine(continuar);
                        intro = Console.ReadKey(true);
                        Console.WriteLine(intro.KeyChar);

                        while (intro.KeyChar != 13)
                        {
                            Console.WriteLine(continuar);
                            intro = Console.ReadKey(true);
                        }
                    }

                }
                else
                {
                    Console.WriteLine("Por favor, pulsa enter.");
                }
                Console.ReadKey();
            }
        }
    }
}