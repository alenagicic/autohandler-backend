using Microsoft.AspNetCore.Mvc;

namespace autohandler_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{

    [HttpGet(Name = "GetWeatherForecast")]
    public string Get()
    {
        return "success";
    }
}
