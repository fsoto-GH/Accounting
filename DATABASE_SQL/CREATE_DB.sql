--USE master

GO
DROP DATABASE IF EXISTS Accounting

GO
CREATE DATABASE Accounting

GO
USE Accounting

GO
CREATE TABLE Persons
(
	PersonID int IDENTITY(1, 1) NOT NULL,
	FirstName nvarchar(50) NOT NULL CONSTRAINT CK_Persons_FirstName CHECK(LEN(TRIM(FirstName)) != 0),
	LastName nvarchar(50) NOT NULL CONSTRAINT CK_Persons_LastName CHECK(LEN(TRIM(LastName)) != 0),
	MiddleName nvarchar(50) NULL CONSTRAINT DF_Persons_MiddleName DEFAULT NULL,
	CONSTRAINT PK_Persons_PersonID PRIMARY KEY CLUSTERED (PersonID)
)

GO
CREATE TABLE AccountTypes (
	AccountTypeID int IDENTITY(1, 1) NOT NULL,
	Name varchar(20) NOT NULL,
	CONSTRAINT PK_AccountTypes_AccountType PRIMARY KEY CLUSTERED (AccountTypeID),
)

GO
CREATE TABLE Accounts
(
	AccountID int IDENTITY(1, 1) NOT NULL,
	PersonID int NOT NULL,
	AccountTypeID int NOT NULL,
	NickName nvarchar(100) NULL CONSTRAINT DF_Accounts_NickName DEFAULT NULL,
	Status bit NOT NULL CONSTRAINT DF_Accounts_Status DEFAULT 1,
	CONSTRAINT PK_Accounts_Account PRIMARY KEY CLUSTERED (AccountID),
	CONSTRAINT FK_Accounts_Person FOREIGN KEY (PersonID) REFERENCES Persons(PersonID),
	CONSTRAINT FK_Accounts_AccountType FOREIGN KEY (AccountTypeID) REFERENCES AccountTypes(AccountTypeID),
)

GO
CREATE TABLE TransactionTypes
(
	TransactionTypeID int IDENTITY(1, 1) NOT NULL,
	Name varchar(20) NOT NULL,
	CONSTRAINT PK_TransactionTypes_TransactionType PRIMARY KEY CLUSTERED (TransactionTypeID),
)

GO
CREATE TABLE Transactions
(
	TransactionID int IDENTITY(1, 1) NOT NULL,
	AccountID int NOT NULL,
	TransactionTypeID int NOT NULL,
	Description nvarchar(200) NULL CONSTRAINT DF_Transactions_Description DEFAULT NULL,
	Amount decimal(19, 4) NOT NULL CONSTRAINT CK_Transactions_Amount CHECK (Amount >= 0),
	Date datetime NOT NULL CONSTRAINT DF_Transaction_Date DEFAULT getdate()
	CONSTRAINT PK_Transactions_TransactionID PRIMARY KEY CLUSTERED (TransactionID),
	CONSTRAINT FK_Transactions_Accounts FOREIGN KEY (AccountID) REFERENCES Accounts(AccountID) ON DELETE CASCADE,
	CONSTRAINT FK_Transactions_TransactionType FOREIGN KEY (TransactionTypeID) REFERENCES TransactionTypes(TransactionTypeID)
)

GO
INSERT INTO AccountTypes (Name)
VALUES ('CHECKING'), ('SAVINGS')

GO
INSERT INTO TransactionTypes (Name)
VALUES ('CREDIT'), ('DEBIT')

