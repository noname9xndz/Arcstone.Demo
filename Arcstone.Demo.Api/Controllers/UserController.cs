using Arcstone.Demo.Application.Domain.Users.Queries;
using Arcstone.Demo.Application.Models.Others;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Arcstone.Demo.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ResponseApi<string>> Login([FromBody] GetTokenQuery request)
        {
            return await _mediator.Send(request);
        }
    }
}