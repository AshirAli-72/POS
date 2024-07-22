USE [installment_db]
GO
-- Check and create indexes if they do not exist

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_HoldItems_billNo')
    CREATE NONCLUSTERED INDEX IX_tbl_HoldItems_billNo ON pos_hold_items(billNo);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_HoldItems_customer_id')
    CREATE NONCLUSTERED INDEX IX_tbl_HoldItems_customer_id ON pos_hold_items(customer_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_HoldItems_employee_id')
    CREATE NONCLUSTERED INDEX IX_tbl_HoldItems_employee_id ON pos_hold_items(employee_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_HoldItemDetails_sales_acc_id')
    CREATE NONCLUSTERED INDEX IX_tbl_HoldItemDetails_sales_acc_id ON pos_hold_items_details(sales_acc_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_HoldItemDetails_prod_id')
    CREATE NONCLUSTERED INDEX IX_tbl_HoldItemDetails_prod_id ON pos_hold_items_details(prod_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_HoldItemDetails_stock_id')
    CREATE NONCLUSTERED INDEX IX_tbl_HoldItemDetails_stock_id ON pos_hold_items_details(stock_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_GroupedItems_prod_id')
    CREATE NONCLUSTERED INDEX IX_tbl_GroupedItems_prod_id ON pos_grouped_items(prod_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_GroupedItems_stock_id')
    CREATE NONCLUSTERED INDEX IX_tbl_GroupedItems_stock_id ON pos_grouped_items(stock_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_PromoGroupItems_prod_id')
    CREATE NONCLUSTERED INDEX IX_tbl_PromoGroupItems_prod_id ON pos_promo_group_items(prod_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_PromoGroupItems_promo_group_id')
    CREATE NONCLUSTERED INDEX IX_tbl_PromoGroupItems_promo_group_id ON pos_promo_group_items(promo_group_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_PromoGroupItems_stock_id')
    CREATE NONCLUSTERED INDEX IX_tbl_PromoGroupItems_stock_id ON pos_promo_group_items(stock_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_PromoGroups_title')
    CREATE NONCLUSTERED INDEX IX_tbl_PromoGroups_title ON pos_promo_groups(title);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Promotions_start_date')
    CREATE NONCLUSTERED INDEX IX_tbl_Promotions_start_date ON pos_promotions(start_date);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Promotions_end_date')
    CREATE NONCLUSTERED INDEX IX_tbl_Promotions_end_date ON pos_promotions(end_date);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Promotions_start_time')
    CREATE NONCLUSTERED INDEX IX_tbl_Promotions_start_time ON pos_promotions(start_time);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Promotions_end_time')
    CREATE NONCLUSTERED INDEX IX_tbl_Promotions_end_time ON pos_promotions(end_time);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Promotions_status')
    CREATE NONCLUSTERED INDEX IX_tbl_Promotions_status ON pos_promotions(status);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Promotions_promo_group_id')
    CREATE NONCLUSTERED INDEX IX_tbl_Promotions_promo_group_id ON pos_promotions(promo_group_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_ClockOut_date')
    CREATE NONCLUSTERED INDEX IX_tbl_ClockOut_date ON pos_clock_out(date);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_ClockOut_end_time')
    CREATE NONCLUSTERED INDEX IX_tbl_ClockOut_end_time ON pos_clock_out(end_time);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_ClockOut_clock_in_id')
    CREATE NONCLUSTERED INDEX IX_tbl_ClockOut_clock_in_id ON pos_clock_out(clock_in_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_ClockOut_to_user_id')
    CREATE NONCLUSTERED INDEX IX_tbl_ClockOut_to_user_id ON pos_clock_out(to_user_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_ClockIn_to_user_id')
    CREATE NONCLUSTERED INDEX IX_tbl_ClockIn_to_user_id ON pos_clock_in(to_user_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_ClockIn_date')
    CREATE NONCLUSTERED INDEX IX_tbl_ClockIn_date ON pos_clock_in(date);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_ClockIn_start_time')
    CREATE NONCLUSTERED INDEX IX_tbl_ClockIn_start_time ON pos_clock_in(start_time);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_ClockIn_shift_id')
    CREATE NONCLUSTERED INDEX IX_tbl_ClockIn_shift_id ON pos_clock_in(shift_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_ClockIn_counter_id')
    CREATE NONCLUSTERED INDEX IX_tbl_ClockIn_counter_id ON pos_clock_in(counter_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_ClockIn_status')
    CREATE NONCLUSTERED INDEX IX_tbl_ClockIn_status ON pos_clock_in(status);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_SalesAccounts_billNo')
    CREATE NONCLUSTERED INDEX IX_tbl_SalesAccounts_billNo ON pos_sales_accounts(billNo);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_SalesAccounts_date')
    CREATE NONCLUSTERED INDEX IX_tbl_SalesAccounts_date ON pos_sales_accounts(date);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_SalesAccounts_clock_in_id')
    CREATE NONCLUSTERED INDEX IX_tbl_SalesAccounts_clock_in_id ON pos_sales_accounts(clock_in_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_SalesAccounts_is_returned')
    CREATE NONCLUSTERED INDEX IX_tbl_SalesAccounts_is_returned ON pos_sales_accounts(is_returned);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_SalesAccounts_customer_id')
    CREATE NONCLUSTERED INDEX IX_tbl_SalesAccounts_customer_id ON pos_sales_accounts(customer_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_SalesAccounts_employee_id')
    CREATE NONCLUSTERED INDEX IX_tbl_SalesAccounts_employee_id ON pos_sales_accounts(employee_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_SalesReturnsAccounts_billNo')
    CREATE NONCLUSTERED INDEX IX_tbl_SalesReturnsAccounts_billNo ON pos_return_accounts(billNo);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_SalesReturnsAccounts_date')
    CREATE NONCLUSTERED INDEX IX_tbl_SalesReturnsAccounts_date ON pos_return_accounts(date);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_SalesReturnsAccounts_clock_in_id')
    CREATE NONCLUSTERED INDEX IX_tbl_SalesReturnsAccounts_clock_in_id ON pos_return_accounts(clock_in_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_SalesReturnsAccounts_customer_id')
    CREATE NONCLUSTERED INDEX IX_tbl_SalesReturnsAccounts_customer_id ON pos_return_accounts(customer_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_SalesReturnsAccounts_employee_id')
    CREATE NONCLUSTERED INDEX IX_tbl_SalesReturnsAccounts_employee_id ON pos_return_accounts(employee_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_SalesDetails_prod_id')
    CREATE NONCLUSTERED INDEX IX_tbl_SalesDetails_prod_id ON pos_sales_details(prod_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_SalesReturnsDetails_prod_id')
    CREATE NONCLUSTERED INDEX IX_tbl_SalesReturnsDetails_prod_id ON pos_returns_details(prod_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Products_prod_name')
    CREATE NONCLUSTERED INDEX IX_tbl_Products_prod_name ON pos_products(prod_name);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Products_barcode')
    CREATE NONCLUSTERED INDEX IX_tbl_Products_barcode ON pos_products(barcode);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Products_expiry_date')
    CREATE NONCLUSTERED INDEX IX_tbl_Products_expiry_date ON pos_products(expiry_date);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Products_category_id')
    CREATE NONCLUSTERED INDEX IX_tbl_Products_category_id ON pos_products(category_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Products_brand_id')
    CREATE NONCLUSTERED INDEX IX_tbl_Products_brand_id ON pos_products(brand_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Products_sub_cate_id')
    CREATE NONCLUSTERED INDEX IX_tbl_Products_sub_cate_id ON pos_products(sub_cate_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Products_color_id')
    CREATE NONCLUSTERED INDEX IX_tbl_Products_color_id ON pos_products(color_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_StockDetails_item_barcode')
    CREATE NONCLUSTERED INDEX IX_tbl_StockDetails_item_barcode ON pos_stock_details(item_barcode);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_StockDetails_quantity')
    CREATE NONCLUSTERED INDEX IX_tbl_StockDetails_quantity ON pos_stock_details(quantity);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_StockDetails_prod_id')
    CREATE NONCLUSTERED INDEX IX_tbl_StockDetails_prod_id ON pos_stock_details(prod_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_StockBackup_date')
    CREATE NONCLUSTERED INDEX IX_tbl_StockBackup_date ON pos_stock_backup(date);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_StockBackup_item_barcode')
    CREATE NONCLUSTERED INDEX IX_tbl_StockBackup_item_barcode ON pos_stock_backup(item_barcode);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_StockBackup_quantity')
    CREATE NONCLUSTERED INDEX IX_tbl_StockBackup_quantity ON pos_stock_backup(quantity);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_StockBackup_prod_id')
    CREATE NONCLUSTERED INDEX IX_tbl_StockBackup_prod_id ON pos_stock_backup(prod_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_StockHistory_date')
    CREATE NONCLUSTERED INDEX IX_tbl_StockHistory_date ON pos_stock_history(date);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_StockHistory_product_id')
    CREATE NONCLUSTERED INDEX IX_tbl_StockHistory_product_id ON pos_stock_history(product_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_StockHistory_user_id')
    CREATE NONCLUSTERED INDEX IX_tbl_StockHistory_user_id ON pos_stock_history(user_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Customers_full_name')
    CREATE NONCLUSTERED INDEX IX_tbl_Customers_full_name ON pos_customers(full_name);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Customers_cus_code')
    CREATE NONCLUSTERED INDEX IX_tbl_Customers_cus_code ON pos_customers(cus_code);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Customers_mobile_no')
    CREATE NONCLUSTERED INDEX IX_tbl_Customers_mobile_no ON pos_customers(mobile_no);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Customers_status')
    CREATE NONCLUSTERED INDEX IX_tbl_Customers_status ON pos_customers(status);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Customers_age')
    CREATE NONCLUSTERED INDEX IX_tbl_Customers_age ON pos_customers(age);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Brand_brand_title')
    CREATE NONCLUSTERED INDEX IX_tbl_Brand_brand_title ON pos_brand(brand_title);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Category_title')
    CREATE NONCLUSTERED INDEX IX_tbl_Category_title ON pos_category(title);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_CartItems_product_name')
    CREATE NONCLUSTERED INDEX IX_tbl_CartItems_product_name ON pos_cart_items(product_name);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_CartItems_barcode')
    CREATE NONCLUSTERED INDEX IX_tbl_CartItems_barcode ON pos_cart_items(barcode);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_CartItems_product_id')
    CREATE NONCLUSTERED INDEX IX_tbl_CartItems_product_id ON pos_cart_items(product_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_CartItems_stock_id')
    CREATE NONCLUSTERED INDEX IX_tbl_CartItems_stock_id ON pos_cart_items(stock_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_CartItems_category_id')
    CREATE NONCLUSTERED INDEX IX_tbl_CartItems_category_id ON pos_cart_items(category_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_CartItems_brand_id')
    CREATE NONCLUSTERED INDEX IX_tbl_CartItems_brand_id ON pos_cart_items(brand_id);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_CartItems_mac_address')
    CREATE NONCLUSTERED INDEX IX_tbl_CartItems_mac_address ON pos_cart_items(mac_address);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_pos')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_pos ON pos_tbl_authorities_dashboard(pos);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_purchases')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_purchases ON pos_tbl_authorities_dashboard(purchases);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_products')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_products ON pos_tbl_authorities_dashboard(products);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_recoveries')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_recoveries ON pos_tbl_authorities_dashboard(recoveries);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_expenses')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_expenses ON pos_tbl_authorities_dashboard(expenses);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_suppliers')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_suppliers ON pos_tbl_authorities_dashboard(suppliers);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_employee')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_employee ON pos_tbl_authorities_dashboard(employee);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_customers')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_customers ON pos_tbl_authorities_dashboard(customers);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_stock')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_stock ON pos_tbl_authorities_dashboard(stock);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_reports')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_reports ON pos_tbl_authorities_dashboard(reports);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_customer_dues')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_customer_dues ON pos_tbl_authorities_dashboard(customer_dues);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_settings')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_settings ON pos_tbl_authorities_dashboard(settings);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_notifications')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_notifications ON pos_tbl_authorities_dashboard(notifications);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_backups')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_backups ON pos_tbl_authorities_dashboard(backups);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_restores')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_restores ON pos_tbl_authorities_dashboard(restores);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_about')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_about ON pos_tbl_authorities_dashboard(about);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_logout')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_logout ON pos_tbl_authorities_dashboard(logout);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_capital')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_capital ON pos_tbl_authorities_dashboard(capital);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_dailyBalance')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_dailyBalance ON pos_tbl_authorities_dashboard(dailyBalance);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_investors')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_investors ON pos_tbl_authorities_dashboard(investors);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_investorPaybook')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_investorPaybook ON pos_tbl_authorities_dashboard(investorPaybook);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_guarantors')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_guarantors ON pos_tbl_authorities_dashboard(guarantors);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_aboutLicense')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_aboutLicense ON pos_tbl_authorities_dashboard(aboutLicense);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_EmployeeSalaries')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_EmployeeSalaries ON pos_tbl_authorities_dashboard(EmployeeSalaries);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_bankLoan')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_bankLoan ON pos_tbl_authorities_dashboard(bankLoan);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_bankLoanPaybook')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_bankLoanPaybook ON pos_tbl_authorities_dashboard(bankLoanPaybook);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_supplierPaybook')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_supplierPaybook ON pos_tbl_authorities_dashboard(supplierPaybook);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_charity')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_charity ON pos_tbl_authorities_dashboard(charity);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_tbl_Dashboard_role_id')
    CREATE NONCLUSTERED INDEX IX_tbl_Dashboard_role_id ON pos_tbl_authorities_dashboard(role_id);
