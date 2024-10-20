using Domain.DTOs;
using Domain.Errors;
using Domain.Models;
using Domain.Repository.Interfaces;
using ErrorOr;

namespace Domain.UseCases;

public class UserUc(IUserRepo repo)
{
    public async Task<ErrorOr<Created>> Create(UserCreateCommand dto)
    {
        var prev = await GetByEmail(dto.Email);
        if (prev.IsError && prev.FirstError == GErrors.User.UserNotFound)
        {
            return await repo.Create(User.FromCommand(dto));
        }
        return GErrors.User.EmailExists;
    }
    
    public Task<List<User>> List()
    {
        return repo.List();
    }
    
    public async Task<ErrorOr<Updated>> Update(UserUpdateCommand command)
    {
        var user = await Get(command.Id);
        if (user.IsError) return user.FirstError;
        return await repo.Update(user.Value with
        {
            Name = command.Name,
            LastName = command.LastName,
            Email = command.Email,
        });
    }
    
    public Task<ErrorOr<Deleted>> Delete(Guid id)
    {
        return repo.Delete(id);
    }
    
    public Task<ErrorOr<User>> Get(Guid id)
    {
        return repo.Get(id);
    }
    
    public Task<ErrorOr<User>> GetByEmail(string email)
    {
        return repo.GetByEmail(email);
    }
    
    public async Task<ErrorOr<Updated>> UpdatePassword(Guid id,string newPassword)
    {
        var user = await Get(id);
        if (user.IsError) return user.FirstError;
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        return await repo.Update(user.Value with
        {
            Password = passwordHash
        });
    }
}