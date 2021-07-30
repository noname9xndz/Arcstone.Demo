using Arcstone.Demo.Application.Models.Others;
using FluentValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Arcstone.Demo.Application.Domain.Users.Queries
{
    public class GetTokenQuery : IRequest<ResponseApi<string>>
    {
        [Required] public string UserName { set; get; }

        [Required] public string Password { set; get; }
    }

    public class GetTokenQueryValidator : AbstractValidator<GetTokenQuery>
    {
        public GetTokenQueryValidator()
        {
        }
    }
}