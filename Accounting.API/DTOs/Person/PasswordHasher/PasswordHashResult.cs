namespace Accounting.API.DTOs.Person.PasswordHasher
{
    public class PasswordHashResult
    {
        public byte[] PasswordSalt { get; set; } = new byte[16];
        public byte[] PasswordHash { get; set; } = new byte[60];
    }
}
