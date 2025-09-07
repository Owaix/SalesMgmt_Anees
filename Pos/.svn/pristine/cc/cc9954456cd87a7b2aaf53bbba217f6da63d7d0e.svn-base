using Lib.Entity;
using Lib.Model;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Zen.Barcode;

namespace SalesMngmt.Reporting
{
    public partial class Form1 : Form
    {
        SaleManagerEntities db = null;
        List<Lib.Entity.Items> Items = null;
        public Form1()
        {
            InitializeComponent();
            db = new SaleManagerEntities();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Items = db.Items.ToList();
            cmbxProducts.DataSource = Items;
            cmbxProducts.DisplayMember = "IName";
            cmbxProducts.ValueMember = "IID";
            cmbxProducts.SelectedIndex = -1;
            this.reportViewer1.RefreshReport();
        }
        private void AddSubQty(int IID, int inc, string Flag)
        {
            bool isAdd = true;
            var quantity = txtQuantity.Text == "" ? "1" : txtQuantity.Text;
            foreach (DataGridViewRow row in gvBar.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    var itemID = Convert.ToInt32(row.Cells[0].Value.ToString() == "" ? "0" : row.Cells[0].Value);
                    if (IID == itemID)
                    {
                        row.Cells[0].Value = cmbxProducts.SelectedValue;
                        row.Cells[1].Value = cmbxProducts.Text;
                        row.Cells[2].Value = quantity;
                        isAdd = false;
                    }
                }
            }
            if (isAdd == true)
            {
                this.gvBar.Rows.Add(cmbxProducts.SelectedValue, cmbxProducts.Text, quantity);
            }
        }

        private void gvBar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var senderGrid = (DataGridView)sender;
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 3)
                    {
                        gvBar.Rows.RemoveAt(e.RowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Try Again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddSubQty(Convert.ToInt32(cmbxProducts.SelectedValue), 0, "dec");
            cmbxProducts.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                List<Barcode> items = new List<Barcode>();
                foreach (DataGridViewRow row in gvBar.Rows)
                {
                    var len = Convert.ToInt32(row.Cells[2].Value);
                    for (int i = 0; i < len; i++)
                    {
                        Barcode item = new Barcode();
                        item.IID = Convert.ToInt32(row.Cells[0].Value);
                        var Item = Items.Where(x => x.IID == item.IID).FirstOrDefault();
                        item.Quantity = Convert.ToInt32(row.Cells[2].Value);
                        item.SalesPrice = Convert.ToDouble(Item.SalesPrice);
                        item.IName = Item.IName;
                        var Articles = db.Article.Where(x => x.ProductID == item.IID).FirstOrDefault();
                        if (Articles != null)
                        {
                            item.ArticleNo = Articles.ArticleNo;
                        }
                        CheckSum_8221(Item.BarcodeNo);
                        item.BarCodeNo = "file:" + $"{Application.StartupPath}\\Barcode\\{Item.BarcodeNo}.png";
                        items.Add(item);
                    }
                }
                reportViewer1.LocalReport.EnableExternalImages = true;
                string path = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", "") + "\\Reporting\\Definition\\Barcode.rdlc";
                this.reportViewer1.LocalReport.ReportPath = path; //@"C:\Users\champ\Downloads\SalesMgmt\SalesMngmt\Reporting\Definition\Barcode.rdlc";
                //reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("barCode", "file:" + items[0].IName));
                this.reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Ds", items));
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckSum_8221(string labNo)
        {
            Code128BarcodeDraw zbc = BarcodeDrawFactory.Code128WithChecksum;
            System.Drawing.Image img2 = zbc.Draw(labNo, 60);
            System.IO.MemoryStream ms2 = new System.IO.MemoryStream();
            img2.Save(ms2, System.Drawing.Imaging.ImageFormat.Png);
            System.Windows.Forms.PictureBox pb = new PictureBox();
            img2 = Rotate_Graph90(img2);
            pb.Image = img2;
            var das = Application.StartupPath;
            pb.Image.Save(Application.StartupPath + "\\Barcode\\" + labNo.Replace("/", "").Replace("\\", "") + ".png");
        }
        public Image Rotate_Graph90(Image img)
        {
            var bmp = new Bitmap(img);
            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                gfx.Clear(System.Drawing.Color.White);
                gfx.DrawImage(img, 0, 0, img.Width, img.Height);
            }
            bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
            return bmp;
        }
    }
}