using Microsoft.AspNetCore.Authorization;
using PolicyApp.Auth.Requirements;
using PolicyApp.Core.StaticData;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PolicyApp.Core.Requirements.Handlers
{
    public class ShouldBeAMetalFanAuthHandler
        : AuthorizationHandler<ShouldBeAMetalFanRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ShouldBeAMetalFanRequirement requirement)
        {
            if (!context.User.HasClaim(x => x.Type == ClaimTypes.Email))
            {
                return Task.CompletedTask;
            }

            var requiredRole = "Admin";

            var email = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            if(ReaderStore.Users.Any(
                x => x.Email == email &&
                x.IsMetalFan &&
                x.Role == requiredRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
