using Microsoft.Extensions.DependencyInjection;

namespace UserFunc.Repo.CosmosImp;

public static class CosmosConfigInjection
{
    public static IServiceCollection AddRepoImps(
        this IServiceCollection services
    )
    {
        services.AddScoped<IUserRepo, UserCosmosRepo>();
        return services;
    }
}