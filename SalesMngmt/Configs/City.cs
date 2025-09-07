using Lib;
using Lib.Entity;
using Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SalesMngmt.Configs
{
    public partial class City : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<tbl_city> list = null;
        int compID = 0;

        public City(int cmpID)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            compID = cmpID;
        }

        private void Category_Load(object sender, EventArgs e)
        {
            //cat type = new cat();
            //DataAccess access = new DataAccess();
            //type.PType_ID = 1;
            //var con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            //con.Open();
            //SqlTransaction trans = con.BeginTransaction();
            //var objcat = access.Get<cat>("Sp_cat_Select", type, con, trans);

            pnlMain.Hide();
            list = db.tbl_city.Where(x => x.CompyID == compID).ToList();
            tblcityBindingSource.DataSource = list;
        }

        private void lblAdd_Click(object sender, EventArgs e)
        {
            tblcityBindingSource.AddNew();
            pnlMain.Show();
            GetDocCode();
            txtcat.Focus();
            label3.Text = "ADD";
        }

        private void lblEdit_Click(object sender, EventArgs e)
        {
            tbl_city obj = (tbl_city)tblcityBindingSource.Current;
            pnlMain.Show();
            txtcat.Focus();
            label3.Text = "EDIT";
        }

        #region -- Global variables start --

        string docCode;

        #endregion -- Global variable end --


        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlMain.Hide();
            tbl_city us = (tbl_city)tblcityBindingSource.Current;
            if (us.Id == 0)
            {
                tblcityBindingSource.RemoveCurrent();
            }
        }

        //public void alluser(string username)
        //{
        //    lblUserName.Text = username;
        //}
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtcat.Text == "")
            { MessageBox.Show("Please Provide Party Type", "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
                tbl_city obj = (tbl_city)tblcityBindingSource.Current;

                var Currentobj = list.Find(x => x.CityName == txtcat.Text.Trim());
                if (obj.Id == 0)
                {
                    if (Currentobj != null)
                    {
                        MessageBox.Show("City Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    bool isCodeExist = list.Any(record =>
                                         record.CityName == obj.CityName &&
                                         record.Id != obj.Id);
                    if (isCodeExist)
                    {
                        MessageBox.Show("City Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                obj.CityName = txtcat.Text.Trim();
                obj.isDeleted = chkIsActive.Checked;
                if (obj.Id == 0)
                {
                    obj.CompyID = compID;
                    db.tbl_city.Add(obj);
                }
                else
                {
                    var result = db.tbl_city.SingleOrDefault(b => b.Id == obj.Id);
                    if (result != null)
                    {
                        result.CityName = txtcat.Text.Trim();
                        result.isDeleted = chkIsActive.Checked;
                    }
                }
                db.SaveChanges();
                pnlMain.Hide();
            }
        }
        private void catDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CategorysDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        #region -- Helper Method Start --
        public void GetDocCode()
        {
            //catList obj = new catList(new cat { }.Select());
            //docCode = "DOC-" + (obj.Count + 1);
        }

        private void toolStripTextBoxFind_Leave(object sender, EventArgs e)
        {
            try
            {
                if (toolStripTextBoxFind.Text.Trim().Length == 0) { CategorysDataGridView.DataSource = list; }
                else
                {
                    CategorysDataGridView.DataSource = list.FindAll(x => x.CityName.ToLower().Contains(toolStripTextBoxFind.Text.ToLower().Trim()));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //
        #endregion -- Helper Method End --
    }
}
