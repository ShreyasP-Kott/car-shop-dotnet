using Microsoft.AspNetCore.Mvc;
using MongoConnection.Models;
using MongoConnection.Services;

namespace MongoConnection.Controllers
{
    [ApiController]
    [Route("api/address")]
    public class AddressController : ControllerBase
    {
        private readonly MongoService _mongoService;

        public AddressController(MongoService mongoService)
        {
            _mongoService = mongoService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress([FromBody] Address address)
        {
            await _mongoService.AddAddressAsync(address);
            return Ok("Address created successfully");
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAddress(string userId)
        {
            var address = await _mongoService.GetAddressesByUserIdAsync(userId);
            if (address == null)
                return NotFound("Address not found");
            return Ok(new { Address = address } );
        }
    }
}
