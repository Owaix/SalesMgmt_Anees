using System;
using System.Windows.Forms;

namespace SalesMngmt
{
    public partial class PasswordPrompt : Form
    {
        public string PasswordText => txtPassword.Text;
        public PasswordPrompt()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private void PasswordPrompt_Load(object sender, EventArgs e)
        {

        }
    }
}
