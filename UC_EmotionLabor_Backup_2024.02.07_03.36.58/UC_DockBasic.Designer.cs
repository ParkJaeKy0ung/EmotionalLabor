namespace NBOGUN
{
    partial class UC_DockBasic
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_DockBasic));
            this.radDock1 = new Telerik.WinControls.UI.Docking.RadDock();
            this.toolWindow1 = new Telerik.WinControls.UI.Docking.ToolWindow();
            this.txtSaupjaName = new Telerik.WinControls.UI.RadTextBox();
            this.btnSaupjaSearch = new Telerik.WinControls.UI.RadButton();
            this.cboYear = new Telerik.WinControls.UI.RadDropDownList();
            this.toolTabStrip1 = new Telerik.WinControls.UI.Docking.ToolTabStrip();
            this.documentContainer2 = new Telerik.WinControls.UI.Docking.DocumentContainer();
            this.documentTabStrip1 = new Telerik.WinControls.UI.Docking.DocumentTabStrip();
            this.documentWindow4 = new Telerik.WinControls.UI.Docking.DocumentWindow();
            this.pvMain = new Telerik.WinControls.UI.RadPageView();
            this.pvpSaupja = new Telerik.WinControls.UI.RadPageViewPage();
            this.pvpPerson = new Telerik.WinControls.UI.RadPageViewPage();
            this.btnPersonDel = new Telerik.WinControls.UI.RadButton();
            this.pvpPrint = new Telerik.WinControls.UI.RadPageViewPage();
            this.radLabel6 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.btnExcel = new Telerik.WinControls.UI.RadButton();
            this.txtPrintJosaDate = new Telerik.WinControls.UI.RadTextBox();
            this.btnPrint = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.radDock1)).BeginInit();
            this.radDock1.SuspendLayout();
            this.toolWindow1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSaupjaName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaupjaSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolTabStrip1)).BeginInit();
            this.toolTabStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.documentContainer2)).BeginInit();
            this.documentContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.documentTabStrip1)).BeginInit();
            this.documentTabStrip1.SuspendLayout();
            this.documentWindow4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pvMain)).BeginInit();
            this.pvMain.SuspendLayout();
            this.pvpPerson.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPersonDel)).BeginInit();
            this.pvpPrint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrintJosaDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).BeginInit();
            this.SuspendLayout();
            // 
            // radDock1
            // 
            this.radDock1.ActiveWindow = this.documentWindow4;
            this.radDock1.Controls.Add(this.toolTabStrip1);
            this.radDock1.Controls.Add(this.documentContainer2);
            this.radDock1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radDock1.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.radDock1.IsCleanUpTarget = true;
            this.radDock1.Location = new System.Drawing.Point(0, 0);
            this.radDock1.MainDocumentContainer = this.documentContainer2;
            this.radDock1.Name = "radDock1";
            this.radDock1.Padding = new System.Windows.Forms.Padding(0);
            // 
            // 
            // 
            this.radDock1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.radDock1.Size = new System.Drawing.Size(1807, 820);
            this.radDock1.SplitterWidth = 8;
            this.radDock1.TabIndex = 2;
            this.radDock1.TabStop = false;
            this.radDock1.ThemeName = "VisualStudio2012Light";
            // 
            // toolWindow1
            // 
            this.toolWindow1.Caption = null;
            this.toolWindow1.Controls.Add(this.txtSaupjaName);
            this.toolWindow1.Controls.Add(this.btnSaupjaSearch);
            this.toolWindow1.Controls.Add(this.cboYear);
            this.toolWindow1.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.toolWindow1.Location = new System.Drawing.Point(4, 29);
            this.toolWindow1.Name = "toolWindow1";
            this.toolWindow1.PreviousDockState = Telerik.WinControls.UI.Docking.DockState.Docked;
            this.toolWindow1.Size = new System.Drawing.Size(541, 787);
            this.toolWindow1.Text = "사업장 검색";
            this.toolWindow1.ToolCaptionButtons = Telerik.WinControls.UI.Docking.ToolStripCaptionButtons.None;
            // 
            // txtSaupjaName
            // 
            this.txtSaupjaName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSaupjaName.AutoSize = false;
            this.txtSaupjaName.Font = new System.Drawing.Font("맑은 고딕", 11F);
            this.txtSaupjaName.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtSaupjaName.Location = new System.Drawing.Point(94, 3);
            this.txtSaupjaName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtSaupjaName.Name = "txtSaupjaName";
            this.txtSaupjaName.Size = new System.Drawing.Size(339, 26);
            this.txtSaupjaName.TabIndex = 53;
            this.txtSaupjaName.ThemeName = "Office2013Dark";
            // 
            // btnSaupjaSearch
            // 
            this.btnSaupjaSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaupjaSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(170)))), ((int)(((byte)(95)))));
            this.btnSaupjaSearch.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaupjaSearch.ForeColor = System.Drawing.Color.White;
            this.btnSaupjaSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSaupjaSearch.Image")));
            this.btnSaupjaSearch.Location = new System.Drawing.Point(441, 3);
            this.btnSaupjaSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSaupjaSearch.Name = "btnSaupjaSearch";
            this.btnSaupjaSearch.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnSaupjaSearch.Size = new System.Drawing.Size(97, 26);
            this.btnSaupjaSearch.TabIndex = 39;
            this.btnSaupjaSearch.Text = "검  색";
            this.btnSaupjaSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSaupjaSearch.ThemeName = "VisualStudio2012Light";
            // 
            // cboYear
            // 
            this.cboYear.AutoSize = false;
            this.cboYear.DropDownHeight = 114;
            this.cboYear.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.cboYear.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboYear.ItemHeight = 24;
            this.cboYear.Location = new System.Drawing.Point(4, 3);
            this.cboYear.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboYear.Name = "cboYear";
            this.cboYear.Size = new System.Drawing.Size(82, 26);
            this.cboYear.TabIndex = 38;
            this.cboYear.ThemeName = "VisualStudio2012Light";
            // 
            // toolTabStrip1
            // 
            this.toolTabStrip1.CanUpdateChildIndex = true;
            this.toolTabStrip1.Controls.Add(this.toolWindow1);
            this.toolTabStrip1.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.toolTabStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolTabStrip1.Name = "toolTabStrip1";
            // 
            // 
            // 
            this.toolTabStrip1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.toolTabStrip1.SelectedIndex = 0;
            this.toolTabStrip1.Size = new System.Drawing.Size(549, 820);
            this.toolTabStrip1.SizeInfo.AbsoluteSize = new System.Drawing.Size(549, 200);
            this.toolTabStrip1.TabIndex = 1;
            this.toolTabStrip1.TabStop = false;
            this.toolTabStrip1.TabStripVisible = false;
            this.toolTabStrip1.ThemeName = "VisualStudio2012Light";
            // 
            // documentContainer2
            // 
            this.documentContainer2.Controls.Add(this.documentTabStrip1);
            this.documentContainer2.Name = "documentContainer2";
            // 
            // 
            // 
            this.documentContainer2.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.documentContainer2.SizeInfo.SizeMode = Telerik.WinControls.UI.Docking.SplitPanelSizeMode.Fill;
            this.documentContainer2.SplitterWidth = 8;
            this.documentContainer2.TabIndex = 2;
            this.documentContainer2.ThemeName = "VisualStudio2012Light";
            // 
            // documentTabStrip1
            // 
            this.documentTabStrip1.CanUpdateChildIndex = true;
            this.documentTabStrip1.Controls.Add(this.documentWindow4);
            this.documentTabStrip1.Location = new System.Drawing.Point(0, 0);
            this.documentTabStrip1.Name = "documentTabStrip1";
            // 
            // 
            // 
            this.documentTabStrip1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.documentTabStrip1.SelectedIndex = 0;
            this.documentTabStrip1.Size = new System.Drawing.Size(1250, 820);
            this.documentTabStrip1.TabIndex = 0;
            this.documentTabStrip1.TabStop = false;
            this.documentTabStrip1.TabStripVisible = false;
            this.documentTabStrip1.ThemeName = "VisualStudio2012Light";
            // 
            // documentWindow4
            // 
            this.documentWindow4.Controls.Add(this.pvMain);
            this.documentWindow4.DocumentButtons = Telerik.WinControls.UI.Docking.DocumentStripButtons.None;
            this.documentWindow4.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.documentWindow4.Location = new System.Drawing.Point(4, 4);
            this.documentWindow4.Name = "documentWindow4";
            this.documentWindow4.PreviousDockState = Telerik.WinControls.UI.Docking.DockState.TabbedDocument;
            this.documentWindow4.Size = new System.Drawing.Size(1242, 812);
            this.documentWindow4.Text = "근로자 관리";
            this.documentWindow4.ToolCaptionButtons = Telerik.WinControls.UI.Docking.ToolStripCaptionButtons.None;
            // 
            // pvMain
            // 
            this.pvMain.Controls.Add(this.pvpSaupja);
            this.pvMain.Controls.Add(this.pvpPerson);
            this.pvMain.Controls.Add(this.pvpPrint);
            this.pvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pvMain.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pvMain.Location = new System.Drawing.Point(0, 0);
            this.pvMain.Name = "pvMain";
            this.pvMain.SelectedPage = this.pvpPerson;
            this.pvMain.Size = new System.Drawing.Size(1242, 812);
            this.pvMain.TabIndex = 68;
            this.pvMain.ThemeName = "VisualStudio2012Light";
            ((Telerik.WinControls.UI.RadPageViewStripElement)(this.pvMain.GetChildAt(0))).StripButtons = Telerik.WinControls.UI.StripViewButtons.None;
            // 
            // pvpSaupja
            // 
            this.pvpSaupja.ItemSize = new System.Drawing.SizeF(94F, 29F);
            this.pvpSaupja.Location = new System.Drawing.Point(5, 35);
            this.pvpSaupja.Name = "pvpSaupja";
            this.pvpSaupja.Size = new System.Drawing.Size(1232, 772);
            this.pvpSaupja.Text = "사업장 선택";
            // 
            // pvpPerson
            // 
            this.pvpPerson.Controls.Add(this.btnPersonDel);
            this.pvpPerson.ItemSize = new System.Drawing.SizeF(89F, 29F);
            this.pvpPerson.Location = new System.Drawing.Point(5, 35);
            this.pvpPerson.Name = "pvpPerson";
            this.pvpPerson.Size = new System.Drawing.Size(1232, 772);
            this.pvpPerson.Text = "근로자관리";
            // 
            // btnPersonDel
            // 
            this.btnPersonDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPersonDel.Font = new System.Drawing.Font("맑은 고딕", 11F);
            this.btnPersonDel.Image = ((System.Drawing.Image)(resources.GetObject("btnPersonDel.Image")));
            this.btnPersonDel.Location = new System.Drawing.Point(10552, 3);
            this.btnPersonDel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnPersonDel.Name = "btnPersonDel";
            this.btnPersonDel.Size = new System.Drawing.Size(84, 28);
            this.btnPersonDel.TabIndex = 57;
            this.btnPersonDel.Text = "삭  제";
            this.btnPersonDel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPersonDel.ThemeName = "Office2013Dark";
            // 
            // pvpPrint
            // 
            this.pvpPrint.Controls.Add(this.radLabel6);
            this.pvpPrint.Controls.Add(this.radLabel5);
            this.pvpPrint.Controls.Add(this.btnExcel);
            this.pvpPrint.Controls.Add(this.txtPrintJosaDate);
            this.pvpPrint.Controls.Add(this.btnPrint);
            this.pvpPrint.ItemSize = new System.Drawing.SizeF(89F, 29F);
            this.pvpPrint.Location = new System.Drawing.Point(5, 35);
            this.pvpPrint.Name = "pvpPrint";
            this.pvpPrint.Size = new System.Drawing.Size(1232, 772);
            this.pvpPrint.Text = "보고서출력";
            // 
            // radLabel6
            // 
            this.radLabel6.AutoSize = false;
            this.radLabel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(219)))));
            this.radLabel6.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.radLabel6.Image = ((System.Drawing.Image)(resources.GetObject("radLabel6.Image")));
            this.radLabel6.Location = new System.Drawing.Point(3, 35);
            this.radLabel6.Name = "radLabel6";
            this.radLabel6.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            // 
            // 
            // 
            this.radLabel6.RootElement.EnableElementShadow = false;
            this.radLabel6.Size = new System.Drawing.Size(118, 26);
            this.radLabel6.TabIndex = 174;
            this.radLabel6.Text = "엑셀 출력";
            this.radLabel6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radLabel6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.radLabel6.ThemeName = "VisualStudio2012Light";
            this.radLabel6.Visible = false;
            // 
            // radLabel5
            // 
            this.radLabel5.AutoSize = false;
            this.radLabel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(219)))));
            this.radLabel5.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.radLabel5.Image = ((System.Drawing.Image)(resources.GetObject("radLabel5.Image")));
            this.radLabel5.Location = new System.Drawing.Point(3, 3);
            this.radLabel5.Name = "radLabel5";
            this.radLabel5.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            // 
            // 
            // 
            this.radLabel5.RootElement.EnableElementShadow = false;
            this.radLabel5.Size = new System.Drawing.Size(118, 26);
            this.radLabel5.TabIndex = 173;
            this.radLabel5.Text = "조사일 표시";
            this.radLabel5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radLabel5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.radLabel5.ThemeName = "VisualStudio2012Light";
            // 
            // btnExcel
            // 
            this.btnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(170)))), ((int)(((byte)(95)))));
            this.btnExcel.Font = new System.Drawing.Font("맑은 고딕", 11F);
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Image = global::NBOGUN.Properties.Resources.excel;
            this.btnExcel.Location = new System.Drawing.Point(478, 3);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Padding = new System.Windows.Forms.Padding(7, 0, 5, 0);
            this.btnExcel.Size = new System.Drawing.Size(97, 26);
            this.btnExcel.TabIndex = 71;
            this.btnExcel.Text = "엑  셀";
            this.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExcel.ThemeName = "VisualStudio2012Light";
            // 
            // txtPrintJosaDate
            // 
            this.txtPrintJosaDate.AutoSize = false;
            this.txtPrintJosaDate.Font = new System.Drawing.Font("맑은 고딕", 11F);
            this.txtPrintJosaDate.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtPrintJosaDate.Location = new System.Drawing.Point(128, 3);
            this.txtPrintJosaDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtPrintJosaDate.Name = "txtPrintJosaDate";
            this.txtPrintJosaDate.Size = new System.Drawing.Size(237, 26);
            this.txtPrintJosaDate.TabIndex = 68;
            this.txtPrintJosaDate.ThemeName = "Office2013Dark";
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(170)))), ((int)(((byte)(95)))));
            this.btnPrint.Font = new System.Drawing.Font("맑은 고딕", 11F);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Image = global::NBOGUN.Properties.Resources.icon_print;
            this.btnPrint.Location = new System.Drawing.Point(373, 3);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnPrint.Size = new System.Drawing.Size(97, 26);
            this.btnPrint.TabIndex = 66;
            this.btnPrint.Text = "출  력";
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrint.ThemeName = "VisualStudio2012Light";
            // 
            // UC_DockBasic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radDock1);
            this.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "UC_DockBasic";
            this.Size = new System.Drawing.Size(1807, 820);
            ((System.ComponentModel.ISupportInitialize)(this.radDock1)).EndInit();
            this.radDock1.ResumeLayout(false);
            this.toolWindow1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSaupjaName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaupjaSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolTabStrip1)).EndInit();
            this.toolTabStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.documentContainer2)).EndInit();
            this.documentContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.documentTabStrip1)).EndInit();
            this.documentTabStrip1.ResumeLayout(false);
            this.documentWindow4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pvMain)).EndInit();
            this.pvMain.ResumeLayout(false);
            this.pvpPerson.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnPersonDel)).EndInit();
            this.pvpPrint.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPrintJosaDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.Docking.RadDock radDock1;
        private Telerik.WinControls.UI.Docking.DocumentWindow documentWindow4;
        private Telerik.WinControls.UI.RadPageView pvMain;
        private Telerik.WinControls.UI.RadPageViewPage pvpSaupja;
        private Telerik.WinControls.UI.RadPageViewPage pvpPerson;
        private Telerik.WinControls.UI.RadButton btnPersonDel;
        private Telerik.WinControls.UI.RadPageViewPage pvpPrint;
        private Telerik.WinControls.UI.RadLabel radLabel6;
        private Telerik.WinControls.UI.RadLabel radLabel5;
        private Telerik.WinControls.UI.RadButton btnExcel;
        private Telerik.WinControls.UI.RadTextBox txtPrintJosaDate;
        private Telerik.WinControls.UI.RadButton btnPrint;
        private Telerik.WinControls.UI.Docking.ToolTabStrip toolTabStrip1;
        private Telerik.WinControls.UI.Docking.ToolWindow toolWindow1;
        private Telerik.WinControls.UI.RadTextBox txtSaupjaName;
        private Telerik.WinControls.UI.RadButton btnSaupjaSearch;
        private Telerik.WinControls.UI.RadDropDownList cboYear;
        private Telerik.WinControls.UI.Docking.DocumentContainer documentContainer2;
        private Telerik.WinControls.UI.Docking.DocumentTabStrip documentTabStrip1;
    }
}
