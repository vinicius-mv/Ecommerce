using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data.MongoMappings;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly IMongoCollection<ProductBrand> _brands;

    public BrandRepository(IMongoDatabase catalogDb)
    {
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