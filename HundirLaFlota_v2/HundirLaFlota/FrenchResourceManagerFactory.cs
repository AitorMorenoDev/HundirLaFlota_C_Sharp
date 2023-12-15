using System.Reflection;

namespace HundirLaFlota
{
    internal class FrenchResourceManagerFactory: IResourceManagerFactory
    {
        public System.Resources.ResourceManager CreateResourceManager()
        {
            return new System.Resources.ResourceManager("HundirLaFlota.Resources.Resources_fr_FR", Assembly.GetExecutingAssembly());
        }
    }
}
