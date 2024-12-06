namespace NBOGUN
{
    partial class UC_BONBU_OverVisit
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
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn10 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn11 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn12 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_BONBU_OverVisit));
            this.btnRefresh = new Telerik.WinControls.UI.RadButton();
            this.grdVisit = new Telerik.WinControls.UI.RadGridView();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.btnExcel = new Telerik.WinControls.UI.RadButton();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.cboYear = new Telerik.WinControls.UI.RadDropDownList();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.btnRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdVisit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdVisit.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(170)))), ((int)(((byte)(95)))));
            this.btnRefresh.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(160, 3);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 26);
            this.btnRefresh.TabIndex = 429;
            this.btnRefresh.Text = "새로고침";
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefresh.ThemeName = "VisualStudio2012Light";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // grdVisit
            // 
            this.grdVisit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdVisit.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdVisit.Location = new System.Drawing.Point(3, 35);
            // 
            // 
            // 
            gridViewTextBoxColumn1.FieldName = "Center";
            gridViewTextBoxColumn1.HeaderText = "clmCenter";
            gridViewTextBoxColumn1.IsVisible = false;
            gridViewTextBoxColumn1.Name = "clmCenter";
            gridViewTextBoxColumn1.ReadOnly = true;
            gridViewTextBoxColumn2.FieldName = "CenterName";
            gridViewTextBoxColumn2.HeaderText = "센터";
            gridViewTextBoxColumn2.Name = "clmCenterName";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn2.Width = 60;
            gridViewTextBoxColumn3.FieldName = "SaupjaNum";
            gridViewTextBoxColumn3.HeaderText = "관리번호";
            gridViewTextBoxColumn3.Name = "clmSaupjaNum";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn3.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewTextBoxColumn3.Width = 100;
            gridViewTextBoxColumn4.FieldName = "SaupjaName";
            gridViewTextBoxColumn4.HeaderText = "사업장명";
            gridViewTextBoxColumn4.Name = "clmSaupjaName";
            gridViewTextBoxColumn4.ReadOnly = true;
            gridViewTextBoxColumn4.Width = 200;
            gridViewTextBoxColumn5.FieldName = "VisitDate";
            gridViewTextBoxColumn5.HeaderText = "방문일";
            gridViewTextBoxColumn5.Name = "clmVisitDate";
            gridViewTextBoxColumn5.ReadOnly = true;
            gridViewTextBoxColumn5.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewTextBoxColumn5.Width = 100;
            gridViewTextBoxColumn6.FieldName = "VisitorName";
            gridViewTextBoxColumn6.HeaderText = "방문자";
            gridViewTextBoxColumn6.Name = "clmVisitorName";
            gridViewTextBoxColumn6.ReadOnly = true;
            gridViewTextBoxColumn6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewTextBoxColumn6.Width = 100;
            gridViewTextBoxColumn7.FieldName = "DamdangSignDate";
            gridViewTextBoxColumn7.HeaderText = "담당자사인일";
            gridViewTextBoxColumn7.Name = "clmDamdangSignDate";
            gridViewTextBoxColumn7.ReadOnly = true;
            gridViewTextBoxColumn7.Width = 120;
            gridViewTextBoxColumn8.FieldName = "BuseojangSignDate";
            gridViewTextBoxColumn8.HeaderText = "부서장사인일";
            gridViewTextBoxColumn8.Name = "clmBuseojangSignDate";
            gridViewTextBoxColumn8.ReadOnly = true;
            gridViewTextBoxColumn8.Width = 120;
            gridViewTextBoxColumn9.FieldName = "BuseojangName";
            gridViewTextBoxColumn9.HeaderText = "부서장명";
            gridViewTextBoxColumn9.Name = "clmBuseojangName";
            gridViewTextBoxColumn9.ReadOnly = true;
            gridViewTextBoxColumn9.Width = 100;
            gridViewTextBoxColumn10.FieldName = "Diff";
            gridViewTextBoxColumn10.HeaderText = "일수";
            gridViewTextBoxColumn10.Name = "clmDiff";
            gridViewTextBoxColumn10.ReadOnly = true;
            gridViewTextBoxColumn10.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewTextBoxColumn11.FieldName = "Visitor";
            gridViewTextBoxColumn11.HeaderText = "clmVisitor";
            gridViewTextBoxColumn11.IsVisible = false;
            gridViewTextBoxColumn11.Name = "clmVisitor";
            gridViewTextBoxColumn11.ReadOnly = true;
            gridViewTextBoxColumn12.FieldName = "Part2Check";
            gridViewTextBoxColumn12.HeaderText = "전결";
            gridViewTextBoxColumn12.Name = "clmPart2Check";
            gridViewTextBoxColumn12.ReadOnly = true;
            gridViewTextBoxColumn12.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.grdVisit.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6,
            gridViewTextBoxColumn7,
            gridViewTextBoxColumn8,
            gridViewTextBoxColumn9,
            gridViewTextBoxColumn10,
            gridViewTextBoxColumn11,
            gridViewTextBoxColumn12});
            this.grdVisit.MasterTemplate.EnableFiltering = true;
            this.grdVisit.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.grdVisit.Name = "grdVisit";
            this.grdVisit.Size = new System.Drawing.Size(1361, 665);
            this.grdVisit.TabIndex = 430;
            this.grdVisit.ThemeName = "VisualStudio2012Light";
            this.grdVisit.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.grdVisit_DataBindingComplete);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(170)))), ((int)(((byte)(95)))));
            this.btnExcel.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Location = new System.Drawing.Point(1274, 3);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(90, 26);
            this.btnExcel.TabIndex = 434;
            this.btnExcel.Text = "엑  셀";
            this.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExcel.ThemeName = "VisualStudio2012Light";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // radLabel1
            // 
            this.radLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel1.AutoSize = false;
            this.radLabel1.BackColor = System.Drawing.Color.SteelBlue;
            this.radLabel1.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.radLabel1.ForeColor = System.Drawing.Color.White;
            this.radLabel1.Image = ((System.Drawing.Image)(resources.GetObject("radLabel1.Image")));
            this.radLabel1.Location = new System.Drawing.Point(257, 3);
            this.radLabel1.Name = "radLabel1";
            // 
            // 
            // 
            this.radLabel1.RootElement.EnableElementShadow = false;
            this.radLabel1.Size = new System.Drawing.Size(1010, 26);
            this.radLabel1.TabIndex = 433;
            this.radLabel1.Text = "7일이상 지난 방문일";
            this.radLabel1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.radLabel1.ThemeName = "VisualStudio2012Light";
            // 
            // cboYear
            // 
            this.cboYear.AutoSize = false;
            this.cboYear.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.cboYear.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboYear.Location = new System.Drawing.Point(83, 3);
            this.cboYear.Name = "cboYear";
            this.cboYear.Size = new System.Drawing.Size(70, 26);
            this.cboYear.TabIndex = 436;
            this.cboYear.ThemeName = "VisualStudio2012Light";
            // 
            // radLabel3
            // 
            this.radLabel3.AutoSize = false;
            this.radLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(219)))));
            this.radLabel3.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.radLabel3.Image = ((System.Drawing.Image)(resources.GetObject("radLabel3.Image")));
            this.radLabel3.Location = new System.Drawing.Point(3, 3);
            this.radLabel3.Name = "radLabel3";
            // 
            // 
            // 
            this.radLabel3.RootElement.EnableElementShadow = false;
            this.radLabel3.Size = new System.Drawing.Size(74, 26);
            this.radLabel3.TabIndex = 435;
            this.radLabel3.Text = "연  도";
            this.radLabel3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.radLabel3.ThemeName = "Office2013Dark";
            // 
            // UC_BONBU_OverVisit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cboYear);
            this.Controls.Add(this.radLabel3);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.grdVisit);
            this.Controls.Add(this.btnRefresh);
            this.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "UC_BONBU_OverVisit";
            this.Size = new System.Drawing.Size(1367, 703);
            this.Load += new System.EventHandler(this.UC_BONBU_OverVisit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdVisit.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdVisit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadButton btnRefresh;
        private Telerik.WinControls.UI.RadGridView grdVisit;
        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private Telerik.WinControls.UI.RadButton btnExcel;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadDropDownList cboYear;
        private Telerik.WinControls.UI.RadLabel radLabel3;
    }
}
