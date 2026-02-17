using Catalog.Core.Entities;
using Catalog.Infrastructure.Data.MongoMappings;
using Catalog.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data;

public class DatabaseSeeder
{
    private static readonly string _seedBasePath;

    static DatabaseSeeder()
    {
        _seedBasePath = Path.Combine(AppContext.BaseDirectory, "Data", "SeedData");
    }

    public static async Task Seed(IOptions<DatabaseSettings> databaseOptions)
    {
        var databaseSettings = databaseOptions.Value;
        var client = new MongoClient(databaseSettings.ConnectionString);
        var catalogDb = client.GetDatabase(databaseSettings.DatabaseName);

        var brands = catalogDb.GetCollection<ProductBrand>(ProductBrandMapping.CollectionName);
        await SeedBrands(brands);

        var types = catalogDb.GetCollection<ProductType>(ProductTypeMapping.CollectionName);
        await SeedTypes(types);

        var products = catalogDb.GetCollection<Product>(ProductMapping.CollectionName);
        await SeedProducts(products);
    }

    private static async Task SeedTypes(IMongoCollection<ProductType> types)
    {
        if (await types.CountDocumentsAsync(_ => true) > 0) return;

        var typeData = await File.ReadAllTextAsync(Path.Combine(_seedBasePath, "types.json"));
        var typesToInsert = JsonSerializer.Deserialize<IEnumerable<ProductType>>(typeData);
        await types.InsertManyAsync(typesToInsert);
    }

    private static async Task SeedBrands(IMongoCollection<ProductBrand> brands)
    {
        if (await brands.CountDocumentsAsync(_ => true) > 0) return;

        var brandData = await File.ReadAllTextAsync(Path.Combine(_seedBasePath, "brands.json"));
        var brandsToInsert = JsonSerializer.Deserialize<IEnumerable<ProductBrand>>(brandData);
        await brands.InsertManyAsync(brandsToInsert);
    }

    private static async Task SeedProducts(IMongoCollection<Product> products)
    {
        if (await products.CountDocumentsAsync(_ => true) > 0) return;

        var productData = await File.ReadAllTextAsync(Path.Combine(_seedBasePath, "products.json"));
        var productsToInsert = JsonSerializer.Deserialize<IEnumerable<Product>>(productData);
        foreach (var product in productsToInsert)
        {
            if (product.CreatedDate == default)
                product.SetCreatedDate();

            await products.InsertOneAsync(product);
        }
    }
}
