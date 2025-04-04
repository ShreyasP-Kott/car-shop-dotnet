using Microsoft.AspNetCore.Mvc;
using MongoConnection.Services;

[ApiController]
[Route("api/cars")]
public class CarController : ControllerBase
{
    private readonly CarService _carService;

    public CarController(CarService carService)
    {
        _carService = carService;
    }


    [HttpGet("all")]
    public async Task<IActionResult> GetCars()
    {
        var cars = await _carService.GetAllCarsAsync();
        return Ok(cars);
    }
}

