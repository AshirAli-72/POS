USE [installment_db];
GO

-- Persons Table
-- 1. Create pos_persons first
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'pos_persons' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE [dbo].[pos_persons](
        [id] INT IDENTITY(1,1) NOT NULL,
        [name] NVARCHAR(200) NULL,
        [mobile_number] NVARCHAR(20) NULL,
        [cnic_number] NVARCHAR(20) NULL,
        [address] NVARCHAR(300) NULL,
        [status] NVARCHAR(50) NULL,
        CONSTRAINT [PK_pos_persons] PRIMARY KEY CLUSTERED ([id] ASC)
    ) ON [PRIMARY]
END
GO

-- 2. Now create pos_cash_management
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'pos_cash_management' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE [dbo].[pos_cash_management](
        [id] INT IDENTITY(1,1) NOT NULL,
        [person_id] INT NOT NULL,
        [status_id] INT NULL,
        [payment_id] INT NULL,
        [date] DATE NULL,
        [time] TIME NULL,
        [remarks] NVARCHAR(300) NULL,
        [amount] DECIMAL(18,2) NULL,
        CONSTRAINT [PK_pos_cash_management] PRIMARY KEY CLUSTERED ([id] ASC),
        CONSTRAINT [FK_pos_cash_person] FOREIGN KEY ([person_id])
            REFERENCES [dbo].[pos_persons]([id]),
        CONSTRAINT [FK_cash_status] FOREIGN KEY ([status_id])
            REFERENCES [dbo].[pos_payment_status]([id]),
        CONSTRAINT [FK_cash_payment_type] FOREIGN KEY ([payment_id])
            REFERENCES [dbo].[pos_payment_type]([id])
    ) ON [PRIMARY]
END
GO

-- Payment Type Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'pos_payment_type' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE [dbo].[pos_payment_type](
        [id] INT IDENTITY(1,1) NOT NULL,
        [payment_title] NVARCHAR(300) NULL, 
        CONSTRAINT [PK_pos_payment_type] PRIMARY KEY CLUSTERED ([id] ASC)
    ) ON [PRIMARY]
END
GO

-- Payment Status Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'pos_payment_status' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE [dbo].[pos_payment_status](
        [id] INT IDENTITY(1,1) NOT NULL,
        [status_title] NVARCHAR(300) NULL, 
        CONSTRAINT [PK_pos_payment_status] PRIMARY KEY CLUSTERED ([id] ASC)
    ) ON [PRIMARY]
END
GO

