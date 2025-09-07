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
    public partial class PartyType : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<Lib.Entity.Party_Type> list = null;
        int compID = 0;



        public PartyType(int company)
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
            list = db.Party_Type.AsNoTracking().Where(x=>x.CompanyID== compID).ToList();
            partyTypeBindingSource.DataSource = list;
        }

        private void lblAdd_Click(object sender, EventArgs e)
        {
            partyTypeBindingSource.AddNew();
            pnlMain.Show();
            GetDocCode();
            txtcat.Focus();
            label3.Text = "ADD";
        }

        private void lblEdit_Click(object sender, EventArgs e)
        {
            Lib.Entity.Party_Type obj = (Lib.Entity.Party_Type)articleTypeBindingSource.Current;
            pnlMain.Show();
            txtcat.Focus();
            label3.Text = "EDIT";
        }

        #region -- Global variables start --

        string docCode;

        #endregion -- Global variable end --


        private void btnCancel_Click(object sender, EventArgs e)
        {
           
        }

        //public void alluser(string username)
        //{
        //    lblUserName.Text = username;
        //}
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtcat.Text == "")
            { MessageBox.Show("Please Provide PartyTypeName", "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
                Lib.Entity.Party_Type obj = (Lib.Entity.Party_Type)partyTypeBindingSource.Current;

                var Currentobj = db.Party_Type.ToList().Find(x => x.Party_Type1 == txtcat.Text.Trim() && x.CompanyID==compID);// list.Find(x => x.Name == txtcat.Text.Trim());
                if (obj.PType_ID == 0)
                {
                    if (Currentobj != null)
                    {
                        MessageBox.Show("This Party Type Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    bool isCodeExist = list.Any(record =>
                                         record.Party_Type1 == obj.Party_Type1 &&
                                         record.PType_ID != obj.PType_ID    && record.CompanyID==compID);
                    if (isCodeExist)
                    {
                        MessageBox.Show("Party Type Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                obj.Party_Type1 = txtcat.Text.Trim();
                obj.isDeleted = chkIsActive.Checked;
                obj.CompanyID = compID;
                if (obj.PType_ID == 0)
                {
                    db.Party_Type.Add(obj);
                }
                else
                {
                    var result = db.Party_Type.SingleOrDefault(b => b.PType_ID == obj.PType_ID && b.CompanyID==compID);
                    if (result != null)
                    {
                        result.Party_Type1 = txtcat.Text.Trim();
                        result.isDeleted = chkIsActive.Checked;
                    }
                }
                db.SaveChanges();
                pnlMain.Hide();


                list = db.Party_Type.AsNoTracking().Where(x=>x.CompanyID==compID).ToList();
                partyTypeBindingSource.DataSource = list;
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
                    CategorysDataGridView.DataSource = list.FindAll(x => x.Party_Type1.ToLower().Contains(toolStripTextBoxFind.Text.ToLower().Trim()));
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

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            pnlMain.Hide();
            Lib.Entity.Party_Type us = (Lib.Entity.Party_Type)partyTypeBindingSource.Current;
            if (us.PType_ID == 0)
            {
                partyTypeBindingSource.RemoveCurrent();
                list = db.Party_Type.Where(x=>x.CompanyID==compID).ToList();
                partyTypeBindingSource.DataSource = list;
            }
            else
            {

                partyTypeBindingSource.Clear();
                var listcancel = db.Party_Type.AsNoTracking().Where(x => x.CompanyID == compID).ToList();
                partyTypeBindingSource.DataSource = listcancel;


            }

        }
    }
    //{
    //    public PartyType()
    //    {
    //        InitializeComponent();
    //    }
    //}
}
