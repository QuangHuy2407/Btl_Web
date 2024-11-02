using FASTFOOD.Models;
using FASTFOOD.Responsitory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = "Data Source=PHONG\\PHONG;Initial Catalog=QLBanDoAn;Integrated Security=True;";
builder.Services.AddDbContext<QLBanDoAnContext>(x=>x.UseSqlServer(connectionString));
builder.Services.AddScoped<ILoaiMonResponsitory, TenLoaiMonAnResponsitory>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
var app = builder.Build();

builder.Services.AddSession();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();


app.MapControllerRoute(
   name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapControllerRoute(
// name: "default",
// pattern: "{controller=Access}/{action=Login}/{id?}");
app.Run();

