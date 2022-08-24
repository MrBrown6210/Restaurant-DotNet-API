using ErrorOr;
using Hakin.Application.Common.Interfaces.Authentication;
using Hakin.Application.Common.Interfaces.Persistence;
using Hakin.Domain.Entities;
using Hakin.Domain.Common.Errors;
using MediatR;
using Hakin.Application.Authentication.Common;

namespace Hakin.Application.Authentication.Commands.Register;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJWTTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJWTTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        // Check user exists
        if (_userRepository.GetByEmail(query.Email) is not User user)
        {
            return Errors.Authentication.InvalidCredential;
        }

        // Validate password
        if (user.Password != query.Password)
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
}