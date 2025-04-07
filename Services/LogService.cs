using MongoDB.Driver;
using MongoConnection.Context;
using MongoConnection.Model;
using Microsoft.AspNetCore.Mvc;

namespace MongoConnection.Services.CarService
{
    public class LogService
    {
        private readonly IMongoCollection<Logs> _logs;


        public LogService(MongoDbContext dbContext)
        {
            _logs = dbContext.Logs;
        }




        // ✅ Fetch all logs
        public Task<List<Logs>> GetAllLogs()
        {
            return _logs.Find(_ => true).ToListAsync();
        }

        public async void AddLog(Logs message)
        {
            await _logs.InsertOneAsync(message);
        }

    }
}