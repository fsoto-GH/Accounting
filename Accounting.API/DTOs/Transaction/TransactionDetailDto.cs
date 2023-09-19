namespace Accounting.API.DTOs.Transaction;

public class TransactionDetailDto : TransactionBaseDto
{
    public int TransactionID { get; set; }

    public int Amount { get; set; }

    public string FormattedAmount => $"${Amount / 100}.{Amount % 100:00}";
    public DateTime Date { get; set; }
}
