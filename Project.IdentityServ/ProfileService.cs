using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.EntityFrameworkCore;
using Project.IdentityServ;
using Project.IdentityServ.Models;
using System.Security.Claims;

public class ProfileService : IProfileService
{
    private readonly ProjectApiContext _db;


    public ProfileService(ProjectApiContext db)
    {
        _db = db;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {

        var user = await _db.People.Where(x => x.Id == long.Parse(context.Subject.GetSubjectId())).SingleAsync();


        List < Claim> claims = new List<Claim>()
        {
            new Claim("UserId" ,  user.Id.ToString()),
            new Claim("NationalCode" , user.NationalCode)
            
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