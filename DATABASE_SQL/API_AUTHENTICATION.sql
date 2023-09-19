CREATE TABLE PersonTokens
(
	TokenID int IDENTITY(1, 1) NOT NULL,
	PersonID int NOT NULL,
	Token varchar(255) NOT NULL,
	ExpiryDate datetime NOT NULL CONSTRAINT DF_PersonTokens_ExpiryDate DEFAULT DATEADD(day, 30, GETUTCDATE()),
	CONSTRAINT PK_PersonTokens_TokenID PRIMARY KEY CLUSTERED(TokenID),
	CONSTRAINT FK_PersonTokens_Persons FOREIGN KEY (PersonID) REFERENCES Persons(PersonID) ON DELETE CASCADE,
)

-- MODIFY existing default transaction date since we are moving torwards UTC
ALTER TABLE Transactions 
DROP CONSTRAINT DF_Transaction_Date

ALTER TABLE Transactions 
ADD CONSTRAINT DF_Transaction_Date 
DEFAULT (GETUTCDATE()) FOR Date


-- ADD username column to Persons table, make usernames unique to add UNIQUE constraint and index to boost Login logic.
ALTER TABLE Persons 
ADD UserName nvarchar(100)

UPDATE Persons
SET UserName = TRIM(CAST(PersonID AS nchar)) + LEFT(FirstName + LastName, 100 - LEN(PersonID))

CREATE UNIQUE INDEX UX_Persons_UserName ON Persons(UserName) WHERE UserName IS NOT NULL

ALTER TABLE Persons
ADD CONSTRAINT UQ_Persons_UserName UNIQUE (UserName)

-- ADD fields for password hash and salt
ALTER TABLE Persons 
ADD PasswordHash binary(60)

ALTER TABLE Persons
ADD PasswordSalt binary(16)

CREATE TABLE ClaimTypes 
(
	ClaimTypeID int IDENTITY(1, 1) NOT NULL,
	ClaimName char(50) NOT NULL,
	CONSTRAINT PK_ClaimTypes_ClaimTypeID PRIMARY KEY CLUSTERED (ClaimTypeID)
)

CREATE TABLE Claims
(
	ClaimID int IDENTITY(1, 1) NOT NULL,
	ClaimTypeID int NOT NULL,
	ClaimValue char(50) NOT NULL,
	CONSTRAINT PK_Claims_ClaimID PRIMARY KEY CLUSTERED (ClaimID),
	CONSTRAINT FK_Claims_ClaimTypes FOREIGN KEY (ClaimTypeID) REFERENCES ClaimTypes(ClaimTypeID)
)

CREATE TABLE PersonClaims
(
	PersonID int NOT NULL,
	ClaimID int NOT NULL,
	CONSTRAINT PK_PersonClaims_PersonID_ClaimID PRIMARY KEY CLUSTERED(PersonID, ClaimID),
	CONSTRAINT FK_PersonClaims_Person FOREIGN KEY (PersonID) REFERENCES Persons(PersonID),
	CONSTRAINT FK_PersonClaims_Claims FOREIGN KEY (ClaimID) REFERENCES Claims(ClaimID)
)

