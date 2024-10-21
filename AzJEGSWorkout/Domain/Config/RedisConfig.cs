namespace Domain.Config;

public class RedisConfig
{
    public static string SectionName = "RedisSettings";
    public string Host { get; set; }= null!;
    public int Port { get; set; }
    public string Password { get; set; }= null!;
}
