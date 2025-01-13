using Accounting.APP.Enums;

namespace Accounting.APP;

class Transaction
{
    public int TransactionID { get; set; }
    public string Description { get; set; }
    public TransactionType Type { get; set; }
    private DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public Transaction(int id, string desc, TransactionType type, DateTime date, decimal amount)
    {
        TransactionID = id;
        Description = desc;
        Type = type;
        Amount = amount;
        Date = date;
    }

    public override string ToString()
    {
        string sign = Type == TransactionType.CREDIT ? "" : "-";
        return $"{Date:MM/dd/yyyy} | {Description,-100} | {sign,1}{Amount,10:C2}";
    }
}
