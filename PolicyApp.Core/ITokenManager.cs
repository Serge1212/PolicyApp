using PolicyApp.Core.Models;
using PolicyApp.Core.Models.Auth;

namespace PolicyApp.Core
{
    public interface ITokenManager
    {
        public AuthToken Generate(User user);
    }
}
