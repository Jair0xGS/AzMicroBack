using UserFunc.Model;
using UserFunc.Repo;

namespace UserFunc.Svc;

public class UserSvc(
    IUserRepo repo)
{
    public async Task<IEnumerable<User>> SimpleList()
    {
        return await repo.SimpleList();
    }
    public Task Create(User user)
    {
        return repo.Create(user);
    }
    public Task Update(User user)
    {
        return repo.Update(user);
    }
    public Task Delete(string id)
    {
        return repo.Delete(id);
    }
}

