namespace Hakin.Application.Common.Interfaces.Authentication;

public interface IJWTTokenGenerator
{
    string GenerateToken(Guid userId, string email, string firstName, string lastName);
}