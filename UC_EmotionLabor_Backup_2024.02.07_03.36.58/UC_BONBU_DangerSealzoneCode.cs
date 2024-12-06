using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Telerik.WinControls.UI;
using Telerik.Windows.Diagrams.Core;
using static NBOGUN.KihaMail;

namespace NBOGUN
{
    public partial class UC_BONBU_DangerSealzoneCode : UserControl
    {
        AbortableBackgroundWorker _Worker;
        DataTable _TableCode;
        ColumnGroupsViewDefinition _CGV;
        private void SetGridViewTemplate()
        {
            // column groups view             
            this._CGV = new ColumnGroupsViewDefinition();
            //센터명
            _CGV.ColumnGroups.Add(new GridViewColumnGroup(""));
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows.Add(new GridViewColumnGroupRow() { MinHeight = 26 });
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].ShowHeader = false;
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmIdx");
            //관리번호
            _CGV.ColumnGroups.Add(new GridViewColumnGroup(""));
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows.Add(new GridViewColumnGroupRow() { MinHeight = 26 });
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].ShowHeader = false;
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmGrpNO");

            _CGV.ColumnGroups.Add(new GridViewColumnGroup(""));
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows.Add(new GridViewColumnGroupRow() { MinHeight = 26 });
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].ShowHeader = false;
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmCode");

            //사업장명
            _CGV.ColumnGroups.Add(new GridViewColumnGroup(""));
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows.Add(new GridViewColumnGroupRow() { MinHeight = 26 });
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].ShowHeader = false;
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmName1");

            //사업장관리번호
            _CGV.ColumnGroups.Add(new GridViewColumnGroup(""));
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows.Add(new GridViewColumnGroupRow() { MinHeight = 26 });
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].ShowHeader = false;
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmName2");

            //공단관리번호
            _CGV.ColumnGroups.Add(new GridViewColumnGroup(""));
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows.Add(new GridViewColumnGroupRow() { MinHeight = 26 });
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].ShowHeader = false;
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmName3");

            //공단개시번호 
            _CGV.ColumnGroups.Add(new GridViewColumnGroup(""));
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows.Add(new GridViewColumnGroupRow() { MinHeight = 26 });
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].ShowHeader = false;
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmQuestion");

            //노동지청
            _CGV.ColumnGroups.Add(new GridViewColumnGroup(""));
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows.Add(new GridViewColumnGroupRow() { MinHeight = 26 });
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].ShowHeader = false;
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmContent");

            //대표자
            _CGV.ColumnGroups.Add(new GridViewColumnGroup(""));
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows.Add(new GridViewColumnGroupRow() { MinHeight = 26 });
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].ShowHeader = false;
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmAnswerY");

            _CGV.ColumnGroups.Add(new GridViewColumnGroup(""));
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows.Add(new GridViewColumnGroupRow() { MinHeight = 26 });
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].ShowHeader = false;
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmGoY");

            _CGV.ColumnGroups.Add(new GridViewColumnGroup(""));
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows.Add(new GridViewColumnGroupRow() { MinHeight = 26 });
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].ShowHeader = false;
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmAnswerN");

            _CGV.ColumnGroups.Add(new GridViewColumnGroup(""));
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows.Add(new GridViewColumnGroupRow() { MinHeight = 26 });
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].ShowHeader = false;
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmGoN");

            _CGV.ColumnGroups.Add(new GridViewColumnGroup(""));
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows.Add(new GridViewColumnGroupRow() { MinHeight = 26 });
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].ShowHeader = false;
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmAnswerU");

            _CGV.ColumnGroups.Add(new GridViewColumnGroup(""));
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows.Add(new GridViewColumnGroupRow() { MinHeight = 26 });
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].ShowHeader = false;
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmGoU");
            //clmShownedAnswer
            _CGV.ColumnGroups.Add(new GridViewColumnGroup("노출문구"));
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows.Add(new GridViewColumnGroupRow() { MinHeight = 26 });
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].ShowHeader = false;
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmShownedAnswer");
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmShownedLabel");

            _CGV.ColumnGroups.Add(new GridViewColumnGroup(""));
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows.Add(new GridViewColumnGroupRow() { MinHeight = 26 });
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].ShowHeader = false;
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmIsCritical");

            //주소, 방문주소
            _CGV.ColumnGroups.Add(new GridViewColumnGroup("합리성"));
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows.Add(new GridViewColumnGroupRow() { MinHeight = 26 });
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmPointReasonY");
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmPointReasonN");
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmPointReasonU");
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmPointReasonWeight");
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmPointReasonMax");
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmPointReasonMin");
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmPointReasonSum");
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmPointReason");

            _CGV.ColumnGroups.Add(new GridViewColumnGroup("위험성"));
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows.Add(new GridViewColumnGroupRow() { MinHeight = 26 });
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmPointDangerY");
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmPointDangerN");
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmPointDangerU");
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmPointDangerWeight");
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmPointDangerMax");
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmPointDangerMin");
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmPointDangerSum");
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmPointDanger");

            _CGV.ColumnGroups.Add(new GridViewColumnGroup(""));
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows.Add(new GridViewColumnGroupRow() { MinHeight = 26 });
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].ShowHeader = false;
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmBigo1");

            _CGV.ColumnGroups.Add(new GridViewColumnGroup(""));
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows.Add(new GridViewColumnGroupRow() { MinHeight = 26 });
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].ShowHeader = false;
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmBigo2");

            _CGV.ColumnGroups.Add(new GridViewColumnGroup(""));
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows.Add(new GridViewColumnGroupRow() { MinHeight = 26 });
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].ShowHeader = false;
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmBigo3");

            _CGV.ColumnGroups.Add(new GridViewColumnGroup(""));
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows.Add(new GridViewColumnGroupRow() { MinHeight = 26 });
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].ShowHeader = false;
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmMemo");

           

            _CGV.ColumnGroups.Add(new GridViewColumnGroup("파일"));
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows.Add(new GridViewColumnGroupRow() { MinHeight = 26 });
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].ShowHeader = false;
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmFileFind");
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmFileUrl");
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmFileDel");

            _CGV.ColumnGroups.Add(new GridViewColumnGroup(""));
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows.Add(new GridViewColumnGroupRow() { MinHeight = 26 });
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].ShowHeader = false;
            this._CGV.ColumnGroups[_CGV.ColumnGroups.Count - 1].Rows[0].ColumnNames.Add("clmOdx");

            this.grdCode.ViewDefinition = _CGV;
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
                _TableCode = Biz.Instance.BONBU_CodeSealZone();

                if (_TableCode != null)
                {
                    args.Result = _TableCode;
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
                DataTable result;

                if(Biz.Instance.BONBU_CodeSealZoneDel(con, tran, -1) < 0)
                {
                    tran.Rollback();
                    con.Close();
                    return;
                }

                int idx;
                string grpNO;
                string Name1, Name2, Name3, Question, Content, AnswerY, GoY, AnswerN, GoN, AnswerU, GoU, ShownedLabel, IsCritical, PointReasonY, PointReasonN,
                    PointReasonU, PointReasonWeight, PointReasonMax, PointReasonMin, PointDangerY, PointDangerN, PointDangerU, PointDangerWeight, PointDangerMax,
                    PointDangerMin, PointReasonSum, PointDangerSum, PointReason, PointDanger, Bigo1, Bigo2, Bigo3, Memo, FileUrl, Code, ShownedAnswer;

                grdCode.Rows.AsParallel().ForEach(row =>
                {
                    try
                    {
                        idx = Convert.ToInt32(row.Cells["clmIdx"].Value);
                    }
                    catch
                    {
                        idx = -1;
                    }
                    grpNO = row.Cells["clmGrpNO"].Value?.ToString() ?? "";
                    Name1 = row.Cells["clmName1"].Value?.ToString() ?? "";
                    Name2 = row.Cells["clmName2"].Value?.ToString() ?? "";
                    Name3 = row.Cells["clmName3"].Value?.ToString() ?? "";
                    Question = row.Cells["clmQuestion"].Value?.ToString() ?? "";
                    Content = row.Cells["clmContent"].Value?.ToString() ?? "";
                    AnswerY = row.Cells["clmAnswerY"].Value?.ToString() ?? "";
                    GoY = row.Cells["clmGoY"].Value?.ToString() ?? "";
                    AnswerN = row.Cells["clmAnswerN"].Value?.ToString() ?? "";
                    GoN = row.Cells["clmGoN"].Value?.ToString() ?? "";
                    AnswerU = row.Cells["clmAnswerU"].Value?.ToString() ?? "";
                    GoU = row.Cells["clmGoU"].Value?.ToString() ?? "";
                    ShownedLabel = row.Cells["clmShownedLabel"].Value?.ToString() ?? "";
                    IsCritical = row.Cells["clmIsCritical"].Value?.ToString() ?? "";
                    PointReasonY = row.Cells["clmPointReasonY"].Value?.ToString() ?? "";
                    PointReasonN = row.Cells["clmPointReasonN"].Value?.ToString() ?? "";
                    PointReasonU = row.Cells["clmPointReasonU"].Value?.ToString() ?? "";
                    PointReasonWeight = row.Cells["clmPointReasonWeight"].Value?.ToString() ?? "";
                    PointReasonMax = row.Cells["clmPointReasonMax"].Value?.ToString() ?? "";
                    PointReasonMin = row.Cells["clmPointReasonMin"].Value?.ToString() ?? "";
                    PointDangerY = row.Cells["clmPointDangerY"].Value?.ToString() ?? "";
                    PointDangerN = row.Cells["clmPointDangerN"].Value?.ToString() ?? "";
                    PointDangerU = row.Cells["clmPointDangerU"].Value?.ToString() ?? "";
                    PointDangerWeight = row.Cells["clmPointDangerWeight"].Value?.ToString() ?? "";
                    PointDangerMax = row.Cells["clmPointDangerMax"].Value?.ToString() ?? "";
                    PointDangerMin = row.Cells["clmPointDangerMin"].Value?.ToString() ?? "";
                    PointReasonSum = row.Cells["clmPointReasonSum"].Value?.ToString() ?? "";
                    PointDangerSum = row.Cells["clmPointDangerSum"].Value?.ToString() ?? "";
                    PointReason = row.Cells["clmPointReason"].Value?.ToString() ?? "";
                    PointDanger = row.Cells["clmPointDanger"].Value?.ToString() ?? "";
                    Bigo1 = row.Cells["clmBigo1"].Value?.ToString() ?? "";
                    Bigo2 = row.Cells["clmBigo2"].Value?.ToString() ?? "";
                    Bigo3 = row.Cells["clmBigo3"].Value?.ToString() ?? "";
                    Memo = row.Cells["clmMemo"].Value?.ToString() ?? "";
                    FileUrl = row.Cells["clmFileUrl"].Value?.ToString() ?? "";
                    Code = row.Cells["clmCode"].Value?.ToString() ?? "";
                    ShownedAnswer = row.Cells["clmShownedAnswer"].Value?.ToString() ?? "";
                    result = new DataTable();
                    result = Biz.Instance.BONBU_CodeSealZoneSave(con, tran, idx, grpNO, Name1, Name2, Name3, Question, Content, AnswerY, GoY, AnswerN, GoN, AnswerU, GoU, ShownedLabel, IsCritical, PointReasonY, PointReasonN,
                        PointReasonU, PointReasonWeight, PointReasonMax, PointReasonMin, PointDangerY, PointDangerN, PointDangerU, PointDangerWeight, PointDangerMax,
                        PointDangerMin, PointReasonSum, PointDangerSum, PointReason, PointDanger, Bigo1, Bigo2, Bigo3, Memo, FileUrl, Code, ShownedAnswer);

                    if (result == null || result.Rows.Count == 0)
                    {
                        if (con.State == ConnectionState.Closed)
                            return;

                        tran.Rollback();
                        con.Close();
                        return;
                    }
                    else
                    {
                        row.Cells["clmIdx"].Value = result.Rows[0][0];

                        _Worker.ReportProgress(-1, idx);
                    }
                });

                tran.Commit();
                con.Close();

                _TableCode = Biz.Instance.BONBU_CodeSealZone();

                if (_TableCode != null)
                {
                    args.Result = _TableCode;
                }
            };
            _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                if(args.ProgressPercentage < 0)
                {
                    waitingBar.Text = args.UserState.ToString() + "/" + grdCode.RowCount.ToString();
                }
            };
            _Worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    //DataTable dt = (DataTable)args.Result;
                    grdCode.DataSource = _TableCode;

                    Biz.Show(this, "저장에 성공 했습니다.");
                }
                else
                {
                    Biz.Show(this, "저장에 실패 했습니다.");
                }

                waitingBar.StopWaiting();
            };

            waitingBar.StartWaiting();
            await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
            BeginInvoke(new MethodInvoker(_Worker.RunWorkerAsync));
        }
        public UC_BONBU_DangerSealzoneCode()
        {
            InitializeComponent();

            //Biz.Instance.SetGridViewOption(grdCode);

            SetGridViewTemplate();

            DataTable dt = new DataTable();
            dt.Columns.Add("Name", typeof(string));
            dt.Rows.Add(new string[] { "" });
            dt.Rows.Add(new string[] { "1.예" });
            dt.Rows.Add(new string[] { "2.아니오" });
            dt.Rows.Add(new string[] { "3.모름" });

            GridViewComboBoxColumn col = grdCode.Columns["clmShownedAnswer"] as GridViewComboBoxColumn;
            col.DataSource = dt;
            col.DisplayMember = "Name";
        }

        ~UC_BONBU_DangerSealzoneCode()
        {
            if (_Worker != null && _Worker.IsBusy)
            {
                _Worker.CancelAsync();
                _Worker.Abort();
                _Worker.Dispose();
            }
        }

        private void UC_BONBU_DangerSealzoneCode_Load(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }
        //행 추가
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (_Worker != null && _Worker.IsBusy)
            {
                Biz.Show("진행중인 작업이 있으므로 잠시 후 다시 실행해 주십시오.");
                return;
            }

            if (_TableCode == null)
            {
                Biz.Show(this, "데이터를 초기화 필요.");
                return;
            }

            DataRow row = this._TableCode.NewRow();

            if (_TableCode.Rows.Count == 0)
            {
                row["Odx"] = 1;
            }
            else
            {
                row["Odx"] = Convert.ToInt32(_TableCode.Compute("max([Odx])", string.Empty)) + 1;
            }

            _TableCode.DefaultView.Sort = "Odx ASC";

            this._TableCode.Rows.Add(row);            

            grdCode.CurrentRow = null;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (_TableCode == null)
            {
                Biz.Show(this, "데이터를 초기화 필요.");
                return;
            }

            grdCode.Rows.Remove(grdCode.CurrentRow);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Biz.ExportGridView(grdCode);
        }

        //저장하기
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void grdCode_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewTableHeaderRowInfo)
                return;

            if (e.Column.Name == "clmIsCritical")
            {
                if ((e.Row.Cells["clmIsCritical"].Value?.ToString() ?? "") == "")
                    e.Row.Cells["clmIsCritical"].Value = "○";
                else
                    e.Row.Cells["clmIsCritical"].Value = "";
            }
            else if(e.Column.Name == "clmFileFind")
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //기존 파일 있는 지 여부 확인 있으면 삭제
                    string uploadfilename = e.Row.Cells["clmFileUrl"].Value?.ToString().Trim() ?? "";
                    string url;

                    if(uploadfilename != "")
                    {
                        url = "https://ohis.kiha21.or.kr/FileUpload/BOGUN/" + uploadfilename;

                        //기존 파일 삭제
                        if (uploadfilename != "")
                        {
                            // 기존 파일 삭제
                            if (Biz.URLExists(url))
                            {
                                //파일 삭제
                                Biz.Instance.FileDelete("/BOGUN", uploadfilename);

                                if (Biz.URLExists(url))
                                {
                                    Biz.Show(this, "기존 파일 삭제에 실패했습니다");
                                    return;
                                }
                            }
                        }                        
                    }

                    SqlConnection con = Biz.Instance.Connection;
                    if (con.State != ConnectionState.Open) con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    int idx;
                    string grpNO;
                    string Name1, Name2, Name3, Question, Content, AnswerY, GoY, AnswerN, GoN, AnswerU, GoU, ShownedLabel, IsCritical, PointReasonY, PointReasonN,
                        PointReasonU, PointReasonWeight, PointReasonMax, PointReasonMin, PointDangerY, PointDangerN, PointDangerU, PointDangerWeight, PointDangerMax,
                        PointDangerMin, PointReasonSum, PointDangerSum, PointReason, PointDanger, Bigo1, Bigo2, Bigo3, Memo, FileUrl, Code, ShownedAnswer;

                    var row = e.Row;

                    try
                    {
                        idx = Convert.ToInt32(row.Cells["clmIdx"].Value);
                    }
                    catch
                    {
                        idx = -1;
                    }
                    grpNO = row.Cells["clmGrpNO"].Value?.ToString() ?? "";
                    Name1 = row.Cells["clmName1"].Value?.ToString() ?? "";
                    Name2 = row.Cells["clmName2"].Value?.ToString() ?? "";
                    Name3 = row.Cells["clmName3"].Value?.ToString() ?? "";
                    Question = row.Cells["clmQuestion"].Value?.ToString() ?? "";
                    Content = row.Cells["clmContent"].Value?.ToString() ?? "";
                    AnswerY = row.Cells["clmAnswerY"].Value?.ToString() ?? "";
                    GoY = row.Cells["clmGoY"].Value?.ToString() ?? "";
                    AnswerN = row.Cells["clmAnswerN"].Value?.ToString() ?? "";
                    GoN = row.Cells["clmGoN"].Value?.ToString() ?? "";
                    AnswerU = row.Cells["clmAnswerU"].Value?.ToString() ?? "";
                    GoU = row.Cells["clmGoU"].Value?.ToString() ?? "";
                    ShownedLabel = row.Cells["clmShownedLabel"].Value?.ToString() ?? "";
                    IsCritical = row.Cells["clmIsCritical"].Value?.ToString() ?? "";
                    PointReasonY = row.Cells["clmPointReasonY"].Value?.ToString() ?? "";
                    PointReasonN = row.Cells["clmPointReasonN"].Value?.ToString() ?? "";
                    PointReasonU = row.Cells["clmPointReasonU"].Value?.ToString() ?? "";
                    PointReasonWeight = row.Cells["clmPointReasonWeight"].Value?.ToString() ?? "";
                    PointReasonMax = row.Cells["clmPointReasonMax"].Value?.ToString() ?? "";
                    PointReasonMin = row.Cells["clmPointReasonMin"].Value?.ToString() ?? "";
                    PointDangerY = row.Cells["clmPointDangerY"].Value?.ToString() ?? "";
                    PointDangerN = row.Cells["clmPointDangerN"].Value?.ToString() ?? "";
                    PointDangerU = row.Cells["clmPointDangerU"].Value?.ToString() ?? "";
                    PointDangerWeight = row.Cells["clmPointDangerWeight"].Value?.ToString() ?? "";
                    PointDangerMax = row.Cells["clmPointDangerMax"].Value?.ToString() ?? "";
                    PointDangerMin = row.Cells["clmPointDangerMin"].Value?.ToString() ?? "";
                    PointReasonSum = row.Cells["clmPointReasonSum"].Value?.ToString() ?? "";
                    PointDangerSum = row.Cells["clmPointDangerSum"].Value?.ToString() ?? "";
                    PointReason = row.Cells["clmPointReason"].Value?.ToString() ?? "";
                    PointDanger = row.Cells["clmPointDanger"].Value?.ToString() ?? "";
                    Bigo1 = row.Cells["clmBigo1"].Value?.ToString() ?? "";
                    Bigo2 = row.Cells["clmBigo2"].Value?.ToString() ?? "";
                    Bigo3 = row.Cells["clmBigo3"].Value?.ToString() ?? "";
                    Memo = row.Cells["clmMemo"].Value?.ToString() ?? "";
                    FileUrl = row.Cells["clmFileUrl"].Value?.ToString() ?? "";
                    Code = row.Cells["clmCode"].Value?.ToString() ?? "";
                    ShownedAnswer = row.Cells["clmShownedAnswer"].Value?.ToString() ?? "";
                    DataTable result = new DataTable();
                    result = Biz.Instance.BONBU_CodeSealZoneSave(con, tran, idx, grpNO, Name1, Name2, Name3, Question, Content, AnswerY, GoY, AnswerN, GoN, AnswerU, GoU, ShownedLabel, IsCritical, PointReasonY, PointReasonN,
                        PointReasonU, PointReasonWeight, PointReasonMax, PointReasonMin, PointDangerY, PointDangerN, PointDangerU, PointDangerWeight, PointDangerMax,
                        PointDangerMin, PointReasonSum, PointDangerSum, PointReason, PointDanger, Bigo1, Bigo2, Bigo3, Memo, FileUrl, Code, ShownedAnswer);

                    if (result == null || result.Rows.Count == 0)
                    {
                        tran.Rollback();
                        con.Close();
                        return;
                    }
                    else
                    {
                        row.Cells["clmIdx"].Value = result.Rows[0][0];
                        tran.Commit();
                        con.Close();
                    }
                    uploadfilename = openFileDialog.FileName;
                    string pfileName = uploadfilename.Substring(uploadfilename.LastIndexOf("\\") + 1, uploadfilename.Length - uploadfilename.LastIndexOf("\\") - 1);
                    string guid = Guid.NewGuid().ToString();
                    Biz.Instance.FileUpload("/BOGUN", openFileDialog.FileName, guid + "_" + pfileName);

                    System.Threading.Thread.Sleep(1000);

                    url = "https://ohis.kiha21.or.kr/FileUpload/BOGUN/" + guid + "_" + pfileName;

                    //기존 파일 삭제
                    if (uploadfilename != "")
                    {
                        // 기존 파일 삭제
                        if (Biz.URLExists(url))
                        {
                            result = Biz.Instance.BONBU_CodeSealZoneSave(con, tran, idx, grpNO, Name1, Name2, Name3, Question, Content, AnswerY, GoY, AnswerN, GoN, AnswerU, GoU, ShownedLabel, IsCritical, PointReasonY, PointReasonN,
                                PointReasonU, PointReasonWeight, PointReasonMax, PointReasonMin, PointDangerY, PointDangerN, PointDangerU, PointDangerWeight, PointDangerMax,
                                PointDangerMin, PointReasonSum, PointDangerSum, PointReason, PointDanger, Bigo1, Bigo2, Bigo3, Memo, guid + "_" + pfileName, Code, ShownedAnswer);
                            e.Row.Cells["clmFileUrl"].Value = guid + "_" + pfileName;
                            Biz.Show(this, "저장에 성공했습니다");
                            return;
                        }
                        else
                        {
                            Biz.Show(this, "저장에 실패했습니다");
                            return;
                        }
                    }
                    
                }
                openFileDialog.Dispose();
            }
            else if(e.Column.Name == "clmFileUrl")
            {
                string uploadfilename = e.Row.Cells["clmFileUrl"].Value?.ToString().Trim() ?? "";
                if(uploadfilename != "")
                {
                    string url = "https://ohis.kiha21.or.kr/FileUpload/BOGUN/" + uploadfilename;
                    System.Diagnostics.Process.Start(url);
                }
            }
            else if(e.Column.Name == "clmFileDel")
            {
                string uploadfilename = e.Row.Cells["clmFileUrl"].Value?.ToString().Trim() ?? "";
                if (uploadfilename != "")
                {
                    DialogResult dialogResult = Biz.Show(this, "파일 삭제는 저장이 바로 진행됩니다.\r\n계속 진행하시겠습니까?", "알림", MessageBoxButtons.OKCancel);

                    if (dialogResult != DialogResult.OK)
                        return;

                    string url = "https://ohis.kiha21.or.kr/FileUpload/BOGUN/" + uploadfilename;

                    Biz.Instance.FileDelete("/BOGUN", uploadfilename);

                    if(Biz.URLExists(url))
                    {
                        Biz.Show(this, "삭제에 실패했습니다.");
                        return;
                    }
                    else
                    {
                        SqlConnection con = Biz.Instance.Connection;
                        if (con.State != ConnectionState.Open) con.Open();
                        SqlTransaction tran = con.BeginTransaction();
                        int idx;
                        string grpNO;
                        string Name1, Name2, Name3, Question, Content, AnswerY, GoY, AnswerN, GoN, AnswerU, GoU, ShownedLabel, IsCritical, PointReasonY, PointReasonN,
                            PointReasonU, PointReasonWeight, PointReasonMax, PointReasonMin, PointDangerY, PointDangerN, PointDangerU, PointDangerWeight, PointDangerMax,
                            PointDangerMin, PointReasonSum, PointDangerSum, PointReason, PointDanger, Bigo1, Bigo2, Bigo3, Memo, FileUrl, Code, ShownedAnswer;

                        var row = e.Row;

                        try
                        {
                            idx = Convert.ToInt32(row.Cells["clmIdx"].Value);
                        }
                        catch
                        {
                            idx = -1;
                        }
                        grpNO = row.Cells["clmGrpNO"].Value?.ToString() ?? "";
                        Name1 = row.Cells["clmName1"].Value?.ToString() ?? "";
                        Name2 = row.Cells["clmName2"].Value?.ToString() ?? "";
                        Name3 = row.Cells["clmName3"].Value?.ToString() ?? "";
                        Question = row.Cells["clmQuestion"].Value?.ToString() ?? "";
                        Content = row.Cells["clmContent"].Value?.ToString() ?? "";
                        AnswerY = row.Cells["clmAnswerY"].Value?.ToString() ?? "";
                        GoY = row.Cells["clmGoY"].Value?.ToString() ?? "";
                        AnswerN = row.Cells["clmAnswerN"].Value?.ToString() ?? "";
                        GoN = row.Cells["clmGoN"].Value?.ToString() ?? "";
                        AnswerU = row.Cells["clmAnswerU"].Value?.ToString() ?? "";
                        GoU = row.Cells["clmGoU"].Value?.ToString() ?? "";
                        ShownedLabel = row.Cells["clmShownedLabel"].Value?.ToString() ?? "";
                        IsCritical = row.Cells["clmIsCritical"].Value?.ToString() ?? "";
                        PointReasonY = row.Cells["clmPointReasonY"].Value?.ToString() ?? "";
                        PointReasonN = row.Cells["clmPointReasonN"].Value?.ToString() ?? "";
                        PointReasonU = row.Cells["clmPointReasonU"].Value?.ToString() ?? "";
                        PointReasonWeight = row.Cells["clmPointReasonWeight"].Value?.ToString() ?? "";
                        PointReasonMax = row.Cells["clmPointReasonMax"].Value?.ToString() ?? "";
                        PointReasonMin = row.Cells["clmPointReasonMin"].Value?.ToString() ?? "";
                        PointDangerY = row.Cells["clmPointDangerY"].Value?.ToString() ?? "";
                        PointDangerN = row.Cells["clmPointDangerN"].Value?.ToString() ?? "";
                        PointDangerU = row.Cells["clmPointDangerU"].Value?.ToString() ?? "";
                        PointDangerWeight = row.Cells["clmPointDangerWeight"].Value?.ToString() ?? "";
                        PointDangerMax = row.Cells["clmPointDangerMax"].Value?.ToString() ?? "";
                        PointDangerMin = row.Cells["clmPointDangerMin"].Value?.ToString() ?? "";
                        PointReasonSum = row.Cells["clmPointReasonSum"].Value?.ToString() ?? "";
                        PointDangerSum = row.Cells["clmPointDangerSum"].Value?.ToString() ?? "";
                        PointReason = row.Cells["clmPointReason"].Value?.ToString() ?? "";
                        PointDanger = row.Cells["clmPointDanger"].Value?.ToString() ?? "";
                        Bigo1 = row.Cells["clmBigo1"].Value?.ToString() ?? "";
                        Bigo2 = row.Cells["clmBigo2"].Value?.ToString() ?? "";
                        Bigo3 = row.Cells["clmBigo3"].Value?.ToString() ?? "";
                        Memo = row.Cells["clmMemo"].Value?.ToString() ?? "";
                        Code = row.Cells["clmCode"].Value?.ToString() ?? "";
                        ShownedAnswer = row.Cells["clmShownedAnswer"].Value?.ToString() ?? "";
                        DataTable result = new DataTable();
                        result = Biz.Instance.BONBU_CodeSealZoneSave(con, tran, idx, grpNO, Name1, Name2, Name3, Question, Content, AnswerY, GoY, AnswerN, GoN, AnswerU, GoU, ShownedLabel, IsCritical, PointReasonY, PointReasonN,
                            PointReasonU, PointReasonWeight, PointReasonMax, PointReasonMin, PointDangerY, PointDangerN, PointDangerU, PointDangerWeight, PointDangerMax,
                            PointDangerMin, PointReasonSum, PointDangerSum, PointReason, PointDanger, Bigo1, Bigo2, Bigo3, Memo, "", Code, ShownedAnswer);

                        if (result == null || result.Rows.Count == 0)
                        {
                            tran.Rollback();
                            con.Close();
                            return;
                        }
                        else
                        {
                            row.Cells["clmIdx"].Value = result.Rows[0][0];
                            tran.Commit();
                            con.Close();
                        }

                        e.Row.Cells["clmFileUrl"].Value = "";
                    }
                }
            }
        }

        private void grdCode_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            if (e.Column.Name == "clmGoY")
            {
                if (int.TryParse((e.Value?.ToString() ?? "0"), out int result) == true)
                {
                    if (result > 0)
                    {
                        e.Row.Cells["clmAnswerY"].Value = "○";
                    }
                    else
                        e.Row.Cells["clmAnswerY"].Value = "";

                    e.Row.Cells["clmGoY"].Value = result.ToString();
                }
                else
                {
                    e.Row.Cells["clmAnswerY"].Value = "";
                    e.Row.Cells["clmGoY"].Value = "0";
                }
            }
            else if (e.Column.Name == "clmGoN")
            {
                if (int.TryParse((e.Value?.ToString() ?? "0"), out int result) == true)
                {
                    if (result > 0)
                    {
                        e.Row.Cells["clmAnswerN"].Value = "○";
                    }
                    else
                        e.Row.Cells["clmAnswerN"].Value = "";

                    e.Row.Cells["clmGoN"].Value = result.ToString();
                }
                else
                {
                    e.Row.Cells["clmAnswerN"].Value = "";
                    e.Row.Cells["clmGoN"].Value = "0";
                }
            }
            else if (e.Column.Name == "clmGoU")
            {
                if (int.TryParse((e.Value?.ToString() ?? "0"), out int result) == true)
                {
                    if (result > 0)
                    {
                        e.Row.Cells["clmAnswerU"].Value = "○";
                    }
                    else
                        e.Row.Cells["clmAnswerU"].Value = "";

                    e.Row.Cells["clmGoU"].Value = result.ToString();
                }
                else
                {
                    e.Row.Cells["clmAnswerU"].Value = "";
                    e.Row.Cells["clmGoU"].Value = "0";
                }
            }
            else if (e.Column.Name.Contains("clmPoint") || e.Column.Name == "clmGrpNO")
            {
                if (float.TryParse((e.Value?.ToString() ?? "0"), out float result) == false)
                    e.Row.Cells[e.Column.Name].Value = ((int)result).ToString();
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            grdCode.SelectedCells.AsParallel().ForEach(cell => {
                cell.Value = txtChange.Text.Trim();
            });
        }

        private void grdCode_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i =0;i < grdCode.Rows.Count;i++)
            {
                grdCode.Rows[i].Cells["clmOdx"].Value = i + 1;
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (grdCode.SelectedCells == null)
                return;
            else if (grdCode.SelectedCells.Count == 1)//한개 행만 선택할때
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
                //int idx;
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

            grdCode_DataBindingComplete(null, null);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (grdCode.SelectedCells == null)
                return;
            else if (grdCode.SelectedCells.Count == 1)
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

                    for (int i = 0; i < grdCode.Rows.Count; i++)
                    {
                        if (grdCode.Rows[i].Cells["clmOdx"].Value.ToString() == targetOdx.ToString())
                        {
                            grdCode.Rows[i].IsCurrent = true;
                        }
                    }
                }
            }
            else
            {
                List<int> selectedOdxs = new List<int>();//선택한 행
                List<int[]> targetOdxs = new List<int[]>();//이동하고 선택할 대상 행의 Odx들
                int idx;
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
        }

        private void grdCode_SortChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            for (int i = 0; i < grdCode.Rows.Count; i++)
            {
                grdCode.Rows[i].Cells["clmOdx"].Value = i + 1;
            }
            //mTableBohogu.AcceptChanges();
            _TableCode.DefaultView.Sort = "Odx ASC";

            grdCode_DataBindingComplete(null, null);
        }
    }
}
