using log4net;
using log4net.Config;
using Newtonsoft.Json;

namespace HundirLaFlota
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string json = File.ReadAllText("config.json");
            Configuration config = JsonConvert.DeserializeObject<Configuration>(json) ?? throw new InvalidDataException();

            var resLanguage = new ResourceFactory(config.Language);

            XmlConfigurator.Configure();

            Console.WriteLine(resLanguage.GetString("welcomeMessage"));
            Console.WriteLine(resLanguage.GetString("enterToStart"));

            ConsoleKeyInfo intro;
            intro = Console.ReadKey(true);

            if (intro.KeyChar == 13)
            {
                Juego juego = new Juego();
                juego.JuegoMain();
            }
        }
    }
}
