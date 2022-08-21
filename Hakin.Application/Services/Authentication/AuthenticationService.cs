using Hakin.Application.Common.Interfaces.Authentication;
using Hakin.Application.Common.Interfaces.Persistence;
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

    public AuthenticationResult Login(string email, string password)
    {
        // Check user exists
        if (_userRepository.GetByEmail(email) is not User user)
        {
            throw new Exception("User does not exist");
        }

        // Validate password
        if (user.Password != password)
        {
            throw new Exception("Invalid password");
        }

        // Gen JWT Token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            User: user,
            Token: token
        );
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        // Check user exists
        if (_userRepository.GetByEmail(email) is not null)
        {
            throw new Exception("User already exist");
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