using AspDotNet9ApiSample.Data;
using AspDotNet9ApiSample.Interfaces;
using AspDotNet9ApiSample.Services;
using Microsoft.EntityFrameworkCore;

namespace AspDotNet9ApiSample.Configuration
{
    public static class Injection
    {
        public static IServiceCollection RegisterInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddDbContextPool<CarClinchDbContext>(options =>
              options.UseSqlServer(
                configuration.GetConnectionString("CarClinchDbContext") ??
                throw new InvalidOperationException("Connection string 'CarClinchDbContext' not found."),
                sqlServerOptions => {
                    sqlServerOptions.EnableRetryOnFailure();
                }
            ));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddScoped<ICarService, CarService>();

            return services;
        }
    }
}
