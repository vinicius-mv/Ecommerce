using Catalog.Core.Entities;
using MongoDB.Bson.Serialization;

namespace Catalog.Infrastructure.Data.MongoMappings;

public static class ProductBrandMapping
{
    public const string CollectionName = "Brands";

    public static void Configure()
    {
        if (BsonClassMap.IsClassMapRegistered(typeof(ProductBrand)))
            return;

        BsonClassMap.RegisterClassMap<ProductBrand>(cm =>
        {
            cm.AutoMap();
        });
    }
}
