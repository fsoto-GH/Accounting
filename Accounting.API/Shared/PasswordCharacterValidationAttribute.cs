using System.ComponentModel.DataAnnotations;

namespace Accounting.API.Shared
{
    public class PasswordCharacterValidationAttribute : ValidationAttribute
    {
        private readonly HashSet<char> REQUIRED_CHARACTERS = "[!@#$%^&*()]+".ToHashSet();

        public override bool IsValid(object? value)
        {
            string? strValue = value as string;
            if (!string.IsNullOrEmpty(strValue))
            {
                var passwordCharSet = new HashSet<char>(strValue);

                if (!passwordCharSet.Overlaps(REQUIRED_CHARACTERS))
                    return false;

                if (!passwordCharSet.Any(char.IsUpper))
                    return false;

                if (!passwordCharSet.Any(char.IsLower))
                    return false;
            }
            return true;
        }
    }
}
