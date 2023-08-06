using Microsoft.AspNetCore.Authentication.Cookies;
using Report.Application;
using Report.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
// builder.Services.AddApplication(builder.Services.AddApplication(
// builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException(),
// builder.Configuration.GetSection("AppSettings:Token").Value!););dsdaf
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.WebHost.ConfigureKestrel(s =>
{
    s.Listen(IPAddress.Any, 80);
    s.Limits.MaxRequestBodySize = null;
    s.Limits.MaxRequestBufferSize = null;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
        options.LoginPath = "/auth/index";
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// app.MapRazorPages();
app.MapDefaultControllerRoute();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();