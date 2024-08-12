using UserFunc.Model;

namespace UserFunc.Repo.Imp;

public class UserCosmosRepo():IUserRepo
{
    public IEnumerable<User> SimpleList()
    {
        return [];
    }
}