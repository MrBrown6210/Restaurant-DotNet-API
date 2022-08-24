using Hakin.Domain.Entities;

namespace Hakin.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token
);