using StackExchange.Redis;
using UserFunc.Config;

namespace UserFunc.Repo.RedisImp;

public class RedisConnectionFactory(RedisConfig redisConfig)
{
    private IDatabase? _db;
    private readonly TimeSpan _healthCheckInterval = TimeSpan.FromMinutes(10);
    private DateTime? _lastHealthCheck;
    private ConnectionMultiplexer? _cnn;

    public ConnectionMultiplexer? GetConnection()
    {
            var redisConfiguration =
                $"{redisConfig.Host}:{redisConfig.Port},password={redisConfig.Password},user=default,abortConnect=false";
            _lastHealthCheck = DateTime.UtcNow;
            var conn = ConnectionMultiplexer.Connect(redisConfiguration);
            if (!conn.IsConnected) return null;
            _cnn = conn;
            return _cnn;
    }
    
    public IDatabase? GetDb()
    {
        if (_db is not null) return _db;
        try
        {
            var cnn = GetConnection();
            if (cnn is null) return null;
            _db =cnn.GetDatabase();
            return _db;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Redis Err>>> {ex.Message}");
            return null;
        }

    }
}