namespace Catalog.Core.Entities;

public class ProductBrand : BaseEntity
{
    public string Name { get; private set; }

    public ProductBrand(string id, string name)
    {
        Id = id;
        Name = name;
    }

    protected ProductBrand() // constructor for rehydration
    {
    }
}
