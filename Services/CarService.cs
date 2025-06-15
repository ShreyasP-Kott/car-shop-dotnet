using MongoDB.Driver;
using MongoConnection.Context;
using MongoConnection.Model;
using System.Text;

namespace MongoConnection.Services.CarService
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

        public async Task<string> InsertCarData(Cars carDetails)
        {
            await _cars.InsertOneAsync(carDetails);

            return carDetails.Id;
        }


    }
}