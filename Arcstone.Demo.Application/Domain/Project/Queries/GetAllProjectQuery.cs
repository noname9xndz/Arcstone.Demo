using System;
using Arcstone.Demo.Application.Helpers;
using Arcstone.Demo.Application.Models.Others;
using FluentValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;
using Noname.UnitOfWork.Lib.Extensions;

namespace Arcstone.Demo.Application.Domain.Project.Queries
{
    public class GetAllProjectQuery : IRequest<ResponseApi<PaginatedList<GetAllProjectQueryResponse>>>
    {
        public string Keyword { set; get; }

        [Required]
        public int PageIndex { set; get; } = 1;

        [Required]
        public int PageSize { set; get; } = 10;

        public long? StartTime { set; get; }
        public long? EndTime { set; get; }
    }

    public class GetAllProjectValidator : AbstractValidator<GetAllProjectQuery>
    {
        public GetAllProjectValidator()
        {
        }
    }
}