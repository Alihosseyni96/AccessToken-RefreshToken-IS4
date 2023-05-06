using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectApi.Data.Models.Services;
using System.IdentityModel.Tokens.Jwt;

namespace Project.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class PersonController : ControllerBase
    {
        private readonly IPersonServices _personService;

        public PersonController(IPersonServices personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPersonDetails()
        {

            //to get claims from token ( should place into middleware or actionFilter )
            string tokenStr = HttpContext.Request.Headers["Authorization"];
            var token = tokenStr.Split(' ')[1];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;
            var userId = tokenS.Claims.First(claim => claim.Type == "UserId").Value;
            var nationalCode = tokenS.Claims.First(claim => claim.Type == "NationalCode").Value;
            //Get It From Items


            var res = await _personService.GetPersonDetials(nationalCode);
            return Ok(res);
        }
    }
}
