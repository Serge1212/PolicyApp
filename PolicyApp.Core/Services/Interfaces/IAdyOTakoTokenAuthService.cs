using System.Security.Claims;
using System.Threading.Tasks;

namespace PolicyApp.Core.Services.Interfaces {
  public interface IAdyOTakoTokenAuthService {
        Task<bool> IsValidAsync(string token);

        Task<ClaimsPrincipal> GetPrincipalAsync(string token);
  }
}
