using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace NBOGUN
{
    public partial class UC_ChkjnDate : UserControl
    {
        int _SaupID;
        string _VisitDate;

        public int SaupID { get => _SaupID; set => _SaupID = value; }
        public string VisitDate { get => _VisitDate; set => _VisitDate = value; }
        string _JaupDate = "";

        private void _Biz_SaupIDChanged(object sender, EventArgs e)
        {
            _SaupID = ((Biz)sender).CurrentSaupID;

            Biz.Show(this, _SaupID.ToString());
        }

        public string JakupDate
        {
            get
            {
                try
                {
                    return _JaupDate;
                }
                catch
                {
                    return "";
                }
            }
        }

        public UC_ChkjnDate()
        {
            InitializeComponent();

            Biz.Instance.SaupIDChanged += _Biz_SaupIDChanged;
        }
        private void dtpChkjnDate_Closing(object sender, Telerik.WinControls.UI.RadPopupClosingEventArgs args)
        {
            txtChkjnDate.Text = dtpChkjnDate.Value.ToString("yyyy-MM-dd");
        }
        public async void RefreshData()
        {
            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                DataTable dt = Biz.Instance.VisitChkjnDateInquire(_SaupID, _VisitDate);

                args.Result = dt;
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {

            };
            worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    DataTable dt = (DataTable)args.Result;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        try
                        {
                            dtpChkjnDate.Value = Convert.ToDateTime(dt.Rows[0]["SaupDate"].ToString());
                        }
                        catch
                        {

                        }

                        txtChkjnDate.Text = dt.Rows[0]["SaupDate"]?.ToString() ?? "____-__-__";
                        txtChkjnSaupjaName.Text = dt.Rows[0]["SaupjaName"]?.ToString() ?? "";

                        this._JaupDate = txtChkjnDate.Text.Trim();
                    }
                    else
                    {
                        txtChkjnDate.Text = "____-__-__";
                        txtChkjnSaupjaName.Text = "";
                        this._JaupDate = "";
                    }
                }
                else
                {
                    Biz.Show(this, "데이터를 불러오는데 실패했습니다.");
                }

                waitingBar.StopWaiting();
            };
            waitingBar.StartWaiting();
            await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
            BeginInvoke(new MethodInvoker(worker.RunWorkerAsync));
        }

        async private void btnSave_Click(object sender, EventArgs e)
        {
            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                SqlConnection con = Biz.Instance.Connection;
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlTransaction tran = con.BeginTransaction();
                int r = -1;
                string saupjaname = txtChkjnSaupjaName.Text.Trim();
                string saupdate = txtChkjnDate.Text;

                r = Biz.Instance.JakupSaupSave(con, tran, _SaupID, _VisitDate, saupjaname, saupdate);

                if (r < 1)
                {
                    tran.Rollback();
                    con.Close();
                    return;
                }

                tran.Commit();
                con.Close();

                args.Result = r;
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {

            };
            worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result != null)
                {
                    _JaupDate = dtpChkjnDate.Value.ToString("yyyy-MM-dd");
                    Biz.Show(this, "저장에 성공했습니다.");
                }
                else
                {
                    Biz.Show(this, "저장에 실패했습니다.");
                }

                waitingBar.StopWaiting();
            };
            waitingBar.StartWaiting();
            await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
            BeginInvoke(new MethodInvoker(worker.RunWorkerAsync));
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

    }
}
