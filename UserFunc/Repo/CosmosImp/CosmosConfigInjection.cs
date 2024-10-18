using Microsoft.Extensions.DependencyInjection;
using UserFunc.Repo.RedisImp;

namespace UserFunc.Repo.CosmosImp;

public static class CosmosConfigInjection
{
    public static IServiceCollection AddRepoImps(
        this IServiceCollection services
    )
    {
        // services.AddScoped<IUserRepo, UserCosmosRepo>();
        // services.AddSingleton<CosmosConnectionFactory>();
        services.AddScoped<IUserRepo, UserRedisRepo>();
        services.AddSingleton<RedisConnectionFactory>();
        return services;
    }
}