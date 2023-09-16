using Accounting.API.Enums;

namespace Accounting.API.DTOs.Transaction
{
    public interface ITransactionDto
    {
        public string? Description { get; set; }
    }
}
