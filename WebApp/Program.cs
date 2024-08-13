using CoreLib.Injection;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ninject.Modules;
using System.Configuration;
using WebApp.Core.Configuration;

//TEST
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Client}/{action=Index}/{id?}");

ConfigurationHelper.Initialize(app.Configuration);

List<INinjectModule> list = new List<INinjectModule>();
list.Add(new WebApp.Core.Mappings.ProdBinder());
Injector.InitializeKernel(list);

app.Run();
