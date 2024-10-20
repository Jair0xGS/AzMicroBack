using System.Text.Json;
using Domain.Errors;
using Domain.Models;
using Domain.Repository.Interfaces;
using ErrorOr;

namespace Domain.Repository.Imp.Redis;

public class UserRepo(RedisConnection redisConnection):IUserRepo
{
    private string key = "users";
    private Task<T?> Get<T>(string ky)
    {
        var ls = redisConnection.GetDb()!.StringGet(ky);
        return Task.FromResult(!ls.HasValue ?  default : JsonSerializer.Deserialize<T>(ls.ToString()));
    }
    
    private async Task<List<T>> GetAll<T>(string regex)
    {
        var cnn = redisConnection.GetConnection();
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
    
    
    private Task Write<T>(string ky,T reg,TimeSpan? duration)
    {
        var rsStr = JsonSerializer.Serialize(reg);
        redisConnection.GetDb()!.StringSet(ky, rsStr, duration);
        return Task.CompletedTask;
    }
    
    private Task Remove(string ky)
    {
        redisConnection.GetDb()!.KeyDelete(ky);
        return Task.CompletedTask;
    }

    public async Task<ErrorOr<User>> Get(Guid id)
    {
        var reg =await Get<User>($"{key}:{id.ToString()}");
        if (reg is null) return GErrors.User.UserNotFound;
        return reg;
    }

    public async Task<ErrorOr<User>> GetByEmail(string email)
    {
        var all = await List();
        var reg =  all.FirstOrDefault(el => el.Email == email);
        if (reg is null) return GErrors.User.UserNotFound;
        return reg;
    }

    public async Task<List<User>> List()
    {
        return await GetAll<User>($"{key}:*");
    }

    public async Task<ErrorOr<Updated>> Update(User user)
    {
        await Write($"{key}:{user.Id}",user,null);
        return Result.Updated;
    }

    public async Task<ErrorOr<Deleted>> Delete(Guid id)
    {
        await Remove($"{key}:{id.ToString()}");
        return Result.Deleted;
    }

    public async Task<ErrorOr<Created>> Create(User user)
    {
        Console.WriteLine(JsonSerializer.Serialize(user));
        await  Write($"{key}:{user.Id}",user,null);
        return Result.Created;
    }    
}