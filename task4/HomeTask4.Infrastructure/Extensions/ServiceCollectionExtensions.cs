using HomeTask4.Infrastructure.Data;
using HomeTask4.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HomeTask4.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<RecipeBookDbContext>(opts => opts.UseSqlServer(connectionString));
            services.AddScoped<IRepository, EfRepository>(sp => new EfRepository(sp.GetRequiredService<RecipeBookDbContext>()));
            services.AddScoped<IUnitOfWork, UnitOfWork>(sp => new UnitOfWork(
                sp.GetRequiredService<RecipeBookDbContext>(),
                sp.GetRequiredService<IRepository>()));

            return services;
        }
    }
}
