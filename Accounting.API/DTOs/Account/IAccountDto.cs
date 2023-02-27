namespace AccountingAPI.DTOs.Account;

public interface IAccountDto
{
    public string Type { get; set; }

    public string? NickName { get; set; }

    public void TrimType()
    {
        Type = Type?.Trim() ?? string.Empty;
    }

    public void TrimNickName()
    {
        Type = NickName?.Trim() ?? string.Empty;
    }
}
