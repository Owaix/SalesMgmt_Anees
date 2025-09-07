using Lib.Entity;
using Lib.Utilities;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        ComboBox waiters = null;
        int waitervalue = 0;

        public TablsList(int Company, String tblStatus, string CurrentTbl, String _UsrID, Pos _pos, AspNetUsers _user, ComboBox waiter, string waiterID)
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
            waiters = waiter;
            waitervalue = Convert.ToInt32(waiterID);

            var waiterList = db.tbl_Employee.AsNoTracking().Where(x => x.ID == waitervalue && x.companyID == compID).ToList();

            if (waiterList.Count > 0)
            {
                cmbxWaiter.DataSource = waiterList;
                cmbxWaiter.DisplayMember = "EmployeName"; // Column Name
                cmbxWaiter.ValueMember = "ID";  // Column Name
                cmbxWaiter.SelectedIndex = 0;
            }
        }

        public void FillCombo<T>(ComboBox comboBox, IEnumerable<T> obj, String Name, String ID, int selected = 0)
        {
            if (obj.Count() > 0)
            {
                comboBox.DataSource = obj;
                comboBox.DisplayMember = Name; // Column Name
                comboBox.ValueMember = ID;  // Column Name
                comboBox.SelectedIndex = selected;
            }
        }

        private void TablsList_Load(object sender, EventArgs e)
        {
            //var waiterList = db.tbl_Employee.AsNoTracking().Where(x => x.companyID == compID).ToList();
            //waiterList.Insert(0, new tbl_Employee { ID = 0, EmployeName = "All" });
            //cmbxWaiter.DataSource = waiterList;
            //cmbxWaiter.DisplayMember = "EmployeName"; // Column Name
            //cmbxWaiter.ValueMember = "ID";  // Column Name
            //cmbxWaiter.SelectedIndex = 0;

            //generateTbl();
            //var order = db.tbl_Order.Where(x => x.isComplete == false).OrderByDescending(x => x.OrderDate).ToList();
            //if (order.Count > 0)
            //{
            //    foreach (var control in panel5.Controls)
            //    {
            //        if (control is MetroTile)
            //        {
            //            var ordr = order.Find(x => x.TblID.ToString() == ((MetroTile)control).Name);
            //            String waiter = String.Empty;
            //            if (ordr != null)
            //            {
            //                var waitr = db.tbl_Employee.Where(x => x.ID == ordr.WaiterID && x.companyID == compID).FirstOrDefault();
            //                waiter = waitr == null ? "" : waitr.EmployeName ?? "";
            //                ((MetroTile)control).BackColor = System.Drawing.Color.Red;
            //                ((MetroTile)control).TileTextFontSize = MetroFramework.MetroTileTextSize.Medium;
            //                ((MetroTile)control).Text = ((MetroTile)control).Text + Environment.NewLine + Convert.ToInt32(ordr.Amount) + Environment.NewLine + waiter + Environment.NewLine + ordr.OrderNo;
            //            }
            //        }
            //    }
            //}
        }

        private void generateTbl()
        {
            List<NewTable> Table = new List<NewTable>();





            var itemList = db.tbl_Table.AsNoTracking().Where(x => x.companyID == compID && x.isDeleted == false).ToList();



            var order = db.tbl_Order.Where(x => x.isComplete == false).OrderByDescending(x => x.OrderDate).ToList();



            int itemLen = itemList.Count - order.Count;


            for (int a = 0; a < itemList.Count; a++)
            {
                bool active = false;
                for (int b = 0; b < order.Count; b++)
                {

                    if (order[b].TblID == itemList[a].ID)
                    {

                        active = true;
                        break;
                    }
                }
                if (active)
                {


                }
                else
                {
                    NewTable NTable = new NewTable();
                    NTable.Sno = itemList[a].ID;
                    NTable.Table = itemList[a].TableName;
                    Table.Add(NTable);
                }


            }

            //var itemList = db.tbl_Table.AsNoTracking().Where(x => x.companyID == compID && x.isDeleted == false).ToList();

            int locX = 19;
            int locY = 100;

            double firstLoop = (double)Table.Count / 5;
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
                        tiles1.Name = Table[len].Sno.ToString();
                        tiles1.Size = new System.Drawing.Size(133, 99);
                        tiles1.TabIndex = 0;
                        tiles1.Text = Table[len].Table;
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
            var id = Convert.ToInt32(button.Name);
            int waiterId = Convert.ToInt32(cmbxWaiter.SelectedValue);

            var obj = db.tbl_Order.Where(x => x.CompanyID == User.AccessFailedCount && x.TblID == id && x.isComplete == false && x.WaiterID== waiterId).FirstOrDefault();
            // comment by Shahzaib 4/11/23 
            //if (obj != null)
            //{
               
            //}
            //else {

            //    MessageBox.Show("selected table is reserved by other user , please reopen table screen", "Table is Reserved", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //Pos pos = new Pos(userID);

            var chk = pos.TableSelected(button.Name, tblStat, lblTblID.Text, cmbxWaiter.SelectedValue.ToString(), waiters);
            if (chk)
            {
                pos.Show();
                this.Hide();
                pos.WindowState = FormWindowState.Maximized;
                //
            }
        }

        private void TablsList_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            //  Pos pos = new Pos(User, compID);            
            pos.Show();
            this.Hide();
            //pos.WindowState = FormWindowState.Maximized;
        }

        private void cmbxWaiter_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Control item in panel5.Controls.OfType<MetroTile>().ToList())
            {
                panel5.Controls.Remove(item);
            }

            //var dsa = Convert.ToInt32(cmbxWaiter.SelectedIndex);
            //if (dsa >= 1)
            //{
            int id = waitervalue;
            // int id = 1;
            int AllowTable = 1000;
            var emp = db.tbl_Employee.FirstOrDefault(x => x.ID == waitervalue && x.companyID == compID);
            if (emp != null)
            {
                AllowTable = emp.AllowTable ?? 1000;
            }
            var Query = string.Format(@"Select top {0} * from (
                                                Select distinct TableName , tbl.ID from tbl_Table tbl
                                                inner join tbl_Order ord on ord.TblID = tbl.ID
                                                Where isComplete = 0 and (WaiterID = {1})
                                                UNION 
                                                Select distinct TableName , ID from tbl_Table tbl
                                                left join tbl_Order ord on ord.TblID = tbl.ID
                                                Where ISNULL(isComplete,1) = 1 and tbl.companyID={2}
                                                and tbl.ID NOT IN (
                                                Select  tbl.ID from tbl_Table tbl
                                                left join tbl_Order ord on ord.TblID = tbl.ID
                                                where ISNULL(isComplete,1) = 0 and WaiterID <> {1})
                                           ) as ab", AllowTable, id, compID);
            SqlCommand cmd = new SqlCommand(Query, SqlHelper.DefaultSqlConnection);
            cmd.CommandType = CommandType.Text;
            var rows = SqlHelper.ExecuteDataset(cmd).Tables[0];

            //var itemList = (from a in db.tbl_Order
            //                join b in db.tbl_Table on a.TblID equals b.ID
            //                where a.WaiterID == id && a.CompanyID == compID
            //                select new
            //                {
            //                    ID = b.ID,
            //                    TableName = b.TableName,
            //                    isComplete = a.isComplete
            //                }).OrderBy(x => x.isComplete).Take(AllowTable).ToList();

            int locX = 19;
            int locY = 100;
            int itemLen = rows.Rows.Count;

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
                        tiles1.Name = rows.Rows[len]["ID"].ToString();
                        tiles1.Size = new System.Drawing.Size(133, 99);
                        tiles1.TabIndex = 0;
                        //tiles1.CompanyName = 
                        tiles1.Text = rows.Rows[len]["TableName"].ToString();
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
                            var waitr = db.tbl_Employee.Where(x => x.ID == ordr.WaiterID && x.companyID == compID).FirstOrDefault();
                            waiter = waitr == null ? "" : waitr.EmployeName ?? "";
                            ((MetroTile)control).BackColor = System.Drawing.Color.Red;
                            ((MetroTile)control).TileTextFontSize = MetroFramework.MetroTileTextSize.Medium;
                            ((MetroTile)control).Text = ((MetroTile)control).Text + Environment.NewLine + Convert.ToInt32(ordr.Amount) + Environment.NewLine + waiter + Environment.NewLine + ordr.OrderNo;
                        }
                    }
                }
            }
            //tiles1
            //  }

            //else if (dsa == 0)
            //{

            //    TablsList_Load(null, null);
            //    return;

            //}
        }

    }

    public class NewTable
    {

        public NewTable()
        {



        }

        public Nullable<int> Sno { get; set; }
        public string Table { get; set; }

    }
}