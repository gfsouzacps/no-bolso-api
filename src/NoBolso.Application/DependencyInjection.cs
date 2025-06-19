using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace NoBolso.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Pega o Assembly da Aplicação para não precisar referenciar classes específicas
            var applicationAssembly = typeof(DependencyInjection).Assembly;

            // Registra o AutoMapper, procurando por todos os perfis de mapeamento
            services.AddAutoMapper(applicationAssembly);

            // Registra todos os validadores do FluentValidation que encontrar
            services.AddValidatorsFromAssembly(applicationAssembly);

            // Registra o MediatR, encontrando todos os Handlers
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

            return services;
        }
    }
}