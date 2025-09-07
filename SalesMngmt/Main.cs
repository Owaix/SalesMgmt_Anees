using Lib.Entity;
using SalesMngmt.Invoice;
using System;
using System.Windows.Forms;

namespace SalesMngmt
{
    public partial class Main : MetroFramework.Forms.MetroForm
    {
        int cmpID = 0;
        int usrID = 0;
        AspNetUsers User;
        public Main(int CompayId, AspNetUsers _user)
        {
            InitializeComponent();
            cmpID = CompayId;
            User = _user;
            usrID = Convert.ToInt32(_user.Id);
            label2.Text = _user.UserName;
            if (_user.PhoneNumber == "2")
            {
                metroTile1.Visible = false;
                metroTile5.Visible = false;
                metroTile6.Visible = false;
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void metroPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            Config config = new Config(cmpID, User);
            config.Show();
        }

        private void metroTile5_Click(object sender, EventArgs e)
        {


            Invoices config = new Invoices(cmpID, User);
            config.Show();
        }

        private void metroTile7_Click(object sender, EventArgs e)
        {
            Report config = new Report(cmpID, User);
            config.Show();
        }

        private void metroTile6_Click(object sender, EventArgs e)
        {

            Configs.Customer config = new Configs.Customer(cmpID);
            config.Show();
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            Signin signin = new Signin();
            signin.Show();
            this.Dispose();
        }

        private void metroTile9_Click(object sender, EventArgs e)
        {
            Pos inv = new Pos(User, cmpID);
            //  inv.MdiParent = this;
            inv.Show();
        }
    }
}
