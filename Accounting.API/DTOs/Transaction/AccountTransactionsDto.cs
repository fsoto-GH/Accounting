using Accounting.API.Enums;
using System.Globalization;

namespace Accounting.API.DTOs.Transaction;

public class AccountTransactionsDto
{
    public AccountTransactionsDto(int personID, int accountID, List<TransactionDetailDto> transactions)
    {
        PersonID = personID;
        AccountID = accountID;
        Transactions = transactions;
    }

    public AccountTransactionsDto() { }

    public int PersonID { get; set; } = 0;
    public int AccountID { get; set; } = 0;
    public List<TransactionDetailDto> Transactions { get; set; } = new List<TransactionDetailDto>();
    public int NetBalance => Transactions.Sum(x => x.Amount);
    public int TotalPurchases => Transactions.Where(x => string.Equals(x.Type, nameof(TransactionType.DEBIT).ToUpperInvariant())).Sum(x => x.Amount);
    public int TotalPayments => Transactions.Where(x => string.Equals(x.Type, nameof(TransactionType.CREDIT).ToUpperInvariant())).Sum(x => x.Amount);
    public string FormattedNetBalance => $"${NetBalance / 100}.{NetBalance % 100:00}";
    public string FormattedTotalPurchases => $"${TotalPurchases / 100}.{TotalPurchases % 100:00}";
    public string FormattedTotalPayments => $"${TotalPayments / 100}.{TotalPayments % 100:00}";
}
