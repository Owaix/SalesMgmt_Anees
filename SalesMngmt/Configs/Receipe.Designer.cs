namespace SalesMngmt.Configs
{
    partial class Receipe
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
            this.cmbxReceipe = new MetroFramework.Controls.MetroComboBox();
            this.cmbxUnit = new System.Windows.Forms.Label();
            this.combxIngredient = new MetroFramework.Controls.MetroComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDescribtion = new MetroFramework.Controls.MetroTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtQty = new MetroFramework.Controls.MetroTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStep = new MetroFramework.Controls.MetroTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSave = new MetroFramework.Controls.MetroButton();
            this.btnUpdate = new MetroFramework.Controls.MetroButton();
            this.btnClear = new MetroFramework.Controls.MetroButton();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.ReceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ingredient = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Step = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isActive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceipeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IngredientID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbxReceipe
            // 
            this.cmbxReceipe.FormattingEnabled = true;
            this.cmbxReceipe.ItemHeight = 23;
            this.cmbxReceipe.Location = new System.Drawing.Point(125, 76);
            this.cmbxReceipe.Name = "cmbxReceipe";
            this.cmbxReceipe.Size = new System.Drawing.Size(648, 29);
            this.cmbxReceipe.TabIndex = 59;
            this.cmbxReceipe.UseSelectable = true;
            this.cmbxReceipe.SelectedIndexChanged += new System.EventHandler(this.cmbxReceipe_SelectedIndexChanged);
            this.cmbxReceipe.SelectedValueChanged += new System.EventHandler(this.cmbxReceipe_SelectedValueChanged);
            // 
            // cmbxUnit
            // 
            this.cmbxUnit.AutoSize = true;
            this.cmbxUnit.BackColor = System.Drawing.Color.White;
            this.cmbxUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbxUnit.Location = new System.Drawing.Point(17, 77);
            this.cmbxUnit.Name = "cmbxUnit";
            this.cmbxUnit.Size = new System.Drawing.Size(102, 15);
            this.cmbxUnit.TabIndex = 58;
            this.cmbxUnit.Text = "Receipe Name";
            // 
            // combxIngredient
            // 
            this.combxIngredient.FormattingEnabled = true;
            this.combxIngredient.ItemHeight = 23;
            this.combxIngredient.Location = new System.Drawing.Point(125, 148);
            this.combxIngredient.Name = "combxIngredient";
            this.combxIngredient.Size = new System.Drawing.Size(261, 29);
            this.combxIngredient.TabIndex = 61;
            this.combxIngredient.UseSelectable = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 60;
            this.label1.Text = "Ingredient";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 221);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 15);
            this.label4.TabIndex = 67;
            this.label4.Text = "Description";
            // 
            // txtDescribtion
            // 
            // 
            // 
            // 
            this.txtDescribtion.CustomButton.Image = null;
            this.txtDescribtion.CustomButton.Location = new System.Drawing.Point(398, 2);
            this.txtDescribtion.CustomButton.Name = "";
            this.txtDescribtion.CustomButton.Size = new System.Drawing.Size(47, 47);
            this.txtDescribtion.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtDescribtion.CustomButton.TabIndex = 1;
            this.txtDescribtion.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtDescribtion.CustomButton.UseSelectable = true;
            this.txtDescribtion.CustomButton.Visible = false;
            this.txtDescribtion.Lines = new string[0];
            this.txtDescribtion.Location = new System.Drawing.Point(125, 207);
            this.txtDescribtion.MaxLength = 32767;
            this.txtDescribtion.Multiline = true;
            this.txtDescribtion.Name = "txtDescribtion";
            this.txtDescribtion.PasswordChar = '\0';
            this.txtDescribtion.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtDescribtion.SelectedText = "";
            this.txtDescribtion.SelectionLength = 0;
            this.txtDescribtion.SelectionStart = 0;
            this.txtDescribtion.ShortcutsEnabled = true;
            this.txtDescribtion.Size = new System.Drawing.Size(448, 52);
            this.txtDescribtion.TabIndex = 66;
            this.txtDescribtion.UseSelectable = true;
            this.txtDescribtion.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtDescribtion.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(392, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 15);
            this.label2.TabIndex = 69;
            this.label2.Text = "Qty";
            // 
            // txtQty
            // 
            // 
            // 
            // 
            this.txtQty.CustomButton.Image = null;
            this.txtQty.CustomButton.Location = new System.Drawing.Point(113, 2);
            this.txtQty.CustomButton.Name = "";
            this.txtQty.CustomButton.Size = new System.Drawing.Size(23, 23);
            this.txtQty.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtQty.CustomButton.TabIndex = 1;
            this.txtQty.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtQty.CustomButton.UseSelectable = true;
            this.txtQty.CustomButton.Visible = false;
            this.txtQty.Lines = new string[0];
            this.txtQty.Location = new System.Drawing.Point(434, 148);
            this.txtQty.MaxLength = 32767;
            this.txtQty.Multiline = true;
            this.txtQty.Name = "txtQty";
            this.txtQty.PasswordChar = '\0';
            this.txtQty.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtQty.SelectedText = "";
            this.txtQty.SelectionLength = 0;
            this.txtQty.SelectionStart = 0;
            this.txtQty.ShortcutsEnabled = true;
            this.txtQty.Size = new System.Drawing.Size(139, 28);
            this.txtQty.TabIndex = 68;
            this.txtQty.UseSelectable = true;
            this.txtQty.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtQty.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQty_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(580, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 15);
            this.label3.TabIndex = 71;
            this.label3.Text = "Step";
            // 
            // txtStep
            // 
            // 
            // 
            // 
            this.txtStep.CustomButton.Image = null;
            this.txtStep.CustomButton.Location = new System.Drawing.Point(113, 2);
            this.txtStep.CustomButton.Name = "";
            this.txtStep.CustomButton.Size = new System.Drawing.Size(23, 23);
            this.txtStep.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtStep.CustomButton.TabIndex = 1;
            this.txtStep.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtStep.CustomButton.UseSelectable = true;
            this.txtStep.CustomButton.Visible = false;
            this.txtStep.Lines = new string[0];
            this.txtStep.Location = new System.Drawing.Point(634, 148);
            this.txtStep.MaxLength = 32767;
            this.txtStep.Multiline = true;
            this.txtStep.Name = "txtStep";
            this.txtStep.PasswordChar = '\0';
            this.txtStep.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtStep.SelectedText = "";
            this.txtStep.SelectionLength = 0;
            this.txtStep.SelectionStart = 0;
            this.txtStep.ShortcutsEnabled = true;
            this.txtStep.Size = new System.Drawing.Size(139, 28);
            this.txtStep.TabIndex = 70;
            this.txtStep.UseSelectable = true;
            this.txtStep.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtStep.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ReceName,
            this.Ingredient,
            this.Qty,
            this.Step,
            this.Description,
            this.isActive,
            this.ReceipeID,
            this.IngredientID,
            this.RID});
            this.dataGridView1.Location = new System.Drawing.Point(20, 279);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(797, 231);
            this.dataGridView1.TabIndex = 72;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(186, 516);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 32);
            this.btnSave.TabIndex = 73;
            this.btnSave.Text = "Save";
            this.btnSave.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btnSave.UseCustomBackColor = true;
            this.btnSave.UseCustomForeColor = true;
            this.btnSave.UseSelectable = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnUpdate.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(376, 516);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(94, 32);
            this.btnUpdate.TabIndex = 74;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btnUpdate.UseCustomBackColor = true;
            this.btnUpdate.UseCustomForeColor = true;
            this.btnUpdate.UseSelectable = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnClear.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(566, 516);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(94, 32);
            this.btnClear.TabIndex = 76;
            this.btnClear.Text = "Clear";
            this.btnClear.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.btnClear.UseCustomBackColor = true;
            this.btnClear.UseCustomForeColor = true;
            this.btnClear.UseSelectable = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Location = new System.Drawing.Point(634, 221);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(76, 17);
            this.chkIsActive.TabIndex = 77;
            this.chkIsActive.Text = "NonActive";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // ReceName
            // 
            this.ReceName.HeaderText = "ReceName";
            this.ReceName.Name = "ReceName";
            this.ReceName.ReadOnly = true;
            // 
            // Ingredient
            // 
            this.Ingredient.HeaderText = "Ingredient";
            this.Ingredient.Name = "Ingredient";
            this.Ingredient.ReadOnly = true;
            // 
            // Qty
            // 
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            // 
            // Step
            // 
            this.Step.HeaderText = "Step";
            this.Step.Name = "Step";
            this.Step.ReadOnly = true;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // isActive
            // 
            this.isActive.HeaderText = "isActive";
            this.isActive.Name = "isActive";
            this.isActive.ReadOnly = true;
            // 
            // ReceipeID
            // 
            this.ReceipeID.HeaderText = "ReceipeID";
            this.ReceipeID.Name = "ReceipeID";
            this.ReceipeID.ReadOnly = true;
            this.ReceipeID.Visible = false;
            // 
            // IngredientID
            // 
            this.IngredientID.HeaderText = "IngredientID";
            this.IngredientID.Name = "IngredientID";
            this.IngredientID.ReadOnly = true;
            this.IngredientID.Visible = false;
            // 
            // RID
            // 
            this.RID.HeaderText = "RID";
            this.RID.Name = "RID";
            this.RID.ReadOnly = true;
            this.RID.Visible = false;
            // 
            // Receipe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 552);
            this.Controls.Add(this.chkIsActive);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtStep);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDescribtion);
            this.Controls.Add(this.combxIngredient);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbxReceipe);
            this.Controls.Add(this.cmbxUnit);
            this.Name = "Receipe";
            this.Text = "Receipe";
            this.Load += new System.EventHandler(this.Receipe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroComboBox cmbxReceipe;
        private System.Windows.Forms.Label cmbxUnit;
        private MetroFramework.Controls.MetroComboBox combxIngredient;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private MetroFramework.Controls.MetroTextBox txtDescribtion;
        private System.Windows.Forms.Label label2;
        private MetroFramework.Controls.MetroTextBox txtQty;
        private System.Windows.Forms.Label label3;
        private MetroFramework.Controls.MetroTextBox txtStep;
        private System.Windows.Forms.DataGridView dataGridView1;
        private MetroFramework.Controls.MetroButton btnSave;
        private MetroFramework.Controls.MetroButton btnUpdate;
        private MetroFramework.Controls.MetroButton btnClear;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ingredient;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Step;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn isActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceipeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn IngredientID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RID;
    }
}