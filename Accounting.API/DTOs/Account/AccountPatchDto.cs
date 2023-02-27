namespace AccountingAPI.DTOs.Account;

public class AccountPatchDto : IAccountDto
{

    public string Type { get; set; } = string.Empty;

    public string? NickName { get; set; } = string.Empty;

    public bool? Status { get; set; } = null;
}
