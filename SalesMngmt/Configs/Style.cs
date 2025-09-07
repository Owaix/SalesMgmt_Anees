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
    public partial class Style : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<Lib.Entity.Styles> list = null;
        int compID = 0;



        public Style(int company)
        {
            InitializeComponent();

            db = new SaleManagerEntities();
            compID = company;

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
            list = db.Styles.Where(x=>x.CompanyID== compID).ToList();
            styleBindingSource.DataSource = list;
        }

        private void lblAdd_Click(object sender, EventArgs e)
        {
            styleBindingSource.AddNew();
            pnlMain.Show();
            GetDocCode();
            txtcat.Focus();
            label3.Text = "ADD";
        }

        private void lblEdit_Click(object sender, EventArgs e)
        {
            Lib.Entity.Styles obj = (Lib.Entity.Styles)styleBindingSource.Current;
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
            Lib.Entity.Styles us = (Lib.Entity.Styles)styleBindingSource.Current;
            if (us.StyleID == 0)
            {
                styleBindingSource.RemoveCurrent();
                list = db.Styles.Where(x => x.CompanyID == compID).ToList();
                styleBindingSource.DataSource = list;
            }
            else
            {

                styleBindingSource.Clear();
                var listcancel = db.Styles.AsNoTracking().Where(x => x.CompanyID == compID).ToList();
                styleBindingSource.DataSource = listcancel;
            }
        }

        //public void alluser(string username)
        //{
        //    lblUserName.Text = username;
        //}
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtcat.Text == "")
            { MessageBox.Show("Please Provide styple", "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
                Lib.Entity.Styles obj = (Lib.Entity.Styles)styleBindingSource.Current;

                var Currentobj = db.Styles.ToList().Find(x => x.StyleName == txtcat.Text.Trim() && x.CompanyID == compID);// list.Find(x => x.Name == txtcat.Text.Trim());
                if (obj.StyleID == 0)
                {
                    if (Currentobj != null)
                    {
                        MessageBox.Show("Style Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    bool isCodeExist = list.Any(record =>
                                         record.StyleName == obj.StyleName &&
                                         record.StyleID != obj.StyleID
                                         && record.CompanyID == compID);
                    if (isCodeExist)
                    {
                        MessageBox.Show("Style Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                obj.StyleName = txtcat.Text.Trim();
                obj.IsDeleted = chkIsActive.Checked;
                obj.CompanyID = compID;
                if (obj.StyleID == 0)
                {
                    db.Styles.Add(obj);
                }
                else
                {
                    var result = db.Styles.SingleOrDefault(b => b.StyleID == obj.StyleID && obj.CompanyID == compID);
                    if (result != null)
                    {
                        result.StyleName = txtcat.Text.Trim();
                        result.IsDeleted = chkIsActive.Checked;
                    }
                }
                db.SaveChanges();
                pnlMain.Hide();

                list = db.Styles.AsNoTracking().Where(x => x.CompanyID == compID).ToList();
                styleBindingSource.DataSource = list;
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
                    CategorysDataGridView.DataSource = list.FindAll(x => x.StyleName.ToLower().Contains(toolStripTextBoxFind.Text.ToLower().Trim()));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //
        #endregion -- Helper Method End --

        private void CategorysDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //    public Style()
        //    {
        //        InitializeComponent();
        //    }
        //}
    } }
