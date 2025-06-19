using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NoBolso.Domain.Interfaces.Repositories;
using NoBolso.Infrastructure.Data;
using NoBolso.Infrastructure.Repositories;
using NoBolso.Infrastructure.Services;
using NoBolso.Domain.Interfaces.Services;

namespace NoBolso.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuração do banco de dados
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var useInMemoryDatabase = configuration.GetValue<bool>("UseInMemoryDatabase");

            if (useInMemoryDatabase)
            {
                services.AddDbContext<NoBolsoDbContext>(options =>
                    options.UseInMemoryDatabase("NoBolsoInMemoryDb"));
            }
            else
            {
                var databaseProvider = configuration.GetValue<string>("DatabaseProvider");

                switch (databaseProvider?.ToLower())
                {
                    case "sqlite":
                        services.AddDbContext<NoBolsoDbContext>(options =>
                            options.UseSqlite(connectionString));
                        break;
                    case "postgresql":
                    default:
                        services.AddDbContext<NoBolsoDbContext>(options =>
                            options.UseNpgsql(connectionString));
                        break;
                }
            }

            // Repositórios
            services.AddScoped<ICarteiraRepository, CarteiraRepository>();
            services.AddScoped<ITransacaoRepository, TransacaoRepository>();

            // Serviços
            services.AddScoped<IEventService, EventService>();

            return services;
        }

        public static async Task EnsureDatabaseCreatedAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<NoBolsoDbContext>();
            await dbContext.Database.EnsureCreatedAsync();
        }
    }
}