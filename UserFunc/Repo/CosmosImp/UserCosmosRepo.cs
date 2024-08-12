using Microsoft.Azure.Cosmos;
using User = UserFunc.Model.User;

namespace UserFunc.Repo.CosmosImp;

public class UserCosmosRepo(CosmosConnectionFactory connectionFactory):IUserRepo
{

    public async Task<IEnumerable<User>> SimpleList()
    {
        var query = connectionFactory.GetContainer().GetItemQueryIterator<User>();
        var results = new List<User>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.ToList());
        }
        return results;
    }

    public async Task Update(User user)
    {
        await connectionFactory.GetContainer().UpsertItemAsync(user, new PartitionKey(user.UserId));
    }

    public async Task Delete(string userId)
    {
        await connectionFactory.GetContainer().DeleteItemAsync<User>(userId, new PartitionKey(userId));
    }

    public async Task Create(User user)
    {
        await connectionFactory.GetContainer().CreateItemAsync(user, new PartitionKey(user.UserId));
    }

    public async Task<User?> View(string userId)
    {
        try
        {
            var response = await connectionFactory.GetContainer().ReadItemAsync<User>(userId, new PartitionKey(userId));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}