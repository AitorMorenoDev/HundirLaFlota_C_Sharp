using System;
using System.Globalization;
using log4net;
using log4net.Config;

namespace HundirLaFlota
{
    class Juego
    {
        static void Main()
        {
            var factoryEnglish = new ResourceFactory(typeof(Resources));
            var factorySpanish = new ResourceFactory(typeof(Resources_es_ES));
            var factoryDeutsch = new ResourceFactory(typeof(Resources_de_DE));
            var factoryItalian = new ResourceFactory(typeof(Resources_it_IT));
            var factoryFrench = new ResourceFactory(typeof(Resources_fr_FR));

            

            CultureInfo spanish = new CultureInfo("es-ES");
            string welcomeMessageSpanish = factorySpanish.GetString("welcomeMessage", spanish);
            string enterToStartSpanish = factorySpanish.GetString("enterToStart", spanish);

            CultureInfo english = new CultureInfo("en-US");
            string welcomeMessageEnglish = factoryEnglish.GetString("welcomeMessage", english);
            string enterToStartEnglish = factoryEnglish.GetString("enterToStart", english);

            CultureInfo deutsch = new CultureInfo("de-DE");
            string welcomeMessageDeutsch = factoryDeutsch.GetString("welcomeMessage", deutsch);
            string enterToStartDeutsch = factoryDeutsch.GetString("enterToStart", deutsch);

            CultureInfo italian = new CultureInfo("it-IT");
            string welcomeMessageItalian = factoryItalian.GetString("welcomeMessage", italian);
            string enterToStartItalian = factoryItalian.GetString("enterToStart", italian);

            CultureInfo french = new CultureInfo("fr-FR");
            string welcomeMessageFrench = factoryFrench.GetString("welcomeMessage", french);
            string enterToStartFrench = factoryFrench.GetString("enterToStart", french);


            XmlConfigurator.Configure();
            Console.WriteLine(welcomeMessageSpanish);
            Console.WriteLine(welcomeMessageEnglish);
            Console.WriteLine(welcomeMessageDeutsch);
            Console.WriteLine(welcomeMessageItalian);
            Console.WriteLine(welcomeMessageFrench);

            Console.WriteLine("");

            Console.WriteLine(enterToStartSpanish);
            Console.WriteLine(enterToStartEnglish);
            Console.WriteLine(enterToStartDeutsch);
            Console.WriteLine(enterToStartItalian);
            Console.WriteLine(enterToStartFrench);

            ConsoleKeyInfo intro;
            intro = Console.ReadKey(true);

            if (intro.KeyChar == 13) {
                bool victoria = false;
                Random r = new Random();
                int fila, columna;
                int[] turno = { 0, 1 }; // 0 corresponde al turno del oponente, 1 al del jugador
                char[] valor = { '=', 'X' };
                string mensaje = "";
                string continuar = "Pulsa intro para continuar";

                Tablero tableroJugador = new Tablero();
                Tablero tableroOrdenador = new Tablero();
                Tablero tableroOponente = new Tablero();

                //El jugador coloca sus barcos:
                tableroJugador.ColocarBarcos();
                Console.WriteLine("Tu tablero:");
                tableroJugador.MostrarTablero();

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
                        tableroOponente.MostrarTablero();
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

                            if (mensaje == "Agua.")
                            {
                                Console.WriteLine(mensaje);
                                tableroOponente.TableroOponente(fila, columna, valor[0]);

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