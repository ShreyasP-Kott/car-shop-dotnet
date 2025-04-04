using MongoDB.Driver;
using MongoConnection.Context;
using MongoConnection.Model;

namespace MongoConnection.Services
{
    public class CarService
    {
        private readonly IMongoCollection<Cars> _cars;
        

        public CarService(MongoDbContext dbContext)
        {
            _cars = dbContext.Cars;
        }


        // ✅ Fetch all Cars
        public async Task<List<Cars>> GetAllCarsAsync()
        {
            return await _cars.Find(_ => true).ToListAsync();
        }

    }
}