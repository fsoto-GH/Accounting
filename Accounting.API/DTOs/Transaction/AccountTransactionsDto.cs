using Accounting.API.Enums;

namespace Accounting.API.DTOs.Transaction;

public class AccountTransactionsDto
{
    public AccountTransactionsDto(int personID, int accountID, int applicableTransactionCount, IEnumerable<TransactionDetailDto> transactions)
    {
        PersonID = personID;
        AccountID = accountID;
        Transactions = transactions;
        ApplicableTransactionCount = applicableTransactionCount;
    }

    public int PersonID { get; set; }
    
    public int AccountID { get; set; }

    public int ApplicableTransactionCount { get; set; }

    public IEnumerable<TransactionDetailDto> Transactions { get; set; }

    public int TotalPurchases => GetTotalOfType(TransactionType.DEBIT);

    public int TotalPayments => GetTotalOfType(TransactionType.CREDIT);

    public string FormattedTotalPurchases => $"${TotalPurchases / 100.00:N}";

    public string FormattedTotalPayments => $"${TotalPayments / 100.00:N}";

    private int GetTotalOfType(TransactionType type) => Transactions?.Where(x => x.Type == type)?.Sum(x => x.Amount) ?? 0;
}
