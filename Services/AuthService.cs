using MongoConnection.Context;
using MongoConnection.Model;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using JWTService;

namespace MongoConnection.Auth
{   
    
    public class AuthService
    {
        private readonly IMongoCollection<UserDetails> _users;
        private readonly JwtTokenService _jwtService;

        public AuthService(MongoDbContext dbContext, JwtTokenService jwt)
        {
            _users = dbContext.Users;
            _jwtService = jwt;
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

            var token = _jwtService.GenerateToken("1", user.Email);

            return new { Token = token, User = user, Message = "User successfully logged in." };

            
        }
    }
}
