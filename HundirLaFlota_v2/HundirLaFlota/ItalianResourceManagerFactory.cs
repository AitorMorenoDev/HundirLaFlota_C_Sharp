using System.Reflection;

namespace HundirLaFlota
{
    internal class ItalianResourceManagerFactory: IResourceManagerFactory
    {
        public System.Resources.ResourceManager CreateResourceManager()
        {
            return new System.Resources.ResourceManager("HundirLaFlota.Resources.Resources_it_IT", Assembly.GetExecutingAssembly());
        }
    }
}
