using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoConnection.Model;
using MongoConnection.Services;
using MongoConnection.Services.CarService;


[ApiController]
[Route("api/cars")]
public class CarController : ControllerBase
{
    private readonly CarService _carService;
    
    public CarController(CarService carService) // Example of constructor DI
    {
        _carService = carService;
    }
    

    [HttpGet("all")]
    [Authorize]
    public async Task<IActionResult> GetCars()
    {
        var cars = await _carService.GetAllCarsAsync();
        return Ok(cars);
    }

    [HttpPost("addCar")]
    [Authorize]
    public async Task<IActionResult> InsertCar([FromBody] Cars carDetails)
    {
        if (carDetails == null)
        {
            return BadRequest("Car details are required.");
        }

        var result = await _carService.InsertCarData(carDetails);

        if (string.IsNullOrEmpty(result))
        {
            return StatusCode(500, "Failed to insert car details.");
        }

        return Ok(new { Id = result, Message = "Car details inserted successfully." });
    }




}

