using ErrorOr;
using Hakin.Application.Common.Interfaces.Authentication;
using Hakin.Application.Common.Interfaces.Persistence;
using Hakin.Domain.Common.Errors;
using Hakin.Domain.Entities;

namespace Hakin.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJWTTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJWTTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        // Check user exists
        if (_userRepository.GetByEmail(email) is not User user)
        {
            return Errors.Authentication.InvalidCredential;
        }

        // Validate password
        if (user.Password != password)
        {
            return Errors.Authentication.InvalidCredential;
        }

        // Gen JWT Token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            User: user,
            Token: token
        );
    }

    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        // Check user exists
        if (_userRepository.GetByEmail(email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        // Create user (with unique ID)
        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };
        _userRepository.Add(user);

        // Gen JWT Token
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(
            User: user,
            Token: token
        );
    }
}