using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arcstone.Demo.Application.Helpers;
using Arcstone.Demo.Application.Models.Others;
using FluentValidation;
using MediatR;

namespace Arcstone.Demo.Application.Domain.Task.Queries
{
    
    public class GetAllTaskQuery : IRequest<ResponseApi<PaginatedList<GetAllTaskQueryResponse>>>
    {
        [Required] public int PageIndex { get; set; } = 1;

        [Required] public int PageSize { get; set; } = 10;
    }

    public class GetAllTasValidator : AbstractValidator<GetAllTaskQuery>
    {
        public GetAllTasValidator()
        {
        }
    }
}
