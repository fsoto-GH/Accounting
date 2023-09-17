using System.ComponentModel.DataAnnotations;

namespace Accounting.API.DTOs.Person;

public class PersonPatchDto : PersonBaseDto
{
    [MaxLength(50)]
    public override string? FirstName { get; set; }

    [MaxLength(50)]
    public override string? LastName { get; set; } = null;

    [MaxLength(50)]
    public override string? MiddleName { get; set; } = null;

    public override void TrimNames()
    {
        FirstName = FirstName?.Trim();
        LastName = LastName?.Trim();
        MiddleName = MiddleName?.Trim();
    }
}
