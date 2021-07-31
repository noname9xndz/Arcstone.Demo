using Arcstone.Demo.Client.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Arcstone.Demo.Client.Heplers
{
    public static class ClaimHelper
    {
        public static ClaimsPrincipal GetPrincipalFromExpiredToken(this string token, ConfigAudience audience)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(audience.Secret)),
                ValidateIssuer = true,
                ValidIssuer = audience.Iss,
                ValidateAudience = true,
                ValidAudience = audience.Aud,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase)) return null;
            //throw new SecurityTokenException("Invalid token");
            return principal;
        }

        public static string GetJwtTokenFromClaim(this IEnumerable<Claim> claims)
        {
            return claims?.FirstOrDefault(x => x.Type.Equals("JwtToken", StringComparison.OrdinalIgnoreCase))?.Value;
        }

        public static string GetJwtTokenFromClaim(this IHttpContextAccessor httpContextAccessor)
        {
            var token = httpContextAccessor?.HttpContext?.User?.FindFirst("JwtToken")?.Value;
            if (!string.IsNullOrWhiteSpace(token))
            {
                return token;
            }

            return string.Empty;
        }
    }
}