using Arcstone.Demo.Application.Models.Others;
using FluentValidation;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using Arcstone.Demo.Application.Helpers;

namespace Arcstone.Demo.Application.Domain.Task.Queries
{
    public class GetAllTaskByProjectIdQuery : IRequest<ResponseApi<PaginatedList<GetAllTaskByProjectIdQueryResponse>>>
    {
        [Required]
        public Guid ProjectId { get; set; }

        [Required] public int PageIndex { get; set; } = 1;

        [Required] public int PageSize { get; set; } = 10;
    }

    public class GetAllTaskByProjectIdValidator : AbstractValidator<GetAllTaskByProjectIdQuery>
    {
        public GetAllTaskByProjectIdValidator()
        {
        }
    }
}