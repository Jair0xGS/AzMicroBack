using System.Text.Json;
using Domain.DTOs;

namespace Domain.Models;

public record User(Guid Id, string Name, string LastName, string Email, string Password)
{
    public bool CheckPassword(string otherPassword)
    {
        return BCrypt.Net.BCrypt.Verify(otherPassword,Password);
    }

    public static User FromCommand(UserCreateCommand dto)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        var rs = new User(
            Guid.NewGuid(), 
            dto.Name,
            dto.LastName,
            dto.Email,
            passwordHash??""
            );
        return rs;
    }
    public static UserResponse ToResponse(User user)
    {
        return new UserResponse(user.Id, user.Name, user.LastName, user.Email);
    }
}