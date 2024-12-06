using NBOGUN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace Test
{
    public partial class Form1 : Form
    {
        RadWaitingBar _bar = new RadWaitingBar();
        public Form1()
        {
            InitializeComponent();

            _bar = Biz.Instance.SetWaitingBar();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Biz.Instance.DHCenter = "06";

            Biz.Instance.UserID = "2009112";
            Biz.Instance.UserName = "강승덕";
            Biz.Instance.UserEmailID = "kukuro@kiha21.or.kr";

            _bar.AssociatedControl = radPageViewPage1;
            this.uC_BUSEOJANG_SangtaeDataUpdate1.SetWaitingBar = _bar;
            //uC_BONBU_JidoCodeKeywordSetting1.WaitingBar = _bar;

            SetControl();
        }
        UC_JidoSearch _JidoSearch;
        private void radPageView1_SelectedPageChanged(object sender, EventArgs e)
        {
            SetControl();
        }

        private void SetControl()
        {
            if (radPageView1.SelectedPage == radPageViewPage1)
            {
                _bar = Biz.Instance.SetWaitingBar();
                _bar.AssociatedControl = radPageViewPage1;
                this.uC_BUSEOJANG_SangtaeDataUpdate1.SetWaitingBar = _bar;
            }
            if (radPageView1.SelectedPage == radPageViewPage2 && radPageViewPage2.Controls.Count == 0)
            {
                _bar.AssociatedControl = radPageViewPage2;
                _JidoSearch = new UC_JidoSearch();
                _JidoSearch.WaitingBar = _bar;
                _JidoSearch.VisitDate = DateTime.Now.ToString("yyyy-MM-dd");
                radPageViewPage2.Controls.Add(_JidoSearch);
                _JidoSearch.Dock = DockStyle.Fill;
            }
            if (radPageView1.SelectedPage == radPageViewPage3)
            {
                _bar.AssociatedControl = radPageViewPage3;

                if (radPageViewPage3.Controls.Count == 0)
                {
                    UC_SealzoneDangerPyeongga f = new UC_SealzoneDangerPyeongga();
                    radPageViewPage3.Controls.Add(f);
                    f.Dock = DockStyle.Fill;
                }
                
            }
            //UC_BONBU_DangerSealzoneCode
            if (radPageView1.SelectedPage == radPageViewPage4)
            {
                _bar.AssociatedControl = radPageViewPage4;

                if (radPageViewPage4.Controls.Count == 0)
                {
                    UC_BONBU_DangerSealzoneCode f = new UC_BONBU_DangerSealzoneCode();
                    radPageViewPage4.Controls.Add(f);
                    f.Dock = DockStyle.Fill;
                }
            }

            //감정노동
            if (radPageView1.SelectedPage == radPageViewPage5)
            {
                _bar.AssociatedControl = radPageViewPage5;

                if (radPageViewPage5.Controls.Count == 0)
                {
                    UC_EmotionLabor f = new UC_EmotionLabor();
                    radPageViewPage5.Controls.Add(f);
                    f.Dock = DockStyle.Fill;
                }
            }

            //pvpBonbuSangtaeCode
            //본부상태보고서코드연동
            if (radPageView1.SelectedPage == pvpBonbuKikwanSangtaeCode)
            {
                _bar.AssociatedControl = pvpBonbuKikwanSangtaeCode;

                if (pvpBonbuKikwanSangtaeCode.Controls.Count == 0)
                {
                    UC_BONBU_JidoCode f = new UC_BONBU_JidoCode();
                    f.SetWaitingBar = this._bar;
                    pvpBonbuKikwanSangtaeCode.Controls.Add(f);
                    f.Dock = DockStyle.Fill;
                }
            }
        }
    }
}
