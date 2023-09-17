using System.ComponentModel.DataAnnotations;

namespace Accounting.API.DTOs.Person;

public class PersonAddDto : PersonBaseDto
{
    [Required]
    [MaxLength(50)]
    public override string? FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public override string? LastName { get; set; } = string.Empty;

    [MaxLength(50)]
    public override string? MiddleName { get; set; }
}
