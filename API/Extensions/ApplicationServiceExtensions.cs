using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    //Extension method needs to be static
    public static class ApplicationServiceExtensions
    {
        //Always specify this before the type that you are extending. 
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            //The reason for creating Interfaces is testing. Mock the interface easily. 
            services.AddScoped<ITokenService, TokenService>();
            //Photo Service
            services.AddScoped<IPhotoService, PhotoService>();
            //Add User Repository its now available to our user controller
            services.AddScoped<IUserRepository, UserRepository>();

            //add automapper as a service
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            //Lambda expressions, pass an expression as a parameter. 
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}