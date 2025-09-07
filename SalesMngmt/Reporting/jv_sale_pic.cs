using Lib.Entity;
using Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesMngmt.Reporting
{
    public partial class jv_sale_pic : Form
    {
        SaleManagerEntities db = null;
        public jv_sale_pic(int id)
        {
            InitializeComponent();
            db = new SaleManagerEntities();


            var tbl = db.JV_M.AsNoTracking().Where(x => x.RID == id).FirstOrDefault();

            string path = Application.StartupPath + "\\Img\\" + tbl.img;
            //string path = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10)) + "\\Img\\" + obj.BarCode_ID;
         
            
            // label10.Text = tbl.img;
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = Utillityfunctions.LoadImage(tbl.images);// Image.FromFile(path);
        }
    }
}
