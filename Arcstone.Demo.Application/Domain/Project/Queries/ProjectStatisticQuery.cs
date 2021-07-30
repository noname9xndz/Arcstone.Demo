using System.ComponentModel.DataAnnotations;
using Arcstone.Demo.Application.Helpers;
using Arcstone.Demo.Application.Models.Others;
using FluentValidation;
using MediatR;

namespace Arcstone.Demo.Application.Domain.Project.Queries
{

    public class ProjectStatisticQuery : IRequest<ResponseApi<PaginatedList<GetAllProjectQueryResponse>>>
    {
        [Required]
        public int PageIndex { set; get; } = 1;

        [Required]
        public int PageSize { set; get; } = 10;

        [Required]
        public long StartTime { set; get; }

        [Required]

        public long EndTime { set; get; }
    }

    public class ProjectStatisticValidator : AbstractValidator<ProjectStatisticQuery>
    {
        public ProjectStatisticValidator()
        {
        }
    }
}
