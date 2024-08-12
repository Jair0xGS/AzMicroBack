using UserFunc.Model;

namespace UserFunc.Repo;

public interface IUserRepo
{
    public Task<User?> View(string userId);
    public Task<IEnumerable<User>> SimpleList();
    public Task Update(User user);
    public Task Delete(string userId);
    public Task Create(User user);
}