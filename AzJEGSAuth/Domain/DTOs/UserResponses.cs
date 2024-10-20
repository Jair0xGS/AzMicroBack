namespace Domain.DTOs;

public record UserResponse
(
    Guid Id,
    string Name,
    string LastName,
    string Email
);