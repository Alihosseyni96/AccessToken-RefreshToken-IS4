using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.EntityFrameworkCore;
using Project.IdentityServ.Models;
using System.Security.Claims;

namespace Project.IdentityServ
{
    public class Config
    {




        public static IEnumerable<ApiResource> GetAllApiResources()
        {
            //here is our project as resources of api and we introduce them in Clients part
            return new List<ApiResource>
            {
                new ApiResource("projectApi" , "Customer Api  for projectApi")
            };
        }


        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                // client -creadential frant type
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AccessTokenLifetime = 3600, //a hour
                    AllowedScopes = { "projectApi" },
                      AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true
                },
                //Reasouce Owner Pssword grant type
                new Client
                {
                    ClientId = "Ro_client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AccessTokenLifetime = 3600, //a hour
                    AllowedScopes = { 
                        "projectApi" },
                      AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    //AccessTokenType=AccessTokenType.Reference
                }

            };
        }

        public static IEnumerable<ApiScope> GetScope()
        {
            return new List<ApiScope>
            {
                new ApiScope
                {
                    Name = "projectApi"
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
            };
        }
    }
}


