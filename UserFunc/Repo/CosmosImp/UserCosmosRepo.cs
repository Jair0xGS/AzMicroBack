using UserFunc.Model;

namespace UserFunc.Repo.CosmosImp;

public class UserCosmosRepo():IUserRepo
{
    public async Task<IEnumerable<User>> SimpleList()
    {
        return [new User()];
    }

    public async Task Update(User user)
    {
        await Task.CompletedTask;
    }

    public async Task Delete(string userId)
    {
        await Task.CompletedTask;
    }

    public async Task Create(User user)
    {
        await Task.CompletedTask;
    }

}