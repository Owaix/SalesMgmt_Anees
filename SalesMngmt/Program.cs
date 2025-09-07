using System;
using System.Windows.Forms;
using Lib.Model;
using SalesMngmt.Configs;
using SalesMngmt.Invoice;
using SalesMngmt.Reporting;

namespace SalesMngmt
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //  Application.Run(new labINventory(1024));
              Application.Run(new Signin() );

           // Application.Run(new AVERAGEITEM());
            //  Application.Run(new migrate_data());
            // Application.Run(new Form1());

            // Application.Run(new Bike_Sale_Purchase());

        }
    }
}