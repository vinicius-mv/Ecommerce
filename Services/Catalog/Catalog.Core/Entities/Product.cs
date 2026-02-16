using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Entities;

public class Product : BaseEntity
{
    public string Name { get; private set; }

    public string Summary { get; private set; }

    public string Description { get; private set; }

    public string ImageFile { get; private set; }

    public ProductBrand Brand { get; private set; }

    public ProductType Type { get; private set; }

    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; private set; }

    public DateTimeOffset CreatedDate { get; private set; }
}
