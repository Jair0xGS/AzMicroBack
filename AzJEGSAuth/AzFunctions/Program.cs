using Domain.Config;
using Domain.Repository.Imp.Redis;
using Domain.UseCases;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var config =new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .Build();

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services => {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddPersistenceConfig(config);
        services.AddRedisRepositoryImp();
        services.AddDomainImp();
    })
    .Build();

host.Run();
