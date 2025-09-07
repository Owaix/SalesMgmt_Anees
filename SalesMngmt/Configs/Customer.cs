using Lib;
using Lib.Entity;
using Lib.Model;
using Lib.Reporting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace SalesMngmt.Configs
{
    public partial class Customer : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<Lib.Entity.Customers> list = null;
        int compID = 0;
        int aa = 0;
        public Customer(int comID)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            compID = comID;
        }

        public void clear() {
            //CstBindingSource.RemoveCurrent();
            //CstBindingSource.Clear();
            //list = db.Customers.AsNoTracking().Where(x => x.CompanyID == compID).ToList();
            //CstBindingSource.DataSource = list;
          
            var party = db.Party_Type.Where(x => x.CompanyID == compID && x.isDeleted == false).FirstOrDefault();
            // var zone = db.Party_Type.Where(x => x.CompanyID == compID).FirstOrDefault();


            //PArty Type CombboBox
            List<Party_Type> PartyTypes = new List<Party_Type>();
            PartyTypes.Add(new Party_Type { PType_ID = 0, Party_Type1 = "--Select--" });
            PartyTypes.AddRange(db.Party_Type.AsNoTracking().Where(x => x.CompanyID == compID && x.isDeleted == false).ToList());

            FillCombo(cmbCType, PartyTypes, "Party_Type1", "PType_ID", 0);

            //Employee Type CombboBox
            List<tbl_Employee> Employee = new List<tbl_Employee>();
            Employee.Add(new tbl_Employee { ID = 0, EmployeName = "--Select--" });
            Employee.AddRange(db.tbl_Employee.AsNoTracking().Where(x => x.companyID == compID && x.isDeleted == false).ToList());

            FillCombo(cmbxEmployee, Employee, "EmployeName", "ID", 0);


            // city

            List<tbl_city> City = new List<tbl_city>();
            City.Add(new tbl_city { Id = 0, CityName = "--Select--" });
            City.AddRange(db.tbl_city.AsNoTracking().Where(x => x.CompyID == compID && x.isDeleted == false).ToList());
            FillCombo(cmbxCity, City, "CityName", "Id", 0);

          

            CstBindingSource.AddNew();

            cmbCType.SelectedValue = 0;
            cmbxCity.SelectedValue = 0;
            cmbxEmployee.SelectedValue = 0;
            chkIsActive.Checked = false;
            txtAddress.Text = "";
            txtCstName.Text = "";
            txtDebit.Text = "";
            txtCredit.Text = "";


        }

        private void Customer_Load(object sender, EventArgs e)
        {
            pnlMain.Hide();
            list =db.Customers.Where(x =>x.CompanyID==compID ).ToList();// db.Customers.Where(x => x.CompanyID == compID).ToList();
            CstBindingSource.DataSource = list;
            var party = db.Party_Type.Where(x => x.CompanyID == compID && x.isDeleted==false).FirstOrDefault();
           // var zone = db.Party_Type.Where(x => x.CompanyID == compID).FirstOrDefault();


            //PArty Type CombboBox
            List<Party_Type> PartyTypes = new List<Party_Type>();
            PartyTypes.Add(new Party_Type { PType_ID = 0, Party_Type1 = "--Select--" });
            PartyTypes.AddRange(db.Party_Type.AsNoTracking().Where(x => x.CompanyID == compID && x.isDeleted == false).ToList());
            
            FillCombo(cmbCType, PartyTypes, "Party_Type1", "PType_ID", 0);

            //Employee Type CombboBox
            List<tbl_Employee> Employee = new List<tbl_Employee>();
            Employee.Add(new tbl_Employee { ID = 0, EmployeName = "--Select--" });
            Employee.AddRange(db.tbl_Employee.AsNoTracking().Where(x => x.companyID == compID && x.isDeleted == false).ToList());

            FillCombo(cmbxEmployee, Employee, "EmployeName", "ID", 0);


            // city

            List<tbl_city> City = new List<tbl_city>();
            City.Add(new tbl_city { Id = 0, CityName = "--Select--" });
            City.AddRange(db.tbl_city.AsNoTracking().Where(x => x.CompyID == compID && x.isDeleted == false).ToList());
            FillCombo(cmbxCity, City, "CityName", "Id", 0);
            //   FillCombo(cmbxZone, db.Zones.Where(x => x.CompanyID == compID).ToList(), "Zone1", "ZID",1);
        }

        private void lblAdd_Click(object sender, EventArgs e)
        {
          

            CstBindingSource.AddNew();

            cmbCType.SelectedValue = 0;
            cmbxCity.SelectedValue = 0;
            cmbxEmployee.SelectedValue = 0;
            pnlMain.Show();
               
            

            label3.Text = "ADD";
            cmbCType.Focus();
            //PArty Type CombboBox
            //List<Party_Type> PartyTypes = new List<Party_Type>();
            //PartyTypes.Add(new Party_Type { PType_ID = 0, Party_Type1 = "--Select--" });
            //PartyTypes.AddRange(db.Party_Type.Where(x => x.CompanyID == compID).ToList());
            //FillCombo(cmbCType, PartyTypes, "Party_Type1", "PType_ID", 0);



        }

        public void customerAdd() {
            CstBindingSource.AddNew();
            pnlMain.Show();
            txtCstName.Focus();
            label3.Text = "ADD";


           
        }

        private void lblEdit_Click(object sender, EventArgs e)
        {
            Lib.Entity.Customers obj = (Lib.Entity.Customers)CstBindingSource.Current;
            pnlMain.Show();
         //  cmbCType.SelectedValue = obj.PType_ID;
            txtCstName.Focus();
            label3.Text = "EDIT";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlMain.Hide();
            Lib.Entity.Customers us = (Lib.Entity.Customers)CstBindingSource.Current;
            if (us.CID == 0)
            {
                CstBindingSource.RemoveCurrent();
                CstBindingSource.Clear();
                list = db.Customers.AsNoTracking().Where(x => x.CompanyID == compID).ToList();
                CstBindingSource.DataSource = list;

            }
            else
            {

                CstBindingSource.Clear();
                list = db.Customers.AsNoTracking().Where(x => x.CompanyID == compID).ToList();
                CstBindingSource.DataSource = list;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var ac = cmbCType.SelectedValue;

            bool isAdd = true;
            SqlConnection con = null;
            SqlTransaction trans = null;
            DbContextTransaction transaction = null;
            try
            {
                if (txtCstName.Text == "")
                { MessageBox.Show("Please Provide Cstdor Name", "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                else
                {
                    Lib.Entity.Customers obj = (Lib.Entity.Customers)CstBindingSource.Current;

                    var Currentobj = db.Customers.ToList().Find(x => x.CusName == txtCstName.Text.Trim() && x.CompanyID == compID);
                    if (obj.CID == 0)
                    {
                        if (Currentobj != null)
                        {
                            MessageBox.Show("Customer Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        bool isCodeExist = db.Customers.Any(record =>
                                             record.CusName == obj.CusName &&
                                             record.CID != obj.CID &&
                                             record.CompanyID == compID);
                        if (isCodeExist)
                        {
                            MessageBox.Show("Customer Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                            return;
                        }
                    }

                    transaction = db.Database.BeginTransaction();
                    if (obj.CID > 0)
                    {
                        isAdd = false;
                    }

                    DataAccess access = new DataAccess();
                    COA coa = new COA();
                    String code = "";
                    coa.AC_Code = 3;

                    con = new SqlConnection(ConnectionStrings.GetCS);
                    con.Open();
                    trans = con.BeginTransaction();
                    code = access.GetScalar("GetAc_Code", coa, con, trans);

                    var Maxcapital = ReportsController.getMixACodeById(12, compID);
                    int capital = 0;
                    int a = (int)Maxcapital.Rows[0]["Min"];


                    if (a == 0)
                    {
                        capital = (int)db.GetAc_Code(12).FirstOrDefault();
                        COA_D coD = new COA_D();
                        coD.CAC_Code = 12;
                        coD.PType_ID = 1;
                        coD.ZID = 0;
                        coD.AC_Code = capital;
                        coD.AC_Title = "Capital";
                        coD.DR = 0;
                        coD.CR = 0;
                        coD.Qty = 0;
                        coD.CompanyID = compID;
                        coD.InActive = false;

                        a= capital;
                        db.COA_D.Add(coD);
                        db.SaveChanges();


                    }
                    else
                    {

                        capital = a;

                    }



                    COA_D coaD = new COA_D();
                    if (!isAdd)
                    {
                        coaD = db.COA_D.Where(b => b.AC_Code == obj.AC_Code && b.CompanyID == compID).FirstOrDefault();
                    }
                    coaD.CAC_Code = 3;
                    coaD.PType_ID = Convert.ToInt32(cmbCType.SelectedValue);//1;
                    coaD.ZID = 0;
                    coaD.AC_Title = txtCstName.Text.Trim();
                    coaD.DR = 0;
                    coaD.CR = 0;
                    coaD.Qty = 0;
                    coaD.CompanyID = compID;
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

                    obj.CusName = txtCstName.Text.Trim();
                    obj.City = Convert.ToInt32(cmbxCity.SelectedValue);
                    obj.Add = txtAddress.Text.Trim();
                    obj.Tel = txtCell.Text.Trim();
                    obj.Eml = txtMail.Text.Trim();
                    obj.ContactPerson = txtContact.Text.Trim();
                    obj.Tel = txtTell.Text.Trim();
                    obj.Cell = txtCell.Text.Trim();
                    obj.NTN_No = txtNTN.Text.Trim();
                    obj.ZID = Convert.ToInt32(cmbxZone.SelectedValue);
                    obj.InActive = chkIsActive.Checked;
                    obj.Debit = Convert.ToDouble(txtDebit.Text == "" ? "0" : txtDebit.Text);
                    obj.Credit = Convert.ToDouble(txtCredit.Text = txtCredit.Text == "" ? "0" : txtCredit.Text);
                    obj.CompanyID = compID;
                    obj.PType_ID= Convert.ToInt32(cmbCType.SelectedValue);
                    obj.CollectPerMonth= Convert.ToDecimal(txtMonth.Text == "" ? "0" : txtMonth.Text);
                    obj.MrNO = txtMrNo.Text;
                    obj.CompanyID = compID;
                    obj.SID = Convert.ToInt32(cmbxEmployee.SelectedValue);
                    if (obj.CID == 0)
                    {
                        obj.AC_Code = Convert.ToInt32(code);
                        db.Customers.Add(obj);
                    }
                    else
                    {
                        var result = db.Customers.SingleOrDefault(b => b.CID == obj.CID && b.CompanyID==compID);
                        if (result != null)
                        {
                            result.PType_ID = Convert.ToInt32(cmbCType.SelectedValue);
                            result.CusName = txtCstName.Text.Trim();
                            result.Add = txtAddress.Text.Trim();
                            result.City= Convert.ToInt32(cmbxCity.SelectedValue);
                            result.Eml = txtMail.Text.Trim();
                            result.ContactPerson = txtContact.Text.Trim();
                            result.Tel = txtTell.Text.Trim();
                            result.Cell = txtCell.Text.Trim();
                            result.NTN_No = txtNTN.Text.Trim();
                            result.ZID = Convert.ToInt32(cmbxZone.SelectedValue);
                            result.InActive = chkIsActive.Checked;
                            result.Debit = Convert.ToDouble(txtDebit.Text == "" ? "0" : txtDebit.Text);
                            result.Credit = Convert.ToDouble(txtCredit.Text = txtCredit.Text == "" ? "0" : txtCredit.Text);
                            result.CollectPerMonth = Convert.ToDecimal(txtMonth.Text == "" ? "0" : txtMonth.Text);
                            result.MrNO = txtMrNo.Text;
                            result.CompanyID = compID;
                            result.SID = Convert.ToInt32(cmbxEmployee.SelectedValue);

                        }
                    }
                    db.SaveChanges();

                    //credit Vendor
                    txtDebit.Text = txtDebit.Text == "" ? "0" : txtDebit.Text;
                    txtCredit.Text = txtCredit.Text == "" ? "0" : txtCredit.Text;

                    GL gl = new GL();
                    if (!isAdd)
                    {
                        gl = db.GL.Where(b => b.AC_Code == obj.AC_Code && b.TypeCode== 0 && b.CompID == compID).FirstOrDefault();
                    }
                    gl.RID = 0;
                    gl.RID2 = 0;
                    gl.TypeCode = 0;
              
                   
                
                    gl.Narration = "Opening Entry";
                    gl.Debit = Convert.ToDouble(txtDebit.Text == "" ? "0" : txtDebit.Text);
                    gl.Credit = Convert.ToDouble(txtCredit.Text = txtCredit.Text == "" ? "0" : txtCredit.Text);
                    gl.CompID = compID;
                    if (!isAdd)
                    {
                      
                        db.Entry(gl).State = EntityState.Modified;
                    }
                    else
                    {
                        gl.AC_Code = Convert.ToInt32(code);
                        gl.AC_Code2 = a;
                        gl.GLDate = DateTime.Now;
                        db.GL.Add(gl);
                    }
                    //db.SaveChanges();

                    GL gl2 = new GL();
                    if (!isAdd)
                    {
                        gl2 = db.GL.Where(b => b.AC_Code2 == obj.AC_Code && b.TypeCode == 0 && b.CompID == compID).FirstOrDefault();
                    }
                    gl2.RID = 0;
                    gl2.RID2 = 0;
                    gl2.TypeCode = 0;
                   
                  
                    gl2.Narration = "Opening " + txtCstName.Text.Trim();
                    gl2.Debit = Convert.ToDouble(txtCredit.Text = txtCredit.Text == "" ? "0" : txtCredit.Text);
                    gl2.Credit = Convert.ToDouble(txtDebit.Text == "" ? "0" : txtDebit.Text);
                    gl2.CompID = compID;
                    if (!isAdd)
                    {
                        db.Entry(gl2).State = EntityState.Modified;
                    }
                    else
                    {
                        gl2.AC_Code = a;
                        gl2.AC_Code2 = Convert.ToInt32(code);
                        gl2.GLDate = DateTime.Now;
                        db.GL.Add(gl2);
                    }
                    db.SaveChanges();
                    transaction.Commit();




                   

                    clear();
                    label3.Text = "ADD";
                 

                    MessageBox.Show("Entry Save Successfully");
                    cmbCType.Focus();

                  
        

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void PartyTypeDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CustomerDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public void FillCombo(ComboBox comboBox, object obj, String Name, String ID, int selected )
        {
            comboBox.DataSource = obj;
            comboBox.DisplayMember = Name; // Column Name
            comboBox.ValueMember = ID;  // Column Name
            comboBox.SelectedIndex = selected;
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

        private void metroTabPage1_Click(object sender, EventArgs e)
        {

        }

        private void CustomerDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 11)
            {

                int code = Convert.ToInt32(CustomerDataGridView.Rows[e.RowIndex].Cells[0].Value);

                Reporting.CustomerLedgerSummary ledger = new Reporting.CustomerLedgerSummary(compID);

                ledger.Show();
                ledger.customerledgers(code);
                
            }
       }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (toolStripTextBoxFind.Text.Trim().Length == 0)
                {
                    CstBindingSource.Clear();
                    list = db.Customers.AsNoTracking().Where(x => x.CompanyID == compID).ToList();
                    CstBindingSource.DataSource = list;
                }
                else
                {

                    CstBindingSource.Clear();
                    list = db.Customers.AsNoTracking().Where(x => x.CompanyID == compID).ToList();


                    CstBindingSource.DataSource = list.FindAll(x => x.CusName.ToLower().Contains(toolStripTextBoxFind.Text.ToLower().Trim()));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
