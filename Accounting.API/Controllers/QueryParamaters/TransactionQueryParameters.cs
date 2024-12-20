using System.ComponentModel.DataAnnotations;

namespace Accounting.API.Controllers.QueryParamaters;

public class TransactionQueryParameters: PagingParameters
{
    private string? _nameQuery;
    public string? NameQuery { get
        {
            return _nameQuery;
        } set { 
            _nameQuery = $"%{value}%";
        }
    }

    [Range(minimum: 0, maximum: int.MaxValue)]
    public int? MinAmount { get; set; }

    [Range(minimum: 0, maximum: int.MaxValue)]
    public int? MaxAmount { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}
