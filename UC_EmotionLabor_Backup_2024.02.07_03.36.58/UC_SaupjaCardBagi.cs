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
    public partial class UC_SaupjaCardBagi : UserControl
    {
        enum Gubun { Year, Visit };

        Gubun _CurrentGubun = Gubun.Year;

        AbortableBackgroundWorker _Worker;
        int _SaupID;
        string _VisitDate = "";
        string _Visitor = "";
        string _Year = "";
        DataTable mTableEduItem;
        public string VisitDate
        {
            set
            {
                _VisitDate = value;
                grdBagi.Columns["clmVisitorName"].IsVisible = true;
                if(_CurrentGubun != Gubun.Visit)
                {
                    _CurrentGubun = Gubun.Visit;
                    SetColumn();
                }
                
            }
            get { return VisitDate; }
        }

        public string Visitor
        {
            set { 
                _Visitor = value; grdBagi.Columns["clmVisitor"].IsVisible = false; _CurrentGubun = Gubun.Visit; SetColumn(); 
            }
            get { return _Visitor; }
        }

        public string Year
        {
            set
            {
                _Year = value;
                grdBagi.Columns["clmVisitorName"].IsVisible = false;
                grdBagi.Columns["clmVisitor"].IsVisible = false;
                if (_CurrentGubun != Gubun.Year)
                {
                    _CurrentGubun = Gubun.Year; SetColumn();
                }
                
            }
            get { return _Year; }
        }

        string _Saupjanum;
        public string Saupjanum
        {
            set => _Saupjanum = value;
        }

        public int SaupID
        {
            get { return _SaupID; }
            set { _SaupID = value; }
        }

        void SetColumn()
        {
            grdBagi.BeginUpdate();

            switch (_CurrentGubun)
            {
                case Gubun.Visit:
                    grdBagi.Columns["clmSeolbiName"].FieldName = "기계기구명";
                    grdBagi.Columns["clmDate1"].IsVisible = false;//clmYear1
                    grdBagi.Columns["clmYear1"].IsVisible = false;//
                    grdBagi.Columns["clmDate2"].IsVisible = false;//clmYear1
                    grdBagi.Columns["clmYear2"].IsVisible = false;//
                    grdBagi.Columns["clmDate3"].IsVisible = false;//clmYear1
                    grdBagi.Columns["clmYear3"].IsVisible = false;//
                    grdBagi.Columns["clmDate4"].IsVisible = false;//clmYear1
                    grdBagi.Columns["clmYear4"].IsVisible = false;//
                    grdBagi.Columns["clmGumsaDate"].IsVisible = true;//clmGumsaDate//clmIsDaesang
                    grdBagi.Columns["clmIsDaesang"].IsVisible = true;//clmGumsaDate//clmIsExcept
                    grdBagi.Columns["clmIsExcept"].IsVisible = true;//clmGumsaDate//clmIsExcept
                    grdBagi.Columns["clmGumsaKikwan"].IsVisible = true;//clmGumsaKikwan
                    grdBagi.Columns["clmGumsaKikwan"].FieldName = "검사기관";
                    grdBagi.Columns["clmGJName"].IsVisible = true;//clmGumsaKikwan
                    grdBagi.Columns["clmSiteName"].IsVisible = true;//clmGumsaKikwan
                    grdBagi.Columns["clmCnt"].IsVisible = false;//clmGumsaKikwan
                    grdBagi.Columns["clmGumsaJugi"].IsVisible = false;//clmGumsaKikwan
                    break;
                case Gubun.Year:
                    grdBagi.Columns["clmSeolbiName"].FieldName = "기계기구명";
                    grdBagi.Columns["clmDate1"].IsVisible = true;//clmYear1
                    grdBagi.Columns["clmYear1"].IsVisible = true;//
                    grdBagi.Columns["clmDate2"].IsVisible = true;//clmYear1
                    grdBagi.Columns["clmYear2"].IsVisible = true;//
                    grdBagi.Columns["clmDate3"].IsVisible = true;//clmYear1
                    grdBagi.Columns["clmYear3"].IsVisible = true;//
                    grdBagi.Columns["clmDate4"].IsVisible = true;//clmYear1
                    grdBagi.Columns["clmYear4"].IsVisible = true;//clmGumsaDate
                    grdBagi.Columns["clmGumsaDate"].IsVisible = false;//clmGumsaDate
                    grdBagi.Columns["clmIsDaesang"].IsVisible = false;//clmGumsaDate//clmIsDaesang
                    grdBagi.Columns["clmIsExcept"].IsVisible = false;//clmGumsaDate//clmIsExcept
                    grdBagi.Columns["clmGumsaKikwan"].IsVisible = true;//clmGumsaKikwan
                    grdBagi.Columns["clmGumsaKikwan"].FieldName = "검사기관";//clmGumsaKikwan
                    grdBagi.Columns["clmGJName"].IsVisible = false;//clmGumsaKikwan
                    grdBagi.Columns["clmSiteName"].IsVisible = false;//clmGumsaKikwan
                    grdBagi.Columns["clmCnt"].IsVisible = true;//clmGumsaKikwan
                    grdBagi.Columns["clmCnt"].FieldName = "대수";
                    grdBagi.Columns["clmGumsaJugi"].IsVisible = true;//clmGumsaKikwan
                    grdBagi.Columns["clmGumsaJugi"].FieldName = "검사주기";//clmGumsaKikwan
                    break;
            }

            grdBagi.EndUpdate();
        }

        async public void RefreshData()
        {
            if ((_CurrentGubun == Gubun.Year && _Year == "") || (_CurrentGubun == Gubun.Visit && _VisitDate == ""))
            {
                return;
            }

            //RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            //waitingBar.AssociatedControl = this;
            AbortableBackgroundWorker _Worker = new AbortableBackgroundWorker();

            _Worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                //DataTable dt = null;

                if (_CurrentGubun == Gubun.Year)//사업장관리카드 새로고침 했을때
                {
                    mTableEduItem = Biz.Instance.RPT_SaupjaCardZ18(_SaupID, _Year);
                }
                else if (_CurrentGubun == Gubun.Visit)//방문일 새로고침 했을때
                {
                    mTableEduItem = Biz.Instance.RPT_SaupjaCardBagiList(_SaupID, _VisitDate);
                }

                args.Result = mTableEduItem;
            };
            _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
            };
            _Worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    mTableEduItem = (DataTable)args.Result;

                    if (mTableEduItem.Columns.Contains("Odx") == false)
                        mTableEduItem.Columns.Add("Odx", typeof(int));

                    grdBagi.DataSource = mTableEduItem;
                }
                else
                {
                    Biz.Show(this, "데이터를 불러올 수 없습니다.");
                }

                
            };

            await Task.Run(() =>
            {
                System.Threading.Thread.Sleep(Biz._ThreadTime);

                // UI 작업을 UI 스레드로 이동
                BeginInvoke(new MethodInvoker(() =>
                {
                    _Worker.RunWorkerAsync();
                }));
            });
            //await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
            //BeginInvoke(new MethodInvoker(_Worker.RunWorkerAsync));
        }

        //저장
        async public void SaveData()
        {
            if ((_CurrentGubun == Gubun.Year && _Year == "") || (_CurrentGubun == Gubun.Visit && _VisitDate == ""))
            {
                return;
            }

            AbortableBackgroundWorker _Worker = new AbortableBackgroundWorker();
            int r = -1;

            _Worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                SqlConnection con = Biz.Instance.Connection;
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlTransaction tran = con.BeginTransaction();
                int resultCnt = 0;
                string seolbiname = "";
                string gumsajugi = "";
                string gumsakikawn = "";
                string year1 = "";
                string date1 = "";
                string year2 = "";
                string date2 = "";
                string year3 = "";
                string date3 = "";
                string year4 = "";
                string date4 = "";

                int cnt = 0;
                switch (this._CurrentGubun)
                {
                    case Gubun.Year:
                        r = Biz.Instance.RPT_SaupjaCardBagiDel(con, tran, _SaupID, _Year, "");

                        resultCnt = 0;



                        grdBagi.Rows.ForEach(row =>
                        {
                            seolbiname = row.Cells["clmSeolbiName"].Value.ToString() ?? "";

                            cnt = int.TryParse((row.Cells["clmCnt"].Value?.ToString() ?? "0"), out cnt) == true ? cnt : 0;

                            gumsajugi = row.Cells["clmGumsaJugi"].Value?.ToString() ?? "";
                            gumsakikawn = row.Cells["clmGumsaKikwan"].Value?.ToString() ?? "";
                            year1 = row.Cells["clmYear1"].Value?.ToString() ?? "";
                            date1 = row.Cells["clmDate1"].Value?.ToString() ?? "";
                            year2 = row.Cells["clmYear2"].Value?.ToString() ?? "";
                            date2 = row.Cells["clmDate2"].Value?.ToString() ?? "";
                            year3 = row.Cells["clmYear3"].Value?.ToString() ?? "";
                            date3 = row.Cells["clmDate3"].Value?.ToString() ?? "";
                            year4 = row.Cells["clmYear4"].Value?.ToString() ?? "";
                            date4 = row.Cells["clmDate4"].Value?.ToString() ?? "";

                            r = Biz.Instance.RPT_SaupjaCardBagiSave(con, tran, _SaupID, _Year, seolbiname, cnt, gumsajugi, gumsakikawn, year1, date1, year2, date2, year3, date3, year4, date4);

                            if (r < 0)
                            {
                                tran.Rollback();
                                con.Close();
                                return;
                            }
                            else
                            {
                                resultCnt++;
                                _Worker.ReportProgress(-1, resultCnt);
                            }
                        });

                        tran.Commit();
                        con.Close();
                        //저장후 새로고침
                        mTableEduItem = Biz.Instance.RPT_SaupjaCardZ18(_SaupID, _Year);
                        args.Result = mTableEduItem;

                        break;
                    case Gubun.Visit:
                        //기존 데이터 초기화
                        r = Biz.Instance.VisitJakupEnvSelbiDel(con, tran, _SaupID, _VisitDate, _Visitor, -1, -1);
                        if (r < 0)
                        {
                            tran.Rollback();
                            con.Close();
                            return;
                        }

                        string visitor = "";
                        int _siteno = -1;
                        int _gjno = -1;
                        int _seqno = -1;
                        seolbiname = "";
                        string isdaesang = "";
                        string isexcept = "";
                        string gumsakikwan = "";
                        string gumsadate = "";

                        resultCnt = 0;
                        grdBagi.Rows.ForEach(row =>
                        {
                            visitor = row.Cells["clmVisitor"].Value?.ToString() ?? "";

                            if (visitor == "")
                            {
                                visitor = _Visitor;
                            }

                            _siteno = (int.TryParse((row.Cells["clmSiteNO"].Value?.ToString() ?? ""), out _siteno) == true ? _siteno : -1);
                            _gjno = (int.TryParse((row.Cells["clmGJNO"].Value?.ToString() ?? ""), out _gjno) == true ? _gjno : -1);
                            _seqno = (int.TryParse((row.Cells["clmSeqNO"].Value?.ToString() ?? ""), out _seqno) == true ? _seqno : -1);

                            seolbiname = row.Cells["clmSeolbiName"].Value?.ToString() ?? "";

                            if (row.Cells["clmIsDaesang"].Value != null && (row.Cells["clmIsDaesang"].Value.ToString().ToLower() == "true" || row.Cells["clmIsDaesang"].Value.ToString().ToLower() == "on" || row.Cells["clmIsDaesang"].Value.ToString().ToLower() == "1"))
                                isdaesang = "1";
                            else
                                isdaesang = "0";

                            if (row.Cells["clmIsExcept"].Value != null && (row.Cells["clmIsExcept"].Value.ToString().ToLower() == "true" || row.Cells["clmIsExcept"].Value.ToString().ToLower() == "on" || row.Cells["clmIsExcept"].Value.ToString().ToLower() == "1"))
                                isexcept = "1";
                            else
                                isexcept = "0";

                            gumsakikwan = row.Cells["clmGumsaKikwan"].Value?.ToString() ?? "";
                            gumsadate = row.Cells["clmGumsaDate"].Value?.ToString() ?? "";

                            r = Biz.Instance.VisitJakupEnvSelbiSave(con, tran, _SaupID, _VisitDate, visitor, _siteno, _gjno, _seqno, seolbiname, isdaesang, isexcept, gumsakikwan, gumsadate);

                            if (r < 0)
                            {
                                tran.Rollback();
                                con.Close();
                                return;
                            }
                            else
                            {
                                resultCnt++;
                                _Worker.ReportProgress(-1, resultCnt);
                            }
                        });

                        tran.Commit();
                        con.Close();
                        //저장후 새로고침
                        mTableEduItem = Biz.Instance.RPT_SaupjaCardBagiList(_SaupID, _VisitDate);
                        args.Result = mTableEduItem;
                        break;
                }
            };
            _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                //if (args.ProgressPercentage < 0)
                //{
                //    waitingBar.Text = (args.UserState?.ToString() ?? "") + "/" + grdBagi.RowCount.ToString();
                //}
            };
            _Worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    mTableEduItem = (DataTable)args.Result;

                    if (mTableEduItem.Columns.Contains("Odx") == false)
                        mTableEduItem.Columns.Add("Odx", typeof(int));

                    grdBagi.DataSource = mTableEduItem;

                    Biz.Show(this, "저장에 성공했습니다.");
                }
                else
                {
                    Biz.Show(this, "저장에 실패했습니다.");
                }

                
            };

            //if (this.InvokeRequired)
            //{
            //    this.Invoke((MethodInvoker)delegate { waitingBar.StartWaiting(); });
            //}
            //else
            //{
            //    waitingBar.StartWaiting();
            //}

            await Task.Run(() =>
            {
                System.Threading.Thread.Sleep(Biz._ThreadTime);

                // UI 작업을 UI 스레드로 이동
                BeginInvoke(new MethodInvoker(() =>
                {
                    _Worker.RunWorkerAsync();
                }));
            });
        }

        public UC_SaupjaCardBagi()
        {
            InitializeComponent();

            Biz.Instance.SetGridViewOption(grdBagi);

            Biz.Instance.SaupIDChanged += Instance_SaupIDChanged;
        }

        private void Instance_SaupIDChanged(object sender, EventArgs e)
        {
            _Year = "";
            _VisitDate = "";
        }

        ~UC_SaupjaCardBagi()
        {
            if (_Worker != null && _Worker.IsBusy)
            {
                _Worker.Abort();
                _Worker.Dispose();
            }
        }


        private void UC_SaupjaCardBagi_Load(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            grdBagi.SelectedCells.ForEach(cell =>
            {
                if (cell.ColumnInfo is GridViewTextBoxColumn && (cell.ColumnInfo.Name == "clmSeolbiName" || cell.ColumnInfo.Name != "clmGJName"))
                {
                    cell.Value = txtChange.Text.Trim();
                }
            });
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (grdBagi.SelectedCells == null)
                return;
            else if (grdBagi.SelectedCells.Count == 1)//한개 행만 선택할때
            {
                int odx = 0;

                int.TryParse(grdBagi.CurrentRow.Cells["clmOdx"].Value.ToString(), out odx);

                DataRow[] rows = this.mTableEduItem.Select("Odx > " + odx);

                if (rows.Length > 0)
                {
                    int targetOdx = Convert.ToInt32(rows.CopyToDataTable().Compute("min([Odx])", string.Empty));

                    this.mTableEduItem.Select("Odx = " + targetOdx)[0]["Odx"] = -9999;

                    this.mTableEduItem.Select("Odx = " + odx)[0]["Odx"] = targetOdx;
                    this.mTableEduItem.Select("Odx = -9999")[0]["Odx"] = odx;

                    mTableEduItem.DefaultView.Sort = "Odx ASC";

                    for (int i = 0; i < grdBagi.Rows.Count; i++)
                    {
                        if (grdBagi.Rows[i].Cells["clmOdx"].Value.ToString() == targetOdx.ToString())
                        {
                            grdBagi.Rows[i].IsCurrent = true;
                        }
                    }

                    mTableEduItem.AcceptChanges();
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
                int maxOdx = Convert.ToInt32(mTableEduItem.Compute("max([Odx])", string.Empty));
                List<string> selectedColumName = new List<string>();

                //grdCode.SelectedCells.GetEnumerator().
                foreach (var k in grdBagi.SelectedCells)
                {
                    if (selectedColumName.Contains(k.ColumnInfo.Name) == false)
                        selectedColumName.Add(k.ColumnInfo.Name);
                }

                //옮겨야할 일련번호 추출
                for (int i = grdBagi.SelectedCells.Count - 1; i >= 0; i--)
                {
                    odx = Convert.ToInt32(grdBagi.SelectedCells[i].RowInfo.Cells["clmOdx"].Value);

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

                    DataRow[] rows = this.mTableEduItem.Select("Odx > " + odx);

                    if (rows.Length > 0)
                    {
                        //targetOdx = Convert.ToInt32(rows.CopyToDataTable().Compute("max([Odx])", string.Empty));
                        targetOdx = targetOdxs[i][1];
                        this.mTableEduItem.Select("Odx = " + targetOdx)[0]["Odx"] = -9999;
                        this.mTableEduItem.Select("Odx = " + odx)[0]["Odx"] = targetOdx;
                        this.mTableEduItem.Select("Odx = -9999")[0]["Odx"] = odx;

                        mTableEduItem.DefaultView.Sort = "Odx ASC";
                    }
                }
                grdBagi.ClearSelection();
                grdBagi.CurrentRow = null;
                //셀 재선택
                for (int i = 0; i < grdBagi.Rows.Count; i++)
                {
                    for (int j = 0; j < targetOdxs.Count; j++)
                    {
                        if (Convert.ToInt32(grdBagi.Rows[i].Cells["clmOdx"].Value) == targetOdxs[j][1])
                        {
                            foreach (var el in grdBagi.Columns)
                            {
                                if (selectedColumName.Contains(el.Name))
                                {
                                    grdBagi.Rows[i].Cells[el.Name].IsSelected = true;
                                    grdBagi.Rows[i].Cells[el.Name].EnsureVisible();
                                }

                            }
                        }
                    }
                }
            }

            grdBagi_DataBindingComplete(null, null);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (grdBagi.SelectedCells == null)
                return;
            else if (grdBagi.SelectedCells.Count == 1)
            {
                int odx;

                int.TryParse(grdBagi.CurrentRow.Cells["clmOdx"].Value.ToString(), out odx);

                DataRow[] rows = this.mTableEduItem.Select("Odx < " + odx);

                if (rows.Length > 0)
                {
                    int targetOdx = Convert.ToInt32(rows.CopyToDataTable().Compute("max([Odx])", string.Empty));

                    this.mTableEduItem.Select("Odx = " + targetOdx)[0]["Odx"] = -9999;

                    this.mTableEduItem.Select("Odx = " + odx)[0]["Odx"] = targetOdx;
                    this.mTableEduItem.Select("Odx = -9999")[0]["Odx"] = odx;

                    mTableEduItem.DefaultView.Sort = "Odx ASC";

                    for (int i = 0; i < grdBagi.Rows.Count; i++)
                    {
                        if (grdBagi.Rows[i].Cells["clmOdx"].Value.ToString() == targetOdx.ToString())
                        {
                            grdBagi.Rows[i].IsCurrent = true;
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
                foreach (var k in grdBagi.SelectedCells)
                {
                    if (selectedColumName.Contains(k.ColumnInfo.Name) == false)
                        selectedColumName.Add(k.ColumnInfo.Name);
                }

                //옮겨야할 일련번호 추출
                for (int i = grdBagi.SelectedCells.Count - 1; i >= 0; i--)
                {
                    odx = Convert.ToInt32(grdBagi.SelectedCells[i].RowInfo.Cells["clmOdx"].Value);

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

                    DataRow[] rows = this.mTableEduItem.Select("Odx < " + odx);

                    if (rows.Length > 0)
                    {
                        //targetOdx = Convert.ToInt32(rows.CopyToDataTable().Compute("max([Odx])", string.Empty));
                        targetOdx = targetOdxs[i][1];
                        this.mTableEduItem.Select("Odx = " + targetOdx)[0]["Odx"] = -9999;

                        this.mTableEduItem.Select("Odx = " + odx)[0]["Odx"] = targetOdx;
                        this.mTableEduItem.Select("Odx = -9999")[0]["Odx"] = odx;

                        mTableEduItem.DefaultView.Sort = "Odx ASC";
                    }
                }
                grdBagi.ClearSelection();
                grdBagi.CurrentRow = null;
                //셀 재선택
                for (int i = 0; i < grdBagi.Rows.Count; i++)
                {
                    for (int j = 0; j < targetOdxs.Count; j++)
                    {
                        if (Convert.ToInt32(grdBagi.Rows[i].Cells["clmOdx"].Value) == targetOdxs[j][1])
                        {
                            foreach (var el in grdBagi.Columns)
                            {
                                if (selectedColumName.Contains(el.Name))
                                {
                                    grdBagi.Rows[i].Cells[el.Name].IsSelected = true;
                                    grdBagi.Rows[i].Cells[el.Name].EnsureVisible();
                                }

                            }
                        }
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (_Worker != null && _Worker.IsBusy)
            {
                Biz.Show("진행중인 작업이 있으므로 잠시 후 다시 실행해 주십시오.");
                return;
            }

            if (mTableEduItem == null)
            {
                Biz.Show(this, "데이터를 초기화 필요.");
                return;
            }

            DataRow row = this.mTableEduItem.NewRow();

            if (mTableEduItem.Rows.Count == 0)
            {
                row["Odx"] = 1;
            }
            else
            {
                row["Odx"] = Convert.ToInt32(mTableEduItem.Compute("max([Odx])", string.Empty)) + 1;
            }

            mTableEduItem.DefaultView.Sort = "Odx ASC";

            this.mTableEduItem.Rows.Add(row);

            grdBagi.CurrentRow = null;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (mTableEduItem == null)
            {
                Biz.Show(this, "데이터를 초기화 필요.");
                return;
            }

            for (int i = 0; i < grdBagi.Rows.Count; i++)
            {
                if (grdBagi.Rows[i].Cells["clmIsSelect"].Value != null && (grdBagi.Rows[i].Cells["clmIsSelect"].Value.ToString().ToLower() == "true" || grdBagi.Rows[i].Cells["clmIsSelect"].Value.ToString().ToLower() == "on" || grdBagi.Rows[i].Cells["clmIsSelect"].Value.ToString().ToLower() == "1"))
                {
                    grdBagi.Rows.Remove(grdBagi.Rows[i]);

                    i--;
                }
            }

            //grdBagi.Rows.Remove(grdBagi.SelectedCells.fo);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Biz.ExportGridView(grdBagi);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(_CurrentGubun == Gubun.Visit)
            {
                DialogResult res = Biz.Show("공정이 선택되지 않은 국소배기 설비는 저장되지 않고 삭제됩니다." + Environment.NewLine + "계속진행하시겠습니까?", "경고", MessageBoxButtons.OKCancel);

                if (res != DialogResult.OK)
                    return;
            }
            else if(_CurrentGubun == Gubun.Visit)
            {
                DialogResult res = Biz.Show(_Year + "년도 데이터를 초기화 후 저장합니다." + Environment.NewLine + "계속진행하시겠습니까?", "경고", MessageBoxButtons.OKCancel);

                if (res != DialogResult.OK)
                    return;
            }
            SaveData();
        }

        private void grdBagi_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < grdBagi.Rows.Count; i++)
            {
                grdBagi.Rows[i].Cells["clmOdx"].Value = i + 1;
            }
        }

        private void btnGongjung_Click(object sender, EventArgs e)
        {
            if (mTableEduItem == null)
            {
                Biz.Show(this, "데이터를 초기화 필요.");
                return;
            }

            var rows = grdBagi.Rows.Where(row =>
            {
                if (row.Cells["clmIsSelect"].Value != null && (row.Cells["clmIsSelect"].Value.ToString().ToLower() == "true" || row.Cells["clmIsSelect"].Value.ToString().ToLower() == "on" || row.Cells["clmIsSelect"].Value.ToString().ToLower() == "1"))
                {
                    return true;
                }
                return false;
            });

            if (rows == null || rows.Count() < 1)
            {
                Biz.Show(this, "선택한 행이 없습니다.", "알림");
                return;
            }

            Sub.frmGongjung f = new Sub.frmGongjung(_SaupID, _VisitDate, "", Sub.frmGongjung.ViewGubun.단일공정선택);
            f.StartPosition = FormStartPosition.CenterParent;
            if (f.ShowDialog(this) == DialogResult.Yes)
            {
                rows.ForEach(row =>
                {
                    row.Cells["clmSiteNO"].Value = f.SiteNO;
                    row.Cells["clmSiteName"].Value = f.SiteName;
                    row.Cells["clmGJNO"].Value = f.GJNO;
                    row.Cells["clmGJName"].Value = f.GJName;
                });
            }
            f.Dispose();
            //for (int i = 0; i < grdBagi.Rows.Count; i++)
            //{
            //    if (grdBagi.Rows[i].Cells["clmIsSelect"].Value != null && (grdBagi.Rows[i].Cells["clmIsSelect"].Value.ToString().ToLower() == "true" || grdBagi.Rows[i].Cells["clmIsSelect"].Value.ToString().ToLower() == "on" || grdBagi.Rows[i].Cells["clmIsSelect"].Value.ToString().ToLower() == "1"))
            //    {
            //    }
            //}
        }
    }
}
