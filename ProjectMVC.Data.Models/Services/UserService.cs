using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
//using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectMVC.Data.Models.Models;
using ProjectMVC.Data.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMVC.Data.Models.Services
{
    public class UserService : IUserService
    {
        private readonly ProjectMvcContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(ProjectMvcContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _client = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
        }

        private readonly HttpClient _client;
        private readonly string AuthUrl = "http://localhost:5000/connect/token";
        private readonly string ClientId = "Client3";
        private readonly string Secret = "Secret";
        public async Task<ResultViewModel> UserSignIn(string nationalCode)
        {
            var client = new HttpClient();
            var accessToken = _httpContextAccessor.HttpContext.Request.Cookies["access_token"];
            var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies["refresh_token"];
            if (string.IsNullOrEmpty(accessToken))
            {
                var result = await client.PostAsync("http://localhost:5000/connect/token",
                     new FormUrlEncodedContent(new[]
                     {
                    new KeyValuePair<string, string>("client_id",ClientId),
                    new KeyValuePair<string, string>("client_secret",Secret),
                    new KeyValuePair<string, string>("grant_type","password"),
                    new KeyValuePair<string, string>("scope","offline_access"),
                    new KeyValuePair<string, string>("username",nationalCode),


                      }));

                var responseBody = await result.Content.ReadAsStringAsync();
                // response would be a JSON, just extract token from it
                accessToken = (string)JToken.Parse(responseBody)["access_token"];
                refreshToken = (string)JToken.Parse(responseBody)["refresh_token"];

                var CookieOption = new CookieOptions()
                {
                    Path = "/",
                    HttpOnly = false,
                    Secure = false

                    //if httpOnlu = false => java script cant get coocki with js command
                };
                _httpContextAccessor.HttpContext.Response.Cookies.Append("access_token", accessToken, CookieOption);
                _httpContextAccessor.HttpContext.Response.Cookies.Append("refresh_token", refreshToken, CookieOption);



            }

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(accessToken);
            //when access token will be expired (base utc time)
            var accessTokenExpireDate = jwtSecurityToken.ValidTo;

            //when access token created (base utc time)
            var accessTokenGeneratedDate = jwtSecurityToken.ValidFrom;

            if (accessTokenExpireDate < DateTime.UtcNow)
            {
                // if accessToken Goes Expired
                var getAccessTokenWithRefreshToken = await client.PostAsync("http://localhost:5000/connect/token",
                     new FormUrlEncodedContent(new[]
                     {
                    new KeyValuePair<string, string>("client_id",ClientId),
                    new KeyValuePair<string, string>("client_secret",Secret),
                    new KeyValuePair<string, string>("grant_type","refresh_token"),
                    new KeyValuePair<string, string>("refresh_token",refreshToken),
                    //new KeyValuePair<string, string>("username",nationalCode),
                      }));

                var responseBody = await getAccessTokenWithRefreshToken.Content.ReadAsStringAsync();

                accessToken = (string)JToken.Parse(responseBody)["access_token"];

                var CookieOption = new CookieOptions()
                {
                    Path = "/",
                    HttpOnly = false,
                    Secure = false

                    //if httpOnlu = false => java script cant get coocki with js command
                };

                _httpContextAccessor.HttpContext.Response.Cookies.Append("access_token", accessToken, CookieOption);





            }






            var url = "https://localhost:7211/api/Person/GetPersonDetails";
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var response = await client.GetStringAsync(url);





            var personInfo = JsonConvert.DeserializeObject<UserInfoViewModel>(response);


            var userInfo = _context.Users.Where(x => x.NationalCode == nationalCode).Include(x => x.Travels).Single().Travels.ToList();

            var travelViewModel = new List<TravelViewModel>();
            foreach (var item in userInfo)
            {
                var travelModel = new TravelViewModel()
                {
                    DepartedDate = item.DepartedDate,
                    Destenition = item.Destenition,
                    UserId = item.UserId,
                    Origin = item.Origin,
                    Id = item.Id,
                    ReturnDate = item.ReturnDate
                };
                travelViewModel.Add(travelModel);
            }

            



            return new ResultViewModel()
            {
                Travels = travelViewModel,
                UserInfo = personInfo
            };
        }
    }
}
