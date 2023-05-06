using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectApi.Data.Models.Data;
using ProjectApi.Data.Models.IRepositories;
using ProjectApi.Data.Models.Repositories;
using ProjectApi.Data.Models.Services;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

#region Serilog Configs

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);



#endregion

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Identity Server 4 Configs for api source project

builder.Services.AddAuthentication("Bearer")
    .AddIdentityServerAuthentication(options =>
    {
        options.Authority = "http://localhost:5000";
        options.RequireHttpsMetadata = false;
        //options.ApiName = "projectApi";
        options.LegacyAudienceValidation = true;
    });

#endregion



builder.Services.AddDbContext<ProjectApiContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionString:ProjectApiConnectionString"]);
});
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonServices, PersonServices>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();
