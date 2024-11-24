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
            
            return $@"
SELECT *
INTO #FilteredTransactions
FROM 
	(SELECT 
		T.TransactionID [{nameof(TransactionDetailDto.TransactionID)}] 
		, T.Date [{nameof(TransactionDetailDto.Date)}] 
		, T.Description [{nameof(TransactionDetailDto.Description)}]
		, TT.Name [{nameof(TransactionDetailDto.Type)}] 
		, CAST(T.Amount * 100 as int) [{nameof(TransactionDetailDto.Amount)}]
		, CAST(SUM(CASE WHEN T.TransactionTypeID = 1 THEN -T.Amount ELSE 1 * T.Amount END) OVER (ORDER BY T.Date ASC) * 100 as int)  [{nameof(TransactionDetailDto.RollingBalance)}]
	FROM
		[dbo].[Persons] P INNER JOIN [dbo].[Accounts] A ON P.PersonID = A.PersonID 
		INNER JOIN [dbo].[Transactions] T ON A.AccountID = T.AccountID 
		INNER JOIN [dbo].[TransactionTypes] TT ON T.TransactionTypeID = TT.TransactionTypeID 
	WHERE 
		P.PersonID = @personID
		AND A.AccountID = @accountID
	) as PersonAccounTransactions
{(string.Equals(whereClause, string.Empty) ? "" : $"WHERE {whereClause}")} 

SELECT COUNT([{nameof(TransactionDetailDto.TransactionID)}]) [{nameof(AccountTransactionsDto.ApplicableTransactionCount)}]
FROM #FilteredTransactions

-- this is the returned rows, need to page
SELECT *
FROM #FilteredTransactions
ORDER BY
     [{nameof(TransactionDetailDto.Date)}]  DESC

{GetPagingClause(queryParameters)}

DROP TABLE IF EXISTS #FilteredTransactions";
        }

        private static string GetComplexWhereClause(TransactionQueryParameters queryParameters)
        {
            string[] whereConditions = [
                (queryParameters.StartDate.HasValue ? $"[{nameof(TransactionDetailDto.Date)}] >= @{nameof(queryParameters.StartDate)}" : string.Empty),
                (queryParameters.EndDate.HasValue ? $"[{nameof(TransactionDetailDto.Date)}] <= @{nameof(queryParameters.EndDate)}" : string.Empty),
                (queryParameters.MinAmount.HasValue ? $"[{nameof(TransactionDetailDto.Amount)}] >= @{nameof(queryParameters.MinAmount)}" : string.Empty),
                (queryParameters.MaxAmount.HasValue ? $"[{nameof(TransactionDetailDto.Amount)}] <= @{nameof(queryParameters.MaxAmount)}" : string.Empty),
                (queryParameters.NameQuery is not null ? $"[{nameof(TransactionDetailDto.Description)}] LIKE @{nameof(queryParameters.NameQuery)}" : string.Empty)
            ];

            return string.Join("\n\tAND", whereConditions.Where(x => !string.Equals(x, string.Empty)));
        }

        private static string GetPagingClause(TransactionQueryParameters queryParameters)
        {
            return $@"
OFFSET ({(queryParameters.Page - 1) * queryParameters.Size}) ROWS 
FETCH NEXT { queryParameters.Size} ROWS ONLY";
        }
    }
}
