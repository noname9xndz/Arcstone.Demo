using Arcstone.Demo.Client.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace Arcstone.Demo.Client.Services
{
    public class UserClientService : BaseApiClient, IUserClientService
    {
        public UserClientService(IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory, configuration, httpContextAccessor)
        {
        }

        public async Task<ResponseApi<string>> GetToken(LoginModel request)
        {
            return await PostAsync<LoginModel, ResponseApi<string>>($"/api/user/login", request, false);
        }
    }
}