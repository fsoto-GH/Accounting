using System.Globalization;

namespace Accounting.API.DTOs.Transaction;

public class TransactionDetailDto : ITransactionDto
{
    public int TransactionID { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int Amount { get; set; }
    public string FormattedAmount => $"${Amount / 100}.{Amount % 100:00}";
    public DateTime Date { get; set; }
}
