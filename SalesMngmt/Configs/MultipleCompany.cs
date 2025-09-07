using Lib.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesMngmt.Configs
{
    public partial class MultipleCompany : Form
    {
        SaleManagerEntities db = null;
        List<tbl_Company> list = null;
        int companyID = 0;
        public MultipleCompany(int company)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            companyID = company;

        }

        private void Company_Load(object sender, EventArgs e)
        {
            //PartyType type = new PartyType();
            //DataAccess access = new DataAccess();
            //type.PType_ID = 1;
            //var con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            //con.Open();
            //SqlTransaction trans = con.BeginTransaction();
            //var objPartyType = access.Get<PartyType>("Sp_partyType_Select", type, con, trans);

            pnlMain.Hide();
            list = db.tbl_Company.Where(x=>x.companyID== companyID).ToList();
            companyBindingSource.DataSource = list;
        }

        private void lblAdd_Click(object sender, EventArgs e)
        {
            companyBindingSource.AddNew();
            pnlMain.Show();
            GetDocCode();
            txtCompName.Focus();
            label3.Text = "ADD";
        }

        private void lblEdit_Click(object sender, EventArgs e)
        {
            tbl_Company obj = (tbl_Company)companyBindingSource.Current;

            pnlMain.Show();
            txtCompName.Focus();
            label3.Text = "EDIT";
        }

        #region -- Global variables start --

        string docCode;

        #endregion -- Global variable end --


        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlMain.Hide();
            tbl_Company us = (tbl_Company)companyBindingSource.Current;
            if (us.CompID == 0)
            {
                companyBindingSource.RemoveCurrent();
            }
        }

        //public void alluser(string username)
        //{
        //    lblUserName.Text = username;
        //}
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCompName.Text == "")
            { MessageBox.Show("Please Provide Party Type", "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
                tbl_Company obj = (tbl_Company)companyBindingSource.Current;

                var Currentobj = db.tbl_Company.ToList().Find(x => x.Comp == txtCompName.Text.Trim() && x.CompID==companyID);
                if (obj.CompID == 0)
                {
                    if (Currentobj != null)
                    {
                        MessageBox.Show("Company Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    bool isCodeExist = list.Any(record =>
                                         record.Comp == obj.Comp &&
                                         record.CompID != obj.CompID);
                    if (isCodeExist)
                    {
                        MessageBox.Show("Company Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                obj.Comp = txtCompName.Text.Trim();
                obj.isDelete = chkIsActive.Checked;
                obj.Address = txtAddress.Text.Trim();
                obj.Tel = txtTel.Text.Trim();
                obj.companyID = companyID;
                if (obj.CompID == 0)
                {
                    db.tbl_Company.Add(obj);
                }
                else
                {
                    var result = db.tbl_Company.SingleOrDefault(b => b.CompID == obj.CompID);
                    if (result != null)
                    {
                        result.Comp = txtCompName.Text.Trim();
                        result.isDelete = chkIsActive.Checked;
                        result.Address = txtAddress.Text.Trim();
                        result.Tel = txtTel.Text.Trim();
                    }
                }
                db.SaveChanges();
                pnlMain.Hide();
            }
        }
        private void PartyTypeDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CompanyDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        #region -- Helper Method Start --
        public void GetDocCode()
        {
            //PartyTypeList obj = new PartyTypeList(new PartyType { }.Select());
            //docCode = "DOC-" + (obj.Count + 1);
        }

        #endregion -- Helper Method End --


        private void toolStripTextBoxFind_Leave(object sender, EventArgs e)
        {
            try
            {
                if (toolStripTextBoxFind.Text.Trim().Length == 0) { CompanyDataGridView.DataSource = list; }
                else
                {
                    CompanyDataGridView.DataSource = list.FindAll(x => x.Comp.ToLower().Contains(toolStripTextBoxFind.Text.ToLower().Trim()));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }




    }

}
