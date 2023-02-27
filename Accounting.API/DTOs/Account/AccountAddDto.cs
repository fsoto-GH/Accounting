using System.ComponentModel.DataAnnotations;

namespace AccountingAPI.DTOs.Account;

public class AccountAddDto : IAccountDto
{
    [Required]
    public string Type { get; set; } = string.Empty;

    public string? NickName { get; set; } = null;
}
