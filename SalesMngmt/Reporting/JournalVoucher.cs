using Lib.Entity;
using Lib.Model;
using Lib.Reporting;
using Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesMngmt.Reporting
{
    public partial class JournalVoucher : MetroFramework.Forms.MetroForm
    {

        SaleManagerEntities db = null;
        List<COA_D> list = null;
        int compID = 0;
        int Rid = 0;

        public JournalVoucher(int cmpID)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            compID = cmpID;

        }


        public void LoadFuncaTion()
        {
            List<Article> article = new List<Article>();

            article.Add(new Article { ProductID = 0, ArticleNo = "--SELECT--" });
            article.AddRange(db.Article.ToList());
            var Account = db.COA_M.ToList();
            FillCombo(cmbxAccID, Account, "CATitle", "CAC_Code", 0);


            var CreditAccount = db.COA_M.ToList();
            FillCombo(cmbxCreditAccount, CreditAccount, "CATitle", "CAC_Code", 0);


            List<COA_D> vendor = new List<COA_D>();
            vendor.Add(new COA_D { CAC_Code = 0, AC_Title = "--SELECT--" });
            vendor.AddRange(db.COA_D.Where(x => x.CAC_Code == 1 && x.CompanyID==compID).ToList());


            FillCombo(cmbxvendor, vendor, "AC_Title", "AC_Code", 0);



            List<COA_D> Creditvendor = new List<COA_D>();
            Creditvendor.Add(new COA_D { CAC_Code = 0, AC_Title = "--SELECT--" });
            Creditvendor.AddRange(db.COA_D.Where(x => x.CAC_Code == 1 && x.CompanyID == compID).ToList());


            FillCombo(cmbxCreditVendor, Creditvendor, "AC_Title", "AC_Code", 0);



            txtAmount.Text = "";
            chkForSale.Checked = false;
            txtDesc.Text = "";

        }

        private void COA_Load(object sender, EventArgs e)
        {
            pnlMain.Hide();
            LoadFuncaTion();


        }
        public void FillCombo<T>(ComboBox comboBox, IEnumerable<T> obj, String Name, String ID, int selected = 0)
        {
            try
            {
                if (obj.Count() > 0)
                {
                    comboBox.DataSource = obj;
                    comboBox.DisplayMember = Name; // Column Name
                    comboBox.ValueMember = ID;  // Column Name
                    comboBox.SelectedIndex = selected;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblAdd_Click(object sender, EventArgs e)
        {
            //COABindingSource.AddNew();
            //GetDocCode("1");
            ////LoadFuncaTion();
            //txtCOA.Focus();
            //cmbxCOA.Enabled = true;


            LoadFuncaTion();
            label3.Text = "ADD";
            pnlMain.Show();

            string path = Application.StartupPath + "\\Img\\124444444.png";
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = Image.FromFile(path);


        }

        private void lblEdit_Click(object sender, EventArgs e)
        {
            if (Rid == 0)
            {
                MessageBox.Show("Please select any row first");
            }
            else
            {

                pnlMain.Show();

                label3.Text = "EDIT";

                var tbl = db.JV_M.AsNoTracking().Where(x => x.RID == Rid).FirstOrDefault();

                string path = Application.StartupPath + "\\Img\\" + tbl.img;
                //string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10)) + "\\Img\\" + obj.BarCode_ID;
                label10.Text = tbl.img;
                pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = Utillityfunctions.LoadImage(tbl.images);// Image.FromFile(path);

                chkForSale.Checked =Convert.ToBoolean( tbl.ForSale);
            }
        }

        #region -- Global variables start --

        string docCode;

        #endregion -- Global variable end --

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Rid = 0;
            pnlMain.Hide();
            LoadFuncaTion();
            //COA_D us = (COA_D)COABindingSource.Current;
            //if (us.AC_Code == 0)
            //{
            //    COABindingSource.RemoveCurrent();
            //}
            //else
            //{
            //   // cmbxCOA.Enabled = true;
            //    COABindingSource.Clear();
            //    var listcancel = db.COA_D.AsNoTracking().ToList();
            //    COABindingSource.DataSource = listcancel;
            //}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DbContextTransaction transaction = null;//db.Database.BeginTransaction();
            try
            {
                if (Rid == 0)
                {
                    transaction = db.Database.BeginTransaction();
                    JV_M journalVoucherM = new JV_M();
                    int ABC = db.JV_M.Select(x => x.RID).DefaultIfEmpty(0).Max();
                    journalVoucherM.RID = 1 + ABC;
                    journalVoucherM.EDate = dtEntryDate.Value;
                    journalVoucherM.CompID = compID;
                    journalVoucherM.SID = 1;
                    journalVoucherM.ForSale = chkForSale.Checked;


                    string path = Application.StartupPath;
                    // string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
                    string filename = System.IO.Path.GetFileName(openFileDialog1.FileName);

                    if (Rid == 0)
                    {
                        if (filename == null)
                        {
                            MessageBox.Show("Please select a valid image.");
                            return;
                        }
                        else
                        {
                            try
                            {
                                //System.IO.File.Copy(openFileDialog1.FileName, path + "\\Img\\" + filename);
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }
                    journalVoucherM.img = filename;
                    journalVoucherM.images = Utillityfunctions.ToBase64(openFileDialog1.FileName, path + "\\Img\\" + filename);
                    db.JV_M.Add(journalVoucherM);
                    db.SaveChanges();

                    JV_D journalVoucherD = new JV_D();

                    journalVoucherD.RID = journalVoucherM.RID;
                    journalVoucherD.Narr = txtDesc.Text;
                    journalVoucherD.AC_Code = Convert.ToInt32(cmbxvendor.SelectedValue);
                    journalVoucherD.AC_Code2 = Convert.ToInt32(cmbxCreditVendor.SelectedValue);
                    journalVoucherD.Amt = Convert.ToDouble(txtAmount.Text);
                    
                   
                    // journalVoucherD.co
                    db.JV_D.Add(journalVoucherD);
                    db.SaveChanges();


                    GL g1 = new GL();
                    g1.RID = journalVoucherM.RID;
                    g1.TypeCode = 17;
                    g1.AC_Code = Convert.ToInt32(cmbxvendor.SelectedValue);
                    g1.AC_Code2 = Convert.ToInt32(cmbxCreditVendor.SelectedValue);
                    g1.Debit = Convert.ToDouble(txtAmount.Text);
                    g1.Credit = 0;
                    g1.Narration = txtDesc.Text;
                    g1.GLDate = dtEntryDate.Value;
                    g1.CompID = compID;
                    db.GL.Add(g1);
                    db.SaveChanges();


                    GL g2 = new GL();
                    g2.RID = journalVoucherM.RID;
                    g2.TypeCode = 17;
                    g2.AC_Code = Convert.ToInt32(cmbxCreditVendor.SelectedValue);
                    g2.AC_Code2 = Convert.ToInt32(cmbxvendor.SelectedValue);
                    g2.Debit = 0;
                    g2.Credit = Convert.ToDouble(txtAmount.Text);
                    g2.GLDate = dtEntryDate.Value;
                    g2.Narration = txtDesc.Text;
                    g2.CompID = compID; 
                    db.GL.Add(g2);
                    db.SaveChanges();

                    transaction.Commit();
                    MessageBox.Show("Entry Save Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Rid = 0;
                    pnlMain.Hide();
                    LoadFuncaTion();
                }
                else
                {
                    transaction = db.Database.BeginTransaction();
                    var journalVoucherM = db.JV_M.SingleOrDefault(b => b.RID == Rid);
                    journalVoucherM.EDate = dtEntryDate.Value;
                    journalVoucherM.CompID =compID;
                    journalVoucherM.SID = 1;
                    journalVoucherM.ForSale = chkForSale.Checked;



                    string path = Application.StartupPath;
                    // string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
                    string filename = System.IO.Path.GetFileName(openFileDialog1.FileName);

                  if (filename == null)
                        {
                            MessageBox.Show("Please select a valid image.");
                            return;
                        }
                        else
                        {
                            try
                            {
                                //System.IO.File.Copy(openFileDialog1.FileName, path + "\\Img\\" + filename);
                            }
                            catch (Exception)
                            {

                            }
                        }


                    journalVoucherM.img = filename;
                    journalVoucherM.images = Utillityfunctions.ToBase64(openFileDialog1.FileName, path + "\\Img\\" + filename);
                    db.SaveChanges();


                    var journalVoucherD = db.JV_D.SingleOrDefault(b => b.RID == Rid);
                    journalVoucherD.Narr = txtDesc.Text;
                    journalVoucherD.AC_Code = Convert.ToInt32(cmbxvendor.SelectedValue);
                    journalVoucherD.AC_Code2 = Convert.ToInt32(cmbxCreditVendor.SelectedValue);
                    journalVoucherD.Amt = Convert.ToDouble(txtAmount.Text);

                    db.GL.RemoveRange(db.GL.Where(x => x.RID == Rid && x.TypeCode == 17));

                    db.SaveChanges();


                    GL g1 = new GL();
                    g1.RID = Rid;
                    g1.TypeCode = 17;
                    g1.AC_Code = Convert.ToInt32(cmbxvendor.SelectedValue);
                    g1.AC_Code2 = Convert.ToInt32(cmbxCreditVendor.SelectedValue);
                    g1.Debit = Convert.ToDouble(txtAmount.Text);
                    g1.Credit = 0;
                    g1.Narration = txtDesc.Text;
                    g1.CompID = compID;
                    g1.GLDate = dtEntryDate.Value;

                    db.GL.Add(g1);
                    db.SaveChanges();


                    GL g2 = new GL();
                    g2.RID = Rid;
                    g2.TypeCode = 17;
                    g2.AC_Code = Convert.ToInt32(cmbxCreditVendor.SelectedValue);
                    g2.AC_Code2 = Convert.ToInt32(cmbxvendor.SelectedValue);
                    g2.Debit = 0;
                    g2.Credit = Convert.ToDouble(txtAmount.Text);
                    g2.GLDate = dtEntryDate.Value;
                    g2.Narration = txtDesc.Text;
                    g2.CompID = compID;
                    db.GL.Add(g2);
                    db.SaveChanges();

                    transaction.Commit();
                    MessageBox.Show("Entry Save Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Rid = 0;
                    pnlMain.Hide();
                    LoadFuncaTion();

                }
                bindGrid();
            }

            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
        }
        private void cmbxAccID_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dsa = Convert.ToInt32(cmbxAccID.SelectedIndex);

            if (dsa >= 1)
            {


                int value = Convert.ToInt32(cmbxAccID.SelectedValue);

                if (value == 3)
                {


                    List<CustomerList> customerList = new List<CustomerList>();
                    customerList.Add(new CustomerList { AC_Code = 0, Name = "--SELECT--" });
                    var aa = ReportsController.getcustumerList(compID);

                    foreach (DataRow row in aa.Rows)
                    {
                        customerList.Add(new CustomerList { AC_Code = (int)row["AC_Code"], Name = row["Name"] + "/" + row["city"] });
                    }

                    FillCombo(cmbxvendor, customerList, "Name", "AC_Code", 0);
                }

                else if (value == 9)
                {

                    List<CustomerList> customerList = new List<CustomerList>();
                    customerList.Add(new CustomerList { AC_Code = 0, Name = "--SELECT--" });
                    var aa = ReportsController.getVendorList(compID);

                    foreach (DataRow row in aa.Rows)
                    {
                        customerList.Add(new CustomerList { AC_Code = (int)row["AC_Code"], Name = row["Name"] + "/" + row["city"] });
                    }

                    FillCombo(cmbxvendor, customerList, "Name", "AC_Code", 0);



                }
                else
                {
                    List<COA_D> d = new List<COA_D>();
                    d.Add(new COA_D { AC_Code = 0, AC_Title = "--Select--" });
                    d.AddRange(db.COA_D.Where(x => x.CAC_Code == value && x.CompanyID == compID && x.InActive == false).ToList());
                    FillCombo(cmbxvendor, d, "AC_Title", "AC_Code", 0);
                    //var vendor = db.COA_D.Where(x => x.CAC_Code == value).ToList();

                }



                //List<COA_D> vendor = new List<COA_D>();
                //vendor.Add(new COA_D { CAC_Code = 0, AC_Title = "--SELECT--" });
                //vendor.AddRange(db.COA_D.Where(x => x.CAC_Code == value && x.CompanyID==compID).ToList());


                //FillCombo(cmbxvendor, vendor, "AC_Title", "AC_Code", 0);
            }
            else if (dsa == 0)
            {

                List<COA_D> vendor = new List<COA_D>();
                vendor.Add(new COA_D { CAC_Code = 0, AC_Title = "--SELECT--" });
                vendor.AddRange(db.COA_D.Where(x => x.CAC_Code == 1 && x.CompanyID == compID).ToList());


                FillCombo(cmbxvendor, vendor, "AC_Title", "AC_Code", 0);

            }
        }

        private void cmbxCreditAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dsa = Convert.ToInt32(cmbxCreditAccount.SelectedIndex);

            if (dsa >= 1)
            {


                int value = Convert.ToInt32(cmbxCreditAccount.SelectedValue);

                if (value == 3)
                {


                    List<CustomerList> customerList = new List<CustomerList>();
                    customerList.Add(new CustomerList { AC_Code = 0, Name = "--SELECT--" });
                    var aa = ReportsController.getcustumerList(compID);

                    foreach (DataRow row in aa.Rows)
                    {
                        customerList.Add(new CustomerList { AC_Code = (int)row["AC_Code"], Name = row["Name"] + "/" + row["city"] });
                    }

                    FillCombo(cmbxCreditVendor, customerList, "Name", "AC_Code", 0);
                }

                else if (value == 9)
                {

                    List<CustomerList> customerList = new List<CustomerList>();
                    customerList.Add(new CustomerList { AC_Code = 0, Name = "--SELECT--" });
                    var aa = ReportsController.getVendorList(compID);

                    foreach (DataRow row in aa.Rows)
                    {
                        customerList.Add(new CustomerList { AC_Code = (int)row["AC_Code"], Name = row["Name"] + "/" + row["city"] });
                    }

                    FillCombo(cmbxCreditVendor, customerList, "Name", "AC_Code", 0);



                }
                else
                {
                    List<COA_D> d = new List<COA_D>();
                    d.Add(new COA_D { AC_Code = 0, AC_Title = "--Select--" });
                    d.AddRange(db.COA_D.Where(x => x.CAC_Code == value && x.CompanyID == compID && x.InActive == false).ToList());
                    FillCombo(cmbxCreditVendor, d, "AC_Title", "AC_Code", 0);
                    //var vendor = db.COA_D.Where(x => x.CAC_Code == value).ToList();

                }


                //List<COA_D> vendor = new List<COA_D>();
                //vendor.Add(new COA_D { CAC_Code = 0, AC_Title = "--SELECT--" });
                //vendor.AddRange(db.COA_D.Where(x => x.CAC_Code == value && x.CompanyID == compID).ToList());


                //FillCombo(cmbxCreditVendor, vendor, "AC_Title", "AC_Code", 0);
            }
            else if (dsa == 0)
            {

                List<COA_D> vendor = new List<COA_D>();
                vendor.Add(new COA_D { CAC_Code = 0, AC_Title = "--SELECT--" });
                vendor.AddRange(db.COA_D.Where(x => x.CAC_Code == 1 && x.CompanyID == compID).ToList());


                FillCombo(cmbxCreditVendor, vendor, "AC_Title", "AC_Code", 0);

            }
        }

        private void metroTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as MetroFramework.Controls.MetroTextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtbilty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as MetroFramework.Controls.MetroTextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            bindGrid();


        }

        public void bindGrid()
        {

            COADataGridView.Rows.Clear();
            var query = db.getJournalVoucherRecord(dtTo.Value, dtFrom.Value);

            foreach (var list in query)
            {

                var DebitName = db.COA_D.Where(x => x.AC_Code == list.AC_Code).FirstOrDefault();
                var CreditName = db.COA_D.Where(x => x.AC_Code == list.AC_Code2).FirstOrDefault();


                COADataGridView.Rows.Add(list.RID, list.EDate, list.Narr, DebitName.AC_Title, CreditName.AC_Title, list.Amt, CreditName.AC_Code, DebitName.AC_Code, CreditName.CAC_Code, DebitName.CAC_Code);



            }

        }
        private void COADataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (COADataGridView.Rows.Count >= 1)
            {

                Rid = Convert.ToInt32(COADataGridView.Rows[e.RowIndex].Cells[0].Value);
                dtEntryDate.Value = Convert.ToDateTime(COADataGridView.Rows[e.RowIndex].Cells[1].Value);
                txtAmount.Text = COADataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtDesc.Text = COADataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                cmbxCreditAccount.SelectedValue = Convert.ToInt32(COADataGridView.Rows[e.RowIndex].Cells[8].Value);
                cmbxAccID.SelectedValue = Convert.ToInt32(COADataGridView.Rows[e.RowIndex].Cells[9].Value);
                cmbxCreditVendor.SelectedValue = Convert.ToInt32(COADataGridView.Rows[e.RowIndex].Cells[6].Value);
                cmbxvendor.SelectedValue = Convert.ToInt32(COADataGridView.Rows[e.RowIndex].Cells[7].Value);
                chkForSale.Checked = Convert.ToBoolean(COADataGridView.Rows[e.RowIndex].Cells[10].Value);

            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "C://Desktop";
            openFileDialog1.Title = "Select image to be upload.";
            openFileDialog1.Filter = "Image Only(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            openFileDialog1.FilterIndex = 1;
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        string path = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                        label10.Text = path;
                        pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
                else
                {
                    MessageBox.Show("Please Upload image.");
                }
            }
            catch (Exception ex)
            {
                //it will give if file is already exits..
                MessageBox.Show(ex.Message);
            }

        }
        //private void CoaDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    COADataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //}

        //#region -- Helper Method Start --
        //public void GetDocCode(string selected)
        //{

        //}
        //public void FillCombo(ToolStripComboBox comboBox, object obj, String Name, String ID, int selected = 1)
        //{
        //    comboBox.ComboBox.DataSource = obj;
        //    comboBox.ComboBox.DisplayMember = Name; // Column Name
        //    comboBox.ComboBox.ValueMember = ID;  // Column Name
        //    comboBox.ComboBox.SelectedValue = selected;
        //}

        //private void toolStripTextBoxFind_Leave(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (toolStripTextBoxFind.Text.Trim().Length == 0) { COADataGridView.DataSource = list; }
        //        else
        //        {
        //            COADataGridView.DataSource = list.FindAll(x => x.AC_Title.ToLower().Contains(toolStripTextBoxFind.Text.ToLower().Trim()));
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //


        //private void panel1_Paint(object sender, PaintEventArgs e)
        //{

        //}

        //private void ddlCOA_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var ddl = (COA_M)ddlCOA.SelectedItem;
        //    if (ddl.CAC_Code == 0)
        //    {
        //        list = db.COA_D.ToList();
        //        COABindingSource.DataSource = list;
        //    }
        //    else
        //    {
        //        list = db.COA_D.Where(x => x.CAC_Code == ddl.CAC_Code).ToList();
        //        COABindingSource.DataSource = list;
        //    }
        //}

        //private void txtDebit_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
        //    {
        //        e.Handled = true;
        //    }

        //    // only allow one decimal point
        //    if ((e.KeyChar == '.') && ((sender as MetroFramework.Controls.MetroTextBox).Text.IndexOf('.') > -1))
        //    {
        //        e.Handled = true;
        //    }
        //} 

        //private void txtCredit_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
        //    {
        //        e.Handled = true;
        //    }

        //    // only allow one decimal point
        //    if ((e.KeyChar == '.') && ((sender as MetroFramework.Controls.MetroTextBox).Text.IndexOf('.') > -1))
        //    {
        //        e.Handled = true;
        //    }
        //}

        //private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
        //    {
        //        e.Handled = true;
        //    }

        //    // only allow one decimal point
        //    if ((e.KeyChar == '.') && ((sender as MetroFramework.Controls.MetroTextBox).Text.IndexOf('.') > -1))
        //    {
        //        e.Handled = true;
        //    }
        //}
        //public JournalVoucher()
        //{
        //    InitializeComponent();
        //}
    }

}