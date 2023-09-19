using Accounting.API.Enums;

namespace Accounting.API.DTOs.Account;

public class AccountPatchDto : AccountBaseDto
{
    public override AccountType? Type { get; set; } = null;

    public override string? NickName { get; set; } = null;

    public bool? Status { get; set; } = null;
}
