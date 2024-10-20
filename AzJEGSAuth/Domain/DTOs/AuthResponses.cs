namespace Domain.DTOs;

public record AuthResponse(
    string Token,
    long Expiration,
    string RefreshToken,
    long RefreshExpiration
);