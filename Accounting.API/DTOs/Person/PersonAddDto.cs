using System.ComponentModel.DataAnnotations;

namespace AccountingAPI.DTOs.Person;

public class PersonAddDto : IPersonDto
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    public string? MiddleName { get; set; }
}
