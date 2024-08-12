using Microsoft.Extensions.DependencyInjection;

namespace UserFunc.Svc;

public static class SvcConfigInjection
{
    
    public static IServiceCollection AddSvcImps(
        this IServiceCollection services
    )
    {
        services.AddScoped<UserSvc,UserSvc>();
        return services;
    }
}