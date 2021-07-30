using Arcstone.Demo.Application.Helpers;
using Arcstone.Demo.Application.Models.Others;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Arcstone.Demo.Application.Fake
{
    public class UserService : IUserService
    {
        private List<UserModel> _users = new List<UserModel>()
        {
            new UserModel()
            {
                Id = Guid.Parse("3ea5bcb0-7f99-432f-8fca-d94fea455e62"),
                UserName = "manager@gmail.com",
                Password = "123123",
                Role = new List<string>(){Constants.Role.ManagerRole, Constants.Role.EmployeeRole}
            },
            new UserModel()
            {
                Id = Guid.Parse("a5a8e5d2-b6c2-49d9-a4d9-281a2e0d03eb"),
                UserName = "employee@gmail.com",
                Password = "123123",
                Role = new List<string>(){Constants.Role.EmployeeRole }
            }
        };

        private readonly ConfigAudience _config;

        public UserService(IOptions<ConfigAudience> options)
        {
            _config = options.Value;
        }

        public UserModel GetUserById(string id)
        {
            return _users.FirstOrDefault(x => x.Id == Guid.Parse(id));
        }

        public async Task<string> GetJwtToken(string userName, string password)
        {
            if (!userName.IsNullOrWhiteSpaceCustom() && !password.IsNullOrWhiteSpaceCustom())
            {
                var user = _users.FirstOrDefault(x => x.UserName.Equals(userName) && x.Password.Equals(password));
                if (user != null)
                {
                    string key = _config.Secret;
                    var issuer = _config.Iss;
                    var aud = _config.Aud;
                    var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                    var permClaims = new List<Claim>();
                    permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                    permClaims.Add(new Claim(Constants.ClaimType.UserName, user.UserName));
                    permClaims.Add(new Claim(Constants.ClaimType.UserId, user.Id.ToString()));
                    if (user.Role.Any())
                    {
                        foreach (var item in user.Role)
                        {
                            permClaims.Add(new Claim(Constants.ClaimType.Role, item));
                        }
                    }

                    JwtSecurityToken token = new JwtSecurityToken(
                        issuer,
                         aud,
                        permClaims,
                        expires: DateTime.Now.AddDays(1),
                        signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
                    var res = new JwtSecurityTokenHandler().WriteToken(token);
                    return await Task.FromResult(res);
                }
            }
            return string.Empty;
        }
    }
}