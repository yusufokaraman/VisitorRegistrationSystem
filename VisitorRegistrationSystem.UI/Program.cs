using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json.Serialization;
using VisitorRegistrationSystem.Repository.Context;
using VisitorRegistrationSystem.Services.AutoMapper.Profiles;
using VisitorRegistrationSystem.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<VisitorDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                   .LogTo(Console.WriteLine)
                   .EnableSensitiveDataLogging());

//builder.Services.AddDefaultIdentity<IdentityUser>()
//    .AddEntityFrameworkStores<VisitorDbContext>();
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;

});
builder.Services.AddSession();
builder.Services.AddAutoMapper(typeof(DepartmentProfile), typeof(UserProfile), typeof(VisitorProfile));
builder.Services.LoadMyServices();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/User/Login");
    options.LogoutPath = new PathString("/User/Logout");
    options.Cookie = new CookieBuilder
    {
        Name = "VisitorSystem",
        HttpOnly = true,
        SameSite = SameSiteMode.Strict,
        SecurePolicy = CookieSecurePolicy.SameAsRequest//Always

    };
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = System.TimeSpan.FromDays(1);
    options.AccessDeniedPath = new PathString("/User/AccessDenied");

});


var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
