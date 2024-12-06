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
using Telerik.Windows.Diagrams.Core;

namespace NBOGUN
{
    public partial class UC_BONBU_BagiCode : UserControl
    {
        AbortableBackgroundWorker _Worker;
        DataTable _TableCode;

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
                _TableCode = Biz.Instance.GetDHCodeList("HD");
                if(_TableCode.Columns.Contains("Cnt") == false)
                    _TableCode.Columns.Add("Cnt", typeof(string));
                args.Result = _TableCode;
            };
            _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
            };
            _Worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    grdCode.DataSource = _TableCode;
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

        async void SaveData()
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
                SqlConnection con = Biz.Instance.Connection;
                if (con.State != ConnectionState.Open) con.Open();
                SqlTransaction tran = con.BeginTransaction();

                int r;

                //var rows = grdCode.Rows.Where(row => row.Cells["clmCode"].Value?.ToString() == "");
                var rows = grdCode.Rows;

                if(rows.Count() > 0)
                {
                    rows.AsParallel().ForEach(row => {
                        r = Biz.Instance.BONBU_CodeHoodSave(con, tran, row.Cells["clmCode"].Value?.ToString() ?? "", row.Cells["clmGroupName"].Value?.ToString() ?? "", row.Cells["clmName"].Value?.ToString() ?? "", row.Cells["clmWindSpeed"].Value?.ToString() ?? "");

                        if (r < 0)
                        {
                            tran.Rollback();
                            con.Close();
                            return;
                        }
                    });
                }

                tran.Commit();
                con.Close();

                _TableCode = Biz.Instance.GetDHCodeList("HD");
                if (_TableCode.Columns.Contains("Cnt") == false)
                    _TableCode.Columns.Add("Cnt", typeof(string));
                args.Result = _TableCode;
            };
            _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
            };
            _Worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    grdCode.DataSource = _TableCode;
                }
                else
                {
                    Biz.Show(this, "저장에 실패했습니다.");
                }

                waitingBar.StopWaiting();
            };

            waitingBar.StartWaiting();
            await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
            BeginInvoke(new MethodInvoker(_Worker.RunWorkerAsync));
        }

        async void DelCode(string Code)
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
                SqlConnection con = Biz.Instance.Connection;
                if (con.State != ConnectionState.Open) con.Open();
                SqlTransaction tran = con.BeginTransaction();

                int r;

                r = Biz.Instance.BONBU_CodeHoodDel(con, tran, Code);

                if (r < 0)
                {
                    tran.Rollback();
                    con.Close();
                    return;
                }

                tran.Commit();
                con.Close();

                _TableCode = Biz.Instance.GetDHCodeList("HD");
                if (_TableCode.Columns.Contains("Cnt") == false)
                    _TableCode.Columns.Add("Cnt", typeof(string));
                args.Result = _TableCode;
            };
            _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
            };
            _Worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    grdCode.DataSource = _TableCode;
                }
                else
                {
                    Biz.Show(this, "삭제에 실패했습니다.");
                }

                waitingBar.StopWaiting();
            };

            waitingBar.StartWaiting();
            await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
            BeginInvoke(new MethodInvoker(_Worker.RunWorkerAsync));
        }

        public UC_BONBU_BagiCode()
        {
            InitializeComponent();

            Biz.Instance.SetGridViewOption(grdCode);
        }

        ~UC_BONBU_BagiCode()
        {
            if(_Worker != null && _Worker.IsBusy)
            {
                _Worker.Abort();
                _Worker.Dispose();
            }
        }

        private void UC_BONBU_BagiCode_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(_TableCode == null)
            {
                Biz.Show(this, "최초 데이터를 불러와 주시기 바랍니다.");
                return;
            }

            DataRow row = this._TableCode.NewRow();
            _TableCode.Rows.Add(row);
            _TableCode.AcceptChanges();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (grdCode.SelectedRows == null || grdCode.SelectedRows.Count == 0)
                return;

            if (grdCode.SelectedRows[0].Cells["clmCnt"].Value?.ToString() != "" && grdCode.SelectedRows[0].Cells["clmCnt"].Value?.ToString() != "0")
            {
                Biz.Show("사용한 코드는 삭제 할 수 없습니다.");
                return;
            }

            string code = grdCode.SelectedRows[0].Cells["clmCode"].Value?.ToString() ?? "";

            if(code == "")
            {
                grdCode.Rows.Remove(grdCode.SelectedRows[0]);
                _TableCode.AcceptChanges();
            }
            else
            {
                DelCode(code);
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void grdCode_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            grdCode.Rows.AsParallel().ForEach(row => {
                DataTable dt = Biz.Instance.BONBU_CodeHoodCnt(row.Cells["clmCode"].Value.ToString());

                if(dt != null && dt.Rows.Count > 0)
                {
                    row.Cells["clmCnt"].Value = dt.Rows[0]["Cnt"];
                }
            });
        }
    }
}
