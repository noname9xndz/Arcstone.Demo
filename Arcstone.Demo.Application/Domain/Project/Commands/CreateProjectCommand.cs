using Arcstone.Demo.Application.Models.Others;
using FluentValidation;
using MediatR;

namespace Arcstone.Demo.Application.Domain.Project.Commands
{
    public class CreateProjectCommand : ProjectInfo, IRequest<ResponseApi<CreateProjectCommandResponse>>
    {
    }

    public class CreateProjectValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectValidator()
        {
        }
    }
}