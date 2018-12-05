DROP TABLE [dbo].[Address];
DROP TABLE [dbo].[CashJournal];
DROP TABLE [dbo].[ContributionType];
DROP TABLE [dbo].[ContributionCategory];
DROP TABLE [dbo].[Contributor];
DROP TABLE [dbo].[Report];
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
    PRIMARY KEY CLUSTERED ([AddressID] ASC)
);
GO

CREATE TABLE [dbo].[ContributionCategory] (
    [ContributionCategoryID]                       INT           IDENTITY (1, 1) NOT NULL,
    [ContributionCategoryName] NVARCHAR (50) NOT NULL,
    [Description]              NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([ContributionCategoryID] ASC)
);
GO

CREATE UNIQUE INDEX UIX_ContributionCategory_Name ON ContributionCategory(ContributionCategoryName)
GO

ALTER TABLE ContributionCategory
ADD CONSTRAINT UC_ContributionCategory_Name
UNIQUE (ContributionCategoryName)
GO

CREATE TABLE [dbo].[ContributionType] (
    [ContributionTypeID]                   INT            IDENTITY (1, 1) NOT NULL,
    [CategoryID]           INT            NOT NULL,
    [ContributionTypeName] NVARCHAR (100) NOT NULL,
    [Description]          NVARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([ContributionTypeID] ASC),
    CONSTRAINT [FK_ContributionType_ContributionCategory] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[ContributionCategory] ([ContributionCategoryID])
);
GO

CREATE UNIQUE INDEX UIX_ContributionType_Name ON ContributionType(ContributionTypeName)
GO

ALTER TABLE ContributionType
ADD CONSTRAINT UC_ContributionType_Name
UNIQUE (ContributionTypeName)
GO

CREATE TABLE [dbo].[Contributor] (
    [ContributorID]         INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]  NVARCHAR (100) NOT NULL,
    [MiddleName] NVARCHAR (100) NULL,
    [LastName]   NVARCHAR (100) NOT NULL,
    [AddressID]  INT            NULL,
    PRIMARY KEY CLUSTERED ([ContributorID] ASC)
);
GO

CREATE TABLE [dbo].[CashJournal] (
    [CashJournalID]                 INT              IDENTITY (1, 1) NOT NULL,
    [CheckNumber]        INT              NULL,
    [Amount]             DECIMAL (18, 2)  NOT NULL,
    [ContributorID]      INT              NOT NULL,
    [BahaiID]            NVARCHAR (100)   NULL,
    [ContributionTypeID] INT              NOT NULL,
    [EffectiveDate]      DATETIME         NULL,
    [CreatedBy]          UNIQUEIDENTIFIER NOT NULL,
    [CreatedDate]        DATETIME         NOT NULL,
    [LastModifiedBy]     UNIQUEIDENTIFIER NOT NULL,
    [LastModifiedDate]   DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([CashJournalID] ASC),
    CONSTRAINT [FK_CashJournal_Contributor] FOREIGN KEY ([ContributorID]) REFERENCES [dbo].[Contributor] ([ContributorID]),
    CONSTRAINT [FK_CashJournal_ContributionType] FOREIGN KEY ([ContributionTypeID]) REFERENCES [dbo].[ContributionType] ([ContributionTypeID])
);
GO
CREATE NONCLUSTERED INDEX [IDX_Contributor_AddressID] ON [dbo].[Contributor]([AddressID] ASC);
GO

CREATE TABLE [dbo].[Report] (
    [ReportID]                INT             IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (50)   NOT NULL,
    [DisplayName]       NVARCHAR (256)  NULL,
    [ConfigurationJson] NVARCHAR (2048) NULL,
    [Active]            BIT             DEFAULT ((1)) NOT NULL,
    [DisplayOrder]      INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([ReportID] ASC)
);
GO

CREATE UNIQUE INDEX UIX_Report_Name ON Report(Name)
GO

ALTER TABLE Report
ADD CONSTRAINT UC_Report_Name
UNIQUE (Name)
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

INSERT INTO [dbo].[Report] ([Name], [DisplayName], [ConfigurationJson], [Active], [DisplayOrder]) VALUES (N'cashjournal', N'Cash Journal', NULL, 1, 10)
INSERT INTO [dbo].[Report] ([Name], [DisplayName], [ConfigurationJson], [Active], [DisplayOrder]) VALUES (N'cashjournalbydaterange', N'Cash Journal - By Date Range', NULL, 1, 20)
INSERT INTO [dbo].[Report] ([Name], [DisplayName], [ConfigurationJson], [Active], [DisplayOrder]) VALUES (N'cashjournalbycontributor', N'Cash Journal - By Contributor', NULL, 1, 30)
INSERT INTO [dbo].[Report] ([Name], [DisplayName], [ConfigurationJson], [Active], [DisplayOrder]) VALUES (N'voidedchecks', N'Voided Checks', NULL, 1, 40)
INSERT INTO [dbo].[Report] ([Name], [DisplayName], [ConfigurationJson], [Active], [DisplayOrder]) VALUES (N'receiptsbycontribution', N'Receipts - By Contribution', NULL, 1, 50)
INSERT INTO [dbo].[Report] ([Name], [DisplayName], [ConfigurationJson], [Active], [DisplayOrder]) VALUES (N'receiptsbygregorianyear', N'Receipts - By Gregorian Year', NULL, 1, 60)
GO

INSERT INTO [dbo].[Address] ([AddressLine1], [AddressLine2], [AddressLine3], [City], [State], [PostalCode]) VALUES (N'1927 1st Ave SE', N'Baha''i Property', NULL, N'Cedar Rapids', N'IA', N'52403')
INSERT INTO [dbo].[Address] ([AddressLine1], [AddressLine2], [AddressLine3], [City], [State], [PostalCode]) VALUES (N'2323 Desoto St SW', N'', N'', N'Cedar Rapids', N'IA', N'52404')
INSERT INTO [dbo].[Address] ([AddressLine1], [AddressLine2], [AddressLine3], [City], [State], [PostalCode]) VALUES (N'2306 Desoto St SW', NULL, NULL, N'Cedar Rapids', N'IA', N'52404')
INSERT INTO [dbo].[Address] ([AddressLine1], [AddressLine2], [AddressLine3], [City], [State], [PostalCode]) VALUES (N'2100 Lindsay Lohan Lane NE', NULL, NULL, N'Cedar Rapids', N'IA', N'52402')
INSERT INTO [dbo].[Address] ([AddressLine1], [AddressLine2], [AddressLine3], [City], [State], [PostalCode]) VALUES (N'1923 1st Ave SE', NULL, NULL, N'Cedar Rapids', N'IA', N'52403')
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




