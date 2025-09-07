using Lib;
using Lib.Entity;
using Lib.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesMngmt.Configs
{
    public partial class Employee : MetroFramework.Forms.MetroForm
    {
        public Employee(int cmpID)
        {
            InitializeComponent();

            db = new SaleManagerEntities();
            compID = cmpID;
            pnlMain.Hide();
            list = db.tbl_Employee.ToList();
            tblEmployeeBindingSource.DataSource = list;
            var party = db.Party_Type.Where(x => x.CompanyID == compID).FirstOrDefault();
            FillCombo(cmbxParty, db.Party_Type.Where(x => x.CompanyID == compID).ToList(), "Party_Type1", "PType_ID", 1);
            // GetDocCode("1");
        }
        public void FillCombo(ComboBox comboBox, object obj, String Name, String ID, int selected)
        {
            comboBox.DataSource = obj;
            comboBox.DisplayMember = Name; // Column Name
            comboBox.ValueMember = ID;  // Column Name
            comboBox.SelectedValue = selected;
        }


        SaleManagerEntities db = null;
        List<tbl_Employee> list = null;
        int compID = 0;

        //public Coa(int cmpID)
        //{
        //    InitializeComponent();
        //    db = new SaleManagerEntities();
        //    compID = cmpID;
        //}

        private void COA_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'saleManagerDataSet3.tbl_Employee' table. You can move, or remove it, as needed.
          //  this.tbl_EmployeeTableAdapter.Fill(this.saleManagerDataSet3.tbl_Employee);
            // TODO: This line of code loads data into the 'saleManagerDataSet2.Party_Type' table. You can move, or remove it, as needed.
         //   this.party_TypeTableAdapter.Fill(this.saleManagerDataSet2.Party_Type);
            // TODO: This line of code loads data into the 'saleManagerDataSet1.tbl_Employee' table. You can move, or remove it, as needed.
            //this.tbl_EmployeeTableAdapter1.Fill(this.saleManagerDataSet1.tbl_Employee);
            //// TODO: This line of code loads data into the 'saleManagerDataSet.tbl_Employee' table. You can move, or remove it, as needed.
            //this.tbl_EmployeeTableAdapter.Fill(this.saleManagerDataSet.tbl_Employee);
           
            //var listCOA = new List<COA_M>();
            //listCOA.Add(new COA_M { CATitle = "Select", CAC_Code = 0 });
            //listCOA.AddRange(db.COA_M.ToList());
            //FillCombo(ddlCOA, listCOA, "CATitle", "CAC_Code", 0);
        }

        private void lblAdd_Click(object sender, EventArgs e)
        {
            tblEmployeeBindingSource.AddNew();
            pnlMain.Show();
           
            txtCOA.Focus();
            label3.Text = "ADD";
        }

        private void lblEdit_Click(object sender, EventArgs e)
        {
            Lib.Entity.tbl_Employee obj = (Lib.Entity.tbl_Employee)tblEmployeeBindingSource.Current;
            cmbxParty.SelectedValue = obj.PyteTypeID;
            pnlMain.Show();
            txtCOA.Focus();
            //var party = db.Party_Type.Where(x => x.CompanyID == compID).FirstOrDefault();
            //FillCombo(cmbxParty, db.Party_Type.Where(x => x.CompanyID == compID).ToList(), "Party_Type1", "PType_ID", party.PType_ID);
            //// GetDocCode(obj.CAC_Code.ToString());
            // cmbxParty.Enabled = false;
            label3.Text = "EDIT";
        }

        #region -- Global variables start --

        string docCode;

        #endregion -- Global variable end --

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlMain.Hide();
            tbl_Employee us = (tbl_Employee)tblEmployeeBindingSource.Current;
            if (us.ID == 0)
            {
                tblEmployeeBindingSource.RemoveCurrent();
                list = db.tbl_Employee.ToList();
                tblEmployeeBindingSource.DataSource = list;
            }

            else
            {
                tblEmployeeBindingSource.Clear();
                var listcancel = db.tbl_Employee.AsNoTracking().ToList();
                tblEmployeeBindingSource.DataSource = listcancel;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isAdd = true;
            SqlConnection con = null;
            SqlTransaction trans = null;
            DbContextTransaction transaction = null;
            try
            {
                if (txtCOA.Text == "")
                { MessageBox.Show("Please Provide Employee Name", "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                else
                {
                    Lib.Entity.tbl_Employee obj = (Lib.Entity.tbl_Employee)tblEmployeeBindingSource.Current;

                    var Currentobj = db.tbl_Employee.ToList().Find(x => x.EmployeName == txtCOA.Text.Trim());
                    if (obj.ID == 0)
                    {
                        if (Currentobj != null)
                        {
                            MessageBox.Show("Employee Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        bool isCodeExist = list.Any(record =>
                                             record.EmployeName == obj.EmployeName &&
                                             record.ID != obj.ID);
                        if (isCodeExist)
                        {
                            MessageBox.Show("Employee Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    transaction = db.Database.BeginTransaction();
                    if (obj.ID > 0)
                    {
                        isAdd = false;
                    }

                    DataAccess access = new DataAccess();
                    COA coa = new COA();
                    String code = "";
                    coa.AC_Code = 10;

                    con = new SqlConnection(ConnectionStrings.GetCS);
                    con.Open();
                    trans = con.BeginTransaction();
                    code = access.GetScalar("GetAc_Code", coa, con, trans);

                    COA_D coaD = new COA_D();
                    if (!isAdd)
                    {
                        coaD = db.COA_D.Where(b => b.AC_Code == obj.ACCode).FirstOrDefault();
                    }
                    coaD.CAC_Code = 10;
                    coaD.PType_ID = Convert.ToInt32(cmbxParty.SelectedValue);//1;
                    coaD.ZID = 0;
                    coaD.AC_Title = txtCOA.Text.Trim();
                    coaD.DR = 0;
                    coaD.CR = 0;
                    coaD.Qty = 0;
                    coaD.InActive = false;//chkIsActive.Checked;
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

                    obj.EmployeName = txtCOA.Text.Trim();
                    //obj.Add = txtAddress.Text.Trim();
                    //obj.Tel = txtCell.Text.Trim();
                    //obj.Eml = txtMail.Text.Trim();
                    //obj.ContactPerson = txtContact.Text.Trim();
                    //obj.Tel = txtCell.Text.Trim();
                    //obj.Cell = txtTell.Text.Trim();
                    //obj.NTN_No = txtNTN.Text.Trim();
                    //obj.ZID = Convert.ToInt32(cmbxZone.SelectedValue);
                    obj.isDeleted = chkIsActive.Checked;
                    //obj.Debit = Convert.ToDouble(txtDebit.Text == "" ? "0" : txtDebit.Text);
                    //obj.Credit = Convert.ToDouble(txtCredit.Text = txtCredit.Text == "" ? "0" : txtCredit.Text);
                    obj.companyID = compID;
                    obj.PyteTypeID =Convert.ToInt32(cmbxParty.SelectedValue);
                    if (obj.ID == 0)
                    {
                        obj.ACCode = Convert.ToInt32(code);
                        
                        db.tbl_Employee.Add(obj);
                    }
                    else
                    {
                        var result = db.tbl_Employee.SingleOrDefault(b => b.ID == obj.ID);
                        if (result != null)
                        {
                            result.EmployeName = txtCOA.Text.Trim();
                            //result.Add = txtAddress.Text.Trim();
                            //result.Tel = txtCell.Text.Trim();
                            //result.Eml = txtMail.Text.Trim();
                            //result.ContactPerson = txtContact.Text.Trim();
                            //result.Tel = txtCell.Text.Trim();
                            //result.Cell = txtTell.Text.Trim();
                            //result.NTN_No = txtNTN.Text.Trim();
                            //result.ZID = Convert.ToInt32(cmbxZone.SelectedValue);
                            result.isDeleted = chkIsActive.Checked;
                            //result.Debit = Convert.ToDouble(txtDebit.Text == "" ? "0" : txtDebit.Text);
                            //result.Credit = Convert.ToDouble(txtCredit.Text = txtCredit.Text == "" ? "0" : txtCredit.Text);
                        }
                    }
                    db.SaveChanges();

                    //credit Vendor
                    //txtDebit.Text = txtDebit.Text == "" ? "0" : txtDebit.Text;
                    //txtCredit.Text = txtCredit.Text == "" ? "0" : txtCredit.Text;

                    GL gl = new GL();
                    if (!isAdd)
                    {
                        gl = db.GL.Where(b => b.AC_Code == obj.ACCode && b.TypeCode==0 && b.RID==0).FirstOrDefault();
                    }
                    gl.RID = 0;
                    gl.RID2 = 0;
                    gl.TypeCode = 0;
                    gl.GLDate = DateTime.Now;
                    gl.AC_Code2 = 12000001;
                    gl.Narration = "Opening Entry";
                    gl.Debit = 0;//Convert.ToDouble(txtDebit.Text == "" ? "0" : txtDebit.Text);
                    gl.Credit = 0;//Convert.ToDouble(txtCredit.Text = txtCredit.Text == "" ? "0" : txtCredit.Text);
                    if (!isAdd)
                    {
                        db.Entry(gl).State = EntityState.Modified;
                    }
                    else
                    {
                        gl.AC_Code = Convert.ToInt32(code);
                        db.GL.Add(gl);
                    }
                    //db.SaveChanges();

                    GL gl2 = new GL();
                    if (!isAdd)
                    {
                        gl2 = db.GL.Where(b => b.AC_Code2 == obj.ACCode && b.TypeCode == 0 && b.RID == 0).FirstOrDefault();
                    }
                    gl2.RID = 0;
                    gl2.RID2 = 0;
                    gl2.TypeCode = 0;
                    gl2.GLDate = DateTime.Now;
                    gl2.AC_Code = 12000001;
                    gl2.Narration = "Opening " + txtCOA.Text.Trim();
                    gl2.Debit = 0;//Convert.ToDouble(txtCredit.Text = txtCredit.Text == "" ? "0" : txtCredit.Text);
                    gl2.Credit = 0;// Convert.ToDouble(txtDebit.Text == "" ? "0" : txtDebit.Text);
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
                    list = db.tbl_Employee.ToList();
                    tblEmployeeBindingSource.DataSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CoaDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            COADataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        #region -- Helper Method Start --
        //public void GetDocCode(string selected)
        //{
        //    cmbxParty.DataSource = db.Party_Type.ToList();
        //    cmbxParty.ValueMember = "PType_ID";
        //    cmbxParty.DisplayMember = "Party_Type1";
        //    cmbxParty.SelectedIndex = C;
        //}
        public void FillCombo(ToolStripComboBox comboBox, object obj, String Name, String ID, int selected = 1)
        {
            comboBox.ComboBox.DataSource = obj;
            comboBox.ComboBox.DisplayMember = Name; // Column Name
            comboBox.ComboBox.ValueMember = ID;  // Column Name
            comboBox.ComboBox.SelectedValue = selected;
        }

        private void toolStripTextBoxFind_Leave(object sender, EventArgs e)
        {
            try
            {
                if (toolStripTextBoxFind.Text.Trim().Length == 0) { COADataGridView.DataSource = list; }
                else
                {
                    // COADataGridView.DataSource = list.FindAll(x => x.AC_Title.ToLower().Contains(toolStripTextBoxFind.Text.ToLower().Trim()));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        //
        #endregion -- Helper Method End --

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ddlCOA_SelectedIndexChanged(object sender, EventArgs e)
        {
            //    var ddl = (COA_M)ddlCOA.SelectedItem;
            //    if (ddl.CAC_Code == 0)
            //    {
            //        //list = db.tbl_Employee.ToList();
            //        //COABindingSource.DataSource = list;
            //    }
            //    else
            //    {
            //      //  list = db.COA_D.Where(x => x.CAC_Code == ddl.CAC_Code).ToList();
            //        COABindingSource.DataSource = list;
            //    }
            //}
        }
    }
}
