using System.Net;
using Report.Application;
using Report.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.WebHost.ConfigureKestrel(s =>
{
    s.Listen(IPAddress.Any, 80);
    s.Limits.MaxRequestBodySize =null;
    s.Limits.MaxRequestBufferSize = null;
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();