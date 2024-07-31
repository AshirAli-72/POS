USE [installment_db]
GO


-- Alter pos_general_settings table to adding Columns
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'pos_general_settings' AND TABLE_SCHEMA = 'dbo')
BEGIN
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'pos_general_settings' AND COLUMN_NAME = 'showShiftCurrency')
    BEGIN
        ALTER TABLE [dbo].[pos_general_settings] ADD [showShiftCurrency] [nvarchar](20) NULL
    END
    
End
GO



-- Alter the prod_name data type in pos_customerChequeDetails table
ALTER TABLE [dbo].[pos_customerChequeDetails] ALTER COLUMN [customer_id] int NULL
ALTER TABLE [dbo].[pos_customerChequeDetails] ALTER COLUMN [billNo] [nvarchar](50) NULL
ALTER TABLE [dbo].[pos_customerChequeDetails] ALTER COLUMN [accountNo] [nvarchar](200) NULL
ALTER TABLE [dbo].[pos_customerChequeDetails] ALTER COLUMN [chequeNo] [nvarchar](200) NULL
GO



/****** Object:  StoredProcedure [dbo].[ReportProcedureChequeNotifications]    Script Date: 7/31/2024 6:34:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




ALTER procedure [dbo].[ReportProcedureChequeNotifications]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50)
)

as

SELECT pos_bank.bank_title, pos_customers.full_name, pos_customers.fatherName, pos_customers.cus_code, pos_customers.cnic, pos_customers.mobile_no, pos_customers.address1, pos_customers.email, 
pos_customerChequeDetails.billNo, pos_customerChequeDetails.date, pos_customerChequeDetails.bounceDate, pos_customerChequeDetails.accountNo, pos_customerChequeDetails.remarks, 
pos_customerChequeDetails.amount, pos_customerChequeDetails.status, pos_customerChequeDetails.chequeNo
FROM pos_bank INNER JOIN pos_customerChequeDetails ON pos_bank.bank_id = pos_customerChequeDetails.bank_id Left JOIN
pos_customers ON pos_customerChequeDetails.customer_id = pos_customers.customer_id
where (pos_customerChequeDetails.date between @fromDate and @toDate) and (pos_customerChequeDetails.status != 'Complete')





