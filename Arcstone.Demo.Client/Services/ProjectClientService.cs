using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Arcstone.Demo.Application.Domain.Project.Queries;
using Arcstone.Demo.Client.Models;
using Arcstone.Demo.Client.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Arcstone.Demo.Client.Services
{
    public class ProjectClientService :  BaseApiClient, IProjectClientService
    {
        public ProjectClientService(IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
            : base(httpClientFactory, configuration, httpContextAccessor)
        {
        }

        public async Task<ResponseApi<PaginatedList<GetAllProjectQueryResponse>>> GetProjectsPaging(GetAllProjectQuery request)
        {
            return await PostAsync<GetAllProjectQuery, ResponseApi<PaginatedList<GetAllProjectQueryResponse>>>($"/api/project/list", request);
        }
    }
}
