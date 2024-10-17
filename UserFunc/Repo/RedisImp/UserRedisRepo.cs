
using System.Text.Json;

namespace UserFunc.Repo.RedisImp;

public class UserRedisRepo(
    RedisConnectionFactory connectionFactory
){
    
    private async Task<(string, T?)> Get<T>(string key)
    {
        var ls = connectionFactory.GetDb()!.StringGet(key);
        return !ls.HasValue ? (key, default) : (key, JsonSerializer.Deserialize<T>(ls.ToString()));
    }
    
    private async Task Write<T>(string key,T reg,TimeSpan duration)
    {
        var rsStr = JsonSerializer.Serialize(reg);
        connectionFactory.GetDb()!.StringSet(key, rsStr, duration);
    }
    
    private async Task Remove<T>(string key)
    {
        connectionFactory.GetDb()!.KeyDelete(key);
    }
    
}