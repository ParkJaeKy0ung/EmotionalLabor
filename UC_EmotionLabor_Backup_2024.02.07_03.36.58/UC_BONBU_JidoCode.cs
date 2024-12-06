using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;
using Telerik.WinControls.VirtualKeyboard;

namespace NBOGUN
{
    public partial class UC_BONBU_JidoCode : UserControl
    {
        RadWaitingBar _WaitingBar;
        AbortableBackgroundWorker _Worker;
        DataTable _TableCode;
        void OrderbyData()
        {
            for (int i = 0; i < grdCode.Rows.Count; i++)
            {
                grdCode.Rows[i].Cells["clmOdx"].Value = i + 1;
            }
        }

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
                DataSet dt = Biz.Instance.BONBU_KikawnPyeonggaCode1List();

                args.Result = dt;
            };
            _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {

            };
            _Worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataSet)
                {
                    DataSet dt = (DataSet)args.Result;

                    if (dt != null)
                    {
                        _TableCode = dt.Tables[0];
                        grdCode.DataSource = _TableCode;
                        grdJidoCode.DataSource = dt.Tables[1];
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

        public void DeleteData(int Idx)
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

                int r = Biz.Instance.BONBU_KikawnPyeonggaCode1Del(con, tran, Idx);

                if (r < 0)
                {
                    tran.Rollback();
                    con.Close();
                    return;
                }

                tran.Commit();
                con.Close();

                DataSet dt = Biz.Instance.BONBU_KikawnPyeonggaCode1List("2");

                args.Result = dt.Tables[0];
            };
            _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {

            };
            _Worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    DataTable dt = (DataTable)args.Result;

                    if (dt != null)
                    {
                        grdCode.DataSource = dt;
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
        public RadWaitingBar SetWaitingBar { get => _WaitingBar; set => _WaitingBar = value; }

        public UC_BONBU_JidoCode()
        {
            InitializeComponent();

            Biz.Instance.SetGridViewOption(grdCode);

            Biz.Instance.SetGridViewOption(grdJidoCode);

            grdCode.Columns["clmOdx"].SortOrder = RadSortOrder.Ascending;

            this.grdJidoCode.MasterTemplate.SortDescriptors.Clear();
            SortDescriptor devisit = new SortDescriptor();
            devisit.PropertyName = "clmIsSelect";
            devisit.Direction = ListSortDirection.Descending;
            this.grdJidoCode.MasterTemplate.SortDescriptors.Add(devisit);

            SortDescriptor devisit1 = new SortDescriptor();
            devisit1.PropertyName = "clmCode";
            devisit1.Direction = ListSortDirection.Ascending;
            this.grdJidoCode.MasterTemplate.SortDescriptors.Add(devisit1);
        }

        private void UC_BONBU_JidoCode_Load(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (_Worker != null && _Worker.IsBusy || (_WaitingBar.IsWaiting))
            {
                Biz.Show(this, "현재 진행중인 작업 있어 잠시 후 다시 시도해 주십시오.");
                return;
            }

            RefreshData();
        }

        private void grdCode_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewTableHeaderRowInfo)
                return;

            if (e.Column.Name == "clmDel")
            {
                int i = 0;

                try
                {
                    i = Convert.ToInt32(e.Row.Cells["clmIdx"].Value);
                }
                catch
                {
                    Biz.Show("올바른 인덱스가 아닙니다.");
                    return;
                }

                if (_Worker != null && _Worker.IsBusy || (_WaitingBar.IsWaiting))
                {
                    Biz.Show(this, "현재 진행중인 작업 있어 잠시 후 다시 시도해 주십시오.");
                    return;
                }

                DeleteData(i);

                return;
            }
            else if (e.Column.Name == "clmIdx")
            {

                int idx = Convert.ToInt32(e.Row.Cells["clmIdx"].Value);

                _Worker = new AbortableBackgroundWorker();
                if (_WaitingBar != null)
                {
                    _WaitingBar.Text = "";
                    _WaitingBar.StartWaiting();
                }
                _Worker.WorkerReportsProgress = true;
                _Worker.WorkerSupportsCancellation = true;

                _Worker.DoWork += delegate (object workersender, DoWorkEventArgs workere)
                {
                    DataTable dt = Biz.Instance.BONBU_KikawnPyeonggaCode2List(idx);

                    if (dt != null)
                    {
                        _Worker.ReportProgress(0, dt);
                    }
                    else
                        _Worker.CancelAsync();
                };

                _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
                {
                    if (args.ProgressPercentage < 0)
                    {
                        return;
                    }
                    else
                    {
                        DataTable ds = args.UserState as DataTable;

                        if (ds != null)
                        {
                            //grdSangtaeCodeJido.Rows.AsParallel().ForAll(row => {
                            //    ds.Rows.AsQueryable().
                            //});
                            foreach (GridViewDataRowInfo row in grdJidoCode.Rows)
                            {
                                string codejido = row.Cells["clmCode"].Value.ToString(); // 'Code' 컬럼의 값을 가져옴
                                bool isChecked = false; // 체크박스의 체크 여부

                                // DataTable에서 'Code' 컬럼 값과 일치하는 행을 찾음
                                DataRow[] foundRows = ds.Select("Code = '" + codejido + "'");
                                if (foundRows.Length > 0)
                                {
                                    // 'Code' 컬럼 값과 일치하는 행이 있다면 체크박스를 체크함
                                    isChecked = true;
                                }

                                // 체크박스의 상태를 업데이트
                                row.Cells["clmIsSelect"].Value = isChecked;
                            }
                        }
                    }
                };

                _Worker.RunWorkerCompleted += delegate (object workersender, RunWorkerCompletedEventArgs workere)
                {
                    _WaitingBar?.StopWaiting();
                    if (workere.Cancelled)
                    {
                        return;
                    }
                };

                BeginInvoke(new MethodInvoker(_Worker.RunWorkerAsync));
            }
        }

        private void btnCodeAdd_Click(object sender, EventArgs e)
        {
            if (grdCode.DataSource != null)
            {
                int targetOdx = Convert.ToInt32(this._TableCode.Compute("max([Odx])", string.Empty));

                SqlConnection con = Biz.Instance.Connection;
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlTransaction tran = con.BeginTransaction();

                int r = Biz.Instance.BONBU_KikawnPyeonggaCode1Save(con, tran, -1, "", targetOdx);

                if (r < 0)
                {
                    tran.Rollback();
                    con.Close();
                    return;
                }

                tran.Commit();
                con.Close();

                DataSet dt = Biz.Instance.BONBU_KikawnPyeonggaCode1List("2");

                this._TableCode = dt.Tables[0];
                grdCode.DataSource = this._TableCode;

                grdCode.Rows[grdCode.Rows.Count - 1].IsCurrent = true;
                grdCode.Rows[grdCode.Rows.Count - 1].EnsureVisible();

                OrderbyData();
            }
        }

        private void grdCode_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            OrderbyData();
        }

        private void grdCode_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            if (e.Row.Cells["clmIdx"].Value != null)
            {
                SqlConnection con = Biz.Instance.Connection;
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlTransaction tran = con.BeginTransaction();
                int idx = Convert.ToInt32(e.Row.Cells["clmIdx"].Value);
                int r = Biz.Instance.BONBU_KikawnPyeonggaCode1Save(con, tran, Convert.ToInt32(e.Row.Cells["clmIdx"].Value), e.Row.Cells["clmName"].Value.ToString());

                if (r < 0)
                {
                    tran.Rollback();
                    con.Close();
                    return;
                }

                tran.Commit();
                con.Close();

                DataSet dt = Biz.Instance.BONBU_KikawnPyeonggaCode1List("2");

                grdCode.DataSource = dt.Tables[0];

                for (int i = 0; i < grdCode.Rows.Count; i++)
                {
                    if (grdCode.Rows[i].Cells["clmIdx"].Value.ToString() == idx.ToString())
                    {
                        grdCode.Rows[i].IsCurrent = true;
                        grdCode.Rows[i].EnsureVisible();
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (grdCode.CurrentRow == null)
            {
                Biz.Show(this, "좌측의 코드를 선택해 주십시오.", "알림");
                return;
            }

            int idx = Convert.ToInt32(grdCode.CurrentRow.Cells["clmIdx"].Value);

            _Worker = new AbortableBackgroundWorker();

            if (_WaitingBar != null)
            {
                _WaitingBar.Text = "";
                _WaitingBar.StartWaiting();
            }

            _Worker.WorkerReportsProgress = true;
            _Worker.WorkerSupportsCancellation = true;

            _Worker.DoWork += delegate (object workersender, DoWorkEventArgs args)
            {
                SqlConnection con = Biz.Instance.Connection;
                if (con.State != ConnectionState.Open)
                    con.Open();

                SqlTransaction tran = con.BeginTransaction();
                _Worker.ReportProgress(-1, "초기화중");
                int r = Biz.Instance.BONBU_KikawnPyeonggaCode2Del(con, tran, idx);

                if (r < 0)
                {
                    tran.Rollback();
                    con.Close();
                    return;
                }

                var rows = from row in grdJidoCode.Rows
                           where row.Cells["clmIsSelect"].Value != null && Convert.ToBoolean(row.Cells["clmIsSelect"].Value) == true
                           group row by row.Cells["clmCode"].Value into grouped
                           select new
                           {
                               jidocode = grouped.Key
                               //, Rows = grouped.ToList()
                           };

                string jidocode = "";
                bool isSuccess = true;
                rows.ToList().ForEach(row =>
                {
                    jidocode = row.jidocode.ToString();
                    //jidocode = row.Cells["clmCode"].Value.ToString();
                    _Worker.ReportProgress(-1, jidocode + " 저장중");
                    r = Biz.Instance.BONBU_KikawnPyeonggaCode2Save(con, tran, idx, jidocode);
                    if (r < 0)
                    {
                        isSuccess = false;
                    }
                });

                if (isSuccess == false)
                {
                    tran.Rollback();
                    con.Close();
                }
                else
                {
                    tran.Commit();
                    con.Close();

                    args.Result = "저장에 성공했습니다.";
                }
            };

            _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                if (args.ProgressPercentage < 0)
                {
                    _WaitingBar.Text = args.UserState.ToString();
                    return;
                }
            };

            _Worker.RunWorkerCompleted += delegate (object workersender, RunWorkerCompletedEventArgs workere)
            {
                _WaitingBar.StopWaiting();
                if (workere.Result != null && workere.Result.ToString() != "")
                {
                    Biz.Show(this, workere.Result.ToString(), "알림");
                }
                else
                {
                    Biz.Show(this, "저장에 실패했습니다.", "알림");
                }
            };

            BeginInvoke(new MethodInvoker(_Worker.RunWorkerAsync));
        }

        private void grdJidoCode_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            var editor = e.ActiveEditor;

            if (editor is RadCheckBoxEditor)
            {
                editor.ValueChanged += delegate (object obj, EventArgs args) {
                    grdJidoCode.EndEdit();
                };
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            #region Up
            if (grdCode.SelectedCells == null)
                return;
            else if (grdCode.CurrentRow != null)
            {
                int odx;

                int.TryParse(grdCode.CurrentRow.Cells["clmOdx"].Value.ToString(), out odx);

                DataRow[] rows = this._TableCode.Select("Odx < " + odx);

                if (rows.Length > 0)
                {
                    int targetOdx = Convert.ToInt32(rows.CopyToDataTable().Compute("max([Odx])", string.Empty));

                    this._TableCode.Select("Odx = " + targetOdx)[0]["Odx"] = -9999;

                    this._TableCode.Select("Odx = " + odx)[0]["Odx"] = targetOdx;
                    this._TableCode.Select("Odx = -9999")[0]["Odx"] = odx;

                    _TableCode.DefaultView.Sort = "Odx ASC";
                    grdCode.DataSource = _TableCode;
                    for (int i = 0; i < grdCode.Rows.Count; i++)
                    {
                        if (grdCode.Rows[i].Cells["clmOdx"].Value.ToString() == targetOdx.ToString())
                        {
                            grdCode.Rows[i].IsCurrent = true;
                            grdCode.Rows[i].EnsureVisible();
                        }
                    }
                }
            }
            else
            {
                List<int> selectedOdxs = new List<int>();//선택한 행
                List<int[]> targetOdxs = new List<int[]>();//이동하고 선택할 대상 행의 Odx들
                //int idx;
                int odx;
                int targetOdx;
                bool isExists = false;

                List<string> selectedColumName = new List<string>();

                //grdCode.SelectedCells.GetEnumerator().
                foreach (var k in grdCode.SelectedCells)
                {
                    if (selectedColumName.Contains(k.ColumnInfo.Name) == false)
                        selectedColumName.Add(k.ColumnInfo.Name);
                }

                //옮겨야할 일련번호 추출
                for (int i = grdCode.SelectedCells.Count - 1; i >= 0; i--)
                {
                    odx = Convert.ToInt32(grdCode.SelectedCells[i].RowInfo.Cells["clmOdx"].Value);

                    //targetOdx = Convert.ToInt32(rows.CopyToDataTable().Compute("min([Odx])", string.Empty));
                    if (selectedOdxs.Contains(odx) == false)
                        selectedOdxs.Add(odx);
                }
                //초기화
                selectedOdxs.Sort();

                //추출된 일련번호의 목표 일련번호 산출
                for (int i = 0; i < selectedOdxs.Count; i++)
                {
                    odx = Convert.ToInt32(selectedOdxs[i]);
                    targetOdx = odx - 1;
                    isExists = false;
                    foreach (var el in targetOdxs)
                    {
                        if (el[1] == targetOdx)
                        {
                            isExists = true;
                            break;
                        }
                    }

                    if (isExists == false)
                        targetOdxs.Add(new int[2] { odx, (targetOdx < 1 ? odx : targetOdx) });
                    else
                        targetOdxs.Add(new int[2] { odx, odx });
                }
                //일련번호 변경
                for (int i = 0; i < targetOdxs.Count; i++)
                {
                    if (targetOdxs[i][0] == targetOdxs[i][1])
                        continue;

                    odx = targetOdxs[i][0];

                    DataRow[] rows = this._TableCode.Select("Odx < " + odx);

                    if (rows.Length > 0)
                    {
                        //targetOdx = Convert.ToInt32(rows.CopyToDataTable().Compute("max([Odx])", string.Empty));
                        targetOdx = targetOdxs[i][1];
                        this._TableCode.Select("Odx = " + targetOdx)[0]["Odx"] = -9999;

                        this._TableCode.Select("Odx = " + odx)[0]["Odx"] = targetOdx;
                        this._TableCode.Select("Odx = -9999")[0]["Odx"] = odx;

                        _TableCode.DefaultView.Sort = "Odx ASC";
                    }
                }
                grdCode.ClearSelection();
                grdCode.CurrentRow = null;
                //셀 재선택
                for (int i = 0; i < grdCode.Rows.Count; i++)
                {
                    for (int j = 0; j < targetOdxs.Count; j++)
                    {
                        if (Convert.ToInt32(grdCode.Rows[i].Cells["clmOdx"].Value) == targetOdxs[j][1])
                        {
                            foreach (var el in grdCode.Columns)
                            {
                                if (selectedColumName.Contains(el.Name))
                                {
                                    grdCode.Rows[i].Cells[el.Name].IsSelected = true;
                                    grdCode.Rows[i].Cells[el.Name].EnsureVisible();
                                }

                            }
                        }
                    }
                }
            }

            OrderbyData();
            #endregion
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            #region Down
            if (grdCode.SelectedCells == null)
                return;
            else if (grdCode.CurrentRow != null)//한개 행만 선택할때
            {
                int odx = 0;

                int.TryParse(grdCode.CurrentRow.Cells["clmOdx"].Value.ToString(), out odx);

                DataRow[] rows = this._TableCode.Select("Odx > " + odx);

                if (rows.Length > 0)
                {
                    int targetOdx = Convert.ToInt32(rows.CopyToDataTable().Compute("min([Odx])", string.Empty));

                    this._TableCode.Select("Odx = " + targetOdx)[0]["Odx"] = -9999;

                    this._TableCode.Select("Odx = " + odx)[0]["Odx"] = targetOdx;
                    this._TableCode.Select("Odx = -9999")[0]["Odx"] = odx;

                    _TableCode.DefaultView.Sort = "Odx ASC";

                    for (int i = 0; i < grdCode.Rows.Count; i++)
                    {
                        if (grdCode.Rows[i].Cells["clmOdx"].Value.ToString() == targetOdx.ToString())
                        {
                            grdCode.Rows[i].IsCurrent = true;
                        }
                    }

                    _TableCode.AcceptChanges();
                }
            }
            else//여러개 선택할때
            {
                List<int> selectedOdxs = new List<int>();//선택한 행
                List<int[]> targetOdxs = new List<int[]>();//이동하고 선택할 대상 행의 Odx들
                int idx;
                int odx;
                int targetOdx;
                bool isExists = false;
                int maxOdx = Convert.ToInt32(_TableCode.Compute("max([Odx])", string.Empty));
                List<string> selectedColumName = new List<string>();

                //grdCode.SelectedCells.GetEnumerator().
                foreach (var k in grdCode.SelectedCells)
                {
                    if (selectedColumName.Contains(k.ColumnInfo.Name) == false)
                        selectedColumName.Add(k.ColumnInfo.Name);
                }

                //옮겨야할 일련번호 추출
                for (int i = grdCode.SelectedCells.Count - 1; i >= 0; i--)
                {
                    odx = Convert.ToInt32(grdCode.SelectedCells[i].RowInfo.Cells["clmOdx"].Value);

                    //targetOdx = Convert.ToInt32(rows.CopyToDataTable().Compute("min([Odx])", string.Empty));
                    if (selectedOdxs.Contains(odx) == false)
                        selectedOdxs.Add(odx);
                }
                //초기화
                selectedOdxs.Sort();
                selectedOdxs.Reverse();

                //추출된 일련번호의 목표 일련번호 산출
                for (int i = 0; i < selectedOdxs.Count; i++)
                {
                    odx = Convert.ToInt32(selectedOdxs[i]);
                    targetOdx = odx + 1;
                    isExists = false;
                    foreach (var el in targetOdxs)
                    {
                        if (el[1] == targetOdx)
                        {
                            isExists = true;
                            break;
                        }
                    }

                    if (isExists == false)
                        targetOdxs.Add(new int[2] { odx, (targetOdx > maxOdx ? odx : targetOdx) });
                    else
                        targetOdxs.Add(new int[2] { odx, odx });
                }
                //일련번호 변경
                for (int i = 0; i < targetOdxs.Count; i++)
                {
                    if (targetOdxs[i][0] == targetOdxs[i][1])
                        continue;

                    odx = targetOdxs[i][0];

                    DataRow[] rows = this._TableCode.Select("Odx > " + odx);

                    if (rows.Length > 0)
                    {
                        //targetOdx = Convert.ToInt32(rows.CopyToDataTable().Compute("max([Odx])", string.Empty));
                        targetOdx = targetOdxs[i][1];
                        this._TableCode.Select("Odx = " + targetOdx)[0]["Odx"] = -9999;
                        this._TableCode.Select("Odx = " + odx)[0]["Odx"] = targetOdx;
                        this._TableCode.Select("Odx = -9999")[0]["Odx"] = odx;

                        _TableCode.DefaultView.Sort = "Odx ASC";
                    }
                }
                grdCode.ClearSelection();
                grdCode.CurrentRow = null;
                //셀 재선택
                for (int i = 0; i < grdCode.Rows.Count; i++)
                {
                    for (int j = 0; j < targetOdxs.Count; j++)
                    {
                        if (Convert.ToInt32(grdCode.Rows[i].Cells["clmOdx"].Value) == targetOdxs[j][1])
                        {
                            foreach (var el in grdCode.Columns)
                            {
                                if (selectedColumName.Contains(el.Name))
                                {
                                    grdCode.Rows[i].Cells[el.Name].IsSelected = true;
                                    grdCode.Rows[i].Cells[el.Name].EnsureVisible();
                                }
                            }
                        }
                    }
                }
            }

            OrderbyData();
            #endregion
        }

        private void btnOdxSave_Click(object sender, EventArgs e)
        {
            if (_Worker != null && _Worker.IsBusy || (_WaitingBar.IsWaiting))
            {
                Biz.Show(this, "현재 진행중인 작업 있어 잠시 후 다시 시도해 주십시오.");
                return;
            }

            int idx = Convert.ToInt32(grdCode.CurrentRow.Cells["clmIdx"].Value);

            _Worker = new AbortableBackgroundWorker();

            if (_WaitingBar != null)
            {
                _WaitingBar.Text = "";
                _WaitingBar.StartWaiting();
            }

            _Worker.WorkerReportsProgress = true;
            _Worker.WorkerSupportsCancellation = true;

            _Worker.DoWork += delegate (object workersender, DoWorkEventArgs args)
            {
                SqlConnection con = Biz.Instance.Connection;
                if (con.State != ConnectionState.Open)
                    con.Open();

                SqlTransaction tran = con.BeginTransaction();
                _Worker.ReportProgress(-1, "초기화중");
                int r = -1;

                var rows = from row in grdCode.Rows
                           select row;
                string name = "";

                bool isSuccess = true;

                int odx = 0;

                rows.ToList().ForEach(row =>
                {
                    idx = Convert.ToInt32(row.Cells["clmIdx"].Value);
                    odx = Convert.ToInt32(row.Cells["clmOdx"].Value);

                    name = row.Cells["clmName"].Value.ToString();

                    _Worker.ReportProgress(-1, name + " 저장중");

                    r = Biz.Instance.BONBU_KikawnPyeonggaCode1Save(con, tran, idx, name, odx);

                    if (r < 0)
                    {
                        isSuccess = false;
                    }
                });

                if (isSuccess == false)
                {
                    tran.Rollback();
                    con.Close();
                }
                else
                {
                    tran.Commit();
                    con.Close();

                    args.Result = "저장에 성공했습니다.";
                }
            };

            _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                if (args.ProgressPercentage < 0)
                {
                    _WaitingBar.Text = args.UserState.ToString();
                    return;
                }
            };

            _Worker.RunWorkerCompleted += delegate (object workersender, RunWorkerCompletedEventArgs workere)
            {
                _WaitingBar.StopWaiting();
                if (workere.Result != null && workere.Result.ToString() != "")
                {
                    Biz.Show(this, workere.Result.ToString(), "알림");
                }
                else
                {
                    Biz.Show(this, "저장에 실패했습니다.", "알림");
                }
            };

            BeginInvoke(new MethodInvoker(_Worker.RunWorkerAsync));
        }
    }
}
