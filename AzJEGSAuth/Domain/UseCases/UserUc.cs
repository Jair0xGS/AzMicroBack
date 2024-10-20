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
        var prev = await repo.GetByEmail(dto.Email);
        if (prev.IsError && prev.FirstError == GErrors.User.UserNotFound)
        {
            return await repo.Create(User.FromCommand(dto));
        }
        return GErrors.User.EmailExists;
    }
    
    public async Task<List<UserResponse>> List()
    {
        var elems = await repo.List();
        return elems.Select(User.ToResponse).ToList();
    }
    
    public async Task<ErrorOr<Updated>> Update(UserUpdateCommand command)
    {
        var user = await repo.Get(command.Id);
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
    
    public async Task<ErrorOr<UserResponse>> Get(Guid id)
    {
        var reg =await repo.Get(id);
        if (reg.IsError) return reg.FirstError;
        return User.ToResponse(reg.Value);
    }
    
    
    public async Task<ErrorOr<Updated>> UpdatePassword(Guid id,string newPassword)
    {
        var user = await repo.Get(id);
        if (user.IsError) return user.FirstError;
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        return await repo.Update(user.Value with
        {
            Password = passwordHash
        });
    }
}