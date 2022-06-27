using BrowserChat.Client.Core.Services;
using BrowserChat.Client.Core.Session;
using BrowserChat.Client.Core.Util;

var builder = WebApplication.CreateBuilder(args);
ConfigurationHelper.Initialize(builder.Configuration);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<IRestAPIService, RestAPIService>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped(typeof(SessionManagement));

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.UseSession();

app.Run();