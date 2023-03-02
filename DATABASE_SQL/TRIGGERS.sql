USE Accounting

GO
CREATE OR ALTER TRIGGER TR_Status_Change
ON Accounts
FOR UPDATE
AS
	DECLARE @newStatus bit
	DECLARE @id int, @debitTypeID int

	SELECT @newStatus = i.Status, @id = i.AccountID
	FROM inserted i

	SELECT @debitTypeID = TransactionTypeID FROM TransactionTypes WHERE Name = 'DEBIT'

	-- check that balance is 0 before closing account
	IF @newStatus = 0 
	BEGIN
		DECLARE @netBalance decimal(18, 2)
		SELECT
			@netBalance = ISNULL(SUM(CASE WHEN T.TransactionTypeID = @debitTypeID THEN -T.Amount ELSE T.Amount END), 0)
		FROM 
			Accounts A INNER JOIN Transactions T ON A.AccountID = T.AccountID
			INNER JOIN TransactionTypes TT ON T.TransactionTypeID = TT.TransactionTypeID

		WHERE A.AccountID = @id

		IF @netBalance != 0
		BEGIN
			RAISERROR('Account has a balance.', 16, 1)
			ROLLBACK
		END
	END

	-- Trigger to delete a person.
	-- Either execute the close account USP to ensure there is no balance or implement an alternative solution
	-- UPDATE Referential integrity on Person FK to Cascade