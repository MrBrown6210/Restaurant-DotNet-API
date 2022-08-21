using Hakin.Domain.Entities;

namespace Hakin.Application.Common.Interfaces.Authentication;

public interface IJWTTokenGenerator
{
    string GenerateToken(User user);
}