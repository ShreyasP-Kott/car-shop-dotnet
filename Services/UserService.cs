using MongoDB.Driver;
using MongoConnection.Context;
using MongoConnection.Model;

namespace MongoConnection.Services
{
    public class UserService
    {
        private readonly IMongoCollection<UserDetails> _users;
        private readonly IMongoCollection<Address> _addresses;

        public UserService(MongoDbContext dbContext)
        {
            _users = dbContext.Users;
            _addresses = dbContext.Addresses;
        }

        // ✅ Insert User and Address
        public async Task<string> CreateUserWithAddress(UserDetails user, Address address)
        {
            // Insert Address First
            await _addresses.InsertOneAsync(address);

            // Assign AddressId to User
            user.AddressId = address.Id;

            // Insert User
            await _users.InsertOneAsync(user);

            return user.Id;
        }

        // ✅ Fetch User with Address using LINQ
        public async Task<object> GetUserWithAddress(string userId)
        {
            var userWithAddress = (from user in _users.AsQueryable()
                                   join address in _addresses.AsQueryable()
                                   on user.AddressId equals address.Id
                                   where user.Id == userId
                                   select new
                                   {
                                       user.Id,
                                       user.Name,
                                       user.Email,
                                       Address = new
                                       {
                                           address.Street,
                                           address.City,
                                           address.State,
                                           address.ZipCode
                                       }
                                   });

            return userWithAddress;
        }
    }
}