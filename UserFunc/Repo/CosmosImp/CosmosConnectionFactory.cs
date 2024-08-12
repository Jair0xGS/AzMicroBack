using Microsoft.Azure.Cosmos;
using UserFunc.Config;
using Container = Microsoft.Azure.Cosmos.Container;

namespace UserFunc.Repo.CosmosImp;

public class CosmosConnectionFactory(CosmosConfig config)
{
    private Container? _container;

    public Container GetContainer()
    {
        if (_container is not null) return _container;
        var connection = new CosmosClient(config.Connection,config.Key);
        var db = connection.GetDatabase(config.Database);
        _container = db.GetContainer(config.Container);
        if (_container is null) throw new Exception("Cosmos Connection Failed");
        return _container;
    }
    
}