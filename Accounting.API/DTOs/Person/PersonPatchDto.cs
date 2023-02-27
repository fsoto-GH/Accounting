namespace AccountingAPI.DTOs.Person;

public class PersonPatchDto : IPersonDto
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string? MiddleName { get; set; } = null;
}
