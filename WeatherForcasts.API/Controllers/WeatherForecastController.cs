using Application.Features.Forcasts.Commands;
using Application.Features.Forcasts.Queries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WeatherForcasts.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IMediator _mediator;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> GetAll()
        {
          var result = await _mediator.Send(new GetAllForcastsQuery());
            return Ok(result);
        }

        [HttpPost]
        [Route("api/CreateForcast")]
        public async Task<IActionResult> CreateForcast([FromBody] WeatherForecast forcast)
        {
            var command = new CreateForcastCommand()
            {
                NewForcast = forcast
            };
            var result=await _mediator.Send(command);
            return Ok(result);
        }
    }
}
