using Arcstone.Demo.Application.Models.Others;
using FluentValidation;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Arcstone.Demo.Application.Domain.Project.Commands
{
    public class DeleteProjectCommand : ProjectInfo, IRequest<ResponseApi<bool>>
    {
        [Required]
        public Guid Id { set; get; }
    }

    public class DeleteProjectValidator : AbstractValidator<DeleteProjectCommand>
    {
        public DeleteProjectValidator()
        {
        }
    }
}