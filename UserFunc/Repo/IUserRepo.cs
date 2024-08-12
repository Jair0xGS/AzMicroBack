using UserFunc.Model;

namespace UserFunc.Repo;

public interface IUserRepo
{
    public IEnumerable<User> SimpleList();
}