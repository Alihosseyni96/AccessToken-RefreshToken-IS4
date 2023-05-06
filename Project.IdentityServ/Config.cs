using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Security.Claims;
using static IdentityModel.OidcConstants;
using static IdentityServer4.Events.TokenIssuedSuccessEvent;

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
                new Client
                {
                    ClientId = "Client1",
                    AllowedGrantTypes = IdentityServer4.Models.GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = { new IdentityServer4.Models.Secret("Secret".Sha256()) },
                    AccessTokenLifetime = 3600, //a hour
                    AllowedScopes = {
                        "projectApi" },
                      AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    //AccessTokenType=AccessTokenType.Reference
                },
                new Client
                {
                    ClientId = "Client2",
                    AllowedGrantTypes = IdentityServer4.Models.GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new IdentityServer4.Models.Secret("Secret".Sha256())
                    },
                    AccessTokenLifetime = 900, //15min
                    AllowedScopes = {
                        "projectApi" },
                      AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                },

                //Refresh token with access token
                new Client
                {
                    ClientId = "Client3",
                    AllowedGrantTypes = IdentityServer4.Models.GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new IdentityServer4.Models.Secret("Secret".Sha256())
                    },
                    AccessTokenLifetime = 900, //15 min
                    //scope to get refresh token
                    AllowedScopes = {
                        "offline_access"  },
                      AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    // To :  when ask new accessToken , refreshToken Dont Changed
                    RefreshTokenUsage = TokenUsage.ReUse,
                    RefreshTokenExpiration  = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = 25920000,//1 Month,
                    UpdateAccessTokenClaimsOnRefresh= true,
                    AllowOfflineAccess = true
                    //Now when you request this you will get access token and refresh token 
                    //when your access token expires, you should call a new request like bellow with your refresh token to get new acees token
                    //your refresh token will be alive for 30 days and when ever your access token expired you will use this to get new access token


                          //POST /connect/token
 
                        //client_id=client&
                       //client_secret=secret&
                      //grant_type=refresh_token&
                     //refresh_token=hdh922



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
                },
                new ApiScope
                {
                    Name = "offline_access"
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


