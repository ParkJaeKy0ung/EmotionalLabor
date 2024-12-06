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

namespace NBOGUN
{
    public partial class UC_BONBU_OverVisit : UserControl
    {
        AbortableBackgroundWorker _Worker;
        RadWaitingBar _WaitingBar;

        public UC_BONBU_OverVisit()
        {
            InitializeComponent();

            Biz.Instance.SetDropDownList(cboYear);
            Biz.Instance.SetGridViewOption(grdVisit);

            _WaitingBar = Biz.Instance.SetWaitingBar();
            _WaitingBar.AssociatedControl = this;

            for (int i = DateTime.Now.Year; i > 2019; i--)
            {
                //cboExcelYear.Items.Add(i.ToString());
                cboYear.Items.Add(i.ToString());
            }
        }

        ~UC_BONBU_OverVisit()
        {
            if(_Worker != null && _Worker.IsBusy)
            {
                _Worker.Abort();
                _Worker = null;
            }
        }

        private void UC_BONBU_OverVisit_Load(object sender, EventArgs e)
        {

        }

        async public void  RefreshData()
        {
            string year = cboYear.Text;
            try
            {
                year = Convert.ToDateTime(year + "-01-01").Year.ToString();
            }
            catch
            {
                year = "";
            }

            if (year == "")
                return;
            
            _Worker = new AbortableBackgroundWorker();

            _Worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                DataTable dt = Biz.Instance.BONBU_OverVisitList(year);

                //_Worker.ReportProgress(1, dt);
                if (dt != null)
                {
                    args.Result = dt;
                }
            };
            _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                if(args.ProgressPercentage == 1)
                {
                    //DataTable dt = (DataTable)args.UserState;
                    //grdVisit.DataSource = dt;
                }
            };
            _Worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    DataTable dt = (DataTable)args.Result;
                    grdVisit.DataSource = dt;
                }
                else
                {
                    Biz.Show(this, "데이터를 불러올 수 없습니다.");
                }                
            };

            _WaitingBar.StartWaiting();
            await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
            BeginInvoke(new MethodInvoker(_Worker.RunWorkerAsync));
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Biz.ExportGridView(grdVisit);
        }

        private void grdVisit_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            _WaitingBar.StopWaiting();
        }
    }
}
