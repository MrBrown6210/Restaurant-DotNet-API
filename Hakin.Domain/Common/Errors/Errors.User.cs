using System.Security.Cryptography.X509Certificates;
using ErrorOr;

namespace Hakin.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(code: "User.DuplicateEmail", description: "Email already exists");
    }
}