using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Datalayer;
using Login_info.controllers;
using Message_box_info.forms;
using WebConfig;
using Supplier_Chain_info.Reports;
using Microsoft.Reporting.WinForms;

namespace Supplier_Chain_info.Reports
{
    public partial class form_supplier_list : Form
    {
        public form_supplier_list()
        {
            InitializeComponent();
        }

        datalayer data = new datalayer(webConfig.con_string);
        error_form error = new error_form();
        done_form done = new done_form();

        public void Suppliers_list()
        {
            Supplier_list_ds report = new Supplier_list_ds();
            string quer_get_data_db = @"SELECT name, phone1, address, tbl_city.title as city, cnic, code, phone2, tbl_region.title as supplier_region, supplier_fax, email, comments, supplier_id 
                                        FROM Suppliers inner join tbl_city on Suppliers.city_id = tbl_city.city_id inner join tbl_region on Suppliers.region_id = tbl_region.region_id;";

            SqlConnection conn = new SqlConnection(webConfig.con_string);
            SqlDataAdapter da = new SqlDataAdapter(quer_get_data_db, conn);
            da.Fill(report, report.Tables[0].TableName);

            ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", report.Tables[0]);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(rds);
            this.reportViewer1.LocalReport.Refresh();

            this.reportViewer1.RefreshReport();
        }

        private void form_supplier_list_Load(object sender, EventArgs e)
        {
            Suppliers_list();
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
