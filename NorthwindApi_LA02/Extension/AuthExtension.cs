using Microsoft.AspNetCore.Authentication;
using NorthwindApi_LA02.Auth;

namespace NorthwindApi_LA02.Extension
{
    public static class AuthExtension
    {
        public static AuthenticationBuilder AddApiKeySupport(this AuthenticationBuilder builder,
            Action<ApiKeyAuthenticationOptions> option)
        {
            return builder.AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(
                ApiKeyAuthenticationOptions.DefaultScheme, option
                );
        
        }
    }
}
