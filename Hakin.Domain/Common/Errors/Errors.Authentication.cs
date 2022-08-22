using System.Security.Cryptography.X509Certificates;
using ErrorOr;

namespace Hakin.Domain.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredential => Error.Validation(code: "Auth.Invalid", description: "Invalid credential.");
    }
}