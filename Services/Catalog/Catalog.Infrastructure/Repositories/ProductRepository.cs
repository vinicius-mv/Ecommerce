using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Specifications;
using Catalog.Infrastructure.Data.MongoMappings;
using Catalog.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using static System.Net.WebRequestMethods;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _produts;
    private readonly IMongoCollection<ProductBrand> _brands;
    private readonly IMongoCollection<ProductType> _types;

    public ProductRepository(IOptions<DatabaseSettings> databaseOptions)
    {
        var databaseSettings = databaseOptions.Value;
        var client = new MongoClient(databaseSettings.ConnectionString);
        var catalogDb = client.GetDatabase(databaseSettings.DatabaseName);

        _produts = catalogDb.GetCollection<Product>(ProductMapping.CollectionName);
        _brands = catalogDb.GetCollection<ProductBrand>(ProductBrandMapping.CollectionName);
        _types = catalogDb.GetCollection<ProductType>(ProductTypeMapping.CollectionName);
    }

    public async Task<Product> CreateProduct(Product product)
    {
        await _produts.InsertOneAsync(product);
        return product;
    }

    public async Task<bool> DeleteProduct(string productId)
    {
        var deleteResult = await _produts.DeleteOneAsync(p => p.Id == productId);
        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await _produts.Find(_ => true).ToListAsync();
    }

    public async Task<ProductBrand> GetBrandById(string brandId)
    {
        return _brands.Find(p => p.Id == brandId).FirstOrDefault();
    }

    public async Task<Product> GetProduct(string productId)
    {
        return await _produts.Find(p => p.Id == productId).FirstOrDefaultAsync();
    }

    public async Task<Pagination<Product>> GetProducts(CatalogSpecParams specParams)
    {
        var builder = Builders<Product>.Filter;
        var filter = builder.Empty;

        if (!string.IsNullOrEmpty(specParams.Search))
        {
            var productNameToSearch = specParams.Search.ToLower();
            filter &= builder.Where(p => p.Name.ToLower().Contains(productNameToSearch));
        }

        if (!string.IsNullOrEmpty(specParams.BrandId))
        {
            filter &= builder.Eq(p => p.Brand.Id, specParams.BrandId);
        }

        if (!string.IsNullOrEmpty(specParams.TypeId))
        {
            filter &= builder.Where(p => p.Type.Id == specParams.TypeId);
        }

        var totalItems = await _produts.CountDocumentsAsync(filter);
        var data = await ApplyDataFilter(specParams, filter);

        return new Pagination<Product>(specParams.PageIndex, specParams.PageSize, totalItems, data);
    }

    public async Task<IEnumerable<Product>> GetProductsByBrand(string brandName)
    {
        return await _produts.Find(p => p.Brand.Name.ToLower() == brandName.ToLower()).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByName(string productName)
    {
        var filter = Builders<Product>.Filter.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression($".*{productName}.*", "i"));
        return await _produts.Find(filter).ToListAsync();
    }

    public async Task<ProductType> GetTypeById(string typeId)
    {
        return await _types.Find(t => t.Id == typeId).FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var replaceResult = await _produts.ReplaceOneAsync(p => p.Id == product.Id, product);
        return replaceResult.IsAcknowledged && replaceResult.ModifiedCount > 0;
    }

    private async Task<IReadOnlyCollection<Product>> ApplyDataFilter(CatalogSpecParams specParams, FilterDefinition<Product> filter)
    {
        var sortDefinitionBuilder = Builders<Product>.Sort.Ascending();

        if (specParams.Sort is not null)
        {
            sortDefinitionBuilder = specParams.Sort switch
            {
                ProductSortOption.PriceAsc => Builders<Product>.Sort.Ascending(p => p.Price),
                ProductSortOption.PriceDesc => Builders<Product>.Sort.Descending(p => p.Price),
                ProductSortOption.NameAsc => Builders<Product>.Sort.Ascending(p => p.Name),
                ProductSortOption.NameDesc => Builders<Product>.Sort.Descending(p => p.Name),
                _ => Builders<Product>.Sort.Ascending(p => p.Name),
            };
        }

        var skipCount = (specParams.PageIndex - 1) * specParams.PageSize;

        var data = await _produts.Find(filter)
            .Sort(sortDefinitionBuilder)
            .Skip(skipCount)
            .Limit(specParams.PageSize)
            .ToListAsync();

        return await _produts.Find(filter).Sort(sortDefinitionBuilder).ToListAsync();
    }
}