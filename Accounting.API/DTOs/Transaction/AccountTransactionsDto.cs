using Accounting.API.Enums;

namespace Accounting.API.DTOs.Transaction;

public class AccountTransactionsDto
{
    public AccountTransactionsDto(int personID, int accountID, IEnumerable<TransactionDetailDto> transactions)
    {
        PersonID = personID;
        AccountID = accountID;
        Transactions = transactions;
    }

    public int PersonID { get; set; }
    
    public int AccountID { get; set; }

    public IEnumerable<TransactionDetailDto> Transactions { get; set; }

    public int NetBalance => TotalPurchases - TotalPayments;
    
    public int TotalPurchases => GetTotalOfType(TransactionType.DEBIT);

    public int TotalPayments => GetTotalOfType(TransactionType.CREDIT);

    public string FormattedNetBalance => $"${NetBalance / 100}.{NetBalance % 100:00}";

    public string FormattedTotalPurchases => $"${TotalPurchases / 100}.{TotalPurchases % 100:00}";

    public string FormattedTotalPayments => $"${TotalPayments / 100}.{TotalPayments % 100:00}";

    private int GetTotalOfType(TransactionType type) => Transactions?.Where(x => x.Type == type)?.Sum(x => x.Amount) ?? 0;
}
