using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using User.Resource.Application.CQRS.Command;
using User.Resource.Application.CQRS.Query;
using User.Resource.Application.DTOs;

namespace User.Resource.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    //[Authorize]
    public class ResourceController : Controller
    {
        IMediator mediator;
        public ResourceController(IMediator med)
        {
            mediator = med;
        }

        [HttpGet]
        [Authorize]
        // [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            Console.WriteLine("end point hit");
            if (ModelState.IsValid)
            {
                Console.WriteLine(User.Claims.FirstOrDefault(c => c.Type == "username")?.Value);
                var foundUser = await mediator.Send(new GetUserQuery() { uname = new GetUserDataDTO() { UserName = User.Claims.FirstOrDefault(c => c.Type == "username")?.Value } });
                if (foundUser != null)
                {
                    return Ok(foundUser);
                }
                else
                    return BadRequest(new { message = "not found" });

            }

            return BadRequest();

        }


        [HttpPost]
        [Authorize(Policy = "Adminpolicy")]
       // [Route("{param:string}")]
        public async Task<IActionResult> Set( [FromForm] string  address, [FromForm] string phone)
        {
            if (ModelState.IsValid)
            {
                SetUserDataDTO setuserdat = new SetUserDataDTO()
                {
                    userName = User.Claims.FirstOrDefault(c => c.Type == "username")?.Value,
                    Address=address,
                    phone=phone
                };


                var setUser = await mediator.Send(new SetUserCommand() { stData = setuserdat });
                if (setUser != null)
                {
                    return Ok(setUser);
                }
                else
                {
                    return BadRequest(new { message = "not found" });
                }
            }
            else

                return BadRequest(new { message = "invalid data" });


            
        }
    }
}
