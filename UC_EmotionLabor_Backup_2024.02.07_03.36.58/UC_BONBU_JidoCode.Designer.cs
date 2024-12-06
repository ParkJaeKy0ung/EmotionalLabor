namespace NBOGUN
{
    partial class UC_BONBU_JidoCode
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
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn1 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewCommandColumn gridViewCommandColumn1 = new Telerik.WinControls.UI.GridViewCommandColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.grdJidoCode = new Telerik.WinControls.UI.RadGridView();
            this.btnRefresh = new Telerik.WinControls.UI.RadButton();
            this.btnCodeAdd = new Telerik.WinControls.UI.RadButton();
            this.grdCode = new Telerik.WinControls.UI.RadGridView();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.btnUp = new Telerik.WinControls.UI.RadButton();
            this.btnDown = new Telerik.WinControls.UI.RadButton();
            this.btnOdxSave = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdJidoCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdJidoCode.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCodeAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCode.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOdxSave)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(170)))), ((int)(((byte)(95)))));
            this.btnSave.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSave.Location = new System.Drawing.Point(367, 3);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnSave.Size = new System.Drawing.Size(84, 26);
            this.btnSave.TabIndex = 370;
            this.btnSave.Text = "저장하기";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.ThemeName = "VisualStudio2012Light";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grdJidoCode
            // 
            this.grdJidoCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdJidoCode.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdJidoCode.Location = new System.Drawing.Point(367, 35);
            // 
            // 
            // 
            gridViewCheckBoxColumn1.HeaderText = "√";
            gridViewCheckBoxColumn1.Name = "clmIsSelect";
            gridViewCheckBoxColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewCheckBoxColumn1.Width = 40;
            gridViewTextBoxColumn1.FieldName = "Code";
            gridViewTextBoxColumn1.HeaderText = "코드";
            gridViewTextBoxColumn1.Name = "clmCode";
            gridViewTextBoxColumn1.ReadOnly = true;
            gridViewTextBoxColumn1.Width = 150;
            gridViewTextBoxColumn2.FieldName = "SDate";
            gridViewTextBoxColumn2.HeaderText = "시작일";
            gridViewTextBoxColumn2.Name = "clmSDate";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn2.Width = 92;
            gridViewTextBoxColumn3.FieldName = "EDate";
            gridViewTextBoxColumn3.HeaderText = "종료일";
            gridViewTextBoxColumn3.Name = "clmEDate";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn3.Width = 92;
            gridViewTextBoxColumn4.FieldName = "Name1";
            gridViewTextBoxColumn4.HeaderText = "대";
            gridViewTextBoxColumn4.Name = "clmName1";
            gridViewTextBoxColumn4.ReadOnly = true;
            gridViewTextBoxColumn4.Width = 150;
            gridViewTextBoxColumn5.FieldName = "Name2";
            gridViewTextBoxColumn5.HeaderText = "중";
            gridViewTextBoxColumn5.Name = "clmName2";
            gridViewTextBoxColumn5.ReadOnly = true;
            gridViewTextBoxColumn5.Width = 150;
            gridViewTextBoxColumn6.FieldName = "Name3";
            gridViewTextBoxColumn6.HeaderText = "소";
            gridViewTextBoxColumn6.Name = "clmName3";
            gridViewTextBoxColumn6.ReadOnly = true;
            gridViewTextBoxColumn6.Width = 150;
            this.grdJidoCode.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewCheckBoxColumn1,
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6});
            this.grdJidoCode.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.grdJidoCode.Name = "grdJidoCode";
            this.grdJidoCode.Size = new System.Drawing.Size(1215, 775);
            this.grdJidoCode.TabIndex = 369;
            this.grdJidoCode.ThemeName = "VisualStudio2012Light";
            this.grdJidoCode.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(this.grdJidoCode_CellBeginEdit);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(170)))), ((int)(((byte)(95)))));
            this.btnRefresh.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRefresh.Location = new System.Drawing.Point(3, 3);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnRefresh.Size = new System.Drawing.Size(84, 26);
            this.btnRefresh.TabIndex = 368;
            this.btnRefresh.Text = "새로고침";
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefresh.ThemeName = "VisualStudio2012Light";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnCodeAdd
            // 
            this.btnCodeAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(170)))), ((int)(((byte)(95)))));
            this.btnCodeAdd.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCodeAdd.ForeColor = System.Drawing.Color.White;
            this.btnCodeAdd.Image = global::NBOGUN.Properties.Resources.icon_add;
            this.btnCodeAdd.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCodeAdd.Location = new System.Drawing.Point(95, 3);
            this.btnCodeAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCodeAdd.Name = "btnCodeAdd";
            this.btnCodeAdd.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnCodeAdd.Size = new System.Drawing.Size(43, 26);
            this.btnCodeAdd.TabIndex = 367;
            this.btnCodeAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCodeAdd.ThemeName = "VisualStudio2012Light";
            this.btnCodeAdd.Click += new System.EventHandler(this.btnCodeAdd_Click);
            // 
            // grdCode
            // 
            this.grdCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grdCode.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdCode.Location = new System.Drawing.Point(3, 35);
            // 
            // 
            // 
            gridViewTextBoxColumn7.FieldName = "Idx";
            gridViewTextBoxColumn7.HeaderText = "인덱스";
            gridViewTextBoxColumn7.Name = "clmIdx";
            gridViewTextBoxColumn7.ReadOnly = true;
            gridViewTextBoxColumn7.Width = 60;
            gridViewTextBoxColumn8.FieldName = "Name";
            gridViewTextBoxColumn8.HeaderText = "열 이름";
            gridViewTextBoxColumn8.Name = "clmName";
            gridViewTextBoxColumn8.Width = 200;
            gridViewCommandColumn1.DefaultText = "삭 제";
            gridViewCommandColumn1.HeaderText = "";
            gridViewCommandColumn1.Name = "clmDel";
            gridViewCommandColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewCommandColumn1.UseDefaultText = true;
            gridViewTextBoxColumn9.FieldName = "Odx";
            gridViewTextBoxColumn9.HeaderText = "Odx";
            gridViewTextBoxColumn9.Name = "clmOdx";
            this.grdCode.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn7,
            gridViewTextBoxColumn8,
            gridViewCommandColumn1,
            gridViewTextBoxColumn9});
            this.grdCode.MasterTemplate.ViewDefinition = tableViewDefinition2;
            this.grdCode.Name = "grdCode";
            this.grdCode.Size = new System.Drawing.Size(356, 775);
            this.grdCode.TabIndex = 365;
            this.grdCode.ThemeName = "VisualStudio2012Light";
            this.grdCode.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdCode_CellEndEdit);
            this.grdCode.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdCode_CellClick);
            this.grdCode.DataBindingComplete += new Telerik.WinControls.UI.GridViewBindingCompleteEventHandler(this.grdCode_DataBindingComplete);
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(170)))), ((int)(((byte)(95)))));
            this.btnUp.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnUp.ForeColor = System.Drawing.Color.White;
            this.btnUp.Image = global::NBOGUN.Properties.Resources.icon_up;
            this.btnUp.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnUp.Location = new System.Drawing.Point(146, 3);
            this.btnUp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnUp.Name = "btnUp";
            this.btnUp.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnUp.Size = new System.Drawing.Size(43, 26);
            this.btnUp.TabIndex = 371;
            this.btnUp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUp.ThemeName = "VisualStudio2012Light";
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(170)))), ((int)(((byte)(95)))));
            this.btnDown.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnDown.ForeColor = System.Drawing.Color.White;
            this.btnDown.Image = global::NBOGUN.Properties.Resources.icon_down;
            this.btnDown.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDown.Location = new System.Drawing.Point(197, 3);
            this.btnDown.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDown.Name = "btnDown";
            this.btnDown.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnDown.Size = new System.Drawing.Size(43, 26);
            this.btnDown.TabIndex = 372;
            this.btnDown.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDown.ThemeName = "VisualStudio2012Light";
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnOdxSave
            // 
            this.btnOdxSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(170)))), ((int)(((byte)(95)))));
            this.btnOdxSave.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnOdxSave.ForeColor = System.Drawing.Color.White;
            this.btnOdxSave.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnOdxSave.Location = new System.Drawing.Point(275, 3);
            this.btnOdxSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnOdxSave.Name = "btnOdxSave";
            this.btnOdxSave.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnOdxSave.Size = new System.Drawing.Size(84, 26);
            this.btnOdxSave.TabIndex = 373;
            this.btnOdxSave.Text = "정렬저장";
            this.btnOdxSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOdxSave.ThemeName = "VisualStudio2012Light";
            this.btnOdxSave.Click += new System.EventHandler(this.btnOdxSave_Click);
            // 
            // UC_BONBU_JidoCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnOdxSave);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grdJidoCode);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnCodeAdd);
            this.Controls.Add(this.grdCode);
            this.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "UC_BONBU_JidoCode";
            this.Size = new System.Drawing.Size(1585, 813);
            this.Load += new System.EventHandler(this.UC_BONBU_JidoCode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdJidoCode.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdJidoCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCodeAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCode.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOdxSave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadButton btnSave;
        private Telerik.WinControls.UI.RadGridView grdJidoCode;
        private Telerik.WinControls.UI.RadButton btnRefresh;
        private Telerik.WinControls.UI.RadButton btnCodeAdd;
        private Telerik.WinControls.UI.RadGridView grdCode;
        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private Telerik.WinControls.UI.RadButton btnUp;
        private Telerik.WinControls.UI.RadButton btnDown;
        private Telerik.WinControls.UI.RadButton btnOdxSave;
    }
}
