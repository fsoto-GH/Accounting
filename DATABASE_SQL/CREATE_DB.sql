USE master

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
CREATE TABLE Accounts
(
	AccountID int IDENTITY(1, 1) NOT NULL,
	PersonID int NOT NULL,
	Type varchar(10) NULL CONSTRAINT DF_Accounts_Type DEFAULT 'CHECKING',
	NickName nvarchar(100) NULL CONSTRAINT DF_Accounts_NickName DEFAULT NULL,
	Status bit NOT NULL CONSTRAINT DF_Accounts_Status DEFAULT 1,
	CONSTRAINT PK_Accounts_PersonID PRIMARY KEY CLUSTERED (AccountID),
	CONSTRAINT FK_Accounts_Person FOREIGN KEY (PersonID) REFERENCES Persons(PersonID),
)

GO
CREATE TABLE Transactions
(
	TransactionID int IDENTITY(1, 1) NOT NULL,
	AccountID int NOT NULL,
	Type varchar(10) NOT NULL CONSTRAINT CK_Transactions_Type CHECK (Type in('CREDIT', 'DEBIT')),
	Description nvarchar(200) NULL CONSTRAINT DF_Transactions_Description DEFAULT NULL,
	Amount decimal(10, 2) NOT NULL CONSTRAINT CK_Transactions_Amount CHECK (Amount >= 0),
	Date datetime NOT NULL CONSTRAINT DF_Transaction_Date DEFAULT getdate()
	CONSTRAINT PK_Transactions_PersonID PRIMARY KEY CLUSTERED (TransactionID),
	CONSTRAINT FK_Transactions_Accounts FOREIGN KEY (AccountID) REFERENCES Accounts(AccountID)
)

GO
USE master