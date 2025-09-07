using Lib;
using Lib.Entity;
using Lib.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace SalesMngmt.Configs
{
    public partial class Vandor_Fr : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<Vendors> list = null;
        int compID = 0;
        public Vandor_Fr(int comID)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            compID = comID;
        }

        private void Vandor_Fr_Load(object sender, EventArgs e)
        {
            pnlMain.Hide();
            list = db.Vendors.AsNoTracking().Where(x => x.CompanyID == compID).ToList();
            VendorBindingSource.DataSource = list;
            FillCombo(cmbPType, db.Party_Type.Where(x => x.CompanyID == compID).ToList(), "Party_Type1", "PType_ID");
            FillCombo(cmbxZone, db.Zone.Where(x => x.CompanyID == compID).ToList(), "Zone1", "ZID");
        }

        private void lblAdd_Click(object sender, EventArgs e)
        {
            VendorBindingSource.AddNew();
            pnlMain.Show();
            txtVndrName.Focus();
            label3.Text = "ADD";
        }

        private void lblEdit_Click(object sender, EventArgs e)
        {
            Vendors obj = (Vendors)VendorBindingSource.Current;
            pnlMain.Show();
            txtVndrName.Focus();
            label3.Text = "EDIT";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlMain.Hide();
            Vendors us = (Vendors)VendorBindingSource.Current;
            if (us.VID == 0)
            {
                VendorBindingSource.RemoveCurrent();
                VendorBindingSource.Clear();
                list = db.Vendors.AsNoTracking().Where(x => x.CompanyID == compID).ToList();
                VendorBindingSource.DataSource = list;
            }
            else {

                VendorBindingSource.Clear();
                list = db.Vendors.AsNoTracking().Where(x => x.CompanyID == compID).ToList();
                VendorBindingSource.DataSource = list;

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isAdd = true;
            SqlConnection con = null;
            SqlTransaction trans = null;
            DbContextTransaction transaction = db.Database.BeginTransaction();

            try
            {
                if (txtVndrName.Text == "")
                { MessageBox.Show("Please Provide Vendor Name", "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                else
                {
                    Vendors obj = (Vendors)VendorBindingSource.Current;
                    var Currentobj = db.Vendors.ToList().Find(x => x.VendName == txtVndrName.Text.Trim());
                    if (obj.VID == 0)
                    {
                        if (Currentobj != null)
                        {
                            MessageBox.Show("Vendor Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        bool isCodeExist = list.Any(record =>
                                             record.VendName == obj.VendName &&
                                             record.VID != obj.VID);
                        if (isCodeExist)
                        {
                            MessageBox.Show("Vendor Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                            return;
                        }
                    }

                    transaction = db.Database.BeginTransaction();
                    if (obj.VID > 0)
                    {
                        isAdd = false;
                    }

                    DataAccess access = new DataAccess();
                    COA coa = new COA();
                    String code = "";
                    coa.AC_Code = 9;

                    con = new SqlConnection(ConnectionStrings.GetCS);
                    con.Open();
                    trans = con.BeginTransaction();
                    code = access.GetScalar("GetAc_Code", coa, con, trans);

                    COA_D coaD = new COA_D();
                    if (!isAdd)
                    {
                        coaD = db.COA_D.Where(b => b.AC_Code == obj.AC_Code).FirstOrDefault();
                    }
                    coaD.CAC_Code = 9;
                    coaD.PType_ID = 1;
                    coaD.ZID = 0;
                    coaD.AC_Code = Convert.ToInt32(code);
                    coaD.AC_Title = txtVndrName.Text.Trim();
                    coaD.DR = 0;
                    coaD.CR = 0;
                    coaD.Qty = 0;
                    coaD.InActive = false;
                    if (!isAdd)
                    {
                        db.Entry(coaD).State = EntityState.Modified;
                    }
                    else
                    {
                        coaD.AC_Code = Convert.ToInt32(code);
                        db.COA_D.Add(coaD);
                    }

                    db.SaveChanges();

                    obj.VendName = txtVndrName.Text.Trim();
                    obj.Add = txtAddress.Text.Trim();
                    obj.Tel = txtTel.Text.Trim();
                    obj.Eml = txtMail.Text.Trim();
                    obj.ContactPerson = txtContact.Text.Trim();
                    obj.Tel = txtTel.Text.Trim();
                    obj.Cell = txtCell.Text.Trim();
                    obj.NTN_No = txtNTN.Text.Trim();
                    obj.ZID = Convert.ToInt32(cmbxZone.SelectedValue);
                    obj.InActive = chkIsActive.Checked;
                    obj.Debit = Convert.ToDouble(txtDebit.Text == "" ? "0" : txtDebit.Text);
                    obj.Credit = Convert.ToDouble(txtCredit.Text = txtCredit.Text == "" ? "0" : txtCredit.Text);
                    obj.CompanyID = compID;
                    obj.AC_Code = Convert.ToInt32(code);

                    if (obj.VID == 0)
                    {
                        db.Vendors.Add(obj);
                    }
                    else
                    {
                        var result = db.Vendors.SingleOrDefault(b => b.VID == obj.VID);
                        if (result != null)
                        {
                            result.VendName = txtVndrName.Text.Trim();
                            result.Add = txtAddress.Text.Trim();
                            result.Tel = txtTel.Text.Trim();
                            result.Eml = txtMail.Text.Trim();
                            result.ContactPerson = txtContact.Text.Trim();
                            result.Tel = txtTel.Text.Trim();
                            result.Cell = txtCell.Text.Trim();
                            result.NTN_No = txtNTN.Text.Trim();
                            result.ZID = Convert.ToInt32(cmbxZone.SelectedValue);
                            result.InActive = chkIsActive.Checked;
                            result.Debit = Convert.ToDouble(txtDebit.Text == "" ? "0" : txtDebit.Text);
                            result.Credit = Convert.ToDouble(txtCredit.Text = txtCredit.Text == "" ? "0" : txtCredit.Text);
                        }
                    }
                    db.SaveChanges();

                    //credit Vendor
                    txtDebit.Text = txtDebit.Text == "" ? "0" : txtDebit.Text;
                    txtCredit.Text = txtCredit.Text == "" ? "0" : txtCredit.Text;

                    GL gl = new GL();

                    if (!isAdd)
                    {
                        gl = db.GL.Where(b => b.AC_Code == obj.AC_Code && b.TypeCode == 0).FirstOrDefault();
                    }
                    gl.RID = 0;
                    gl.RID2 = 0;
                    gl.TypeCode = 0;
                    gl.GLDate = DateTime.Now;
                    gl.AC_Code = Convert.ToInt32(code);
                    gl.AC_Code2 = 12000001;
                    gl.Narration = "Opening " + txtVndrName.Text.Trim();
                    gl.Debit = Convert.ToDouble(txtDebit.Text == "" ? "0" : txtDebit.Text);
                    gl.Credit = Convert.ToDouble(txtCredit.Text = txtCredit.Text == "" ? "0" : txtCredit.Text);

                    if (!isAdd)
                    {
                        db.Entry(gl).State = EntityState.Modified;
                    }
                    else
                    {
                        gl.AC_Code = Convert.ToInt32(code);
                        db.GL.Add(gl);
                    }



                    GL gl2 = new GL();

                    {
                        gl2 = db.GL.Where(b => b.AC_Code2 == obj.AC_Code && b.TypeCode == 0).FirstOrDefault();
                    }
                    gl2.RID = 0;
                    gl2.RID2 = 0;
                    gl2.TypeCode = 0;
                    gl2.GLDate = DateTime.Now;
                    gl2.AC_Code = 12000001;
                    gl2.AC_Code2 = Convert.ToInt32(code);
                    gl2.Narration = "Opening Entry";
                    gl2.Debit = Convert.ToDouble(txtCredit.Text = txtCredit.Text == "" ? "0" : txtCredit.Text);
                    gl2.Credit = Convert.ToDouble(txtDebit.Text == "" ? "0" : txtDebit.Text);
                    if (!isAdd)
                    {
                        db.Entry(gl2).State = EntityState.Modified;
                    }
                    else
                    {
                        gl2.AC_Code2 = Convert.ToInt32(code);
                        db.GL.Add(gl2);
                    }
                    db.SaveChanges();
                    transaction.Commit();
                    pnlMain.Hide();

                    VendorBindingSource.Clear();
                    list = db.Vendors.AsNoTracking().Where(x => x.CompanyID == compID).ToList();
                    VendorBindingSource.DataSource = list;
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show(ex.Message);
            }
        }
        private void PartyTypeDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            VendorsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public void FillCombo(ComboBox comboBox, object obj, String Name, String ID, int selected = 1)
        {
            comboBox.DataSource = obj;
            comboBox.DisplayMember = Name; // Column Name
            comboBox.ValueMember = ID;  // Column Name
            comboBox.SelectedValue = selected;
        }

        private void toolStripTextBoxFind_Leave(object sender, EventArgs e)
        {
           
        }

        private void txtDebit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as MetroFramework.Controls.MetroTextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtCredit_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as MetroFramework.Controls.MetroTextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (toolStripTextBoxFind.Text.Trim().Length == 0)
                {
                    VendorBindingSource.Clear();
                    list = db.Vendors.AsNoTracking().Where(x => x.CompanyID == compID).ToList();
                    VendorBindingSource.DataSource = list;
                }
                else
                {
                    VendorBindingSource.Clear();
                    list = db.Vendors.AsNoTracking().Where(x => x.CompanyID == compID).ToList();


                    VendorsDataGridView.DataSource = list.FindAll(x => x.VendName.ToLower().Contains(toolStripTextBoxFind.Text.ToLower().Trim()));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
