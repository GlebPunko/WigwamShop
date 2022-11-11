using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace IdentityServerAPI.IdentityServer
{
    public class IdentityConfiguration
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() => new List<IdentityResource>()
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

        public static List<TestUser> GetTestUsers() => new List<TestUser>()
        {
            new TestUser()
            {
                SubjectId = "1",
                Username = "Hleb",
                Password = "string111",
                Claims = new List<Claim>()
                {
                    new Claim(JwtClaimTypes.GivenName, "Hleb"),
                    new Claim(JwtClaimTypes.FamilyName, "Punko")
                }
            },
            new TestUser()
            {
                SubjectId = "2",
                Username = "Test",
                Password = "Test1Test1",
                Claims = new List<Claim>()
                {
                    new Claim(JwtClaimTypes.GivenName, "Test"),
                    new Claim(JwtClaimTypes.FamilyName, "Test")
                }
            }
        };

        public static IEnumerable<Client> GetClients() => new[]
        {
            new Client
            {
                ClientId = "Api",
                ClientName = "ClientApi",
                AllowAccessTokensViaBrowser = true,
                ClientSecrets = new [] { new Secret("wigwamshopsecret".Sha512()) },
                AllowedGrantTypes =
                {
                    GrantType.ClientCredentials,
                    GrantType.ResourceOwnerPassword
                },
                AllowedScopes = { "BasketAPI", "CatalogAPI" },
                AllowOfflineAccess = true,
            },
            new Client
            {
                ClientId = "TestClient",
                ClientSecrets = new [] { new Secret("testclientsecret".Sha512()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "CatalogAPI" }
            }
        };

        public static IEnumerable<ApiResource> GetApiResources() => new List<ApiResource>()
        {
            new ApiResource("CatalogAPI", "CatalogAPI") { Scopes = { "CatalogAPI" } },
            new ApiResource("BasketAPI", "BasketAPI")
        };

        public static IEnumerable<ApiScope> GetApiScopes() => new List<ApiScope>()
        {
            new ApiScope("CatalogAPI", "CatalogAPI"),
            new ApiScope("BasketAPI", "BasketAPI")
        };
    }
}
