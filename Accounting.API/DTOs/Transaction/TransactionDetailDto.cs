using Accounting.API.Enums;

namespace Accounting.API.DTOs.Transaction;


/// <summary>
/// Model representation of a transaction.
/// </summary>
public class TransactionDetailDto : TransactionBaseDto
{
    /// <summary>
    /// The unique identifier for this transaction.
    /// </summary>
    public int TransactionID { get; set; }

    /// <summary>
    /// The amount in cents of this transaction.
    /// $1.23 = 123
    /// </summary>
    public int Amount { get; set; }
    
    /// <summary>
    /// The standing balance of the account after this transaction.
    /// </summary>
    public int RollingBalance { get; set; }

    /// <summary>
    /// For display purposes only, this displays the dollar amount.
    /// </summary>
    public string FormattedAmount => $"{(Type == TransactionType.DEBIT? "-":"")}${Amount / 100.00:N}";


    /// <summary>
    /// For display purposes only, this displays the dollar amount.
    /// </summary>
    public string FormattedRollingBalance => $"{(RollingBalance < 0 ? "-" : "")}${Math.Abs(RollingBalance) / 100.00:N}";

    /// <summary>
    /// The date and time in which the transaction occurred.
    /// </summary>
    public DateTime Date { get; set; }
}
