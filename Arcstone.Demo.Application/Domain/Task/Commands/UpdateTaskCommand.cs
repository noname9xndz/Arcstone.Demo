using Arcstone.Demo.Application.Models.Others;
using FluentValidation;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Arcstone.Demo.Application.Domain.Task.Commands
{
    public class UpdateTaskCommand : TaskInfo, IRequest<ResponseApi<bool>>
    {
        [Required]
        public Guid Id { set; get; }

        [Required]
        public string Name { set; get; }

        public string Description { set; get; }

        [Required]
        public long StartTime { set; get; }

        [Required]
        public long EndTime { set; get; }

        public string OtherInfo { set; get; }
    }

    public class UpdateTaskValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskValidator()
        {
        }
    }
}