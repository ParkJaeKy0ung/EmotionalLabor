namespace NBOGUN.Sub
{
    partial class frmDamdang
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
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition3 = new Telerik.WinControls.UI.TableViewDefinition();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grdDr = new Telerik.WinControls.UI.RadGridView();
            this.grdNr = new Telerik.WinControls.UI.RadGridView();
            this.grdHr = new Telerik.WinControls.UI.RadGridView();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDr.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNr.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdHr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdHr.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.4F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3F));
            this.tableLayoutPanel1.Controls.Add(this.grdDr, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.grdNr, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.grdHr, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1080, 357);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // grdDr
            // 
            this.grdDr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDr.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdDr.Location = new System.Drawing.Point(3, 3);
            // 
            // 
            // 
            this.grdDr.MasterTemplate.AutoGenerateColumns = false;
            this.grdDr.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewTextBoxColumn1.FieldName = "DamdangName";
            gridViewTextBoxColumn1.HeaderText = "이 름";
            gridViewTextBoxColumn1.Name = "clmName";
            gridViewTextBoxColumn1.ReadOnly = true;
            gridViewTextBoxColumn1.Width = 245;
            gridViewTextBoxColumn2.AllowResize = false;
            gridViewTextBoxColumn2.FieldName = "Sabun";
            gridViewTextBoxColumn2.HeaderText = "사 번";
            gridViewTextBoxColumn2.Name = "clmSabun";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewTextBoxColumn2.Width = 90;
            this.grdDr.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2});
            this.grdDr.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.grdDr.Name = "grdDr";
            this.grdDr.Size = new System.Drawing.Size(353, 351);
            this.grdDr.TabIndex = 0;
            this.grdDr.ThemeName = "VisualStudio2012Light";
            // 
            // grdNr
            // 
            this.grdNr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdNr.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNr.Location = new System.Drawing.Point(362, 3);
            // 
            // 
            // 
            this.grdNr.MasterTemplate.AutoGenerateColumns = false;
            this.grdNr.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewTextBoxColumn3.FieldName = "DamdangName";
            gridViewTextBoxColumn3.HeaderText = "이 름";
            gridViewTextBoxColumn3.Name = "clmName";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn3.Width = 246;
            gridViewTextBoxColumn4.AllowResize = false;
            gridViewTextBoxColumn4.FieldName = "Sabun";
            gridViewTextBoxColumn4.HeaderText = "사 번";
            gridViewTextBoxColumn4.Name = "clmSabun";
            gridViewTextBoxColumn4.ReadOnly = true;
            gridViewTextBoxColumn4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewTextBoxColumn4.Width = 90;
            this.grdNr.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4});
            this.grdNr.MasterTemplate.ViewDefinition = tableViewDefinition2;
            this.grdNr.Name = "grdNr";
            this.grdNr.Size = new System.Drawing.Size(354, 351);
            this.grdNr.TabIndex = 1;
            this.grdNr.ThemeName = "VisualStudio2012Light";
            // 
            // grdHr
            // 
            this.grdHr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdHr.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdHr.Location = new System.Drawing.Point(722, 3);
            // 
            // 
            // 
            this.grdHr.MasterTemplate.AutoGenerateColumns = false;
            this.grdHr.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewTextBoxColumn5.FieldName = "DamdangName";
            gridViewTextBoxColumn5.HeaderText = "이 름";
            gridViewTextBoxColumn5.Name = "clmName";
            gridViewTextBoxColumn5.ReadOnly = true;
            gridViewTextBoxColumn5.Width = 247;
            gridViewTextBoxColumn6.AllowResize = false;
            gridViewTextBoxColumn6.FieldName = "Sabun";
            gridViewTextBoxColumn6.HeaderText = "사 번";
            gridViewTextBoxColumn6.Name = "clmSabun";
            gridViewTextBoxColumn6.ReadOnly = true;
            gridViewTextBoxColumn6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewTextBoxColumn6.Width = 90;
            this.grdHr.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6});
            this.grdHr.MasterTemplate.ViewDefinition = tableViewDefinition3;
            this.grdHr.Name = "grdHr";
            this.grdHr.Size = new System.Drawing.Size(355, 351);
            this.grdHr.TabIndex = 2;
            this.grdHr.ThemeName = "VisualStudio2012Light";
            // 
            // frmDamdang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 357);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmDamdang";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "담당자";
            this.ThemeName = "VisualStudio2012Light";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDamdang_FormClosed);
            this.Load += new System.EventHandler(this.frmDamdang_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDr.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNr.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdHr.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdHr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Telerik.WinControls.UI.RadGridView grdDr;
        private Telerik.WinControls.UI.RadGridView grdNr;
        private Telerik.WinControls.UI.RadGridView grdHr;
    }
}