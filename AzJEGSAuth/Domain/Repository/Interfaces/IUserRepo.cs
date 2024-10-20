using Domain.Models;
using ErrorOr;

namespace Domain.Repository.Interfaces;

public interface IUserRepo
{
    public Task<ErrorOr<User>> Get(Guid id);
    public Task<ErrorOr<User>> GetByEmail(string email);
    public Task<List<User>> List();
    public Task<ErrorOr<Updated>> Update(User user);
    public Task<ErrorOr<Deleted>> Delete(Guid id);
    public Task<ErrorOr<Created>> Create(User user);
}