namespace Catalog.Core.Entities;

public class Product : BaseEntity
{
    public string Name { get; private set; }

    public string Summary { get; private set; }

    public string Description { get; private set; }

    public string ImageFile { get; private set; }

    public ProductBrand Brand { get; private set; }

    public ProductType Type { get; private set; }

    public decimal Price { get; private set; }

    public DateTimeOffset CreatedDate { get; private set; }

    public Product(string name, string summary, string description, string imageFile, ProductBrand brand, ProductType type, decimal price)
    {
        Name = name;
        Summary = summary;
        Description = description;
        ImageFile = imageFile;
        Brand = brand;
        Type = type;
        Price = price;

        SetCreatedDate();
    }

    public void SetCreatedDate()
    {
        if (this.CreatedDate != default)
            throw new InvalidOperationException("CreatedDate has already been set and cannot be modified.");

        this.CreatedDate = DateTime.UtcNow;
    }

    private Product() // For MongoDB deserialization
    {
    }
}
