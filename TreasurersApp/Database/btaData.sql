DROP TABLE [dbo].[ContributorAddress];
DROP TABLE [dbo].[AddressType];
DROP TABLE [dbo].[ContributorEmailAddress];
DROP TABLE [dbo].[EmailType]
DROP TABLE [dbo].[ContributorPhoneNumber];
DROP TABLE [dbo].[PhoneType];
DROP TABLE [dbo].[PhoneNumber];
DROP TABLE [dbo].[Address];
DROP TABLE [dbo].[EmailAddress];
DROP TABLE [dbo].[CashJournal];
DROP TABLE [dbo].[TransactionType];
DROP TABLE [dbo].[TransactionCategory];
DROP TABLE [dbo].[Contributor];
DROP TABLE [dbo].[Report];
DROP TABLE [Security].[History];
DROP TABLE [Security].[UserClaim];
DROP TABLE [Security].[User];
DROP TABLE [Security].[Claim];
GO

CREATE TABLE [dbo].[Address] (
    [AddressID]           INT            IDENTITY (1, 1) NOT NULL,
    [AddressLine1] NVARCHAR (100) NOT NULL,
    [AddressLine2] NVARCHAR (100) NULL,
    [AddressLine3] NVARCHAR (100) NULL,
    [City]         NVARCHAR (100) NOT NULL,
    [State]        NVARCHAR (255) NOT NULL,
    [PostalCode]   NVARCHAR (100) NOT NULL,
    [CreatedBy]          UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]        DATETIME         NOT NULL,
    [LastModifiedBy]     UNIQUEIDENTIFIER NOT NULL,
    [LastModifiedDate]   DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([AddressID] ASC)
);
GO

CREATE TABLE [dbo].[AddressType] (
    [AddressTypeID]          INT          IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (10) NOT NULL,
    [Description] VARCHAR (50) NOT NULL,
    [Active]            BIT             DEFAULT ((1)) NOT NULL,
    [CreatedBy]          UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]        DATETIME         NOT NULL,
    [LastModifiedBy]     UNIQUEIDENTIFIER NOT NULL,
    [LastModifiedDate]   DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([AddressTypeID] ASC)
);
GO

CREATE TABLE [dbo].[EmailAddress] (
    [EmailAddressID]    INT            IDENTITY (1, 1) NOT NULL,
    [Email] NVARCHAR (256) NOT NULL,
    [CreatedBy]          UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]        DATETIME         NOT NULL,
    [LastModifiedBy]     UNIQUEIDENTIFIER NOT NULL,
    [LastModifiedDate]   DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([EmailAddressID] ASC)
);

CREATE TABLE [dbo].[EmailType] (
    [EmailTypeID]          INT          IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (10) NOT NULL,
    [Description] VARCHAR (50) NULL,
    [Active]            BIT             DEFAULT ((1)) NOT NULL,
    [CreatedBy]          UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]        DATETIME         NOT NULL,
    [LastModifiedBy]     UNIQUEIDENTIFIER NOT NULL,
    [LastModifiedDate]   DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([EmailTypeID] ASC)
);

CREATE TABLE [dbo].[PhoneNumber] (
    [PhoneNumberID]          INT          IDENTITY (1, 1) NOT NULL,
    [PhoneNumber] VARCHAR (50) NOT NULL,
    [CreatedBy]          UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]        DATETIME         NOT NULL,
    [LastModifiedBy]     UNIQUEIDENTIFIER NOT NULL,
    [LastModifiedDate]   DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([PhoneNumberID] ASC)
);

CREATE TABLE [dbo].[PhoneType] (
    [PhoneTypeID]          INT          IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (20) NOT NULL,
    [Description] VARCHAR (50) NULL,
    [Active]            BIT             DEFAULT ((1)) NOT NULL,
    [CreatedBy]          UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]        DATETIME         NOT NULL,
    [LastModifiedBy]     UNIQUEIDENTIFIER NOT NULL,
    [LastModifiedDate]   DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([PhoneTypeID] ASC)
);

