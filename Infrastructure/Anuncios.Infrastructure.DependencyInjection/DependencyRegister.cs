using Anuncios.Domain.Handlers;
using Anuncios.Infrastructure.SqlServer.Read.DbConnections;
using Anuncios.Infrastructure.SqlServer.Write.DbContexts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Domain.Repositories;

namespace Anuncios.Infrastructure.DependencyInjection
{
    public static class DependencyRegister
    {
        public static IServiceCollection AddAnunciosDI(this IServiceCollection services, IConfiguration configuration)
        {
            DependencyRegister.RegisterServices(services);
            DependencyRegister.RegisterRepositories(services, configuration);
            return services;
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<AnuncioHandler>();
        }

        private static void RegisterRepositories(IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(c => new AnunciosDbContext(configuration.GetSection("ConnectionStrings")["AnunciosConnection"]));
            services.AddScoped(c => new AnunciosDbConnection(configuration.GetSection("ConnectionStrings")["AnunciosConnection"]));
            services.AddScoped<Domain.Repositories.Write.IAnuncioRepository, SqlServer.Write.Repositories.AnuncioRepository>();
            services.AddScoped<Domain.Repositories.Read.IAnuncioRepository, SqlServer.Read.Repositories.AnuncioRepository>();
        }
    }
}
