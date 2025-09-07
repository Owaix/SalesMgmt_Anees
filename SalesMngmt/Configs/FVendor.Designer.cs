using Lib.Entity;

namespace SalesMngmt.Configs
{
    partial class FVendor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FVendor));
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.VendDataGridView = new System.Windows.Forms.DataGridView();
            this.aCCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vendNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nTNNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contactPersonDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cellDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emlDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.telDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.debitDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.creditDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.landDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.villageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inActiveDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ledger = new System.Windows.Forms.DataGridViewLinkColumn();
            this.VendBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.VendBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoVendextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lblAdd = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblEdit = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.pnlMain = new MetroFramework.Controls.MetroPanel();
            this.cmbxZone = new MetroFramework.Controls.MetroComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCredit = new MetroFramework.Controls.MetroTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtDebit = new MetroFramework.Controls.MetroTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtNTN = new MetroFramework.Controls.MetroTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCell = new MetroFramework.Controls.MetroTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtContact = new MetroFramework.Controls.MetroTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbPType = new MetroFramework.Controls.MetroComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMail = new MetroFramework.Controls.MetroTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTel = new MetroFramework.Controls.MetroTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAddress = new MetroFramework.Controls.MetroTextBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new MetroFramework.Controls.MetroButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtVndrName = new MetroFramework.Controls.MetroTextBox();
            this.btnSave = new MetroFramework.Controls.MetroButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbxCity = new MetroFramework.Controls.MetroComboBox();
            this.metroPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VendDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VendBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VendBindingNavigator)).BeginInit();
            this.VendBindingNavigator.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.VendDataGridView);
            this.metroPanel1.Controls.Add(this.VendBindingNavigator);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(20, 60);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(760, 370);
            this.metroPanel1.TabIndex = 0;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // VendDataGridView
            // 
            this.VendDataGridView.AllowUserToAddRows = false;
            this.VendDataGridView.AllowUserToDeleteRows = false;
            this.VendDataGridView.AllowUserToResizeColumns = false;
            this.VendDataGridView.AllowUserToResizeRows = false;
            this.VendDataGridView.AutoGenerateColumns = false;
            this.VendDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.VendDataGridView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Book Antiqua", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.VendDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.VendDataGridView.ColumnHeadersHeight = 44;
            this.VendDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.aCCodeDataGridViewTextBoxColumn,
            this.vendNameDataGridViewTextBoxColumn,
            this.nTNNoDataGridViewTextBoxColumn,
            this.contactPersonDataGridViewTextBoxColumn,
            this.cellDataGridViewTextBoxColumn,
            this.emlDataGridViewTextBoxColumn,
            this.telDataGridViewTextBoxColumn,
            this.debitDataGridViewTextBoxColumn,
            this.creditDataGridViewTextBoxColumn,
            this.addDataGridViewTextBoxColumn,
            this.landDataGridViewTextBoxColumn,
            this.cityDataGridViewTextBoxColumn,
            this.villageDataGridViewTextBoxColumn,
            this.inActiveDataGridViewTextBoxColumn,
            this.ledger});
            this.VendDataGridView.DataSource = this.VendBindingSource;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Tan;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.VendDataGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.VendDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VendDataGridView.EnableHeadersVisualStyles = false;
            this.VendDataGridView.Location = new System.Drawing.Point(0, 25);
            this.VendDataGridView.MultiSelect = false;
            this.VendDataGridView.Name = "VendDataGridView";
            this.VendDataGridView.ReadOnly = true;
            this.VendDataGridView.RowHeadersVisible = false;
            this.VendDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.VendDataGridView.Size = new System.Drawing.Size(760, 345);
            this.VendDataGridView.TabIndex = 3;
            this.VendDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.VendDataGridView_CellContentClick);
            // 
            // aCCodeDataGridViewTextBoxColumn
            // 
            this.aCCodeDataGridViewTextBoxColumn.DataPropertyName = "AC_Code";
            this.aCCodeDataGridViewTextBoxColumn.HeaderText = "AC Code";
            this.aCCodeDataGridViewTextBoxColumn.Name = "aCCodeDataGridViewTextBoxColumn";
            this.aCCodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vendNameDataGridViewTextBoxColumn
            // 
            this.vendNameDataGridViewTextBoxColumn.DataPropertyName = "VendName";
            this.vendNameDataGridViewTextBoxColumn.HeaderText = "Vendor";
            this.vendNameDataGridViewTextBoxColumn.Name = "vendNameDataGridViewTextBoxColumn";
            this.vendNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // nTNNoDataGridViewTextBoxColumn
            // 
            this.nTNNoDataGridViewTextBoxColumn.DataPropertyName = "NTN_No";
            this.nTNNoDataGridViewTextBoxColumn.HeaderText = "NTN No";
            this.nTNNoDataGridViewTextBoxColumn.Name = "nTNNoDataGridViewTextBoxColumn";
            this.nTNNoDataGridViewTextBoxColumn.ReadOnly = true;
            this.nTNNoDataGridViewTextBoxColumn.Visible = false;
            // 
            // contactPersonDataGridViewTextBoxColumn
            // 
            this.contactPersonDataGridViewTextBoxColumn.DataPropertyName = "ContactPerson";
            this.contactPersonDataGridViewTextBoxColumn.HeaderText = "Contact Person";
            this.contactPersonDataGridViewTextBoxColumn.Name = "contactPersonDataGridViewTextBoxColumn";
            this.contactPersonDataGridViewTextBoxColumn.ReadOnly = true;
            this.contactPersonDataGridViewTextBoxColumn.Visible = false;
            // 
            // cellDataGridViewTextBoxColumn
            // 
            this.cellDataGridViewTextBoxColumn.DataPropertyName = "Cell";
            this.cellDataGridViewTextBoxColumn.HeaderText = "Cell";
            this.cellDataGridViewTextBoxColumn.Name = "cellDataGridViewTextBoxColumn";
            this.cellDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // emlDataGridViewTextBoxColumn
            // 
            this.emlDataGridViewTextBoxColumn.DataPropertyName = "Eml";
            this.emlDataGridViewTextBoxColumn.HeaderText = "Email";
            this.emlDataGridViewTextBoxColumn.Name = "emlDataGridViewTextBoxColumn";
            this.emlDataGridViewTextBoxColumn.ReadOnly = true;
            this.emlDataGridViewTextBoxColumn.Visible = false;
            // 
            // telDataGridViewTextBoxColumn
            // 
            this.telDataGridViewTextBoxColumn.DataPropertyName = "Tel";
            this.telDataGridViewTextBoxColumn.HeaderText = "Tel";
            this.telDataGridViewTextBoxColumn.Name = "telDataGridViewTextBoxColumn";
            this.telDataGridViewTextBoxColumn.ReadOnly = true;
            this.telDataGridViewTextBoxColumn.Visible = false;
            // 
            // debitDataGridViewTextBoxColumn
            // 
            this.debitDataGridViewTextBoxColumn.DataPropertyName = "Debit";
            this.debitDataGridViewTextBoxColumn.HeaderText = "Debit";
            this.debitDataGridViewTextBoxColumn.Name = "debitDataGridViewTextBoxColumn";
            this.debitDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // creditDataGridViewTextBoxColumn
            // 
            this.creditDataGridViewTextBoxColumn.DataPropertyName = "Credit";
            this.creditDataGridViewTextBoxColumn.HeaderText = "Credit";
            this.creditDataGridViewTextBoxColumn.Name = "creditDataGridViewTextBoxColumn";
            this.creditDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // addDataGridViewTextBoxColumn
            // 
            this.addDataGridViewTextBoxColumn.DataPropertyName = "Add";
            this.addDataGridViewTextBoxColumn.HeaderText = "Address";
            this.addDataGridViewTextBoxColumn.Name = "addDataGridViewTextBoxColumn";
            this.addDataGridViewTextBoxColumn.ReadOnly = true;
            this.addDataGridViewTextBoxColumn.Visible = false;
            // 
            // landDataGridViewTextBoxColumn
            // 
            this.landDataGridViewTextBoxColumn.DataPropertyName = "Land";
            this.landDataGridViewTextBoxColumn.HeaderText = "Land";
            this.landDataGridViewTextBoxColumn.Name = "landDataGridViewTextBoxColumn";
            this.landDataGridViewTextBoxColumn.ReadOnly = true;
            this.landDataGridViewTextBoxColumn.Visible = false;
            // 
            // cityDataGridViewTextBoxColumn
            // 
            this.cityDataGridViewTextBoxColumn.DataPropertyName = "City";
            this.cityDataGridViewTextBoxColumn.HeaderText = "City";
            this.cityDataGridViewTextBoxColumn.Name = "cityDataGridViewTextBoxColumn";
            this.cityDataGridViewTextBoxColumn.ReadOnly = true;
            this.cityDataGridViewTextBoxColumn.Visible = false;
            // 
            // villageDataGridViewTextBoxColumn
            // 
            this.villageDataGridViewTextBoxColumn.DataPropertyName = "Village";
            this.villageDataGridViewTextBoxColumn.HeaderText = "Village";
            this.villageDataGridViewTextBoxColumn.Name = "villageDataGridViewTextBoxColumn";
            this.villageDataGridViewTextBoxColumn.ReadOnly = true;
            this.villageDataGridViewTextBoxColumn.Visible = false;
            // 
            // inActiveDataGridViewTextBoxColumn
            // 
            this.inActiveDataGridViewTextBoxColumn.DataPropertyName = "InActive";
            this.inActiveDataGridViewTextBoxColumn.HeaderText = "Active";
            this.inActiveDataGridViewTextBoxColumn.Name = "inActiveDataGridViewTextBoxColumn";
            this.inActiveDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ledger
            // 
            this.ledger.HeaderText = "ledger";
            this.ledger.Name = "ledger";
            this.ledger.ReadOnly = true;
            this.ledger.Text = "Detail";
            this.ledger.UseColumnTextForLinkValue = true;
            // 
            // VendBindingSource
            // 
            this.VendBindingSource.DataSource = typeof(Lib.Entity.Vendors);
            // 
            // VendBindingNavigator
            // 
            this.VendBindingNavigator.AddNewItem = this.bindingNavigatorPositionItem;
            this.VendBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.VendBindingNavigator.DeleteItem = null;
            this.VendBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoVendextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.lblAdd,
            this.toolStripSeparator1,
            this.lblEdit,
            this.toolStripSeparator2,
            this.toolStripTextBoxFind,
            this.toolStripButton1});
            this.VendBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.VendBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.VendBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.VendBindingNavigator.MoveNextItem = this.bindingNavigatorMoVendextItem;
            this.VendBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.VendBindingNavigator.Name = "VendBindingNavigator";
            this.VendBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.VendBindingNavigator.Size = new System.Drawing.Size(760, 25);
            this.VendBindingNavigator.TabIndex = 4;
            this.VendBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoVendextItem
            // 
            this.bindingNavigatorMoVendextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoVendextItem.Name = "bindingNavigatorMoVendextItem";
            this.bindingNavigatorMoVendextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoVendextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoVendextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // lblAdd
            // 
            this.lblAdd.Name = "lblAdd";
            this.lblAdd.Size = new System.Drawing.Size(29, 22);
            this.lblAdd.Text = "Add";
            this.lblAdd.Click += new System.EventHandler(this.lblAdd_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // lblEdit
            // 
            this.lblEdit.Name = "lblEdit";
            this.lblEdit.Size = new System.Drawing.Size(27, 22);
            this.lblEdit.Text = "Edit";
            this.lblEdit.Click += new System.EventHandler(this.lblEdit_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripTextBoxFind
            // 
            this.toolStripTextBoxFind.Name = "toolStripTextBoxFind";
            this.toolStripTextBoxFind.Size = new System.Drawing.Size(100, 25);
            this.toolStripTextBoxFind.Leave += new System.EventHandler(this.toolStripTextBoxFind_Leave);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlMain.Controls.Add(this.label9);
            this.pnlMain.Controls.Add(this.cmbxCity);
            this.pnlMain.Controls.Add(this.cmbxZone);
            this.pnlMain.Controls.Add(this.label4);
            this.pnlMain.Controls.Add(this.txtCredit);
            this.pnlMain.Controls.Add(this.label14);
            this.pnlMain.Controls.Add(this.txtDebit);
            this.pnlMain.Controls.Add(this.label12);
            this.pnlMain.Controls.Add(this.label13);
            this.pnlMain.Controls.Add(this.txtNTN);
            this.pnlMain.Controls.Add(this.label10);
            this.pnlMain.Controls.Add(this.txtCell);
            this.pnlMain.Controls.Add(this.label11);
            this.pnlMain.Controls.Add(this.txtContact);
            this.pnlMain.Controls.Add(this.label8);
            this.pnlMain.Controls.Add(this.cmbPType);
            this.pnlMain.Controls.Add(this.label7);
            this.pnlMain.Controls.Add(this.txtMail);
            this.pnlMain.Controls.Add(this.label5);
            this.pnlMain.Controls.Add(this.txtTel);
            this.pnlMain.Controls.Add(this.label6);
            this.pnlMain.Controls.Add(this.txtAddress);
            this.pnlMain.Controls.Add(this.chkIsActive);
            this.pnlMain.Controls.Add(this.label2);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Controls.Add(this.label1);
            this.pnlMain.Controls.Add(this.txtVndrName);
            this.pnlMain.Controls.Add(this.btnSave);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.HorizontalScrollbarBarColor = true;
            this.pnlMain.HorizontalScrollbarHighlightOnWheel = false;
            this.pnlMain.HorizontalScrollbarSize = 10;
            this.pnlMain.Location = new System.Drawing.Point(170, 9);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(472, 418);
            this.pnlMain.TabIndex = 5;
            this.pnlMain.UseCustomBackColor = true;
            this.pnlMain.VerticalScrollbarBarColor = true;
            this.pnlMain.VerticalScrollbarHighlightOnWheel = false;
            this.pnlMain.VerticalScrollbarSize = 10;
            // 
            // cmbxZone
            // 
            this.cmbxZone.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.VendBindingSource, "ZID", true));
            this.cmbxZone.FormattingEnabled = true;
            this.cmbxZone.ItemHeight = 23;
            this.cmbxZone.Location = new System.Drawing.Point(325, 264);
            this.cmbxZone.Name = "cmbxZone";
            this.cmbxZone.Size = new System.Drawing.Size(133, 29);
            this.cmbxZone.TabIndex = 63;
            this.cmbxZone.UseSelectable = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(273, 325);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 15);
            this.label4.TabIndex = 62;
            this.label4.Text = "Credit";
            // 
            // txtCredit
            // 
            // 
            // 
            // 
            this.txtCredit.CustomButton.Image = null;
            this.txtCredit.CustomButton.Location = new System.Drawing.Point(111, 1);
            this.txtCredit.CustomButton.Name = "";
            this.txtCredit.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtCredit.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCredit.CustomButton.TabIndex = 1;
            this.txtCredit.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCredit.CustomButton.UseSelectable = true;
            this.txtCredit.CustomButton.Visible = false;
            this.txtCredit.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.VendBindingSource, "Credit", true));
            this.txtCredit.Lines = new string[0];
            this.txtCredit.Location = new System.Drawing.Point(325, 321);
            this.txtCredit.MaxLength = 32767;
            this.txtCredit.Name = "txtCredit";
            this.txtCredit.PasswordChar = '\0';
            this.txtCredit.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCredit.SelectedText = "";
            this.txtCredit.SelectionLength = 0;
            this.txtCredit.SelectionStart = 0;
            this.txtCredit.ShortcutsEnabled = true;
            this.txtCredit.Size = new System.Drawing.Size(133, 23);
            this.txtCredit.TabIndex = 61;
            this.txtCredit.UseSelectable = true;
            this.txtCredit.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtCredit.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtCredit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCredit_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(21, 325);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 15);
            this.label14.TabIndex = 60;
            this.label14.Text = "Debit";
            // 
            // txtDebit
            // 
            // 
            // 
            // 
            this.txtDebit.CustomButton.Image = null;
            this.txtDebit.CustomButton.Location = new System.Drawing.Point(119, 1);
            this.txtDebit.CustomButton.Name = "";
            this.txtDebit.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtDebit.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtDebit.CustomButton.TabIndex = 1;
            this.txtDebit.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtDebit.CustomButton.UseSelectable = true;
            this.txtDebit.CustomButton.Visible = false;
            this.txtDebit.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.VendBindingSource, "Debit", true));
            this.txtDebit.Lines = new string[0];
            this.txtDebit.Location = new System.Drawing.Point(111, 321);
            this.txtDebit.MaxLength = 32767;
            this.txtDebit.Name = "txtDebit";
            this.txtDebit.PasswordChar = '\0';
            this.txtDebit.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDebit.SelectedText = "";
            this.txtDebit.SelectionLength = 0;
            this.txtDebit.SelectionStart = 0;
            this.txtDebit.ShortcutsEnabled = true;
            this.txtDebit.Size = new System.Drawing.Size(141, 23);
            this.txtDebit.TabIndex = 59;
            this.txtDebit.UseSelectable = true;
            this.txtDebit.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtDebit.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtDebit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDebit_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(273, 271);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(39, 15);
            this.label12.TabIndex = 58;
            this.label12.Text = "Zone";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(21, 271);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 15);
            this.label13.TabIndex = 57;
            this.label13.Text = "NTN";
            // 
            // txtNTN
            // 
            // 
            // 
            // 
            this.txtNTN.CustomButton.Image = null;
            this.txtNTN.CustomButton.Location = new System.Drawing.Point(119, 1);
            this.txtNTN.CustomButton.Name = "";
            this.txtNTN.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtNTN.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtNTN.CustomButton.TabIndex = 1;
            this.txtNTN.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtNTN.CustomButton.UseSelectable = true;
            this.txtNTN.CustomButton.Visible = false;
            this.txtNTN.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.VendBindingSource, "NTN_No", true));
            this.txtNTN.Lines = new string[0];
            this.txtNTN.Location = new System.Drawing.Point(111, 267);
            this.txtNTN.MaxLength = 32767;
            this.txtNTN.Name = "txtNTN";
            this.txtNTN.PasswordChar = '\0';
            this.txtNTN.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtNTN.SelectedText = "";
            this.txtNTN.SelectionLength = 0;
            this.txtNTN.SelectionStart = 0;
            this.txtNTN.ShortcutsEnabled = true;
            this.txtNTN.Size = new System.Drawing.Size(141, 23);
            this.txtNTN.TabIndex = 56;
            this.txtNTN.UseSelectable = true;
            this.txtNTN.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtNTN.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(273, 240);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 15);
            this.label10.TabIndex = 55;
            this.label10.Text = "Cell";
            // 
            // txtCell
            // 
            // 
            // 
            // 
            this.txtCell.CustomButton.Image = null;
            this.txtCell.CustomButton.Location = new System.Drawing.Point(111, 1);
            this.txtCell.CustomButton.Name = "";
            this.txtCell.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtCell.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtCell.CustomButton.TabIndex = 1;
            this.txtCell.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtCell.CustomButton.UseSelectable = true;
            this.txtCell.CustomButton.Visible = false;
            this.txtCell.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.VendBindingSource, "Cell", true));
            this.txtCell.Lines = new string[0];
            this.txtCell.Location = new System.Drawing.Point(325, 236);
            this.txtCell.MaxLength = 32767;
            this.txtCell.Name = "txtCell";
            this.txtCell.PasswordChar = '\0';
            this.txtCell.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtCell.SelectedText = "";
            this.txtCell.SelectionLength = 0;
            this.txtCell.SelectionStart = 0;
            this.txtCell.ShortcutsEnabled = true;
            this.txtCell.Size = new System.Drawing.Size(133, 23);
            this.txtCell.TabIndex = 54;
            this.txtCell.UseSelectable = true;
            this.txtCell.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtCell.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(4, 240);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(104, 15);
            this.label11.TabIndex = 53;
            this.label11.Text = "Contact Person";
            // 
            // txtContact
            // 
            // 
            // 
            // 
            this.txtContact.CustomButton.Image = null;
            this.txtContact.CustomButton.Location = new System.Drawing.Point(119, 1);
            this.txtContact.CustomButton.Name = "";
            this.txtContact.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtContact.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtContact.CustomButton.TabIndex = 1;
            this.txtContact.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtContact.CustomButton.UseSelectable = true;
            this.txtContact.CustomButton.Visible = false;
            this.txtContact.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.VendBindingSource, "ContactPerson", true));
            this.txtContact.Lines = new string[0];
            this.txtContact.Location = new System.Drawing.Point(111, 236);
            this.txtContact.MaxLength = 32767;
            this.txtContact.Name = "txtContact";
            this.txtContact.PasswordChar = '\0';
            this.txtContact.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtContact.SelectedText = "";
            this.txtContact.SelectionLength = 0;
            this.txtContact.SelectionStart = 0;
            this.txtContact.ShortcutsEnabled = true;
            this.txtContact.Size = new System.Drawing.Size(141, 23);
            this.txtContact.TabIndex = 52;
            this.txtContact.UseSelectable = true;
            this.txtContact.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtContact.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(21, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 15);
            this.label8.TabIndex = 49;
            this.label8.Text = "Party Type";
            // 
            // cmbPType
            // 
            this.cmbPType.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.VendBindingSource, "PType_ID", true));
            this.cmbPType.FormattingEnabled = true;
            this.cmbPType.ItemHeight = 23;
            this.cmbPType.Location = new System.Drawing.Point(111, 83);
            this.cmbPType.Name = "cmbPType";
            this.cmbPType.Size = new System.Drawing.Size(141, 29);
            this.cmbPType.TabIndex = 48;
            this.cmbPType.UseSelectable = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(273, 209);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 15);
            this.label7.TabIndex = 47;
            this.label7.Text = "Email";
            // 
            // txtMail
            // 
            // 
            // 
            // 
            this.txtMail.CustomButton.Image = null;
            this.txtMail.CustomButton.Location = new System.Drawing.Point(111, 1);
            this.txtMail.CustomButton.Name = "";
            this.txtMail.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtMail.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtMail.CustomButton.TabIndex = 1;
            this.txtMail.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtMail.CustomButton.UseSelectable = true;
            this.txtMail.CustomButton.Visible = false;
            this.txtMail.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.VendBindingSource, "Eml", true));
            this.txtMail.Lines = new string[0];
            this.txtMail.Location = new System.Drawing.Point(325, 205);
            this.txtMail.MaxLength = 32767;
            this.txtMail.Name = "txtMail";
            this.txtMail.PasswordChar = '\0';
            this.txtMail.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtMail.SelectedText = "";
            this.txtMail.SelectionLength = 0;
            this.txtMail.SelectionStart = 0;
            this.txtMail.ShortcutsEnabled = true;
            this.txtMail.Size = new System.Drawing.Size(133, 23);
            this.txtMail.TabIndex = 46;
            this.txtMail.UseSelectable = true;
            this.txtMail.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtMail.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(21, 209);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 15);
            this.label5.TabIndex = 45;
            this.label5.Text = "Telephone";
            // 
            // txtTel
            // 
            // 
            // 
            // 
            this.txtTel.CustomButton.Image = null;
            this.txtTel.CustomButton.Location = new System.Drawing.Point(119, 1);
            this.txtTel.CustomButton.Name = "";
            this.txtTel.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtTel.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtTel.CustomButton.TabIndex = 1;
            this.txtTel.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtTel.CustomButton.UseSelectable = true;
            this.txtTel.CustomButton.Visible = false;
            this.txtTel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.VendBindingSource, "Tel", true));
            this.txtTel.Lines = new string[0];
            this.txtTel.Location = new System.Drawing.Point(111, 205);
            this.txtTel.MaxLength = 32767;
            this.txtTel.Name = "txtTel";
            this.txtTel.PasswordChar = '\0';
            this.txtTel.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtTel.SelectedText = "";
            this.txtTel.SelectionLength = 0;
            this.txtTel.SelectionStart = 0;
            this.txtTel.ShortcutsEnabled = true;
            this.txtTel.Size = new System.Drawing.Size(141, 23);
            this.txtTel.TabIndex = 44;
            this.txtTel.UseSelectable = true;
            this.txtTel.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtTel.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(21, 154);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 15);
            this.label6.TabIndex = 43;
            this.label6.Text = "Address";
            // 
            // txtAddress
            // 
            // 
            // 
            // 
            this.txtAddress.CustomButton.Image = null;
            this.txtAddress.CustomButton.Location = new System.Drawing.Point(303, 2);
            this.txtAddress.CustomButton.Name = "";
            this.txtAddress.CustomButton.Size = new System.Drawing.Size(41, 41);
            this.txtAddress.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtAddress.CustomButton.TabIndex = 1;
            this.txtAddress.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtAddress.CustomButton.UseSelectable = true;
            this.txtAddress.CustomButton.Visible = false;
            this.txtAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.VendBindingSource, "Add", true));
            this.txtAddress.Lines = new string[0];
            this.txtAddress.Location = new System.Drawing.Point(111, 150);
            this.txtAddress.MaxLength = 32767;
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.PasswordChar = '\0';
            this.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtAddress.SelectedText = "";
            this.txtAddress.SelectionLength = 0;
            this.txtAddress.SelectionStart = 0;
            this.txtAddress.ShortcutsEnabled = true;
            this.txtAddress.Size = new System.Drawing.Size(347, 46);
            this.txtAddress.TabIndex = 42;
            this.txtAddress.UseSelectable = true;
            this.txtAddress.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtAddress.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.VendBindingSource, "InActive", true));
            this.chkIsActive.Location = new System.Drawing.Point(114, 301);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(15, 14);
            this.chkIsActive.TabIndex = 41;
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 299);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 15);
            this.label2.TabIndex = 40;
            this.label2.Text = "Is Active";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCancel.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(254, 367);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 32);
            this.btnCancel.TabIndex = 39;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btnCancel.UseCustomBackColor = true;
            this.btnCancel.UseCustomForeColor = true;
            this.btnCancel.UseSelectable = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 38;
            this.label1.Text = "Name";
            // 
            // txtVndrName
            // 
            // 
            // 
            // 
            this.txtVndrName.CustomButton.Image = null;
            this.txtVndrName.CustomButton.Location = new System.Drawing.Point(325, 1);
            this.txtVndrName.CustomButton.Name = "";
            this.txtVndrName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtVndrName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtVndrName.CustomButton.TabIndex = 1;
            this.txtVndrName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtVndrName.CustomButton.UseSelectable = true;
            this.txtVndrName.CustomButton.Visible = false;
            this.txtVndrName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.VendBindingSource, "VendName", true));
            this.txtVndrName.Lines = new string[0];
            this.txtVndrName.Location = new System.Drawing.Point(111, 119);
            this.txtVndrName.MaxLength = 32767;
            this.txtVndrName.Name = "txtVndrName";
            this.txtVndrName.PasswordChar = '\0';
            this.txtVndrName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtVndrName.SelectedText = "";
            this.txtVndrName.SelectionLength = 0;
            this.txtVndrName.SelectionStart = 0;
            this.txtVndrName.ShortcutsEnabled = true;
            this.txtVndrName.Size = new System.Drawing.Size(347, 23);
            this.txtVndrName.TabIndex = 37;
            this.txtVndrName.UseSelectable = true;
            this.txtVndrName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtVndrName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(154, 367);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 32);
            this.btnSave.TabIndex = 36;
            this.btnSave.Text = "Save";
            this.btnSave.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btnSave.UseCustomBackColor = true;
            this.btnSave.UseCustomForeColor = true;
            this.btnSave.UseSelectable = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(472, 62);
            this.panel1.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(184, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 33);
            this.label3.TabIndex = 0;
            this.label3.Text = "label3";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(258, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 15);
            this.label9.TabIndex = 65;
            this.label9.Text = "City";
            // 
            // cmbxCity
            // 
            this.cmbxCity.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.VendBindingSource, "City", true));
            this.cmbxCity.FormattingEnabled = true;
            this.cmbxCity.ItemHeight = 23;
            this.cmbxCity.Location = new System.Drawing.Point(294, 83);
            this.cmbxCity.Name = "cmbxCity";
            this.cmbxCity.Size = new System.Drawing.Size(164, 29);
            this.cmbxCity.TabIndex = 64;
            this.cmbxCity.UseSelectable = true;
            // 
            // FVendor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.metroPanel1);
            this.Name = "FVendor";
            this.Text = "Vendor";
            this.Load += new System.EventHandler(this.FVendor_Load);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VendDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VendBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VendBindingNavigator)).EndInit();
            this.VendBindingNavigator.ResumeLayout(false);
            this.VendBindingNavigator.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel metroPanel1;
        private System.Windows.Forms.DataGridView VendDataGridView;
        private System.Windows.Forms.BindingNavigator VendBindingNavigator;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoVendextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripLabel lblAdd;
        private System.Windows.Forms.ToolStripLabel lblEdit;
        private MetroFramework.Controls.MetroPanel pnlMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.BindingSource VendBindingSource;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxFind;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.DataGridViewTextBoxColumn compIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn compDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cShortDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn isDeleteDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn VenddNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cusNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ownerNameDataGridViewTextBoxColumn;
        private MetroFramework.Controls.MetroComboBox cmbxZone;
        private System.Windows.Forms.Label label4;
        private MetroFramework.Controls.MetroTextBox txtCredit;
        private System.Windows.Forms.Label label14;
        private MetroFramework.Controls.MetroTextBox txtDebit;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private MetroFramework.Controls.MetroTextBox txtNTN;
        private System.Windows.Forms.Label label10;
        private MetroFramework.Controls.MetroTextBox txtCell;
        private System.Windows.Forms.Label label11;
        private MetroFramework.Controls.MetroTextBox txtContact;
        private System.Windows.Forms.Label label8;
        private MetroFramework.Controls.MetroComboBox cmbPType;
        private System.Windows.Forms.Label label7;
        private MetroFramework.Controls.MetroTextBox txtMail;
        private System.Windows.Forms.Label label5;
        private MetroFramework.Controls.MetroTextBox txtTel;
        private System.Windows.Forms.Label label6;
        private MetroFramework.Controls.MetroTextBox txtAddress;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Label label2;
        private MetroFramework.Controls.MetroButton btnCancel;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroTextBox txtVndrName;
        private MetroFramework.Controls.MetroButton btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn aCCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vendNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nTNNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contactPersonDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cellDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn emlDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn telDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn debitDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn creditDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn addDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn landDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn villageDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn inActiveDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewLinkColumn ledger;
        private System.Windows.Forms.Label label9;
        private MetroFramework.Controls.MetroComboBox cmbxCity;
    }
}