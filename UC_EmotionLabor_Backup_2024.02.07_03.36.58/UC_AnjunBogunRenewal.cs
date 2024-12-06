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
    public partial class UC_AnjunBogunRenewal : UserControl
    {
        string _Year;
        int _SaupID;

        DataTable mTableAnjunBogunRenewal;
        AbortableBackgroundWorker _Worker;

        public int SaupID
        {
            set { _SaupID = value; }
        }

        public string Year
        {
            set => _Year = value;
        }

        async public void RefreshData()
        {
            if(_Worker != null &&  _Worker.IsBusy)
            {
                Biz.Show("진행중인 작업이 있으므로 잠시 후 다시 실행해 주십시오.");
                return;
            }

            string year;

            try
            {
                year = Convert.ToDateTime(_Year + "-01-01").Year.ToString();
            }
            catch
            {
                year = DateTime.Now.Year.ToString();
                //cboYear.Text = year;
            }

            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            _Worker = new AbortableBackgroundWorker();

            _Worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                mTableAnjunBogunRenewal = Biz.Instance.AnjunBogunRenewalList(_SaupID, year, (cboYear.Text == "전체" ? "3" : "1"));

                if (mTableAnjunBogunRenewal != null)
                {
                    args.Result = mTableAnjunBogunRenewal;
                }
            };
            _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
            };
            _Worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    //DataTable dt = (DataTable)args.Result;
                    grdAnjunBogunRenewal.DataSource = mTableAnjunBogunRenewal;
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

        public UC_AnjunBogunRenewal()
        {
            InitializeComponent();

            Biz.Instance.SetGridViewOption(grdAnjunBogunRenewal);
            Biz.Instance.SetDropDownList(cboYear);

            cboYear.Items.Add("전체");

            for (int i = DateTime.Now.Year; i > 2019; i--)
            {
                //cboExcelYear.Items.Add(i.ToString());
                cboYear.Items.Add(i.ToString());
            }

            cboYear.SelectedIndex = 0;

            GridViewComboBoxColumn col = this.grdAnjunBogunRenewal.Columns["clmGubun"] as GridViewComboBoxColumn;
            col.DisplayMember = "Name";
            col.ValueMember = "Code";
            col.DataSource = Biz.Instance.GetDHCodeList("사업장안전보건관리규정");

            Biz.Instance.SaupIDChanged += Instance_SaupIDChanged;
        }

        private void Instance_SaupIDChanged(object sender, EventArgs e)
        {
            _SaupID = NBOGUN.Biz.Instance.CurrentSaupID;

            RefreshData();// Biz.Show(this, "바뀜");
        }

        ~UC_AnjunBogunRenewal()
        {
            if(_Worker != null && _Worker.IsBusy)
            {
                _Worker.CancelAsync();
                _Worker.Abort();
                _Worker.Dispose();
            }
        }
        private void UC_AnjunBogunRenewal_Load(object sender, EventArgs e)
        {
        }

        private void cboYear_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            _Year = cboYear.Text;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataRow row = this.mTableAnjunBogunRenewal.NewRow();
            mTableAnjunBogunRenewal.Rows.InsertAt(row, 0);
            mTableAnjunBogunRenewal.AcceptChanges();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (grdAnjunBogunRenewal.SelectedRows == null || grdAnjunBogunRenewal.SelectedRows.Count == 0)
                return;

            grdAnjunBogunRenewal.Rows.Remove(grdAnjunBogunRenewal.SelectedRows[0]);
            mTableAnjunBogunRenewal.AcceptChanges();
        }

        private void btnJaehaejaSearch_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (_SaupID < 0)
            {
                Biz.Show(this, "사업장을 선택해 주십시오.");
                return;
            }

            if (grdAnjunBogunRenewal.DataSource == null)
            {
                Biz.Show(this, "엑셀전환을 할 수 없습니다.");
                return;
            }

            Biz.ExportGridView(grdAnjunBogunRenewal);
        }

        async private void btnSave_Click(object sender, EventArgs e)
        {

            if (_Worker != null && _Worker.IsBusy)
            {
                Biz.Show("진행중인 작업이 있으므로 잠시 후 다시 실행해 주십시오.");
                return;
            }

            string year;
            int r;
            string gubun = "";
            string date = "";
            string content = "";

            try
            {
                year = Convert.ToDateTime(_Year + "-01-01").Year.ToString();
            }
            catch
            {
                year = DateTime.Now.Year.ToString();
                //cboYear.Text = year;
            }

            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            _Worker = new AbortableBackgroundWorker();

            _Worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                SqlConnection con = Biz.Instance.Connection;
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlTransaction tran = con.BeginTransaction();

                r = Biz.Instance.AnjunBogunRenewalDel(con, tran, _SaupID, (cboYear.Text == "전체" ? "" : year));

                if (r < 0)
                {
                    tran.Rollback();
                    con.Close();
                    
                    return;
                }

                grdAnjunBogunRenewal.Rows.ForEach(row => {
                    gubun = row.Cells["clmGubun"].Value.ToString();
                    date = row.Cells["clmDate"].Value.ToString();
                    content = row.Cells["clmContent"].Value.ToString();

                    r = Biz.Instance.AnjunBogunRenewalAdd(con, tran, _SaupID, date, gubun, content);

                    if (r < 0)
                    {
                        tran.Rollback();
                        con.Close();

                        return;
                    }
                });

                tran.Commit();
                con.Close();

                mTableAnjunBogunRenewal = Biz.Instance.AnjunBogunRenewalList(_SaupID, year, (cboYear.Text == "전체" ? "3" : "1"));

                if (mTableAnjunBogunRenewal != null)
                {
                    args.Result = mTableAnjunBogunRenewal;
                }
            };
            _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
            };
            _Worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    //DataTable dt = (DataTable)args.Result;
                    grdAnjunBogunRenewal.DataSource = mTableAnjunBogunRenewal;
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
            BeginInvoke(new MethodInvoker(_Worker.RunWorkerAsync));
        }
    }
}
