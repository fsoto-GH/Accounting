
namespace AccountingAPI.DTOs.Person;

public interface IPersonDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }

    public void TrimNames()
    {
        FirstName = FirstName?.Trim() ?? string.Empty;
        LastName = LastName?.Trim() ?? string.Empty;
        MiddleName = MiddleName?.Trim();
    }
}
