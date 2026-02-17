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
            if (BsonClassMap.IsClassMapRegistered(typeof(Product)))
                return;

            BsonClassMap.RegisterClassMap<Product>(cm =>
            {
                cm.AutoMap();
            });
        }
    }
}
