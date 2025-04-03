using MongoConnection.Context;
using MongoConnection.Model;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MongoConnection.Auth
{   
    
    public class AuthService
    {
        private readonly IMongoCollection<UserDetails> _users;

        public AuthService(MongoDbContext dbContext)
        {
            _users = dbContext.Users;
        }

        public async Task<dynamic> Authenticate(string email, string password)
        {
            var user = await _users.AsQueryable()
                .Where(u => u.Email == email && u.Password == password)
                .Select(u => new { u.Name, u.Email })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return  new { Message = "Invalid email or password", Token = "" };
            }

            // Generate JWT token (implement this method)
            var token = GenerateJwtToken(user);

            return new { Token = token, User = user, Message = "User successfully logged in." };

            
        }


        private string GenerateJwtToken(object user)
        {
            // Implement JWT token generation here
            return "your-jwt-token";
        }
    }
}
