using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data.MongoMappings;
using Catalog.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Repositories;

public class TypeRepository : ITypeRepository
{
    private readonly IMongoCollection<ProductType> _types;

    public TypeRepository(IOptions<DatabaseSettings> databaseOptions)
    {
        var databaseSettings = databaseOptions.Value;
        var client = new MongoClient(databaseSettings.ConnectionString);
        var catalogDb = client.GetDatabase(databaseSettings.DatabaseName);
        _types = catalogDb.GetCollection<ProductType>(ProductTypeMapping.CollectionName);
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
