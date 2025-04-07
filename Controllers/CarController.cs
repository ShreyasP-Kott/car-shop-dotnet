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

    [HttpGet("log")]
    public async Task<IActionResult> GetAllLogs([FromServices] LogService logger) // Example of Parameter DI
    {
        var logs = await logger.GetAllLogs();
        return Ok(logs);
    }

    // Method Injection: service passed to a helper method
    [HttpPost("log-maintenance")]
    public IActionResult LogMaintenance([FromServices] LogService logger, [FromBody] Logs message)
    {
        PerformMaintenanceLogging(message, logger);  // Method Injection
        return Ok("Maintenance logged.");
    }

    // This method is not part of the HTTP pipeline; injected service passed in manually
    private  async void PerformMaintenanceLogging(Logs message, LogService logger)
    {
        logger.AddLog(message);
    }

    // Parameter Injection using [FromServices]
    [HttpGet("request-id")]
    public IActionResult GetRequestId([FromServices] IRequestIdGenerator generator)
    {
        return Ok(new { RequestId = generator.GetRequestId() });
    }
}

