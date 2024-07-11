using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using VisitorRegistrationSystem.Domain.Entitiy;
using VisitorRegistrationSystem.Repository.Concrete;
using VisitorRegistrationSystem.Repository.Context;
using VisitorRegistrationSystem.Repository.IRepository;
using VisitorRegistrationSystem.Services.IServices;
using VisitorRegistrationSystem.Services.Services;

namespace VisitorRegistrationSystem.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection)
        {
            //Servisler kaydedilir.
            serviceCollection.AddDbContext<VisitorDbContext>();
            serviceCollection.AddIdentity<User, Role>(options =>
            {
                //User password options
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

                //User username and email options
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+$";
                options.User.RequireUniqueEmail = true;




            }).AddEntityFrameworkStores<VisitorDbContext>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<IDepartmentService, DepartmentManager>();
            serviceCollection.AddScoped<IVisitorService, VisitorManager>();


            return serviceCollection;


        }
    }
}
