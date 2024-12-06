namespace NBOGUN
{
    partial class UC_BONBU_SangtaeCodeKeyword
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.grdCode = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCode.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // grdCode
            // 
            this.grdCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grdCode.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdCode.Location = new System.Drawing.Point(3, 3);
            // 
            // 
            // 
            gridViewTextBoxColumn1.FieldName = "Code";
            gridViewTextBoxColumn1.HeaderText = "코드";
            gridViewTextBoxColumn1.Name = "clmCode";
            gridViewTextBoxColumn1.ReadOnly = true;
            gridViewTextBoxColumn1.Width = 100;
            gridViewTextBoxColumn2.HeaderText = "이름";
            gridViewTextBoxColumn2.Name = "column1";
            gridViewTextBoxColumn2.ReadOnly = true;
            this.grdCode.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2});
            this.grdCode.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.grdCode.Name = "grdCode";
            this.grdCode.Size = new System.Drawing.Size(360, 735);
            this.grdCode.TabIndex = 0;
            this.grdCode.ThemeName = "VisualStudio2012Light";
            // 
            // UC_BONBU_SangtaeCodeKeyword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdCode);
            this.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "UC_BONBU_SangtaeCodeKeyword";
            this.Size = new System.Drawing.Size(1430, 741);
            this.Load += new System.EventHandler(this.UC_BONBU_SangtaeCodeKeyword_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdCode.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private Telerik.WinControls.UI.RadGridView grdCode;
    }
}
