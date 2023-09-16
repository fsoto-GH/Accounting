
using System.ComponentModel.DataAnnotations;

namespace Accounting.API.DTOs.Person;

public class PersonBaseDto
{
    [MaxLength(50)]
    public virtual string FirstName { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public virtual string LastName { get; set; } = string.Empty;

    [MaxLength(50)]
    public virtual string? MiddleName { get; set; } = null;

    [MaxLength(100)]
    public virtual string UserName { get; set; } = string.Empty;

    public void TrimNames()
    {
        FirstName = FirstName?.Trim() ?? string.Empty;
        LastName = LastName?.Trim() ?? string.Empty;
        MiddleName = MiddleName?.Trim();
        UserName = UserName?.Trim() ?? string.Empty;
    }
}
