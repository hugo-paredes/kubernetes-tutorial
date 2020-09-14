using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Ordering.API.Application.Database.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string Date { get; set; }
        public decimal TotalPrice { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
