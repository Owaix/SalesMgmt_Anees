using Lib;
using Lib.Entity;
using Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SalesMngmt.Configs
{
    public partial class Company : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<IComp_M> list = null;
        public Company()
        {
            InitializeComponent();
            db = new SaleManagerEntities();
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
            list = db.IComp_M.ToList();
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
            IComp_M obj = (IComp_M)companyBindingSource.Current;
            
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
            IComp_M us = (IComp_M)companyBindingSource.Current;
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
                IComp_M obj = (IComp_M)companyBindingSource.Current;

                var Currentobj = db.IComp_M.ToList().Find(x => x.Comp == txtCompName.Text.Trim());
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
                if (obj.CompID == 0)
                {
                    db.IComp_M.Add(obj);
                }
                else
                {
                    var result = db.IComp_M.SingleOrDefault(b => b.CompID == obj.CompID);
                    if (result != null)
                    {
                        result.Comp = txtCompName.Text.Trim();
                        result.isDelete = chkIsActive.Checked;
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
