USE [installment_db]
GO

/****** Object:  Table [dbo].[pos_age_restricted_items]    Script Date: 3/14/2024 1:28:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'pos_age_restricted_items' AND TABLE_SCHEMA = 'dbo')
BEGIN
    CREATE TABLE [dbo].[pos_age_restricted_items](
	    [id] [int] IDENTITY(1,1) NOT NULL,
	    [prod_id] [int] NULL,
	    [age_limit] [int] NULL,
     CONSTRAINT [PK_pos_age_restricted_items] PRIMARY KEY CLUSTERED 
    (
	    [id] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY]
End
GO


/****** Object:  Table [dbo].[pos_credit_card_api_settings]    Script Date: 3/14/2024 1:28:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'pos_credit_card_api_settings' AND TABLE_SCHEMA = 'dbo')
BEGIN
    CREATE TABLE [dbo].[pos_credit_card_api_settings](
	    [id] [int] IDENTITY(1,1) NOT NULL,
	    [api_url] [ntext] NULL,
	    [register_id] [nvarchar](50) NULL,
	    [authentication_key] [nvarchar](50) NULL,
     CONSTRAINT [PK_pos_credit_card_api_settings] PRIMARY KEY CLUSTERED 
    (
	    [id] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY]
End
GO


/****** Object:  Table [dbo].[pos_credit_card_history]    Script Date: 3/14/2024 1:28:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'pos_credit_card_history' AND TABLE_SCHEMA = 'dbo')
BEGIN
    CREATE TABLE [dbo].[pos_credit_card_history](
	    [id] [bigint] IDENTITY(1,1) NOT NULL,
	    [date] [date] NULL,
	    [time] [nvarchar](50) NULL,
	    [invoice_number] [nvarchar](50) NULL,
	    [response_message] [nvarchar](50) NULL,
	    [mac_address] [ntext] NULL,
	    [customer_id] [nvarchar](50) NULL,
	    [employee_id] [nvarchar](50) NULL,
	    [clock_in_id] [nvarchar](50) NULL,
     CONSTRAINT [PK_pos_credit_card_history] PRIMARY KEY CLUSTERED 
    (
	    [id] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY]
    End
GO

/****** Object:  Table [dbo].[pos_onedrive_options]    Script Date: 3/14/2024 1:28:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'pos_onedrive_options' AND TABLE_SCHEMA = 'dbo')
BEGIN
    CREATE TABLE [dbo].[pos_onedrive_options](
	    [id] [int] IDENTITY(1,1) NOT NULL,
	    [daily_sales] [nvarchar](10) NULL,
	    [daily_returns] [nvarchar](10) NULL,
	    [expenses] [nvarchar](10) NULL,
	    [inventory_history] [nvarchar](10) NULL,
	    [daily_receivings] [nvarchar](10) NULL,
	    [task_schedule] [nvarchar](10) NULL,
     CONSTRAINT [PK_pos_onedrive_options] PRIMARY KEY CLUSTERED 
    (
	    [id] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY]
End
GO


/****** Object:  Table [dbo].[pos_inventory_audit]    Script Date: 3/14/2024 1:28:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'pos_inventory_audit' AND TABLE_SCHEMA = 'dbo')
BEGIN
   CREATE TABLE [dbo].[pos_inventory_audit](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[date] [date] NULL,
	[prod_name] [nvarchar](200) NULL,
	[barcode] [nvarchar](50) NULL,
	[quantity] [float] NULL,
	[old_quantity] [float] NULL,
	[pur_price] [float] NULL,
	[old_cost_price] [float] NULL,
	[sale_price] [float] NULL,
	[old_sale_price] [float] NULL,
	[tax] [float] NULL,
	[old_tax] [float] NULL,
	[reason] [ntext] NULL,
	[prod_id] [bigint] NULL,
	[stock_id] [bigint] NULL,
 CONSTRAINT [PK_pos_inventory_audit] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
End
GO


/****** Object:  Table [dbo].[pos_ticket_details]    Script Date: 7/20/2024 10:26:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'pos_ticket_details' AND TABLE_SCHEMA = 'dbo')
BEGIN
	CREATE TABLE [dbo].[pos_ticket_details](
		[sales_id] [bigint] IDENTITY(1,1) NOT NULL,
		[quantity] [float] NOT NULL,
		[pkg] [float] NOT NULL,
		[full_pkg] [float] NOT NULL,
		[discount] [float] NULL,
		[Total_price] [float] NOT NULL,
		[taxation] [float] NULL,
		[note] [ntext] NULL,
		[sales_acc_id] [bigint] NOT NULL,
		[prod_id] [int] NOT NULL,
		[stock_id] [float] NULL,
	 CONSTRAINT [PK_pos_ticket_details] PRIMARY KEY CLUSTERED 
	(
		[sales_id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] 
END
GO


/****** Object:  Table [dbo].[pos_tickets]    Script Date: 7/20/2024 10:26:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'pos_tickets' AND TABLE_SCHEMA = 'dbo')
BEGIN

	CREATE TABLE [dbo].[pos_tickets](
		[sales_acc_id] [bigint] IDENTITY(1,1) NOT NULL,
		[billNo] [nvarchar](50) NOT NULL,
		[date] [date] NOT NULL,
		[time] [nvarchar](50) NULL,
		[no_of_items] [nvarchar](50) NOT NULL,
		[total_qty] [nvarchar](50) NOT NULL,
		[sub_total] [float] NOT NULL,
		[discount] [float] NOT NULL,
		[tax] [nvarchar](50) NOT NULL,
		[amount_due] [float] NOT NULL,
		[paid] [float] NOT NULL,
		[advance_amount] [float] NOT NULL,
		[balance] [float] NOT NULL,
		[status] [nvarchar](50) NOT NULL,
		[customer_id] [int] NOT NULL,
		[employee_id] [int] NOT NULL,
		[total_taxation] [float] NULL,
		[macAddress] [ntext] NULL,
	 CONSTRAINT [PK_pos_tickets] PRIMARY KEY CLUSTERED 
	(
		[sales_acc_id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] 
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_pos_ticket_details_pos_products]') AND parent_object_id = OBJECT_ID(N'[dbo].[pos_ticket_details]'))
BEGIN
    ALTER TABLE [dbo].[pos_ticket_details]  WITH CHECK ADD  CONSTRAINT [FK_pos_ticket_details_pos_products] FOREIGN KEY([prod_id])
    REFERENCES [dbo].[pos_products] ([product_id])
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_pos_ticket_details_pos_tickets]') AND parent_object_id = OBJECT_ID(N'[dbo].[pos_ticket_details]'))
BEGIN
    ALTER TABLE [dbo].[pos_ticket_details]  WITH CHECK ADD  CONSTRAINT [FK_pos_ticket_details_pos_tickets] FOREIGN KEY([sales_acc_id])
    REFERENCES [dbo].[pos_tickets] ([sales_acc_id])
END
GO

ALTER TABLE [dbo].[pos_ticket_details] CHECK CONSTRAINT [FK_pos_ticket_details_pos_tickets]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_pos_tickets_pos_customers]') AND parent_object_id = OBJECT_ID(N'[dbo].[pos_tickets]'))
BEGIN
    ALTER TABLE [dbo].[pos_tickets]  WITH CHECK ADD  CONSTRAINT [FK_pos_tickets_pos_customers] FOREIGN KEY([customer_id])
    REFERENCES [dbo].[pos_customers] ([customer_id])
END
GO

ALTER TABLE [dbo].[pos_tickets] CHECK CONSTRAINT [FK_pos_tickets_pos_customers]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_pos_tickets_pos_employees]') AND parent_object_id = OBJECT_ID(N'[dbo].[pos_tickets]'))
BEGIN
    ALTER TABLE [dbo].[pos_tickets]  WITH CHECK ADD  CONSTRAINT [FK_pos_tickets_pos_employees] FOREIGN KEY([employee_id])
    REFERENCES [dbo].[pos_employees] ([employee_id])
END
GO

ALTER TABLE [dbo].[pos_tickets] CHECK CONSTRAINT [FK_pos_tickets_pos_employees]
GO


/****** Object:  Table [dbo].[pos_weblink_api_settings]    Script Date: 7/20/2024 10:26:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'pos_weblink_api_settings' AND TABLE_SCHEMA = 'dbo')
BEGIN

	CREATE TABLE [dbo].[pos_weblink_api_settings](
		[id] [int] IDENTITY(1,1) NOT NULL,
		[api_url] [ntext] NULL,
		[store_id] [nvarchar](50) NULL,
		[token] [ntext] NULL,
	 CONSTRAINT [PK_pos_weblink_api_settings] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] 
End
GO


/****** Object:  Table [dbo].[pos_migrations]    Script Date: 7/20/2024 10:26:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'pos_migrations' AND TABLE_SCHEMA = 'dbo')
BEGIN

	CREATE TABLE [dbo].[pos_migrations](
		[id] [int] IDENTITY(1,1) NOT NULL,
		[date] [date] NULL,
		[time] [nvarchar](50) NULL,
		[migration] [nvarchar](300) NULL,
	 CONSTRAINT [PK_pos_migrations] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] 
End
GO


-- Alter the prod_name data type in pos_products table
ALTER TABLE [dbo].[pos_products] ALTER COLUMN [prod_name] NVARCHAR(200)
GO


-- Alter the prod_name data type in [pos_inventory_audit] table
ALTER TABLE [dbo].[pos_inventory_audit] ALTER COLUMN [prod_name] NVARCHAR(200)
GO


-- Alter the age column in pos_customers table
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'pos_customers' AND TABLE_SCHEMA = 'dbo')
BEGIN
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_customers' AND COLUMN_NAME = 'age')
    BEGIN
        ALTER TABLE [dbo].[pos_customers] ADD [age] [nvarchar](10) NULL
    END
End
GO


-- Alter the data type in [pos_AllCodes] table
ALTER TABLE [dbo].[pos_AllCodes] ALTER COLUMN [customerCodes] float
ALTER TABLE [dbo].[pos_AllCodes] ALTER COLUMN [employeeCodes] float
ALTER TABLE [dbo].[pos_AllCodes] ALTER COLUMN [supplierCodes] float
ALTER TABLE [dbo].[pos_AllCodes] ALTER COLUMN [purchaseCodes] float
ALTER TABLE [dbo].[pos_AllCodes] ALTER COLUMN [productCodes] float
ALTER TABLE [dbo].[pos_AllCodes] ALTER COLUMN [purchaseReturnCodes] float
ALTER TABLE [dbo].[pos_AllCodes] ALTER COLUMN [bankLoanCodes] float
ALTER TABLE [dbo].[pos_AllCodes] ALTER COLUMN [salesCodes] float
ALTER TABLE [dbo].[pos_AllCodes] ALTER COLUMN [salesReturnsCodes] float
ALTER TABLE [dbo].[pos_AllCodes] ALTER COLUMN [demandCodes] float
ALTER TABLE [dbo].[pos_AllCodes] ALTER COLUMN [guarantorsCodes] float
ALTER TABLE [dbo].[pos_AllCodes] ALTER COLUMN [holdItemsCodes] float
ALTER TABLE [dbo].[pos_AllCodes] ALTER COLUMN [investorsCodes] float

GO



-- Alter pos_general_settings table to adding Columns
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'pos_general_settings' AND TABLE_SCHEMA = 'dbo')
BEGIN
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_general_settings' AND COLUMN_NAME = 'isCreditCardConnected')
    BEGIN
        ALTER TABLE [dbo].[pos_general_settings] ADD [isCreditCardConnected] [nvarchar](20) NULL
    END
    
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_general_settings' AND COLUMN_NAME = 'notificationSound')
    BEGIN
        ALTER TABLE [dbo].[pos_general_settings] ADD [notificationSound] [nvarchar](20) NULL
    END
      
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_general_settings' AND COLUMN_NAME = 'changeAmountPopUp')
    BEGIN
        ALTER TABLE [dbo].[pos_general_settings] ADD [changeAmountPopUp] [nvarchar](20) NULL
    END
      
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_general_settings' AND COLUMN_NAME = 'customerAgeLimit')
    BEGIN
        ALTER TABLE [dbo].[pos_general_settings] ADD [customerAgeLimit] [nvarchar](20) NULL
    END
      
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_general_settings' AND COLUMN_NAME = 'salesmanTips')
    BEGIN
        ALTER TABLE [dbo].[pos_general_settings] ADD [salesmanTips] [nvarchar](20) NULL
    END
      
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_general_settings' AND COLUMN_NAME = 'useSurcharges')
    BEGIN
        ALTER TABLE [dbo].[pos_general_settings] ADD [useSurcharges] [nvarchar](20) NULL
    END
      
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_general_settings' AND COLUMN_NAME = 'surchargePercentage')
    BEGIN
        ALTER TABLE [dbo].[pos_general_settings] ADD [surchargePercentage] [nvarchar](20) NULL
    END
End
GO


-- Alter pos_onedrive_options table to adding Columns
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'pos_onedrive_options' AND TABLE_SCHEMA = 'dbo')
BEGIN
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_onedrive_options' AND COLUMN_NAME = 'drive')
    BEGIN
        ALTER TABLE [dbo].[pos_onedrive_options] ADD [drive] [nvarchar](20) NULL
    END

END
GO



-- Alter pos_no_sale table to adding Columns
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'pos_no_sale' AND TABLE_SCHEMA = 'dbo')
BEGIN
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_no_sale' AND COLUMN_NAME = 'time')
    BEGIN
        ALTER TABLE [dbo].[pos_no_sale] ADD [time] [nvarchar](20) NULL
    END

END
GO


-- Alter pos_void_orders table to adding Columns
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'pos_void_orders' AND TABLE_SCHEMA = 'dbo')
BEGIN
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_void_orders' AND COLUMN_NAME = 'time')
    BEGIN
        ALTER TABLE [dbo].[pos_void_orders] ADD [time] [nvarchar](20) NULL
    END

END
GO


-- Alter pos_sales_account table to adding Columns
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'pos_sales_account' AND TABLE_SCHEMA = 'dbo')
BEGIN
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_sales_account' AND COLUMN_NAME = 'customerPoints')
    BEGIN
        ALTER TABLE [dbo].[pos_sales_account] ADD [customerPoints] [nvarchar](20) NULL
    END
    
    
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_sales_account' AND COLUMN_NAME = 'employeeCommission')
    BEGIN
        ALTER TABLE [dbo].[pos_sales_account] ADD [employeeCommission] [float] NULL
    END 
    
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_sales_account' AND COLUMN_NAME = 'employeeTip')
    BEGIN
        ALTER TABLE [dbo].[pos_sales_account] ADD [employeeTip] [float] NULL
    END 
    
     IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_sales_account' AND COLUMN_NAME = 'surcharges')
    BEGIN
        ALTER TABLE [dbo].[pos_sales_account] ADD [surcharges] [float] NULL
    END 
    
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_sales_account' AND COLUMN_NAME = 'advance_payment')
    BEGIN
        ALTER TABLE [dbo].[pos_sales_account] ADD [advance_payment] [float] NULL
    END 

END
GO


-- Alter pos_return_account table to adding Columns
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'pos_return_account' AND TABLE_SCHEMA = 'dbo')
BEGIN
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_return_account' AND COLUMN_NAME = 'customerPoints')
    BEGIN
        ALTER TABLE [dbo].[pos_return_account] ADD [customerPoints] [nvarchar](20) NULL
    END
    
    
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_return_account' AND COLUMN_NAME = 'employeeCommission')
    BEGIN
        ALTER TABLE [dbo].[pos_return_account] ADD [employeeCommission] [float] NULL
    END 
    
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_return_account' AND COLUMN_NAME = 'employeeTip')
    BEGIN
        ALTER TABLE [dbo].[pos_return_account] ADD [employeeTip] [float] NULL
    END 
    
     IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_return_account' AND COLUMN_NAME = 'surcharges')
    BEGIN
        ALTER TABLE [dbo].[pos_return_account] ADD [surcharges] [float] NULL
    END 
    
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_return_account' AND COLUMN_NAME = 'advance_payment')
    BEGIN
        ALTER TABLE [dbo].[pos_return_account] ADD [advance_payment] [float] NULL
    END 

END
GO


-- Alter pos_sales_details table to adding Columns
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'pos_sales_details' AND TABLE_SCHEMA = 'dbo')
BEGIN
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_sales_details' AND COLUMN_NAME = 'per_item_commission')
    BEGIN
        ALTER TABLE [dbo].[pos_sales_details] ADD [per_item_commission] [float] NULL
    END
   
END
GO


-- Alter pos_returns_details table to adding Columns
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'pos_returns_details' AND TABLE_SCHEMA = 'dbo')
BEGIN
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_returns_details' AND COLUMN_NAME = 'per_item_commission')
    BEGIN
        ALTER TABLE [dbo].[pos_returns_details] ADD [per_item_commission] [float] NULL
    END
   
END
GO



-- Alter pos_salariesPaybook table to adding Columns
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'pos_salariesPaybook' AND TABLE_SCHEMA = 'dbo')
BEGIN
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_salariesPaybook' AND COLUMN_NAME = 'previous_paid_salary')
    BEGIN
        ALTER TABLE [dbo].[pos_salariesPaybook] ADD [previous_paid_salary] [float] NULL
    END
    
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_salariesPaybook' AND COLUMN_NAME = 'previous_paid_commission')
    BEGIN
        ALTER TABLE [dbo].[pos_salariesPaybook] ADD [previous_paid_commission] [float] NULL
    END
    
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_salariesPaybook' AND COLUMN_NAME = 'commission_balance')
    BEGIN
        ALTER TABLE [dbo].[pos_salariesPaybook] ADD [commission_balance] [float] NULL
    END
   
END
GO



-- Alter pos_clock_in table to adding Columns
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'pos_clock_in' AND TABLE_SCHEMA = 'dbo')
BEGIN
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_clock_in' AND COLUMN_NAME = 'total100s')
    BEGIN
        ALTER TABLE [dbo].[pos_clock_in] ADD [total100s] [int] NULL
    END

    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_clock_in' AND COLUMN_NAME = 'total50s')
    BEGIN
        ALTER TABLE [dbo].[pos_clock_in] ADD [total50s] [int] NULL
    END

    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_clock_in' AND COLUMN_NAME = 'total20s')
    BEGIN
        ALTER TABLE [dbo].[pos_clock_in] ADD [total20s] [int] NULL
    END

    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_clock_in' AND COLUMN_NAME = 'total10s')
    BEGIN
        ALTER TABLE [dbo].[pos_clock_in] ADD [total10s] [int] NULL
    END

    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_clock_in' AND COLUMN_NAME = 'total5s')
    BEGIN
        ALTER TABLE [dbo].[pos_clock_in] ADD [total5s] [int] NULL
    END

    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_clock_in' AND COLUMN_NAME = 'total2s')
    BEGIN
        ALTER TABLE [dbo].[pos_clock_in] ADD [total2s] [int] NULL
    END

    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_clock_in' AND COLUMN_NAME = 'total1s')
    BEGIN
        ALTER TABLE [dbo].[pos_clock_in] ADD [total1s] [int] NULL
    END

    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_clock_in' AND COLUMN_NAME = 'total1c')
    BEGIN
        ALTER TABLE [dbo].[pos_clock_in] ADD [total1c] [int] NULL
    END

    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_clock_in' AND COLUMN_NAME = 'total5c')
    BEGIN
        ALTER TABLE [dbo].[pos_clock_in] ADD [total5c] [int] NULL
    END

    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_clock_in' AND COLUMN_NAME = 'total10c')
    BEGIN
        ALTER TABLE [dbo].[pos_clock_in] ADD [total10c] [int] NULL
    END

    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_clock_in' AND COLUMN_NAME = 'total25c')
    BEGIN
        ALTER TABLE [dbo].[pos_clock_in] ADD [total25c] [int] NULL
    END
END
GO


-- Alter pos_pos_clock_out table to adding columns

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'pos_clock_out' AND TABLE_SCHEMA = 'dbo')
BEGIN
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_clock_out' AND COLUMN_NAME = 'total100s')
    BEGIN
        ALTER TABLE [dbo].[pos_clock_out] ADD [total100s] [int] NULL
    END

    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_clock_out' AND COLUMN_NAME = 'total50s')
    BEGIN
        ALTER TABLE [dbo].[pos_clock_out] ADD [total50s] [int] NULL
    END

    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_clock_out' AND COLUMN_NAME = 'total20s')
    BEGIN
        ALTER TABLE [dbo].[pos_clock_out] ADD [total20s] [int] NULL
    END

    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_clock_out' AND COLUMN_NAME = 'total10s')
    BEGIN
        ALTER TABLE [dbo].[pos_clock_out] ADD [total10s] [int] NULL
    END

    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_clock_out' AND COLUMN_NAME = 'total5s')
    BEGIN
        ALTER TABLE [dbo].[pos_clock_out] ADD [total5s] [int] NULL
    END

    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_clock_out' AND COLUMN_NAME = 'total2s')
    BEGIN
        ALTER TABLE [dbo].[pos_clock_out] ADD [total2s] [int] NULL
    END

    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_clock_out' AND COLUMN_NAME = 'total1s')
    BEGIN
        ALTER TABLE [dbo].[pos_clock_out] ADD [total1s] [int] NULL
    END

    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_clock_out' AND COLUMN_NAME = 'total1c')
    BEGIN
        ALTER TABLE [dbo].[pos_clock_out] ADD [total1c] [int] NULL
    END

    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_clock_out' AND COLUMN_NAME = 'total5c')
    BEGIN
        ALTER TABLE [dbo].[pos_clock_out] ADD [total5c] [int] NULL
    END

    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_clock_out' AND COLUMN_NAME = 'total10c')
    BEGIN
        ALTER TABLE [dbo].[pos_clock_out] ADD [total10c] [int] NULL
    END

    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_clock_out' AND COLUMN_NAME = 'total25c')
    BEGIN
        ALTER TABLE [dbo].[pos_clock_out] ADD [total25c] [int] NULL
    END
END
GO