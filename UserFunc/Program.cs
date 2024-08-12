using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using UserFunc.Config;
using UserFunc.Repo.CosmosImp;
using UserFunc.Svc;

var config =new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .Build();

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddPersistenceConfig(config);
        services.AddRepoImps();
        services.AddSvcImps();
    })
    .Build();

host.Run();
