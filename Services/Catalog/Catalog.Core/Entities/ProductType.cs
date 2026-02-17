using System.Text.Json.Serialization;

namespace Catalog.Core.Entities;

public class ProductType : BaseEntity
{
    public string Name { get; private set; }

    public ProductType(string id, string name)
    {
        Id = id;
        Name = name;
    }

    protected ProductType() // constructor for rehydration
    {
    }
}