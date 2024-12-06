namespace NBOGUN
{
    partial class UC_ChkjnDate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_ChkjnDate));
            this.txtChkjnSaupjaName = new Telerik.WinControls.UI.RadTextBox();
            this.dtpChkjnDate = new Telerik.WinControls.UI.RadDateTimePicker();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.btnRefresh = new Telerik.WinControls.UI.RadButton();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.txtChkjnDate = new Telerik.WinControls.UI.RadMaskedEditBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtChkjnSaupjaName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpChkjnDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChkjnDate)).BeginInit();
            this.SuspendLayout();
            // 
            // txtChkjnSaupjaName
            // 
            this.txtChkjnSaupjaName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtChkjnSaupjaName.AutoSize = false;
            this.txtChkjnSaupjaName.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChkjnSaupjaName.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.txtChkjnSaupjaName.Location = new System.Drawing.Point(218, 3);
            this.txtChkjnSaupjaName.Name = "txtChkjnSaupjaName";
            this.txtChkjnSaupjaName.Size = new System.Drawing.Size(305, 26);
            this.txtChkjnSaupjaName.TabIndex = 175;
            this.txtChkjnSaupjaName.ThemeName = "VisualStudio2012Light";
            // 
            // dtpChkjnDate
            // 
            this.dtpChkjnDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpChkjnDate.AutoSize = false;
            this.dtpChkjnDate.CalendarSize = new System.Drawing.Size(290, 320);
            this.dtpChkjnDate.CustomFormat = "yyyy-MM-dd";
            this.dtpChkjnDate.Font = new System.Drawing.Font("맑은 고딕", 11F);
            this.dtpChkjnDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpChkjnDate.Location = new System.Drawing.Point(653, 3);
            this.dtpChkjnDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dtpChkjnDate.Name = "dtpChkjnDate";
            this.dtpChkjnDate.Size = new System.Drawing.Size(93, 26);
            this.dtpChkjnDate.TabIndex = 173;
            this.dtpChkjnDate.TabStop = false;
            this.dtpChkjnDate.Text = "2022-01-21";
            this.dtpChkjnDate.ThemeName = "VisualStudio2012Light";
            this.dtpChkjnDate.Value = new System.DateTime(2022, 1, 21, 9, 22, 59, 696);
            this.dtpChkjnDate.Closing += new Telerik.WinControls.UI.RadPopupClosingEventHandler(this.dtpChkjnDate_Closing);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(170)))), ((int)(((byte)(95)))));
            this.btnSave.Font = new System.Drawing.Font("맑은 고딕", 11F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(754, 3);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnSave.Size = new System.Drawing.Size(111, 26);
            this.btnSave.TabIndex = 171;
            this.btnSave.Text = "저  장";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.ThemeName = "VisualStudio2012Light";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(170)))), ((int)(((byte)(95)))));
            this.btnRefresh.Font = new System.Drawing.Font("맑은 고딕", 11F);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(4, 3);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnRefresh.Size = new System.Drawing.Size(111, 26);
            this.btnRefresh.TabIndex = 170;
            this.btnRefresh.Text = "새로고침";
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefresh.ThemeName = "VisualStudio2012Light";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // radLabel1
            // 
            this.radLabel1.AutoSize = false;
            this.radLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(219)))));
            this.radLabel1.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.radLabel1.Image = ((System.Drawing.Image)(resources.GetObject("radLabel1.Image")));
            this.radLabel1.Location = new System.Drawing.Point(122, 3);
            this.radLabel1.Name = "radLabel1";
            // 
            // 
            // 
            this.radLabel1.RootElement.EnableElementShadow = false;
            this.radLabel1.Size = new System.Drawing.Size(90, 26);
            this.radLabel1.TabIndex = 174;
            this.radLabel1.Text = "측정기관";
            this.radLabel1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.radLabel1.ThemeName = "VisualStudio2012Light";
            // 
            // radLabel3
            // 
            this.radLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radLabel3.AutoSize = false;
            this.radLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(219)))));
            this.radLabel3.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.radLabel3.Image = ((System.Drawing.Image)(resources.GetObject("radLabel3.Image")));
            this.radLabel3.Location = new System.Drawing.Point(529, 3);
            this.radLabel3.Name = "radLabel3";
            // 
            // 
            // 
            this.radLabel3.RootElement.EnableElementShadow = false;
            this.radLabel3.Size = new System.Drawing.Size(90, 26);
            this.radLabel3.TabIndex = 172;
            this.radLabel3.Text = "측정일자";
            this.radLabel3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.radLabel3.ThemeName = "VisualStudio2012Light";
            // 
            // txtChkjnDate
            // 
            this.txtChkjnDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtChkjnDate.AutoSize = false;
            this.txtChkjnDate.Font = new System.Drawing.Font("맑은 고딕", 11F);
            this.txtChkjnDate.Location = new System.Drawing.Point(626, 3);
            this.txtChkjnDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtChkjnDate.Mask = "9999-99-99";
            this.txtChkjnDate.MaskType = Telerik.WinControls.UI.MaskType.Standard;
            this.txtChkjnDate.Name = "txtChkjnDate";
            this.txtChkjnDate.Size = new System.Drawing.Size(102, 26);
            this.txtChkjnDate.TabIndex = 167;
            this.txtChkjnDate.TabStop = false;
            this.txtChkjnDate.Text = "____-__-__";
            this.txtChkjnDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtChkjnDate.ThemeName = "VisualStudio2012Light";
            // 
            // UC_ChkjnDate
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.txtChkjnDate);
            this.Controls.Add(this.txtChkjnSaupjaName);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.radLabel3);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dtpChkjnDate);
            this.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "UC_ChkjnDate";
            this.Size = new System.Drawing.Size(869, 32);
            ((System.ComponentModel.ISupportInitialize)(this.txtChkjnSaupjaName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpChkjnDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChkjnDate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadTextBox txtChkjnSaupjaName;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadDateTimePicker dtpChkjnDate;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadButton btnSave;
        private Telerik.WinControls.UI.RadButton btnRefresh;
        private Telerik.WinControls.UI.RadMaskedEditBox txtChkjnDate;
    }
}
