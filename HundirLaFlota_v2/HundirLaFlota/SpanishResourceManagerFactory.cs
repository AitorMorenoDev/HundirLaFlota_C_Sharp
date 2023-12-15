using System.Reflection;

namespace HundirLaFlota
{
    internal class SpanishResourceManagerFactory: IResourceManagerFactory
    {
        public System.Resources.ResourceManager CreateResourceManager()
        {
            return new System.Resources.ResourceManager("HundirLaFlota.Resources.Resources_es_ES", Assembly.GetExecutingAssembly());
        }
    }
}
