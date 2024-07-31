USE [installment_db]
GO
/****** Object:  View [dbo].[CounterDealIndividualItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  Table [dbo].[pos_weblink_api_settings]    Script Date: 7/16/2024 3:58:55 AM ******/



CREATE view [dbo].[CounterDealIndividualItems]
as

SELECT        dbo.pos_products.product_id, pos_stock_details.stock_id, dbo.pos_products.prod_name, pos_stock_details.item_barcode, 
dbo.pos_deals.deal_id, dbo.pos_deals.deal_title, dbo.pos_products.status, dbo.pos_stock_details.sale_price, dbo.pos_deal_items.quantity, dbo.pos_products.item_type, 
dbo.pos_deal_items.price AS dealPrice
FROM            dbo.pos_deal_items INNER JOIN
dbo.pos_deals ON dbo.pos_deal_items.deal_id = dbo.pos_deals.deal_id INNER JOIN
dbo.pos_products ON dbo.pos_deal_items.prod_id = dbo.pos_products.product_id INNER JOIN
dbo.pos_stock_details ON dbo.pos_deal_items.stock_id = dbo.pos_stock_details.stock_id




GO
/****** Object:  View [dbo].[ReportViewBillWiseCounterReturns]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[ReportViewBillWiseCounterReturns]
as

SELECT        dbo.pos_customers.full_name, dbo.pos_employees.full_name AS Expr1, dbo.pos_return_accounts.billNo, dbo.pos_return_accounts.date AS date, dbo.pos_return_accounts.no_of_items, 
dbo.pos_return_accounts.total_qty, dbo.pos_return_accounts.sub_total, dbo.pos_return_accounts.discount, dbo.pos_return_accounts.tax, dbo.pos_return_accounts.amount_due, dbo.pos_return_accounts.paid, 
dbo.pos_return_accounts.credits, dbo.pos_return_accounts.pCredits, dbo.pos_return_accounts.status, dbo.pos_products.prod_name, dbo.pos_products.barcode, dbo.pos_stock_details.pur_price, 
dbo.pos_stock_details.sale_price, dbo.pos_stock_details.market_value, dbo.pos_returns_details.quantity, dbo.pos_returns_details.pkg, dbo.pos_returns_details.full_pkg, dbo.pos_returns_details.Total_price
FROM            dbo.pos_customers INNER JOIN
dbo.pos_return_accounts ON dbo.pos_customers.customer_id = dbo.pos_return_accounts.customer_id INNER JOIN
dbo.pos_employees ON dbo.pos_return_accounts.employee_id = dbo.pos_employees.employee_id INNER JOIN
dbo.pos_returns_details ON dbo.pos_return_accounts.return_acc_id = dbo.pos_returns_details.return_acc_id INNER JOIN
dbo.pos_products ON dbo.pos_returns_details.prod_id = dbo.pos_products.product_id INNER JOIN
dbo.pos_stock_details ON dbo.pos_products.product_id = dbo.pos_stock_details.prod_id








GO
/****** Object:  View [dbo].[ReportViewBillWiseSales]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[ReportViewBillWiseSales]
as

SELECT        dbo.pos_customers.full_name, dbo.pos_employees.full_name AS Expr1, dbo.pos_sales_accounts.billNo, dbo.pos_sales_accounts.date, dbo.pos_sales_accounts.no_of_items, dbo.pos_sales_accounts.total_qty, 
dbo.pos_sales_accounts.sub_total, dbo.pos_sales_accounts.discount, dbo.pos_sales_accounts.tax, dbo.pos_sales_accounts.amount_due, dbo.pos_sales_accounts.paid, dbo.pos_sales_accounts.credits, 
dbo.pos_sales_accounts.pCredits, dbo.pos_sales_accounts.status, dbo.pos_products.prod_name, dbo.pos_products.barcode, dbo.pos_sales_details.quantity, dbo.pos_sales_details.pkg, dbo.pos_sales_details.full_pkg, 
dbo.pos_sales_details.Total_price, dbo.pos_stock_details.pur_price, dbo.pos_stock_details.sale_price, dbo.pos_stock_details.market_value, dbo.pos_sales_details.note, dbo.pos_sales_accounts.total_taxation, 
dbo.pos_sales_details.total_marketPrice, dbo.pos_sales_accounts.check_sale_status, dbo.pos_sales_accounts.credit_card_amount, dbo.pos_sales_accounts.paypal_amount, dbo.pos_sales_accounts.google_pay_amount, 
dbo.pos_sales_details.discount AS perItemDiscount, dbo.pos_sales_accounts.customerPoints, dbo.pos_sales_accounts.balance_amount, dbo.pos_sales_accounts.surcharges, dbo.pos_sales_accounts.advance_payment
FROM            dbo.pos_customers INNER JOIN
dbo.pos_sales_accounts ON dbo.pos_customers.customer_id = dbo.pos_sales_accounts.customer_id INNER JOIN
dbo.pos_employees ON dbo.pos_sales_accounts.employee_id = dbo.pos_employees.employee_id INNER JOIN
dbo.pos_sales_details ON dbo.pos_sales_accounts.sales_acc_id = dbo.pos_sales_details.sales_acc_id INNER JOIN
dbo.pos_products ON dbo.pos_sales_details.prod_id = dbo.pos_products.product_id INNER JOIN
dbo.pos_stock_details ON dbo.pos_products.product_id = dbo.pos_stock_details.prod_id

GO
/****** Object:  View [dbo].[ReportViewBillWiseSalesReturns]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE view [dbo].[ReportViewBillWiseSalesReturns]
as

SELECT        dbo.pos_customers.full_name, dbo.pos_employees.full_name AS Expr1, dbo.pos_return_accounts.billNo, dbo.pos_return_accounts.date, dbo.pos_return_accounts.no_of_items, dbo.pos_return_accounts.total_qty, 
dbo.pos_return_accounts.sub_total, dbo.pos_return_accounts.discount, dbo.pos_return_accounts.tax, dbo.pos_return_accounts.amount_due, dbo.pos_return_accounts.paid, dbo.pos_return_accounts.credits, 
dbo.pos_return_accounts.pCredits, dbo.pos_return_accounts.status, dbo.pos_products.prod_name, dbo.pos_products.barcode, dbo.pos_returns_details.quantity, dbo.pos_returns_details.pkg, dbo.pos_returns_details.full_pkg, 
dbo.pos_returns_details.Total_price, dbo.pos_stock_details.pur_price, dbo.pos_stock_details.sale_price, dbo.pos_stock_details.market_value, dbo.pos_returns_details.note, dbo.pos_return_accounts.total_taxation, 
dbo.pos_returns_details.total_marketPrice, dbo.pos_return_accounts.check_sale_status, dbo.pos_return_accounts.credit_card_amount, dbo.pos_return_accounts.paypal_amount, dbo.pos_return_accounts.google_pay_amount, 
dbo.pos_returns_details.discount AS perItemDiscount, dbo.pos_return_accounts.customerPoints, dbo.pos_return_accounts.advance_payment
FROM            dbo.pos_customers INNER JOIN
dbo.pos_return_accounts ON dbo.pos_customers.customer_id = dbo.pos_return_accounts.customer_id INNER JOIN
dbo.pos_employees ON dbo.pos_return_accounts.employee_id = dbo.pos_employees.employee_id INNER JOIN
dbo.pos_returns_details ON dbo.pos_return_accounts.return_acc_id = dbo.pos_returns_details.return_acc_id INNER JOIN
dbo.pos_products ON dbo.pos_returns_details.prod_id = dbo.pos_products.product_id INNER JOIN
dbo.pos_stock_details ON dbo.pos_products.product_id = dbo.pos_stock_details.prod_id

GO
/****** Object:  View [dbo].[reportViewPrintTicket]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[reportViewPrintTicket]
as

SELECT        dbo.pos_customers.full_name as customer_name, dbo.pos_employees.full_name AS employee_name, dbo.pos_tickets.billNo, dbo.pos_tickets.date, dbo.pos_tickets.time, dbo.pos_tickets.no_of_items, dbo.pos_tickets.total_qty, 
dbo.pos_tickets.sub_total, dbo.pos_tickets.discount, dbo.pos_tickets.amount_due, dbo.pos_tickets.paid, dbo.pos_tickets.advance_amount,
dbo.pos_tickets.status, dbo.pos_products.prod_name, dbo.pos_products.barcode, dbo.pos_ticket_details.quantity, dbo.pos_ticket_details.pkg, dbo.pos_ticket_details.full_pkg, dbo.pos_ticket_details.discount as perItemDiscount, 
dbo.pos_ticket_details.Total_price, dbo.pos_stock_details.pur_price, dbo.pos_stock_details.sale_price, dbo.pos_stock_details.market_value, dbo.pos_tickets.total_taxation , dbo.pos_tickets.balance
FROM  dbo.pos_customers INNER JOIN
dbo.pos_tickets ON dbo.pos_customers.customer_id = dbo.pos_tickets.customer_id INNER JOIN
dbo.pos_employees ON dbo.pos_tickets.employee_id = dbo.pos_employees.employee_id INNER JOIN
dbo.pos_ticket_details ON dbo.pos_tickets.sales_acc_id = dbo.pos_ticket_details.sales_acc_id INNER JOIN
dbo.pos_products ON dbo.pos_ticket_details.prod_id = dbo.pos_products.product_id INNER JOIN
dbo.pos_stock_details ON dbo.pos_products.product_id = dbo.pos_stock_details.prod_id

GO
/****** Object:  View [dbo].[ViewBankDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[ViewBankDetails]
as

SELECT        dbo.pos_banking_details.date as [Date], dbo.pos_banking_details.time as [Time], dbo.pos_employees.full_name AS Employee, dbo.pos_transaction_status.status_title AS [T.Status], 
                         dbo.pos_transaction_type.transaction_type AS [T.Type], dbo.pos_bank.bank_title AS [Bank Title], dbo.pos_bank_branch.branch_title AS Branch, dbo.pos_bank_account.account_title AS [Account Title], 
                         dbo.pos_account_no.account_no AS [Account #], dbo.pos_banking_details.amount
FROM            dbo.pos_account_no INNER JOIN
                         dbo.pos_banking_details ON dbo.pos_account_no.account_no_id = dbo.pos_banking_details.account_no_id INNER JOIN
                         dbo.pos_bank ON dbo.pos_banking_details.bank_id = dbo.pos_bank.bank_id INNER JOIN
                         dbo.pos_bank_account ON dbo.pos_banking_details.account_id = dbo.pos_bank_account.account_id INNER JOIN
                         dbo.pos_bank_branch ON dbo.pos_banking_details.branch_id = dbo.pos_bank_branch.branch_id INNER JOIN
                         dbo.pos_employees ON dbo.pos_banking_details.employee_id = dbo.pos_employees.employee_id INNER JOIN
                         dbo.pos_transaction_status ON dbo.pos_banking_details.status_id = dbo.pos_transaction_status.status_id INNER JOIN
                         dbo.pos_transaction_type ON dbo.pos_banking_details.t_type_id = dbo.pos_transaction_type.transaction_id












GO
/****** Object:  View [dbo].[ViewBankLoanDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[ViewBankLoanDetails]
as

SELECT        dbo.pos_bankLoansDetails.date AS [Date], dbo.pos_bankLoansDetails.time as [Time], dbo.pos_bankLoansDetails.bank_name AS [Bank Title], dbo.pos_bankLoansDetails.code, 
                         dbo.pos_bank_branch.branch_title AS [Branch Title], dbo.pos_transaction_type.transaction_type AS [Repay Type], dbo.pos_bankLoansDetails.principle, dbo.pos_bankLoansDetails.interest, 
                         dbo.pos_bankLoansDetails.totalAmount AS [Total Amount], dbo.pos_bankLoanPayables.last_balance AS [Last Balance], dbo.pos_bankLoansDetails.remarks AS Note, dbo.pos_bankLoansDetails.status
FROM            dbo.pos_bank_branch INNER JOIN
                         dbo.pos_bankLoansDetails ON dbo.pos_bank_branch.branch_id = dbo.pos_bankLoansDetails.branch_id INNER JOIN
                         dbo.pos_bankLoanPayables ON dbo.pos_bankLoansDetails.BankLoan_id = dbo.pos_bankLoanPayables.BankLoan_id INNER JOIN
                         dbo.pos_transaction_type ON dbo.pos_bankLoansDetails.t_type_id = dbo.pos_transaction_type.transaction_id








GO
/****** Object:  View [dbo].[ViewBankLoanPaybookDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE view [dbo].[ViewBankLoanPaybookDetails]

as

SELECT        dbo.pos_bankLoanPaybook.date, dbo.pos_bankLoanPaybook.time, dbo.pos_bankLoanPaybook.paymentDate, dbo.pos_bankLoansDetails.bank_name, dbo.pos_bankLoansDetails.code, dbo.pos_transaction_status.status_title, 
dbo.pos_employees.full_name, dbo.pos_employees.emp_code, dbo.pos_bankLoanPayables.last_balance, dbo.pos_bankLoanPayables.last_payment, dbo.pos_bankLoanPaybook.reference, 
dbo.pos_bankLoanPaybook.remarks, dbo.pos_bankLoanPaybook.amount, dbo.pos_bankLoanPaybook.previous_payables, dbo.pos_bankLoanPaybook.balance
FROM            dbo.pos_bankLoanPaybook INNER JOIN
dbo.pos_bankLoansDetails ON dbo.pos_bankLoanPaybook.BankLoan_id = dbo.pos_bankLoansDetails.BankLoan_id INNER JOIN
dbo.pos_bankLoanPayables ON dbo.pos_bankLoansDetails.BankLoan_id = dbo.pos_bankLoanPayables.BankLoan_id INNER JOIN
dbo.pos_transaction_status ON dbo.pos_bankLoanPaybook.status_id = dbo.pos_transaction_status.status_id INNER JOIN
dbo.pos_employees ON dbo.pos_bankLoanPaybook.employee_id = dbo.pos_employees.employee_id










GO
/****** Object:  View [dbo].[ViewCapitalHistoryDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[ViewCapitalHistoryDetails]
as


SELECT        date AS Date, time, amount, total_capital AS [Total Capital], total_investment AS [Total Investment], remarks AS Note, status
FROM            dbo.pos_capital_history

























GO
/****** Object:  View [dbo].[ViewCashOutDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[ViewCashOutDetails]
as

SELECT        dbo.pos_clock_out.id as [ID], dbo.pos_clock_out.date as [Date], dbo.pos_clock_in.start_time AS [Start Time], dbo.pos_clock_out.end_time AS [End Time], dbo.pos_clock_out.total_hours AS Duration, dbo.pos_employees.full_name AS [User], 
dbo.pos_clock_out.opening_cash AS [Opening Cash], dbo.pos_clock_out.total_sales AS [Total Sales], dbo.pos_clock_out.total_return_amount AS [Total Returns], dbo.pos_clock_out.total_void_orders AS [Void Orders], 
dbo.pos_clock_out.expected_amount AS [Expected Cash], dbo.pos_clock_out.cash_amount_received AS [Cash Received],  dbo.pos_clock_out.shortage_amount AS [Shortage Amount], dbo.pos_clock_out.balance as [Balance], dbo.pos_clock_out.remarks as [Remarks]
FROM            dbo.pos_clock_in INNER JOIN
dbo.pos_clock_out ON dbo.pos_clock_in.id = dbo.pos_clock_out.clock_in_id INNER JOIN
dbo.pos_users AS from_user ON dbo.pos_clock_in.from_user_id = from_user.user_id INNER JOIN
dbo.pos_users AS to_user ON dbo.pos_clock_in.to_user_id = to_user.user_id INNER JOIN
dbo.pos_employees ON dbo.pos_employees.employee_id = to_user.emp_id





GO
/****** Object:  View [dbo].[ViewCharityDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE View [dbo].[ViewCharityDetails]
as

SELECT formDate as [From Date], toDate as [To Date], paymentDate as [Payment Date], 
time as [Time], fullName as [Full Name], fatherName as [Father Name], mobile_no as [Mobile No], amount as [Payment], reference as [References], 
note as [Note], lessAmount as [less Amount], netProfit as [Profit], balance as [Balance]
FROM dbo.pos_charityDetails












GO
/****** Object:  View [dbo].[ViewClockInDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE view [dbo].[ViewClockInDetails]
as

SELECT        dbo.pos_clock_in.id, dbo.pos_clock_in.date as [Date], dbo.pos_employees.full_name AS [User], dbo.pos_shift.title AS [Shift], dbo.pos_counter.title AS [Counter], dbo.pos_clock_in.amount as [Opening Amount], dbo.pos_clock_in.remarks as [Remarks], 
CASE WHEN dbo.pos_clock_in.status IS NOT NULL AND (dbo.pos_clock_in.status = '0' or dbo.pos_clock_in.status = '-1') THEN 'Clocked-In' ELSE 'Clocked-Out' END AS [Status]
FROM            dbo.pos_clock_in INNER JOIN
dbo.pos_counter ON dbo.pos_clock_in.counter_id = dbo.pos_counter.id INNER JOIN
dbo.pos_shift ON dbo.pos_clock_in.shift_id = dbo.pos_shift.id INNER JOIN
dbo.pos_users AS from_user ON dbo.pos_clock_in.from_user_id = from_user.user_id INNER JOIN
dbo.pos_users AS to_user ON dbo.pos_clock_in.to_user_id = to_user.user_id INNER JOIN
dbo.pos_employees ON dbo.pos_employees.employee_id = to_user.emp_id


GO
/****** Object:  View [dbo].[ViewCounterCashDeals]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[ViewCounterCashDeals]
as

SELECT DISTINCT deal_title
FROM            dbo.pos_deals
WHERE        (status = 'Active')




GO
/****** Object:  View [dbo].[ViewCounterCashGroupedIndividualItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[ViewCounterCashGroupedIndividualItems]
as

SELECT        dbo.pos_products.product_id, dbo.pos_stock_details.stock_id, dbo.pos_products.prod_name, dbo.pos_stock_details.item_barcode, dbo.pos_products.image_path, dbo.pos_category.title, dbo.pos_products.status, 
dbo.pos_stock_details.sale_price, dbo.pos_stock_details.market_value, dbo.pos_stock_details.quantity
FROM            dbo.pos_products INNER JOIN
dbo.pos_category ON dbo.pos_products.category_id = dbo.pos_category.category_id INNER JOIN
dbo.pos_stock_details ON dbo.pos_stock_details.prod_id = dbo.pos_products.product_id




GO
/****** Object:  View [dbo].[ViewCounterCashGroupedItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[ViewCounterCashGroupedItems] 
as
SELECT DISTINCT dbo.pos_category.title
FROM            dbo.pos_products INNER JOIN
                         dbo.pos_category ON dbo.pos_products.category_id = dbo.pos_category.category_id










GO
/****** Object:  View [dbo].[ViewCounterCashRegularItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE view [dbo].[ViewCounterCashRegularItems]
as

SELECT        dbo.pos_products.product_id, dbo.pos_products.prod_name, dbo.pos_products.image_path, 
dbo.pos_stock_details.sale_price, dbo.pos_stock_details.market_value, dbo.pos_stock_details.quantity, dbo.pos_products.status,
 dbo.pos_stock_details.item_barcode, dbo.pos_stock_details.stock_id
FROM            dbo.pos_grouped_items INNER JOIN
dbo.pos_products ON dbo.pos_grouped_items.prod_id = dbo.pos_products.product_id INNER JOIN
dbo.pos_stock_details ON dbo.pos_stock_details.stock_id = dbo.pos_grouped_items.stock_id





GO
/****** Object:  View [dbo].[ViewCounterSaleCategoryWiseItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[ViewCounterSaleCategoryWiseItems]
as

select prod_name, image_path, title from pos_products inner join pos_category on pos_products.category_id = pos_category.category_id
















GO
/****** Object:  View [dbo].[ViewCounterSaleRegularItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE view [dbo].[ViewCounterSaleRegularItems]
as

select prod_name, image_path from pos_grouped_items inner join  pos_products on pos_grouped_items.prod_id = pos_products.product_id 
where pos_products.status = 'Enabled'

















GO
/****** Object:  View [dbo].[ViewCustomerDuesDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE View [dbo].[ViewCustomerDuesDetails]
as


SELECT        dbo.pos_customers.full_name AS [Customer Name], dbo.pos_customers.cus_code AS Code, dbo.pos_customers.cnic, dbo.pos_country.title AS Country, dbo.pos_city.title AS Province, 
dbo.pos_customers.mobile_no AS [Mobile No], dbo.pos_customers.telephone_no AS [Telephone No],dbo.pos_customer_lastCredits.due_days AS [Last Recovery], dbo.pos_customer_lastCredits.lastCredits AS Credits, 
dbo.pos_customers.status
FROM            dbo.pos_city INNER JOIN
dbo.pos_customers ON dbo.pos_city.city_id = dbo.pos_customers.city_id INNER JOIN
dbo.pos_country ON dbo.pos_customers.country_id = dbo.pos_country.country_id INNER JOIN
dbo.pos_customer_lastCredits ON dbo.pos_customers.customer_id = dbo.pos_customer_lastCredits.customer_id
WHERE        (dbo.pos_customer_lastCredits.lastCredits <> '0') AND (dbo.pos_customer_lastCredits.lastCredits > '0')














GO
/****** Object:  View [dbo].[ViewCustomerLastReceipt]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[ViewCustomerLastReceipt]
as

SELECT        dbo.pos_sales_accounts.sales_acc_id AS SID, dbo.pos_sales_accounts.billNo AS [Receipt No], dbo.pos_sales_accounts.date AS [Date], dbo.pos_sales_accounts.time as [Time], dbo.pos_employees.full_name AS Employee, 
CASE WHEN pos_customers.full_name IS NOT NULL AND pos_customers.full_name != 'nill' THEN pos_customers.full_name ELSE 'Walk-In' END AS Customer, dbo.pos_customers.cus_code AS Code, 
dbo.pos_sales_accounts.no_of_items AS [T.Items], dbo.pos_sales_accounts.total_qty AS [T.Qty], dbo.pos_sales_accounts.sub_total AS [Sub Total], dbo.pos_sales_accounts.discount as [Discount], 
dbo.pos_sales_accounts.amount_due AS [Amount Due], dbo.pos_sales_accounts.paid AS Cash, dbo.pos_sales_accounts.advance_payment AS [Advance], dbo.pos_sales_accounts.credit_card_amount AS [Credit Card], dbo.pos_sales_accounts.paypal_amount AS [Apple Pay], 
dbo.pos_sales_accounts.google_pay_amount AS [Zelle/Cashapp/Venmo], dbo.pos_sales_accounts.credits as [Credits], dbo.pos_sales_accounts.pCredits AS [P.Credits], dbo.pos_sales_accounts.check_sale_status AS [Payment Type], 
dbo.pos_sales_accounts.is_returned AS [Is Returned]
FROM            dbo.pos_sales_accounts INNER JOIN
dbo.pos_customers ON dbo.pos_sales_accounts.customer_id = dbo.pos_customers.customer_id INNER JOIN
dbo.pos_employees ON dbo.pos_sales_accounts.employee_id = dbo.pos_employees.employee_id

GO
/****** Object:  View [dbo].[ViewCustomerRecoveryDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[ViewCustomerRecoveryDetails]
as

SELECT        dbo.pos_recovery_details.date AS [Date], dbo.pos_recovery_details.time as [Time], dbo.pos_recoveries.installmentDate AS [Recovery Date], 
                         dbo.pos_employees.full_name AS [Received By], dbo.pos_customers.full_name AS Customer, dbo.pos_customers.cus_code AS Code, dbo.pos_customers.mobile_no AS [Mobile No], 
                         dbo.pos_customer_lastCredits.due_days AS [Last Recovery], dbo.pos_recoveries.amount AS [Paid Amount], dbo.pos_recoveries.credits as [Credits], 
                         dbo.pos_customer_lastCredits.lastCredits AS [Previous Payables], dbo.pos_recovery_details.reference AS [References], dbo.pos_recovery_details.remarks AS Description
FROM            dbo.pos_customer_lastCredits INNER JOIN
                         dbo.pos_customers ON dbo.pos_customer_lastCredits.customer_id = dbo.pos_customers.customer_id INNER JOIN
                         dbo.pos_recovery_details ON dbo.pos_customers.customer_id = dbo.pos_recovery_details.customer_id INNER JOIN
                         dbo.pos_employees ON dbo.pos_recovery_details.employee_id = dbo.pos_employees.employee_id INNER JOIN
                         dbo.pos_recoveries ON dbo.pos_recovery_details.recovery_id = dbo.pos_recoveries.recovery_id












GO
/****** Object:  View [dbo].[ViewCustomerSalesReturnLastReceipt]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[ViewCustomerSalesReturnLastReceipt]

as


SELECT        dbo.pos_return_accounts.return_acc_id AS SID, dbo.pos_return_accounts.billNo AS [Receipt No], dbo.pos_return_accounts.date as [Date], dbo.pos_return_accounts.time as [Time], dbo.pos_employees.full_name AS Employee, 
CASE WHEN pos_customers.full_name IS NOT NULL AND pos_customers.full_name != 'nill' THEN pos_customers.full_name ELSE 'Walk-In' END AS Customer, dbo.pos_customers.cus_code AS Code, 
dbo.pos_return_accounts.no_of_items AS [T.Items], dbo.pos_return_accounts.total_qty AS [T.Qty], dbo.pos_return_accounts.sub_total AS [Sub Total], dbo.pos_return_accounts.discount as [Discount], 
dbo.pos_return_accounts.amount_due AS [Amount Due], dbo.pos_return_accounts.paid AS Cash, dbo.pos_return_accounts.advance_payment AS [Advance], dbo.pos_return_accounts.credit_card_amount AS [Credit Card], dbo.pos_return_accounts.paypal_amount AS [Apple Pay], 
dbo.pos_return_accounts.google_pay_amount AS [Zelle/Cashapp/Venmo], dbo.pos_return_accounts.credits as [Credits]
FROM            dbo.pos_return_accounts INNER JOIN
dbo.pos_customers ON dbo.pos_return_accounts.customer_id = dbo.pos_customers.customer_id INNER JOIN
dbo.pos_employees ON dbo.pos_return_accounts.employee_id = dbo.pos_employees.employee_id

GO
/****** Object:  View [dbo].[ViewCustomersDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE view [dbo].[ViewCustomersDetails]
as


SELECT        dbo.pos_customers.date, dbo.pos_customers.full_name AS [Customer Name], dbo.pos_customers.cus_code AS Code, dbo.pos_customers.mobile_no AS [Cell No 1], dbo.pos_customers.telephone_no AS [Cell No 2], dbo.pos_customers.address1 AS Address, 
dbo.pos_customers.points, dbo.pos_customers.discount, dbo.pos_customers.credit_limit AS [C.Limit], dbo.pos_customers.status
FROM            dbo.pos_customers INNER JOIN
dbo.pos_country ON dbo.pos_customers.country_id = dbo.pos_country.country_id INNER JOIN
dbo.pos_city ON dbo.pos_customers.city_id = dbo.pos_city.city_id INNER JOIN
dbo.pos_batchNo ON dbo.pos_customers.batch_id = dbo.pos_batchNo.batch_id


GO
/****** Object:  View [dbo].[ViewCustomerWiseOrders]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[ViewCustomerWiseOrders]
as

SELECT        dbo.pos_sales_accounts.sales_acc_id AS SID, dbo.pos_sales_accounts.clock_in_id AS CID, dbo.pos_sales_accounts.billNo AS [Receipt No], dbo.pos_sales_accounts.date as [Date], dbo.pos_sales_accounts.time as [Time], 
dbo.pos_employees.full_name AS Employee, CASE WHEN pos_customers.full_name IS NOT NULL AND pos_customers.full_name != 'nill' THEN pos_customers.full_name ELSE 'Walk-In' END AS Customer, 
dbo.pos_sales_accounts.no_of_items AS [T.Items], dbo.pos_sales_accounts.total_qty AS [T.Qty], dbo.pos_sales_accounts.total_taxation AS Tax, dbo.pos_sales_accounts.sub_total AS [Sub Total], 
dbo.pos_sales_accounts.discount as [Discount], dbo.pos_sales_accounts.amount_due AS [Amount Due], dbo.pos_sales_accounts.paid AS Cash, dbo.pos_sales_accounts.advance_payment AS [Advance], dbo.pos_sales_accounts.credit_card_amount AS [Credit Card], 
dbo.pos_sales_accounts.paypal_amount AS [Apple Pay], dbo.pos_sales_accounts.google_pay_amount AS [Zelle/Cashapp/Venmo], dbo.pos_sales_accounts.credits as [Credits],
dbo.pos_sales_accounts.check_sale_status AS [Payment Type]
FROM            dbo.pos_sales_accounts INNER JOIN
dbo.pos_customers ON dbo.pos_sales_accounts.customer_id = dbo.pos_customers.customer_id INNER JOIN
dbo.pos_employees ON dbo.pos_sales_accounts.employee_id = dbo.pos_employees.employee_id

GO
/****** Object:  View [dbo].[ViewCustomerWiseRecovery]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE view [dbo].[ViewCustomerWiseRecovery]

as

SELECT        dbo.pos_recovery_details.date AS [Date], dbo.pos_employees.full_name AS [Received By], dbo.pos_customers.full_name AS Customer, dbo.pos_customers.cus_code AS Code, 
                         dbo.pos_customer_lastCredits.due_days AS [Last Recovery], dbo.pos_recoveries.amount AS [Paid Amount], dbo.pos_recoveries.credits as [Credits], dbo.pos_customer_lastCredits.lastCredits AS [P.Credits], 
                         dbo.pos_recovery_details.reference AS [References]
FROM            dbo.pos_customer_lastCredits INNER JOIN
                         dbo.pos_customers ON dbo.pos_customer_lastCredits.customer_id = dbo.pos_customers.customer_id INNER JOIN
                         dbo.pos_recovery_details ON dbo.pos_customers.customer_id = dbo.pos_recovery_details.customer_id INNER JOIN
                         dbo.pos_employees ON dbo.pos_recovery_details.employee_id = dbo.pos_employees.employee_id INNER JOIN
                         dbo.pos_recoveries ON dbo.pos_recovery_details.recovery_id = dbo.pos_recoveries.recovery_id










GO
/****** Object:  View [dbo].[ViewDateWiseOrderReturned]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[ViewDateWiseOrderReturned]
as


SELECT        dbo.pos_return_accounts.billNo AS [Receipt No], dbo.pos_return_accounts.date AS [Date], dbo.pos_customers.full_name AS Customer, dbo.pos_employees.full_name AS Employee, 
dbo.pos_return_accounts.no_of_items AS [T.Items], dbo.pos_return_accounts.total_qty AS [T.Qty], dbo.pos_return_accounts.sub_total AS [Sub Total], dbo.pos_return_accounts.discount, 
dbo.pos_return_accounts.amount_due AS [Amount Due], dbo.pos_return_accounts.paid AS Cash, dbo.pos_return_accounts.credits, dbo.pos_return_accounts.pCredits AS [P.Credits], dbo.pos_return_accounts.status
FROM            dbo.pos_return_accounts INNER JOIN
dbo.pos_customers ON dbo.pos_return_accounts.customer_id = dbo.pos_customers.customer_id INNER JOIN
dbo.pos_employees ON dbo.pos_return_accounts.employee_id = dbo.pos_employees.employee_id








GO
/****** Object:  View [dbo].[ViewDateWiseOrdersSold]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[ViewDateWiseOrdersSold]
as

SELECT        dbo.pos_sales_accounts.billNo AS [Receipt No], dbo.pos_sales_accounts.date AS [Date], dbo.pos_customers.full_name AS Customer, dbo.pos_employees.full_name AS Employee, 
dbo.pos_sales_accounts.no_of_items AS [T.Items], dbo.pos_sales_accounts.total_qty AS [T.Qty], dbo.pos_sales_accounts.sub_total AS [Sub Total], dbo.pos_sales_accounts.discount, 
dbo.pos_sales_accounts.amount_due AS [Amount Due], dbo.pos_sales_accounts.paid AS Cash, dbo.pos_sales_accounts.credits, dbo.pos_sales_accounts.pCredits AS [P.Credits], dbo.pos_sales_accounts.status as [Status]
FROM            dbo.pos_sales_accounts INNER JOIN
dbo.pos_customers ON dbo.pos_sales_accounts.customer_id = dbo.pos_customers.customer_id INNER JOIN
dbo.pos_employees ON dbo.pos_sales_accounts.employee_id = dbo.pos_employees.employee_id








GO
/****** Object:  View [dbo].[ViewDealDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[ViewDealDetails]
as

SELECT        deal_id AS [Promotion ID], deal_title AS [Promotion Title], note as [Note], status as [Status]
FROM            dbo.pos_deals




GO
/****** Object:  View [dbo].[ViewDemandList]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[ViewDemandList]
as

SELECT        dbo.pos_demand_list.date as [Date], dbo.pos_demand_list.bill_no AS [Demand No], dbo.pos_employees.full_name AS Employee, dbo.pos_suppliers.full_name AS Supplier, dbo.pos_demand_list.no_of_items AS [T. Items], 
dbo.pos_demand_list.total_qty AS [T. Quantity], dbo.pos_demand_list.net_amount AS [Net Amount], dbo.pos_demand_list.paid as [Paid], dbo.pos_demand_list.discount as [Discount], dbo.pos_demand_list.credits as [Credits]
FROM dbo.pos_demand_list  INNER JOIN
dbo.pos_employees ON dbo.pos_demand_list.employee_id = dbo.pos_employees.employee_id INNER JOIN
dbo.pos_suppliers ON dbo.pos_demand_list.supplier_id = dbo.pos_suppliers.supplier_id



GO
/****** Object:  View [dbo].[ViewEmployeeCommission]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[ViewEmployeeCommission]

as

SELECT        dbo.pos_employee_commission.commission_id AS CID, dbo.pos_employee_commission.end_time AS [Commission Title], dbo.pos_employees.full_name AS [Employee Name], 
                         dbo.pos_employee_commission.start_date AS [Start Date], dbo.pos_employee_commission.end_date AS [End Date], dbo.pos_employee_commission.start_date AS [Start Time], 
                         dbo.pos_employee_commission.end_date AS [End Time], dbo.pos_employee_commission.commission_amount AS [Commission Amount], dbo.pos_employee_commission.commission_percentage AS [Commission %], 
                         dbo.pos_employee_commission.status
FROM            dbo.pos_employees INNER JOIN
                         dbo.pos_employee_commission ON dbo.pos_employees.employee_id = dbo.pos_employee_commission.employee_id


GO
/****** Object:  View [dbo].[ViewEmployeeSalariesDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE view [dbo].[ViewEmployeeSalariesDetails]
as

SELECT        dbo.pos_salariesPaybook.salary_id, dbo.pos_salariesPaybook.date, dbo.pos_salariesPaybook.time, dbo.pos_salariesPaybook.paymentDate, dbo.pos_salariesPaybook.amount, dbo.pos_salariesPaybook.credits, 
dbo.pos_salariesPaybook.balance, dbo.pos_salariesPaybook.reference, dbo.pos_salariesPaybook.remarks, dbo.pos_employees.full_name, dbo.pos_employees.emp_code, dbo.pos_employees.cnic, 
dbo.pos_employees.mobile_no, dbo.pos_employees.address1, dbo.pos_employees.email, dbo.pos_salariesPaybook.salary, dbo.pos_salariesPaybook.commission, dbo.pos_salariesPaybook.hourly_wages, 
dbo.pos_salariesPaybook.form_date, dbo.pos_salariesPaybook.to_date, dbo.pos_salariesPaybook.commission_payment, dbo.pos_salariesPaybook.total_duration,  dbo.pos_salariesPaybook.previous_paid_salary, dbo.pos_salariesPaybook.previous_paid_commission, dbo.pos_salariesPaybook.commission_balance 
FROM            dbo.pos_salariesPaybook INNER JOIN
dbo.pos_employees ON dbo.pos_salariesPaybook.employee_id = dbo.pos_employees.employee_id


GO
/****** Object:  View [dbo].[ViewEmployeesDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[ViewEmployeesDetails]
as


SELECT        dbo.pos_employees.full_name AS [Employee Name], dbo.pos_employees.emp_code AS Code, 
dbo.pos_employees.mobile_no AS [Mobile No], dbo.pos_employees.address1 AS Address, dbo.pos_employees.salary, dbo.pos_employees.status AS [Status], 
dbo.pos_employees.email as [Email]
FROM            dbo.pos_employees INNER JOIN
dbo.pos_country ON dbo.pos_employees.country_id = dbo.pos_country.country_id INNER JOIN
dbo.pos_city ON dbo.pos_employees.city_id = dbo.pos_city.city_id



GO
/****** Object:  View [dbo].[ViewExpensesDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE view [dbo].[ViewExpensesDetails]

as

SELECT        dbo.pos_expense_details.date AS [Date], dbo.pos_expense_details.time as [Time], dbo.pos_expenses.title AS [Expense Title], dbo.pos_expense_items.amount as [Amount], dbo.pos_expense_items.remarks AS Note
FROM            dbo.pos_expense_items INNER JOIN
                         dbo.pos_expense_details ON dbo.pos_expense_items.expense_id = dbo.pos_expense_details.expense_id INNER JOIN
                         dbo.pos_expenses ON dbo.pos_expense_details.exp_id = dbo.pos_expenses.exp_id










GO
/****** Object:  View [dbo].[ViewExpiredItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[ViewExpiredItems]
as

SELECT      pos_products.product_id as [PID], pos_stock_details.stock_id as [SID],  dbo.pos_expired_items.date AS [Expired Date], dbo.pos_products.prod_name AS [Product Name], dbo.pos_stock_details.item_barcode as [Barcode], dbo.pos_category.title AS Category, dbo.pos_brand.brand_title AS Brand,  
dbo.pos_expired_items.quantity AS [QTY], dbo.pos_expired_items.pkg as [PKG], dbo.pos_expired_items.full_pak AS [Full Pack], dbo.pos_expired_items.pur_price AS [Purchase], dbo.pos_expired_items.sale_price AS [Sale Price], 
dbo.pos_products.status as [Status]
FROM            dbo.pos_expired_items INNER JOIN
dbo.pos_products ON dbo.pos_expired_items.prod_id = dbo.pos_products.product_id INNER JOIN
dbo.pos_subcategory ON dbo.pos_products.sub_cate_id = dbo.pos_subcategory.sub_cate_id INNER JOIN
dbo.pos_category ON dbo.pos_products.category_id = dbo.pos_category.category_id INNER JOIN
dbo.pos_brand ON dbo.pos_products.brand_id = dbo.pos_brand.brand_id inner join pos_stock_details on  pos_stock_details.stock_id = pos_expired_items.stock_id



GO
/****** Object:  View [dbo].[ViewGranterDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE View [dbo].[ViewGranterDetails]
as

SELECT        dbo.pos_granters.date as date, dbo.pos_granters.full_name AS [Guarantor Name], dbo.pos_granters.fatherName AS [Father Name], dbo.pos_granters.code, dbo.pos_granters.cnic, dbo.pos_country.title AS Country, 
                         dbo.pos_city.title AS Province, dbo.pos_granters.mobile_no AS [Mobile No], dbo.pos_granters.telephone_no AS [Telephone No], dbo.pos_granters.address1 AS Address, dbo.pos_granters.email, dbo.pos_granters.status
FROM            dbo.pos_granters INNER JOIN
                         dbo.pos_city ON dbo.pos_granters.city_id = dbo.pos_city.city_id INNER JOIN
                         dbo.pos_country ON dbo.pos_granters.country_id = dbo.pos_country.country_id















GO
/****** Object:  View [dbo].[ViewGroupedItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[ViewGroupedItems]
as

SELECT     pos_products.product_id as [PID], pos_stock_details.stock_id as [SID], dbo.pos_products.prod_name AS [Product Name], dbo.pos_stock_details.item_barcode as [Barcode], dbo.pos_category.title AS Category, dbo.pos_brand.brand_title AS Brand, dbo.pos_subcategory.title AS [Sub Category], 
dbo.pos_stock_details.date_of_expiry as [Expiry],  dbo.pos_stock_details.quantity as [Stock], dbo.pos_stock_details.pur_price as [Purchase], dbo.pos_stock_details.sale_price as [Sale Price], 
dbo.pos_products.status as [Status]
FROM            dbo.pos_stock_details INNER JOIN
dbo.pos_products ON dbo.pos_stock_details.prod_id = dbo.pos_products.product_id INNER JOIN
dbo.pos_subcategory ON dbo.pos_products.sub_cate_id = dbo.pos_subcategory.sub_cate_id INNER JOIN
dbo.pos_category ON dbo.pos_products.category_id = dbo.pos_category.category_id INNER JOIN
dbo.pos_brand ON dbo.pos_products.brand_id = dbo.pos_brand.brand_id INNER JOIN
dbo.pos_color ON dbo.pos_products.color_id = dbo.pos_color.color_id



GO
/****** Object:  View [dbo].[ViewInstallmentAccountDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE view [dbo].[ViewInstallmentAccountDetails]
as

SELECT DISTINCT 
dbo.pos_sales_accounts.billNo AS [Receipt No], dbo.pos_sales_accounts.date AS date, dbo.pos_employees.full_name AS Employee, dbo.pos_customers.full_name AS Customer, dbo.pos_customers.fatherName AS [Father Name], 
dbo.pos_customers.cus_code AS Code, dbo.pos_sales_accounts.no_of_items AS [T.Items], dbo.pos_sales_accounts.total_qty AS [T.Qty], dbo.pos_sales_accounts.sub_total AS [Sub Total], 
dbo.pos_sales_accounts.discount, dbo.pos_sales_accounts.amount_due AS [Amount Due], dbo.pos_sales_accounts.paid AS Cash, dbo.pos_sales_accounts.credits
FROM            dbo.pos_sales_accounts INNER JOIN
dbo.pos_customers ON dbo.pos_sales_accounts.customer_id = dbo.pos_customers.customer_id INNER JOIN
dbo.pos_employees ON dbo.pos_sales_accounts.employee_id = dbo.pos_employees.employee_id INNER JOIN
dbo.pos_installment_accounts ON dbo.pos_installment_accounts.sales_acc_id = dbo.pos_sales_accounts.sales_acc_id INNER JOIN
dbo.pos_installment_plan ON dbo.pos_installment_accounts.installment_acc_id = dbo.pos_installment_plan.installment_acc_id
WHERE        (dbo.pos_installment_plan.status = 'Incomplete')











GO
/****** Object:  View [dbo].[ViewInventoryHistory]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE view [dbo].[ViewInventoryHistory]
as

select pos_stock_history.id as [ID], pos_products.prod_name as [Product Name], pos_stock_details.item_barcode as [Barcode], pos_stock_history.date as [Date],
pos_stock_history.old_quantity as [Old Stock], pos_stock_history.new_quantity as [New Stock], pos_stock_history.old_cost_price as [Old Cost Price],
pos_stock_history.new_cost_price as [New Cost Price],pos_stock_history.old_sale_price as [Old Sale Price], pos_stock_history.new_sale_price as [New Sale Price],
pos_stock_history.details as [Detail], pos_employees.full_name as [Employee]
from pos_stock_history inner join pos_products on pos_products.product_id = pos_stock_history.product_id
inner join pos_stock_details on pos_stock_details.prod_id = pos_products.product_id
inner join pos_users on pos_users.user_id = pos_stock_history.user_id 
inner join pos_employees on pos_employees.employee_id = pos_users.emp_id


GO
/****** Object:  View [dbo].[ViewInvestorDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE view [dbo].[ViewInvestorDetails]
as

SELECT        dbo.pos_investors.date AS [Date], dbo.pos_investors.full_name AS [Investor Name], dbo.pos_investors.code as [Code], dbo.pos_investors.cnic as [CNIC], dbo.pos_country.title AS Country, dbo.pos_city.title AS Province, 
                         dbo.pos_investors.mobile_no AS [Mobile No], dbo.pos_investors.telephone_no AS [Telephone No], dbo.pos_investors.address1 AS Address, dbo.pos_investors.share_percentage AS [Share %], 
                         dbo.pos_investors.profit_percentage AS [Profit %], dbo.pos_investors.investment as [Investment], dbo.pos_investors.status as [Status]
FROM            dbo.pos_city INNER JOIN
                         dbo.pos_investors ON dbo.pos_city.city_id = dbo.pos_investors.city_id INNER JOIN
                         dbo.pos_country ON dbo.pos_investors.country_id = dbo.pos_country.country_id









GO
/****** Object:  View [dbo].[ViewInvestorPaybookDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE View [dbo].[ViewInvestorPaybookDetails]
as

SELECT        dbo.pos_investorPaybook.date as date, dbo.pos_investorPaybook.time, dbo.pos_employees.full_name AS Casher, dbo.pos_investors.full_name AS Investor, dbo.pos_investors.code, dbo.pos_investorPaybook.investment, 
                         dbo.pos_investorPaybook.investorShare AS [Share %], dbo.pos_investorPaybook.payment
FROM            dbo.pos_employees INNER JOIN
                         dbo.pos_investorPaybook ON dbo.pos_employees.employee_id = dbo.pos_investorPaybook.employee_id INNER JOIN
                         dbo.pos_investors ON dbo.pos_investorPaybook.investor_id = dbo.pos_investors.investor_id













GO
/****** Object:  View [dbo].[ViewInvestorPaymentsDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE View [dbo].[ViewInvestorPaymentsDetails]
as

SELECT        dbo.pos_investorPaybook.date as date, dbo.pos_investorPaybook.time, dbo.pos_employees.full_name AS Casher, dbo.pos_investors.full_name AS Investor, dbo.pos_investors.code, dbo.pos_investors.cnic, 
                         dbo.pos_investors.mobile_no AS [Mobile No], dbo.pos_investorPaybook.investment, dbo.pos_investorPaybook.investorShare AS [Share %], dbo.pos_investorPaybook.payment, 
                         dbo.pos_investorPaybook.reference AS [References], dbo.pos_investorPaybook.remarks AS Description
FROM            dbo.pos_employees INNER JOIN
                         dbo.pos_investorPaybook ON dbo.pos_employees.employee_id = dbo.pos_investorPaybook.employee_id INNER JOIN
                         dbo.pos_investors ON dbo.pos_investorPaybook.investor_id = dbo.pos_investors.investor_id












GO
/****** Object:  View [dbo].[ViewLoanGivenDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE view [dbo].[ViewLoanGivenDetails]
as

SELECT date AS [Date], time as [Time], fullName AS [Full Name], fatherName AS [Father Name], 
contactNo AS [Mobile No], amount AS Amount, reference AS [References], remarks as [Note], status as [Status]
FROM            dbo.pos_loanDetails













GO
/****** Object:  View [dbo].[ViewOrdersReturned]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[ViewOrdersReturned]
as


SELECT        dbo.pos_return_accounts.billNo AS [Receipt No], dbo.pos_return_accounts.date AS [Date], dbo.pos_customers.full_name AS Customer, dbo.pos_employees.full_name AS Employee, 
dbo.pos_return_accounts.no_of_items AS [T.Items], dbo.pos_return_accounts.total_qty AS [T.Qty], dbo.pos_return_accounts.sub_total AS [Sub Total], dbo.pos_return_accounts.total_taxation AS Tax, 
dbo.pos_return_accounts.discount, dbo.pos_return_accounts.amount_due AS [Amount Due], dbo.pos_return_accounts.paid AS Cash, dbo.pos_return_accounts.credit_card_amount AS [Credit Card], 
dbo.pos_return_accounts.paypal_amount AS Paypal, dbo.pos_return_accounts.google_pay_amount AS [Google Pay], dbo.pos_return_accounts.credits, dbo.pos_return_accounts.pCredits AS [P.Credits], 
dbo.pos_return_accounts.check_sale_status AS [Payment Type]
FROM            dbo.pos_return_accounts INNER JOIN
dbo.pos_customers ON dbo.pos_return_accounts.customer_id = dbo.pos_customers.customer_id INNER JOIN
dbo.pos_employees ON dbo.pos_return_accounts.employee_id = dbo.pos_employees.employee_id



GO
/****** Object:  View [dbo].[ViewOverAllOrders]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[ViewOverAllOrders]
as

SELECT        dbo.pos_sales_accounts.billNo AS [Receipt No], dbo.pos_sales_accounts.date as [Date], dbo.pos_employees.full_name AS Employee, dbo.pos_customers.full_name AS Customer,
dbo.pos_sales_accounts.no_of_items AS [T.Items], dbo.pos_sales_accounts.total_qty AS [T.Qty], dbo.pos_sales_accounts.total_taxation AS Tax, dbo.pos_sales_accounts.sub_total AS [Sub Total], 
dbo.pos_sales_accounts.discount as [Discount], dbo.pos_sales_accounts.amount_due AS [Amount Due], dbo.pos_sales_accounts.paid AS Cash, dbo.pos_sales_accounts.credit_card_amount AS [Credit Card], dbo.pos_sales_accounts.paypal_amount AS [Paypal], dbo.pos_sales_accounts.google_pay_amount AS [Google Pay],
dbo.pos_sales_accounts.credits as [Credits], dbo.pos_sales_accounts.pCredits AS [P Credits], 
dbo.pos_sales_accounts.check_sale_status AS [Payment Type]
FROM            dbo.pos_sales_accounts INNER JOIN
dbo.pos_customers ON dbo.pos_sales_accounts.customer_id = dbo.pos_customers.customer_id INNER JOIN
dbo.pos_employees ON dbo.pos_sales_accounts.employee_id = dbo.pos_employees.employee_id



GO
/****** Object:  View [dbo].[ViewProducts]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[ViewProducts]
as

SELECT        dbo.pos_products.product_id AS PID, dbo.pos_products.prod_name AS [Product Name], dbo.pos_category.title AS Category, dbo.pos_brand.brand_title AS Brand, dbo.pos_subcategory.title AS [Sub Category], 
                         dbo.pos_products.allow_promotion AS [Allow Promotion], dbo.pos_products.remarks AS Description, dbo.pos_products.status as [Status]
FROM            dbo.pos_products INNER JOIN
                         dbo.pos_subcategory ON dbo.pos_products.sub_cate_id = dbo.pos_subcategory.sub_cate_id INNER JOIN
                         dbo.pos_category ON dbo.pos_products.category_id = dbo.pos_category.category_id INNER JOIN
                         dbo.pos_brand ON dbo.pos_products.brand_id = dbo.pos_brand.brand_id



GO
/****** Object:  View [dbo].[ViewProductStocks]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[ViewProductStocks]
as


SELECT        dbo.pos_products.product_id AS PID, dbo.pos_products.prod_name AS [Product Name], dbo.pos_stock_details.item_barcode AS Barcode, dbo.pos_category.title AS Category, dbo.pos_brand.brand_title AS Brand, 
dbo.pos_stock_details.quantity AS Stock, dbo.pos_stock_details.sale_price AS [Price], ROUND(dbo.pos_stock_details.sale_price + dbo.pos_stock_details.sale_price * dbo.pos_stock_details.market_value / 100, 2) AS [Sale Price], 
dbo.pos_stock_details.date_of_expiry AS Expiry, dbo.pos_stock_details.stock_id AS SID, dbo.pos_products.status as [Status]
FROM            dbo.pos_stock_details INNER JOIN
dbo.pos_products ON dbo.pos_stock_details.prod_id = dbo.pos_products.product_id INNER JOIN
dbo.pos_brand ON dbo.pos_brand.brand_id = dbo.pos_products.brand_id INNER JOIN
dbo.pos_category ON dbo.pos_category.category_id = dbo.pos_products.category_id





GO
/****** Object:  View [dbo].[ViewPromoGroupItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[ViewPromoGroupItems]
as

SELECT       pos_products.product_id AS [PID], pos_stock_details.stock_id as [SID],  dbo.pos_products.prod_name AS [Product Name], dbo.pos_products.barcode as [Barcode], dbo.pos_promo_groups.title AS [Group Title], dbo.pos_category.title AS Category, dbo.pos_brand.brand_title AS Brand, 
dbo.pos_subcategory.title AS [Sub Category], dbo.pos_products.unit as [Unit], dbo.pos_products.status as [Status]
FROM            dbo.pos_stock_details INNER JOIN
dbo.pos_products ON dbo.pos_stock_details.prod_id = dbo.pos_products.product_id INNER JOIN
dbo.pos_subcategory ON dbo.pos_products.sub_cate_id = dbo.pos_subcategory.sub_cate_id INNER JOIN
dbo.pos_category ON dbo.pos_products.category_id = dbo.pos_category.category_id INNER JOIN
dbo.pos_brand ON dbo.pos_products.brand_id = dbo.pos_brand.brand_id INNER JOIN
dbo.pos_promo_group_items ON dbo.pos_promo_group_items.prod_id = dbo.pos_products.product_id INNER JOIN
dbo.pos_promo_groups ON dbo.pos_promo_groups.id = dbo.pos_promo_group_items.promo_group_id



GO
/****** Object:  View [dbo].[ViewPromotions]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[ViewPromotions]
as

select pos_promotions.id as [PID], pos_promotions.title as [Promo Title], pos_promo_groups.title as [Group Title],  start_date as [Start Date],
end_date as [End Date], start_time as [Start Time], end_time as [End Time], quantity as [Quantity],
new_price as [New Price], discount as [Discount], discount_percentage as [Discount %], status as [Status]
from pos_promotions inner join pos_promo_groups on pos_promo_groups.id = pos_promotions.promo_group_id






GO
/****** Object:  View [dbo].[ViewPurchaseReturnDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE view [dbo].[ViewPurchaseReturnDetails]
as

SELECT       dbo.pos_purchase_return.pur_return_id as [ID], dbo.pos_purchase_return.date as [Date], dbo.pos_suppliers.full_name AS Supplier, dbo.pos_purchase_return.bill_no AS [Bill No], dbo.pos_purchase_return.invoice_no AS [Invoice No], 
dbo.pos_purchase_return.no_of_items AS [Total Items], dbo.pos_purchase_return.total_quantity AS Quantity, dbo.pos_purchase_return.net_trade_off AS [Trade Off], dbo.pos_purchase_return.net_carry_exp AS Shipping, 
dbo.pos_purchase_return.net_total AS [Sub Total], dbo.pos_purchase_return.discount_percentage AS [Discount %], dbo.pos_purchase_return.discount_amount AS [Discount], dbo.pos_purchase_return.paid as [Paid Amount], dbo.pos_purchase_return.credits as [Credits], dbo.pos_purchase_return.freight as [Freight], 
dbo.pos_purchase_return.fee_amount as [Fee Amount], dbo.pos_suppliers.remarks AS Note, 
CASE WHEN dbo.pos_purchase_return.status IS NOT NULL AND (dbo.pos_purchase_return.status = '0') THEN 'Pending' ELSE 'Completed' END AS Status
FROM            dbo.pos_purchase_return INNER JOIN
dbo.pos_suppliers ON dbo.pos_purchase_return.supplier_id = dbo.pos_suppliers.supplier_id INNER JOIN
dbo.pos_employees ON dbo.pos_purchase_return.employee_id = dbo.pos_employees.employee_id



--update pos_purchase set status = '1'
--update pos_purchase_return set status = '1'


GO
/****** Object:  View [dbo].[ViewPurchasingDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE view [dbo].[ViewPurchasingDetails]
as

SELECT       dbo.pos_purchase.purchase_id as [ID], dbo.pos_purchase.date as [Date], dbo.pos_suppliers.full_name AS Supplier, dbo.pos_purchase.bill_no AS [Bill No], dbo.pos_purchase.invoice_no AS [Invoice No], dbo.pos_purchase.no_of_items AS [Total Items], 
dbo.pos_purchase.total_quantity AS Quantity, dbo.pos_purchase.net_trade_off AS [Trade Off], dbo.pos_purchase.net_carry_exp AS Shipping, dbo.pos_purchase.net_total AS [Sub Total], dbo.pos_purchase.discount_percentage AS [Discount %], 
dbo.pos_purchase.discount_amount AS [Discount], dbo.pos_purchase.paid as [Paid Amount], 
dbo.pos_purchase.credits as [Credits], dbo.pos_purchase.freight as [Freight], dbo.pos_purchase.freight as [Fee Amount], dbo.pos_suppliers.remarks AS Note, CASE WHEN dbo.pos_purchase.status IS NOT NULL AND (dbo.pos_purchase.status = '0') THEN 'Pending' ELSE 'Completed' END AS Status
FROM            dbo.pos_purchase INNER JOIN
dbo.pos_suppliers ON dbo.pos_purchase.supplier_id = dbo.pos_suppliers.supplier_id INNER JOIN
dbo.pos_employees ON dbo.pos_purchase.employee_id = dbo.pos_employees.employee_id


GO
/****** Object:  View [dbo].[ViewReturnPricesAdjustments]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE view [dbo].[ViewReturnPricesAdjustments]
as


SELECT dbo.pos_products.product_id, pos_return_accounts.return_acc_id, dbo.pos_return_accounts.date, dbo.pos_products.prod_name, dbo.pos_products.barcode, dbo.pos_stock_details.pur_price, dbo.pos_stock_details.sale_price, dbo.pos_stock_details.market_value, 
dbo.pos_products.size, dbo.pos_returns_details.quantity
FROM dbo.pos_customers INNER JOIN dbo.pos_return_accounts ON dbo.pos_customers.customer_id = dbo.pos_return_accounts.customer_id INNER JOIN
dbo.pos_employees ON dbo.pos_return_accounts.employee_id = dbo.pos_employees.employee_id INNER JOIN
dbo.pos_returns_details ON dbo.pos_return_accounts.return_acc_id = dbo.pos_returns_details.return_acc_id INNER JOIN
dbo.pos_products ON dbo.pos_returns_details.prod_id = dbo.pos_products.product_id INNER JOIN
dbo.pos_stock_details ON dbo.pos_products.product_id = dbo.pos_stock_details.prod_id










GO
/****** Object:  View [dbo].[ViewSalesPricesAdjustments]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE view [dbo].[ViewSalesPricesAdjustments]

as

SELECT dbo.pos_products.product_id, pos_sales_accounts.sales_acc_id, dbo.pos_sales_accounts.date, dbo.pos_products.prod_name, dbo.pos_products.barcode, dbo.pos_stock_details.pur_price, dbo.pos_stock_details.sale_price, dbo.pos_stock_details.market_value, 
dbo.pos_products.size, dbo.pos_sales_details.quantity
FROM            dbo.pos_customers INNER JOIN
dbo.pos_sales_accounts ON dbo.pos_customers.customer_id = dbo.pos_sales_accounts.customer_id INNER JOIN
dbo.pos_employees ON dbo.pos_sales_accounts.employee_id = dbo.pos_employees.employee_id INNER JOIN
dbo.pos_sales_details ON dbo.pos_sales_accounts.sales_acc_id = dbo.pos_sales_details.sales_acc_id INNER JOIN
dbo.pos_products ON dbo.pos_sales_details.prod_id = dbo.pos_products.product_id INNER JOIN
dbo.pos_stock_details ON dbo.pos_products.product_id = dbo.pos_stock_details.prod_id





GO
/****** Object:  View [dbo].[ViewShelfGouping]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[ViewShelfGouping]
as

SELECT       pos_products.product_id as [PID], pos_stock_details.stock_id as [SID], dbo.pos_products.prod_name AS [Product Name], dbo.pos_products.barcode as [Barcode], dbo.pos_shelfItems.title AS [Shelf Title], dbo.pos_category.title AS Category, dbo.pos_brand.brand_title AS Brand, 
dbo.pos_subcategory.title AS [Sub Category], dbo.pos_products.unit as [Unit], dbo.pos_products.status as [Status]
FROM            dbo.pos_stock_details INNER JOIN
dbo.pos_products ON dbo.pos_stock_details.prod_id = dbo.pos_products.product_id INNER JOIN
dbo.pos_subcategory ON dbo.pos_products.sub_cate_id = dbo.pos_subcategory.sub_cate_id INNER JOIN
dbo.pos_category ON dbo.pos_products.category_id = dbo.pos_category.category_id INNER JOIN
dbo.pos_brand ON dbo.pos_products.brand_id = dbo.pos_brand.brand_id INNER JOIN
dbo.pos_shelf_grouping ON dbo.pos_shelf_grouping.prod_id = dbo.pos_products.product_id INNER JOIN
dbo.pos_shelfItems ON dbo.pos_shelf_grouping.shelf_id = dbo.pos_shelfItems.shelf_id



GO
/****** Object:  View [dbo].[ViewStockDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  view [dbo].[ViewStockDetails]
as


SELECT        dbo.pos_products.prod_name AS [Product Name], dbo.pos_products.barcode, dbo.pos_category.title AS Category, dbo.pos_brand.brand_title AS Brand, dbo.pos_subcategory.title AS [Sub Category], 
dbo.pos_products.expiry_date AS [Expiry Date], dbo.pos_stock_details.quantity AS Qty, dbo.pos_stock_details.pkg, dbo.pos_stock_details.tab_pieces AS [Tab PCS], dbo.pos_stock_details.pur_price AS [Pur Price], 
dbo.pos_stock_details.sale_price AS [Sale Price], dbo.pos_products.size AS [Whole Sale], dbo.pos_stock_details.market_value AS [Tax %], dbo.pos_stock_details.discount_limit AS [Dis Limit],  CASE WHEN pur_price > 0 THEN (round((((sale_price - pur_price) / pur_price) * 100), 2)) ELSE '100' END AS [Profit (%)], dbo.pos_products.status as [Status]
FROM            dbo.pos_stock_details INNER JOIN
dbo.pos_products ON dbo.pos_stock_details.prod_id = dbo.pos_products.product_id INNER JOIN
dbo.pos_subcategory ON dbo.pos_products.sub_cate_id = dbo.pos_subcategory.sub_cate_id INNER JOIN
dbo.pos_category ON dbo.pos_products.category_id = dbo.pos_category.category_id INNER JOIN
dbo.pos_brand ON dbo.pos_products.brand_id = dbo.pos_brand.brand_id INNER JOIN
dbo.pos_color ON dbo.pos_products.color_id = dbo.pos_color.color_id






GO
/****** Object:  View [dbo].[ViewStoreEndDay]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE view [dbo].[ViewStoreEndDay]

as


SELECT  pos_store_day_end.id as [ID], pos_store_day_end.date as [Date], from_user_employee.full_name as [User], pos_store_day_end.cash_amount_received as [Cash Received],
pos_store_day_end.shortage_amount as [Shortage Amount],
pos_store_day_end.opening_cash as [Opening Balance], pos_store_day_end.total_sales as [Cash Amount], pos_store_day_end.credit_card_amount as [Credit Card],
pos_store_day_end.paypal_amount as [Apple Pay], pos_store_day_end.google_pay_amount as [Zelle / Cashapp / Venmo], pos_store_day_end.misc_items_amount as [Misc Sales],
pos_store_day_end.total_return_amount as [Total Returns], pos_store_day_end.total_discount as [Discounts], pos_store_day_end.total_taxation as [Total Tax], 
pos_store_day_end.total_void_orders as [Void Orders], pos_store_day_end.no_sales as [No Sale], pos_store_day_end.payout as [Payout], pos_store_day_end.expected_amount as [Expected Amount],
pos_store_day_end.total_tickets as [Total Tickets],  pos_store_day_end.balance as [Balance]
FROM pos_store_day_end INNER JOIN 
pos_users ON pos_store_day_end.user_id = pos_users.user_id
INNER JOIN pos_employees as to_user_employee ON to_user_employee.employee_id = pos_users.emp_id
INNER JOIN pos_employees as from_user_employee ON from_user_employee.employee_id = pos_users.emp_id


GO
/****** Object:  View [dbo].[ViewSupplierDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE View [dbo].[ViewSupplierDetails]
as

SELECT        dbo.pos_suppliers.date as date, dbo.pos_suppliers.full_name AS [Supplier Name], dbo.pos_suppliers.code, dbo.pos_country.title AS Country, dbo.pos_city.title AS City, dbo.pos_suppliers.mobile_no AS [Mobile No], 
                         dbo.pos_suppliers.telephone_no AS [Telephone No], dbo.pos_suppliers.contact_person AS [Contact Person], dbo.pos_suppliers.address, dbo.pos_suppliers.bank_name AS Bank, dbo.pos_suppliers.bank_account AS [Account #],
                          dbo.pos_suppliers.status
FROM            dbo.pos_supplier_payables INNER JOIN
                         dbo.pos_suppliers ON dbo.pos_supplier_payables.supplier_id = dbo.pos_suppliers.supplier_id INNER JOIN
                         dbo.pos_country ON dbo.pos_suppliers.country_id = dbo.pos_country.country_id INNER JOIN
                         dbo.pos_city ON dbo.pos_suppliers.city_id = dbo.pos_city.city_id














GO
/****** Object:  View [dbo].[ViewSupplierPaybookDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[ViewSupplierPaybookDetails]
as

SELECT        dbo.pos_supplier_paybook.date AS [Date], dbo.pos_supplier_paybook.time as [Time], dbo.pos_employees.full_name AS Employee, dbo.pos_suppliers.full_name AS [Supplier], dbo.pos_suppliers.code as [Code], 
                         dbo.pos_supplier_paybook.payment as [Payment], dbo.pos_supplier_paybook.previous_payables AS [P.Payables], dbo.pos_supplier_paybook.balance as [Balance]
FROM            dbo.pos_employees INNER JOIN
                         dbo.pos_supplier_paybook ON dbo.pos_employees.employee_id = dbo.pos_supplier_paybook.employee_id INNER JOIN
                         dbo.pos_suppliers ON dbo.pos_supplier_paybook.supplier_id = dbo.pos_suppliers.supplier_id











GO
/****** Object:  View [dbo].[ViewSupplierPaymentDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE view [dbo].[ViewSupplierPaymentDetails]
as

SELECT dbo.pos_supplier_paybook.date AS [Date], dbo.pos_supplier_paybook.time as [Time], dbo.pos_employees.full_name AS [Employee], 
dbo.pos_suppliers.full_name as [Supplier], dbo.pos_suppliers.code as [Code],  dbo.pos_suppliers.mobile_no as [Mobile No], dbo.pos_supplier_paybook.payment as [Payment],
 dbo.pos_supplier_paybook.reference as [References], dbo.pos_supplier_paybook.remarks as [Description]
FROM            dbo.pos_supplier_paybook INNER JOIN
                         dbo.pos_suppliers ON dbo.pos_supplier_paybook.supplier_id = dbo.pos_suppliers.supplier_id INNER JOIN
                         dbo.pos_employees ON dbo.pos_supplier_paybook.employee_id = dbo.pos_employees.employee_id








GO
/****** Object:  View [dbo].[ViewTicketHistory]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[ViewTicketHistory]
as

SELECT        dbo.pos_tickets.sales_acc_id AS TID, dbo.pos_tickets.billNo AS [Receipt No], dbo.pos_tickets.date as [Date], dbo.pos_tickets.time as [Time], dbo.pos_customers.full_name AS Customer, dbo.pos_employees.full_name AS Employee, 
                         dbo.pos_tickets.no_of_items AS [T.Items], dbo.pos_tickets.total_qty AS [T.Qty], dbo.pos_tickets.sub_total AS [Sub Total], dbo.pos_tickets.discount as 'Discount', dbo.pos_tickets.amount_due AS [Amount Due], 
                         dbo.pos_tickets.advance_amount AS [Advance Amount], dbo.pos_tickets.balance as 'Balance', dbo.pos_tickets.status as 'Status'
FROM            dbo.pos_tickets INNER JOIN
                         dbo.pos_customers ON dbo.pos_tickets.customer_id = dbo.pos_customers.customer_id INNER JOIN
                         dbo.pos_employees ON dbo.pos_tickets.employee_id = dbo.pos_employees.employee_id


GO
/****** Object:  View [dbo].[ViewTopSellingProducts]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[ViewTopSellingProducts]
as

SELECT TOP 25 prod_id, SUM(Quantity) AS TotalQuantity, SUM(Total_price) as TotalPrice
FROM pos_sales_details
INNER JOIN pos_sales_accounts ON pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
INNER JOIN pos_products ON pos_sales_details.prod_id = pos_products.product_id
WHERE (date >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) - 1, 0)
AND date < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0)) and (pos_products.status = 'Enabled')
GROUP BY prod_id ORDER BY TotalQuantity DESC




GO
/****** Object:  View [dbo].[ViewUnholdItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE View [dbo].[ViewUnholdItems]
as

SELECT        dbo.pos_hold_items.billNo AS [Receipt No], dbo.pos_hold_items.date as date, dbo.pos_customers.full_name AS Customer, dbo.pos_employees.full_name AS Employee, dbo.pos_hold_items.no_of_items AS [T.Items], 
                         dbo.pos_hold_items.total_qty AS [T.Qty], dbo.pos_hold_items.sub_total AS [Sub Total], dbo.pos_hold_items.discount, dbo.pos_hold_items.amount_due AS [Amount Due], dbo.pos_hold_items.paid AS Cash, 
                         dbo.pos_hold_items.credits, dbo.pos_hold_items.pCredits AS [P.Credits], dbo.pos_hold_items.remarks
FROM            dbo.pos_hold_items INNER JOIN
                         dbo.pos_customers ON dbo.pos_hold_items.customer_id = dbo.pos_customers.customer_id INNER JOIN
                         dbo.pos_employees ON dbo.pos_hold_items.employee_id = dbo.pos_employees.employee_id














GO
/****** Object:  View [dbo].[ViewWholeInventory]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE View [dbo].[ViewWholeInventory]
as


SELECT        dbo.pos_products.prod_name AS [Product Name], dbo.pos_products.barcode, dbo.pos_category.title AS Category, dbo.pos_brand.brand_title AS Brand, dbo.pos_subcategory.title AS [Sub Category], 
                        dbo.pos_products.expiry_date AS [Expired Date], dbo.pos_products.prod_state AS [P.State], dbo.pos_products.unit, dbo.pos_stock_details.quantity AS Qty, dbo.pos_stock_details.pkg, 
                         dbo.pos_stock_details.full_pak AS [Full PKG], dbo.pos_stock_details.pur_price AS [Pur Price], dbo.pos_stock_details.sale_price AS [Sale Price], dbo.pos_stock_details.market_value AS [M.Value], 
                         dbo.pos_stock_details.qty_alert AS [Alert Qty]
FROM            dbo.pos_stock_details INNER JOIN
                         dbo.pos_products ON dbo.pos_stock_details.prod_id = dbo.pos_products.product_id INNER JOIN
                         dbo.pos_subcategory ON dbo.pos_products.sub_cate_id = dbo.pos_subcategory.sub_cate_id INNER JOIN
                         dbo.pos_category ON dbo.pos_products.category_id = dbo.pos_category.category_id INNER JOIN
                         dbo.pos_brand ON dbo.pos_products.brand_id = dbo.pos_brand.brand_id INNER JOIN
                         dbo.pos_color ON dbo.pos_products.color_id = dbo.pos_color.color_id














GO
/****** Object:  StoredProcedure [dbo].[DashboardProcedureGetCountForAvailableCheques]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[DashboardProcedureGetCountForAvailableCheques]
(
	@Date date,
	@Date2 date,
	@result float output
)
AS

SET @result = (select IsNull((select COUNT(cheque_id) from pos_customerChequeDetails where (date = @Date) or (bounceDate = @Date2)), 0))













GO
/****** Object:  StoredProcedure [dbo].[DashboardProcedureGetCountForDefaulters]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[DashboardProcedureGetCountForDefaulters]
(
	@Date date,
	@result float output
)
AS

SET @result = (select IsNull((select COUNT(installment_plan_id) from pos_installment_plan where (installmentDate = @Date) and (status != 'Completed')), 0))













GO
/****** Object:  StoredProcedure [dbo].[DashboardProcedureGetCountForExpiredInventory]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[DashboardProcedureGetCountForExpiredInventory]
(
	@Date date,
	@result float output
)
AS

SET @result = (select IsNull((select COUNT(product_id) from pos_products where expiry_date = @Date), 0))













GO
/****** Object:  StoredProcedure [dbo].[DashboardProcedureGetCountForLowInventory]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[DashboardProcedureGetCountForLowInventory]
(
	@result float output
)
AS

SET @result = (select IsNull((select COUNT(quantity) from pos_stock_details inner join pos_products on pos_stock_details.prod_id = pos_products.product_id and quantity <= qty_alert), 0))













GO
/****** Object:  StoredProcedure [dbo].[DashboardProcedureGetSumOfAssetsAmount]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[DashboardProcedureGetSumOfAssetsAmount]
(
	@result float output
)
AS

SET @result = (select IsNull((select round(sum(pur_price * quantity), 2) from pos_stock_details), 0))













GO
/****** Object:  StoredProcedure [dbo].[DashboardProcedureGetSumOfBankBalance]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[DashboardProcedureGetSumOfBankBalance]
(
	@result float output
)
AS

SET @result = (select IsNull((select round(sum(amount), 2) from pos_banking_details inner join pos_transaction_status on pos_banking_details.status_id = pos_transaction_status.status_id where status_title != 'Withdraw'), 0))















GO
/****** Object:  StoredProcedure [dbo].[DashboardProcedureGetSumOfDuesAmount]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[DashboardProcedureGetSumOfDuesAmount]
(
	@result float output
)
AS

SET @result = (select IsNull((select round(sum(dues), 2) from pos_installment_plan), 0))













GO
/****** Object:  StoredProcedure [dbo].[DashboardProcedureGetSumOfExpenses]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[DashboardProcedureGetSumOfExpenses]
(
	@result float output
)
AS

SET @result = (select IsNull((select round(sum(amount), 2) from pos_expense_items), 0))













GO
/****** Object:  StoredProcedure [dbo].[DashboardProcedureGetSumOfExpiredItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[DashboardProcedureGetSumOfExpiredItems]
(
	@result float output
)
AS

SET @result = (select IsNull((select sum(quantity) from pos_expired_items), 0))













GO
/****** Object:  StoredProcedure [dbo].[DashboardProcedureGetSumOfLowInventory]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[DashboardProcedureGetSumOfLowInventory]
(
	@result float output
)
AS

SET @result = (select IsNull((select sum(quantity) from pos_stock_details inner join pos_products on pos_stock_details.prod_id = pos_products.product_id and pos_stock_details.quantity <= pos_stock_details.qty_alert), 0))













GO
/****** Object:  StoredProcedure [dbo].[DashboardProcedureGetSumOfPayablesAmount]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[DashboardProcedureGetSumOfPayablesAmount]
(
	@result float output
)
AS

SET @result = (select IsNull((select round(sum(previous_payables), 2) from pos_supplier_payables), 0))













GO
/****** Object:  StoredProcedure [dbo].[DashboardProcedureGetSumOfPurchases]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[DashboardProcedureGetSumOfPurchases]
(
	@result float output
)
AS

SET @result = (select IsNull((select round(sum(paid), 2) from pos_purchase), 0))













GO
/****** Object:  StoredProcedure [dbo].[DashboardProcedureGetSumOfReceivablesAmount]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[DashboardProcedureGetSumOfReceivablesAmount]
(
	@result float output
)
AS

SET @result = (select IsNull((select round(sum(lastCredits), 2) from pos_customer_lastCredits), 0))













GO
/****** Object:  StoredProcedure [dbo].[DashboardProcedureGetSumOfReturnItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[DashboardProcedureGetSumOfReturnItems]
(
	@result float output
)
AS

SET @result = (select IsNull((select sum(quantity) from pos_returns_details), 0))













GO
/****** Object:  StoredProcedure [dbo].[DashboardProcedureGetSumOfSoldItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[DashboardProcedureGetSumOfSoldItems]

(
	@result float output
)
AS

SET @result = (select IsNull((select sum(quantity) from pos_sales_details), 0))













GO
/****** Object:  StoredProcedure [dbo].[DashboardProcedureGetSumOfStock]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[DashboardProcedureGetSumOfStock] 

(
	@result float output
)
AS

SET @result = (select IsNull((select sum(quantity) from pos_stock_details inner join pos_products on pos_stock_details.prod_id = pos_products.product_id), 0))













GO
/****** Object:  StoredProcedure [dbo].[DashboardProcedureGetSumOfSupplierPayments]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[DashboardProcedureGetSumOfSupplierPayments]
(
	@result float output
)
AS

SET @result = (select IsNull((select round(sum(payment), 2) from pos_supplier_paybook), 0))








GO
/****** Object:  StoredProcedure [dbo].[DashboardProcedureGetSumWithdrawBankBalance]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[DashboardProcedureGetSumWithdrawBankBalance]
(
	@result float output
)
AS

SET @result = (select IsNull((select round(sum(amount), 2) from pos_banking_details inner join pos_transaction_status on pos_banking_details.status_id = pos_transaction_status.status_id where status_title = 'Withdraw'), 0))












GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxAccountTitle]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[fillComboBoxAccountTitle]
as

select account_title from pos_bank_account  where account_title != 'others'














GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxBankAccountNo]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[fillComboBoxBankAccountNo]
as

select account_no from pos_account_no

















GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxBankTitle]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[fillComboBoxBankTitle]
as

select bank_title from pos_bank














GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxBatchNumbers]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE procedure [dbo].[fillComboBoxBatchNumbers]
as

select title from pos_batchNo















GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxBranchkTitle]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[fillComboBoxBranchkTitle]
as

select branch_title from pos_bank_branch where branch_title != 'others'














GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxBrand]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[fillComboBoxBrand]
as

select brand_title from pos_brand where brand_title != 'others'














GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxCategory]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[fillComboBoxCategory]
as

select title from pos_category where title != 'others'














GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxCityNames]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[fillComboBoxCityNames]
as

select title from pos_city where title != 'nill'














GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxColors]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[fillComboBoxColors]
as

select title from pos_color where title != 'none'














GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxCounters]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[fillComboBoxCounters]
as

select title from pos_counter where title != 'nill'













GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxCounterSalesItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[fillComboBoxCounterSalesItems]
as

select prod_name, barcode from pos_products where pos_products.status = 'Enabled'














GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxCountryNames]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[fillComboBoxCountryNames]
as

select title from pos_country where title != 'nill'














GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxCustomerNames]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[fillComboBoxCustomerNames]
as

select full_name, cus_code from pos_customers where full_name != 'nill'














GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxEmployeeNames]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[fillComboBoxEmployeeNames]
as

select full_name, emp_code from pos_employees














GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxExpenseTitle]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[fillComboBoxExpenseTitle]
as

select title from pos_expenses













GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxInstallmentMonths]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[fillComboBoxInstallmentMonths]
as

select months, interest_rate from pos_installment_months














GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxInvestorsNames]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[fillComboBoxInvestorsNames]
as

select full_name, code from pos_investors where full_name != 'nill'














GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxInvoicesNumbers]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[fillComboBoxInvoicesNumbers]
as

select distinct(pos_sales_accounts.billNo) from pos_installment_plan inner join pos_installment_accounts on 
pos_installment_plan.installment_acc_id = pos_installment_accounts.installment_acc_id inner join 
pos_sales_accounts on pos_installment_accounts.sales_acc_id = pos_sales_accounts.sales_acc_id 
where (pos_installment_plan.status = 'Incomplete')















GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxLoanBankTitles]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[fillComboBoxLoanBankTitles]
as

select bank_name, code from pos_bankLoansDetails











GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxLoanHolders]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[fillComboBoxLoanHolders]
as

select full_name from pos_loanHolders where full_name != 'others'














GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxNamesLoanDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[fillComboBoxNamesLoanDetails]
as

select fullName from pos_loanDetails








GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxProductNames]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[fillComboBoxProductNames]
as

select DISTINCT prod_name from pos_products













GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxPromoGroups]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[fillComboBoxPromoGroups]
as

select title from pos_promo_groups














GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxRoles]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[fillComboBoxRoles]
as

select roleTitle from pos_roles













GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxShelfItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[fillComboBoxShelfItems]
as

select title from pos_shelfItems














GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxShifts]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[fillComboBoxShifts]
as

select title from pos_shift where title != 'nill'













GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxSubCategory]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[fillComboBoxSubCategory]
as

select title from pos_subcategory where title != 'others'














GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxSupplierNames]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[fillComboBoxSupplierNames]
as

select full_name, code from pos_suppliers














GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxTransactionStatus]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[fillComboBoxTransactionStatus]
as

select status_title from pos_transaction_status where status_title != 'others' 














GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxTransactionType]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[fillComboBoxTransactionType]
as

select transaction_type from pos_transaction_type where transaction_type != 'others'















GO
/****** Object:  StoredProcedure [dbo].[fillComboBoxUsers]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[fillComboBoxUsers]
as

select full_name from pos_employees where full_name != 'nill'













GO
/****** Object:  StoredProcedure [dbo].[ProcedureCustomerDuesDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Searching for a Product
CREATE procedure [dbo].[ProcedureCustomerDuesDetails]

as 

SELECT pos_customers.full_name as 'Customer Name', pos_customers.cus_code as 'Code', pos_customers.cnic as 'CNIC', pos_country.title as 'Country', pos_city.title as 'Province', pos_customers.mobile_no as 'Mobile No', pos_customers.telephone_no as 'Telephone No', format(pos_customer_lastCredits.due_days, 'dd/MMMM/yyyy') as 'Last Recovery', pos_customer_lastCredits.lastCredits as 'Credits', pos_customers.status as 'Status'
FROM pos_city INNER JOIN pos_customers ON pos_city.city_id = pos_customers.city_id INNER JOIN pos_country ON pos_customers.country_id = pos_country.country_id INNER JOIN pos_customer_lastCredits ON pos_customers.customer_id = pos_customer_lastCredits.customer_id
where pos_customer_lastCredits.lastCredits != '0' and pos_customer_lastCredits.lastCredits > '0'



















GO
/****** Object:  StoredProcedure [dbo].[ProcedureCustomerLastReceiptItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Enanbled Available Stock Details
CREATE procedure [dbo].[ProcedureCustomerLastReceiptItems]
(
	@search nvarchar(50)
 )
as 

select pos_products.prod_name as 'Items Name', pos_products.barcode as 'Barcode', pos_sales_details.quantity as 'Qty', pos_sales_details.pkg as 'PKG', pos_sales_details.full_pkg as 'Full PKG', pos_sales_details.total_marketPrice as 'Tax',
pos_sales_details.Total_price/pos_sales_details.quantity as 'Rate',   pos_sales_details.discount as 'Discount', pos_sales_details.Total_price as 'Amount', pos_sales_details.note as 'Note'
from pos_sales_details inner join pos_sales_accounts on pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
inner join pos_customers on pos_sales_accounts.customer_id = pos_customers.customer_id inner join pos_employees on pos_sales_accounts.employee_id = pos_employees.employee_id
inner join pos_products on pos_sales_details.prod_id = pos_products.product_id 
where pos_sales_accounts.billNo = (@search)


















GO
/****** Object:  StoredProcedure [dbo].[ProcedureCustomerLastReceiptReturnItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Enanbled Available Stock Details
CREATE procedure [dbo].[ProcedureCustomerLastReceiptReturnItems]
(
	@search nvarchar(50)
 )
as 

select pos_products.prod_name as 'Items Name', pos_products.barcode as 'Barcode', pos_returns_details.quantity as 'Qty', pos_returns_details.pkg as 'PKG', pos_returns_details.full_pkg as 'Full PKG', pos_returns_details.total_marketPrice as 'Tax',
pos_returns_details.Total_price/pos_returns_details.quantity as 'Rate',   pos_returns_details.discount as 'Discount', pos_returns_details.Total_price as 'Amount', pos_returns_details.note as 'Note'
from pos_returns_details inner join pos_return_accounts on pos_returns_details.return_acc_id = pos_return_accounts.return_acc_id
inner join pos_customers on pos_return_accounts.customer_id = pos_customers.customer_id inner join pos_employees on pos_return_accounts.employee_id = pos_employees.employee_id
inner join pos_products on pos_returns_details.prod_id = pos_products.product_id 
where pos_return_accounts.billNo = (@search)

















GO
/****** Object:  StoredProcedure [dbo].[ProcedureCustomersDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Show All Products Details
CREATE procedure [dbo].[ProcedureCustomersDetails]
as 

select * from ViewCustomersDetails


















GO
/****** Object:  StoredProcedure [dbo].[ProcedureDateWiseLoanDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ProcedureDateWiseLoanDetails]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50)
)
as

SELECT format(date, 'dd/MMMM/yyyy') AS [Date], time , fullName, fatherName, 
contactNo, amount, reference , remarks, status
FROM dbo.pos_loanDetails where date between @fromDate and @toDate 














GO
/****** Object:  StoredProcedure [dbo].[ProcedureDealItemsList]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ProcedureDealItemsList]
(
	@search nvarchar(50)
)

as

SELECT dbo.pos_products.prod_name as [Product Name], dbo.pos_stock_details.item_barcode as [Barcode], dbo.pos_deal_items.quantity as [Quantity], dbo.pos_deal_items.price as [Price]
FROM dbo.pos_deal_items INNER JOIN
dbo.pos_deals ON dbo.pos_deal_items.deal_id = dbo.pos_deals.deal_id INNER JOIN
dbo.pos_products ON dbo.pos_deal_items.prod_id = dbo.pos_products.product_id
INNER JOIN
dbo.pos_stock_details ON dbo.pos_deal_items.stock_id = dbo.pos_stock_details.stock_id
where (pos_deal_items.deal_id = @search) 






GO
/****** Object:  StoredProcedure [dbo].[ProcedureDemandItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[ProcedureDemandItems]
(
	@search nvarchar(50)
)
as

SELECT pos_products.prod_name as 'Items Name', pos_products.barcode as 'Barcode', pos_category.title as 'Category', pos_brand.brand_title as 'Brand', pos_demand_items.quantity as 'QTY', pos_demand_items.pkg as 'PKG', pos_demand_items.tab_pieces as 'Tab PCS', pos_demand_items.full_pak as 'Full PKG', pos_demand_items.pur_price as 'Pur Price', 
pos_demand_items.sale_price as 'Sale Price', pos_demand_items.total_pur_price as 'T. Pur Price', pos_demand_items.total_sale_price as 'T. Sale Price'
FROM pos_category INNER JOIN pos_brand INNER JOIN pos_demand_list INNER JOIN pos_demand_items ON pos_demand_list.demand_id = pos_demand_items.demand_id INNER JOIN
pos_employees ON pos_demand_list.employee_id = pos_employees.employee_id INNER JOIN pos_products ON pos_demand_items.prod_id = pos_products.product_id ON pos_brand.brand_id = pos_products.brand_id
ON pos_category.category_id = pos_products.category_id INNER JOIN pos_suppliers ON pos_demand_list.supplier_id = pos_suppliers.supplier_id
where pos_demand_list.bill_no = @search;
















GO
/****** Object:  StoredProcedure [dbo].[ProcedureEmployeeCommissionDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[ProcedureEmployeeCommissionDetails]
(
	@search nvarchar(50)
)

as

SELECT dbo.pos_products.prod_name as [Product Name], dbo.pos_stock_details.item_barcode as [Barcode], pos_category.title as [Category], pos_brand.brand_title as [Brand], pos_stock_details.sale_price as [Price]
FROM dbo.pos_employee_commission INNER JOIN dbo.pos_employee_commission_detail ON dbo.pos_employee_commission.commission_id = dbo.pos_employee_commission_detail.commission_id INNER JOIN
dbo.pos_products on pos_products.product_id = pos_employee_commission_detail.prod_id inner join pos_stock_details on pos_stock_details.stock_id = pos_employee_commission_detail.stock_id
inner join pos_category on pos_category.category_id = pos_products.category_id inner join pos_brand on pos_brand.brand_id = pos_products.brand_id
where (pos_employee_commission.commission_id = @search) 



GO
/****** Object:  StoredProcedure [dbo].[ProcedureGeneralSettings]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[ProcedureGeneralSettings]
(
    @value NVARCHAR(50),
    @result NVARCHAR(50) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @sql NVARCHAR(MAX);
    
    -- Construct the dynamic SQL query
    SET @sql = 'SELECT @result = ' + QUOTENAME(@value) + ' FROM pos_general_settings;';

    -- Execute the dynamic SQL
    EXEC sp_executesql @sql, N'@result NVARCHAR(50) OUTPUT', @result OUTPUT;
END;




GO
/****** Object:  StoredProcedure [dbo].[ProcedureGetButtonAuthorities1]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ProcedureGetButtonAuthorities1]
(
    @value NVARCHAR(50), -- Parameter to pass the column name
    @searchValue NVARCHAR(50),
    @result NVARCHAR(50) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @sql NVARCHAR(MAX);
    
    -- Construct the dynamic SQL query
    SET @sql = 'SELECT @result = ' + QUOTENAME(@value) + ' FROM pos_tbl_authorities_button_controls1 WHERE role_id = @searchValue;';

    -- Execute the dynamic SQL
    EXEC sp_executesql @sql, N'@searchValue NVARCHAR(50), @result NVARCHAR(50) OUTPUT', @searchValue, @result OUTPUT;
END;





GO
/****** Object:  StoredProcedure [dbo].[ProcedureGetDashboardAuthorities]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ProcedureGetDashboardAuthorities]
(
    @value NVARCHAR(50), -- Parameter to pass the column name
    @searchValue NVARCHAR(50),
    @result NVARCHAR(50) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @sql NVARCHAR(MAX);
    
    -- Construct the dynamic SQL query
    SET @sql = 'SELECT @result = ' + QUOTENAME(@value) + ' FROM pos_tbl_authorities_dashboard WHERE role_id = @searchValue;';

    -- Execute the dynamic SQL
    EXEC sp_executesql @sql, N'@searchValue NVARCHAR(50), @result NVARCHAR(50) OUTPUT', @searchValue, @result OUTPUT;
END;





GO
/****** Object:  StoredProcedure [dbo].[ProcedureGetIntergerValues]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ProcedureGetIntergerValues]
(   
    @columnName NVARCHAR(50),
    @tableName NVARCHAR(50),
    @condition NVARCHAR(50),
    @searchValue NVARCHAR(50),
    @result INT OUTPUT -- Change data type to INT
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @sql NVARCHAR(MAX);
    
    -- Construct the dynamic SQL query
    SET @sql = 'SELECT @result = ROUND(' + QUOTENAME(@columnName) + ', 0) FROM ' + QUOTENAME(@tableName) + ' WHERE ' + @condition + ' = @searchValue;'; -- Round to nearest integer

    BEGIN TRY
        -- Execute the dynamic SQL
        EXEC sp_executesql @sql, N'@searchValue NVARCHAR(50), @result INT OUTPUT', @searchValue, @result OUTPUT; -- Change data type to INT
    END TRY
    BEGIN CATCH
        -- Handle errors
        PRINT 'Error Message: ' + ERROR_MESSAGE();
        -- Optionally, raise the error for the calling code to handle
        -- THROW;
    END CATCH;
END;




GO
/****** Object:  StoredProcedure [dbo].[ProcedureGetNumericValues]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ProcedureGetNumericValues]
(   
    @columnName NVARCHAR(50),
    @tableName NVARCHAR(50),
    @condition NVARCHAR(50),
    @searchValue NVARCHAR(50),
    @result FLOAT OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @sql NVARCHAR(MAX);
    
    -- Construct the dynamic SQL query
    SET @sql = 'SELECT @result = round(' + QUOTENAME(@columnName) + ', 2) FROM ' + QUOTENAME(@tableName) + ' WHERE ' + @condition + ' = @searchValue;';

    BEGIN TRY
        -- Execute the dynamic SQL
        EXEC sp_executesql @sql, N'@searchValue NVARCHAR(50), @result FLOAT OUTPUT', @searchValue, @result OUTPUT;
    END TRY
    BEGIN CATCH
        -- Handle errors
        PRINT 'Error Message: ' + ERROR_MESSAGE();
        -- Optionally, raise the error for the calling code to handle
        -- THROW;
    END CATCH;
END;




GO
/****** Object:  StoredProcedure [dbo].[ProcedureGetReportsAuthorities]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[ProcedureGetReportsAuthorities]
(
    @value NVARCHAR(50), -- Parameter to pass the column name
    @searchValue NVARCHAR(50),
    @result NVARCHAR(50) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @sql NVARCHAR(MAX);
    
    -- Construct the dynamic SQL query
    SET @sql = 'SELECT @result = ' + QUOTENAME(@value) + ' FROM pos_tbl_authorities_reports WHERE role_id = @searchValue;';

    -- Execute the dynamic SQL
    EXEC sp_executesql @sql, N'@searchValue NVARCHAR(50), @result NVARCHAR(50) OUTPUT', @searchValue, @result OUTPUT;
END;




GO
/****** Object:  StoredProcedure [dbo].[ProcedureGetStringValues]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ProcedureGetStringValues]
(   
    @columnName NVARCHAR(50),
    @tableName NVARCHAR(50),
    @condition NVARCHAR(50),
    @searchValue NVARCHAR(50),
    @result NVARCHAR(50) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @sql NVARCHAR(MAX);
    
    -- Construct the dynamic SQL query
    SET @sql = 'SELECT @result = CONVERT(NVARCHAR(50), round(' + QUOTENAME(@columnName) + ', 2)) FROM ' + QUOTENAME(@tableName) + ' WHERE ' + @condition + ' = @searchValue;';

    BEGIN TRY
        -- Execute the dynamic SQL
        EXEC sp_executesql @sql, N'@searchValue NVARCHAR(50), @result NVARCHAR(50) OUTPUT', @searchValue, @result OUTPUT;
    END TRY
    BEGIN CATCH
        -- Handle errors
        PRINT 'Error Message: ' + ERROR_MESSAGE();
        -- Optionally, raise the error for the calling code to handle
        -- THROW;
    END CATCH;
END;








GO
/****** Object:  StoredProcedure [dbo].[procedureInstallmentPlanDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[procedureInstallmentPlanDetails]
(
	@search nvarchar(50)
)
as

SELECT dbo.pos_installment_plan.installmentNo as [Insatllment #], format(dbo.pos_installment_plan.installmentDate, 'dd/MMMM/yyyy') as [Installment Date], format(dbo.pos_installment_plan.dueDate, 'dd/MMMM/yyyy') as [Penalty Date], dbo.pos_installment_plan.amount as [Principle Amount], 
dbo.pos_installment_plan.interest as [Interest], dbo.pos_installment_plan.interest_percentage as [Interest %], dbo.pos_installment_plan.dues  as [Penalties],
dbo.pos_installment_plan.due_percentage  as [Penalty %], dbo.pos_installment_plan.total_amount as [Total Amount], dbo.pos_installment_plan.status as [Status]
FROM dbo.pos_installment_accounts INNER JOIN dbo.pos_installment_plan ON dbo.pos_installment_accounts.installment_acc_id = dbo.pos_installment_plan.installment_acc_id 
INNER JOIN dbo.pos_sales_accounts ON dbo.pos_installment_accounts.sales_acc_id = dbo.pos_sales_accounts.sales_acc_id 
where pos_sales_accounts.billNo = @search














GO
/****** Object:  StoredProcedure [dbo].[ProcedureOrderReturnedItemsList]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

						 
-- Enanbled Available Stock Details
CREATE procedure [dbo].[ProcedureOrderReturnedItemsList]
(
	@search nvarchar(50)
)
as 

select pos_products.prod_name as 'Items Name', pos_products.barcode as 'Barcode', pos_returns_details.quantity as 'Qty', pos_returns_details.pkg as 'PKG', pos_returns_details.full_pkg as 'Full PKG', pos_returns_details.total_marketPrice as 'Tax',
pos_returns_details.Total_price/pos_returns_details.quantity as 'Rate',  pos_returns_details.discount as 'Discount', pos_returns_details.Total_price as 'Amount', pos_returns_details.note as 'Note'
from pos_returns_details inner join pos_return_accounts on pos_returns_details.return_acc_id = pos_return_accounts.return_acc_id
inner join pos_customers on pos_return_accounts.customer_id = pos_customers.customer_id inner join pos_employees on pos_return_accounts.employee_id = pos_employees.employee_id
inner join pos_products on pos_returns_details.prod_id = pos_products.product_id 
where pos_return_accounts.billNo = @search;



















GO
/****** Object:  StoredProcedure [dbo].[ProcedureOrderSoldItemsList]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

						 
-- Enanbled Available Stock Details
CREATE procedure [dbo].[ProcedureOrderSoldItemsList]
(
	@search nvarchar(50)
 )
as 

select pos_products.prod_name as 'Items Name', pos_products.barcode as 'Barcode', pos_sales_details.quantity as 'Qty', pos_sales_details.pkg as 'PKG', pos_sales_details.full_pkg as 'Full PKG', pos_sales_details.total_marketPrice as 'Tax',
pos_sales_details.Total_price/pos_sales_details.quantity as 'Rate',   pos_sales_details.discount as 'Discount', pos_sales_details.Total_price as 'Amount', pos_sales_details.note as 'Note'
from pos_sales_details inner join pos_sales_accounts on pos_sales_details.sales_acc_id = pos_sales_accounts.sales_acc_id
inner join pos_customers on pos_sales_accounts.customer_id = pos_customers.customer_id inner join pos_employees on pos_sales_accounts.employee_id = pos_employees.employee_id
inner join pos_products on pos_sales_details.prod_id = pos_products.product_id 
where pos_sales_accounts.billNo = @search;



















GO
/****** Object:  StoredProcedure [dbo].[ProcedureOverallEmployeesDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Show All Products Details
CREATE procedure [dbo].[ProcedureOverallEmployeesDetails]
as 

select full_name as 'Employee Name', emp_code as 'Code', cnic as 'CNIC', pos_country.title as 'Country', pos_city.title as 'Province', mobile_no as 'Mobile No', telephone_no as 'Telephone No', address1 as 'Address', salary as 'Salary', status as 'Status', email as 'Email'
from pos_employees inner join pos_country on pos_employees.country_id = pos_country.country_id inner join pos_city on pos_employees.city_id = pos_city.city_id
where full_name != 'nill'
















GO
/****** Object:  StoredProcedure [dbo].[ProcedurePendingInstallmentDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[ProcedurePendingInstallmentDetails]
(
	@search nvarchar(50)
)
as

SELECT dbo.pos_installment_plan.installmentNo as [Insatllment #], format(dbo.pos_installment_plan.installmentDate, 'dd/MMMM/yyyy') as [Installment Date], format(dbo.pos_installment_plan.dueDate, 'dd/MMMM/yyyy') as [Penalty Date], dbo.pos_installment_plan.amount as [Principle Amount], 
dbo.pos_installment_plan.interest as [Interest], dbo.pos_installment_plan.interest_percentage as [Interest %], 
dbo.pos_installment_plan.dues as [Penalties], dbo.pos_installment_plan.total_amount as [Total Amount], dbo.pos_installment_plan.status as [Status]
FROM dbo.pos_installment_accounts INNER JOIN dbo.pos_installment_plan ON dbo.pos_installment_accounts.installment_acc_id = dbo.pos_installment_plan.installment_acc_id 
INNER JOIN dbo.pos_sales_accounts ON dbo.pos_installment_accounts.sales_acc_id = dbo.pos_sales_accounts.sales_acc_id INNER JOIN
dbo.pos_sales_details ON dbo.pos_sales_accounts.sales_acc_id = dbo.pos_sales_details.sales_acc_id
where (pos_sales_accounts.billNo = @search) and (pos_installment_plan.status = 'Incomplete')














GO
/****** Object:  StoredProcedure [dbo].[ProcedureProductSubItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ProcedureProductSubItems]
(
	@search nvarchar(50)
 )
as 


SELECT    dbo.pos_stock_details.stock_id AS [SID], dbo.pos_stock_details.item_barcode AS [Barcode], dbo.pos_stock_details.date_of_expiry AS [Expiry],  dbo.pos_stock_details.quantity AS [Quantity], 
dbo.pos_stock_details.qty_alert AS [Qty Alert], dbo.pos_stock_details.pur_price AS [Purchase], dbo.pos_stock_details.pkg AS [PKG],dbo.pos_stock_details.tab_pieces AS [Tab PCS], 
dbo.pos_stock_details.sale_price AS [Sale Price], dbo.pos_stock_details.whole_sale_price AS [Whole Sale], dbo.pos_stock_details.market_value AS [Tax %], dbo.pos_stock_details.discount_limit AS [Dis Limit], 
CASE WHEN pur_price > 0 THEN (round((((sale_price - pur_price) / pur_price) * 100), 2)) ELSE '100' END AS [Profit (%)]
FROM            dbo.pos_stock_details INNER JOIN
dbo.pos_products ON dbo.pos_stock_details.prod_id = dbo.pos_products.product_id 
where pos_stock_details.prod_id = @search;



GO
/****** Object:  StoredProcedure [dbo].[ProcedurePurchasedItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Enanbled Available Stock Details
CREATE procedure [dbo].[ProcedurePurchasedItems]
(
	@BillNo nvarchar(50),
	@InvoiceNo nvarchar(50)
 )

as 

select pos_products.prod_name as 'Item Name', pos_products.barcode as 'Barcode',  format(pos_products.expiry_date, 'dd/MMMM/yyyy') as 'Expiry Date', pos_purchased_items.pkg as 'PKG', pos_purchased_items.full_pak as 'Full PKG', pos_purchased_items.quantity as 'Qty',
pos_purchased_items.pur_price as 'Rate', pos_purchased_items.sale_price as 'Sale Price', pos_purchased_items.trade_off as 'T.Offer', pos_purchased_items.carry_exp as 'Shipping', pos_purchased_items.total_pur_price as 'Total Price'
from pos_purchased_items inner join pos_purchase on pos_purchased_items.purchase_id = pos_purchase.purchase_id inner join pos_products on pos_purchased_items.prod_id = pos_products.product_id
inner join pos_suppliers on pos_purchase.supplier_id = pos_suppliers.supplier_id inner join pos_employees on pos_purchase.employee_id = pos_employees.employee_id 
where pos_purchase.bill_no = @BillNo and pos_purchase.invoice_no = @InvoiceNo



















GO
/****** Object:  StoredProcedure [dbo].[ProcedurePurchaseIMEIDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ProcedurePurchaseIMEIDetails]
(
	@BillNo nvarchar(50),
	@InvoiceNo nvarchar(50)
)
as

select imei_no as 'IMEI #' from pos_purchase_imei inner join pos_products on pos_purchase_imei.prod_id = pos_products.product_id
where (invoiceNo = @BillNo) and (prod_name = @InvoiceNo)











GO
/****** Object:  StoredProcedure [dbo].[ProcedurePurchaseReturnDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Enanbled Available Stock Details
CREATE procedure [dbo].[ProcedurePurchaseReturnDetails]
as 

select format(pos_purchase_return.date, 'dd/MMMM/yyyy') as Date, pos_suppliers.full_name as 'Supplier', bill_no as 'Bill No', invoice_no as 'Invoice No', no_of_items as 'Total Items', 
total_quantity as 'Quantity', net_trade_off as 'Trade Off', net_carry_exp as 'Shipping', net_total as 'Sub Total', paid as 'Paid',
credits as 'Credits', freight as 'Freight', pos_suppliers.remarks as 'Note'
from pos_purchase_return inner join pos_suppliers on pos_purchase_return.supplier_id = pos_suppliers.supplier_id inner join pos_employees on pos_purchase_return.employee_id = pos_employees.employee_id


















GO
/****** Object:  StoredProcedure [dbo].[ProcedurePurchaseReturnItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- Enanbled Available Stock Details
CREATE procedure [dbo].[ProcedurePurchaseReturnItems]
(
	@BillNo nvarchar(50),
	@InvoiceNo nvarchar(50)
 )

as 

select pos_products.prod_name as 'Item Name', pos_products.barcode as 'Barcode', format(pos_products.expiry_date, 'dd/MMMM/yyyy') as 'Expiry Date', pos_pur_return_items.pkg as 'PKG', pos_pur_return_items.full_pak as 'Full PKG', pos_pur_return_items.quantity as 'Qty',
pos_pur_return_items.pur_price as 'Rate', pos_pur_return_items.sale_price as 'Sale Price', pos_pur_return_items.trade_off as 'T.Offer', pos_pur_return_items.carry_exp as 'Shipping', pos_pur_return_items.total_pur_price as 'Total Price'
from pos_pur_return_items inner join pos_purchase_return on pos_pur_return_items.purchase_id = pos_purchase_return.pur_return_id inner join pos_products on pos_pur_return_items.prod_id = pos_products.product_id
inner join pos_suppliers on pos_purchase_return.supplier_id = pos_suppliers.supplier_id inner join pos_employees on pos_purchase_return.employee_id = pos_employees.employee_id 
where pos_purchase_return.bill_no = @BillNo and pos_purchase_return.invoice_no = @InvoiceNo


















GO
/****** Object:  StoredProcedure [dbo].[ProcedureQueryResult]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProcedureQueryResult]
(
    @query NVARCHAR(150),
    @result NVARCHAR(50) OUTPUT
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @sql NVARCHAR(MAX);
    
    -- Construct the dynamic SQL query
    SET @sql = @query;

    -- Execute the dynamic SQL
    EXEC sp_executesql @sql, N'@result NVARCHAR(50) OUTPUT', @result OUTPUT;
END;



GO
/****** Object:  StoredProcedure [dbo].[ProcedureSearchingUpdateDemandItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ProcedureSearchingUpdateDemandItems]
(
	@BillNo nvarchar(50),
	@InvoiceNo nvarchar(50) 
)
as

SELECT top 20 pos_products.prod_name as 'Items Name', pos_products.barcode as 'Barcode', pos_category.title as 'Category', pos_demand_items.quantity as 'Quantity', pos_demand_items.pkg as 'PKG', pos_demand_items.tab_pieces as 'Tab_PCS', pos_demand_items.full_pak as 'Full PKG', pos_demand_items.pur_price as 'Pur Price', 
pos_demand_items.sale_price as 'Sale Price'
FROM pos_category INNER JOIN pos_brand INNER JOIN pos_demand_list INNER JOIN pos_demand_items ON pos_demand_list.demand_id = pos_demand_items.demand_id INNER JOIN
pos_employees ON pos_demand_list.employee_id = pos_employees.employee_id INNER JOIN pos_products ON pos_demand_items.prod_id = pos_products.product_id ON pos_brand.brand_id = pos_products.brand_id
ON pos_category.category_id = pos_products.category_id INNER JOIN pos_suppliers ON pos_demand_list.supplier_id = pos_suppliers.supplier_id
where (pos_products.prod_name like (@InvoiceNo + '%') or  pos_products.barcode like (@InvoiceNo + '%')) and (pos_demand_list.bill_no = @BillNo)

















GO
/****** Object:  StoredProcedure [dbo].[ProcedureSearchingUpdatePurchasingItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[ProcedureSearchingUpdatePurchasingItems]
(
	@BillNo nvarchar(50),
	@InvoiceNo nvarchar(50) 
)

as

select pos_products.prod_name as 'Item Name', pos_products.barcode as 'Barcode', pos_category.title as 'Category',  format(pos_products.expiry_date, 'dd/MMMM/yyyy') as 'Exp Date', pos_color.title as 'Color', pos_products.size as 'W. Sale', pos_purchased_items.quantity as 'Quantity',
pos_purchased_items.pkg as 'PKG', pos_stock_details.tab_pieces as 'Tab_PCS', pos_purchased_items.full_pak as 'Full PKG', pos_purchased_items.pur_price as 'Pur Price', pos_purchased_items.sale_price as 'Sale Price', pos_stock_details.market_value as 'M. Value',
pos_stock_details.discount as 'Discount', pos_stock_details.discount_limit as 'Dis Limit', pos_purchased_items.trade_off as 'Trade Off', pos_purchased_items.carry_exp as 'Shipping'
from pos_purchased_items inner join pos_purchase on pos_purchased_items.purchase_id = pos_purchase.purchase_id inner join pos_products on pos_purchased_items.prod_id = pos_products.product_id
inner join pos_suppliers on pos_purchase.supplier_id = pos_suppliers.supplier_id inner join pos_employees on pos_purchase.employee_id = pos_employees.employee_id inner join pos_category on pos_products.category_id = pos_category.category_id
inner join pos_color on pos_products.color_id = pos_color.color_id inner join pos_stock_details on pos_stock_details.prod_id = pos_products.product_id
where (pos_products.prod_name like (@InvoiceNo + '%') or  pos_products.barcode like (@InvoiceNo + '%')) and (pos_purchase.bill_no = @BillNo)

















GO
/****** Object:  StoredProcedure [dbo].[ProcedureStatusWiseLoanDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ProcedureStatusWiseLoanDetails]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50),
	@status nvarchar(50)
)
as

SELECT format(date, 'dd/MMMM/yyyy') AS [Date], time , fullName, fatherName, 
contactNo, amount, reference , remarks, status
FROM dbo.pos_loanDetails where (date between @fromDate and @toDate) and status = @status














GO
/****** Object:  StoredProcedure [dbo].[procedureTesting]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE procedure [dbo].[procedureTesting] 

as

select prod_name, image_path from pos_stock_details inner join  pos_products on pos_stock_details.prod_id = pos_products.product_id 
where pos_products.status = 'Enabled'




















GO
/****** Object:  StoredProcedure [dbo].[ProcedureTicketItemsList]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[ProcedureTicketItemsList]
(
	@search nvarchar(50)
 )
as 

select pos_products.prod_name as 'Items Name', pos_products.barcode as 'Barcode', pos_ticket_details.quantity as 'Qty', pos_ticket_details.pkg as 'PKG',
pos_ticket_details.full_pkg as 'Full PKG', pos_ticket_details.Total_price/pos_ticket_details.quantity as 'Rate', pos_ticket_details.discount as 'Discount',
pos_ticket_details.Total_price as 'Amount', pos_ticket_details.note as 'Note'
from pos_ticket_details inner join pos_tickets on pos_ticket_details.sales_acc_id = pos_tickets.sales_acc_id
inner join pos_customers on pos_tickets.customer_id = pos_customers.customer_id inner join pos_employees on pos_tickets.employee_id = pos_employees.employee_id
inner join pos_products on pos_ticket_details.prod_id = pos_products.product_id 
where pos_tickets.billNo = @search

GO
/****** Object:  StoredProcedure [dbo].[ProcedureUnholdItemsList]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

						 
-- Enanbled Available Stock Details
CREATE procedure [dbo].[ProcedureUnholdItemsList]
(
	@search nvarchar(50)
 )
as 

select pos_products.prod_name as 'Items Name', pos_products.barcode as 'Barcode', pos_hold_items_details.quantity as 'Qty', pos_hold_items_details.pkg as 'PKG',
pos_hold_items_details.full_pkg as 'Full PKG', pos_hold_items_details.Total_price/pos_hold_items_details.quantity as 'Rate', pos_hold_items_details.discount as 'Discount',
pos_hold_items_details.Total_price as 'Amount', pos_hold_items_details.note as 'Note'
from pos_hold_items_details inner join pos_hold_items on pos_hold_items_details.sales_acc_id = pos_hold_items.sales_acc_id
inner join pos_customers on pos_hold_items.customer_id = pos_customers.customer_id inner join pos_employees on pos_hold_items.employee_id = pos_employees.employee_id
inner join pos_products on pos_hold_items_details.prod_id = pos_products.product_id 
where pos_hold_items.billNo = @search


















GO
/****** Object:  StoredProcedure [dbo].[ProcedureUpdateDealItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ProcedureUpdateDealItems]
(
	@search nvarchar(50)
)

as

SELECT dbo.pos_products.prod_name as [Product Name], dbo.pos_deal_items.quantity as [Quantity], dbo.pos_deal_items.price as [Price], pos_stock_details.prod_id as [PID], pos_stock_details.stock_id as [SID]
FROM dbo.pos_deal_items INNER JOIN
dbo.pos_deals ON dbo.pos_deal_items.deal_id = dbo.pos_deals.deal_id INNER JOIN
dbo.pos_products ON dbo.pos_deal_items.prod_id = dbo.pos_products.product_id
INNER JOIN
dbo.pos_stock_details ON dbo.pos_deal_items.stock_id = dbo.pos_stock_details.stock_id
where (pos_deal_items.deal_id = @search) 






GO
/****** Object:  StoredProcedure [dbo].[ProcedureUpdateDemandItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ProcedureUpdateDemandItems]
(
	@search nvarchar(50)
)
as

SELECT pos_products.prod_name as 'Items Name', pos_products.barcode as 'Barcode', pos_category.title as 'Category', pos_demand_items.quantity as 'Quantity', pos_demand_items.pkg as 'PKG', pos_demand_items.tab_pieces as 'Tab_PCS', pos_demand_items.full_pak as 'Full PKG', pos_demand_items.pur_price as 'Pur Price', 
pos_demand_items.sale_price as 'Sale Price'
FROM pos_category INNER JOIN pos_brand INNER JOIN pos_demand_list INNER JOIN pos_demand_items ON pos_demand_list.demand_id = pos_demand_items.demand_id INNER JOIN
pos_employees ON pos_demand_list.employee_id = pos_employees.employee_id INNER JOIN pos_products ON pos_demand_items.prod_id = pos_products.product_id ON pos_brand.brand_id = pos_products.brand_id
ON pos_category.category_id = pos_products.category_id INNER JOIN pos_suppliers ON pos_demand_list.supplier_id = pos_suppliers.supplier_id
where pos_demand_list.bill_no = @search;

















GO
/****** Object:  StoredProcedure [dbo].[ProcedureUpdatePurchasingItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ProcedureUpdatePurchasingItems]
(
	@search nvarchar(50) 
)

as

select pos_products.prod_name as 'Item Name', pos_stock_details.item_barcode as 'Barcode', pos_category.title as 'Category',  format(pos_stock_details.date_of_expiry, 'dd/MMMM/yyyy') as 'Exp Date', pos_color.title as 'Color', pos_stock_details.whole_sale_price as 'W. Sale', pos_purchased_items.quantity as 'Quantity',
pos_purchased_items.pkg as 'PKG', pos_stock_details.tab_pieces as 'Tab_PCS', pos_purchased_items.full_pak as 'Full PKG', pos_purchased_items.pur_price as 'Cost Price', pos_purchased_items.sale_price as 'Sale Price', round(pos_stock_details.market_value, 3) as 'Tax %',
pos_stock_details.discount as 'Discount', pos_stock_details.discount_limit as 'Dis Limit', pos_purchased_items.trade_off as 'Trade Off', pos_purchased_items.carry_exp as 'Shipping',  pos_products.product_id as 'PID', ROUND((pos_purchased_items.new_purchase_price * pos_purchased_items.quantity), 3) as 'EXT Price', pos_purchased_items.new_purchase_price as 'New Cost'
from pos_purchased_items inner join pos_purchase on pos_purchased_items.purchase_id = pos_purchase.purchase_id inner join pos_products on pos_purchased_items.prod_id = pos_products.product_id
inner join pos_suppliers on pos_purchase.supplier_id = pos_suppliers.supplier_id inner join pos_employees on pos_purchase.employee_id = pos_employees.employee_id inner join pos_category on pos_products.category_id = pos_category.category_id
inner join pos_color on pos_products.color_id = pos_color.color_id inner join pos_stock_details on pos_stock_details.prod_id = pos_products.product_id
where (pos_purchase.bill_no = @search) order by pos_purchased_items.items_id

















GO
/****** Object:  StoredProcedure [dbo].[ProcedureUpdatePurchasingReturnItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ProcedureUpdatePurchasingReturnItems]
(
	@search nvarchar(50) 
)

as

select pos_products.prod_name as 'Item Name', pos_stock_details.item_barcode as 'Barcode', pos_category.title as 'Category',  format(pos_stock_details.date_of_expiry, 'dd/MMMM/yyyy') as 'Exp Date', pos_color.title as 'Color', pos_stock_details.whole_sale_price as 'W. Sale', pos_pur_return_items.quantity as 'Quantity',
pos_pur_return_items.pkg as 'PKG', pos_stock_details.tab_pieces as 'Tab_PCS', pos_pur_return_items.full_pak as 'Full PKG', ROUND(pos_pur_return_items.pur_price, 3) as 'Pur Price', ROUND(pos_pur_return_items.sale_price, 3) as 'Sale Price', pos_stock_details.market_value as 'Tax %',
pos_stock_details.discount as 'Discount', pos_stock_details.discount_limit as 'Dis Limit', pos_pur_return_items.trade_off as 'Trade Off', pos_pur_return_items.carry_exp as 'Shipping',  pos_products.product_id as 'PID', round((pos_pur_return_items.new_purchase_price * pos_pur_return_items.quantity), 3) as 'EXT Price', ROUND(pos_pur_return_items.new_purchase_price, 3) as 'New Cost'
from pos_pur_return_items inner join pos_purchase_return on pos_pur_return_items.purchase_id = pos_purchase_return.pur_return_id inner join pos_products on pos_pur_return_items.prod_id = pos_products.product_id
inner join pos_suppliers on pos_purchase_return.supplier_id = pos_suppliers.supplier_id inner join pos_employees on pos_purchase_return.employee_id = pos_employees.employee_id inner join pos_category on pos_products.category_id = pos_category.category_id
inner join pos_color on pos_products.color_id = pos_color.color_id inner join pos_stock_details on pos_stock_details.prod_id = pos_products.product_id
where (pos_purchase_return.bill_no = @search) order by pos_pur_return_items.items_id

















GO
/****** Object:  StoredProcedure [dbo].[ProcedureUserPermissions1]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Select items for Permissions
CREATE procedure [dbo].[ProcedureUserPermissions1]
(
	@SearchItem nvarchar(50),
	--@TableName nvarchar(100),
	@role_id int
 )
as 
--pos_tbl_authorities_button_controls2
	select @SearchItem from pos_tbl_authorities_button_controls1 where role_id = '@role_id';


















GO
/****** Object:  StoredProcedure [dbo].[ProcedureUserPermissions2]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Select items for Permissions
CREATE procedure [dbo].[ProcedureUserPermissions2]
(
	@SearchItem nvarchar(50),
	--@TableName nvarchar(100),
	@role_id int
 )
as 
--pos_tbl_authorities_button_controls2
	select @SearchItem from pos_tbl_authorities_button_controls2 where role_id = @role_id;


















GO
/****** Object:  StoredProcedure [dbo].[ProcedureUserPermissionsDashboard]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Select items for Permissions
CREATE procedure [dbo].[ProcedureUserPermissionsDashboard]
(
	@SearchItem nvarchar(50),
	--@TableName nvarchar(100),
	@role_id int
 )
as 
--pos_tbl_authorities_button_controls2
	select @SearchItem from pos_tbl_authorities_dashboard where role_id = '@role_id';


















GO
/****** Object:  StoredProcedure [dbo].[ProcedureUserPermissionsReports]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Select items for Permissions
CREATE procedure [dbo].[ProcedureUserPermissionsReports]
(
	@SearchItem nvarchar(50),
	--@TableName nvarchar(100),
	@role_id int
 )
as 
--pos_tbl_authorities_button_controls2
	select @SearchItem from pos_tbl_authorities_reports where role_id = '@role_id';


















GO
/****** Object:  StoredProcedure [dbo].[ProcedureViewBankDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[ProcedureViewBankDetails]
as

select * from ViewBankDetails

















GO
/****** Object:  StoredProcedure [dbo].[ProcedureViewCounterSaleCategoryWiseItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[ProcedureViewCounterSaleCategoryWiseItems]
as

select * from ViewCounterSaleCategoryWiseItems

















GO
/****** Object:  StoredProcedure [dbo].[ProcedureViewCounterSaleRegularItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[ProcedureViewCounterSaleRegularItems]
as

select * from ViewCounterSaleRegularItems

















GO
/****** Object:  StoredProcedure [dbo].[ProcedureViewGroupedItems]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[ProcedureViewGroupedItems]
as 

select * from ViewGroupedItems

















GO
/****** Object:  StoredProcedure [dbo].[ReporProcedureBillWiseCounterReturns]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[ReporProcedureBillWiseCounterReturns]
(
	@BillNo nvarchar(50)
)

as

select * from ReportViewBillWiseCounterReturns
where billNo = @BillNo;
















GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureAgreementForm]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureAgreementForm]
(
	@billNo nvarchar(50)
)

as

SELECT pos_customers.full_name, pos_customers.fatherName, pos_customers.cus_code, pos_customers.cnic, pos_customers.mobile_no, pos_customers.address1, pos_customers.email, pos_customers.image_path, pos_installment_plan.installmentNo, 
pos_installment_plan.installmentDate, pos_installment_plan.dueDate, pos_installment_plan.amount, pos_installment_plan.interest, pos_installment_plan.interest_percentage, pos_installment_plan.dues, pos_installment_plan.total_amount, 
pos_installment_plan.status, pos_installment_accounts.total_months, pos_installment_accounts.t_amount, pos_installment_accounts.total_interest, pos_installment_accounts.grand_total, 
pos_installment_accounts.total_interest_percent, pos_installment_accounts.total_dues, pos_sales_accounts.paid
FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id INNER JOIN pos_installment_plan INNER JOIN
pos_installment_accounts ON pos_installment_plan.installment_acc_id = pos_installment_accounts.installment_acc_id ON pos_sales_accounts.sales_acc_id = pos_installment_accounts.sales_acc_id 
INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id
where (pos_sales_accounts.billNo = @billNo) and (pos_installment_plan.status = 'Incomplete')















GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureAgreementFormChequeDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[ReportProcedureAgreementFormChequeDetails]
(
	@billNo nvarchar(50)
)

as

SELECT pos_customerChequeDetails.billNo, pos_customerChequeDetails.date, pos_customerChequeDetails.bounceDate, pos_customerChequeDetails.accountNo, pos_customerChequeDetails.chequeNo, 
pos_customerChequeDetails.amount, pos_customerChequeDetails.remarks, pos_customerChequeDetails.status, pos_bank.bank_title
FROM pos_customerChequeDetails INNER JOIN pos_bank ON pos_customerChequeDetails.bank_id = pos_bank.bank_id
where (pos_customerChequeDetails.billNo = @billNo)



















GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureAgreementFormContractPolicies]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[ReportProcedureAgreementFormContractPolicies]
as

select condition1, condition2, condition3, condition4, condition5, condition6, condition7,
condition8, condition9, condition10, condition11, condition12, condition13, condition14
from pos_contractPolicies












GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureAgreementFormGurantors]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[ReportProcedureAgreementFormGurantors]
(
	@billNo nvarchar(50)
)
as

SELECT pos_granters.full_name as granterName, pos_granters.fatherName as granterFather, pos_granters.code as granterCode, pos_granters.cnic as granterCNIC, 
pos_granters.mobile_no as granterMobile, pos_granters.address1 as granterAddress, pos_granters.email as granterEmail, pos_granters.telephone_no as granterTelephoneNo, pos_payment_grantors.billNo as grantorBillNo
FROM  pos_payment_grantors INNER JOIN pos_granters ON pos_payment_grantors.granter_id = pos_granters.granter_id
where (pos_payment_grantors.billNo = @billNo)















GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureBankWiseChequeDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[ReportProcedureBankWiseChequeDetails]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50),
	@bankTitle nvarchar(50)

)

as

SELECT pos_bank.bank_title, pos_customers.full_name, pos_customers.fatherName, pos_customers.cus_code, pos_customers.cnic, pos_customers.mobile_no, pos_customers.address1, pos_customers.email, 
pos_customerChequeDetails.billNo, pos_customerChequeDetails.date, pos_customerChequeDetails.bounceDate, pos_customerChequeDetails.accountNo, pos_customerChequeDetails.remarks, 
pos_customerChequeDetails.amount, pos_customerChequeDetails.status, pos_customerChequeDetails.chequeNo
FROM pos_bank INNER JOIN pos_customerChequeDetails ON pos_bank.bank_id = pos_customerChequeDetails.bank_id INNER JOIN
pos_customers ON pos_customerChequeDetails.customer_id = pos_customers.customer_id
where (pos_customerChequeDetails.date between @fromDate and @toDate) and (pos_bank.bank_title = @bankTitle)














GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureBankWiseLoanDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureBankWiseLoanDetails]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50),
	@bankName nvarchar(50),
	@BankCode nvarchar(25)
)

as

SELECT dbo.pos_bankLoansDetails.date, dbo.pos_bankLoansDetails.time, dbo.pos_bankLoansDetails.bank_name , dbo.pos_bankLoansDetails.code, 
dbo.pos_bank_branch.branch_title, dbo.pos_transaction_type.transaction_type, dbo.pos_bankLoansDetails.principle, dbo.pos_bankLoansDetails.interest, 
dbo.pos_bankLoansDetails.totalAmount, dbo.pos_bankLoanPayables.last_balance, dbo.pos_bankLoansDetails.remarks, dbo.pos_bankLoansDetails.status
FROM dbo.pos_bank_branch INNER JOIN dbo.pos_bankLoansDetails ON dbo.pos_bank_branch.branch_id = dbo.pos_bankLoansDetails.branch_id INNER JOIN
dbo.pos_bankLoanPayables ON dbo.pos_bankLoansDetails.BankLoan_id = dbo.pos_bankLoanPayables.BankLoan_id INNER JOIN dbo.pos_transaction_type ON dbo.pos_bankLoansDetails.t_type_id = dbo.pos_transaction_type.transaction_id
where (dbo.pos_bankLoansDetails.date between @fromDate and @toDate) and (dbo.pos_bankLoansDetails.bank_name = @bankName) and (dbo.pos_bankLoansDetails.code = @bankCode) order by dbo.pos_bankLoansDetails.date asc











GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureBankWiseLoanPaybookDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureBankWiseLoanPaybookDetails]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50),
	@bankName nvarchar(50),
	@BankCode nvarchar(25)
)

as

SELECT dbo.pos_bankLoanPaybook.date, dbo.pos_bankLoanPaybook.time, dbo.pos_bankLoanPaybook.paymentDate, dbo.pos_bankLoansDetails.bank_name, dbo.pos_bankLoansDetails.code, dbo.pos_transaction_status.status_title, 
dbo.pos_employees.full_name, dbo.pos_employees.emp_code, dbo.pos_bankLoanPayables.last_balance, dbo.pos_bankLoanPayables.last_payment, dbo.pos_bankLoanPaybook.reference, 
dbo.pos_bankLoanPaybook.remarks, dbo.pos_bankLoanPaybook.amount, dbo.pos_bankLoanPaybook.previous_payables, dbo.pos_bankLoanPaybook.balance
FROM dbo.pos_bankLoanPaybook INNER JOIN
dbo.pos_bankLoansDetails ON dbo.pos_bankLoanPaybook.BankLoan_id = dbo.pos_bankLoansDetails.BankLoan_id INNER JOIN
dbo.pos_bankLoanPayables ON dbo.pos_bankLoansDetails.BankLoan_id = dbo.pos_bankLoanPayables.BankLoan_id INNER JOIN
dbo.pos_transaction_status ON dbo.pos_bankLoanPaybook.status_id = dbo.pos_transaction_status.status_id INNER JOIN
dbo.pos_employees ON dbo.pos_bankLoanPaybook.employee_id = dbo.pos_employees.employee_id
where (dbo.pos_bankLoanPaybook.paymentDate between @fromDate and @toDate) and (dbo.pos_bankLoansDetails.bank_name = @bankName) and (dbo.pos_bankLoansDetails.code = @BankCode) order by dbo.pos_bankLoanPaybook.paymentDate asc











GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureBatchWiseCustomerDues]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureBatchWiseCustomerDues]
(
	@batchNo nvarchar(50)
)
as

SELECT pos_customers.full_name, pos_customers.fatherName, pos_customers.cus_code, pos_customers.cnic, pos_customers.mobile_no, pos_customers.address1,
pos_customers.customer_id, pos_customer_lastCredits.lastCredits, pos_customer_lastCredits.due_days, pos_batchNo.title as batchNo, pos_country.title AS country, pos_city.title AS city
FROM pos_batchNo INNER JOIN pos_customers ON pos_batchNo.batch_id = pos_customers.batch_id INNER JOIN pos_customer_lastCredits ON pos_customers.customer_id = pos_customer_lastCredits.customer_id
INNER JOIN pos_country ON pos_customers.country_id = pos_country.country_id INNER JOIN pos_city ON pos_customers.city_id = pos_city.city_id
where (pos_batchNo.title = @batchNo) and (pos_customer_lastCredits.lastCredits > 0)














GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureBatchWiseInvoices]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureBatchWiseInvoices]
(
	@batchNo nvarchar(50),
	@toDate nvarchar(50),
	@fromDate nvarchar(50)
)

as
SELECT pos_batchNo.title, pos_country.title AS country, pos_customers.full_name, pos_customers.fatherName, pos_customers.cus_code, pos_installment_accounts.total_months, pos_installment_accounts.t_amount, pos_installment_accounts.total_interest, 
pos_installment_accounts.grand_total, pos_installment_accounts.total_interest_percent, pos_installment_accounts.total_dues, pos_city.title AS province, pos_installment_plan.installmentNo, 
pos_installment_plan.installmentDate, pos_installment_plan.dueDate, pos_installment_plan.interest, pos_installment_plan.interest_percentage, pos_installment_plan.amount, pos_installment_plan.dues, 
pos_installment_plan.status, pos_installment_plan.total_amount, pos_customer_lastCredits.lastCredits, pos_customers.cnic, pos_customers.mobile_no, pos_customers.address1, pos_customers.opening_balance, 
pos_customers.email, pos_customers.house_no, pos_sales_accounts.billNo, pos_sales_accounts.no_of_items, pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, pos_sales_accounts.discount, 
pos_sales_accounts.tax, pos_sales_accounts.amount_due, pos_sales_accounts.credits, pos_sales_accounts.paid, pos_sales_accounts.pCredits, pos_sales_accounts.remarks
FROM pos_installment_plan INNER JOIN pos_installment_accounts ON pos_installment_plan.installment_acc_id = pos_installment_accounts.installment_acc_id INNER JOIN
pos_sales_accounts ON pos_installment_accounts.sales_acc_id = pos_sales_accounts.sales_acc_id INNER JOIN pos_batchNo INNER JOIN
pos_customers ON pos_batchNo.batch_id = pos_customers.batch_id INNER JOIN pos_country ON pos_customers.country_id = pos_country.country_id INNER JOIN
pos_city ON pos_customers.city_id = pos_city.city_id ON pos_sales_accounts.customer_id = pos_customers.customer_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
pos_customer_lastCredits ON pos_customers.customer_id = pos_customer_lastCredits.customer_id
where (pos_batchNo.title = @batchNo) and (pos_installment_plan.installmentDate between @toDate and @fromDate) order by pos_sales_accounts.billNo asc














GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureBillWiseChequeDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[ReportProcedureBillWiseChequeDetails]
(
	@billNo nvarchar(50)

)

as

SELECT pos_bank.bank_title, pos_customers.full_name, pos_customers.fatherName, pos_customers.cus_code, pos_customers.cnic, pos_customers.mobile_no, pos_customers.address1, pos_customers.email, 
pos_customerChequeDetails.billNo, pos_customerChequeDetails.date, pos_customerChequeDetails.bounceDate, pos_customerChequeDetails.accountNo, pos_customerChequeDetails.remarks, 
pos_customerChequeDetails.amount, pos_customerChequeDetails.status, pos_customerChequeDetails.chequeNo
FROM pos_bank INNER JOIN pos_customerChequeDetails ON pos_bank.bank_id = pos_customerChequeDetails.bank_id INNER JOIN
pos_customers ON pos_customerChequeDetails.customer_id = pos_customers.customer_id
where (pos_customerChequeDetails.billNo = @billNo)














GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureBillWiseSales]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[ReportProcedureBillWiseSales] 
(
	@BillNo nvarchar(50)
)

as

SELECT * from ReportViewBillWiseSales 
WHERE billNo = @BillNo;

















GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureBillWiseTotalMarketPrice]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[ReportProcedureBillWiseTotalMarketPrice] 
(
	@BillNo nvarchar(50)
)

as

SELECT sum(pos_sales_details.quantity *  pos_stock_details.market_value) as 'totalMarketPrice' FROM pos_customers INNER JOIN pos_sales_accounts ON pos_customers.customer_id = pos_sales_accounts.customer_id INNER JOIN
pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
pos_products ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id
WHERE pos_sales_accounts.billNo = @BillNo;

















GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureBouncedChequeNotifications]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[ReportProcedureBouncedChequeNotifications]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50)
)

as

SELECT pos_bank.bank_title, pos_customers.full_name, pos_customers.fatherName, pos_customers.cus_code, pos_customers.cnic, pos_customers.mobile_no, pos_customers.address1, pos_customers.email, 
pos_customerChequeDetails.billNo, pos_customerChequeDetails.date, pos_customerChequeDetails.bounceDate, pos_customerChequeDetails.accountNo, pos_customerChequeDetails.remarks, 
pos_customerChequeDetails.amount, pos_customerChequeDetails.status, pos_customerChequeDetails.chequeNo
FROM pos_bank INNER JOIN pos_customerChequeDetails ON pos_bank.bank_id = pos_customerChequeDetails.bank_id INNER JOIN
pos_customers ON pos_customerChequeDetails.customer_id = pos_customers.customer_id
where (pos_customerChequeDetails.bounceDate between @fromDate and @toDate) and (pos_customerChequeDetails.status != 'Complete')














GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureCharityDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureCharityDetails]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50)
)
as

SELECT formDate, toDate, paymentDate, time as [Time], fullName, fatherName, mobile_no, amount, reference as [References], 
note as [Note], lessAmount, netProfit as [Profit], balance as [Balance] FROM dbo.pos_charityDetails
where (paymentDate between @fromDate and @toDate) order by paymentDate












GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureChequeNotifications]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[ReportProcedureChequeNotifications]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50)
)

as

SELECT pos_bank.bank_title, pos_customers.full_name, pos_customers.fatherName, pos_customers.cus_code, pos_customers.cnic, pos_customers.mobile_no, pos_customers.address1, pos_customers.email, 
pos_customerChequeDetails.billNo, pos_customerChequeDetails.date, pos_customerChequeDetails.bounceDate, pos_customerChequeDetails.accountNo, pos_customerChequeDetails.remarks, 
pos_customerChequeDetails.amount, pos_customerChequeDetails.status, pos_customerChequeDetails.chequeNo
FROM pos_bank INNER JOIN pos_customerChequeDetails ON pos_bank.bank_id = pos_customerChequeDetails.bank_id INNER JOIN
pos_customers ON pos_customerChequeDetails.customer_id = pos_customers.customer_id
where (pos_customerChequeDetails.date between @fromDate and @toDate) and (pos_customerChequeDetails.status != 'Complete')














GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureCityWiseCustomerDues]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureCityWiseCustomerDues]
(
	@city nvarchar(50)
)
as

SELECT pos_customers.full_name, pos_customers.fatherName, pos_customers.cus_code, pos_customers.cnic, pos_customers.mobile_no, pos_customers.address1,
pos_customers.customer_id, pos_customer_lastCredits.lastCredits, pos_customer_lastCredits.due_days, pos_batchNo.title as batchNo, pos_country.title AS country, pos_city.title AS city
FROM pos_batchNo INNER JOIN pos_customers ON pos_batchNo.batch_id = pos_customers.batch_id INNER JOIN pos_customer_lastCredits ON pos_customers.customer_id = pos_customer_lastCredits.customer_id
INNER JOIN pos_country ON pos_customers.country_id = pos_country.country_id INNER JOIN pos_city ON pos_customers.city_id = pos_city.city_id
where (pos_city.title = @city) and (pos_customer_lastCredits.lastCredits > 0)














GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureCounterSalesLastCredits]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[ReportProcedureCounterSalesLastCredits]
(
	@Customer nvarchar(50),
	@Code nvarchar(50)
)

as

select lastCredits from pos_customer_lastCredits inner join pos_customers on pos_customer_lastCredits.customer_id = pos_customers.customer_id 
where (pos_customers.full_name = @Customer) and (pos_customers.cus_code = @Code);

















GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureCountryWiseCustomerDues]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureCountryWiseCustomerDues]
(
	@country nvarchar(50)
)
as

SELECT pos_customers.full_name, pos_customers.fatherName, pos_customers.cus_code, pos_customers.cnic, pos_customers.mobile_no, pos_customers.address1,
pos_customers.customer_id, pos_customer_lastCredits.lastCredits, pos_customer_lastCredits.due_days, pos_batchNo.title as batchNo, pos_country.title AS country, pos_city.title AS city
FROM pos_batchNo INNER JOIN pos_customers ON pos_batchNo.batch_id = pos_customers.batch_id INNER JOIN pos_customer_lastCredits ON pos_customers.customer_id = pos_customer_lastCredits.customer_id
INNER JOIN pos_country ON pos_customers.country_id = pos_country.country_id INNER JOIN pos_city ON pos_customers.city_id = pos_city.city_id
where (pos_country.title = @country) and (pos_customer_lastCredits.lastCredits > 0)














GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureCountryWiseInvestorsList]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[ReportProcedureCountryWiseInvestorsList]
(
	@country nvarchar(50)
)

as

SELECT pos_investors.date, pos_investors.full_name, pos_investors.code, pos_investors.post_code, pos_investors.zip_code, pos_investors.cnic, pos_investors.house_no,
pos_investors.telephone_no, pos_investors.mobile_no, pos_investors.address1, pos_investors.address2, pos_investors.email, pos_investors.share_percentage, 
pos_investors.profit_percentage, pos_investors.investment, pos_investors.remarks, pos_investors.status, pos_city.title, pos_country.title AS country
FROM pos_investors INNER JOIN pos_city ON pos_investors.city_id = pos_city.city_id INNER JOIN pos_country ON pos_investors.country_id = pos_country.country_id
where pos_country.title = @country














GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureCountryWiseInvoices]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureCountryWiseInvoices]
(
	@country nvarchar(50),
	@toDate nvarchar(50),
	@fromDate nvarchar(50)
)

as
SELECT pos_batchNo.title, pos_country.title AS country, pos_customers.full_name, pos_customers.fatherName, pos_customers.cus_code, pos_installment_accounts.total_months, pos_installment_accounts.t_amount, pos_installment_accounts.total_interest, 
pos_installment_accounts.grand_total, pos_installment_accounts.total_interest_percent, pos_installment_accounts.total_dues, pos_city.title AS province, pos_installment_plan.installmentNo, 
pos_installment_plan.installmentDate, pos_installment_plan.dueDate, pos_installment_plan.interest, pos_installment_plan.interest_percentage, pos_installment_plan.amount, pos_installment_plan.dues, 
pos_installment_plan.status, pos_installment_plan.total_amount, pos_customer_lastCredits.lastCredits, pos_customers.cnic, pos_customers.mobile_no, pos_customers.address1, pos_customers.opening_balance, 
pos_customers.email, pos_customers.house_no, pos_sales_accounts.billNo, pos_sales_accounts.no_of_items, pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, pos_sales_accounts.discount, 
pos_sales_accounts.tax, pos_sales_accounts.amount_due, pos_sales_accounts.credits, pos_sales_accounts.paid, pos_sales_accounts.pCredits, pos_sales_accounts.remarks
FROM pos_installment_plan INNER JOIN pos_installment_accounts ON pos_installment_plan.installment_acc_id = pos_installment_accounts.installment_acc_id INNER JOIN
pos_sales_accounts ON pos_installment_accounts.sales_acc_id = pos_sales_accounts.sales_acc_id INNER JOIN pos_batchNo INNER JOIN
pos_customers ON pos_batchNo.batch_id = pos_customers.batch_id INNER JOIN pos_country ON pos_customers.country_id = pos_country.country_id INNER JOIN
pos_city ON pos_customers.city_id = pos_city.city_id ON pos_sales_accounts.customer_id = pos_customers.customer_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
pos_customer_lastCredits ON pos_customers.customer_id = pos_customer_lastCredits.customer_id
where (pos_country.title = @country) and (pos_installment_plan.installmentDate between @toDate and @fromDate) order by pos_sales_accounts.billNo asc














GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureCustomerInstallmentStatement]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureCustomerInstallmentStatement]
(
	@billNo nvarchar(50)
)

as

SELECT pos_sales_accounts.billNo, pos_installment_accounts.total_months, pos_installment_accounts.t_amount, pos_installment_accounts.total_interest, pos_installment_accounts.grand_total, 
pos_installment_accounts.total_interest_percent, pos_installment_accounts.total_dues, pos_sales_accounts.paid, pos_sales_accounts.discount, pos_sales_accounts.sub_total, pos_sales_accounts.amount_due, pos_sales_accounts.credits, pos_installment_plan.installmentNo, 
pos_installment_plan.installmentDate, pos_installment_plan.dueDate, pos_installment_plan.amount, pos_installment_plan.interest, pos_installment_plan.interest_percentage, pos_installment_plan.due_percentage, 
pos_installment_plan.dues, pos_installment_plan.total_amount, pos_installment_plan.status, pos_installment_plan.nextDueDate, pos_customers.full_name, pos_customers.fatherName, pos_customers.cus_code, 
pos_customers.cnic, pos_customers.mobile_no, pos_customers.address1, pos_customers.email, pos_customers.credit_limit, pos_customers.opening_balance
FROM  pos_installment_accounts INNER JOIN pos_installment_plan ON pos_installment_accounts.installment_acc_id = pos_installment_plan.installment_acc_id INNER JOIN
pos_sales_accounts ON pos_installment_accounts.sales_acc_id = pos_sales_accounts.sales_acc_id INNER JOIN pos_customers ON pos_sales_accounts.customer_id = pos_customers.customer_id
where pos_sales_accounts.billNo = @billNo














GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureCustomerWiseChequeDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[ReportProcedureCustomerWiseChequeDetails]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50),
	@customerName nvarchar(50),
	@code nvarchar(50)

)

as

SELECT pos_bank.bank_title, pos_customers.full_name, pos_customers.fatherName, pos_customers.cus_code, pos_customers.cnic, pos_customers.mobile_no, pos_customers.address1, pos_customers.email, 
pos_customerChequeDetails.billNo, pos_customerChequeDetails.date, pos_customerChequeDetails.bounceDate, pos_customerChequeDetails.accountNo, pos_customerChequeDetails.remarks, 
pos_customerChequeDetails.amount, pos_customerChequeDetails.status, pos_customerChequeDetails.chequeNo
FROM pos_bank INNER JOIN pos_customerChequeDetails ON pos_bank.bank_id = pos_customerChequeDetails.bank_id INNER JOIN
pos_customers ON pos_customerChequeDetails.customer_id = pos_customers.customer_id
where (pos_customerChequeDetails.date between @fromDate and @toDate) and (pos_customers.full_name = @customerName) and (pos_customers.cus_code = @code)














GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureCustomerWiseInvoices]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[ReportProcedureCustomerWiseInvoices]
(
	@customer nvarchar(50),
	@code nvarchar(50),
	@toDate nvarchar(50),
	@fromDate nvarchar(50)
)

as
SELECT pos_batchNo.title, pos_country.title AS country, pos_customers.full_name, pos_customers.fatherName, pos_customers.cus_code, pos_installment_accounts.total_months, pos_installment_accounts.t_amount, pos_installment_accounts.total_interest, 
pos_installment_accounts.grand_total, pos_installment_accounts.total_interest_percent, pos_installment_accounts.total_dues, pos_city.title AS province, pos_installment_plan.installmentNo, 
pos_installment_plan.installmentDate, pos_installment_plan.dueDate, pos_installment_plan.interest, pos_installment_plan.interest_percentage, pos_installment_plan.amount, pos_installment_plan.dues, 
pos_installment_plan.status, pos_installment_plan.total_amount, pos_customer_lastCredits.lastCredits, pos_customers.cnic, pos_customers.mobile_no, pos_customers.address1, pos_customers.opening_balance, 
pos_customers.email, pos_customers.house_no, pos_sales_accounts.billNo, pos_sales_accounts.no_of_items, pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, pos_sales_accounts.discount, 
pos_sales_accounts.tax, pos_sales_accounts.amount_due, pos_sales_accounts.credits, pos_sales_accounts.paid, pos_sales_accounts.pCredits, pos_sales_accounts.remarks
FROM pos_installment_plan INNER JOIN pos_installment_accounts ON pos_installment_plan.installment_acc_id = pos_installment_accounts.installment_acc_id INNER JOIN
pos_sales_accounts ON pos_installment_accounts.sales_acc_id = pos_sales_accounts.sales_acc_id INNER JOIN pos_batchNo INNER JOIN
pos_customers ON pos_batchNo.batch_id = pos_customers.batch_id INNER JOIN pos_country ON pos_customers.country_id = pos_country.country_id INNER JOIN
pos_city ON pos_customers.city_id = pos_city.city_id ON pos_sales_accounts.customer_id = pos_customers.customer_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
pos_customer_lastCredits ON pos_customers.customer_id = pos_customer_lastCredits.customer_id
where (pos_customers.full_name = @customer) and (pos_customers.cus_code = @code) and (pos_installment_plan.installmentDate between @toDate and @fromDate) order by pos_sales_accounts.billNo asc
















GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureCustomerWiseRecoveries]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[ReportProcedureCustomerWiseRecoveries]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50),
	@customerName nvarchar(50),
	@code nvarchar(50)
)
as

SELECT pos_recovery_details.date, pos_recovery_details.time, pos_recovery_details.invoiceNo, pos_recovery_details.reference, pos_recovery_details.remarks, pos_recovery_details.employee_id, pos_recovery_details.customer_id, 
pos_recoveries.installmentDate, pos_recoveries.installmentNo, pos_recoveries.monthly_installment, pos_recoveries.monthly_interest, pos_recoveries.monthlyDues, pos_recoveries.total_amount, pos_recoveries.amount, 
pos_recoveries.credits, pos_customers.full_name, pos_customers.fatherName, pos_customers.cus_code, pos_customers.cnic, pos_customers.mobile_no, pos_customers.address1, pos_customer_lastCredits.lastCredits, 
pos_employees.full_name AS employee, pos_employees.emp_code
FROM pos_customer_lastCredits INNER JOIN pos_customers ON pos_customer_lastCredits.customer_id = pos_customers.customer_id INNER JOIN
pos_recovery_details ON pos_customers.customer_id = pos_recovery_details.customer_id INNER JOIN pos_employees ON
pos_recovery_details.employee_id = pos_employees.employee_id INNER JOIN pos_recoveries ON pos_recovery_details.recovery_id = pos_recoveries.recovery_id
WHERE (pos_recovery_details.date BETWEEN @fromDate and @toDate) and (pos_customers.full_name = @customerName) and (pos_customers.cus_code = @code) ORDER BY pos_recovery_details.date asc














GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureDateWiseCapitalHistoryDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[ReportProcedureDateWiseCapitalHistoryDetails]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50)
)
as

SELECT date as [Date], time as [Time], amount as [Amount], total_capital, total_investment, remarks as [Note], status as [Status]
FROM dbo.pos_capital_history
where date between @fromDate and @toDate order by date asc













GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureDateWiseIMEIDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureDateWiseIMEIDetails]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50)
)

as


SELECT pos_purchase.date, pos_purchase.bill_no, pos_purchase_imei.imei_no, pos_purchase_imei.isSold, pos_purchase_imei.invoiceNo, 
pos_suppliers.full_name, pos_suppliers.code, pos_products.prod_name, pos_products.barcode
FROM pos_products INNER JOIN pos_purchase_imei ON pos_products.product_id = pos_purchase_imei.prod_id INNER JOIN
pos_purchased_items ON pos_products.product_id = pos_purchased_items.prod_id INNER JOIN pos_purchase INNER JOIN
pos_suppliers ON pos_purchase.supplier_id = pos_suppliers.supplier_id ON pos_purchased_items.purchase_id = pos_purchase.purchase_id
where (pos_purchase.date between @fromDate and @toDate) order by pos_purchase.date asc













GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureDateWiseInvestorsPayments]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureDateWiseInvestorsPayments]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50)
)

as

SELECT pos_investorPaybook.date, pos_investorPaybook.time, pos_investorPaybook.fromDate, pos_investorPaybook.toDate, pos_investorPaybook.reference, pos_investorPaybook.remarks, pos_investorPaybook.investorShare, 
pos_investorPaybook.investment, pos_investorPaybook.netProfit, pos_investorPaybook.profit, pos_investorPaybook.lessAmount, pos_investorPaybook.salaries, pos_investorPaybook.payment, pos_investorPaybook.credits, pos_investorPaybook.balance, pos_investors.full_name, 
pos_investors.code, pos_investors.cnic, pos_investors.mobile_no, pos_investors.address1, pos_investors.email, pos_employees.full_name AS employeeName, pos_employees.emp_code, pos_country.title as country,  pos_city.title AS city
FROM pos_investorPaybook INNER JOIN pos_investors ON pos_investorPaybook.investor_id = pos_investors.investor_id INNER JOIN
pos_employees ON pos_investorPaybook.employee_id = pos_employees.employee_id INNER JOIN
pos_city ON pos_investors.city_id = pos_city.city_id AND pos_employees.city_id = pos_city.city_id INNER JOIN
pos_country ON pos_investors.country_id = pos_country.country_id AND pos_employees.country_id = pos_country.country_id
where (pos_investorPaybook.date between @fromDate and @toDate) order by pos_investorPaybook.date asc















GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureDateWiseLoanDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureDateWiseLoanDetails]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50)
)

as

SELECT dbo.pos_bankLoansDetails.date, dbo.pos_bankLoansDetails.time, dbo.pos_bankLoansDetails.bank_name , dbo.pos_bankLoansDetails.code, 
dbo.pos_bank_branch.branch_title, dbo.pos_transaction_type.transaction_type, dbo.pos_bankLoansDetails.principle, dbo.pos_bankLoansDetails.interest, 
dbo.pos_bankLoansDetails.totalAmount, dbo.pos_bankLoanPayables.last_balance, dbo.pos_bankLoansDetails.remarks, dbo.pos_bankLoansDetails.status
FROM dbo.pos_bank_branch INNER JOIN dbo.pos_bankLoansDetails ON dbo.pos_bank_branch.branch_id = dbo.pos_bankLoansDetails.branch_id INNER JOIN
dbo.pos_bankLoanPayables ON dbo.pos_bankLoansDetails.BankLoan_id = dbo.pos_bankLoanPayables.BankLoan_id INNER JOIN dbo.pos_transaction_type ON dbo.pos_bankLoansDetails.t_type_id = dbo.pos_transaction_type.transaction_id
where (dbo.pos_bankLoansDetails.date between @fromDate and @toDate) order by dbo.pos_bankLoansDetails.date asc











GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureDateWiseLoanPaybookDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureDateWiseLoanPaybookDetails]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50)
)

as

SELECT dbo.pos_bankLoanPaybook.date, dbo.pos_bankLoanPaybook.time, dbo.pos_bankLoanPaybook.paymentDate, dbo.pos_bankLoansDetails.bank_name, dbo.pos_bankLoansDetails.code, dbo.pos_transaction_status.status_title, 
dbo.pos_employees.full_name, dbo.pos_employees.emp_code, dbo.pos_bankLoanPayables.last_balance, dbo.pos_bankLoanPayables.last_payment, dbo.pos_bankLoanPaybook.reference, 
dbo.pos_bankLoanPaybook.remarks, dbo.pos_bankLoanPaybook.amount, dbo.pos_bankLoanPaybook.previous_payables, dbo.pos_bankLoanPaybook.balance
FROM dbo.pos_bankLoanPaybook INNER JOIN
dbo.pos_bankLoansDetails ON dbo.pos_bankLoanPaybook.BankLoan_id = dbo.pos_bankLoansDetails.BankLoan_id INNER JOIN
dbo.pos_bankLoanPayables ON dbo.pos_bankLoansDetails.BankLoan_id = dbo.pos_bankLoanPayables.BankLoan_id INNER JOIN
dbo.pos_transaction_status ON dbo.pos_bankLoanPaybook.status_id = dbo.pos_transaction_status.status_id INNER JOIN
dbo.pos_employees ON dbo.pos_bankLoanPaybook.employee_id = dbo.pos_employees.employee_id
where (dbo.pos_bankLoanPaybook.paymentDate between @fromDate and @toDate) order by dbo.pos_bankLoanPaybook.paymentDate asc











GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureDateWisePaymentDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[ReportProcedureDateWisePaymentDetails]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50)
)
as

SELECT pos_suppliers.full_name, pos_suppliers.code, pos_supplier_paybook.date, pos_supplier_paybook.time, pos_supplier_paybook.reference, pos_supplier_paybook.remarks, pos_supplier_paybook.payment, 
pos_supplier_paybook.previous_payables, pos_supplier_paybook.balance, pos_supplier_payables.previous_payables AS lastPayables
FROM pos_suppliers INNER JOIN pos_supplier_paybook ON pos_suppliers.supplier_id = pos_supplier_paybook.supplier_id INNER JOIN
pos_supplier_payables ON pos_suppliers.supplier_id = pos_supplier_payables.supplier_id
where (pos_supplier_paybook.date between @fromDate and @toDate) order by pos_supplier_paybook.date














GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureDateWiseRecoveries]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[ReportProcedureDateWiseRecoveries]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50)
)
as

SELECT pos_recovery_details.date, pos_recovery_details.time, pos_recovery_details.invoiceNo, pos_recovery_details.reference, pos_recovery_details.remarks, pos_recovery_details.employee_id, pos_recovery_details.customer_id, 
pos_recoveries.installmentDate, pos_recoveries.installmentNo, pos_recoveries.monthly_installment, pos_recoveries.monthly_interest, pos_recoveries.monthlyDues, pos_recoveries.total_amount, pos_recoveries.amount, 
pos_recoveries.credits, pos_customers.full_name, pos_customers.fatherName, pos_customers.cus_code, pos_customers.cnic, pos_customers.mobile_no, pos_customers.address1, pos_customer_lastCredits.lastCredits, 
pos_employees.full_name AS employee, pos_employees.emp_code
FROM pos_customer_lastCredits INNER JOIN pos_customers ON pos_customer_lastCredits.customer_id = pos_customers.customer_id INNER JOIN
pos_recovery_details ON pos_customers.customer_id = pos_recovery_details.customer_id INNER JOIN pos_employees ON
pos_recovery_details.employee_id = pos_employees.employee_id INNER JOIN pos_recoveries ON pos_recovery_details.recovery_id = pos_recoveries.recovery_id
where (pos_recovery_details.date between @fromDate and @toDate) order by pos_recovery_details.date asc














GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureDateWiseSalaryDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureDateWiseSalaryDetails]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(20)
)

as

SELECT dbo.pos_salariesPaybook.date, dbo.pos_salariesPaybook.time, dbo.pos_salariesPaybook.paymentDate, dbo.pos_salariesPaybook.amount, dbo.pos_salariesPaybook.credits, dbo.pos_salariesPaybook.balance, 
dbo.pos_salariesPaybook.reference, dbo.pos_salariesPaybook.remarks, dbo.pos_employees.full_name, dbo.pos_employees.emp_code, dbo.pos_employees.cnic, dbo.pos_employees.mobile_no, 
dbo.pos_employees.address1, dbo.pos_employees.email, dbo.pos_employees.salary, dbo.pos_employees.daily_wages, dbo.pos_employees.commission
FROM dbo.pos_salariesPaybook INNER JOIN
dbo.pos_employees ON dbo.pos_salariesPaybook.employee_id = dbo.pos_employees.employee_id
where (dbo.pos_salariesPaybook.paymentDate between @fromDate and @toDate)











GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureDateWiseSalesReturnSummary]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureDateWiseSalesReturnSummary]
(
	@fromDate nvarchar(50),
	@ToDate nvarchar(50)
)
as

SELECT pos_category.title, pos_brand.brand_title, pos_subcategory.title AS Expr1, pos_products.prod_name, pos_products.barcode, pos_products.expiry_date, pos_products.prod_state, pos_return_accounts.billNo, 
pos_return_accounts.date, pos_return_accounts.no_of_items, pos_return_accounts.total_qty, pos_return_accounts.sub_total, pos_return_accounts.discount, pos_return_accounts.tax, pos_return_accounts.amount_due, 
pos_return_accounts.paid, pos_return_accounts.credits, pos_return_accounts.pCredits, pos_return_accounts.status, pos_return_accounts.remarks, pos_returns_details.quantity, pos_returns_details.pkg, pos_returns_details.full_pkg, 
pos_returns_details.Total_price, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, pos_returns_details.total_purchase, pos_returns_details.note
FROM pos_brand INNER JOIN pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id INNER JOIN
pos_returns_details ON pos_products.product_id = pos_returns_details.prod_id INNER JOIN pos_return_accounts ON pos_returns_details.return_acc_id = pos_return_accounts.return_acc_id INNER JOIN
pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
where (pos_return_accounts.date between @fromDate and @ToDate)








GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureDateWiseSalesSummary]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureDateWiseSalesSummary]
(
	@fromDate nvarchar(50),
	@ToDate nvarchar(50)
)
as

SELECT pos_category.title, pos_brand.brand_title, pos_customers.full_name, pos_customers.cus_code, pos_subcategory.title AS Expr1, pos_sales_accounts.billNo, pos_sales_accounts.date, pos_sales_accounts.no_of_items, 
pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, pos_sales_accounts.discount, pos_sales_accounts.tax, pos_sales_accounts.amount_due, pos_sales_accounts.paid, pos_sales_accounts.credits, 
pos_sales_accounts.pCredits, pos_sales_accounts.status, pos_sales_accounts.remarks, pos_products.prod_name, pos_products.barcode, pos_employees.full_name AS Expr2, pos_employees.emp_code, 
pos_sales_details.quantity, pos_sales_details.pkg, pos_sales_details.full_pkg, pos_sales_details.Total_price, pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value, 
pos_sales_details.total_purchase, pos_sales_details.note, pos_sales_details.discount as perItemDiscount
FROM pos_sales_accounts INNER JOIN pos_employees ON pos_sales_accounts.employee_id = pos_employees.employee_id INNER JOIN
pos_customers ON pos_sales_accounts.customer_id = pos_customers.customer_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN pos_brand INNER JOIN
pos_products ON pos_brand.brand_id = pos_products.brand_id INNER JOIN pos_category ON pos_products.category_id = pos_category.category_id ON pos_sales_details.prod_id = pos_products.product_id INNER JOIN
pos_stock_details ON pos_products.product_id = pos_stock_details.prod_id INNER JOIN pos_subcategory ON pos_products.sub_cate_id = pos_subcategory.sub_cate_id
WHERE (pos_sales_accounts.date BETWEEN @fromDate AND @ToDate) and (pos_sales_accounts.status != 'Installment')








GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureDeals]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureDeals]
(
	@dealTitle nvarchar(50)
)
as

SELECT pos_deals.deal_title, pos_deals.status, pos_products.prod_name, pos_stock_details.item_barcode as barcode, 
pos_stock_details.whole_sale_price as size, pos_products.item_type, 
pos_stock_details.pur_price, pos_stock_details.sale_price, pos_stock_details.market_value,  pos_deal_items.quantity
FROM pos_stock_details INNER JOIN
pos_products ON pos_stock_details.prod_id = pos_products.product_id INNER JOIN
pos_deal_items ON pos_products.product_id = pos_deal_items.prod_id INNER JOIN
pos_deals ON pos_deal_items.deal_id = pos_deals.deal_id
WHERE (pos_deals.deal_title = @dealTitle) order by pos_deal_items.deal_id






GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureEmployeeWiseSalaryDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureEmployeeWiseSalaryDetails]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50),
	@employeeName nvarchar(50),
	@employeeCode nvarchar(25)
)

as

SELECT dbo.pos_salariesPaybook.date, dbo.pos_salariesPaybook.time, dbo.pos_salariesPaybook.paymentDate, dbo.pos_salariesPaybook.amount, dbo.pos_salariesPaybook.credits, dbo.pos_salariesPaybook.balance, 
dbo.pos_salariesPaybook.reference, dbo.pos_salariesPaybook.remarks, dbo.pos_employees.full_name, dbo.pos_employees.emp_code, dbo.pos_employees.cnic, dbo.pos_employees.mobile_no, 
dbo.pos_employees.address1, dbo.pos_employees.email, dbo.pos_employees.salary, dbo.pos_employees.daily_wages, dbo.pos_employees.commission
FROM dbo.pos_salariesPaybook INNER JOIN
dbo.pos_employees ON dbo.pos_salariesPaybook.employee_id = dbo.pos_employees.employee_id
where (dbo.pos_salariesPaybook.paymentDate between @fromDate and @toDate) and (dbo.pos_employees.full_name = @employeeName) and (dbo.pos_employees.emp_code = @employeeCode) order by dbo.pos_salariesPaybook.paymentDate asc











GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureGurantorsBatchNoWise]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[ReportProcedureGurantorsBatchNoWise]
(
	@BatchNo nvarchar(50)
)
as

SELECT pos_customers.full_name as CustomerName, pos_customers.fatherName as CustomerFather, pos_customers.cus_code, pos_granters.full_name AS gurantorName, pos_granters.fatherName AS gurantorFather, pos_granters.code as gurantorCode, 
pos_granters.cnic, pos_granters.house_no, pos_granters.telephone_no, pos_granters.mobile_no, pos_granters.address1, pos_granters.email, pos_payment_grantors.billNo, pos_batchNo.title as batchNo, pos_city.title AS province, pos_country.title AS country
FROM pos_customers INNER JOIN pos_payment_grantors ON pos_customers.customer_id = pos_payment_grantors.customer_id INNER JOIN
pos_granters ON pos_payment_grantors.granter_id = pos_granters.granter_id INNER JOIN pos_batchNo ON pos_customers.batch_id = pos_batchNo.batch_id INNER JOIN
pos_city ON pos_customers.city_id = pos_city.city_id AND pos_granters.city_id = pos_city.city_id INNER JOIN
pos_country ON pos_customers.country_id = pos_country.country_id AND pos_granters.country_id = pos_country.country_id
where (pos_batchNo.title = @BatchNo) order by pos_batchNo.title asc




















GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureGurantorsBillWise]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[ReportProcedureGurantorsBillWise]
(
	@BillNo nvarchar(50)
)
as

SELECT pos_customers.full_name as CustomerName, pos_customers.fatherName as CustomerFather, pos_customers.cus_code, pos_granters.full_name AS gurantorName, pos_granters.fatherName AS gurantorFather, pos_granters.code as gurantorCode, 
pos_granters.cnic, pos_granters.house_no, pos_granters.telephone_no, pos_granters.mobile_no, pos_granters.address1, pos_granters.email, pos_payment_grantors.billNo, pos_batchNo.title as batchNo, pos_city.title AS province, pos_country.title AS country
FROM pos_customers INNER JOIN pos_payment_grantors ON pos_customers.customer_id = pos_payment_grantors.customer_id INNER JOIN
pos_granters ON pos_payment_grantors.granter_id = pos_granters.granter_id INNER JOIN pos_batchNo ON pos_customers.batch_id = pos_batchNo.batch_id INNER JOIN
pos_city ON pos_customers.city_id = pos_city.city_id AND pos_granters.city_id = pos_city.city_id INNER JOIN
pos_country ON pos_customers.country_id = pos_country.country_id AND pos_granters.country_id = pos_country.country_id
where (pos_payment_grantors.billNo = @BillNo) order by pos_payment_grantors.billNo asc




















GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureGurantorsCustomerWise]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[ReportProcedureGurantorsCustomerWise]
(
	@customerName nvarchar(50),
	@customerCode nvarchar(50)
)
as

SELECT pos_customers.full_name as CustomerName, pos_customers.fatherName as CustomerFather, pos_customers.cus_code, pos_granters.full_name AS gurantorName, pos_granters.fatherName AS gurantorFather, pos_granters.code as gurantorCode, 
pos_granters.cnic, pos_granters.house_no, pos_granters.telephone_no, pos_granters.mobile_no, pos_granters.address1, pos_granters.email, pos_payment_grantors.billNo, pos_batchNo.title as batchNo, pos_city.title AS province, pos_country.title AS country
FROM pos_customers INNER JOIN pos_payment_grantors ON pos_customers.customer_id = pos_payment_grantors.customer_id INNER JOIN
pos_granters ON pos_payment_grantors.granter_id = pos_granters.granter_id INNER JOIN pos_batchNo ON pos_customers.batch_id = pos_batchNo.batch_id INNER JOIN
pos_city ON pos_customers.city_id = pos_city.city_id AND pos_granters.city_id = pos_city.city_id INNER JOIN
pos_country ON pos_customers.country_id = pos_country.country_id AND pos_granters.country_id = pos_country.country_id
where (pos_customers.full_name = @customerName) and (pos_customers.cus_code = @customerCode) order by pos_customers.full_name asc




















GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureGurantorsOverallDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[ReportProcedureGurantorsOverallDetails]
as

SELECT pos_customers.full_name as CustomerName, pos_customers.fatherName as CustomerFather, pos_customers.cus_code, pos_granters.full_name AS gurantorName, pos_granters.fatherName AS gurantorFather, pos_granters.code as gurantorCode, 
pos_granters.cnic, pos_granters.house_no, pos_granters.telephone_no, pos_granters.mobile_no, pos_granters.address1, pos_granters.email, pos_payment_grantors.billNo, pos_batchNo.title as batchNo, pos_city.title AS province, pos_country.title AS country
FROM pos_customers INNER JOIN pos_payment_grantors ON pos_customers.customer_id = pos_payment_grantors.customer_id INNER JOIN
pos_granters ON pos_payment_grantors.granter_id = pos_granters.granter_id INNER JOIN pos_batchNo ON pos_customers.batch_id = pos_batchNo.batch_id INNER JOIN
pos_city ON pos_customers.city_id = pos_city.city_id AND pos_granters.city_id = pos_city.city_id INNER JOIN
pos_country ON pos_customers.country_id = pos_country.country_id AND pos_granters.country_id = pos_country.country_id
















GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureInstallmentDefaultersList]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureInstallmentDefaultersList]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50)
)

as
SELECT pos_batchNo.title, pos_country.title AS country, pos_customers.full_name, pos_customers.fatherName, pos_customers.cus_code, pos_installment_accounts.total_months, pos_installment_accounts.t_amount, pos_installment_accounts.total_interest, 
pos_installment_accounts.grand_total, pos_installment_accounts.total_interest_percent, pos_installment_accounts.total_dues, pos_city.title AS province, pos_installment_plan.installmentNo, 
pos_installment_plan.installmentDate, pos_installment_plan.dueDate, pos_installment_plan.interest, pos_installment_plan.interest_percentage, pos_installment_plan.amount, pos_installment_plan.dues, 
pos_installment_plan.status, pos_installment_plan.total_amount, pos_customer_lastCredits.lastCredits, pos_customers.cnic, pos_customers.mobile_no, pos_customers.address1, pos_customers.opening_balance, 
pos_customers.email, pos_customers.house_no, pos_sales_accounts.billNo, pos_sales_accounts.no_of_items, pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, pos_sales_accounts.discount, 
pos_sales_accounts.tax, pos_sales_accounts.amount_due, pos_sales_accounts.credits, pos_sales_accounts.paid, pos_sales_accounts.pCredits, pos_sales_accounts.remarks
FROM pos_installment_plan INNER JOIN pos_installment_accounts ON pos_installment_plan.installment_acc_id = pos_installment_accounts.installment_acc_id INNER JOIN
pos_sales_accounts ON pos_installment_accounts.sales_acc_id = pos_sales_accounts.sales_acc_id INNER JOIN pos_batchNo INNER JOIN
pos_customers ON pos_batchNo.batch_id = pos_customers.batch_id INNER JOIN pos_country ON pos_customers.country_id = pos_country.country_id INNER JOIN
pos_city ON pos_customers.city_id = pos_city.city_id ON pos_sales_accounts.customer_id = pos_customers.customer_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
pos_customer_lastCredits ON pos_customers.customer_id = pos_customer_lastCredits.customer_id
where (pos_installment_plan.installmentDate between @fromDate and @toDate) and (pos_installment_plan.status != 'Completed') order by pos_installment_plan.installmentDate asc











GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureInvestorPaybookReceipt]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[ReportProcedureInvestorPaybookReceipt]
(
	@date nvarchar(50),
	@time nvarchar(50)
)
as

SELECT pos_employees.full_name, pos_employees.emp_code, pos_investorPaybook.date, pos_investorPaybook.time, pos_investorPaybook.fromDate, pos_investorPaybook.toDate, pos_investorPaybook.reference, 
pos_investorPaybook.remarks, pos_investorPaybook.investorShare, pos_investorPaybook.investment, pos_investorPaybook.netProfit, pos_investorPaybook.profit, pos_investorPaybook.lessAmount, pos_investorPaybook.salaries, 
pos_investorPaybook.payment, pos_investorPaybook.balance, pos_investorPaybook.credits, pos_investors.full_name AS investerName, pos_investors.code, pos_investors.mobile_no, pos_investors.address1,
pos_investors.email, pos_investors.cnic  
FROM pos_employees INNER JOIN pos_investorPaybook ON pos_employees.employee_id = pos_investorPaybook.employee_id INNER JOIN
pos_investors ON pos_investorPaybook.investor_id = pos_investors.investor_id
where (pos_investorPaybook.date = @date) and  (pos_investorPaybook.time = @time)














GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureInvestorWisePayments]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureInvestorWisePayments]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50),
	@investorName nvarchar(50),
	@investorCode nvarchar(50)
)

as

SELECT pos_investorPaybook.date, pos_investorPaybook.time, pos_investorPaybook.fromDate, pos_investorPaybook.toDate, pos_investorPaybook.reference, pos_investorPaybook.remarks, pos_investorPaybook.investorShare, 
pos_investorPaybook.investment, pos_investorPaybook.netProfit, pos_investorPaybook.profit, pos_investorPaybook.lessAmount, pos_investorPaybook.salaries, pos_investorPaybook.payment, pos_investorPaybook.credits, pos_investorPaybook.balance, pos_investors.full_name, 
pos_investors.code, pos_investors.cnic, pos_investors.mobile_no, pos_investors.address1, pos_investors.email, pos_employees.full_name AS employeeName, pos_employees.emp_code, pos_country.title as country,  pos_city.title AS city
FROM pos_investorPaybook INNER JOIN pos_investors ON pos_investorPaybook.investor_id = pos_investors.investor_id INNER JOIN
pos_employees ON pos_investorPaybook.employee_id = pos_employees.employee_id INNER JOIN
pos_city ON pos_investors.city_id = pos_city.city_id AND pos_employees.city_id = pos_city.city_id INNER JOIN
pos_country ON pos_investors.country_id = pos_country.country_id AND pos_employees.country_id = pos_country.country_id
where (pos_investorPaybook.date between @fromDate and @toDate) and (pos_investors.full_name = @investorName) and (pos_investors.code = @investorCode) order by pos_investorPaybook.date asc

 















GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureInvoiceWiseIMEIDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureInvoiceWiseIMEIDetails]
(
	@invoiceNo nvarchar(50)
)

as


SELECT pos_purchase.date, pos_purchase.bill_no, pos_purchase_imei.imei_no, pos_purchase_imei.isSold, pos_purchase_imei.invoiceNo, 
pos_suppliers.full_name, pos_suppliers.code, pos_products.prod_name, pos_products.barcode
FROM pos_products INNER JOIN pos_purchase_imei ON pos_products.product_id = pos_purchase_imei.prod_id INNER JOIN
pos_purchased_items ON pos_products.product_id = pos_purchased_items.prod_id INNER JOIN pos_purchase INNER JOIN
pos_suppliers ON pos_purchase.supplier_id = pos_suppliers.supplier_id ON pos_purchased_items.purchase_id = pos_purchase.purchase_id
where (pos_purchase_imei.invoiceNo = @invoiceNo) order by pos_purchase.date asc













GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureItemsWiseIMEIDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureItemsWiseIMEIDetails]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50),
	@itemName nvarchar(50),
	@barcode nvarchar(50)
)

as


SELECT pos_purchase.date, pos_purchase.bill_no, pos_purchase_imei.imei_no, pos_purchase_imei.isSold, pos_purchase_imei.invoiceNo, 
pos_suppliers.full_name, pos_suppliers.code, pos_products.prod_name, pos_products.barcode
FROM pos_products INNER JOIN pos_purchase_imei ON pos_products.product_id = pos_purchase_imei.prod_id INNER JOIN
pos_purchased_items ON pos_products.product_id = pos_purchased_items.prod_id INNER JOIN pos_purchase INNER JOIN
pos_suppliers ON pos_purchase.supplier_id = pos_suppliers.supplier_id ON pos_purchased_items.purchase_id = pos_purchase.purchase_id
where (pos_purchase.date between @fromDate and @toDate) and (pos_products.prod_name = @itemName) and (pos_products.barcode = @barcode) 
order by pos_purchase.date asc













GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureNameWiseLoanDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureNameWiseLoanDetails]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50),
	@fullName nvarchar(50)
)
as

SELECT format(date, 'dd/MMMM/yyyy') AS [Date], time , fullName, fatherName, 
contactNo, amount, reference , remarks, status
FROM dbo.pos_loanDetails where (date between @fromDate and @toDate) and (fullName = @fullName) 














GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureOverAllCustomerDues]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureOverAllCustomerDues]

as

SELECT pos_customers.full_name, pos_customers.fatherName, pos_customers.cus_code, pos_customers.cnic, pos_customers.mobile_no, pos_customers.address1,
pos_customers.customer_id, pos_customer_lastCredits.lastCredits, pos_customer_lastCredits.due_days, pos_batchNo.title as batchNo, pos_country.title AS country, pos_city.title AS city
FROM pos_batchNo INNER JOIN pos_customers ON pos_batchNo.batch_id = pos_customers.batch_id INNER JOIN pos_customer_lastCredits ON pos_customers.customer_id = pos_customer_lastCredits.customer_id
INNER JOIN pos_country ON pos_customers.country_id = pos_country.country_id INNER JOIN pos_city ON pos_customers.city_id = pos_city.city_id
and (pos_customer_lastCredits.lastCredits > 0)














GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureOverAllInvestorPayments]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureOverAllInvestorPayments]

as

SELECT pos_investorPaybook.date, pos_investorPaybook.time, pos_investorPaybook.fromDate, pos_investorPaybook.toDate, pos_investorPaybook.reference, pos_investorPaybook.remarks, pos_investorPaybook.investorShare, 
pos_investorPaybook.investment, pos_investorPaybook.netProfit, pos_investorPaybook.profit, pos_investorPaybook.lessAmount, pos_investorPaybook.salaries, pos_investorPaybook.payment, pos_investorPaybook.credits, pos_investorPaybook.balance, pos_investors.full_name, 
pos_investors.code, pos_investors.cnic, pos_investors.mobile_no, pos_investors.address1, pos_investors.email, pos_employees.full_name AS employeeName, pos_employees.emp_code, pos_country.title as country,  pos_city.title AS city
FROM pos_investorPaybook INNER JOIN pos_investors ON pos_investorPaybook.investor_id = pos_investors.investor_id INNER JOIN
pos_employees ON pos_investorPaybook.employee_id = pos_employees.employee_id INNER JOIN
pos_city ON pos_investors.city_id = pos_city.city_id AND pos_employees.city_id = pos_city.city_id INNER JOIN
pos_country ON pos_investors.country_id = pos_country.country_id AND pos_employees.country_id = pos_country.country_id















GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureOverAllInvestorsList]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureOverAllInvestorsList]

as

SELECT pos_investors.date, pos_investors.full_name, pos_investors.code, pos_investors.post_code, pos_investors.zip_code, pos_investors.cnic, pos_investors.house_no,
pos_investors.telephone_no, pos_investors.mobile_no, pos_investors.address1, pos_investors.address2, pos_investors.email, pos_investors.share_percentage, 
pos_investors.profit_percentage, pos_investors.investment, pos_investors.remarks, pos_investors.status, pos_city.title, pos_country.title AS country
FROM pos_investors INNER JOIN pos_city ON pos_investors.city_id = pos_city.city_id INNER JOIN pos_country ON pos_investors.country_id = pos_country.country_id














GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureOverAllRecoveries]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[ReportProcedureOverAllRecoveries]
(
	@customerName nvarchar(50),
	@code nvarchar(50)
)
as

SELECT pos_recovery_details.date, pos_recovery_details.time, pos_recovery_details.invoiceNo, pos_recovery_details.reference, pos_recovery_details.remarks, pos_recovery_details.employee_id, pos_recovery_details.customer_id, 
pos_recoveries.installmentDate, pos_recoveries.installmentNo, pos_recoveries.monthly_installment, pos_recoveries.monthly_interest, pos_recoveries.monthlyDues, pos_recoveries.total_amount, pos_recoveries.amount, 
pos_recoveries.credits, pos_customers.full_name, pos_customers.fatherName, pos_customers.cus_code, pos_customers.cnic, pos_customers.mobile_no, pos_customers.address1, pos_customer_lastCredits.lastCredits, 
pos_employees.full_name AS employee, pos_employees.emp_code
FROM pos_customer_lastCredits INNER JOIN pos_customers ON pos_customer_lastCredits.customer_id = pos_customers.customer_id INNER JOIN
pos_recovery_details ON pos_customers.customer_id = pos_recovery_details.customer_id INNER JOIN pos_employees ON
pos_recovery_details.employee_id = pos_employees.employee_id INNER JOIN pos_recoveries ON pos_recovery_details.recovery_id = pos_recoveries.recovery_id
WHERE (pos_customers.full_name = @customerName) and (pos_customers.cus_code = @code) ORDER BY pos_recovery_details.date asc














GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureProvinceWiseInvestorsList]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[ReportProcedureProvinceWiseInvestorsList]
(
	@province nvarchar(50)
)

as

SELECT pos_investors.date, pos_investors.full_name, pos_investors.code, pos_investors.post_code, pos_investors.zip_code, pos_investors.cnic, pos_investors.house_no,
pos_investors.telephone_no, pos_investors.mobile_no, pos_investors.address1, pos_investors.address2, pos_investors.email, pos_investors.share_percentage, 
pos_investors.profit_percentage, pos_investors.investment, pos_investors.remarks, pos_investors.status, pos_city.title, pos_country.title AS country
FROM pos_investors INNER JOIN pos_city ON pos_investors.city_id = pos_city.city_id INNER JOIN pos_country ON pos_investors.country_id = pos_country.country_id
where pos_city.title = @province














GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureProvinceWiseInvoices]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureProvinceWiseInvoices]
(
	@province nvarchar(50),
	@toDate nvarchar(50),
	@fromDate nvarchar(50)
)

as
SELECT pos_batchNo.title, pos_country.title AS country, pos_customers.full_name, pos_customers.fatherName, pos_customers.cus_code, pos_installment_accounts.total_months, pos_installment_accounts.t_amount, pos_installment_accounts.total_interest, 
pos_installment_accounts.grand_total, pos_installment_accounts.total_interest_percent, pos_installment_accounts.total_dues, pos_city.title AS province, pos_installment_plan.installmentNo, 
pos_installment_plan.installmentDate, pos_installment_plan.dueDate, pos_installment_plan.interest, pos_installment_plan.interest_percentage, pos_installment_plan.amount, pos_installment_plan.dues, 
pos_installment_plan.status, pos_installment_plan.total_amount, pos_customer_lastCredits.lastCredits, pos_customers.cnic, pos_customers.mobile_no, pos_customers.address1, pos_customers.opening_balance, 
pos_customers.email, pos_customers.house_no, pos_sales_accounts.billNo, pos_sales_accounts.no_of_items, pos_sales_accounts.total_qty, pos_sales_accounts.sub_total, pos_sales_accounts.discount, 
pos_sales_accounts.tax, pos_sales_accounts.amount_due, pos_sales_accounts.credits, pos_sales_accounts.paid, pos_sales_accounts.pCredits, pos_sales_accounts.remarks
FROM pos_installment_plan INNER JOIN pos_installment_accounts ON pos_installment_plan.installment_acc_id = pos_installment_accounts.installment_acc_id INNER JOIN
pos_sales_accounts ON pos_installment_accounts.sales_acc_id = pos_sales_accounts.sales_acc_id INNER JOIN pos_batchNo INNER JOIN
pos_customers ON pos_batchNo.batch_id = pos_customers.batch_id INNER JOIN pos_country ON pos_customers.country_id = pos_country.country_id INNER JOIN
pos_city ON pos_customers.city_id = pos_city.city_id ON pos_sales_accounts.customer_id = pos_customers.customer_id INNER JOIN pos_sales_details ON pos_sales_accounts.sales_acc_id = pos_sales_details.sales_acc_id INNER JOIN
pos_customer_lastCredits ON pos_customers.customer_id = pos_customer_lastCredits.customer_id
where (pos_city.title = @province) and (pos_installment_plan.installmentDate between @toDate and @fromDate) order by pos_sales_accounts.billNo asc






GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureReportsTitles]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[ReportProcedureReportsTitles]

as

SELECT * from pos_report_settings






GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureSalaryReceipt]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[ReportProcedureSalaryReceipt]
(
	@date nvarchar(50),
	@time nvarchar(20)
)

as

SELECT dbo.pos_salariesPaybook.date, dbo.pos_salariesPaybook.time, dbo.pos_salariesPaybook.paymentDate, dbo.pos_salariesPaybook.amount, dbo.pos_salariesPaybook.credits, dbo.pos_salariesPaybook.balance, 
dbo.pos_salariesPaybook.reference, dbo.pos_salariesPaybook.remarks, dbo.pos_employees.full_name, dbo.pos_employees.emp_code, dbo.pos_employees.cnic, dbo.pos_employees.mobile_no, 
dbo.pos_employees.address1, dbo.pos_employees.email, dbo.pos_salariesPaybook.salary, dbo.pos_salariesPaybook.commission, dbo.pos_salariesPaybook.hourly_wages, dbo.pos_salariesPaybook.form_date, dbo.pos_salariesPaybook.to_date, dbo.pos_salariesPaybook.commission_payment, dbo.pos_salariesPaybook.total_duration
FROM dbo.pos_salariesPaybook INNER JOIN
dbo.pos_employees ON dbo.pos_salariesPaybook.employee_id = dbo.pos_employees.employee_id
where (dbo.pos_salariesPaybook.date = @date) and (dbo.pos_salariesPaybook.time = @time)






GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureStatusCapitalHistoryDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[ReportProcedureStatusCapitalHistoryDetails]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50),
	@status nvarchar(50)
)
as

SELECT date as [Date], time as [Time], amount as [Amount], total_capital, total_investment, remarks as [Note], status as [Status]
FROM dbo.pos_capital_history
where (date between @fromDate and @toDate) and (status = @status) order by date asc







GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureStatusWiseChequeDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[ReportProcedureStatusWiseChequeDetails]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50),
	@status nvarchar(50)

)

as

SELECT pos_bank.bank_title, pos_customers.full_name, pos_customers.fatherName, pos_customers.cus_code, pos_customers.cnic, pos_customers.mobile_no, pos_customers.address1, pos_customers.email, 
pos_customerChequeDetails.billNo, pos_customerChequeDetails.date, pos_customerChequeDetails.bounceDate, pos_customerChequeDetails.accountNo, pos_customerChequeDetails.remarks, 
pos_customerChequeDetails.amount, pos_customerChequeDetails.status, pos_customerChequeDetails.chequeNo
FROM pos_bank INNER JOIN pos_customerChequeDetails ON pos_bank.bank_id = pos_customerChequeDetails.bank_id INNER JOIN
pos_customers ON pos_customerChequeDetails.customer_id = pos_customers.customer_id
where (pos_customerChequeDetails.date between @fromDate and @toDate) and (pos_customerChequeDetails.status = @status)







GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureStatusWiseIMEIDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureStatusWiseIMEIDetails]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50),
	@status nvarchar(50)
)

as


SELECT pos_purchase.date, pos_purchase.bill_no, pos_purchase_imei.imei_no, pos_purchase_imei.isSold, pos_purchase_imei.invoiceNo, 
pos_suppliers.full_name, pos_suppliers.code, pos_products.prod_name, pos_products.barcode
FROM pos_products INNER JOIN pos_purchase_imei ON pos_products.product_id = pos_purchase_imei.prod_id INNER JOIN
pos_purchased_items ON pos_products.product_id = pos_purchased_items.prod_id INNER JOIN pos_purchase INNER JOIN
pos_suppliers ON pos_purchase.supplier_id = pos_suppliers.supplier_id ON pos_purchased_items.purchase_id = pos_purchase.purchase_id
where (pos_purchase.date between @fromDate and @toDate) and (pos_purchase_imei.isSold = @status)  order by pos_purchase.date asc







GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureStatusWiseInvestorsList]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[ReportProcedureStatusWiseInvestorsList]
(
	@status nvarchar(50)
)

as

SELECT pos_investors.date, pos_investors.full_name, pos_investors.code, pos_investors.post_code, pos_investors.zip_code, pos_investors.cnic, pos_investors.house_no,
pos_investors.telephone_no, pos_investors.mobile_no, pos_investors.address1, pos_investors.address2, pos_investors.email, pos_investors.share_percentage, 
pos_investors.profit_percentage, pos_investors.investment, pos_investors.remarks, pos_investors.status, pos_city.title, pos_country.title AS country
FROM pos_investors INNER JOIN pos_city ON pos_investors.city_id = pos_city.city_id INNER JOIN pos_country ON pos_investors.country_id = pos_country.country_id
where pos_investors.status = @status







GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureSupplierWiseIMEIDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedureSupplierWiseIMEIDetails]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50),
	@supplierTitle nvarchar(50),
	@supplierCode nvarchar(50)
)

as


SELECT pos_purchase.date, pos_purchase.bill_no, pos_purchase_imei.imei_no, pos_purchase_imei.isSold, pos_purchase_imei.invoiceNo, 
pos_suppliers.full_name, pos_suppliers.code, pos_products.prod_name, pos_products.barcode
FROM pos_products INNER JOIN pos_purchase_imei ON pos_products.product_id = pos_purchase_imei.prod_id INNER JOIN
pos_purchased_items ON pos_products.product_id = pos_purchased_items.prod_id INNER JOIN pos_purchase INNER JOIN
pos_suppliers ON pos_purchase.supplier_id = pos_suppliers.supplier_id ON pos_purchased_items.purchase_id = pos_purchase.purchase_id
where (pos_purchase.date between @fromDate and @toDate) and (pos_suppliers.full_name = @supplierTitle) and (pos_suppliers.code = @supplierCode) 
order by pos_purchase.date asc







GO
/****** Object:  StoredProcedure [dbo].[ReportProcedureSupplierWisePaymentDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[ReportProcedureSupplierWisePaymentDetails]
(
	@fromDate nvarchar(50),
	@toDate nvarchar(50),
	@supplierTitle nvarchar(50),
	@supplierCode nvarchar(50)
)
as

SELECT pos_suppliers.full_name, pos_suppliers.code, pos_supplier_paybook.date, pos_supplier_paybook.time, pos_supplier_paybook.reference, pos_supplier_paybook.remarks, pos_supplier_paybook.payment, 
pos_supplier_paybook.previous_payables, pos_supplier_paybook.balance, pos_supplier_payables.previous_payables AS lastPayables
FROM pos_suppliers INNER JOIN pos_supplier_paybook ON pos_suppliers.supplier_id = pos_supplier_paybook.supplier_id INNER JOIN
pos_supplier_payables ON pos_suppliers.supplier_id = pos_supplier_payables.supplier_id
where (pos_supplier_paybook.date between @fromDate and @toDate) and (pos_suppliers.full_name = @supplierTitle) and (pos_suppliers.code = @supplierCode) order by pos_supplier_paybook.date






GO
/****** Object:  StoredProcedure [dbo].[ReportProcedurModifyChequeDetails]    Script Date: 7/21/2024 1:34:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[ReportProcedurModifyChequeDetails]
(
	@search nvarchar(50)
)

as

SELECT pos_customerChequeDetails.billNo as [Invoice #], pos_bank.bank_title as [Bank Title], format(pos_customerChequeDetails.date, 'dd/MMMM/yyyy') as [Cheque Date], format(pos_customerChequeDetails.bounceDate, 'dd/MMMM/yyyy') as [Bounce Date], 
pos_customerChequeDetails.accountNo as [Account #], pos_customerChequeDetails.chequeNo as [Cheque #], pos_customerChequeDetails.amount as [Amount], pos_customerChequeDetails.remarks as [Note],
pos_customerChequeDetails.status as [Status]
FROM pos_bank INNER JOIN pos_customerChequeDetails ON pos_bank.bank_id = pos_customerChequeDetails.bank_id INNER JOIN
pos_customers ON pos_customerChequeDetails.customer_id = pos_customers.customer_id
where (pos_customerChequeDetails.billNo = @search)






GO
