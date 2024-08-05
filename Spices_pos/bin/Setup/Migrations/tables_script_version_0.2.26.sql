USE [installment_db]
GO


-- Alter the prod_name data type in pos_products table
ALTER TABLE [dbo].[product_name] ALTER COLUMN [prod_name] NVARCHAR(300)
GO

-- Alter the prod_name data type in pos_products table
ALTER TABLE [dbo].[pos_cart_items] ALTER COLUMN [product_name] NVARCHAR(300)
GO



