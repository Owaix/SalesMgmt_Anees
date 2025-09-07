using Lib;
using Lib.Entity;
using Lib.Model;
using Microsoft.Reporting.WinForms;
using SalesMngmt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace LabExpressDesktop.Reporting
{
    public partial class OrderDetailForm : Form
    {
        #region -- Global variables start --
        bool isNight { get; set; } = false;
        SaleManagerEntities db = null;
        #endregion -- Global variable end --
        AspNetUsers user = null;

        public OrderDetailForm(AspNetUsers Usr)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            user = Usr;
        }

        private IEnumerable<Dict> GetPaymentType()
        {
            List<Dict> dict = new List<Dict>();
            dict.Add(new Dict { key = 0, Value = "All" });
            dict.Add(new Dict { key = 1, Value = "Cash" });
            dict.Add(new Dict { key = 3, Value = "Card" });
            dict.Add(new Dict { key = 2, Value = "Void" });
            return dict;
        }
        private void BookingSummary_Load(object sender, EventArgs e)
        {
            fromDate.Value = DateTime.Today;
            endDate.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
            PopulateUsers();
            getuserTime();
            FillCombo<Dict>(comboBox1, GetPaymentType(), "Value", "Key");
            //    this.rptBookingSummary.RefreshReport();
        }

        public void FillCombo<T>(ComboBox comboBox, IEnumerable<T> obj, String Name, String ID, int selected = 0)
        {
            if (obj.Count() > 0)
            {
                comboBox.DataSource = obj;
                comboBox.DisplayMember = Name; // Column Name
                comboBox.ValueMember = ID;  // Column Name
                comboBox.SelectedIndex = selected;
            }
        }
        List<DetailReportModel> list = null;
        private void btnRun_Click(object sender, EventArgs e)
        {
            DateTime dtStart = DateTime.Now;
            DateTime dtEnd = DateTime.Now;
            dtStart = fromDate.Value;
            dtEnd = endDate.Value;

            rptBookingSummary.LocalReport.DataSources.Clear();
            list = Lib.Reporting.Reports.BookingDetailSummary(dtStart, dtEnd, ddlUsers.Text, comboBox1.SelectedValue.ToString());
            List<CompanyModel> list2 = new List<CompanyModel>();
            list2.Add(new CompanyModel { CompanyTitle = new Companies().GetCompanyID(user.AccessFailedCount).CompanyTitle });
            rptBookingSummary.LocalReport.DataSources.Add(new ReportDataSource("Ds", list));
            rptBookingSummary.LocalReport.DataSources.Add(new ReportDataSource("Ds2", list2));
            this.rptBookingSummary.RefreshReport();
        }


        #region -- Helper Method Start --

        private void PopulateUsers()
        {
            var ul = db.AspNetUsers.ToList();
            if (ul.Count > 0)
            {
                AspNetUsers objusers = new AspNetUsers();
                objusers.Id = "0";
                objusers.UserName = "all";
                ul.Add(objusers);
                ddlUsers.DisplayMember = "username";
                ddlUsers.ValueMember = "userid";
                ddlUsers.DataSource = ul.OrderByDescending(x => x.UserName == "all").ThenBy(x => x.UserName).ToList();
            }
            else { ddlUsers.DataSource = null; }
        }

        public void getuserTime()
        {
        }
        #endregion -- Helper Method End --

        private void OrderSummaryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            Main config = new Main(0, user);
            config.Show();
        }
    }
}
