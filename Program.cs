using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;
using WebApplication3.Middleware;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add services to the container.
builder.Services.AddControllersWithViews();



builder.Services.AddSession();



    
var app = builder.Build();

app.UseSession();
app.UseRouting();


app.UseMiddleware<RoleBasedAccessMiddleware>();

app.UseMiddleware<WebApplication3.Middleware.SessionCheckMiddleware>();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();



app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
