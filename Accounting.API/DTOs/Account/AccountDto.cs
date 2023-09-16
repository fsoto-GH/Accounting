using Accounting.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Accounting.API.DTOs.Account;

public class AccountDto : IAccountDto
{
    public int AccountID { get; set; } = 0;
    public int PersonID { get; set; } = 0;

    public string Type { get; set; } = string.Empty;

    public string? NickName { get; set; } = string.Empty;

    public bool Status { get; set; }
}
