using Microsoft.Extensions.DependencyInjection;

namespace Domain.UseCases;

public static class DomainConfigInjection
{
        public static IServiceCollection AddDomainImp(
            this IServiceCollection services
        )
        {
            services.AddScoped<AuthUc,AuthUc>();
            services.AddScoped<UserUc,UserUc>();
            return services;
        }
    }
