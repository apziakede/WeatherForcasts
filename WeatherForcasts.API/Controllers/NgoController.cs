using Application.Dtos.Ngos;
using Application.Features.Forcasts.Commands;
using Application.Features.Forcasts.Queries;
using Application.Features.Ngos.Commands;
using Application.Features.Ngos.Queries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WeatherForcasts.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NgoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NgoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("CreateNgo")]
        [Authorize]
        public async Task<IActionResult> CreateNgo([FromBody] CreateNgoDto model)
        {
            var command = new CreateNgoCommand()
            {
                NewNgo = model
            };
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                Log.Information("Registered an NGO => {@model}", model);
                return Ok(result);
            }
            Log.Error("An error occurred => {@result}", result);
            return BadRequest(result);
        }

        [HttpPut]
        [Route("UpdateNgo")]
        [Authorize]
        public async Task<IActionResult> UpdateNgo([FromBody] UpdateNgoDto model)
        {
            var command = new UpdateNgoCommand()
            {
                UpdateNgo = model
            };
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                Log.Information("Updated NGO Details => {@model}", model);
                return Ok(result);
            }
            Log.Error("An error occurred => {@result}", result);
            return BadRequest(result);
        }

        [HttpGet]
        [Route("GetNGOs")] 
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllNgoQuery()); 
                if (result !=null)
                {
                    Log.Information("Requested All NGO Details => {@model}", result);
                    return Ok(result);
                }
            Log.Error("An error occurred => {@result}", result);
            return BadRequest(result);
        }

        //// GET api/<CustomersController>/5
        [HttpGet("GetNGO/{id}")] 
        public async Task<IActionResult> Get(int id)
        {
            var command = new GetNgoQuery() { Id = id };
            var result = await _mediator.Send(command);
            if (result != null)
            {
                Log.Information("Requested NGO Details => {@model}", result);
                return Ok(result);
            }
            Log.Error("An error occurred => {@result}", result);
            return BadRequest(result);
        }
    }
}
