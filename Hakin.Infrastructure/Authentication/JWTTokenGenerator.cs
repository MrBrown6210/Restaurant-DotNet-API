using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Hakin.Application.Common.Interfaces.Authentication;
using Hakin.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Hakin.Infrastructure.Authentication;

public class JWTTokenGenerator : IJWTTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JWTSettings _jwtSettings;

    public JWTTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JWTSettings> jwtOptions)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtOptions.Value;
    }

    public string GenerateToken(Guid userId, string email, string firstName, string lastName)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, firstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256Signature
            )
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}