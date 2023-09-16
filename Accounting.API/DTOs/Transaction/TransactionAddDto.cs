using Accounting.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Accounting.API.DTOs.Transaction;

public class TransactionAddDto: ITransactionDto
{
    [Required]
    public string? Type { get; set; } = null;

    [MaxLength(200)]
    public string? Description { get; set; } = null;

    [Required]
    [Range(minimum: 0, maximum: int.MaxValue)]
    public int Amount { get; set; }
}
