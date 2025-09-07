using Lib.Entity;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SalesMngmt.Invoice
{
    public partial class TablsList : MetroFramework.Forms.MetroForm
    {
        SaleManagerEntities db = null;
        int compID = 0;
        List<tblStock> stock = null;
        String tblStat = "";
        String userID = "0";
        Pos pos = null;
        AspNetUsers User;

        public TablsList(int Company, String tblStatus, string CurrentTbl, String _UsrID, Pos _pos, AspNetUsers _user)
        {
            InitializeComponent();
            db = new SaleManagerEntities();
            compID = Company;
            stock = new List<tblStock>();
            tblStat = tblStatus;
            lblTblID.Text = CurrentTbl;
            userID = _UsrID;
            pos = _pos;
            User = _user;
        }

        private void TablsList_Load(object sender, EventArgs e)
        {
            generateTbl();
            var order = db.tbl_Order.Where(x => x.isComplete == false).OrderByDescending(x => x.OrderDate).ToList();
            if (order.Count > 0)
            {
                foreach (var control in panel5.Controls)
                {
                    if (control is MetroTile)
                    {
                        var ordr = order.Find(x => x.TblID.ToString() == ((MetroTile)control).Name);
                        String waiter = String.Empty;
                        if (ordr != null)
                        {
                            var waitr = db.tbl_Employee.Where(x => x.ID == ordr.WaiterID).FirstOrDefault();
                            waiter = waitr == null ? "" : waitr.EmployeName ?? "";
                            ((MetroTile)control).BackColor = System.Drawing.Color.Red;
                            ((MetroTile)control).Text = ((MetroTile)control).Text + Environment.NewLine + Convert.ToInt32(ordr.Amount) + Environment.NewLine + waiter;
                        }
                    }
                }
            }
        }

        private void generateTbl()
        {
            var itemList = db.tbl_Table.ToList();
            int locX = 19;
            int locY = 24;
            int itemLen = itemList.Count;

            double firstLoop = (double)itemLen / 5;
            double y = firstLoop - Math.Truncate(firstLoop);
            if (y > 0.0001)
            {
                firstLoop += Convert.ToInt32(y);
            }
            firstLoop = firstLoop == 0 ? 1 : firstLoop;
            int len = 0;
            for (int i = 0; i < firstLoop; i++)
            {
                locX = 19;
                for (int j = 0; j < 5; j++)
                {
                    try
                    {
                        var tiles1 = new MetroTile();
                        tiles1.ActiveControl = null;
                        tiles1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
                        tiles1.Location = new System.Drawing.Point(locX, locY);
                        tiles1.Name = itemList[len].ID.ToString();
                        tiles1.Size = new System.Drawing.Size(133, 99);
                        tiles1.TabIndex = 0;
                        tiles1.Text = itemList[len].TableName;
                        tiles1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                        tiles1.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall;
                        tiles1.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold;
                        tiles1.UseCustomBackColor = true;
                        tiles1.UseSelectable = true;
                        tiles1.Click += new EventHandler(MetroTile_Click);
                        panel5.Controls.Add(tiles1);
                        locX += 149;
                        len += 1;

                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
                locY += 116;
            }
            //tiles1
        }

        private void MetroTile_Click(object sender, EventArgs e)
        {
            MetroTile button = sender as MetroTile;
            //Pos pos = new Pos(userID);

            var chk = pos.TableSelected(button.Name, tblStat, lblTblID.Text);
            if (chk)
            {
                pos.Show();
                this.Hide();
                //pos.WindowState = FormWindowState.Maximized;
                //
            }
        }

        private void TablsList_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            Pos pos = new Pos(User);
            this.Hide();
            pos.Show();
            //pos.WindowState = FormWindowState.Maximized;
        }
    }
}
