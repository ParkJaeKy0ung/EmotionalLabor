namespace NBOGUN.Sub
{
    partial class frmCodeEduItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCodeEduItem));
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewCommandColumn gridViewCommandColumn1 = new Telerik.WinControls.UI.GridViewCommandColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.radLabel45 = new Telerik.WinControls.UI.RadLabel();
            this.txtEduItemYear = new Telerik.WinControls.UI.RadTextBox();
            this.btnEduItemList = new Telerik.WinControls.UI.RadButton();
            this.grdEduItem = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel45)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEduItemYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEduItemList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdEduItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdEduItem.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radLabel45
            // 
            this.radLabel45.AutoSize = false;
            this.radLabel45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(219)))));
            this.radLabel45.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.radLabel45.Image = ((System.Drawing.Image)(resources.GetObject("radLabel45.Image")));
            this.radLabel45.Location = new System.Drawing.Point(12, 12);
            this.radLabel45.Name = "radLabel45";
            // 
            // 
            // 
            this.radLabel45.RootElement.EnableElementShadow = false;
            this.radLabel45.Size = new System.Drawing.Size(74, 26);
            this.radLabel45.TabIndex = 124;
            this.radLabel45.Text = "연  도";
            this.radLabel45.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.radLabel45.ThemeName = "Office2013Dark";
            // 
            // txtEduItemYear
            // 
            this.txtEduItemYear.AutoSize = false;
            this.txtEduItemYear.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEduItemYear.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtEduItemYear.Location = new System.Drawing.Point(92, 12);
            this.txtEduItemYear.Name = "txtEduItemYear";
            this.txtEduItemYear.Size = new System.Drawing.Size(70, 26);
            this.txtEduItemYear.TabIndex = 123;
            this.txtEduItemYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEduItemYear.ThemeName = "VisualStudio2012Light";
            // 
            // btnEduItemList
            // 
            this.btnEduItemList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(170)))), ((int)(((byte)(95)))));
            this.btnEduItemList.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnEduItemList.ForeColor = System.Drawing.Color.White;
            this.btnEduItemList.Location = new System.Drawing.Point(169, 12);
            this.btnEduItemList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnEduItemList.Name = "btnEduItemList";
            this.btnEduItemList.Size = new System.Drawing.Size(90, 26);
            this.btnEduItemList.TabIndex = 122;
            this.btnEduItemList.Text = "불러오기";
            this.btnEduItemList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEduItemList.ThemeName = "VisualStudio2012Light";
            this.btnEduItemList.Click += new System.EventHandler(this.btnEduItemList_Click);
            // 
            // grdEduItem
            // 
            this.grdEduItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdEduItem.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdEduItem.Location = new System.Drawing.Point(12, 44);
            // 
            // 
            // 
            gridViewTextBoxColumn1.FieldName = "Code";
            gridViewTextBoxColumn1.HeaderText = "코드";
            gridViewTextBoxColumn1.Name = "clmCode";
            gridViewTextBoxColumn1.ReadOnly = true;
            gridViewTextBoxColumn1.Width = 100;
            gridViewTextBoxColumn2.FieldName = "Name";
            gridViewTextBoxColumn2.HeaderText = "자료명";
            gridViewTextBoxColumn2.Name = "clmName";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn2.Width = 200;
            gridViewTextBoxColumn3.FieldName = "ItemTypeName";
            gridViewTextBoxColumn3.HeaderText = "자료구분";
            gridViewTextBoxColumn3.Name = "clmItemTypeName";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn3.Width = 80;
            gridViewCommandColumn1.DefaultText = "선 택";
            gridViewCommandColumn1.HeaderText = "";
            gridViewCommandColumn1.Name = "clmOK";
            gridViewCommandColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewCommandColumn1.UseDefaultText = true;
            gridViewCommandColumn1.Width = 60;
            gridViewTextBoxColumn4.FieldName = "ItemType";
            gridViewTextBoxColumn4.HeaderText = "clmItemType";
            gridViewTextBoxColumn4.IsVisible = false;
            gridViewTextBoxColumn4.Name = "clmItemType";
            gridViewTextBoxColumn4.ReadOnly = true;
            this.grdEduItem.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewCommandColumn1,
            gridViewTextBoxColumn4});
            this.grdEduItem.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.grdEduItem.Name = "grdEduItem";
            this.grdEduItem.Size = new System.Drawing.Size(527, 397);
            this.grdEduItem.TabIndex = 125;
            this.grdEduItem.ThemeName = "VisualStudio2012Light";
            this.grdEduItem.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdEduItem_CellClick);
            this.grdEduItem.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdEduItem_CellDoubleClick);
            // 
            // frmCodeEduItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 453);
            this.Controls.Add(this.grdEduItem);
            this.Controls.Add(this.radLabel45);
            this.Controls.Add(this.txtEduItemYear);
            this.Controls.Add(this.btnEduItemList);
            this.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmCodeEduItem";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "교육 자료";
            this.ThemeName = "VisualStudio2012Light";
            this.Load += new System.EventHandler(this.frmCodeEduItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radLabel45)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEduItemYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEduItemList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdEduItem.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdEduItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private Telerik.WinControls.UI.RadLabel radLabel45;
        private Telerik.WinControls.UI.RadTextBox txtEduItemYear;
        private Telerik.WinControls.UI.RadButton btnEduItemList;
        private Telerik.WinControls.UI.RadGridView grdEduItem;
    }
}