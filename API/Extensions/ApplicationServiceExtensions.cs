using API.Data;
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
            //The reason for creating Interfaces is testing. Mock the interface easily. 
            services.AddScoped<ITokenService, TokenService>();

            //Lambda expressions, pass an expression as a parameter. 
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}