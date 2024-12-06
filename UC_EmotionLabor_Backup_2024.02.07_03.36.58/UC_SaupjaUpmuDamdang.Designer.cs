namespace NBOGUN
{
    partial class UC_SaupjaUpmuDamdang
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
            Telerik.WinControls.UI.GridViewComboBoxColumn gridViewComboBoxColumn1 = new Telerik.WinControls.UI.GridViewComboBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn1 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.GridViewCommandColumn gridViewCommandColumn1 = new Telerik.WinControls.UI.GridViewCommandColumn();
            Telerik.WinControls.UI.GridViewCommandColumn gridViewCommandColumn2 = new Telerik.WinControls.UI.GridViewCommandColumn();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_SaupjaUpmuDamdang));
            this.txtYearmon = new Telerik.WinControls.UI.RadMaskedEditBox();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.btnEduItemRefresh = new Telerik.WinControls.UI.RadButton();
            this.btnUpmuDamdangAdd = new Telerik.WinControls.UI.RadButton();
            this.btnUpmuDamdangCopy = new Telerik.WinControls.UI.RadButton();
            this.btnUpmuDamdangSave = new Telerik.WinControls.UI.RadButton();
            this.grdUpmuDamdang = new Telerik.WinControls.UI.RadGridView();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.txtYearmon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEduItemRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpmuDamdangAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpmuDamdangCopy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpmuDamdangSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUpmuDamdang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUpmuDamdang.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtYearmon
            // 
            this.txtYearmon.AutoSize = false;
            this.txtYearmon.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYearmon.Location = new System.Drawing.Point(114, 3);
            this.txtYearmon.Mask = "9999-99";
            this.txtYearmon.MaskType = Telerik.WinControls.UI.MaskType.Standard;
            this.txtYearmon.Name = "txtYearmon";
            this.txtYearmon.Size = new System.Drawing.Size(104, 26);
            this.txtYearmon.TabIndex = 171;
            this.txtYearmon.TabStop = false;
            this.txtYearmon.Text = "____-__";
            this.txtYearmon.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtYearmon.ThemeName = "VisualStudio2012Light";
            // 
            // btnEduItemRefresh
            // 
            this.btnEduItemRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(170)))), ((int)(((byte)(95)))));
            this.btnEduItemRefresh.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnEduItemRefresh.ForeColor = System.Drawing.Color.White;
            this.btnEduItemRefresh.Location = new System.Drawing.Point(225, 3);
            this.btnEduItemRefresh.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnEduItemRefresh.Name = "btnEduItemRefresh";
            this.btnEduItemRefresh.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnEduItemRefresh.Size = new System.Drawing.Size(90, 26);
            this.btnEduItemRefresh.TabIndex = 307;
            this.btnEduItemRefresh.Text = "검  색";
            this.btnEduItemRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEduItemRefresh.ThemeName = "VisualStudio2012Light";
            this.btnEduItemRefresh.Click += new System.EventHandler(this.btnEduItemRefresh_Click);
            // 
            // btnUpmuDamdangAdd
            // 
            this.btnUpmuDamdangAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(170)))), ((int)(((byte)(95)))));
            this.btnUpmuDamdangAdd.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnUpmuDamdangAdd.ForeColor = System.Drawing.Color.White;
            this.btnUpmuDamdangAdd.Location = new System.Drawing.Point(323, 3);
            this.btnUpmuDamdangAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnUpmuDamdangAdd.Name = "btnUpmuDamdangAdd";
            this.btnUpmuDamdangAdd.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnUpmuDamdangAdd.Size = new System.Drawing.Size(106, 26);
            this.btnUpmuDamdangAdd.TabIndex = 308;
            this.btnUpmuDamdangAdd.Text = "담당자 추가";
            this.btnUpmuDamdangAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUpmuDamdangAdd.ThemeName = "VisualStudio2012Light";
            this.btnUpmuDamdangAdd.Click += new System.EventHandler(this.btnUpmuDamdangAdd_Click);
            // 
            // btnUpmuDamdangCopy
            // 
            this.btnUpmuDamdangCopy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(170)))), ((int)(((byte)(95)))));
            this.btnUpmuDamdangCopy.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnUpmuDamdangCopy.ForeColor = System.Drawing.Color.White;
            this.btnUpmuDamdangCopy.Location = new System.Drawing.Point(437, 3);
            this.btnUpmuDamdangCopy.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnUpmuDamdangCopy.Name = "btnUpmuDamdangCopy";
            this.btnUpmuDamdangCopy.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnUpmuDamdangCopy.Size = new System.Drawing.Size(106, 26);
            this.btnUpmuDamdangCopy.TabIndex = 309;
            this.btnUpmuDamdangCopy.Text = "동일 담당자";
            this.btnUpmuDamdangCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUpmuDamdangCopy.ThemeName = "VisualStudio2012Light";
            this.btnUpmuDamdangCopy.Click += new System.EventHandler(this.btnUpmuDamdangCopy_Click);
            // 
            // btnUpmuDamdangSave
            // 
            this.btnUpmuDamdangSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(170)))), ((int)(((byte)(95)))));
            this.btnUpmuDamdangSave.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnUpmuDamdangSave.ForeColor = System.Drawing.Color.White;
            this.btnUpmuDamdangSave.Location = new System.Drawing.Point(1139, 3);
            this.btnUpmuDamdangSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnUpmuDamdangSave.Name = "btnUpmuDamdangSave";
            this.btnUpmuDamdangSave.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnUpmuDamdangSave.Size = new System.Drawing.Size(90, 26);
            this.btnUpmuDamdangSave.TabIndex = 310;
            this.btnUpmuDamdangSave.Text = "저  장";
            this.btnUpmuDamdangSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUpmuDamdangSave.ThemeName = "VisualStudio2012Light";
            this.btnUpmuDamdangSave.Click += new System.EventHandler(this.btnUpmuDamdangSave_Click);
            // 
            // grdUpmuDamdang
            // 
            this.grdUpmuDamdang.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdUpmuDamdang.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdUpmuDamdang.Location = new System.Drawing.Point(3, 35);
            // 
            // 
            // 
            gridViewComboBoxColumn1.FieldName = "Code";
            gridViewComboBoxColumn1.HeaderText = "구분";
            gridViewComboBoxColumn1.Name = "clmCode";
            gridViewComboBoxColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewComboBoxColumn1.Width = 80;
            gridViewTextBoxColumn1.FieldName = "Name";
            gridViewTextBoxColumn1.HeaderText = "성명";
            gridViewTextBoxColumn1.Name = "clmName";
            gridViewTextBoxColumn1.Width = 150;
            gridViewTextBoxColumn2.FieldName = "Sosok";
            gridViewTextBoxColumn2.HeaderText = "소속";
            gridViewTextBoxColumn2.Name = "clmSosok";
            gridViewTextBoxColumn2.Width = 150;
            gridViewTextBoxColumn3.FieldName = "Tel";
            gridViewTextBoxColumn3.HeaderText = "휴대폰";
            gridViewTextBoxColumn3.Name = "clmTel";
            gridViewTextBoxColumn3.Width = 150;
            gridViewTextBoxColumn4.FieldName = "DirectNumber";
            gridViewTextBoxColumn4.HeaderText = "회사번호";
            gridViewTextBoxColumn4.Name = "clmDirectNumber";
            gridViewTextBoxColumn4.Width = 150;
            gridViewTextBoxColumn5.FieldName = "EMail";
            gridViewTextBoxColumn5.HeaderText = "이메일";
            gridViewTextBoxColumn5.Name = "clmEMail";
            gridViewTextBoxColumn5.Width = 200;
            gridViewCheckBoxColumn1.FieldName = "IsSelect";
            gridViewCheckBoxColumn1.HeaderText = "주담당";
            gridViewCheckBoxColumn1.Name = "clmIsSelect";
            gridViewCheckBoxColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewCheckBoxColumn1.Width = 80;
            gridViewCommandColumn1.DefaultText = "삭 제";
            gridViewCommandColumn1.HeaderText = "";
            gridViewCommandColumn1.Name = "clmDel";
            gridViewCommandColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewCommandColumn1.UseDefaultText = true;
            gridViewCommandColumn1.Width = 60;
            gridViewCommandColumn2.DefaultText = "선 택";
            gridViewCommandColumn2.HeaderText = "";
            gridViewCommandColumn2.IsVisible = false;
            gridViewCommandColumn2.Name = "clmSelect";
            gridViewCommandColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewCommandColumn2.UseDefaultText = true;
            gridViewCommandColumn2.Width = 60;
            this.grdUpmuDamdang.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewComboBoxColumn1,
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewCheckBoxColumn1,
            gridViewCommandColumn1,
            gridViewCommandColumn2});
            this.grdUpmuDamdang.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.grdUpmuDamdang.Name = "grdUpmuDamdang";
            this.grdUpmuDamdang.Size = new System.Drawing.Size(1226, 243);
            this.grdUpmuDamdang.TabIndex = 311;
            this.grdUpmuDamdang.ThemeName = "VisualStudio2012Light";
            this.grdUpmuDamdang.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdUpmuDamdang_CellClick);
            this.grdUpmuDamdang.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdUpmuDamdang_CellDoubleClick);
            // 
            // radLabel1
            // 
            this.radLabel1.AutoSize = false;
            this.radLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(219)))));
            this.radLabel1.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.radLabel1.Image = ((System.Drawing.Image)(resources.GetObject("radLabel1.Image")));
            this.radLabel1.Location = new System.Drawing.Point(3, 3);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            // 
            // 
            // 
            this.radLabel1.RootElement.EnableElementShadow = false;
            this.radLabel1.Size = new System.Drawing.Size(105, 26);
            this.radLabel1.TabIndex = 170;
            this.radLabel1.Text = "작업연월";
            this.radLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radLabel1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.radLabel1.ThemeName = "VisualStudio2012Light";
            // 
            // UC_SaupjaUpmuDamdang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdUpmuDamdang);
            this.Controls.Add(this.btnUpmuDamdangSave);
            this.Controls.Add(this.btnUpmuDamdangCopy);
            this.Controls.Add(this.btnUpmuDamdangAdd);
            this.Controls.Add(this.btnEduItemRefresh);
            this.Controls.Add(this.txtYearmon);
            this.Controls.Add(this.radLabel1);
            this.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "UC_SaupjaUpmuDamdang";
            this.Size = new System.Drawing.Size(1233, 281);
            ((System.ComponentModel.ISupportInitialize)(this.txtYearmon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEduItemRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpmuDamdangAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpmuDamdangCopy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpmuDamdangSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUpmuDamdang.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUpmuDamdang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadMaskedEditBox txtYearmon;
        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private Telerik.WinControls.UI.RadButton btnEduItemRefresh;
        private Telerik.WinControls.UI.RadButton btnUpmuDamdangAdd;
        private Telerik.WinControls.UI.RadButton btnUpmuDamdangCopy;
        private Telerik.WinControls.UI.RadButton btnUpmuDamdangSave;
        private Telerik.WinControls.UI.RadGridView grdUpmuDamdang;
    }
}
