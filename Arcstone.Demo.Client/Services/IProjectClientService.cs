
using System.Threading.Tasks;
using Arcstone.Demo.Application.Domain.Project.Queries;
using Arcstone.Demo.Client.Models;
using Arcstone.Demo.Client.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace Arcstone.Demo.Client.Services
{
    public interface IProjectClientService
    {
        Task<ResponseApi<PaginatedList<GetAllProjectQueryResponse>>> GetProjectsPaging(GetAllProjectQuery request);
    }
}
