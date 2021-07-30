using Arcstone.Demo.Application.Models.Others;
using FluentValidation;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Arcstone.Demo.Application.Domain.Task.Queries
{
    public class GetTaskInfoQuery : IRequest<ResponseApi<GetTaskInfoQueryResponse>>
    {
        [Required]
        public Guid TaskId { get; set; }
    }

    public class GetTaskInfoValidator : AbstractValidator<GetTaskInfoQuery>
    {
        public GetTaskInfoValidator()
        {
        }
    }
}