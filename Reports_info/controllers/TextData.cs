using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Login_info.controllers
{
    public class TextData
    {
        public static string from_Date { get; set; }
        public static string to_Date { get; set; }
        public static string bill_No { get; set; }
        public static string customer_name { get; set; }
        public static string customer_code { get; set; }

        public static string date { get; set; }
        public static string remarks { get; set; }
        public static double debits { get; set; }
        public static double credits { get; set; }
        public static double balance { get; set; }
    }
}
