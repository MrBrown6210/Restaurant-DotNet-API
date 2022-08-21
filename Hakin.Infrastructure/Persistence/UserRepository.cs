using Hakin.Application.Common.Interfaces.Persistence;
using Hakin.Domain.Entities;

namespace Hakin.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly static List<User> _users = new();
    public User? GetByEmail(string email)
    {
        return _users.SingleOrDefault(u => u.Email == email);
    }

    public void Add(User user)
    {
        _users.Add(user);
    }
}