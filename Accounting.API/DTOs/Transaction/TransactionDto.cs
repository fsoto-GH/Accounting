using Accounting.API.Enums;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Accounting.API.DTOs.Transaction;

public class TransactionDto : TransactionBaseDto
{
    public int PersonID { get; set; }

    public int TransactionID { get; set; }

    public int AccountID { get; set; }

    public DateTime Date { get; set; }

    [Range(minimum: 0, maximum: int.MaxValue)]
    public int Amount { get; set; }

    public string FormattedAmount => $"{Magnitude()}${Amount / 100}.{Amount % 100:00}";

    public string TransactionTime => Date.ToString("s", CultureInfo.InvariantCulture);

    private string Magnitude()
    {
        switch (Type)
        {
            case TransactionType.DEBIT:
                return "-";
            default:
                return string.Empty;
        }
    }
}
