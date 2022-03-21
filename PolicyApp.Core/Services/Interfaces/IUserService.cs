using PolicyApp.Core.DTOs.User;
using PolicyApp.Core.Models;
using PolicyApp.Core.Models.Auth;

namespace PolicyApp.Core.Services.Interfaces
{
    public interface IUserService
    {
        public AuthResult AuthenticateUser(UserLoginDTO userLogin);
    }
}
