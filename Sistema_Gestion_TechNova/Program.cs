using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Sistema_Gestion_TechNova.Data;
using Sistema_Gestion_TechNova.Models;
using Sistema_Gestion_TechNova.Services;
using Microsoft.AspNetCore.Identity.MongoDB;

var builder = WebApplication.CreateBuilder(args);

// 1. Leer configuración MongoSettings desde appsettings.json
builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("MongoSettings")
);

var mongoSettings = builder.Configuration
    .GetSection("MongoSettings")
    .Get<MongoSettings>();

// 2. Registrar MongoClient
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    return new MongoClient(mongoSettings.ConnectionString);
});

// 3. Configurar Identity con MongoDB
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddMongoDbStores<ApplicationUser, ApplicationRole, string>(
        mongoSettings.ConnectionString,
        mongoSettings.DatabaseName
    )
    .AddDefaultTokenProviders();

// 4. Configurar servicios de la tienda
builder.Services.AddSingleton<MongoService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IVentaService, VentaService>();

// 5. MVC y Razor
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// MIDDLEWARE
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.MapRazorPages();

app.Run();
