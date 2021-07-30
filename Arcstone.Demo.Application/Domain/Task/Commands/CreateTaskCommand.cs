using Arcstone.Demo.Application.Models.Others;
using FluentValidation;
using MediatR;

namespace Arcstone.Demo.Application.Domain.Task.Commands
{
    public class CreateTaskCommand : TaskInfo, IRequest<ResponseApi<CreateTaskCommandResponse>>
    {
    }

    public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskValidator()
        {
        }
    }
}