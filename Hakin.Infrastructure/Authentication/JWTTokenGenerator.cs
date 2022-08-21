using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Hakin.Application.Common.Interfaces.Authentication;
using Hakin.Application.Common.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;

namespace Hakin.Infrastructure.Authentication;

public class JWTTokenGenerator : IJWTTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public JWTTokenGenerator(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
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
            issuer: "Hakin",
            claims: claims,
            expires: _dateTimeProvider.UtcNow.AddMinutes(60),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("super-secret-key")),
                SecurityAlgorithms.HmacSha256Signature
            )
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}