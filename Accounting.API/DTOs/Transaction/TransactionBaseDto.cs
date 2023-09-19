using Accounting.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Accounting.API.DTOs.Transaction
{
    public class TransactionBaseDto
    {
        [MaxLength(200)]
        public virtual string? Description { get; set; }

        public virtual TransactionType? Type { get; set; }
    }
}