CREATE TABLE [dbo].[TransactionCategory] (
    [TransactionCategoryID]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    [Description]              NVARCHAR (50) NOT NULL,
    [DisplayOrder]             INT           DEFAULT ((1)) NOT NULL,
    [Active]                   BIT           DEFAULT ((1)) NOT NULL,
    [CreatedBy]          UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]        DATETIME         NOT NULL,
    [LastModifiedBy]     UNIQUEIDENTIFIER NOT NULL,
    [LastModifiedDate]   DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([TransactionCategoryID] ASC),
    CONSTRAINT [UC_TransactionCategory_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);
GO
CREATE UNIQUE NONCLUSTERED INDEX [UIX_TransactionCategory_Name]
    ON [dbo].[TransactionCategory]([Name] ASC);
GO

CREATE TABLE [dbo].[TransactionType] (
    [TransactionTypeID]		INT            IDENTITY (1, 1) NOT NULL,
    [TransactionCategoryID] INT            NOT NULL,
    [Name]					NVARCHAR (100) NOT NULL,
    [Description]			NVARCHAR (100) NOT NULL,
    [DisplayOrder]			INT            DEFAULT ((1)) NOT NULL,
    [Active]				BIT            DEFAULT ((1)) NOT NULL,
    [CreatedBy]          UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]        DATETIME         NOT NULL,
    [LastModifiedBy]     UNIQUEIDENTIFIER NOT NULL,
    [LastModifiedDate]   DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([TransactionTypeID] ASC),
    CONSTRAINT [UC_TransactionType_Name] UNIQUE NONCLUSTERED ([Name] ASC),
    CONSTRAINT [FK_TransactionType_ContributionCategory] FOREIGN KEY ([TransactionCategoryID]) REFERENCES [dbo].[TransactionCategory] ([TransactionCategoryID])
);
GO
CREATE UNIQUE NONCLUSTERED INDEX [UIX_TransactionType_Name]
    ON [dbo].[TransactionType]([Name] ASC);
GO

CREATE TABLE [dbo].[Contributor] (
    [ContributorID]         INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]  NVARCHAR (100) NOT NULL,
    [MiddleName] NVARCHAR (100) NULL,
    [LastName]   NVARCHAR (100) NOT NULL,
    [BahaiID] nvarchar(50) NULL,
    [CreatedBy]          UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]        DATETIME         NOT NULL,
    [LastModifiedBy]     UNIQUEIDENTIFIER NOT NULL,
    [LastModifiedDate]   DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([ContributorID] ASC)
);
GO

CREATE TABLE [dbo].[ContributorAddress] (
    [ContributorAddressID]            INT IDENTITY (1, 1) NOT NULL,
    [ContributorID] INT NOT NULL,
    [AddressID]     INT NOT NULL,
    [AddressTypeID] INT NOT NULL,
    [Preferred]     BIT NOT NULL,
    [CreatedBy]          UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]        DATETIME         NOT NULL,
    [LastModifiedBy]     UNIQUEIDENTIFIER NOT NULL,
    [LastModifiedDate]   DATETIME         NOT NULL,
    CONSTRAINT [FK_ContributorAddress_Contributor] FOREIGN KEY ([ContributorID]) REFERENCES [dbo].[Contributor] ([ContributorID]),
    CONSTRAINT [FK_ContributorAddress_Address] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[Address] ([AddressID]),
    PRIMARY KEY CLUSTERED ([ContributorAddressID] ASC)
);
GO

CREATE TABLE [dbo].[ContributorEmailAddress] (
    [ContributorEmailAddressID]             INT IDENTITY (1, 1) NOT NULL,
    [ContributorID]  INT NOT NULL,
    [EmailAddressID] INT NOT NULL,
    [Preferred]      BIT NOT NULL,
    [CreatedBy]          UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]        DATETIME         NOT NULL,
    [LastModifiedBy]     UNIQUEIDENTIFIER NOT NULL,
    [LastModifiedDate]   DATETIME         NOT NULL,
    CONSTRAINT [FK_ContributorEmailAddress_Contributor] FOREIGN KEY ([ContributorID]) REFERENCES [dbo].[Contributor] ([ContributorID]),
    CONSTRAINT [FK_ContributorEmailAddress_EmailAddress] FOREIGN KEY ([EmailAddressID]) REFERENCES [dbo].[EmailAddress] ([EmailAddressID]),
    PRIMARY KEY CLUSTERED ([ContributorEmailAddressID] ASC)
);
GO

