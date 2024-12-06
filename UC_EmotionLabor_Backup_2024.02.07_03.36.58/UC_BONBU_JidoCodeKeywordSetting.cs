using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Telerik.Windows.Diagrams.Core;

namespace NBOGUN
{
    public partial class UC_BONBU_JidoCodeKeywordSetting : UserControl
    {
        RadWaitingBar _WaitingBar;
        AbortableBackgroundWorker _Worker;
        DataTable _TableCode;

        public RadWaitingBar WaitingBar { get => _WaitingBar; set => _WaitingBar = value; }
        public AbortableBackgroundWorker Worker { get => _Worker; set => _Worker = value; }
        public DataTable TableCode { get => _TableCode; set => _TableCode = value; }

        enum _Process_DPI_Awareness
        {
            Process_DPI_Unaware = 0,
            Process_System_DPI_Aware = 1,
            Process_Per_Monitor_DPI_Aware = 2
        }

        [DllImport("SHCore.dll")]
        static extern int SetProcessDpiAwareness(_Process_DPI_Awareness value);
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            #region 단축키
            if (keyData == (Keys.Alt | Keys.E))
            {
                //현재 포커스된 그리드 찾기
                //RadGridView grd = this.ActiveControl as RadGridView;
                //if (grd != null)
                //{
                //    SetProcessDpiAwareness(_Process_DPI_Awareness.Process_DPI_Unaware);

                //    Random rnd = new Random(DateTime.Now.Millisecond);
                //    string na = rnd.Next(100000, 999999).ToString();

                //    string exportFile = @"..\..\" + na + ".xlsx";
                //    #region 기존
                //    ////그리드의 엑셀 변환
                //    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                //    {
                //        Telerik.WinControls.Export.GridViewSpreadExport exporter = new Telerik.WinControls.Export.GridViewSpreadExport(grd);
                //        //exporter.CellFormatting += spreadExporter_CellFormatting;
                //        Telerik.WinControls.Export.SpreadExportRenderer renderer = new Telerik.WinControls.Export.SpreadExportRenderer();
                //        exporter.ExportChildRowsGrouped = true;
                //        exporter.ExportHierarchy = true;
                //        exporter.ExportVisualSettings = true;
                //        exporter.FreezePinnedColumns = true;
                //        exporter.ExportGroupedColumns = true;
                //        exporter.RunExport(ms, renderer);

                //        using (System.IO.FileStream fileStream = new System.IO.FileStream(exportFile, FileMode.Create, FileAccess.Write))
                //        {
                //            ms.WriteTo(fileStream);
                //        }
                //    }

                //    Process process = new Process();

                //    process.StartInfo.FileName = exportFile;

                //    process.Start();
                //    #endregion
                //}
            }
            else if (keyData == (Keys.Alt | Keys.D))
            {
                // Alt+F pressed
                //frmDebug f = new frmDebug();
                //f.StartPosition = FormStartPosition.CenterScreen;
                //f.Show();

            }
            #endregion

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void RefreshData()
        {
            if (_WaitingBar != null)
            {
                _WaitingBar.Text = "";
                _WaitingBar.StartWaiting();
            }

            _Worker = new AbortableBackgroundWorker();
            _Worker.WorkerSupportsCancellation = true;
            _Worker.WorkerReportsProgress = true;

            _Worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                DataTable dt = Biz.Instance.BONBU_CODE_JidoKeywordList();

                args.Result = dt;
            };
            _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {

            };
            _Worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataSet)
                {
                    DataTable dt = (DataTable)args.Result;

                    if (dt != null)
                    {
                        _TableCode = dt;
                        grdCode.DataSource = _TableCode;
                    }
                    else
                    {
                        Biz.Show(this, "데이터를 불러오는데 실패했습니다.");
                    }
                }
                else
                {
                    Biz.Show(this, "데이터를 불러오는데 실패했습니다.");
                }

                _WaitingBar?.StopWaiting();
            };

            BeginInvoke(new MethodInvoker(_Worker.RunWorkerAsync));
        }

        public void SaveData()
        {
            if (_WaitingBar != null)
            {
                _WaitingBar.Text = "";
                _WaitingBar.StartWaiting();
            }

            _Worker = new AbortableBackgroundWorker();
            _Worker.WorkerSupportsCancellation = true;
            _Worker.WorkerReportsProgress = true;

            _Worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                SqlConnection con = Biz.Instance.Connection;
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlTransaction tran = con.BeginTransaction();

                int r;

                bool isSuccessed = true;
                string keyword, code;
                grdCode.Rows.AsParallel().ForEach(row => {
                    code = row.Cells["clmCode"].Value?.ToString() ?? "";
                    keyword = row.Cells["clmKeyword"].Value?.ToString() ?? "";
                    if(isSuccessed)
                    {
                        r = Biz.Instance.BONBU_CODE_JidoKeywordSave(con, tran, code, keyword);
                        if (r < 0)
                        {
                            isSuccessed = false;
                            tran.Rollback();
                            con.Close();
                        }
                    }
                });

                if(isSuccessed)
                {
                    tran.Commit();
                    con.Close();

                    DataTable dt = Biz.Instance.BONBU_CODE_JidoKeywordList();

                    args.Result = dt;
                }
            };
            _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {

            };
            _Worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataSet)
                {
                    DataTable dt = (DataTable)args.Result;

                    if (dt != null)
                    {
                        _TableCode = dt;
                        grdCode.DataSource = _TableCode;
                    }
                    else
                    {
                        Biz.Show(this, "데이터를 불러오는데 실패했습니다.");
                    }
                }
                else
                {
                    Biz.Show(this, "데이터를 불러오는데 실패했습니다.");
                }

                _WaitingBar?.StopWaiting();
            };

            BeginInvoke(new MethodInvoker(_Worker.RunWorkerAsync));
        }

        public UC_BONBU_JidoCodeKeywordSetting()
        {
            InitializeComponent();

            Biz.Instance.SetGridViewOption(grdCode);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }
    }
}
