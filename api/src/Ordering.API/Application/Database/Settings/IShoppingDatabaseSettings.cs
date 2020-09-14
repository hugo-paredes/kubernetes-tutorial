namespace Ordering.API.Application.Database.Settings
{
    public interface IShoppingDatabaseSettings
    {
        string OrdersCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }

}
