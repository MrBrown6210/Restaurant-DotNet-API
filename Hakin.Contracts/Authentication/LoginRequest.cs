namespace Hakin.Contracts.Authentication;

public record LoginRequest(
    string Email,
    string Password
);