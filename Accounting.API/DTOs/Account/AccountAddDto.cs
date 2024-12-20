using Accounting.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Accounting.API.DTOs.Account;

public class AccountAddDto : AccountBaseDto
{
    [Required]
    public override AccountType? Type { get; set; }
}
