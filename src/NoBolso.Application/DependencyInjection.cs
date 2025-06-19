using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NoBolso.Application.Behaviors;
using System.Reflection;

namespace NoBolso.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Pega o Assembly da Aplicação para não precisar referenciar classes específicas
            var applicationAssembly = typeof(DependencyInjection).Assembly;

            // Adiciona o MediatR (se já não estiver lá)
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Adiciona o Pipeline Behavior de Validação
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // Encontra e registra automaticamente todos os validadores do assembly da Application
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


            return services;
        }
    }
}