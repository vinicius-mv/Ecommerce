using Catalog.Core.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Catalog.Infrastructure.Data.MongoMappings
{
    public static class ProductMapping
    {
        public const string CollectionName = "Products";

        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Product>(cm =>
            {
                if (BsonClassMap.IsClassMapRegistered(typeof(Product)))
                    return;

                cm.AutoMap();
                cm.MapProperty(p => p.Price)
                    .SetSerializer(new MongoDB.Bson.Serialization.Serializers.DecimalSerializer(BsonType.Decimal128));
                cm.MapCreator(p => new Product(p.Name, p.Summary, p.Description, p.ImageFile, p.Brand, p.Type, p.Price));
            });
        }
    }
}
