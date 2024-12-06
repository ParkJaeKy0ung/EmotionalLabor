using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace NBOGUN.Sub
{
    public partial class frmDamdang : RadForm
    {
        string _Yearmon;

        AbortableBackgroundWorker _Worker;

        public string DamdangDr
        {
            get
            {
                string sabun = "";

                if (grdDr.SelectedRows.Count > 0)
                    sabun = grdDr.SelectedRows[0].Cells["clmSabun"].Value.ToString();

                return sabun;
            }
        }

        public string DamdangDrName
        {
            get
            {
                string sabun = "";

                if (grdDr.SelectedRows.Count > 0)
                    sabun = grdDr.SelectedRows[0].Cells["clmName"].Value.ToString();

                return sabun;
            }
        }

        public string DamdangNr
        {
            get
            {
                string sabun = "";

                if (grdNr.SelectedRows.Count > 0)
                    sabun = grdNr.SelectedRows[0].Cells["clmSabun"].Value.ToString();

                return sabun;
            }
        }

        public string DamdangNrName
        {
            get
            {
                string sabun = "";

                if (grdNr.SelectedRows.Count > 0)
                    sabun = grdNr.SelectedRows[0].Cells["clmName"].Value.ToString();

                return sabun;
            }
        }

        public string DamdangHr
        {
            get
            {
                string sabun = "";

                if (grdHr.SelectedRows.Count > 0)
                    sabun = grdHr.SelectedRows[0].Cells["clmSabun"].Value.ToString();

                return sabun;
            }
        }

        public string DamdangHrName
        {
            get
            {
                string sabun = "";

                if (grdHr.SelectedRows.Count > 0)
                    sabun = grdHr.SelectedRows[0].Cells["clmName"].Value.ToString();

                return sabun;
            }
        }

        async public void RefreshData()
        {
            if (_Worker != null && _Worker.IsBusy)
            {
                Biz.Show("진행중인 작업이 있으므로 잠시 후 다시 실행해 주십시오.");
                return;
            }

            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            _Worker = new AbortableBackgroundWorker();

            _Worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                DataTable dtDr = Biz.Instance.GetStaffList(_Yearmon, "DH1601");
                DataTable dtNr = Biz.Instance.GetStaffList(_Yearmon, "DH1602");
                DataTable dtHr = Biz.Instance.GetStaffList(_Yearmon, "DH1603");

                DataSet dataSet = new DataSet();
                dataSet.Tables.Add(dtDr);
                dataSet.Tables.Add(dtNr);   
                dataSet.Tables.Add(dtHr);

                args.Result = dataSet;
            };
            _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
            };
            _Worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataSet)
                {
                    grdDr.DataSource = ((DataSet)args.Result).Tables[0];
                    grdNr.DataSource = ((DataSet)args.Result).Tables[1];
                    grdHr.DataSource = ((DataSet)args.Result).Tables[2];
                }
                else
                {
                    Biz.Show(this, "데이터를 불러올 수 없습니다.");
                }

                waitingBar.StopWaiting();
            };

            waitingBar.StartWaiting();
            await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
            BeginInvoke(new MethodInvoker(_Worker.RunWorkerAsync));
        }
        public frmDamdang(string Yearmon)
        {
            InitializeComponent();

            Biz.Instance.SetGridViewOption(grdDr);
            Biz.Instance.SetGridViewOption(grdNr);
            Biz.Instance.SetGridViewOption(grdHr);

            _Yearmon = Yearmon;

            RefreshData();
        }

        private void frmDamdang_Load(object sender, EventArgs e)
        {
            
        }

        private void frmDamdang_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(grdDr.CurrentRow != null || grdHr.CurrentRow != null || grdNr.CurrentRow != null)
            {
                this.DialogResult = DialogResult.OK;    
            }
        }
    }
}
