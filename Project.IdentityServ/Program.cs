using IdentityServer4.Services;
using IdentityServer4.Test;
using IdentityServer4.Validation;
using Microsoft.EntityFrameworkCore;
using Project.IdentityServ;
using Project.IdentityServ.Models;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProjectApiContext>(options =>
{
    options.UseSqlServer();
});

builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddInMemoryIdentityResources(Config.GetIdentityResources())
    .AddInMemoryApiResources(Config.GetAllApiResources())
    .AddInMemoryClients(Config.GetClients())
    .AddInMemoryApiScopes(Config.GetScope())
    .AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>()
    .AddProfileService<ProfileService>();




var app = builder.Build();



app.UseIdentityServer();
app.Run();
