using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Spices_pos.DatabaseInfo.DatalayerInfo.JsonFiles
{
    public class GeneralSettingsManager
    {
        private readonly string baseDirectory;
        private readonly string jsonFilesFolder;
        private readonly string jsonFilePath;
        public string connectionString;

        public GeneralSettingsManager(string connectionString)
        {
            this.connectionString = connectionString;
            baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            jsonFilesFolder = Path.Combine(baseDirectory, "JsonFiles");
            jsonFilePath = Path.Combine(jsonFilesFolder, "general_settings.json"); // Updated file name
        }

        public void CreateOrUpdateJsonFile()
        {
            var data = FetchDataFromDatabase();
            File.WriteAllText(jsonFilePath, JsonConvert.SerializeObject(data, Formatting.Indented));
        }


        private JArray FetchDataFromDatabase()
        {
            string query = "SELECT * FROM pos_general_settings";

            using (var connection = new SqlConnection(this.connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();

                var reader = command.ExecuteReader();
                var result = new JArray();

                while (reader.Read())
                {
                    var item = new JObject
                    {
                        ["id"] = reader["id"] != DBNull.Value ? (int)reader["id"] : default(int),
                        ["backup_path"] = reader["backup_path"] != DBNull.Value ? reader["backup_path"].ToString() : string.Empty,
                        ["picture_path"] = reader["picture_path"] != DBNull.Value ? reader["picture_path"].ToString() : string.Empty,
                        ["auto_backup"] = reader["auto_backup"] != DBNull.Value ? reader["auto_backup"].ToString() : string.Empty,
                        ["show_graphs"] = reader["show_graphs"] != DBNull.Value ? reader["show_graphs"].ToString() : string.Empty,
                        ["page_size"] = reader["page_size"] != DBNull.Value ? reader["page_size"].ToString() : string.Empty,
                        ["pos_security"] = reader["pos_security"] != DBNull.Value ? reader["pos_security"].ToString() : string.Empty,
                        ["auto_expiry"] = reader["auto_expiry"] != DBNull.Value ? reader["auto_expiry"].ToString() : string.Empty,
                        ["show_notifications"] = reader["show_notifications"] != DBNull.Value ? reader["show_notifications"].ToString() : string.Empty,
                        ["show_discount"] = reader["show_discount"] != DBNull.Value ? reader["show_discount"].ToString() : string.Empty,
                        ["discount_box"] = reader["discount_box"] != DBNull.Value ? reader["discount_box"].ToString() : string.Empty,
                        ["price_box"] = reader["price_box"] != DBNull.Value ? reader["price_box"].ToString() : string.Empty,
                        ["tax_box"] = reader["tax_box"] != DBNull.Value ? reader["tax_box"].ToString() : string.Empty,
                        ["show_hold"] = reader["show_hold"] != DBNull.Value ? reader["show_hold"].ToString() : string.Empty,
                        ["show_unhold"] = reader["show_unhold"] != DBNull.Value ? reader["show_unhold"].ToString() : string.Empty,
                        ["show_order"] = reader["show_order"] != DBNull.Value ? reader["show_order"].ToString() : string.Empty,
                        ["show_last_order"] = reader["show_last_order"] != DBNull.Value ? reader["show_last_order"].ToString() : string.Empty,
                        ["show_remarks"] = reader["show_remarks"] != DBNull.Value ? reader["show_remarks"].ToString() : string.Empty,
                        ["show_recovery"] = reader["show_recovery"] != DBNull.Value ? reader["show_recovery"].ToString() : string.Empty,
                        ["show_print_receipt"] = reader["show_print_receipt"] != DBNull.Value ? reader["show_print_receipt"].ToString() : string.Empty,
                        ["directly_print_receipt"] = reader["directly_print_receipt"] != DBNull.Value ? reader["directly_print_receipt"].ToString() : string.Empty,
                        ["sale_default_option"] = reader["sale_default_option"] != DBNull.Value ? reader["sale_default_option"].ToString() : string.Empty,
                        ["discountType"] = reader["discountType"] != DBNull.Value ? reader["discountType"].ToString() : string.Empty,
                        ["autoPenalties"] = reader["autoPenalties"] != DBNull.Value ? reader["autoPenalties"].ToString() : string.Empty,
                        ["useCapital"] = reader["useCapital"] != DBNull.Value ? reader["useCapital"].ToString() : string.Empty,
                        ["show_guarantors"] = reader["show_guarantors"] != DBNull.Value ? reader["show_guarantors"].ToString() : string.Empty,
                        ["show_installmentPlan"] = reader["show_installmentPlan"] != DBNull.Value ? reader["show_installmentPlan"].ToString() : string.Empty,
                        ["showNoteInReport"] = reader["showNoteInReport"] != DBNull.Value ? reader["showNoteInReport"].ToString() : string.Empty,
                        ["investorProfit"] = reader["investorProfit"] != DBNull.Value ? reader["investorProfit"].ToString() : string.Empty,
                        ["lessAmount"] = reader["lessAmount"] != DBNull.Value ? reader["lessAmount"].ToString() : string.Empty,
                        ["profitDivide"] = reader["profitDivide"] != DBNull.Value ? reader["profitDivide"].ToString() : string.Empty,
                        ["employeeSalary"] = reader["employeeSalary"] != DBNull.Value ? reader["employeeSalary"].ToString() : string.Empty,
                        ["companyProfit"] = reader["companyProfit"] != DBNull.Value ? reader["companyProfit"].ToString() : string.Empty,
                        ["currency"] = reader["currency"] != DBNull.Value ? reader["currency"].ToString() : string.Empty,
                        ["taxation"] = reader["taxation"] != DBNull.Value ? reader["taxation"].ToString() : string.Empty,
                        ["default_printer"] = reader["default_printer"] != DBNull.Value ? reader["default_printer"].ToString() : string.Empty,
                        ["preview_receipt"] = reader["preview_receipt"] != DBNull.Value ? reader["preview_receipt"].ToString() : string.Empty,
                        ["printer_model"] = reader["printer_model"] != DBNull.Value ? reader["printer_model"].ToString() : string.Empty,
                        ["search_box"] = reader["search_box"] != DBNull.Value ? reader["search_box"].ToString() : string.Empty,
                        ["auto_open_cash_drawer"] = reader["auto_open_cash_drawer"] != DBNull.Value ? reader["auto_open_cash_drawer"].ToString() : string.Empty,
                        ["strictly_check_expiry"] = reader["strictly_check_expiry"] != DBNull.Value ? reader["strictly_check_expiry"].ToString() : string.Empty,
                        ["autoSetPoints"] = reader["autoSetPoints"] != DBNull.Value ? reader["autoSetPoints"].ToString() : string.Empty,
                        ["autoRedeemPoints"] = reader["autoRedeemPoints"] != DBNull.Value ? reader["autoRedeemPoints"].ToString() : string.Empty,
                        ["addPointPerAmount"] = reader["addPointPerAmount"] != DBNull.Value ? reader["addPointPerAmount"].ToString() : string.Empty,
                        ["pointsRedeemLimit"] = reader["pointsRedeemLimit"] != DBNull.Value ? reader["pointsRedeemLimit"].ToString() : string.Empty,
                        ["autoPromotions"] = reader["autoPromotions"] != DBNull.Value ? reader["autoPromotions"].ToString() : string.Empty,
                        ["promotionOnItems"] = reader["promotionOnItems"] != DBNull.Value ? reader["promotionOnItems"].ToString() : string.Empty,
                        ["promotionDiscount"] = reader["promotionDiscount"] != DBNull.Value ? reader["promotionDiscount"].ToString() : string.Empty,
                        ["auto_clear_cart"] = reader["auto_clear_cart"] != DBNull.Value ? reader["auto_clear_cart"].ToString() : string.Empty,
                        ["split_old_and_new_stock"] = reader["split_old_and_new_stock"] != DBNull.Value ? reader["split_old_and_new_stock"].ToString() : string.Empty,
                        ["pointsRedeemDiscount"] = reader["pointsRedeemDiscount"] != DBNull.Value ? reader["pointsRedeemDiscount"].ToString() : string.Empty,
                        ["pointsDiscountInPercentage"] = reader["pointsDiscountInPercentage"] != DBNull.Value ? reader["pointsDiscountInPercentage"].ToString() : string.Empty,
                        ["allowAverageCost"] = reader["allowAverageCost"] != DBNull.Value ? reader["allowAverageCost"].ToString() : string.Empty,
                        ["autoZeroCustomerDiscount"] = reader["autoZeroCustomerDiscount"] != DBNull.Value ? reader["autoZeroCustomerDiscount"].ToString() : string.Empty,
                        ["singleAuthorityClosing"] = reader["singleAuthorityClosing"] != DBNull.Value ? reader["singleAuthorityClosing"].ToString() : string.Empty,
                        ["batchOpeningAmount"] = reader["batchOpeningAmount"] != DBNull.Value ? reader["batchOpeningAmount"].ToString() : string.Empty,
                        ["setStockLimitToZero"] = reader["setStockLimitToZero"] != DBNull.Value ? reader["setStockLimitToZero"].ToString() : string.Empty,
                        ["isCreditCardConnected"] = reader["isCreditCardConnected"] != DBNull.Value ? reader["isCreditCardConnected"].ToString() : string.Empty,
                        ["notificationSound"] = reader["notificationSound"] != DBNull.Value ? reader["notificationSound"].ToString() : string.Empty,
                        ["changeAmountPopUp"] = reader["changeAmountPopUp"] != DBNull.Value ? reader["changeAmountPopUp"].ToString() : string.Empty,
                        ["customerAgeLimit"] = reader["customerAgeLimit"] != DBNull.Value ? reader["customerAgeLimit"].ToString() : string.Empty,
                        ["salesmanTips"] = reader["salesmanTips"] != DBNull.Value ? reader["salesmanTips"].ToString() : string.Empty,
                        ["useSurcharges"] = reader["useSurcharges"] != DBNull.Value ? reader["useSurcharges"].ToString() : string.Empty,
                        ["surchargePercentage"] = reader["surchargePercentage"] != DBNull.Value ? reader["surchargePercentage"].ToString() : string.Empty,
                    };
                    result.Add(item);
                }

                return result;
            }
        }

        public string ReadField(string fieldName)
        {
            if (File.Exists(jsonFilePath))
            {
                var jsonData = File.ReadAllText(jsonFilePath);

                var jsonArray = JArray.Parse(jsonData);

                if (jsonArray.Count > 0)
                {
                    var item = jsonArray[0]; // Assuming we only need the first object

                    if (item[fieldName] != null)
                    {
                        return item[fieldName].ToString();
                    }
                    else
                    {
                        return $"Field '{fieldName}' not found.";
                    }
                }
                else
                {
                    return "No items found in JSON file.";
                }
            }
            else
            {
                return "JSON file does not exist.";
            }
        }
    }
}
