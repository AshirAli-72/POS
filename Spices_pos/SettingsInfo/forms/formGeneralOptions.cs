using System;
using System.Windows.Forms;
using Settings_info.controllers;
using Datalayer;
using Message_box_info.forms;
using Spices_pos.DatabaseInfo.DatalayerInfo.ReferenceClasses;
using System.Drawing.Printing;
using RefereningMaterial;
using System.Diagnostics;
using Spices_pos.DatabaseInfo.WebConfig;

namespace Settings_info.forms
{
    public partial class formGeneralOptions : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED       
                return handleParam;
            }
        }

        ClassShowGridViewData GetSetData = new ClassShowGridViewData(webConfig.con_string);
        Datalayers data = new Datalayers(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();
        public static int role_id = 0;

        public formGeneralOptions()
        {
            InitializeComponent();
            //setFormColorsDynamically();
        }

        //private void setFormColorsDynamically()
        //{
        //    //try
        //    //{
        //    //    int dark_red = data.UserPermissionsIds("dark_red", "pos_colors_settings");
        //    //    int dark_green = data.UserPermissionsIds("dark_green", "pos_colors_settings");
        //    //    int dark_blue = data.UserPermissionsIds("dark_blue", "pos_colors_settings");

        //    //    int back_red = data.UserPermissionsIds("back_red", "pos_colors_settings");
        //    //    int back_green = data.UserPermissionsIds("back_green", "pos_colors_settings");
        //    //    int back_blue = data.UserPermissionsIds("back_blue", "pos_colors_settings");

        //    //    int fore_red = data.UserPermissionsIds("fore_red", "pos_colors_settings");
        //    //    int fore_green = data.UserPermissionsIds("fore_green", "pos_colors_settings");
        //    //    int fore_blue = data.UserPermissionsIds("fore_blue", "pos_colors_settings");

        //    //    //****************************************************************

        //    //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel1, FormNamelabel);
        //    //    GetSetData.setFormColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, panel4, lblCopyrights);

        //    //    //****************************************************************

        //    //    GetSetData.setGunaUIButonColors(back_red, back_green, back_blue, fore_red, fore_green, fore_blue, dark_red, dark_green, dark_blue, Closebutton);
        //    //}
        //    //catch (Exception es)
        //    //{
        //    //    MessageBox.Show(es.Message);
        //    //}
        //}

        private void formGeneralOptions_Load(object sender, EventArgs e)
        {
            GetSetData.addFormCopyrights(lblCopyrights);
            fill_general_setting_details();
            txtPrinterName.Select();
        }

        private void formGeneralOptions_Shown(object sender, EventArgs e)
        {
            try
            {
                fill_general_setting_details();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            settings.role_id = role_id;
            settings reg = new settings();
            reg.Show();

            this.Dispose();
        }

        private void fill_general_setting_details()
        {
            try
            {
                TextData.backup_path = data.UserPermissions("backup_path", "pos_general_settings");
                TextData.pic_path = data.UserPermissions("picture_path", "pos_general_settings");
                TextData.auto_backup = data.UserPermissions("auto_backup", "pos_general_settings");
                TextData.show_graph = data.UserPermissions("show_graphs", "pos_general_settings");
                TextData.page_size = data.UserPermissions("page_size", "pos_general_settings");
                TextData.pos_security = data.UserPermissions("pos_security", "pos_general_settings");
                TextData.auto_expiry = data.UserPermissions("auto_expiry", "pos_general_settings");
                TextData.box_notifications = data.UserPermissions("show_notifications", "pos_general_settings");
                TextData.show_discount = data.UserPermissions("show_discount", "pos_general_settings");
                TextData.box_discount = data.UserPermissions("discount_box", "pos_general_settings");
                TextData.show_price = data.UserPermissions("price_box", "pos_general_settings");
                TextData.show_tax = data.UserPermissions("tax_box", "pos_general_settings");
                TextData.show_hold = data.UserPermissions("show_hold", "pos_general_settings");
                TextData.show_unhold = data.UserPermissions("show_unhold", "pos_general_settings");
                TextData.show_order = data.UserPermissions("show_order", "pos_general_settings");
                TextData.show_last_order = data.UserPermissions("show_last_order", "pos_general_settings");
                TextData.show_remarks = data.UserPermissions("show_remarks", "pos_general_settings");
                TextData.show_recovery = data.UserPermissions("show_recovery", "pos_general_settings");
                TextData.show_print_bill = data.UserPermissions("show_print_receipt", "pos_general_settings");
                TextData.directly_print = data.UserPermissions("directly_print_receipt", "pos_general_settings");
                TextData.sale_df_option = data.UserPermissions("sale_default_option", "pos_general_settings");
                TextData.DiscountPercentage = data.UserPermissions("discountType", "pos_general_settings");
                TextData.autoPenalties = data.UserPermissions("autoPenalties", "pos_general_settings");
                TextData.useCapitalAmount = data.UserPermissions("useCapital", "pos_general_settings");
                TextData.show_guarantors = data.UserPermissions("show_guarantors", "pos_general_settings");
                TextData.show_installmentPlan = data.UserPermissions("show_installmentPlan", "pos_general_settings");
                TextData.showNoteInReport = data.UserPermissions("showNoteInReport", "pos_general_settings");
                TextData.investorProfit = data.UserPermissions("investorProfit", "pos_general_settings");
                TextData.lessInvestorAmount = data.UserPermissions("lessAmount", "pos_general_settings");
                TextData.profitDivide = data.UserPermissions("profitDivide", "pos_general_settings");
                TextData.employeeSalary = data.UserPermissions("employeeSalary", "pos_general_settings");
                TextData.CompanyProfit = data.UserPermissions("companyProfit", "pos_general_settings");
                TextData.default_printer = data.UserPermissions("default_printer", "pos_general_settings");
                TextData.preview_receipt = data.UserPermissions("preview_receipt", "pos_general_settings");
                TextData.currency = data.UserPermissions("currency", "pos_general_settings");
                TextData.taxation = data.UserPermissions("taxation", "pos_general_settings");
                TextData.searchBox = data.UserPermissions("search_box", "pos_general_settings");
                TextData.printerModel = data.UserPermissions("printer_model", "pos_general_settings");
                TextData.autoOpenCashDrawer = data.UserPermissions("auto_open_cash_drawer", "pos_general_settings");
                TextData.strictly_check_expiry = data.UserPermissions("strictly_check_expiry", "pos_general_settings");

                TextData.autoSetPoints = data.UserPermissions("autoSetPoints", "pos_general_settings");
                TextData.autoRedeemPoints = data.UserPermissions("autoRedeemPoints", "pos_general_settings");
                TextData.addPointPerAmount = data.UserPermissions("addPointPerAmount", "pos_general_settings");
                TextData.pointsRedeemLimit = data.UserPermissions("pointsRedeemLimit", "pos_general_settings");
                TextData.autoPromotions = data.UserPermissions("autoPromotions", "pos_general_settings");
                TextData.promotionOnItems = data.UserPermissions("promotionOnItems", "pos_general_settings");
                TextData.promotionDiscount = data.UserPermissions("promotionDiscount", "pos_general_settings");
                TextData.auto_clear_cart = data.UserPermissions("auto_clear_cart", "pos_general_settings");
                TextData.split_old_and_new_stock = data.UserPermissions("split_old_and_new_stock", "pos_general_settings");
                TextData.pointsRedeemDiscount = data.UserPermissions("pointsRedeemDiscount", "pos_general_settings");
                TextData.allowAverageCost = data.UserPermissions("allowAverageCost", "pos_general_settings");
                TextData.singleAuthorityClosing = data.UserPermissions("singleAuthorityClosing", "pos_general_settings");
                TextData.autoZeroCustomerDiscount = data.UserPermissions("autoZeroCustomerDiscount", "pos_general_settings");
                TextData.batchOpeningAmount = data.UserPermissions("batchOpeningAmount", "pos_general_settings");
                TextData.setStockLimitToZero = data.UserPermissions("setStockLimitToZero", "pos_general_settings");
                string pointsDiscountInPercentage = data.UserPermissions("pointsDiscountInPercentage", "pos_general_settings");
                TextData.isCreditCardConnected = data.UserPermissions("isCreditCardConnected", "pos_general_settings");
                TextData.notificationSound = data.UserPermissions("notificationSound", "pos_general_settings");
                TextData.changeAmountPopUp = data.UserPermissions("changeAmountPopUp", "pos_general_settings");
                TextData.salesmanTips = data.UserPermissions("salesmanTips", "pos_general_settings");
                TextData.customerAgeLimit = data.UserPermissions("customerAgeLimit", "pos_general_settings");
                TextData.useSurcharges = data.UserPermissions("useSurcharges", "pos_general_settings");
                TextData.surchargePercentage = data.UserPermissions("surchargePercentage", "pos_general_settings");


                txt_backup_path.Text = TextData.backup_path;
                box_auto_backup.Text = TextData.auto_backup;
                txt_picture_path.Text = TextData.pic_path;
                box_show_graph.Text = TextData.show_graph;
                box_auto_expiry.Text = TextData.auto_expiry;
                box_page_size.Text = TextData.page_size;
                box_pos_security.Text = TextData.pos_security;
                box_notifications.Text = TextData.box_notifications;
                box_discount.Text = TextData.show_discount;
                box_open_discount.Text = TextData.box_discount;
                box_open_price.Text = TextData.show_price;
                box_open_tax.Text = TextData.show_tax;
                box_show_hold.Text = TextData.show_hold;
                box_show_unhold.Text = TextData.show_unhold;
                box_show_orders.Text = TextData.show_order;
                box_show_last_orders.Text = TextData.show_last_order;
                box_show_remarks.Text = TextData.show_remarks;
                box_show_recovery.Text = TextData.show_recovery;
                box_show_print_receipt.Text = TextData.show_print_bill;
                txt_directly_print.Text = TextData.directly_print;
                sale_df_option.Text = TextData.sale_df_option;
                txtDiscountPercentage.Text = TextData.DiscountPercentage;
                txtAutoPenalties.Text = TextData.autoPenalties;
                txtUseCapital.Text = TextData.useCapitalAmount;
                showBoxGuarantors.Text = TextData.show_guarantors;
                showInstallmentPlan.Text = TextData.show_installmentPlan;
                txtShowNoteInReport.Text = TextData.showNoteInReport;
                txtInvestorProfit.Text = TextData.investorProfit;
                txtLessAmount.Text = TextData.lessInvestorAmount;
                txtProfitDivide.Text = TextData.profitDivide;
                txtSalary.Text = TextData.employeeSalary;
                txtCompanyProfit.Text = TextData.CompanyProfit;
                txtPreviewReceipt.Text = TextData.preview_receipt;
                txtCurrency.Text = TextData.currency;
                txtTaxation.Text = TextData.taxation;
                txtSearchBox.Text = TextData.searchBox;
                txtPrinterModel.Text = TextData.printerModel;
                txtCashDrawerAutoOpen.Text = TextData.autoOpenCashDrawer;
                txtStrictlyCheckExpiry.Text = TextData.strictly_check_expiry;
                txtAutoSetPoints.Text = TextData.autoSetPoints;
                txtAutoRedeemPoints.Text = TextData.autoRedeemPoints;
                txtAddPointPerAmount.Text = TextData.addPointPerAmount;
                txtPointsRedeemLimit.Text = TextData.pointsRedeemLimit;
                txtAutoPromotions.Text = TextData.autoPromotions;
                txtPromotionOnItems.Text = TextData.promotionOnItems;
                txtPromotionDiscount.Text = TextData.promotionDiscount;
                txtAutoClearCart.Text = TextData.auto_clear_cart;
                txtAutoZeroCustomerDiscount.Text = TextData.autoZeroCustomerDiscount;
                txtSplitOldNewStock.Text = TextData.split_old_and_new_stock;
                txtAutoRedeemDiscount.Text = TextData.pointsRedeemDiscount;
                txtAllowAverageCost.Text = TextData.allowAverageCost;
                txtSingleAuthorityClosing.Text = TextData.singleAuthorityClosing;
                txtBatchOpeningAmount.Text = TextData.batchOpeningAmount;
                txtSetStockLimitToZero.Text = TextData.setStockLimitToZero;
                txtIsCreditCardConnected.Text = TextData.isCreditCardConnected;
                txtNotificationSound.Text = TextData.notificationSound;
                txtChangeAmountPopup.Text = TextData.changeAmountPopUp;
                txtSalesmanTips.Text = TextData.salesmanTips;
                txtCustomerAgeLimit.Text = TextData.customerAgeLimit;
                txtUseSurcharges.Text = TextData.useSurcharges;
                txtSurchargePercentage.Text = TextData.surchargePercentage;

                if (pointsDiscountInPercentage != "")
                {
                    chkPointsDiscountInPercentage.Checked = bool.Parse(pointsDiscountInPercentage);
                }
                else
                {
                    chkPointsDiscountInPercentage.Checked = false;
                }

                txtPrinterName.SelectedItem = TextData.default_printer;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void reset_general_setting_details()
        {
            try
            {
                txt_backup_path.Text = "";
                box_auto_backup.Text = "No";
                txt_picture_path.Text = "";
                box_show_graph.Text = "Yes";
                box_auto_expiry.Text = "No";
                box_page_size.Text = "A6";
                box_pos_security.Text = "Enabled";
                box_notifications.Text = "Enabled";
                box_discount.Text = "Enabled";
                box_open_discount.Text = "Yes";
                box_open_price.Text = "Yes";
                box_open_tax.Text = "No";
                box_show_hold.Text = "Yes";
                box_show_unhold.Text = "Yes";
                box_show_orders.Text = "Yes";
                box_show_last_orders.Text = "Yes";
                box_show_remarks.Text = "Yes";
                box_show_recovery.Text = "Yes";
                box_show_print_receipt.Text = "Yes";
                txt_directly_print.Text = "Yes";
                sale_df_option.Text = "Pack";
                txtDiscountPercentage.Text = "No";
                txtAutoPenalties.Text = "No";
                showInstallmentPlan.Text = "No";
                txtShowNoteInReport.Text = "No";
                txtInvestorProfit.Text = "W_Sale - Pur";
                txtCompanyProfit.Text = "Sale - Pur";
                txtLessAmount.Text = "5";
                txtProfitDivide.Text = "2";
                txtSalary.Text = "0";
                txtPrinterName.SelectedIndex = 0;
                txtPreviewReceipt.Text = "No";
                txtTaxation.Text = "No";
                txtCurrency.Text = "No";
                txtSearchBox.Text = "No";
                txtCashDrawerAutoOpen.Text = "No";
                txtStrictlyCheckExpiry.Text = "No";
                txtAutoSetPoints.Text = "No";
                txtAutoRedeemPoints.Text = "No";
                txtAddPointPerAmount.Text = "1";
                txtPointsRedeemLimit.Text = "100";
                txtAutoPromotions.Text = "No";
                txtAutoRedeemDiscount.Text = "0";
                txtPromotionOnItems.Text = "1";
                txtPromotionDiscount.Text = "1";
                txtAutoClearCart.Text = "Yes";
                txtAutoZeroCustomerDiscount.Text = "No";
                txtSplitOldNewStock.Text = "No";
                txtAllowAverageCost.Text = "No";
                txtSingleAuthorityClosing.Text = "No";
                txtSetStockLimitToZero.Text = "No";
                txtBatchOpeningAmount.Text = "0";
                txtIsCreditCardConnected.Text = "No";
                txtNotificationSound.Text = "No";
                txtChangeAmountPopup.Text = "No";
                txtSalesmanTips.Text = "No";
                txtUseSurcharges.Text = "No";
                txtCustomerAgeLimit.Text = "0";
                txtSurchargePercentage.Text = "0";

                chkPointsDiscountInPercentage.Checked = false;

                txtPrinterModel.SelectedIndex = 0;
            }
            catch (Exception es)
            {
                error.errorMessage(es.Message);
                error.ShowDialog();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TextData.backup_path = txt_backup_path.Text;
            TextData.auto_backup = box_auto_backup.Text;
            TextData.pic_path = txt_picture_path.Text;
            TextData.show_graph = box_show_graph.Text;
            TextData.auto_expiry = box_auto_expiry.Text;
            TextData.page_size = box_page_size.Text;
            TextData.pos_security = box_pos_security.Text;
            TextData.box_notifications = box_notifications.Text;
            TextData.show_discount = box_discount.Text;
            TextData.box_discount = box_open_discount.Text;
            TextData.show_price = box_open_price.Text;
            TextData.show_tax = box_open_tax.Text;
            TextData.show_hold = box_show_hold.Text;
            TextData.show_unhold = box_show_unhold.Text;
            TextData.show_order = box_show_orders.Text;
            TextData.show_last_order = box_show_last_orders.Text;
            TextData.show_remarks = box_show_remarks.Text;
            TextData.show_recovery = box_show_recovery.Text;
            TextData.show_print_bill = box_show_print_receipt.Text;
            TextData.directly_print = txt_directly_print.Text;
            TextData.sale_df_option = sale_df_option.Text;
            TextData.DiscountPercentage = txtDiscountPercentage.Text;
            TextData.autoPenalties = txtAutoPenalties.Text;
            TextData.useCapitalAmount = txtUseCapital.Text;
            TextData.show_guarantors = showBoxGuarantors.Text;
            TextData.show_installmentPlan = showInstallmentPlan.Text;
            TextData.showNoteInReport = txtShowNoteInReport.Text;
            TextData.investorProfit = txtInvestorProfit.Text;
            TextData.lessInvestorAmount = txtLessAmount.Text;
            TextData.profitDivide = txtProfitDivide.Text;
            TextData.employeeSalary = txtSalary.Text;
            TextData.CompanyProfit = txtCompanyProfit.Text;
            TextData.default_printer = txtPrinterName.Text;
            TextData.strictly_check_expiry = txtStrictlyCheckExpiry.Text;
            TextData.preview_receipt = txtPreviewReceipt.Text;      
            TextData.currency = txtCurrency.Text;
            TextData.taxation= txtTaxation.Text;
            TextData.printerModel = txtPrinterModel.Text;
            TextData.searchBox = txtSearchBox.Text;
            TextData.autoOpenCashDrawer = txtCashDrawerAutoOpen.Text;
            TextData.autoSetPoints = txtAutoSetPoints.Text;
            TextData.autoRedeemPoints = txtAutoRedeemPoints.Text;
            TextData.addPointPerAmount = txtAddPointPerAmount.Text;
            TextData.pointsRedeemLimit = txtPointsRedeemLimit.Text;
            TextData.autoPromotions = txtAutoPromotions.Text;
            TextData.pointsRedeemDiscount = txtAutoRedeemDiscount.Text;
            TextData.promotionOnItems = txtPromotionOnItems.Text;
            TextData.promotionDiscount = txtPromotionDiscount.Text;
            TextData.auto_clear_cart = txtAutoClearCart.Text;
            TextData.autoZeroCustomerDiscount = txtAutoZeroCustomerDiscount.Text;
            TextData.split_old_and_new_stock = txtSplitOldNewStock.Text;
            TextData.allowAverageCost = txtAllowAverageCost.Text;
            TextData.singleAuthorityClosing = txtSingleAuthorityClosing.Text;
            TextData.batchOpeningAmount = txtBatchOpeningAmount.Text;
            TextData.setStockLimitToZero = txtSetStockLimitToZero.Text;
            TextData.isCreditCardConnected = txtIsCreditCardConnected.Text;
            TextData.notificationSound = txtNotificationSound.Text;
            TextData.changeAmountPopUp = txtChangeAmountPopup.Text;
            TextData.salesmanTips = txtSalesmanTips.Text;
            TextData.useSurcharges = txtUseSurcharges.Text;
            TextData.surchargePercentage = txtSurchargePercentage.Text;
            TextData.customerAgeLimit = txtCustomerAgeLimit.Text;


            if (chkPointsDiscountInPercentage.Checked)
            {
                TextData.pointsDiscountInPercentage = true;
            }
            else
            {
                TextData.pointsDiscountInPercentage = false;
            }


            if (txtLessAmount.Text != "")
            {
                if (txtProfitDivide.Text != "")
                {
                    if (txtSalary.Text != "")
                    {
                        TextData.lessInvestorAmount = txtLessAmount.Text;
                        TextData.profitDivide = txtProfitDivide.Text;
                        TextData.employeeSalary = txtSalary.Text;

                        if (double.Parse(TextData.lessInvestorAmount) <= 0)
                        {
                            TextData.lessInvestorAmount = "1";
                        }

                        if (double.Parse(TextData.profitDivide) <= 0)
                        {
                            TextData.profitDivide = "1";
                        }

                        //GetSetData.SaveLogHistoryDetails("Settings Form", "Saving/Updating general settings...", role_id);
                        buttonControls.insert_general_settings();
                    }
                    else
                    {
                        error.errorMessage("Please enter employee salary field!");
                        error.ShowDialog();
                    }
                }
                else
                {
                    error.errorMessage("Please enter profit divide by field!");
                    error.ShowDialog();
                }
            }
            else
            {
                error.errorMessage("Please enter less amount field!");
                error.ShowDialog();
            }
        }

        private void btn_reset(object sender, EventArgs e)
        {
            reset_general_setting_details();
        }

        private void txtLessAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtLessAmount.Text, e);
        }

        private void txtProfitDivide_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtProfitDivide.Text, e);
        }

        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            data.NumericValuesOnly(txtSalary.Text, e);
        }

        private void txtKitchenTitle_Enter(object sender, EventArgs e)
        {
            //GetSetData.FillComboBoxUsingProcedures(txtKitchenTitle, "fillComboBoxKitchen", "title");
        }

        private void txtPrinterName_Enter(object sender, EventArgs e)
        {
            try
            {
                txtPrinterName.Items.Clear();
                PrintDocument prtdoc = new PrintDocument();
                string strDefaultPrinter = prtdoc.PrinterSettings.PrinterName;  

                foreach (String strPrinter in PrinterSettings.InstalledPrinters)
                {
                    txtPrinterName.Items.Add(strPrinter);

                    if (strPrinter == strDefaultPrinter)
                    {
                        txtPrinterName.SelectedIndex = txtPrinterName.Items.IndexOf(strPrinter);
                    }
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btnSavePrinters_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    int kitchen_id = data.UserPermissionsIds("kitchen_id", "pos_kitchen", "title", txtKitchenTitle.Text);

            //    GetSetData.query = @"select printer_id from pos_kitchen_printers where (kitchen_id = '" + kitchen_id.ToString() + "');";
            //    int printer_id = data.Select_ID_for_Foreign_Key_from_db_for_Insertion(GetSetData.query);

            //    if (printer_id == 0)
            //    {
            //        GetSetData.query = @"insert into pos_kitchen_printers values ('" + txtPrinterName.Text + "' , '" + kitchen_id.ToString() + "');";
            //        data.insertUpdateCreateOrDelete(GetSetData.query);
            //    }
            //    else
            //    {
            //        GetSetData.query = @"update pos_kitchen_printers set printer_name = '" + txtPrinterName.Text + "' ,  kitchen_id = '" + kitchen_id.ToString() + "' where (printer_id = '" + printer_id.ToString() +"');";
            //        data.insertUpdateCreateOrDelete(GetSetData.query);
            //    }

            //    done.DoneMessage("Printer Saved Successfully.");
            //    done.ShowDialog();
            //}
            //catch (Exception es)
            //{
            //    MessageBox.Show(es.Message);
            //}
        }

        private void formGeneralOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void txt_backup_path_Click(object sender, EventArgs e)
        {
            Process.Start("tabtip.exe");
        }
    }
}
