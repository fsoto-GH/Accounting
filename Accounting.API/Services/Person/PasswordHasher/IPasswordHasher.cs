using Accounting.API.DTOs.Person.PasswordHasher;

namespace Accounting.API.Services.Person.PasswordHasher;

public interface IPasswordHasher
{
    PasswordHashResult HashPassword(string password);
    /// <summary>
    /// Determines whether the password matches the hashedPassword.
    /// </summary>
    /// <param name="password">A plain text password input.</param>
    /// <param name="hashedPassword">The hash result of the correct password.</param>
    /// <returns>true if the plain text password matches the correct password's hash.</returns>
    bool VerifyPassword(string password, byte[] hashedPassword);
}
