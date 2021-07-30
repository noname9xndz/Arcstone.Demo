using Arcstone.Demo.Application.Models.Others;
using FluentValidation;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Arcstone.Demo.Application.Domain.Project.Queries
{
    public class GetProjectInfoQuery : IRequest<ResponseApi<GetProjectInfoQueryResponse>>
    {
        [Required]
        public Guid ProjectId { get; set; }
    }

    public class GetProjectInfoValidator : AbstractValidator<GetProjectInfoQuery>
    {
        public GetProjectInfoValidator()
        {
        }
    }
}