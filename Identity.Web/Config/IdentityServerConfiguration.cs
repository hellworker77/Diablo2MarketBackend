using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace Identity.Web.Config;

public static class IdentityServerConfiguration
{
    public static IEnumerable<Client> GetClients() =>
        new List<Client>
        {
            new Client
            {
                ClientId = "Api",
                ClientName = "Api",
                ClientSecrets = { new Secret("client_secret".ToSha256())},
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowOfflineAccess = true,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "api",
                    "trading"
                }
            },
            new Client
            {
                ClientId = "m2m.client",
                ClientName = "Client Credentials Client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                AllowedScopes = { "trading" }
            }
        };
    public static IEnumerable<ApiScope> GetApiScopes() =>
        new List<ApiScope>
        {
            new ApiScope
            {
                Name = "api",
                DisplayName = "api",
                Enabled = true,
                UserClaims =
                {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.Address,
                    JwtClaimTypes.Confirmation,
                    JwtClaimTypes.EmailVerified,
                    JwtClaimTypes.Id,
                    JwtClaimTypes.Profile
                }
            },
            new ApiScope
            {
                Name = "trading",
                DisplayName = "trading",
                Enabled = true,
                UserClaims =
                {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.Address,
                    JwtClaimTypes.Confirmation,
                    JwtClaimTypes.EmailVerified,
                    JwtClaimTypes.Id,
                    JwtClaimTypes.Profile
                }
            }
        };

    public static IEnumerable<ApiResource> GetApiResources() =>
        new List<ApiResource>
        {
            new("api", "api") {Scopes = new List<string>{"api"}},
            new("trading", "trading") {Scopes = new List<string>{"trading"}}
        };

    public static IEnumerable<IdentityResource> GetIdentityResources() =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
        };
}