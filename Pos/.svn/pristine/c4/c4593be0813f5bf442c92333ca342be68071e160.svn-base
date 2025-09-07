using Lib.Entity;
using Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SalesMngmt.Configs
{
    public partial class Catgory : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        List<Items_Cat> list = null;
        int compID = 0;

        public Catgory(int cmpID)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            compID = cmpID;
        }

        private void Catgory_Load(object sender, EventArgs e)
        {
            pnlMain.Hide();
            list = db.Items_Cat.ToList();
            itemBindingSource.DataSource = list;
        }

        private void lblAdd_Click(object sender, EventArgs e)
        {
            itemBindingSource.AddNew();
            pnlMain.Show();
            GetDocCode();
            txtCategory.Focus();
            label3.Text = "ADD";
            string path = Application.StartupPath + "\\Img\\124444444.png";
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = Image.FromFile(path);
        }

        private void lblEdit_Click(object sender, EventArgs e)
        {
            Items_Cat obj = (Items_Cat)itemBindingSource.Current;
            pnlMain.Show();
            txtCategory.Focus();
            label3.Text = "EDIT";
            string path = Application.StartupPath + "\\Img\\" + obj.img;
            //string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10)) + "\\Img\\" + obj.BarCode_ID;
            label10.Text = obj.img;
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = Utillityfunctions.LoadImage(obj.Images);// Image.FromFile(path);
        }


        #region -- Global variables start --

        string docCode;

        #endregion -- Global variable end --


        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlMain.Hide();
            Items_Cat us = (Items_Cat)itemBindingSource.Current;
            if (us.CatID == 0)
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
            if (txtCategory.Text == "")
            { MessageBox.Show("Please Provide Party Type", "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
                Items_Cat obj = (Items_Cat)itemBindingSource.Current;

                var Currentobj = db.Items_Cat.Where(x => x.Cat == txtCategory.Text.Trim()).FirstOrDefault();
                if (obj.CatID == 0)
                {
                    if (Currentobj != null)
                    {
                        MessageBox.Show("Vender Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    bool isCodeExist = list.Any(record =>
                                         record.Cat == obj.Cat &&
                                         record.CatID != obj.CatID);
                    if (isCodeExist)
                    {
                        MessageBox.Show("Vender Name Already Exists", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                string path = Application.StartupPath;
                // string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
                string filename = System.IO.Path.GetFileName(openFileDialog1.FileName);

                if (obj.CatID == 0)
                {
                    if (filename == null)
                    {
                        MessageBox.Show("Please select a valid image.");
                        return;
                    }
                    else
                    {
                        try
                        {
                            //System.IO.File.Copy(openFileDialog1.FileName, path + "\\Img\\" + filename);
                        }
                        catch (Exception)
                        {

                        }
                    }
                }

                obj.Cat = txtCategory.Text.Trim();
                obj.isDeleted = chkIsActive.Checked;
                obj.img = filename;
                obj.CompanyID = compID;
                obj.Images = Utillityfunctions.ToBase64(openFileDialog1.FileName , path + "\\Img\\" + filename);
                if (obj.CatID == 0)
                {
                    db.Items_Cat.Add(obj);
                }
                else
                {
                    var result = db.Items_Cat.SingleOrDefault(b => b.CatID == obj.CatID);
                    if (result != null)
                    {
                        result.Cat = txtCategory.Text.Trim();
                        result.isDeleted = chkIsActive.Checked;
                    }
                }
                db.SaveChanges();
                pnlMain.Hide();
            }
        }
        private void CategoryDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CategoryDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "C://Desktop";
            openFileDialog1.Title = "Select image to be upload.";
            openFileDialog1.Filter = "Image Only(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            openFileDialog1.FilterIndex = 1;
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        string path = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                        label10.Text = path;
                        pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
                else
                {
                    MessageBox.Show("Please Upload image.");
                }
            }
            catch (Exception ex)
            {
                //it will give if file is already exits..
                MessageBox.Show(ex.Message);
            }
        }

        #region -- Helper Method Start --
        public void GetDocCode()
        {
            //CategoryList obj = new CategoryList(new Category { }.Select());
            //docCode = "DOC-" + (obj.Count + 1);
        }

        private void toolStripTextBoxFind_Leave(object sender, EventArgs e)
        {
            try
            {
                if (toolStripTextBoxFind.Text.Trim().Length == 0) { CategoryDataGridView.DataSource = list; }
                else
                {
                    List<Items_Cat> filteredList = new List<Items_Cat>();
                    foreach (var item in list)
                    {
                        if (item.Cat.Contains(toolStripTextBoxFind.Text))
                        {
                            filteredList.Add(item);
                        }
                    }
                    CategoryDataGridView.DataSource = filteredList;
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
