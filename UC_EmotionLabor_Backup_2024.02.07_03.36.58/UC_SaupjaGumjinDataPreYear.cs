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
    public partial class UC_SaupjaGumjinDataPreYear : UserControl
    {
        int _SaupID;
        string _VisitDate;
        string _Visitor;

        string _Year;

        string _FKita1 = "";

        AbortableBackgroundWorker _Worker;

        public int SaupID { get => _SaupID; set => _SaupID = value; }
        public string VisitDate { get => _VisitDate; set => _VisitDate = value; }
        public string Visitor { get => _Visitor; set => _Visitor = value; }
        public string FKita1 { get => _FKita1; set => _FKita1 = value; }

        async public void RefreshData(string Gubun = "2")
        {
            //기존 데이터 삭제하기
            if (_Worker != null && _Worker.IsBusy)
            {
                Biz.Show(this, "다른 작업이 진행중입니다.");
                return;
            }

            //기존 데이터 삭제하기
            string yearitem = cboYear.Text;
            if (_Year != "")
            {
                //방문일을 선택했고 검색연도가 같다면 방문일을 기준으로 검색
                if (_Year == yearitem.Substring(0, 4))
                    yearitem = _Year;
                else
                {
                    //방문일을 선택했고 검색연도가 다르다면 검색연도를 기준으로 검색
                }
            }
            else if (_Year == "" && yearitem == DateTime.Now.Year.ToString())
            {
                //방문일을 선택 안했지만 검색연도가 올해라면 오늘을 기준으로 검색
                yearitem = DateTime.Now.ToString("yyyy-MM-dd");
            }

            var con = Biz.Instance.SetWaitingBar();
            RadWaitingBar waitingBar = con;
            waitingBar.AssociatedControl = this;
            _Worker = new AbortableBackgroundWorker();

            _Worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                DataTable r = Biz.Instance.SangtaeGumjinDataList(_SaupID, (Gubun == "1" ? _Year : _VisitDate), Gubun);

                args.Result = r;
            };
            _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
            };
            _Worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    DataTable dt = args.Result as DataTable;
                    if (dt.Rows.Count == 1)
                    {
                        txtD1.Text = dt.Rows[0]["D1"].ToString();
                        txtD2.Text = dt.Rows[0]["D2"].ToString();
                        txtDN.Text = dt.Rows[0]["DN"].ToString();
                        txtC1.Text = dt.Rows[0]["C1"].ToString();
                        txtC2.Text = dt.Rows[0]["C2"].ToString();
                        txtCN.Text = dt.Rows[0]["CN"].ToString();
                    }
                    else
                    {
                        txtD1.Text = "0";
                        txtD2.Text = "0";
                        txtDN.Text = "0";
                        txtC1.Text = "0";
                        txtC2.Text = "0";
                        txtCN.Text = "0";
                    }
                }
                else
                    Biz.Show(this, "데이터를 불러오는데 실패했습니다.");

                waitingBar.StopWaiting();
            };

            waitingBar.StartWaiting();
            await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
            BeginInvoke(new MethodInvoker(_Worker.RunWorkerAsync));
        }

        public UC_SaupjaGumjinDataPreYear()
        {
            InitializeComponent();

            Biz.Instance.SetDropDownList(cboYear);

            for (int i = DateTime.Now.Year; i > 2019; i--)
            {
                //cboExcelYear.Items.Add(i.ToString());
                cboYear.Items.Add(i.ToString());
            }

            cboYear.SelectedIndex = 0;

            Biz.SetTextBox(txtC1);
            Biz.SetTextBox(txtC2);
            Biz.SetTextBox(txtCN);
            Biz.SetTextBox(txtD1);
            Biz.SetTextBox(txtD2);
            Biz.SetTextBox(txtDN);
        }

        private void cboYear_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            _Year = cboYear.Text;
        }

        //새로고침
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData("1");
        }

        //초기화
        private void btnClear_Click(object sender, EventArgs e)
        {
            RefreshData("2");
        }

        async private void btnSave_Click(object sender, EventArgs e)
        {
            string d1 = int.TryParse(txtD1.Text.Trim(), out int result1) ? result1.ToString() : "0";
            string d2 = int.TryParse(txtD2.Text.Trim(), out int result2) ? result2.ToString() : "0";
            string dn = int.TryParse(txtDN.Text.Trim(), out int result3) ? result3.ToString() : "0";
            string c1 = int.TryParse(txtC1.Text.Trim(), out int result4) ? result4.ToString() : "0";
            string c2 = int.TryParse(txtC2.Text.Trim(), out int result5) ? result5.ToString() : "0";
            string cn = int.TryParse(txtCN.Text.Trim(), out int result6) ? result6.ToString() : "0";

            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            _Worker = new AbortableBackgroundWorker();
            _Worker.WorkerSupportsCancellation = true;
            _Worker.WorkerReportsProgress = true;

            _Worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                SqlConnection con = Biz.Instance.Connection;
                if (con.State != ConnectionState.Open) con.Open();
                SqlTransaction tran = con.BeginTransaction();

                DataTable r = Biz.Instance.SangtaeGumjinDataSave(con, tran, _SaupID, _VisitDate, _Visitor, d1, d2, dn, c1, c2, cn);

                if (r == null || r.Rows.Count < 1)
                {
                    tran.Rollback();
                    con.Close();
                    return;
                }

                tran.Commit();
                con.Close();
                args.Result = r;
            };
            _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                try
                {
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog(ex.Message);
                }

            };
            _Worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    _FKita1 = ((DataTable)args.Result).Rows[0]["FKita1"].ToString();
                    Biz.Show(this, "저장에 성공했습니다.");
                }
                else
                    Biz.Show(this, "저장에 실패했습니다.");

                waitingBar.StopWaiting();
            };
            waitingBar.StartWaiting();
            await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
            BeginInvoke(new MethodInvoker(_Worker.RunWorkerAsync));
        }

        private void btnRefreshPre_Click(object sender, EventArgs e)
        {
            RefreshData("3");
        }

        async private void UC_SaupjaGumjinDataPreYear_Load(object sender, EventArgs e)
        {
            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            _Worker = new AbortableBackgroundWorker();
            _Worker.WorkerSupportsCancellation = true;
            _Worker.WorkerReportsProgress = true;

            _Worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                DataTable r = Biz.Instance.VisitDateSignInquire(_SaupID, _VisitDate, "", "1");

                if (r == null)
                {
                    return;
                }
                else
                    args.Result = r.Rows.Count;
            };
            _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                try
                {
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog(ex.Message);
                }

            };
            _Worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null)
                {
                    if ((int)args.Result > 0)
                    {
                        btnSave.Enabled = false;
                        btnSave.Text = "수정 불가";
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        btnSave.Text = "저  장";
                        //저  장
                    }
                }
                else
                    Biz.Show(this, "사인 데이터 불러오는데 실패했습니다.");

                waitingBar.StopWaiting();
            };
            waitingBar.StartWaiting();
            await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
            BeginInvoke(new MethodInvoker(_Worker.RunWorkerAsync));

            RefreshData();
        }
    }
}
