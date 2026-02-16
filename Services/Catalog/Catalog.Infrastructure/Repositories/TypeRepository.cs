using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class TypeRepository : ITypeRepository
{
    private readonly IMongoCollection<ProductType> _types;

    public TypeRepository(IConfiguration configuration)
    {
        var client = new MongoClient(configuration["DatabaseSettings:ConnectionString"]);

        var catalogDb = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
        _types = catalogDb.GetCollection<ProductType>(configuration["DatabaseSettings:TypeCollectionName"]);
    }

    public async Task<IEnumerable<ProductType>> GetAllTypes()
    {
        return await _types.Find(_ => true).ToListAsync();
    }

    public async Task<ProductType> GetType(string id)
    {
        
        return await _types.Find(t => t.Id == id).FirstOrDefaultAsync();
    }
}
