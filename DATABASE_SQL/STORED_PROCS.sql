USE Accounting

GO
CREATE OR ALTER PROC usp_ViewPersonAccounts
	@personID int,
	@netBalance decimal(10, 2) out,
	@accountCount int out
AS
BEGIN
	SET NOCOUNT ON
	IF EXISTS (SELECT TOP 1 1 FROM Persons WHERE PersonID = @personID)
	BEGIN
		SELECT 
			A.AccountID [AccountID]
			, NickName [NickName]
			, Type [Type]
			, Status [Status]
		FROM 
			Persons P INNER JOIN Accounts A ON P.PersonID = A.PersonID
		WHERE 
			P.PersonID =  @personID
		ORDER BY Status DESC

		SELECT
			@accountCount = COUNT(Result.AccountID)
			, @netBalance = ISNULL(SUM(Result.NetAccountBalance), 0)
		FROM (
			SELECT 
				A.AccountID [AccountID]
				, SUM((CASE T.Type WHEN 'DEBIT' THEN -1 ELSE 1 END) * ISNULL(AMOUNT, 0)) [NetAccountBalance]
			FROM 
					Persons P 
					INNER JOIN Accounts A ON P.PersonID = A.PersonID
					LEFT OUTER JOIN Transactions T ON A.AccountID = T.AccountID	
			WHERE P.PersonID = @personID
			GROUP BY A.AccountID, P.PersonID
		) [Result]
		HAVING COUNT(Result.AccountID) >= 0
	END
END

GO
CREATE OR ALTER PROC usp_ViewAccountTransactions
	@accountID int,
	@personID int,
	@netBalance decimal(10, 2) out,
	@totalPayments decimal(10, 2) out,
	@totalPurchases decimal(10, 2) out
AS
BEGIN
	SET NOCOUNT ON
	IF EXISTS (SELECT TOP 1 1 FROM Accounts WHERE AccountID = @accountID AND PersonID = @personID)
	BEGIN
		SELECT
			TransactionID [TransactionID]
			, Description [Description]Acc
			, T.Type [Type]
			, Date [Date]
			, Amount [Amount]
		FROM
			Accounts A INNER JOIN Transactions T ON A.AccountID = T.AccountID
		WHERE
			A.AccountID = @accountID AND A.PersonID = @personID
		ORDER BY Date DESC


		SELECT
			@totalPayments = ISNULL(SUM(CASE T.Type WHEN 'CREDIT' THEN AMOUNT ELSE 0 END), 0),
			@totalPurchases = ISNULL(SUM(CASE T.Type WHEN 'DEBIT' THEN AMOUNT ELSE 0 END), 0),
			@netBalance = ISNULL(SUM(CASE T.Type WHEN 'DEBIT' THEN -AMOUNT ELSE AMOUNT END), 0)
		FROM
			Accounts A JOIN Transactions T ON A.AccountID = T.AccountID
		WHERE
			A.AccountID = @accountID
	END
END