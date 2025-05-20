using Microsoft.EntityFrameworkCore;
using _netmvc.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using _netmvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MvcMovieContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MvcMovieContext") ?? throw new InvalidOperationException("Connection string 'MvcMovieContext' not found.")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, op =>
    {
        op.LoginPath = "/login";
        op.EventsType = typeof(CustomCookieAuthenticationEvents);
        op.AccessDeniedPath = "/fffffff";
    })

    ;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<CustomCookieAuthenticationEvents>();
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 10,
                QueueLimit = 0,
                Window = TimeSpan.FromSeconds(2)
            }));
});

builder.Services.AddSingleton<IDateTime, SystemDateTime>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Em", policy =>
          policy.RequireRole("A", "B", "C"));
});

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    Console.WriteLine(app.Environment);
    StaticWebAssetsLoader.UseStaticWebAssets(app.Environment, app.Configuration);

}

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseFileServer();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "StaticFiles")),
    RequestPath = "/sta"
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();



public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents

{
    public override Task SignedIn(CookieSignedInContext context)
    {
        context.Response.Cookies.Append("name", "huydkjask", new CookieOptions()
        {
            MaxAge = new TimeSpan(1, 1, 1)
        });
        return Task.CompletedTask;
    }
    public override Task SigningOut(CookieSigningOutContext context)
    {
        context.Response.Cookies.Append("name", "huydkjask SigningOut", new CookieOptions()
        {
            MaxAge = new TimeSpan(1, 1, 1)
        });
        return Task.CompletedTask;
    }

    public override Task CheckSlidingExpiration(CookieSlidingExpirationContext context)
    {

        return base.CheckSlidingExpiration(context);
    }

}


public class DocumentAuthorizationHandler :
    AuthorizationHandler<SameAuthorRequirement, Movie>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   SameAuthorRequirement requirement,
                                                   Movie resource)
    {
        if (context.User.Identity?.Name == resource.Title)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}

public class SameAuthorRequirement : IAuthorizationRequirement { }

public interface IDateTime
{
    DateTime Now { get; }
}
public class SystemDateTime : IDateTime
{
    public DateTime Now
    {
        get { return DateTime.Now; }
    }
}