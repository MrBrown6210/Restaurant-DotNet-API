using Hakin.Application.Common.Interfaces.Authentication;

namespace Hakin.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJWTTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(IJWTTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public AuthenticationResult Login(string email, string password)
    {
        // Check user exists

        // Create user (with unique ID)

        // Gen JWT Token
        var userId = Guid.NewGuid();
        var token = _jwtTokenGenerator.GenerateToken(userId, email, "John", "Doe");

        return new AuthenticationResult(
            Id: userId,
            FirstName: "John",
            LastName: "Doe",
            Email: email,
            Token: token
        );
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        // Check user exists

        // Create user (with unique ID)

        // Gen JWT Token
        var userId = Guid.NewGuid();
        var token = _jwtTokenGenerator.GenerateToken(userId, email, firstName, lastName);
        return new AuthenticationResult(
            Id: Guid.NewGuid(),
            FirstName: firstName,
            LastName: lastName,
            Email: email,
            Token: token
        );
    }
}