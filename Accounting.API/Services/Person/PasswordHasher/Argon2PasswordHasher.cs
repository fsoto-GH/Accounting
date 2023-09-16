using System.Security.Cryptography;
using System.Text;
using Accounting.API.DTOs.Person.PasswordHasher;
using Konscious.Security.Cryptography;


namespace Accounting.API.Services.Person.PasswordHasher;

public class Argon2PasswordHasher : IPasswordHasher
{
    private const int SALT_SIZE_BYTES = 16;
    private const int HASH_SIZE_BYTES = 60;
    private const int DEGREES_OF_PARALLELISM = 8;
    private const int MEMORY_SIZE = 8192;
    private const int ITERATIONS = 4;

    public PasswordHashResult HashPassword(string password)
    {
        var salt = GenerateSalt();
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = DEGREES_OF_PARALLELISM,
            MemorySize = MEMORY_SIZE,
            Iterations = ITERATIONS
        };

        return new PasswordHashResult {
            PasswordSalt = salt,
            PasswordHash = argon2.GetBytes(HASH_SIZE_BYTES)
        };
    }

    public bool VerifyPassword(string password, byte[] hashedPassword)
    {
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = ExtractSaltFromHashedPassword(hashedPassword),
            DegreeOfParallelism = DEGREES_OF_PARALLELISM,
            MemorySize = MEMORY_SIZE,
            Iterations = ITERATIONS
        };
        var hashResult = argon2.GetBytes(SALT_SIZE_BYTES);
        return hashResult.AsSpan().SequenceEqual(hashedPassword);
    }

    private static byte[] ExtractSaltFromHashedPassword(byte[] hashedPassword)
    {
        var salt = new byte[SALT_SIZE_BYTES];
        Buffer.BlockCopy(hashedPassword, 0, salt, 0, SALT_SIZE_BYTES);
        return salt;
    }

    private static byte[] GenerateSalt()
    {
        var salt = new byte[SALT_SIZE_BYTES];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return salt;
    }
}

