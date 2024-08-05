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
using Spices_pos.DatabaseInfo.DatalayerInfo.JsonFiles;

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

        GeneralSettingsManager generalSettings = new GeneralSettingsManager(webConfig.con_string);
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
        //    //    int dark_red = generalSettings.ReadFieldIds("dark_red", "pos_colors_settings");
        //    //    int dark_green = generalSettings.ReadFieldIds("dark_green", "pos_colors_settings");
        //    //    int dark_blue = generalSettings.ReadFieldIds("dark_blue", "pos_colors_settings");

        //    //    int back_red = generalSettings.ReadFieldIds("back_red", "pos_colors_settings");
        //    //    int back_green = generalSettings.ReadFieldIds("back_green", "pos_colors_settings");
        //    //    int back_blue = generalSettings.ReadFieldIds("back_blue", "pos_colors_settings");

        //    //    int fore_red = generalSettings.ReadFieldIds("fore_red", "pos_colors_settings");
        //    //    int fore_green = generalSettings.ReadFieldIds("fore_green", "pos_colors_settings");
        //    //    int fore_blue = generalSettings.ReadFieldIds("fore_blue", "pos_colors_settings");

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
                TextData.backup_path = generalSettings.ReadField("backup_path");
                TextData.pic_path = generalSettings.ReadField("picture_path");
                TextData.auto_backup = generalSettings.ReadField("auto_backup");
                TextData.show_graph = generalSettings.ReadField("show_graphs");
                TextData.page_size = generalSettings.ReadField("page_size");
                TextData.pos_security = generalSettings.ReadField("pos_security");
                TextData.auto_expiry = generalSettings.ReadField("auto_expiry");
                TextData.box_notifications = generalSettings.ReadField("show_notifications");
                TextData.show_discount = generalSettings.ReadField("show_discount");
                TextData.box_discount = generalSettings.ReadField("discount_box");
                TextData.show_price = generalSettings.ReadField("price_box");
                TextData.show_tax = generalSettings.ReadField("tax_box");
                TextData.show_hold = generalSettings.ReadField("show_hold");
                TextData.show_unhold = generalSettings.ReadField("show_unhold");
                TextData.show_order = generalSettings.ReadField("show_order");
                TextData.show_last_order = generalSettings.ReadField("show_last_order");
                TextData.show_remarks = generalSettings.ReadField("show_remarks");
                TextData.show_recovery = generalSettings.ReadField("show_recovery");
                TextData.show_print_bill = generalSettings.ReadField("show_print_receipt");
                TextData.directly_print = generalSettings.ReadField("directly_print_receipt");
                TextData.sale_df_option = generalSettings.ReadField("sale_default_option");
                TextData.DiscountPercentage = generalSettings.ReadField("discountType");
                TextData.autoPenalties = generalSettings.ReadField("autoPenalties");
                TextData.useCapitalAmount = generalSettings.ReadField("useCapital");
                TextData.show_guarantors = generalSettings.ReadField("show_guarantors");
                TextData.show_installmentPlan = generalSettings.ReadField("show_installmentPlan");
                TextData.showNoteInReport = generalSettings.ReadField("showNoteInReport");
                TextData.investorProfit = generalSettings.ReadField("investorProfit");
                TextData.lessInvestorAmount = generalSettings.ReadField("lessAmount");
                TextData.profitDivide = generalSettings.ReadField("profitDivide");
                TextData.employeeSalary = generalSettings.ReadField("employeeSalary");
                TextData.CompanyProfit = generalSettings.ReadField("companyProfit");
                TextData.default_printer = generalSettings.ReadField("default_printer");
                TextData.preview_receipt = generalSettings.ReadField("preview_receipt");
                TextData.currency = generalSettings.ReadField("currency");
                TextData.taxation = generalSettings.ReadField("taxation");
                TextData.searchBox = generalSettings.ReadField("search_box");
                TextData.printerModel = generalSettings.ReadField("printer_model");
                TextData.autoOpenCashDrawer = generalSettings.ReadField("auto_open_cash_drawer");
                TextData.strictly_check_expiry = generalSettings.ReadField("strictly_check_expiry");

                TextData.autoSetPoints = generalSettings.ReadField("autoSetPoints");
                TextData.autoRedeemPoints = generalSettings.ReadField("autoRedeemPoints");
                TextData.addPointPerAmount = generalSettings.ReadField("addPointPerAmount");
                TextData.pointsRedeemLimit = generalSettings.ReadField("pointsRedeemLimit");
                TextData.autoPromotions = generalSettings.ReadField("autoPromotions");
                TextData.promotionOnItems = generalSettings.ReadField("promotionOnItems");
                TextData.promotionDiscount = generalSettings.ReadField("promotionDiscount");
                TextData.auto_clear_cart = generalSettings.ReadField("auto_clear_cart");
                TextData.split_old_and_new_stock = generalSettings.ReadField("split_old_and_new_stock");
                TextData.pointsRedeemDiscount = generalSettings.ReadField("pointsRedeemDiscount");
                TextData.allowAverageCost = generalSettings.ReadField("allowAverageCost");
                TextData.singleAuthorityClosing = generalSettings.ReadField("singleAuthorityClosing");
                TextData.autoZeroCustomerDiscount = generalSettings.ReadField("autoZeroCustomerDiscount");
                TextData.batchOpeningAmount = generalSettings.ReadField("batchOpeningAmount");
                TextData.setStockLimitToZero = generalSettings.ReadField("setStockLimitToZero");
                string pointsDiscountInPercentage = generalSettings.ReadField("pointsDiscountInPercentage");
                TextData.isCreditCardConnected = generalSettings.ReadField("isCreditCardConnected");
                TextData.notificationSound = generalSettings.ReadField("notificationSound");
                TextData.changeAmountPopUp = generalSettings.ReadField("changeAmountPopUp");
                TextData.salesmanTips = generalSettings.ReadField("salesmanTips");
                TextData.customerAgeLimit = generalSettings.ReadField("customerAgeLimit");
                TextData.useSurcharges = generalSettings.ReadField("useSurcharges");
                TextData.surchargePercentage = generalSettings.ReadField("surchargePercentage");
                TextData.showShiftCurrency = generalSettings.ReadField("showShiftCurrency");


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
                txtShowShiftCurrency.Text = TextData.showShiftCurrency;

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
                txtShowShiftCurrency.Text = "No";
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
            TextData.showShiftCurrency = txtShowShiftCurrency.Text;


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
            //    int kitchen_id = generalSettings.ReadFieldIds("kitchen_id", "pos_kitchen", "title", txtKitchenTitle.Text);

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

        private void txtSingleAuthorityClosing_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSetData.query = "SELECT TOP 1 id FROM pos_clock_in where (status = '0' or status = '-1');";
            string clock_in_id = data.SearchStringValuesFromDb(GetSetData.query);

            if ((clock_in_id != "") && (txtSingleAuthorityClosing.Text != generalSettings.ReadField("singleAuthorityClosing")))
            {
                error.errorMessage("Please clock out all shifts first!");
                error.ShowDialog();

                txtSingleAuthorityClosing.Text = generalSettings.ReadField("singleAuthorityClosing");
            }
        }
    }
}
