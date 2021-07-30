using Arcstone.Demo.Application.Models.Others;
using FluentValidation;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Arcstone.Demo.Application.Domain.Project.Commands
{
    public class UpdateProjectCommand : ProjectInfo, IRequest<ResponseApi<bool>>
    {
        [Required]
        public Guid Id { set; get; }
    }

    public class UpdateProjectValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectValidator()
        {
        }
    }
}