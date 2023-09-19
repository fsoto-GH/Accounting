using Accounting.API.Enums;

namespace Accounting.API.DTOs.Account;

public class AccountDetailDto : AccountBaseDto
{
    public AccountDetailDto(AccountDto account)
    {
        AccountID = account.AccountID;
        Type = account.Type;
        NickName = account.NickName;
        Status = account.Status;
    }

    public int AccountID { get; set; }
    public override AccountType? Type { get; set; }
    public override string? NickName { get; set; }
    public bool? Status { get; set; }
}