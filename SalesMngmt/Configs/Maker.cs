 using Lib;
using Lib.Entity;
using Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SalesMngmt.Configs
{
    public partial class Maker : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<Item_Maker> list = null;
        int compID = 0;

        public Maker(int cmpID)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            compID = cmpID;
        }

        private void Maker_Load(object sender, EventArgs e)
        {
            //PartyType type = new PartyType();
            //DataAccess access = new DataAccess();
            //type.PType_ID = 1;
            //var con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            //con.Open();
            //SqlTransaction trans = con.BeginTransaction();
            //var objPartyType = access.Get<PartyType>("Sp_partyType_Select", type, con, trans);

            pnlMain.Hide();
            list = db.Item_Maker.Where(x => x.CompanyID == compID).ToList();
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
            Item_Maker obj = (Item_Maker)itemBindingSource.Current;
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
            Item_Maker us = (Item_Maker)itemBindingSource.Current;
            if (us.MakerId == 0)
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
            { MessageBox.Show("Please Provide Maker Name", "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
                Item_Maker obj = (Item_Maker)itemBindingSource.Current;

                //= list.Find(x => x.Name == txtPartyType.Text.Trim());
                var Currentobj = db.Item_Maker.ToList().Find(x => x.Name == txtPartyType.Text.Trim() && x.CompanyID==compID);
                if (obj.MakerId == 0)
                {
                    if (Currentobj != null)
                    {
                        MessageBox.Show("Maker Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    bool isCodeExist = list.Any(record =>
                                         record.Name == obj.Name &&
                                         record.MakerId != obj.MakerId
                                         && record.CompanyID == compID);
                    if (isCodeExist)
                    {
                        MessageBox.Show("Maker Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                obj.Name = txtPartyType.Text.Trim();
                obj.Address = txtAddress.Text.Trim();
                obj.CompanyID = compID;
                obj.IsDelete = chkDelete.Checked;
                if (obj.MakerId == 0)
                {
                    db.Item_Maker.Add(obj);
                }
                else
                {
                    var result = db.Item_Maker.SingleOrDefault(b => b.MakerId == obj.MakerId);
                    if (result != null)
                    {
                        result.Name = txtPartyType.Text.Trim();
                        result.Address = txtAddress.Text.Trim();
                        result.CompanyID = compID;
                        result.IsDelete = chkDelete.Checked;
                    }
                }
                db.SaveChanges();
                pnlMain.Hide();
            }
        }
        private void PartyTypeDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MakerDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
                if (toolStripTextBoxFind.Text.Trim().Length == 0) { MakerDataGridView.DataSource = list; }
                else
                {
                    MakerDataGridView.DataSource = list.FindAll(x => x.Name.ToLower().Contains(toolStripTextBoxFind.Text.ToLower().Trim()));
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
