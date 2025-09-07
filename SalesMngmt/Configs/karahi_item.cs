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
    public partial class karahi_item : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<tbl_karahi_Item> list = null;
        int compID = 0;

        public karahi_item(int cmpID)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            compID = cmpID;
        }

        private void Unit_Load(object sender, EventArgs e)
        {
            pnlMain.Hide();
            list = db.tbl_karahi_Item.AsNoTracking().Where(x => x.Company == compID).ToList();
            tblkarahiItemBindingSource.DataSource = list;
        }

        private void lblAdd_Click(object sender, EventArgs e)
        {
            tblkarahiItemBindingSource.AddNew();
            pnlMain.Show();
            GetDocCode();
            txtUnit.Focus();
            label3.Text = "ADD";
        }

        private void lblEdit_Click(object sender, EventArgs e)
        {
            tbl_karahi_Item obj = (tbl_karahi_Item)tblkarahiItemBindingSource.Current;
            pnlMain.Show();
            txtUnit.Focus();
            label3.Text = "EDIT";
        }

        #region -- Global variables start --

        string docCode;

        #endregion -- Global variable end --


        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlMain.Hide();
            tbl_karahi_Item us = (tbl_karahi_Item)tblkarahiItemBindingSource.Current;
            if (us.Id == 0)
            {
                tblkarahiItemBindingSource.RemoveCurrent();
            }
            else
            {

                tblkarahiItemBindingSource.Clear();
                var listcancel = db.tbl_karahi_Item.AsNoTracking().Where(x => x.Company == compID).ToList();
                tblkarahiItemBindingSource.DataSource = listcancel;
            }
        }

        //public void alluser(string username)
        //{
        //    lblUserName.Text = username;
        //}
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtUnit.Text == "")
            { MessageBox.Show("Please Provide Karahi Name", "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
                tbl_karahi_Item obj = (tbl_karahi_Item)tblkarahiItemBindingSource.Current;

                var Currentobj = db.tbl_karahi_Item.ToList().Find(x => x.ItemName == txtUnit.Text.Trim() && x.Company == compID);
                if (obj.Id == 0)
                {
                    if (Currentobj != null)
                    {
                        MessageBox.Show("Karahi Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    bool isCodeExist = list.Any(record =>
                                         record.ItemName == obj.ItemName &&
                                         record.Id != obj.Id &&
                                         record.Company == compID);
                    if (isCodeExist)
                    {
                        MessageBox.Show("Karahi Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                obj.ItemName = txtUnit.Text.Trim();
                obj.Active = chkIsActive.Checked;
             
                //obj. = DateTime.Now;
                obj.Company = compID;
                if (obj.Id == 0)
                {
                    db.tbl_karahi_Item.Add(obj);
                }
                else
                {
                    var result = db.tbl_karahi_Item.SingleOrDefault(b => b.Id == obj.Id);
                    if (result != null)
                    {
                        result.ItemName = txtUnit.Text.Trim();
                        result.Active = chkIsActive.Checked;
                       
                    }
                }
                db.SaveChanges();
                pnlMain.Hide();
            }
        }
        private void UnitDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            UnitDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        #region -- Helper Method Start --
        public void GetDocCode()
        {
            //UnitList obj = new UnitList(new Unit { }.Select());
            //docCode = "DOC-" + (obj.Count + 1);
        }

        private void toolStripTextBoxFind_Leave(object sender, EventArgs e)
        {
            try
            {
                if (toolStripTextBoxFind.Text.Trim().Length == 0) { UnitDataGridView.DataSource = list; }
                else
                {
                    UnitDataGridView.DataSource = list.FindAll(x => x.ItemName.ToLower().Contains(toolStripTextBoxFind.Text.ToLower().Trim()));
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
      
    }
}
