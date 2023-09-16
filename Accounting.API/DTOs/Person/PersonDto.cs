using System.ComponentModel.DataAnnotations;

namespace Accounting.API.DTOs.Person;

public class PersonDto : PersonBaseDto
{
    [Required]
    public int PersonID { get; set; }

    [Required]
    [MaxLength(100)]
    public override string UserName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public override string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public override string LastName { get; set; } = string.Empty;

    [MaxLength(50)]
    public override string? MiddleName { get; set; }
}