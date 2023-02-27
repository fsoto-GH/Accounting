USE Accounting

GO
CREATE OR ALTER TRIGGER TR_Status_Change
ON Accounts
FOR UPDATE
AS
	DECLARE @newStatus bit
	DECLARE @id int

	SELECT @newStatus = i.Status, @id = i.AccountID
	FROM inserted i

	IF @newStatus = 0 
	BEGIN
		DECLARE @netBalance decimal(18, 2)
		SELECT
			@netBalance = ISNULL(SUM(CASE WHEN T.Type = 'Debit' THEN -T.Amount ELSE T.Amount END), 0)
		FROM 
			Accounts A INNER JOIN Transactions T
			ON A.AccountID = T.AccountID
		WHERE A.AccountID = @id

		IF @netBalance != 0
		BEGIN
			RAISERROR('Account has a balance.', 16, 1)
			ROLLBACK
		END
	END