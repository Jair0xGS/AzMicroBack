using Domain.Config;
using StackExchange.Redis;

namespace Domain.Repository.Imp.Redis;

public class RedisConnection(RedisConfig redisConfig )
{
    private IDatabase? _db;
    private ConnectionMultiplexer? _cnn;

    public ConnectionMultiplexer? GetConnection()
    {
        var redisConfiguration = $"{redisConfig.Host}:{redisConfig.Port}," +
                                 $"password={redisConfig.Password}," +
                                 $"user=default," +
                                 $"abortConnect=false";
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