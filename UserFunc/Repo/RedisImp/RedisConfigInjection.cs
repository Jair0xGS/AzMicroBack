using Microsoft.Extensions.DependencyInjection;

namespace UserFunc.Repo.RedisImp;

public static class RedisConfigInjection
{
    public static IServiceCollection AddRepoImps(
        this IServiceCollection services
    )
    {
        services.AddScoped<IUserRepo, UserRedisRepo>();
        services.AddSingleton<RedisConnectionFactory>();
        return services;
    }
}
