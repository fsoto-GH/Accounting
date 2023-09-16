using System.ComponentModel.DataAnnotations;

namespace Accounting.API.DTOs.Person;

public class PersonPatchDto : PersonBaseDto
{
    [MaxLength(50)]
    public override string FirstName { get; set; } = string.Empty;

    [MaxLength(50)]
    public override string LastName { get; set; } = string.Empty;

    [MaxLength(50)]
    public override string? MiddleName { get; set; } = null;

    [MaxLength(100)]
    public override string UserName { get; set; } = string.Empty;
}
