using Lib;
using Lib.Entity;
using Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SalesMngmt.Configs
{
    public partial class Items : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<Party_Type> list = null;
        int compID = 0;

        public Items(int cmpID)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            compID = cmpID;
        }

        private void Items_Load(object sender, EventArgs e)
        {
            //PartyType type = new PartyType();
            //DataAccess access = new DataAccess();
            //type.PType_ID = 1;
            //var con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            //con.Open();
            //SqlTransaction trans = con.BeginTransaction();
            //var objPartyType = access.Get<PartyType>("Sp_partyType_Select", type, con, trans);

            pnlMain.Hide();
            list = db.Party_Type.Where(x => x.CompanyID == compID).ToList();
            itemBindingSource.DataSource = list;
        }

        private void lblAdd_Click(object sender, EventArgs e)
        {
            itemBindingSource.AddNew();
            pnlMain.Show();
            GetDocCode();
            txtPartyType.Focus();
            label3.Text = "ADD";
        }

        private void lblEdit_Click(object sender, EventArgs e)
        {
            Party_Type obj = (Party_Type)itemBindingSource.Current;
            pnlMain.Show();
            txtPartyType.Focus();
            label3.Text = "EDIT";
        }

        #region -- Global variables start --

        string docCode;

        #endregion -- Global variable end --


        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlMain.Hide();
            Party_Type us = (Party_Type)itemBindingSource.Current;
            if (us.PType_ID == 0)
            {
                itemBindingSource.RemoveCurrent();
            }
        }

        //public void alluser(string username)
        //{
        //    lblUserName.Text = username;
        //}
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPartyType.Text == "")
            { MessageBox.Show("Please Provide Party Type", "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
                Party_Type obj = (Party_Type)itemBindingSource.Current;

                var Currentobj = db.Party_Type.ToList().Find(x => x.Party_Type1 == txtPartyType.Text.Trim());
                if (obj.PType_ID == 0)
                {
                    if (Currentobj != null)
                    {
                        MessageBox.Show("Party Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    bool isCodeExist = list.Any(record =>
                                         record.Party_Type1 == obj.Party_Type1 &&
                                         record.PType_ID != obj.PType_ID);
                    if (isCodeExist)
                    {
                        MessageBox.Show("Party Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                obj.Party_Type1 = txtPartyType.Text.Trim();
                obj.isDeleted = chkIsActive.Checked;
                obj.CompanyID = 0;
                if (obj.PType_ID == 0)
                {
                    db.Party_Type.Add(obj);
                }
                else
                {
                    var result = db.Party_Type.SingleOrDefault(b => b.PType_ID == obj.PType_ID);
                    if (result != null)
                    {
                        result.Party_Type1 = txtPartyType.Text.Trim();
                        result.isDeleted = chkIsActive.Checked;
                    }
                }
                db.SaveChanges();
                pnlMain.Hide();
            }
        }
        private void PartyTypeDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            itemsDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        #region -- Helper Method Start --
        public void GetDocCode()
        {
            //PartyTypeList obj = new PartyTypeList(new PartyType { }.Select());
            //docCode = "DOC-" + (obj.Count + 1);
        }

        private void toolStripTextBoxFind_Leave(object sender, EventArgs e)
        {
            try
            {
                if (toolStripTextBoxFind.Text.Trim().Length == 0) { itemsDataGridView.DataSource = list; }
                else
                {
                    itemsDataGridView.DataSource = list.FindAll(x => x.Party_Type1.ToLower().Contains(toolStripTextBoxFind.Text.ToLower().Trim()));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        //
        #endregion -- Helper Method End --
    }
}
