using System.Globalization;

namespace AccountingAPI.DTOs.Transaction;

public class TransactionDto : ITransactionDto
{
    public int PersonID { get; set; }
    public int TransactionID { get; set; }
    public int AccountID { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? Description { get; set; }
    public double? Amount { get; set; }
    public DateTime Date { get; set; }
}
