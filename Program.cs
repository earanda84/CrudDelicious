// 1.- Conexión a base de datos
using Microsoft.EntityFrameworkCore;

// 2.- Modelos
using CrudDelicious.Models;

var builder = WebApplication.CreateBuilder(args);

// 3.-Session
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

// 4.-String conexión base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 5.- Constructor conexión base de datos
builder.Services.AddDbContext<MyContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});


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

// 6.- Ussing Session
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
