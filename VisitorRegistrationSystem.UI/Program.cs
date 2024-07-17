using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TurkishId.ModelBinder;
using VisitorRegistrationSystem.Domain.Entitiy;
using VisitorRegistrationSystem.Repository.Concrete;
using VisitorRegistrationSystem.Repository.Context;
using VisitorRegistrationSystem.Repository.IRepository;
using VisitorRegistrationSystem.Services.AutoMapper.Profiles;
using VisitorRegistrationSystem.Services.IServices;
using VisitorRegistrationSystem.Services.Services;

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<VisitorDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                   .LogTo(Console.WriteLine)
                   .EnableSensitiveDataLogging());

        builder.Services.AddIdentity<User, Role>(options =>
        {
            // User password options
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 5;
            options.Password.RequiredUniqueChars = 0;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;

            // User username and email options
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+$";
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<VisitorDbContext>()
        .AddDefaultTokenProviders();

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IDepartmentService, DepartmentManager>();
        builder.Services.AddScoped<IVisitorService, VisitorManager>();

builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    })
    .AddMvcOptions(options =>
    {
        options.ModelBinderProviders.Insert(0, new TurkishIdModelBinderProvider());
    });


builder.Services.AddSession();
        builder.Services.AddAutoMapper(typeof(DepartmentProfile), typeof(UserProfile), typeof(VisitorProfile));
        //builder.Services.LoadMyServices();

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = new PathString("/User/Login");
            options.LogoutPath = new PathString("/User/Logout");
            options.Cookie = new CookieBuilder
            {
                Name = "VisitorRegistrationSystem",
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                SecurePolicy = CookieSecurePolicy.SameAsRequest
            };
            options.SlidingExpiration = true;
            options.ExpireTimeSpan = TimeSpan.FromDays(1);
            options.AccessDeniedPath = new PathString("/User/AccessDenied");
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseSession();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
