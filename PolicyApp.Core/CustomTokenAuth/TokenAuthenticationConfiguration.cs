
namespace PolicyApp.Core.CustomTokenAuth
{
    public class TokenAuthenticationConfiguration
    {
        public string AuthenticationType { get; set; }

        public string Realm { get; set; }

        public int TokenLength { get; set; }
    }
}
