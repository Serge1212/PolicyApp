using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PolicyApp.Core.Exceptions;
using PolicyApp.Core.Services.Interfaces;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace PolicyApp.Core.CustomTokenAuth
{
    public class AdyOtakoTokenAuthHandler : AuthenticationHandler<AdyOtakoAuthOptions> {

        private readonly IAdyOTakoTokenAuthService _adyOTakoTokenAuthService;

         public AdyOtakoTokenAuthHandler(
           IOptionsMonitor<AdyOtakoAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IAdyOTakoTokenAuthService adyOTakoTokenAuthService)
             : base(options, logger, encoder, clock)
        {
            _adyOTakoTokenAuthService = adyOTakoTokenAuthService;
        }
         
         
         protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
             {
                 if (!Request.Headers.TryGetValue("Authorization", out var value))
                     return AuthenticateResult.NoResult();
         
                 var authPair = value.ToString().Split(" ").ToList();
         
                 var scheme = Options.Scheme;
                 if (authPair.Count != 2)
                     return Fail("invalid_format", $"Authorization must be formatted as '{scheme} <token>'");
         
                 if (authPair[0] != Scheme.Name)
                     return Fail("invalid_scheme", $"Scheme must be {scheme}");
         
                 if (authPair[1].Length != Options.TokenLength)
                     return Fail("invalid_token_length", $"Access Token`s length must be {Options.TokenLength}");
                 
                 var accessToken = authPair[1];
                 try
                 {
                     if (!await _adyOTakoTokenAuthService.IsValidAsync(accessToken))
                         return Fail("invalid_token", $"{scheme} Token({accessToken}) is invalid");
                 }
                 catch (AuthenticationFailException e)
                 {
                     return AuthenticateResult.Fail(e);
                 }
         
                 var claimsPrincipal = await _adyOTakoTokenAuthService.GetPrincipalAsync(accessToken);
                 var wrapPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claimsPrincipal.Claims, Options.AuthenticationType ?? scheme));
                 return AuthenticateResult.Success(new AuthenticationTicket(wrapPrincipal, Scheme.Name));
           }
         
         private static AuthenticateResult Fail(string error, string description)
          {
              return AuthenticateResult.Fail(new AuthenticationFailException(error, description));
          }
    }
  }

