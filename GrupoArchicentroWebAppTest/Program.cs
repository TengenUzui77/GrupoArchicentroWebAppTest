using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GrupoArchicentroWebAppTest.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<GrupoArchicentroWebAppTestContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GrupoArchicentroWebAppTestContext") ?? throw new InvalidOperationException("Connection string 'GrupoArchicentroWebAppTestContext' not found.")));



// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// crea la bd si no existe y agrega las migraciones.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GrupoArchicentroWebAppTestContext>();
    db.Database.Migrate();
}
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
