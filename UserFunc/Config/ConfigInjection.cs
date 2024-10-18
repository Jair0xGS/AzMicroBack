using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserFunc.Config;

public static class ConfigInjection
{
  public static IServiceCollection AddPersistenceConfig(
    this IServiceCollection services,
    IConfigurationRoot configuration
  )
  {
    services
      // .AddCosmos(configuration)
      .AddRedis(configuration)
      ;
    return services;
  }

  private static IServiceCollection AddCosmos(
    this IServiceCollection services,
    IConfigurationRoot configuration
  )
  {
    //**Old way
    // var cConfig = new CosmosConfig();
    // configuration.Bind(CosmosConfig.SectionName, cConfig);
    //**Compact way
    var cConfig = configuration.GetSection(CosmosConfig.SectionName).Get<CosmosConfig>();
    if(cConfig != null) services.AddSingleton(cConfig);
    //create the connection
    return services;
  }
  

  private static IServiceCollection AddRedis(
    this IServiceCollection services,
    IConfigurationRoot configuration
  )
  {
    var rConfig = configuration.GetSection(RedisConfig.SectionName).Get<RedisConfig>();
    if(rConfig != null) services.AddSingleton(rConfig);
    return services;
  }

}