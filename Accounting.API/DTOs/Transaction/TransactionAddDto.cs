using System.ComponentModel.DataAnnotations;

namespace AccountingAPI.DTOs.Transaction
{
    public class TransactionAddDto: ITransactionDto
    {
        [Required]
        public string Type { get; set; } = string.Empty;
        public string? Description { get; set; } = null;

        [Required]
        public double? Amount { get; set; } = null;
    }
}
