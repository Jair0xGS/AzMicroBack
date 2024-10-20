using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Config;

public static class ConfigInjection
{
    public static IServiceCollection AddPersistenceConfig(
        this IServiceCollection services,
        IConfigurationRoot configuration
    )
    {
        services
            .AddRedis(configuration)
            .AddJwt(configuration)
            ;
        return services;
    }

    private static IServiceCollection AddJwt(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var cConfig = configuration.GetSection(JwtConfig.SectionName).Get<JwtConfig>();
        if(cConfig != null) services.AddSingleton(cConfig);
        return services;
    }
  

    private static IServiceCollection AddRedis(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var cConfig = configuration.GetSection(RedisConfig.SectionName).Get<RedisConfig>();
        if(cConfig != null) services.AddSingleton(cConfig);
        return services;
    }

}
