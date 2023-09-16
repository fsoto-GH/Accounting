using Accounting.API.Enums;
using System.Globalization;

namespace Accounting.API.DTOs.Transaction;

public class TransactionDto : ITransactionDto
{

    public int PersonID { get; set; }
    public int TransactionID { get; set; }
    public int AccountID { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Amount { get; set; }
    public string FormattedAmount => $"${Amount / 100}.{Amount % 100:00}";
    private DateTime Date { get; set; }
    public string TransactionTime => Date.ToString("s", CultureInfo.InvariantCulture);
}
