using Catalog.Core.Entities;
using Catalog.Core.Specifications;

namespace Catalog.Core.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProducts();

    Task<Pagination<Product>> GetProducts(CatalogSpecParams specParams);
     
    Task<IEnumerable<Product>> GetProductsByName(string  productName);

    Task<IEnumerable<Product>> GetProductsByBrand(string brandName);

    Task<Product> GetProduct(string productId);

    Task<Product> CreateProduct(Product product);

    Task<bool> UpdateProduct(Product product);

    Task<bool> DeleteProduct(string productId);

    Task<ProductBrand> GetBrandById(string brandId);

    Task<ProductType> GetTypeById(string typeId);
}
