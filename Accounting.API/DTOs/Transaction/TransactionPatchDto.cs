using Accounting.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Accounting.API.DTOs.Transaction
{
    public class TransactionPatchDto: TransactionBaseDto
    {
        public override TransactionType? Type { get; set; } = null;

        public override string? Description { get; set; } = null;

        [Range(minimum: 0, maximum: int.MaxValue)]
        public int? Amount { get; set; } = null;
    }
}
