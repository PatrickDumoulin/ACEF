using CoreLib.Injection;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ninject.Modules;
using WebApp.Core.Configuration;
using WebApp.Core.Mappings;
using WebApp.Data;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Initialisation de Ninject AVANT l'ajout des services
List<INinjectModule> ninjectModules = new List<INinjectModule>()
{
    new ProdBinder(),
    // Ajoutez vos modules Ninject ici
};
Injector.InitializeKernel(ninjectModules);

// Ajout des services MVC
builder.Services.AddControllersWithViews();

// Exemple d'enregistrement dans Program.cs
//builder.Services.AddTransient<RoleService>();

// Vérification de la chaîne de connexion
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("La chaîne de connexion 'DefaultConnection' est introuvable ou vide.");
}

// Configuration de la chaîne de connexion pour le DbContext Projet_CL
builder.Services.AddDbContext<Projet_CLContext>(options =>
    options.UseSqlServer(connectionString));

// Configuration de la chaîne de connexion pour le DbContext Identity
builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(connectionString));

// Ajout des services d'identité
builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>();

// Configuration du chemin d'accès refusé
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Account/AccessDenied";
});

var app = builder.Build();

// Configuration du pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}


app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseStatusCodePages(async context =>
{
    if (context.HttpContext.Response.StatusCode == 403)
    {
        context.HttpContext.Response.Redirect("/Account/AccessDenied");
    }
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Si vous utilisez les pages Razor d'ASP.NET Identity

ConfigurationHelper.Initialize(app.Configuration);

// Appeler la méthode pour initialiser les rôles
await SeedRolesAndAdminUser(app);

app.Run();

// Méthode pour initialiser les rôles et un utilisateur administrateur
async Task SeedRolesAndAdminUser(IApplicationBuilder app)
{
    using (var serviceScope = app.ApplicationServices.CreateScope())
    {
        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        string[] roleNames = { "AdministrateurCA", "Intervenant", "Superutilisateur" };

        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (!roleResult.Succeeded)
                {
                    throw new Exception($"Erreur lors de la création du rôle {roleName} : {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                }
            }
        }

        var adminUser = new ApplicationUser
        {
            UserName = builder.Configuration["AdminUser:UserName"] ?? "admin@init.com",
            Email = builder.Configuration["AdminUser:Email"] ?? "admin@init.com",
            EmailConfirmed = true,
            FirstName = builder.Configuration["AdminUser:FirstName"] ?? "Admin",
            LastName = builder.Configuration["AdminUser:LastName"] ?? "User"
        };

        var user = await userManager.FindByEmailAsync(adminUser.Email);

        if (user == null)
        {
            var createPowerUser = await userManager.CreateAsync(adminUser, builder.Configuration["AdminUser:Password"] ?? "Admin@123");
            if (!createPowerUser.Succeeded)
            {
                throw new Exception($"Erreur lors de la création de l'utilisateur : {string.Join(", ", createPowerUser.Errors.Select(e => e.Description))}");
            }

            user = adminUser;
        }

        if (!await userManager.IsInRoleAsync(user, "Superutilisateur"))
        {
            var result = await userManager.AddToRoleAsync(user, "Superutilisateur");
            if (!result.Succeeded)
            {
                throw new Exception($"Erreur lors de l'assignation du rôle Superutilisateur : {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
    }
}
