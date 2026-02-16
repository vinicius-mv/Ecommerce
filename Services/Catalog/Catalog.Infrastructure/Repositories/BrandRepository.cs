using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly IMongoCollection<ProductBrand> _brands;

    public BrandRepository(IConfiguration configuration)
    {
        var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);

        var catalogDb = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
        _brands= catalogDb.GetCollection<ProductBrand>(configuration["DatabaseSettings:BrandsCollectionName"]);
    }

    public async Task<IEnumerable<ProductBrand>> GetAllBrands()
    {
        return await _brands.Find(_ => true).ToListAsync();
    }

    public async Task<ProductBrand> GetBrand(string id)
    {
        return await _brands.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
}