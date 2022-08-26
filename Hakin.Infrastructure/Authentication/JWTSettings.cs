namespace Hakin.Infrastructure.Authentication;

public class JWTSettings
{
    public const string SectionName = "JWTSettings";
    public string Secret { get; init; } = null!;
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public int ExpiryInMinutes { get; init; }
}