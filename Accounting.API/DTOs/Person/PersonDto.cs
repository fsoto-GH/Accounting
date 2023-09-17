namespace Accounting.API.DTOs.Person;

public class PersonDto : PersonBaseDto
{
    public int? PersonID { get; set; }

    public override string? FirstName { get; set; } = string.Empty;

    public override string? LastName { get; set; } = string.Empty;

    public override string? MiddleName { get; set; }
}