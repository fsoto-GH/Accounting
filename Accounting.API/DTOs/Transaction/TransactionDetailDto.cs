using System.Globalization;

namespace AccountingAPI.DTOs.Transaction;

public class TransactionDetailDto : ITransactionDto
{
    public int TransactionID { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? Description { get; set; }
    public double? Amount { get; set; }
    public DateTime Date { get; set; }
}
