namespace Catalog.Core.Specifications;

public class CatalogSpecParams
{

    private const int _maxPageSize = 70;

    public int PageIndex { get; private set; } = 1;

    private int _pageSize;

    public int PageSize
    {
        get { return _pageSize; }
        set { _pageSize = value > _maxPageSize ? _maxPageSize : value; }
    }

    public string? BrandId { get; private set; }

    public string? TypeId { get; private set; }

    public ProductSortOption? Sort { get; private set; }

    public string? Search { get; private set; }
}
