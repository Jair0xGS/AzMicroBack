using System.Text.Json;
using Domain.Config;
using StackExchange.Redis;

namespace Domain.Repository.Imp.Redis;

public class RedisConnection(RedisConfig redisConfig)
{
    private IDatabase? _db;
    private ConnectionMultiplexer? _cnn;

    private ConnectionMultiplexer? GetConnection()
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

    private IDatabase? GetDb()
    {
        if (_db is not null) return _db;
        try
        {
            var cnn = GetConnection();
            if (cnn is null) return null;
            _db = cnn.GetDatabase();
            return _db;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Redis Err>>> {ex.Message}");
            return null;
        }
    }

    public Task<T?> Get<T>(string ky)
    {
        var db = GetDb();
        if (db is null) return Task.FromResult<T?>(default);
        var ls = db.StringGet(ky);
        return Task.FromResult(!ls.HasValue ? default : JsonSerializer.Deserialize<T>(ls.ToString()));
    }
    
    public async Task<T?> GetFirst<T>(string regex)
    {
        var cnn = GetConnection();
        if (cnn is null) return default;
        var server = cnn.GetServer(cnn.GetEndPoints()[0]);
        var keys = server.Keys(pattern: regex).ToList();
        if (keys.Count == 0) return default;
        return await Get<T>(keys.First()!);
    }

    public async Task<List<T>> GetAll<T>(string regex)
    {
        var cnn = GetConnection();
        if (cnn is null) return [];
        var server = cnn.GetServer(cnn.GetEndPoints()[0]);
        var keys = server.Keys(pattern: regex).ToList();
        List<T> rs = [];
        foreach (var ky in keys)
        {
            var search = await Get<T>(ky.ToString());
            if (search is not null) rs.Add(search);
        }

        return rs;
    }


    public Task Write<T>(string ky, T reg, TimeSpan? duration)
    {
        var rsStr = JsonSerializer.Serialize(reg);
        var db = GetDb();
        db?.StringSet(ky, rsStr, duration);
        return Task.CompletedTask;
    }

    public Task Remove(string ky)
    {
        var db = GetDb();
        db?.KeyDelete(ky);
        return Task.CompletedTask;
    }
    
    public Task RemoveFirst(string regex)
    {
        var cnn = GetConnection();
        if (cnn is null) return default;
        var server = cnn.GetServer(cnn.GetEndPoints()[0]);
        var keys = server.Keys(pattern: regex).ToList();
        var db = GetDb();
        if (keys.Count == 0) return Task.CompletedTask;
        db?.KeyDelete(keys.First());
        return Task.CompletedTask;
    }
}