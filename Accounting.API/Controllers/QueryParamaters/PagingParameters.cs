using System.ComponentModel.DataAnnotations;

namespace Accounting.API.Controllers.QueryParamaters;

public class PagingParameters
{
    /// <summary>
    /// Paging support. Get records between (Size * Page, Size * (Page + 1)].
    /// Example: (Page = 1, Size = 10) => Records in range of (10 * 0, 10 * (0 + 1)] = (0, 10]
    /// That is to say; 1 - 10.
    /// </summary>
    [Range(minimum: 1, maximum:int.MaxValue, ErrorMessage ="Page number must be non-negative.")]
    public int Page { get; set; } = 1;

    /// <summary>
    /// This is the number of max records to return.
    /// </summary>
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Size number must be non-negative.")]
    public int Size { get; set; } = 10;
}
