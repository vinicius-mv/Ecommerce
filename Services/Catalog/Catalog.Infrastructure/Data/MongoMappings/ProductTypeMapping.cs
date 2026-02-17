using Catalog.Core.Entities;
using MongoDB.Bson.Serialization;

namespace Catalog.Infrastructure.Data.MongoMappings;

public class ProductTypeMapping
{
    public const string CollectionName = "Types";

    public static void Configure()
    {
        if (BsonClassMap.IsClassMapRegistered(typeof(ProductType)))
            return;

        BsonClassMap.RegisterClassMap<ProductType>(cm =>
        {
            cm.AutoMap();
        });
    }
}
