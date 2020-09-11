using MongoDB.Driver;
using Ordering.API.Application.Database.Models;
using Ordering.API.Application.Database.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.API.Application.Services
{
    public class OrderService
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderService(IShoppingDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _orders = database.GetCollection<Order>(settings.OrdersCollectionName);
        }

        public async Task<IEnumerable<Order>> GetAsync(CancellationToken cancellationToken) =>
            await _orders.Find(book => true).ToListAsync(cancellationToken);

        public async Task<Order> GetAsync(string id, CancellationToken cancellationToken) =>
            await _orders
                .Find(order => order.Id == id)
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<Order> CreateAsync(Order book, CancellationToken cancellationToken)
        {
            await _orders.InsertOneAsync(book, new InsertOneOptions(), cancellationToken);
            return book;
        }

        public async Task UpdateAsync(string id, Order bookIn, CancellationToken cancellationToken) =>
            await _orders.ReplaceOneAsync(book => book.Id == id, bookIn, cancellationToken: cancellationToken);

        public async Task RemoveAsync(Order bookIn, CancellationToken cancellationToken)
        {
            await _orders.DeleteOneAsync(book => book.Id == bookIn.Id, cancellationToken);
        }

        public async Task RemoveAsync(string id, CancellationToken cancellationToken) =>
            await _orders.DeleteOneAsync(book => book.Id == id, cancellationToken);
    }
}
