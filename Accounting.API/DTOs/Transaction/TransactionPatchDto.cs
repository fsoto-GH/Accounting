namespace AccountingAPI.DTOs.Transaction
{
    public class TransactionPatchDto: ITransactionDto
    {
        public string Type { get; set; } = string.Empty;
        public string? Description { get; set; } = null;
        public double? Amount { get; set; } = null;
    }
}
