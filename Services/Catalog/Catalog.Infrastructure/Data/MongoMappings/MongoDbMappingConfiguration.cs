namespace Catalog.Infrastructure.Data.MongoMappings;

public class MongoDbMappingConfiguration
{
    private static bool _isConfigured;

    public static void Configure()
    {
        if (_isConfigured) return;

        BaseEntityMapping.Configure();
        ProductBrandMapping.Configure();
        ProductTypeMapping.Configure();
        ProductMapping.Configure();

        _isConfigured = true;
    }
}
