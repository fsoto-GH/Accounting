using Accounting.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Accounting.API.DTOs.Account;

public class AccountAddDto : IAccountDto
{
    [Required]
    [Range(1, 2)]
    public AccountType Type { get; set; }

    public string? NickName { get; set; } = null;
}
