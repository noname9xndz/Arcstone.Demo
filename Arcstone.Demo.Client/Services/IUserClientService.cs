using Arcstone.Demo.Client.Models;
using System.Threading.Tasks;

namespace Arcstone.Demo.Client.Services
{
    public interface IUserClientService
    {
        Task<ResponseApi<string>> GetToken(LoginModel request);
    }
}