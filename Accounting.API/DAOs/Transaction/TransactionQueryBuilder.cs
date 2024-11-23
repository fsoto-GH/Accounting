using Accounting.API.Controllers.QueryParamaters;
using Accounting.API.DTOs.Transaction;
using Accounting.API.Enums;

namespace Accounting.API.DAOs.Transaction
{
    public static class TransactionQueryBuilder
    {
        public static string GetAllAsync(TransactionQueryParameters queryParameters)
        {

            string whereClause = GetComplexWhereClause(queryParameters);
            
            return
$@"
SELECT 
    T.TransactionID [{nameof(TransactionDetailDto.TransactionID)}] 
    , T.Date [{nameof(TransactionDetailDto.Date)}] 
    , T.Description [{nameof(TransactionDetailDto.Description)}]
    , TT.Name [{nameof(TransactionDetailDto.Type)}] 
    , CAST(T.Amount * 100 as int) [{nameof(TransactionDetailDto.Amount)}]
    , CAST(SUM(CASE WHEN T.TransactionTypeID = {(int)TransactionType.DEBIT} THEN -T.Amount ELSE 1 * T.Amount END) OVER (ORDER BY T.Date ASC) * 100 as int)  [{nameof(TransactionDetailDto.RollingBalance)}]
INTO #FilteredTransactions
FROM  
    [dbo].[Persons] P INNER JOIN [dbo].[Accounts] A ON P.PersonID = A.PersonID 
    INNER JOIN [dbo].[Transactions] T ON A.AccountID = T.AccountID 
    INNER JOIN [dbo].[TransactionTypes] TT ON T.TransactionTypeID = TT.TransactionTypeID 
WHERE 
    P.PersonID = @personID
    AND A.AccountID = @accountID {(string.Equals(whereClause, string.Empty) ? "" : $"\n    {whereClause}")} 

SELECT 
    COUNT([{nameof(TransactionDetailDto.TransactionID)}]) [{nameof(AccountTransactionsDto.ApplicableTransactionCount)}]
FROM 
    #FilteredTransactions

SELECT *
FROM
    #FilteredTransactions
ORDER BY
    Date DESC
{GetPagingClause(queryParameters)}

DROP TABLE IF EXISTS #FilteredTransactions";
        }

        private static string GetComplexWhereClause(TransactionQueryParameters queryParameters)
        {
            string[] whereConditions = new string[] {
                (queryParameters.StartDate.HasValue ? $"AND T.Date >= @{nameof(queryParameters.StartDate)}" : string.Empty),
                (queryParameters.EndDate.HasValue ? $"AND T.Date <= @{nameof(queryParameters.EndDate)}" : string.Empty),
                (queryParameters.MinAmount.HasValue ? $"AND CAST(T.Amount * 100 as int) >= @{nameof(queryParameters.MinAmount)}" : string.Empty),
                (queryParameters.MaxAmount.HasValue ? $"AND CAST(T.Amount * 100 as int) <= @{nameof(queryParameters.MaxAmount)}" : string.Empty),
                (queryParameters.NameQuery is not null ? $"AND T.Description LIKE @{nameof(queryParameters.NameQuery)}" : string.Empty)
            };

            return string.Join("\n\t", whereConditions.Where(x => !string.Equals(x, string.Empty)));
        }

        private static string GetPagingClause(TransactionQueryParameters queryParameters)
        {
            return $"OFFSET ({(queryParameters.Page - 1) * queryParameters.Size}) ROWS \nFETCH NEXT { queryParameters.Size} ROWS ONLY";
        }
    }
}
