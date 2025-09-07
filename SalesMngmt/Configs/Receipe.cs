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
    public partial class Receipe : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        int id =0;
        int compy = 0;
        public Receipe(int company)
        {
            compy = company;

            InitializeComponent();
            db = new SaleManagerEntities();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
       


        }

        private void Receipe_Load(object sender, EventArgs e)
        {
            List<Items> receipe = new List<Items>();
            receipe.Add(new Items { IID = 0, IName = "--SELECT--" });
            receipe.AddRange(db.Items.AsNoTracking().Where(x=>x.isDeleted==false && x.CompanyID==compy).ToList());
            FillCombo(cmbxReceipe, receipe, "IName", "IID", 0);

            List<Items> Ingedient = new List<Items>();
            Ingedient.Add(new Items { IID = 0, IName = "--SELECT--" });
            Ingedient.AddRange(db.Items.AsNoTracking().Where(x => x.isDeleted == false && x.CompanyID == compy).ToList());

            FillCombo(combxIngredient, Ingedient, "IName", "IID", 0);
            gridview();
        }


        public void FillCombo(ComboBox comboBox, object obj, String Name, String ID, int selected = 1)
        {
            try
            {
                comboBox.DataSource = obj;
                comboBox.DisplayMember = Name; // Column Name
                comboBox.ValueMember = ID;  // Column Name
                comboBox.SelectedValue = selected;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (id == 0)
            {
            

                tbl_Receipe receipe = new tbl_Receipe();
                receipe.ItemID = Convert.ToInt32(cmbxReceipe.SelectedValue);
                receipe.qty = Convert.ToInt32(txtQty.Text);
                receipe.RecipceItemId = Convert.ToInt32(combxIngredient.SelectedValue);
                receipe.setup = txtStep.Text;
                receipe.isActive = chkIsActive.Checked;
                receipe.Description = txtDescribtion.Text;
                receipe.compy = compy;

                db.tbl_Receipe.Add(receipe);
                db.SaveChanges();

                gridview();
            }
            else {
                MessageBox.Show("cant insert ..you are in updated mode.");
            }
        }

        public void gridview() {
            dataGridView1.Rows.Clear();
            int itemID = Convert.ToInt32(cmbxReceipe.SelectedValue);

            var record = db.tbl_Receipe.Where(x => x.ItemID == itemID).ToList();

        int count = record.Count();

            for (int a = 0; a < count; a++)
            {
                int itemid = Convert.ToInt32(record[a].ItemID);
                int ingredient = Convert.ToInt32(record[a].RecipceItemId);
                var item = db.Items.Where(x => x.IID == itemid).FirstOrDefault();
                var itemDetail = db.Items.Where(x => x.IID == ingredient).FirstOrDefault();

                dataGridView1.Rows.Add(item.IName, itemDetail.IName,record[a].qty,record[a].setup, record[a].Description, record[a].isActive, record[a].ItemID, record[a].RecipceItemId, record[a].RID);

            }

        }

        private void cmbxReceipe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbxReceipe.SelectedIndex != 0)
            {
                gridview();
            }
           

            }
           
        

        private void cmbxReceipe_SelectedValueChanged(object sender, EventArgs e)
        {
          // gridview();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var result = db.tbl_Receipe.SingleOrDefault(b => b.RID == id && b.compy==compy);
            if (result != null)
            {
                result.ItemID = Convert.ToInt32(cmbxReceipe.SelectedValue);
                result.qty = Convert.ToInt32(txtQty.Text);
                result.RecipceItemId = Convert.ToInt32(combxIngredient.SelectedValue);
                result.setup = txtStep.Text;
                result.isActive = chkIsActive.Checked;
               result.Description = txtDescribtion.Text;
                result.compy = compy;
                db.SaveChanges();
                gridview();
                id = 0;
            }
          

           
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            List<Items> receipe = new List<Items>();
            receipe.Add(new Items { IID = 0, IName = "--SELECT--" });
            receipe.AddRange(db.Items.AsNoTracking().Where(x => x.isDeleted == false && x.CompanyID == compy).ToList());
            FillCombo(cmbxReceipe, receipe, "IName", "IID", 0);

            List<Items> Ingedient = new List<Items>();
            Ingedient.Add(new Items { IID = 0, IName = "--SELECT--" });
            Ingedient.AddRange(db.Items.AsNoTracking().Where(x => x.isDeleted == false && x.CompanyID == compy).ToList());

            FillCombo(combxIngredient, Ingedient, "IName", "IID", 0);
            gridview();
            id = 0;
            chkIsActive.Checked = false;
            txtQty.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cmbxReceipe.SelectedValue = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
            combxIngredient.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[7].Value;
            txtQty.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtStep.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtDescribtion.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            string check = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            if (check == "False")
            {
                chkIsActive.Checked = false;
            }
            else
            {
                chkIsActive.Checked = true;
            }
            id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
        }
    }
}
