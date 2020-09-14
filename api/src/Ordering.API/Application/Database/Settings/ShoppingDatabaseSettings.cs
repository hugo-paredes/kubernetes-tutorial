namespace Ordering.API.Application.Database.Settings
{
    public class ShoppingDatabaseSettings : IShoppingDatabaseSettings
    {
        public string OrdersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

}
