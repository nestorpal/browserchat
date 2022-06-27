using BrowserChat.Security.Core.Application;
using BrowserChat.Security.Core.Data;
using BrowserChat.Security.Core.Entities;
using BrowserChat.Security.Core.JWTLogic;
using BrowserChat.Security.Core.Util;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
bool isProduction = builder.Environment.IsProduction();
ConfigurationHelper.Initialize(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* Identity Db Context */
/**************/
builder.Services.AddDbContext<SecurityContext>(opt =>
{
    if (isProduction)
    {
        opt.UseSqlServer(ConfigurationHelper.DbConnection);
    }
    else
    {
        opt.UseInMemoryDatabase("InMem");
    }
});

var secBuilder = builder.Services.AddIdentityCore<User>();
var identityBuilder = new IdentityBuilder(secBuilder.UserType, secBuilder.Services);
identityBuilder.AddEntityFrameworkStores<SecurityContext>();
identityBuilder.AddSignInManager<SignInManager<User>>();
builder.Services.AddSingleton<ISystemClock, SystemClock>();
/**************/

/* JWT */
/**************/
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(ConfigurationHelper.JWTKey)),
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddSingleton<IJWTGenerator, JWTGenerator>();
/**************/

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(typeof(Login.UsuarioLoginCommand).Assembly);

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

await Persistence.PrepPopulation(app, isProduction);

app.Run();
