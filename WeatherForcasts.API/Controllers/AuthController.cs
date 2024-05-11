using Application.Dtos.Users;
using Application.Features.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeatherForcasts.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // /api/auth/register
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterUserDto model)
        {
            if (ModelState.IsValid)
            {
                var command = new RegisterUserCommand() { RegisterUserModel = model };
                var result = await _mediator.Send(command);
                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Kindly provide all the required fields");
        }

        // /api/auth/login
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginUserDto model)
        {
            if (ModelState.IsValid)
            {
                var command = new LoginUserCommand() { LoginUserModel = model };
                var result = await _mediator.Send(command);
                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }
            return BadRequest("Invalid login credentials");
        }
    }
}
