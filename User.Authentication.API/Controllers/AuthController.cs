using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using User.Authentication.Application.Command;
using User.Authentication.Application.DTO;
using User.Authentication.Domain.Entities;

namespace User.Authentication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        IMediator mediator;
        public AuthController(IMediator med)
        {
            mediator = med;
        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> login(LoginDTO logindata)
        {

            if (ModelState.IsValid)
            {
                var token = await mediator.Send(new LoginQuery(logindata));
                if (token != null)
                {
                    return Ok(token);
                }
                else
                    return BadRequest(new { message = "Invalid Credentials" });
            }
            else
                return StatusCode(401);
        }
        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> addUsers(RegisterDTO newUser)
        {
            if (ModelState.IsValid)
            {
               // await mediator.Send(new RegisterCommand(newUser));
                return Ok(await mediator.Send(new RegisterCommand(newUser)));
            }
            else
                return BadRequest(new { message = "details not valid" });
        }
    }
}
