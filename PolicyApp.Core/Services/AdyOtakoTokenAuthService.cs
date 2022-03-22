using PolicyApp.Core.Services.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PolicyApp.Core.Services {
  public class AdyOtakoTokenAuthService : IAdyOTakoTokenAuthService {

    public Task<bool> IsValidAsync(string token) {
      return Task.FromResult(true);
    }

    public Task<ClaimsPrincipal> GetPrincipalAsync(string token) {
      return Task.FromResult(new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, "hassan@gmail.com"),
            })));
    }
  }
}
