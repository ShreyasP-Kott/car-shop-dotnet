using MongoDB.Driver;
using MongoConnection.Model;

namespace MongoConnection.Context
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext()
        {
            var client = new MongoClient("mongodb://localhost:27017"); // Change if needed
            _database = client.GetDatabase("Records");
        }

        public IMongoCollection<UserDetails> Users => _database.GetCollection<UserDetails>("Users");
        public IMongoCollection<Address> Addresses => _database.GetCollection<Address>("Addresses");
        public IMongoCollection<Cars> Cars => _database.GetCollection<Cars>("Cars");
        public IMongoCollection<Logs> Logs => _database.GetCollection<Logs>("Logs");
    }
}