CREATE TABLE [dbo].[ContributorPhoneNumber] (
    [ContributorPhoneNumberID]            INT IDENTITY (1, 1) NOT NULL,
    [ContributorID] INT NOT NULL,
    [PhoneNumberID] INT NOT NULL,
    [Preferred]     BIT NOT NULL,
    [CreatedBy]          UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]        DATETIME         NOT NULL,
    [LastModifiedBy]     UNIQUEIDENTIFIER NOT NULL,
    [LastModifiedDate]   DATETIME         NOT NULL,
    CONSTRAINT [FK_ContributorPhoneNumber_Contributor] FOREIGN KEY ([ContributorID]) REFERENCES [dbo].[Contributor] ([ContributorID]),
    CONSTRAINT [FK_ContributorPhoneNumber_PhoneNumber] FOREIGN KEY ([PhoneNumberID]) REFERENCES [dbo].[PhoneNumber] ([PhoneNumberID]),
    PRIMARY KEY CLUSTERED ([ContributorPhoneNumberID] ASC)
);
GO

CREATE TABLE [dbo].[CashJournal] (
    [CashJournalID]                 INT              IDENTITY (1, 1) NOT NULL,
    [CheckNumber]        INT              NULL,
    [Amount]             DECIMAL (18, 2)  NOT NULL,
    [ContributorID]      INT              NOT NULL,
    [TransactionTypeID] INT              NOT NULL,
    [EffectiveDate]      DATETIME         NULL,
    [CreatedBy]          UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]        DATETIME         NOT NULL,
    [LastModifiedBy]     UNIQUEIDENTIFIER NOT NULL,
    [LastModifiedDate]   DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([CashJournalID] ASC),
    CONSTRAINT [FK_CashJournal_Contributor] FOREIGN KEY ([ContributorID]) REFERENCES [dbo].[Contributor] ([ContributorID]),
    CONSTRAINT [FK_CashJournal_TransactionType] FOREIGN KEY ([TransactionTypeID]) REFERENCES [dbo].[TransactionType] ([TransactionTypeID])
);
GO

CREATE TABLE [dbo].[Report] (
    [ReportID]                INT             IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (50)   NOT NULL,
    [DisplayName]       NVARCHAR (256)  NULL,
    [ConfigurationJson] NVARCHAR (2048) NULL,
    [Active]            BIT             DEFAULT ((1)) NOT NULL,
    [DisplayOrder]      INT             NOT NULL,
    [CreatedBy]          UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]        DATETIME         NOT NULL,
    [LastModifiedBy]     UNIQUEIDENTIFIER NOT NULL,
    [LastModifiedDate]   DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([ReportID] ASC)
);
GO

CREATE UNIQUE INDEX UIX_Report_Name ON Report(Name)
GO

ALTER TABLE Report
ADD CONSTRAINT UC_Report_Name
UNIQUE (Name)
GO

CREATE TABLE [Security].[History] (
    [HistoryID] INT IDENTITY(1,1) NOT NULL,
    [IdChanged] INT NOT NULL,
    [RecordJson] nvarchar(max) NOT NULL,
    [CreatedBy]          UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]        DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([HistoryID])
);
GO

CREATE TABLE [Security].[Claim] (
    [ClaimID]         UNIQUEIDENTIFIER CONSTRAINT [DF_Claim_ID] DEFAULT (newid()) NOT NULL,
    [ClaimType]  VARCHAR (100)    NOT NULL,
    [ClaimValue] VARCHAR (50)     NOT NULL,
    CONSTRAINT [PK_Claim_ClaimID] PRIMARY KEY NONCLUSTERED ([ClaimID] ASC)
);
GO

CREATE UNIQUE INDEX UIX_ContributionType_Name ON [Security].[Claim](ClaimType)
GO

ALTER TABLE [Security].[Claim]
ADD CONSTRAINT UC_SecurityClaim_Name
UNIQUE (ClaimType)
GO

