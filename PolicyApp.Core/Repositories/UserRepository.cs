using PolicyApp.Core.DTOs.User;
using PolicyApp.Core.Models;
using PolicyApp.Core.Models.Auth;
using PolicyApp.Core.Repositories.Interfaces;
using PolicyApp.Core.StaticData;
using System.Linq;

namespace PolicyApp.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ITokenManager _tokenManager;

        public UserRepository(ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }
        public AuthResult AuthenticateUser(UserLoginDTO userLogin)
        {
            var user = ReaderStore.Users
                .FirstOrDefault(x => x.Email.ToLower() == userLogin.Email.ToLower() &&
                                     x.Password == userLogin.Password);

            if (user != null)
            {
                return new AuthResult
                {
                    IsSuccess = true,
                    Token = _tokenManager.Generate(user)
                };
            }

            return new AuthResult { IsSuccess = false };
        }
    }
}
