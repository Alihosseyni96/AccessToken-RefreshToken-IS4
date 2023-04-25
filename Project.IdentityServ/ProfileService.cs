using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.EntityFrameworkCore;
using Project.IdentityServ;
using Project.IdentityServ.Models;
using System.Security.Claims;

public class ProfileService : IProfileService
{
    private readonly IdentityServerContext _db;


    public ProfileService(IdentityServerContext db)
    {
        _db = db;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {

        var user = await _db.Users.Where(x => x.Id == long.Parse(context.Subject.GetSubjectId())).Include(x => x.UserRoles).ThenInclude(x => x.Role).SingleAsync();

        var roles = user.UserRoles.Select(x=> x.Role.Name);
        string[] rolesArray = new string[roles.Count()];
        int i = 0;
        if (roles.Count()> 0)
        {
            foreach (var role in roles)
            {
                rolesArray[i] = role;
                i++;
            }
        }

        List < Claim> claims = new List<Claim>()
        {
            new Claim("UserId" ,  user.Id.ToString()),
            new Claim("UserName" , user.UserName),
            new Claim("Roles" , string.Join("-" , rolesArray))
            
        };
        context.IssuedClaims.AddRange(claims);
        return;



    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        //check if user is active or not here 
        return Task.FromResult(0);
    }
}