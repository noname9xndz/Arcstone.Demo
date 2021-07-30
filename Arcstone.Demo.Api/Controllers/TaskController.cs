using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Arcstone.Demo.Api.Filters;
using Arcstone.Demo.Application;
using Arcstone.Demo.Application.Domain.Task.Commands;
using Arcstone.Demo.Application.Domain.Task.Queries;
using Arcstone.Demo.Application.Helpers;
using Arcstone.Demo.Application.Models.Others;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Arcstone.Demo.Api.Controllers
{
    [Route("api/task")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        [ClaimRequirement(Constants.Role.ManagerRole, Constants.Role.EmployeeRole)]
        public async Task<ResponseApi<CreateTaskCommandResponse>> CreateTask([FromBody] CreateTaskCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost("list/project")]
        [ClaimRequirement(Constants.Role.ManagerRole, Constants.Role.EmployeeRole)]
        public async Task<ResponseApi<PaginatedList<GetAllTaskByProjectIdQueryResponse>>> GetTasksByProjectIdPaging([FromBody] GetAllTaskByProjectIdQuery request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost("list")]
        [ClaimRequirement(Constants.Role.ManagerRole, Constants.Role.EmployeeRole)]
        public async Task<ResponseApi<PaginatedList<GetAllTaskQueryResponse>>> GetTasksPaging([FromBody] GetAllTaskQuery request)
        {
            return await _mediator.Send(request);
        }


        [HttpPost("info")]
        [ClaimRequirement(Constants.Role.ManagerRole, Constants.Role.EmployeeRole)]
        public async Task<ResponseApi<GetTaskInfoQueryResponse>> GetTaskInfo(GetTaskInfoQuery request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut("update")]
        [ClaimRequirement(Constants.Role.ManagerRole, Constants.Role.EmployeeRole)]
        public async Task<ResponseApi<bool>> UpdateTask(UpdateTaskCommand request)
        {
            return await _mediator.Send(request);
        }
    }
}
