using System.Globalization;

namespace AccountingAPI.DTOs.Transaction
{
    public interface ITransactionDto
    {
        public string Type { get; set; }
        public string? Description { get; set; }
        public double? Amount { get; set; }
        public string FormattedAmount => Amount?.ToString("C", CultureInfo.GetCultureInfo("en-us")) ?? string.Empty;
    }
}
