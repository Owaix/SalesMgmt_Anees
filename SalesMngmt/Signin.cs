using Lib.Entity;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;

namespace SalesMngmt
{
    public partial class Signin : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        private string trialFile = "trialinfo.txt"; // local file storage
        private DateTime startDate = DateTime.Now.AddMonths(-4);
        private bool isActivated = false;
        public Signin()
        {
            db = new SaleManagerEntities();
            InitializeComponent();

            if (!File.Exists(trialFile))
            {
                // First run → set start date
                startDate = DateTime.Now;
                File.WriteAllText(trialFile, startDate.ToString());
            }
            else
            {
                var content = File.ReadAllText(trialFile);
                if (content.StartsWith("ACTIVATED"))
                {
                    isActivated = true;
                    return;
                }
                startDate = DateTime.Parse(content);
            }
        }

        private void Signin_Load(object sender, EventArgs e)
        {

        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            int daysUsed = (DateTime.Now - startDate).Days;
            if (daysUsed > 30 && !isActivated)
            {
                using (var prompt = new PasswordPrompt())
                {
                    if (prompt.ShowDialog() == DialogResult.OK)
                    {
                        string input = prompt.PasswordText;

                        if (input == "temppass")
                        {
                            startDate = DateTime.Now;
                            SaveTrialInfo();
                            MessageBox.Show("Trial extended for 30 days.");
                        }
                        else if (input == "permpass")
                        {
                            File.WriteAllText(trialFile, "ACTIVATED");
                            isActivated = true;
                            MessageBox.Show("Software permanently activated!");
                        }
                        else
                        {
                            MessageBox.Show("Invalid password. Closing app.");
                            Application.Exit();
                        }
                    }
                }
            }
            else
            {
                try
                {
                    String UsrName = metroTextBox1.Text.Trim();
                    String Pass = metroTextBox2.Text.Trim();
                    var user = db.AspNetUsers.Where(x => x.UserName == UsrName && x.PasswordHash == Pass).FirstOrDefault();
                    if (user != null)
                    {
                        String DestPath = @"D:\Backup";
                        string backupfileName = $"{DateTime.Now:yyyy-MM-dd}.bak";
                        string fullPath = Path.Combine(DestPath, backupfileName);
                        if (!File.Exists(fullPath))
                        {
                            BackupDb backupDb = new BackupDb();
                            backupDb.backupdata(DestPath, backupfileName);
                        }
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

        private void SaveTrialInfo()
        {
            File.WriteAllText(trialFile, startDate.ToString());
        }
    }
}
