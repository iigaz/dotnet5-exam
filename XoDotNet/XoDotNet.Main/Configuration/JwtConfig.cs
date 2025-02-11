namespace XoDotNet.Main.Configuration;

public class JwtConfig
{
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string SecretKey { get; set; } = null!;
    public int LifetimeMinutes { get; set; }
}