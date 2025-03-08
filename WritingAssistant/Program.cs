using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WritingAssistant.Data;
using WritingAssistant.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.  
builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole<int>>()
   .AddEntityFrameworkStores<ApplicationDbContext>()
   .AddDefaultTokenProviders();


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Thêm xác thực
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
