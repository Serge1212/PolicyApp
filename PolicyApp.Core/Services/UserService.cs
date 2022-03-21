using PolicyApp.Core.DTOs.User;
using PolicyApp.Core.Models.Auth;
using PolicyApp.Core.Repositories.Interfaces;
using PolicyApp.Core.Services.Interfaces;

namespace PolicyApp.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public AuthResult AuthenticateUser(UserLoginDTO userLogin)
        {
            return _userRepository.AuthenticateUser(userLogin);
        }
    }
}
