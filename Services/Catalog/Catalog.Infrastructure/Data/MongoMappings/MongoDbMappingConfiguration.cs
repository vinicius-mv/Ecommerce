namespace Catalog.Infrastructure.Data.MongoMappings;

public class MongoDbMappingConfiguration
{
    private static bool _isConfigured;

    public static void Configure()
    {
        if (_isConfigured) return;

        BaseEntityMapping.Configure();
        ProductMapping.Configure();
        // Add other entity mappings here

        _isConfigured = true;
    }
}
