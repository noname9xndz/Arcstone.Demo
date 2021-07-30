using Arcstone.Demo.Application.Domain.Users.Queries;
using Arcstone.Demo.Application.Fake;
using Arcstone.Demo.Application.Helpers;
using Arcstone.Demo.Application.Models.Others;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Arcstone.Demo.Application.Domain.Users
{
    public class UserQueryHandler : IRequestHandler<GetTokenQuery, ResponseApi<string>>
    {
        private readonly IUserService _userService;

        public UserQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ResponseApi<string>> Handle(GetTokenQuery request, CancellationToken cancellationToken)
        {
            ResponseApi<string> response = new ResponseApi<string>();
            var token = await _userService.GetJwtToken(request.UserName, request.Password);
            if (!token.IsNullOrWhiteSpaceCustom())
            {
                response.Result = token;
            }
            else
            {
                response.ErrorMessage.Add("Has error.");
            }

            return response;
        }
    }
}