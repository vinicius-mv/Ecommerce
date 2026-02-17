using Catalog.Core.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Catalog.Infrastructure.Data.MongoMappings;

public class BaseEntityMapping
{
    public static void Configure()
    {
        BsonClassMap.RegisterClassMap<BaseEntity>(cm =>
        {
            if (BsonClassMap.IsClassMapRegistered(typeof(BaseEntity)))
                return;

            cm.AutoMap();
            cm.MapIdProperty(e => e.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new MongoDB.Bson.Serialization.Serializers.StringSerializer(BsonType.ObjectId));
            cm.SetIsRootClass(true);
        });
    }
}
