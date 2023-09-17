
using System.ComponentModel.DataAnnotations;

namespace Accounting.API.DTOs.Person;

public class PersonBaseDto
{
    [MaxLength(50)]
    public virtual string? FirstName { get; set; }
    
    [MaxLength(50)]
    public virtual string? LastName { get; set; }

    [MaxLength(50)]
    public virtual string? MiddleName { get; set; }

    public virtual void TrimNames()
    {
        FirstName = FirstName?.Trim() ?? string.Empty;
        LastName = LastName?.Trim() ?? string.Empty;
        MiddleName = MiddleName?.Trim();
    }
}
