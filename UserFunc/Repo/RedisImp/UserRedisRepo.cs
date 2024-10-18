
using System.Text.Json;
using UserFunc.Model;

namespace UserFunc.Repo.RedisImp;

public class UserRedisRepo(
    RedisConnectionFactory connectionFactory
):IUserRepo
{
    private string key = "users";
    private async Task<T?> Get<T>(string ky)
    {
        var ls = connectionFactory.GetDb()!.StringGet(ky);
        return !ls.HasValue ?  default : JsonSerializer.Deserialize<T>(ls.ToString());
    }
    
    private async Task<List<T>> GetAll<T>(string regex)
    {
        var cnn = connectionFactory.GetConnection();
        if (cnn is null) return []; 
      var server = cnn.GetServer(cnn.GetEndPoints()[0]);
        var keys = server.Keys(pattern:regex).ToList();
        List<T> rs = [];
        foreach (var ky in keys)
        {
            var search = await Get<T>(ky.ToString());
            if(search is not null) rs.Add(search);
        }
        return rs;
    }
    
    
    private async Task Write<T>(string ky,T reg,TimeSpan? duration)
    {
        var rsStr = JsonSerializer.Serialize(reg);
        connectionFactory.GetDb()!.StringSet(ky, rsStr, duration);
    }
    
    private async Task Remove(string ky)
    {
        connectionFactory.GetDb()!.KeyDelete(ky);
    }

    public Task<User?> View(string userId)
    {
        return Get<User>($"{key}:{userId}");
    }

    public async Task<IEnumerable<User>> SimpleList()
    {
        return await GetAll<User>($"{key}:*");
    }

    public Task Update(User user)
    {
        return Write($"{key}:{user.Id}",user,null);
    }

    public Task Delete(string userId)
    {
        return Remove($"{key}:{userId}");
    }

    public Task Create(User user)
    {
        return Write($"{key}:{user.Id}",user,null);
    }
}