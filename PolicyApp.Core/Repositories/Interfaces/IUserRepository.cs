using PolicyApp.Core.DTOs.User;
using PolicyApp.Core.Models.Auth;

namespace PolicyApp.Core.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public AuthResult AuthenticateUser(UserLoginDTO userLogin);
    }
}
