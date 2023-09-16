using Accounting.API.Shared;
using System.ComponentModel.DataAnnotations;

namespace Accounting.API.DTOs.Person
{
    public class PersonCredentialsDto
    {
        private string password = string.Empty;

        [Required]
        public int PersonID { get; set; }

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MinLength(8, ErrorMessage ="Password needs to be at least 8 characters.")]
        [PasswordCharacterValidationAttribute(ErrorMessage = "Password must have an uppercase letter, a lowercase letter, and one of these special characters: !@#$%^&*()")]
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value.Trim();
            }
        }
    }
}
