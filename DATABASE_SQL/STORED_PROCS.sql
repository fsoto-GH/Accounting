USE Accounting

GO
CREATE OR ALTER PROC usp_ViewPersonAccounts
	@personID int,
	@netBalance decimal(19, 4) out,
	@accountCount int out
AS
BEGIN
	SET NOCOUNT ON
	IF EXISTS (SELECT TOP 1 1 FROM Persons WHERE PersonID = @personID)
	BEGIN
		SELECT 
			A.AccountID [AccountID]
			, NickName [NickName]
			, AT.Name [Type]
			, Status [Status]
		FROM 
			Persons P INNER JOIN Accounts A ON P.PersonID = A.PersonID
			INNER JOIN AccountTypes AT ON A.AccountTypeID = AT.AccountTypeID
		WHERE 
			P.PersonID =  @personID
		ORDER BY Status DESC

		DECLARE @debitTransID int
		SELECT @debitTransID = TransactionTypeID FROM TransactionTypes WHERE Name = 'DEBIT'	
		SELECT
			@accountCount = COUNT(Result.AccountID)
			, @netBalance = ISNULL(SUM(Result.NetAccountBalance), 0)
		FROM (
			SELECT 
				A.AccountID [AccountID]
				, SUM((CASE T.TransactionTypeID WHEN @debitTransID THEN -1 ELSE 1 END) * ISNULL(AMOUNT, 0)) [NetAccountBalance]
			FROM 
					Persons P 
					INNER JOIN Accounts A ON P.PersonID = A.PersonID
					LEFT OUTER JOIN Transactions T ON A.AccountID = T.AccountID	
					LEFT OUTER JOIN TransactionTypes TT ON T.TransactionTypeID = TT.TransactionTypeID
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
	@netBalance decimal(19, 4) out,
	@totalPayments decimal(19, 4) out,
	@totalPurchases decimal(19, 4) out
AS
BEGIN
	SET NOCOUNT ON
	IF EXISTS (SELECT TOP 1 1 FROM Accounts WHERE AccountID = @accountID AND PersonID = @personID)
	BEGIN
		SELECT
			TransactionID [TransactionID]
			, Description [Description]
			, TT.Name [Type]
			, Date [Date]
			, Amount [Amount]
		FROM
			Accounts A INNER JOIN Transactions T ON A.AccountID = T.AccountID
			INNER JOIN TransactionTypes TT ON T.TransactionTypeID = TT.TransactionTypeID
		WHERE
			A.AccountID = @accountID AND A.PersonID = @personID
		ORDER BY Date DESC


		DECLARE @creditTypeID int, @debitTypeID int
		SELECT @creditTypeID = TransactionTypeID FROM TransactionTypes WHERE Name = 'CREDIT'
		SELECT @debitTypeID TransactionTypeID FROM TransactionTypes WHERE Name = 'DEBIT'

		SELECT
			@totalPayments = ISNULL(SUM(CASE T.TransactionTypeID WHEN @creditTypeID THEN AMOUNT ELSE 0 END), 0),
			@totalPurchases = ISNULL(SUM(CASE T.TransactionTypeID WHEN @debitTypeID THEN AMOUNT ELSE 0 END), 0),
			@netBalance = ISNULL(SUM(CASE T.TransactionTypeID WHEN @debitTypeID THEN -AMOUNT ELSE AMOUNT END), 0)
		FROM
			Accounts A JOIN Transactions T ON A.AccountID = T.AccountID
			INNER JOIN TransactionTypes TT ON T.TransactionTypeID = TT.TransactionTypeID
		WHERE
			A.AccountID = @accountID
	END
END

GO 
CREATE OR ALTER PROC usp_DeletePerson
	@personID int,
	@forceClose bit = 0
AS
BEGIN
	SET NOCOUNT ON
	IF EXISTS (SELECT TOP 1 1 FROM Persons WHERE PersonID = @personID)
	BEGIN
		DECLARE @debitTypeID int
		SET @debitTypeID = (SELECT TransactionTypeID FROM TransactionTypes WHERE Name = 'DEBIT')

		DECLARE @netBalance decimal(19, 4) = NULL
		SELECT @netBalance = ISNULL(SUM(CASE T.TransactionTypeID WHEN @debitTypeID THEN -AMOUNT ELSE AMOUNT END), 0)
		FROM 
			Accounts A INNER JOIN Transactions T ON A.AccountID = T.AccountID
		WHERE
			A.PersonID = @personID
		GROUP BY A.AccountID

		IF (@netBalance IS NULL OR @netBalance = 0 OR @forceClose = 1)
		BEGIN
			DELETE FROM Transactions
			WHERE AccountID in (SELECT AccountID FROm Accounts WHERE PersonID = @personID)

			DELETE FROM Accounts
			WHERE PersonID = @personID

			DELETE FROM Persons
			WHERE PersonID = @personID
		END
		ELSE
		BEGIN
			RAISERROR('Person has existing balances. To force close fulfill @forceClose.', 16, 1)
		END
	END
END
