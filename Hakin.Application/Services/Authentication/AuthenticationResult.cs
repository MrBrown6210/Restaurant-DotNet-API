using Hakin.Domain.Entities;

namespace Hakin.Application.Services.Authentication;

public record AuthenticationResult(
    User User,
    string Token
);