using System.Reflection;

namespace HundirLaFlota
{
    internal class DeutschResourceManagerFactory: IResourceManagerFactory
    {
        public System.Resources.ResourceManager CreateResourceManager()
        {
            return new System.Resources.ResourceManager("HundirLaFlota.Resources.Resources_de_DE", Assembly.GetExecutingAssembly());
        }
    }
}
