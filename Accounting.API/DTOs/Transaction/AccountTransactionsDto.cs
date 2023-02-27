using System.Globalization;

namespace AccountingAPI.DTOs.Transaction;

public class AccountTransactionsDto
{
    public AccountTransactionsDto(int personID, int accountID, IEnumerable<TransactionDetailDto> transactions, double netBalance, double totalPurchases, double totalPayments)
    {
        PersonID = personID;
        AccountID = accountID;
        Transactions = transactions;
        FormattedNetBalance = netBalance.ToString("C", CultureInfo.GetCultureInfo("en-us"));
        FormattedTotalPurchases = totalPurchases.ToString("C", CultureInfo.GetCultureInfo("en-us"));
        FormattedTotalPayments = totalPayments.ToString("C", CultureInfo.GetCultureInfo("en-us"));
    }

    public int PersonID { get; set; }
    public int AccountID { get; set; }

    public IEnumerable<TransactionDetailDto> Transactions { get; set; } = new List<TransactionDetailDto>();
    public string FormattedNetBalance { get; set; } = string.Empty;
    public string FormattedTotalPurchases { get; set; } = string.Empty;
    public string FormattedTotalPayments { get; set; } = string.Empty;
}
