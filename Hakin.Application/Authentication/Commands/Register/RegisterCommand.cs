using ErrorOr;
using Hakin.Application.Authentication.Common;
using MediatR;

namespace Hakin.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;