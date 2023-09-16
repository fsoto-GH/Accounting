using Accounting.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Accounting.API.DTOs.Account;

public class AccountPatchDto : IAccountDto
{
    [Range(1, 2)]
    public AccountType? Type { get; set; } = null;

    public string? NickName { get; set; } = null;

    public bool? Status { get; set; } = null;
}