CREATE TABLE [Security].[User] (
    [UserID]          UNIQUEIDENTIFIER CONSTRAINT [DF_User_ID] DEFAULT (newid()) NOT NULL,
    [UserName]    NVARCHAR (255)   NOT NULL,
    [DisplayName] NVARCHAR (255)   NOT NULL,
    [Password]    VARCHAR (255)    NOT NULL,
    CONSTRAINT [PK_User_ID] PRIMARY KEY NONCLUSTERED ([UserID] ASC)
);
GO

CREATE UNIQUE INDEX UIX_SecurityUser_Name ON [Security].[User](UserName)
GO

ALTER TABLE [Security].[User]
ADD CONSTRAINT UC_SecurityUser_Name
UNIQUE (UserName)
GO

CREATE TABLE [Security].[UserClaim] (
    [UserClaimID]      UNIQUEIDENTIFIER CONSTRAINT [DF_UserClaim_ID] DEFAULT (newid()) NOT NULL,
    [ClaimID] UNIQUEIDENTIFIER NOT NULL,
    [UserID]  UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_UserClaim_ID] PRIMARY KEY NONCLUSTERED ([UserClaimID] ASC),
    CONSTRAINT [FK_UserClaim_User] FOREIGN KEY ([UserID]) REFERENCES [Security].[User] ([UserID]),
    CONSTRAINT [FK_UserClaim_Claim] FOREIGN KEY ([ClaimID]) REFERENCES [Security].[Claim] ([ClaimID])
);
GO

CREATE NONCLUSTERED INDEX [UIX_SecurityUserClaim_ClaimID_UserID] ON [Security].[UserClaim]
(
    [ClaimID] ASC,
    [UserID] ASC
)
GO

ALTER TABLE [Security].[UserClaim]
ADD CONSTRAINT UC_SecurityUserClaim_ClaimID_UserID
UNIQUE (ClaimID, UserID)
GO

DECLARE @UserID UNIQUEIDENTIFIER = newid()
INSERT INTO [Security].[User] ([UserID],[UserName],[DisplayName],[Password]) VALUES (@UserID, N'mcdoma', N'McDowell, Mark', N'marcus99')

DECLARE @ClaimID  UNIQUEIDENTIFIER = newid()
INSERT INTO [Security].[Claim] ([ClaimID],[ClaimType],[ClaimValue]) VALUES (@ClaimID, N'CanPerformAdmin', N'true')
INSERT INTO [Security].[UserClaim] ([UserID],[ClaimID]) VALUES (@UserID, @ClaimID)

SET @ClaimID = newid()
INSERT INTO [Security].[Claim] ([ClaimID],[ClaimType],[ClaimValue]) VALUES (@ClaimID, N'CanAccessReports', N'true')
INSERT INTO [Security].[UserClaim] ([UserID],[ClaimID]) VALUES (@UserID, @ClaimID)

SET @ClaimID = newid()
INSERT INTO [Security].[Claim] ([ClaimID],[ClaimType],[ClaimValue]) VALUES (@ClaimID, N'CanAccessCashJournal', N'true')
INSERT INTO [Security].[UserClaim] ([UserID],[ClaimID]) VALUES (@UserID, @ClaimID)

SET @ClaimID = newid()
INSERT INTO [Security].[Claim] ([ClaimID],[ClaimType],[ClaimValue]) VALUES (@ClaimID, N'CanEditCashJournal', N'true')
INSERT INTO [Security].[UserClaim] ([UserID],[ClaimID]) VALUES (@UserID, @ClaimID)
GO

DECLARE @InsertDateTime datetime = GETDATE();
DECLARE @UserID uniqueidentifier;
SELECT @UserID = UserID
FROM [Security].[User]
WHERE [UserName] = 'mcdoma'

