
GO
SET IDENTITY_INSERT [Accounting].[dbo].[Persons] ON

INSERT INTO [Accounting].[dbo].[Persons] (PersonID, FirstName, LastName, MiddleName)
SELECT PersonID, FirstName, LastName, MiddleName
FROM [database_name].[dbo].[Persons]

SET IDENTITY_INSERT [Accounting].[dbo].[Persons] OFF

GO
DECLARE @checkingsTypeID int = (SELECT AccountTypeID FROM [Accounting].[dbo].[AccountTypes] WHERE Name = 'CHECKING')
DECLARE @savingsTypeID int = (SELECT AccountTypeID FROM [Accounting].[dbo].[AccountTypes] WHERE Name = 'SAVINGS')

SET IDENTITY_INSERT [Accounting].[dbo].[Accounts] ON

INSERT INTO [Accounting].[dbo].[Accounts](AccountID, PersonID, AccountTypeID, NickName, Status)
SELECT 
	AccountID
	, PersonID
	, CASE WHEN Type = 'CHECKING' THEN @checkingsTypeID ELSE @savingsTypeID END
	, NickName
	, Status
FROM [database_name].[dbo].[Accounts]

SET IDENTITY_INSERT [Accounting].[dbo].[Accounts] OFF

GO
DECLARE @debitTypeID int = (SELECT TransactionTypeID FROM [Accounting].[dbo].[TransactionTypes] WHERE Name = 'DEBIT')
DECLARE @creditType int = (SELECT TransactionTypeID FROM [Accounting].[dbo].[TransactionTypes] WHERE Name = 'CREDIT')

SET IDENTITY_INSERT [Accounting].[dbo].[Transactions] ON

INSERT INTO [Accounting].[dbo].[Transactions](TransactionID, AccountID, TransactionTypeID, Description, Amount, Date)
SELECT 
	TransactionID
	, AccountID
	, CASE WHEN Type = 'DEBIT' THEN @debitTypeID ELSE @creditType END
	, Description
	, Amount
	, Date
FROM [database_name].[dbo].[Transactions]

SET IDENTITY_INSERT [Accounting].[dbo].[Transactions] OFF

