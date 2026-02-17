using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data.MongoMappings;
using Catalog.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly IMongoCollection<ProductBrand> _brands;

    public BrandRepository(IOptions<DatabaseSettings> databaseOptions)
    {
        var databaseSettings = databaseOptions.Value;
        var client = new MongoClient(databaseSettings.ConnectionString);
        var catalogDb = client.GetDatabase(databaseSettings.DatabaseName);
        _brands = catalogDb.GetCollection<ProductBrand>(ProductBrandMapping.CollectionName);
    }

    public async Task<IEnumerable<ProductBrand>> GetAllBrands()
    {
        return await _brands.Find(_ => true).ToListAsync();
    }

    public async Task<ProductBrand> GetBrand(string id)
    {
        return await _brands.Find(b => b.Id == id).FirstOrDefaultAsync();
    }
}