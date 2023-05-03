using System.Security.Authentication;
using Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;
using Persistence.Context;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Configuration.AddJsonFile("appsettings.json"
                                , false
                                , true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true);
builder.Configuration.AddEnvironmentVariables();

// //Log
// builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
// builder.Logging.AddLog4Net("log4net.config");

if (builder.Environment.IsDevelopment())
{
    builder.Logging.AddConsole();
    builder.Logging.AddDebug();
}

builder.WebHost.ConfigureKestrel(adapterOptions =>
{
    adapterOptions.ConfigureHttpsDefaults(listenOptions => { listenOptions.SslProtocols = SslProtocols.Tls13 | SslProtocols.Tls12; });
});
builder.WebHost.UseIISIntegration();
builder.WebHost.CaptureStartupErrors(true);

//services
IConfiguration configuration = builder.Configuration;

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // prevent access from javascript 
    options.HttpOnly = HttpOnlyPolicy.Always;

    // If the URI that provides the cookie is HTTPS, 
    // cookie will be sent ONLY for HTTPS requests 
    // (refer mozilla docs for details) 
    options.Secure = CookieSecurePolicy.SameAsRequest;

    // refer "SameSite cookies" on mozilla website 
    options.MinimumSameSitePolicy = SameSiteMode.None;
    options.CheckConsentNeeded    = _ => true;
});

builder.Services.AddRazorPages();

var connectionString = configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDatabase(connectionString);
builder.Services.AddRepositories();
builder.Services.AddServices();

//builder.Services.AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseCookiePolicy();
//
// app.UseEndpoints(endpoints =>
// {
//     endpoints.MapControllerRoute(
//         name: "default",
//         pattern: "{controller=Home}/{action=Index}/{id?}");
// });

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EfCoreContext>();
    DbInitializer.Initialize(context);
}

app.Run();
