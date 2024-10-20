
namespace Domain.Config;

public class JwtConfig
{
    public static string SectionName = "JwtSettings";
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public long AccessTokenExpirationMinutes { get; set; }
    public bool UseRefresh { get; set; } 
    public string RefreshSecretKey { get; set; } = string.Empty;
    public long RefreshTokenExpirationMinutes { get; set; }
}