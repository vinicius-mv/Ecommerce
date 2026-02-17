namespace Catalog.Core.Specifications;

public class CatalogSpecParams
{
    private const int _maxPageSize = 70;
    private int _pageSize = 10;
    public int PageIndex { get; set; } = 1;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > _maxPageSize) ? _maxPageSize : value;
    }
    public string? BrandId { get; set; }
    public string? TypeId { get; set; }
    public ProductSortOption? Sort { get; set; }
    public string? Search { get; set; }
}
