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
    public partial class UC_HealthUp : UserControl
    {
        string DisplayGubun = "1";////1:전체, 2:당해연도

        int _SaupID;
        public int SaupID
        {
            get { return _SaupID; }
            set { _SaupID = value; }
        }

        string _VisitDate;
        public string VisitDate
        {
            get { return _VisitDate; }
            set { _VisitDate = value; }
        }

        async public void RefreshData()
        {
            //기존 데이터 삭제하기
            string year;
            if(DisplayGubun == "1")
            {
                year = "";
            }
            else 
            {
                year = _VisitDate.Substring(0, 4);
            }

            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();

            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                DataTable dsItem = Biz.Instance.SangdamHealthList(_SaupID, "", "", "1");

                args.Result = dsItem;
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
            };
            worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    grdHealthProgram3.DataSource = null;
                    grdHealthProgram1.DataSource = (DataTable)args.Result; 
                }
                else
                {
                    Biz.Show(this, "데이터를 불러올 수 없습니다.");
                }

                waitingBar.StopWaiting();
            };

            waitingBar.StartWaiting();
            await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
            BeginInvoke(new MethodInvoker(worker.RunWorkerAsync));
        }

        public UC_HealthUp()
        {
            InitializeComponent();

            Biz.Instance.SetGridViewOption(grdSubject);
            Biz.Instance.SetGridViewOption(grdHealth);
            Biz.Instance.SetGridViewOption(grdHealthProgram1);
            Biz.Instance.SetGridViewOption(grdHealthProgram3);
            Biz.Instance.SetGridViewOption(grdHealthProgram4);
        }

        private void btnProgram1All_Click(object sender, EventArgs e)
        {
            if(_SaupID < 0)
            {
                Biz.Show(this, "사업장을 선택해 주십시오");
                return; 
            }
            DisplayGubun = "1";
            RefreshData();
        }

        private void btnProgram1Year_Click(object sender, EventArgs e)
        {
            if (_SaupID < 0)
            {
                Biz.Show(this, "사업장을 선택해 주십시오");
                return;
            }
            DisplayGubun = "2";
            RefreshData();
        }

        async private void btnHealthProgram1Save_Click(object sender, EventArgs e)
        {
            if (_SaupID < 0)
            {
                Biz.Show(this, "사업장을 선택해 주십시오");
                return;
            }

            DialogResult res = Biz.Show(this, "추진내용은 별도로 저장해야 합니다. 프로그램 저장을 계속 하시겠습니까?", "알림", MessageBoxButtons.OKCancel);

            if (res != DialogResult.OK)
                return;

            string program = "", sdate = "", edate = "", kikwanName, kikwanIdxs, seqnos, subject, year;
            int r = -1, odx = -1, inwon = 0;

            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();

            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                SqlConnection con = Biz.Instance.Connection;
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlTransaction tran = con.BeginTransaction();
                grdHealthProgram1.Rows.AsParallel().ForEach(row => {
                    year = row.Cells["clmYear"].Value == null || row.Cells["clmYear"].Value.ToString().Trim() == "" ? _VisitDate.Substring(0, 4) : row.Cells["clmYear"].Value.ToString();
                    program = row.Cells["clmProgram"].Value?.ToString() ?? "";
                    sdate = row.Cells["clmSDate"].Value?.ToString() ?? "";
                    edate = row.Cells["clmEDate"].Value?.ToString() ?? "";
                    odx = row.Cells["clmOdx"].Value == null ? -1 : (row.Cells["clmOdx"].Value.ToString() == "" ? -1 : Convert.ToInt32(row.Cells["clmOdx"].Value));
                    kikwanName = row.Cells["clmKikwanName"].Value?.ToString() ?? "";
                    kikwanIdxs = row.Cells["clmKikwanIdxs"].Value?.ToString() ?? "";
                    seqnos = row.Cells["clmSeqNOs"].Value?.ToString() ?? "";
                    subject = row.Cells["clmSubject"].Value?.ToString() ?? "";
                    inwon = row.Cells["clmInwon"].Value == null ? 0 : (row.Cells["clmInwon"].Value.ToString() == "" ? 0 : Convert.ToInt32(row.Cells["clmInwon"].Value));

                    r = Biz.Instance.SangdamHealth1Add(con, tran, _SaupID, year, "", program, sdate, edate, odx, kikwanName, kikwanIdxs, seqnos, inwon, subject);

                    if (r < 0)
                    {
                        tran.Rollback();
                        con.Close();
                        //Biz.Show("저장에 실패했습니다.", "오류");
                        return;
                    }
                });

                DataTable dsItem = Biz.Instance.SangdamHealthList(_SaupID, "", "", DisplayGubun);

                args.Result = dsItem;
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
            };
            worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    grdHealthProgram3.DataSource = null;
                    grdHealthProgram1.DataSource = (DataTable)args.Result;
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
    }
}
