namespace Test
{
    partial class Form1
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.radPageView1 = new Telerik.WinControls.UI.RadPageView();
            this.radPageViewPage1 = new Telerik.WinControls.UI.RadPageViewPage();
            this.uC_BUSEOJANG_SangtaeDataUpdate1 = new NBOGUN.UC_BUSEOJANG_SangtaeDataUpdate();
            this.radPageViewPage2 = new Telerik.WinControls.UI.RadPageViewPage();
            this.radPageViewPage3 = new Telerik.WinControls.UI.RadPageViewPage();
            this.radPageViewPage4 = new Telerik.WinControls.UI.RadPageViewPage();
            this.radPageViewPage5 = new Telerik.WinControls.UI.RadPageViewPage();
            this.pvpBonbuKikwanSangtaeCode = new Telerik.WinControls.UI.RadPageViewPage();
            this.pvpBonbuSangtaeJidoCode = new Telerik.WinControls.UI.RadPageViewPage();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            ((System.ComponentModel.ISupportInitialize)(this.radPageView1)).BeginInit();
            this.radPageView1.SuspendLayout();
            this.radPageViewPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // radPageView1
            // 
            this.radPageView1.Controls.Add(this.radPageViewPage1);
            this.radPageView1.Controls.Add(this.radPageViewPage2);
            this.radPageView1.Controls.Add(this.radPageViewPage3);
            this.radPageView1.Controls.Add(this.radPageViewPage4);
            this.radPageView1.Controls.Add(this.radPageViewPage5);
            this.radPageView1.Controls.Add(this.pvpBonbuKikwanSangtaeCode);
            this.radPageView1.Controls.Add(this.pvpBonbuSangtaeJidoCode);
            this.radPageView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPageView1.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radPageView1.Location = new System.Drawing.Point(0, 0);
            this.radPageView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radPageView1.Name = "radPageView1";
            this.radPageView1.SelectedPage = this.pvpBonbuSangtaeJidoCode;
            this.radPageView1.Size = new System.Drawing.Size(2058, 1041);
            this.radPageView1.TabIndex = 1;
            this.radPageView1.ThemeName = "VisualStudio2012Light";
            this.radPageView1.SelectedPageChanged += new System.EventHandler(this.radPageView1_SelectedPageChanged);
            // 
            // radPageViewPage1
            // 
            this.radPageViewPage1.Controls.Add(this.uC_BUSEOJANG_SangtaeDataUpdate1);
            this.radPageViewPage1.ItemSize = new System.Drawing.SizeF(144F, 29F);
            this.radPageViewPage1.Location = new System.Drawing.Point(13, 75);
            this.radPageViewPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radPageViewPage1.Name = "radPageViewPage1";
            this.radPageViewPage1.Size = new System.Drawing.Size(2126, 1688);
            this.radPageViewPage1.Text = "radPageViewPage1";
            // 
            // uC_BUSEOJANG_SangtaeDataUpdate1
            // 
            this.uC_BUSEOJANG_SangtaeDataUpdate1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uC_BUSEOJANG_SangtaeDataUpdate1.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.uC_BUSEOJANG_SangtaeDataUpdate1.Location = new System.Drawing.Point(0, 0);
            this.uC_BUSEOJANG_SangtaeDataUpdate1.Margin = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.uC_BUSEOJANG_SangtaeDataUpdate1.Name = "uC_BUSEOJANG_SangtaeDataUpdate1";
            this.uC_BUSEOJANG_SangtaeDataUpdate1.Size = new System.Drawing.Size(2126, 1688);
            this.uC_BUSEOJANG_SangtaeDataUpdate1.TabIndex = 0;
            // 
            // radPageViewPage2
            // 
            this.radPageViewPage2.ItemSize = new System.Drawing.SizeF(144F, 29F);
            this.radPageViewPage2.Location = new System.Drawing.Point(12, 70);
            this.radPageViewPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radPageViewPage2.Name = "radPageViewPage2";
            this.radPageViewPage2.Size = new System.Drawing.Size(2129, 1698);
            this.radPageViewPage2.Text = "radPageViewPage2";
            // 
            // radPageViewPage3
            // 
            this.radPageViewPage3.ItemSize = new System.Drawing.SizeF(144F, 29F);
            this.radPageViewPage3.Location = new System.Drawing.Point(12, 70);
            this.radPageViewPage3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radPageViewPage3.Name = "radPageViewPage3";
            this.radPageViewPage3.Size = new System.Drawing.Size(2129, 1698);
            this.radPageViewPage3.Text = "radPageViewPage3";
            // 
            // radPageViewPage4
            // 
            this.radPageViewPage4.ItemSize = new System.Drawing.SizeF(144F, 29F);
            this.radPageViewPage4.Location = new System.Drawing.Point(12, 70);
            this.radPageViewPage4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radPageViewPage4.Name = "radPageViewPage4";
            this.radPageViewPage4.Size = new System.Drawing.Size(2129, 1698);
            this.radPageViewPage4.Text = "radPageViewPage4";
            // 
            // radPageViewPage5
            // 
            this.radPageViewPage5.ItemSize = new System.Drawing.SizeF(74F, 29F);
            this.radPageViewPage5.Location = new System.Drawing.Point(5, 35);
            this.radPageViewPage5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radPageViewPage5.Name = "radPageViewPage5";
            this.radPageViewPage5.Size = new System.Drawing.Size(2048, 1001);
            this.radPageViewPage5.Text = "감정노동";
            // 
            // pvpBonbuKikwanSangtaeCode
            // 
            this.pvpBonbuKikwanSangtaeCode.ItemSize = new System.Drawing.SizeF(164F, 29F);
            this.pvpBonbuKikwanSangtaeCode.Location = new System.Drawing.Point(5, 35);
            this.pvpBonbuKikwanSangtaeCode.Name = "pvpBonbuKikwanSangtaeCode";
            this.pvpBonbuKikwanSangtaeCode.Size = new System.Drawing.Size(2048, 1001);
            this.pvpBonbuKikwanSangtaeCode.Text = "본부기관평가지도코드";
            // 
            // pvpBonbuSangtaeJidoCode
            // 
            this.pvpBonbuSangtaeJidoCode.ItemSize = new System.Drawing.SizeF(180F, 29F);
            this.pvpBonbuSangtaeJidoCode.Location = new System.Drawing.Point(5, 35);
            this.pvpBonbuSangtaeJidoCode.Name = "pvpBonbuSangtaeJidoCode";
            this.pvpBonbuSangtaeJidoCode.Size = new System.Drawing.Size(2048, 1001);
            this.pvpBonbuSangtaeJidoCode.Text = "본부상태보고서지도코드";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2058, 1041);
            this.Controls.Add(this.radPageView1);
            this.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radPageView1)).EndInit();
            this.radPageView1.ResumeLayout(false);
            this.radPageViewPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Telerik.WinControls.UI.RadPageView radPageView1;
        private Telerik.WinControls.UI.RadPageViewPage radPageViewPage1;
        private Telerik.WinControls.UI.RadPageViewPage radPageViewPage2;
        private Telerik.WinControls.UI.RadPageViewPage radPageViewPage3;
        private Telerik.WinControls.UI.RadPageViewPage radPageViewPage4;
        private NBOGUN.UC_BUSEOJANG_SangtaeDataUpdate uC_BUSEOJANG_SangtaeDataUpdate1;
        private Telerik.WinControls.UI.RadPageViewPage radPageViewPage5;
        private Telerik.WinControls.UI.RadPageViewPage pvpBonbuKikwanSangtaeCode;
        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private Telerik.WinControls.UI.RadPageViewPage pvpBonbuSangtaeJidoCode;
        //private NBOGUN.UC_BONBU_JidoCodeKeywordSetting uC_BONBU_JidoCodeKeywordSetting1;
    }
}

