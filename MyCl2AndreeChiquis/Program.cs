using MyCl2AndreeChiquis.DAO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddScoped<VentasDAO>();

// 2.- agregar el tiempo maximo de las variables de session
builder.Services.AddSession(
    tiempo => tiempo.IdleTimeout = TimeSpan.FromMinutes(20)
    );

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "Ventas",
    pattern: "{controller=Ventas}/{action=listarArticulos}/{id?}");



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
