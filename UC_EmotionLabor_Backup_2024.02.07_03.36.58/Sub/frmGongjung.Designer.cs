namespace NBOGUN.Sub
{
    partial class frmGongjung
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
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn1 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.grdGJ = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdGJ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdGJ.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // grdGJ
            // 
            this.grdGJ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdGJ.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdGJ.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            gridViewCheckBoxColumn1.HeaderText = "√";
            gridViewCheckBoxColumn1.Name = "clmIsSelect";
            gridViewCheckBoxColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewTextBoxColumn1.FieldName = "SiteName";
            gridViewTextBoxColumn1.HeaderText = "단위사업체명";
            gridViewTextBoxColumn1.Name = "clmSiteName";
            gridViewTextBoxColumn1.ReadOnly = true;
            gridViewTextBoxColumn1.Width = 150;
            gridViewTextBoxColumn2.FieldName = "GJName";
            gridViewTextBoxColumn2.HeaderText = "공정명";
            gridViewTextBoxColumn2.Name = "clmGJName";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn2.Width = 150;
            gridViewTextBoxColumn3.FieldName = "SiteNO";
            gridViewTextBoxColumn3.HeaderText = "column1";
            gridViewTextBoxColumn3.IsVisible = false;
            gridViewTextBoxColumn3.Name = "clmSiteNO";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn4.FieldName = "GJNO";
            gridViewTextBoxColumn4.HeaderText = "column1";
            gridViewTextBoxColumn4.IsVisible = false;
            gridViewTextBoxColumn4.Name = "clmGJNO";
            gridViewTextBoxColumn4.ReadOnly = true;
            this.grdGJ.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewCheckBoxColumn1,
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4});
            this.grdGJ.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.grdGJ.Name = "grdGJ";
            this.grdGJ.Size = new System.Drawing.Size(1108, 740);
            this.grdGJ.TabIndex = 0;
            this.grdGJ.ThemeName = "VisualStudio2012Light";
            this.grdGJ.CellEditorInitialized += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdGJ_CellEditorInitialized);
            this.grdGJ.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdGJ_CellDoubleClick);
            this.grdGJ.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.grdGJ_DataBindingComplete);
            // 
            // frmGongjung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 740);
            this.Controls.Add(this.grdGJ);
            this.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "frmGongjung";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "공정";
            this.ThemeName = "VisualStudio2012Light";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmGongjung_FormClosing);
            this.Load += new System.EventHandler(this.frmGongjung_Load);
            this.Shown += new System.EventHandler(this.frmGongjung_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.grdGJ.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdGJ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private Telerik.WinControls.UI.RadGridView grdGJ;
    }
}