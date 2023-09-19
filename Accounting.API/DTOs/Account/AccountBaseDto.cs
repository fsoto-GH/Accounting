using Accounting.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Accounting.API.DTOs.Account;

public class AccountBaseDto
{
    [MaxLength(100)]
    public virtual string? NickName { get; set; }

    public virtual AccountType? Type { get; set; }

    public void TrimNames()
    {
        NickName = NickName?.Trim();
    }
}
