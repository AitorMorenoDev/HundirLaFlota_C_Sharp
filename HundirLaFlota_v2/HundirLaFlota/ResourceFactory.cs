using System;
using System.Globalization;
using System.Resources;

namespace HundirLaFlota
{
    public class ResourceFactory
    {
        private readonly IResourceManagerFactory factory;
        private readonly System.Resources.ResourceManager resourceManager;

        public ResourceFactory(string language)
        {
            factory = language switch
            {
                "en_EN" => new EnglishResourceManagerFactory(),
                "es_ES" => new SpanishResourceManagerFactory(),
                "de_DE" => new DeutschResourceManagerFactory(),
                "it_IT" => new ItalianResourceManagerFactory(),
                "fr_FR" => new FrenchResourceManagerFactory(),
                _ => throw new ArgumentException("Language not supported"),
            };
            resourceManager = factory.CreateResourceManager();
        }

        public string GetString(string text)
        {
            return resourceManager.GetString(text);
        }
    }
}