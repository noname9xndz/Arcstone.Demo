using System.Threading.Tasks;

namespace Arcstone.Demo.Application.Fake
{
    public interface IUserService
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<string> GetJwtToken(string userName, string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserModel GetUserById(string id);
    }
}