using UserFunc.Model;
using UserFunc.Repo;

namespace UserFunc.Svc;

public class UserSvc(
    IUserRepo repo)
{
    public IEnumerable<User> SimpleList()
    {
        return repo.SimpleList();
    }
}

