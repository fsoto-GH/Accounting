using Accounting.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Accounting.API.DTOs.Transaction;

public class TransactionAddDto: TransactionBaseDto
{
    [Required]
    public override TransactionType? Type { get; set; }
    
    [Required]
    [MaxLength(200)]
    public override string? Description { get; set; }

    [Required]
    [Range(minimum: 0, maximum: int.MaxValue)]
    public int Amount { get; set; }
}
