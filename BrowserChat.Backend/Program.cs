using BrowserChat.Backend.Core;
using BrowserChat.Backend.Core.Data;
using BrowserChat.Backend.Core.Util;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
bool isProduction = builder.Environment.IsProduction();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BrowserChatDbContext>(opt =>
{
    if (isProduction)
    {
        opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConn"));
    }
    else
    {
        opt.UseInMemoryDatabase("InMem");
    }
});

builder.Services.AddScoped<IBrowserChatRepository, BrowserChatRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

ConfigurationHelper.Initialize(builder.Configuration);
Persistence.PrepPopulation(app, isProduction);

app.Run();