INSERT INTO [dbo].[Report] ([Name], [DisplayName], [ConfigurationJson], [Active], [DisplayOrder],[CreatedBy],[CreatedDate],[LastModifiedBy],[LastModifiedDate]) VALUES (N'cashjournal', N'Cash Journal', NULL, 1, 10, @UserID, @InsertDateTime, @UserID, @InsertDateTime)
INSERT INTO [dbo].[Report] ([Name], [DisplayName], [ConfigurationJson], [Active], [DisplayOrder],[CreatedBy],[CreatedDate],[LastModifiedBy],[LastModifiedDate]) VALUES (N'cashjournalbydaterange', N'Cash Journal - By Date Range', NULL, 1, 20, @UserID, @InsertDateTime, @UserID, @InsertDateTime)
INSERT INTO [dbo].[Report] ([Name], [DisplayName], [ConfigurationJson], [Active], [DisplayOrder],[CreatedBy],[CreatedDate],[LastModifiedBy],[LastModifiedDate]) VALUES (N'cashjournalbycontributor', N'Cash Journal - By Contributor', NULL, 1, 30, @UserID, @InsertDateTime, @UserID, @InsertDateTime)
INSERT INTO [dbo].[Report] ([Name], [DisplayName], [ConfigurationJson], [Active], [DisplayOrder],[CreatedBy],[CreatedDate],[LastModifiedBy],[LastModifiedDate]) VALUES (N'voidedchecks', N'Voided Checks', NULL, 1, 40, @UserID, @InsertDateTime, @UserID, @InsertDateTime)
INSERT INTO [dbo].[Report] ([Name], [DisplayName], [ConfigurationJson], [Active], [DisplayOrder],[CreatedBy],[CreatedDate],[LastModifiedBy],[LastModifiedDate]) VALUES (N'receiptsbycontribution', N'Receipts - By Contribution', NULL, 1, 50, @UserID, @InsertDateTime, @UserID, @InsertDateTime)
INSERT INTO [dbo].[Report] ([Name], [DisplayName], [ConfigurationJson], [Active], [DisplayOrder],[CreatedBy],[CreatedDate],[LastModifiedBy],[LastModifiedDate]) VALUES (N'receiptsbygregorianyear', N'Receipts - By Gregorian Year', NULL, 1, 60, @UserID, @InsertDateTime, @UserID, @InsertDateTime)

INSERT INTO [dbo].[Address] ([AddressLine1], [AddressLine2], [AddressLine3], [City], [State], [PostalCode],[CreatedBy],[CreatedDate],[LastModifiedBy],[LastModifiedDate]) VALUES (N'1927 1st Ave SE', N'Baha''i Property', NULL, N'Cedar Rapids', N'IA', N'52403', @UserID, @InsertDateTime, @UserID, @InsertDateTime)
INSERT INTO [dbo].[Address] ([AddressLine1], [AddressLine2], [AddressLine3], [City], [State], [PostalCode],[CreatedBy],[CreatedDate],[LastModifiedBy],[LastModifiedDate]) VALUES (N'2323 Desoto St SW', N'', N'', N'Cedar Rapids', N'IA', N'52404', @UserID, @InsertDateTime, @UserID, @InsertDateTime)
INSERT INTO [dbo].[Address] ([AddressLine1], [AddressLine2], [AddressLine3], [City], [State], [PostalCode],[CreatedBy],[CreatedDate],[LastModifiedBy],[LastModifiedDate]) VALUES (N'2306 Desoto St SW', NULL, NULL, N'Cedar Rapids', N'IA', N'52404', @UserID, @InsertDateTime, @UserID, @InsertDateTime)
INSERT INTO [dbo].[Address] ([AddressLine1], [AddressLine2], [AddressLine3], [City], [State], [PostalCode],[CreatedBy],[CreatedDate],[LastModifiedBy],[LastModifiedDate]) VALUES (N'2100 Lindsay Lohan Lane NE', NULL, NULL, N'Cedar Rapids', N'IA', N'52402', @UserID, @InsertDateTime, @UserID, @InsertDateTime)
INSERT INTO [dbo].[Address] ([AddressLine1], [AddressLine2], [AddressLine3], [City], [State], [PostalCode],[CreatedBy],[CreatedDate],[LastModifiedBy],[LastModifiedDate]) VALUES (N'1923 1st Ave SE', NULL, NULL, N'Cedar Rapids', N'IA', N'52403', @UserID, @InsertDateTime, @UserID, @InsertDateTime)
GO




