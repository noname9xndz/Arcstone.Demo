using Arcstone.Demo.Api.Filters;
using Arcstone.Demo.Application;
using Arcstone.Demo.Application.Domain.Project.Commands;
using Arcstone.Demo.Application.Models.Others;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Arcstone.Demo.Application.Domain.Project.Queries;
using Arcstone.Demo.Application.Helpers;

namespace Arcstone.Demo.Api.Controllers
{
    [Route("api/project")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        [ClaimRequirement(Constants.Role.ManagerRole)]
        public async Task<ResponseApi<CreateProjectCommandResponse>> CreateProject([FromBody] CreateProjectCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost("list")]
        [ClaimRequirement(Constants.Role.ManagerRole,Constants.Role.EmployeeRole)]
        public async Task<ResponseApi<PaginatedList<GetAllProjectQueryResponse>>> GetProjectsPaging([FromBody] GetAllProjectQuery request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost("statistic")]
        [ClaimRequirement(Constants.Role.ManagerRole, Constants.Role.EmployeeRole)]
        public async Task<ResponseApi<PaginatedList<GetAllProjectQueryResponse>>> Statistic([FromBody] ProjectStatisticQuery request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost("info")]
        [ClaimRequirement(Constants.Role.ManagerRole, Constants.Role.EmployeeRole)]
        public async Task<ResponseApi<GetProjectInfoQueryResponse>> GetProjectInfo(GetProjectInfoQuery request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut("update")]
        [ClaimRequirement(Constants.Role.ManagerRole, Constants.Role.EmployeeRole)]
        public async Task<ResponseApi<bool>> UpdateProject(UpdateProjectCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpDelete("delete")]
        [ClaimRequirement(Constants.Role.ManagerRole)]
        public async Task<ResponseApi<bool>> DeleteProject(DeleteProjectCommand request)
        {
            return await _mediator.Send(request);
        }

    }
}