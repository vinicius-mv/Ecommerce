namespace Catalog.Infrastructure.Settings;

public class DatabaseSettings
{
    public  string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string BrandCollectionName { get; set; } = string.Empty;
    public string TypeCollectionName { get; set; } = string.Empty;
    public string ProductCollectionName { get; set; } = string.Empty;
}