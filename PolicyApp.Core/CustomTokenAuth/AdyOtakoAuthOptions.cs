using Microsoft.AspNetCore.Authentication;

namespace PolicyApp.Core.CustomTokenAuth {
  public class AdyOtakoAuthOptions : AuthenticationSchemeOptions {

    public const string DefaultSchemeName = "AdyOtakoAuthenticationScheme";
    public string TokenHeaderName { get; set; } = "X-ADY-OTAKO-TOKEN";

    /// <summary>
    ///     Scheme of token.
    /// </summary>
    public string Scheme { get; set; }

    /// <summary>
    ///     The value is used by ClaimsIdentity.AuthenticationType. Scheme property value will be used if this value is null.
    /// </summary>
    public string AuthenticationType { get; set; }
    
    public string Realm { get; set; }

    /// <summary>
    ///     Length of custom token.
    /// </summary>
    public int TokenLength { get; set; }
  }
}
