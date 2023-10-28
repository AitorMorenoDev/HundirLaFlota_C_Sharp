using System;
using System.Globalization;
using System.Resources;

public class ResourceFactory
{
    private readonly ResourceManager resourceManager;

    public ResourceFactory(Type resourceType)
    {
        resourceManager = new ResourceManager(resourceType);
    }

    public string GetString(string key, CultureInfo culture)
    {
        return resourceManager.GetString(key);
    }
}
