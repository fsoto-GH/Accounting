using System.ComponentModel.DataAnnotations;

namespace AccountingAPI.DTOs.Account;

public class AccountDto : IAccountDto
{
    public int AccountID { get; set; } = 0;
    public int PersonID { get; set; } = 0;

    [Required]
    public string Type { get; set; } = string.Empty;

    public string? NickName { get; set; } = string.Empty;

    [Required]
    public bool Status { get; set; }
}
