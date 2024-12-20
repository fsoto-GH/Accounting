namespace Accounting.API.DTOs.Account;

public class AccountDto : AccountBaseDto
{
    public int TotalPurchases { get; set; }

    public int TotalPayments { get; set; }

    public int NetBalance { get; set; }

    public int CountOfTransactions { get; set; }

    public int AccountID { get; set; }

    public int PersonID { get; set; }

    public bool Status { get; set; }

    public string FormattedNetBalance => $"{(NetBalance < 0 ? "-" : "")}${Math.Abs(NetBalance) / 100.00:N}";

    public string FormattedTotalPurchases => $"${TotalPurchases / 100.00:N}";

    public string FormattedTotalPayments => $"${TotalPayments / 100.00:N}";
}
