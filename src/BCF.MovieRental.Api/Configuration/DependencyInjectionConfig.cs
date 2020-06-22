using BCF.MovieRental.Business.Interfaces;
using BCF.MovieRental.Business.Notifications;
using BCF.MovieRental.Business.Services;
using BCF.MovieRental.Data.Context;
using BCF.MovieRental.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BCF.MovieRental.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MovieRentalDbContext>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();

            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IRentalService, RentalService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();            

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}