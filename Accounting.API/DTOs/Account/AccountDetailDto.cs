namespace AccountingAPI.DTOs.Account;

public class AccountDetailDto : IAccountDto
{
    public AccountDetailDto(AccountDto account)
    {
        AccountID = account.AccountID;
        Type = account.Type;
        NickName = account.NickName;
        Status = account.Status;
    }

    public int AccountID { get; set; } = 0;
    public string Type { get; set; }
    public string? NickName { get; set; }
    public bool Status { get; set; }
}