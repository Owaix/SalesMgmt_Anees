using Lib.Entity;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SalesMngmt
{
    public partial class Signin : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        public Signin()
        {
            db = new SaleManagerEntities();
            InitializeComponent();
        }

        private void Signin_Load(object sender, EventArgs e)
        {

        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            //
            try
            {
                String UsrName = metroTextBox1.Text.Trim();
                String Pass = metroTextBox2.Text.Trim();
                var user = db.AspNetUsers.Where(x => x.UserName == UsrName && x.PasswordHash == Pass).FirstOrDefault();
                if (user != null)
                {
                    this.Hide();
                    SingleMain main = new SingleMain(user.AccessFailedCount, user);
                    main.Show();
                }
                else
                {
                    MessageBox.Show("UserName And Password Is InCorrect", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Enter))
            {
                if (metroTextBox2.Text.Trim() != "")
                {
                    metroButton1_Click(null, null);
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
