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
}

