using ErrorOr;
using Hakin.Application.Authentication.Common;
using MediatR;

namespace Hakin.Application.Authentication.Commands.Register;

public record LoginQuery(
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;