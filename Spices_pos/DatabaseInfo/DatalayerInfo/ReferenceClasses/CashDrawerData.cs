using System.Drawing.Printing;
using System.Windows.Forms;

namespace DataModel.Cash_Drawer_Data_Classes
{
    public static class CashDrawerData
    {
        public static bool OpenDrawer(string PrinterName, string PrinterType)
        {
            using (PrintDialog pd = new PrintDialog())
            {
                pd.PrinterSettings = new PrinterSettings();

                if (PrinterType == "STAR")
                {
                    RawPrinterHelper.SendStringToPrinter(PrinterName,
                        System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { /*Convert.ToByte(Code)*/7 }));
                }
                else if (PrinterType == "EPSON")
                {
                    RawPrinterHelper.SendStringToPrinter(PrinterName,
                        System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, 112, 48, 55, 121 }));
                }
                else if (PrinterType == "POS-X")
                {
                    RawPrinterHelper.SendStringToPrinter(PrinterName,
                        System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, 112, 0, 25, 250 }));
                }
                else if (PrinterType == "POS-X2")
                {
                    RawPrinterHelper.SendStringToPrinter(PrinterName,
                        System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, 112, 0, 25 }));
                }
                else if (PrinterType == "Xprinter")
                {
                    RawPrinterHelper.SendStringToPrinter(PrinterName,
                        System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, 112, 0, 148, 49 }));
                } 
                else if (PrinterType == "Xprinter2")
                {
                    RawPrinterHelper.SendStringToPrinter(PrinterName,
                        System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, 112, 0, 25, 250 }));
                }
                else if (PrinterType == "Custom")
                {
                    RawPrinterHelper.SendStringToPrinter(PrinterName,
                        System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, 112, 0, 50, 250 }));
                }
                else
                {
                    RawPrinterHelper.SendStringToPrinter(PrinterName,
                        System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, 112, 48, 55, 121 }));
                }
            }
            return true;
        }
    }
}
