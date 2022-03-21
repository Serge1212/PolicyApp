using PolicyApp.Core.Models;
using PolicyApp.Core.Models.Auth;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PolicyApp.Core
{
    public class TokenManager : ITokenManager
    {
        public AuthToken Generate(User user)
        {
            List<Claim> claims = new List<Claim>() {
                new Claim (JwtRegisteredClaimNames.Email, user.Email),
                new Claim (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim (ClaimTypes.Role, user.Role)
            };

            JwtSecurityToken token = new TokenBuilder()
                .AddAudience(TokenConstants.Audience)
                .AddIssuer(TokenConstants.Issuer)
                .AddExpiry(TokenConstants.ExpiryInMinutes)
                .AddKey(TokenConstants.Key)
                .AddClaims(claims)
                .Build();

            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthToken()
            {
                AccessToken = accessToken,
                ExpiresIn = TokenConstants.ExpiryInMinutes
            };
        }
    }
}
