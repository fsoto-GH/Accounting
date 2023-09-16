using Accounting.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Accounting.API.DTOs.Transaction
{
    public class TransactionPatchDto: ITransactionDto
    {
        public string? Type { get; set; } = null;

        [MaxLength(200)]
        public string? Description { get; set; } = null;

        [Range(minimum: 0, maximum:int.MaxValue)]
        public int? Amount { get; set; } = null;
    }
}
