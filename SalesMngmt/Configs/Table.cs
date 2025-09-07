using Lib.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SalesMngmt.Configs
{
    public partial class Table : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<tbl_Table> list = null;
        int compID = 0;

        public Table(int cmpID)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            compID = cmpID;
          

        }

        private void Maker_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'saleManagerDataSet5.tbl_Table' table. You can move, or remove it, as needed.
            //   this.tbl_TableTableAdapter.Fill(this.saleManagerDataSet5.tbl_Table);
            // TODO: This line of code loads data into the 'saleManagerDataSet4.tbl_Table' table. You can move, or remove it, as needed.
            //  this.tbl_TableTableAdapter.Fill(this.saleManagerDataSet4.tbl_Table);
            //PartyType type = new PartyType();
            //DataAccess access = new DataAccess();
            //type.PType_ID = 1;
            //var con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            //con.Open();
            //SqlTransaction trans = con.BeginTransaction();
            //var objPartyType = access.Get<PartyType>("Sp_partyType_Select", type, con, trans);
            pnlMain.Hide();
            list = db.tbl_Table.AsNoTracking().Where(x => x.companyID == compID).ToList();
            tblTableBindingSource.DataSource = list;

        }

        private void lblAdd_Click(object sender, EventArgs e)
        {
            tblTableBindingSource.AddNew();
            pnlMain.Show();
            GetDocCode();
            txtPartyType.Focus();
            label3.Text = "ADD";
        }

        private void lblEdit_Click(object sender, EventArgs e)
        {
            tbl_Table obj = (tbl_Table)tblTableBindingSource.Current;
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
            tbl_Table us = (tbl_Table)tblTableBindingSource.Current;
            if (us.ID == 0)
            {
                tblTableBindingSource.RemoveCurrent();
                list = db.tbl_Table.Where(x => x.companyID == compID).ToList();
                tblTableBindingSource.DataSource = list;
            }
            else
            {

                tblTableBindingSource.Clear();
                var listcancel = db.tbl_Table.AsNoTracking().Where(x => x.companyID == compID).ToList();
                tblTableBindingSource.DataSource = listcancel;
            }
        }

        //public void alluser(string username)
        //{
        //    lblUserName.Text = username;
        //}
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPartyType.Text == "")
            { MessageBox.Show("Please Provide Table Name", "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
                tbl_Table obj = (tbl_Table)tblTableBindingSource.Current;

                //= list.Find(x => x.Name == txtPartyType.Text.Trim());
                var Currentobj = db.tbl_Table.ToList().Find(x => x.TableName == txtPartyType.Text.Trim() && x.companyID == compID);
                if (obj.ID == 0)
                {
                    if (Currentobj != null)
                    {
                        MessageBox.Show("Table Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    bool isCodeExist = list.Any(record =>
                                         record.TableName == obj.TableName &&
                                         record.ID != obj.ID &&
                                         record.companyID == compID);
                    if (isCodeExist)
                    {
                        MessageBox.Show("Table Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                obj.TableName = txtPartyType.Text.Trim();
                obj.isDeleted = chkIsActive.Checked;
                obj.companyID = compID;
                // obj.Address = txtAddress.Text.Trim();
                if (obj.ID == 0)
                {
                    db.tbl_Table.Add(obj);
                }
                else
                {
                    var result = db.tbl_Table.SingleOrDefault(b => b.ID == obj.ID);
                    if (result != null)
                    {
                        result.TableName = txtPartyType.Text.Trim();
                        result.isDeleted = chkIsActive.Checked;
                        result.companyID = compID;
                        //result.Address = txtAddress.Text.Trim();
                    }
                }
                db.SaveChanges();
                list = db.tbl_Table.AsNoTracking().Where(x => x.companyID == compID).ToList();
                tblTableBindingSource.DataSource = list;
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
                    MakerDataGridView.DataSource = list.FindAll(x => x.TableName.ToLower().Contains(toolStripTextBoxFind.Text.ToLower().Trim()));
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






    //: Form

    //    public Table()
    //    {
    //        InitializeComponent();
    //    }

}
