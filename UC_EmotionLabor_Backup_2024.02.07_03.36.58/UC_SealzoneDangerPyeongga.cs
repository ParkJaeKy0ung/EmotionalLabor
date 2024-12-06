using NBOGUN.Sub;
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
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Docking;
using Telerik.Windows.Diagrams.Core;
using static System.Windows.Forms.AxHost;

namespace NBOGUN
{
    public partial class UC_SealzoneDangerPyeongga : UserControl
    {
        AbortableBackgroundWorker _Worker;
        int _SaupID;
        string _SaupjaNum;
        string _Year;
        string _SaupjaName;
        string _Date;

        #region Excel Export
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
        [DllImport("SHCore.dll")]
        static extern int SetProcessDpiAwareness(_Process_DPI_Awareness value);
        enum _Process_DPI_Awareness
        {
            Process_DPI_Unaware = 0,
            Process_System_DPI_Aware = 1,
            Process_Per_Monitor_DPI_Aware = 2
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //현재 그리드 엑셀 Export
            if (keyData == (Keys.Alt | Keys.E))
            {
                RadGridView grd = null;

                if (this.ActiveControl is DocumentWindow)
                {
                    DocumentWindow window = (DocumentWindow)this.ActiveControl;

                    if (window.ActiveControl is RadGridView)
                    {
                        grd = window.ActiveControl as RadGridView;
                    }
                }
                else if (this.ActiveControl is RadGridView)
                {
                    grd = this.ActiveControl as RadGridView;
                }
                //현재 포커스된 그리드 찾기


                if (grd != null)
                {
                    Biz.ExportGridView(grd);
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
        void SetValue()
        {
            grdPyeongga.Rows.AsParallel().ForEach(row =>
            {

                string code = row.Cells["clmCode"].Value?.ToString() ?? "";

                if (code != "" && code.Contains("%"))
                {
                    var list = code.Split('%').Where(r => r != "").ToList();

                    if (list != null)
                    {
                        var pRow = grdPyeongga.Rows.Where(r =>
                        {
                            string answer = r.Cells["clmAnswer"].Value?.ToString() ?? "0";
                            answer = answer == "" ? "0" : answer.Substring(0, 1);
                            answer = (answer == "1" ? "Y" : (answer == "2" ? "N" : (answer == "3" ? "U" : (""))));

                            if (list.Contains(r.Cells["clmIdx"].Value.ToString() + answer))
                                return true;
                            else
                                return false;
                        }).ToList();

                        if (pRow != null)
                        {
                            if (pRow.Count() > 0)
                            {
                                row.Cells["clmEnabled"].Value = "1";
                            }
                            else
                                row.Cells["clmEnabled"].Value = "0";
                        }
                        else
                            row.Cells["clmEnabled"].Value = "1";
                    }
                }
                else if (code != "" && code.Contains("&"))
                {
                    var list = code.Split('&').Where(r => r != "").ToList();

                    if (list != null)
                    {
                        var pRow = grdPyeongga.Rows.Where(r =>
                        {
                            string answer = r.Cells["clmAnswer"].Value?.ToString() ?? "0";
                            answer = answer == "" ? "0" : answer.Substring(0, 1);
                            answer = (answer == "1" ? "Y" : (answer == "2" ? "N" : (answer == "3" ? "U" : (""))));

                            if (list.Contains(r.Cells["clmIdx"].Value.ToString() + answer))
                                return true;
                            else
                                return false;
                        }).ToList();

                        if (pRow != null)
                        {
                            if (pRow.Count() == list.Count)
                            {
                                row.Cells["clmEnabled"].Value = "1";
                            }
                            else
                                row.Cells["clmEnabled"].Value = "0";
                        }
                        else
                            row.Cells["clmEnabled"].Value = "1";
                    }
                }
                else if (code != "")
                {
                    string idx = Convert.ToInt16(code.PadLeft(3, '0').Substring(0, 2)).ToString();
                    string yn = code.Substring(idx.Length, 1);
                    var pRow = grdPyeongga.Rows.Where(r => r.Cells["clmIdx"].Value.ToString() == idx);

                    if (pRow != null)
                    {
                        if (yn == "Y")
                        {
                            var answer = pRow.First().Cells["clmAnswer"].Value?.ToString() ?? "0";

                            if (answer != null && answer != "" && answer.Substring(0, 1) == "1")
                            {
                                row.Cells["clmEnabled"].Value = "1";
                            }
                            else
                                row.Cells["clmEnabled"].Value = "0";
                        }
                        else if (yn == "N")
                        {
                            if ((pRow.First().Cells["clmAnswer"].Value?.ToString().Substring(0, 1) ?? "0") == "2")
                            {
                                row.Cells["clmEnabled"].Value = "1";
                            }
                            else
                                row.Cells["clmEnabled"].Value = "0";
                        }
                        else if (yn == "U")
                        {
                            if ((pRow.First().Cells["clmAnswer"].Value?.ToString().Substring(0, 1) ?? "0") == "3")
                            {
                                row.Cells["clmEnabled"].Value = "1";
                            }
                            else
                                row.Cells["clmEnabled"].Value = "0";
                        }
                    }
                }

                string y = row.Cells["clmGoY"].Value.ToString();
                string n = row.Cells["clmGoN"].Value.ToString();
                string u = row.Cells["clmGoU"].Value.ToString();

                if (y != "0" && y != "")
                {

                }
            });
        }

        async void DelDate()
        {
            //날짜는 사전에 체크
            string date = txtDate.Text;

            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            _Worker = new AbortableBackgroundWorker();

            _Worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                SqlConnection con = Biz.Instance.Connection;
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlTransaction tran = con.BeginTransaction();

                int r;

                r = Biz.Instance.SEALZONE_SaupjaDateDel(con, tran, _SaupjaNum, date);

                if(r < 0)
                {
                    tran.Rollback();
                    con.Close();
                    return;
                }

                tran.Commit();
                con.Close();

                //날짜 데이터 리프레쉬
                cboDate.SelectedIndexChanged -= cboDate_SelectedIndexChanged;

                DataTable dt = Biz.Instance.SEALZONE_SaupjaDateList(_SaupjaNum);

                args.Result = dt;
            };
            _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                waitingBar.Text = args.ProgressPercentage.ToString() + "/" + grdPyeongga.RowCount.ToString();
            };
            _Worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    cboDate.DataSource = (DataTable)args.Result;
                    cboDate.DisplayMember = "Date";
                    cboDate.Text = date;

                    Biz.Show(this, "삭제에 성공했습니다.");

                    cboDate.SelectedIndexChanged += cboDate_SelectedIndexChanged;
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

        void SetDateList()
        {
            txtDate.Text = "____-__-__";
            cboDate.SelectedIndexChanged -= cboDate_SelectedIndexChanged;
            //cboDate.DataSource = null;//BackgroundWorker 오류가 발생함

            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            _Worker = new AbortableBackgroundWorker();

            _Worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                DataTable dt = Biz.Instance.SEALZONE_SaupjaDateList(_SaupjaNum);

                //기존 데이터가 없을 경우 
                if(dt.Rows.Count == 0)
                {
                    _Date = "";
                }
                else
                {
                    _Date = dt.AsEnumerable().First()["Date"].ToString();
                }

                DataTable dtData = Biz.Instance.SEALZONE_SaupjaDate(_SaupjaNum, _Date);

                _Worker.ReportProgress(0, dtData);

                args.Result = dt;
            };
            _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                grdPyeongga.DataSource = args.UserState as DataTable;
            };
            _Worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    cboDate.DataSource = (DataTable)args.Result;
                    cboDate.DisplayMember = "Date";
                    if (cboDate.Items.Count > 0)
                        cboDate.SelectedIndex = 0;                    
                    cboDate.SelectedIndexChanged += cboDate_SelectedIndexChanged;

                    if (_Date != "")
                        txtDate.Text = _Date;
                }
                else
                {
                    Biz.Show(this, "데이터를 불러올 수 없습니다.");
                }

                waitingBar.StopWaiting();
            };

            waitingBar.StartWaiting();

            BeginInvoke(new MethodInvoker(_Worker.RunWorkerAsync));
        }

        async void SetDate()
        {
            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            _Worker = new AbortableBackgroundWorker();

            _Worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                DataTable dt = Biz.Instance.SEALZONE_SaupjaDate(_SaupjaNum, _Date);

                args.Result = dt;
            };
            _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {

            };
            _Worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    grdPyeongga.DataSource = (DataTable)args.Result;
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

        /// <summary>
        /// 저장하기
        /// </summary>
        async void SaveData()
        {
            //날짜는 사전에 체크
            string date = txtDate.Text;

            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            _Worker = new AbortableBackgroundWorker();

            _Worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                SqlConnection con = Biz.Instance.Connection;
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlTransaction tran = con.BeginTransaction();

                int r, cnt = 0;
                int idx, odx;
                string grpno, name1, name2, name3, question, content, answerY, goY, answerN, goN, answerU, goU, shownedlabel, iscritical,
                    pointreasonY, pointreasonN, pointreasonU, pointreasonWeight, pointreasonMax, pointreasonMin,
                    pointdangerY, pointdangerN, pointdangerU, pointdangerWeight, pointdangerMax, pointdangerMin,
                    pointreasonSum, pointdangerSum, pointreason, pointdanger, bigo1, bigo2, bigo3, memo, fileurl, answer, code, shownedanswer,
                    enabled;

                r = Biz.Instance.SEALZONE_SaupjaDateDel(con, tran, _SaupjaNum, date);

                if (r < 0)
                {
                    tran.Rollback();
                    con.Close();
                    return;
                }

                grdPyeongga.Rows.AsParallel().ForEach(row =>
                {
                    if (con.State == ConnectionState.Open)
                    {
                        idx = Convert.ToInt32(row.Cells["clmIdx"].Value);
                        grpno = row.Cells["clmGrpNO"].Value?.ToString() ?? "";
                        name1 = row.Cells["clmName1"].Value?.ToString() ?? "";
                        name2 = row.Cells["clmName1"].Value?.ToString() ?? "";
                        name3 = row.Cells["clmName1"].Value?.ToString() ?? "";
                        question = row.Cells["clmQuestion"].Value?.ToString() ?? "";
                        content = row.Cells["clmContent"].Value?.ToString() ?? "";
                        answerY = row.Cells["clmAnswerY"].Value?.ToString() ?? "";
                        goY = row.Cells["clmGoY"].Value?.ToString() ?? "";
                        answerN = row.Cells["clmAnswerN"].Value?.ToString() ?? "";
                        goN = row.Cells["clmGoN"].Value?.ToString() ?? "";
                        answerU = row.Cells["clmAnswerU"].Value?.ToString() ?? "";
                        goU = row.Cells["clmGoU"].Value?.ToString() ?? "";
                        shownedlabel = row.Cells["clmShownedLabel"].Value?.ToString() ?? "";
                        iscritical = row.Cells["clmIsCritical"].Value?.ToString() ?? "";
                        pointreasonY = row.Cells["clmGoU"].Value?.ToString() ?? "";
                        pointreasonN = row.Cells["clmGoU"].Value?.ToString() ?? "";
                        pointreasonU = row.Cells["clmGoU"].Value?.ToString() ?? "";
                        pointreasonWeight = row.Cells["clmPointReasonWeight"].Value?.ToString() ?? "";
                        pointreasonMax = row.Cells["clmPointReasonMax"].Value?.ToString() ?? "";
                        pointreasonMin = row.Cells["clmPointReasonMin"].Value?.ToString() ?? "";
                        pointdangerY = row.Cells["clmPointDangerY"].Value?.ToString() ?? "";
                        pointdangerN = row.Cells["clmPointDangerN"].Value?.ToString() ?? "";
                        pointdangerU = row.Cells["clmPointDangerU"].Value?.ToString() ?? "";
                        pointdangerWeight = row.Cells["clmPointReasonWeight"].Value?.ToString() ?? "";
                        pointdangerMax = row.Cells["clmPointReasonMax"].Value?.ToString() ?? "";
                        pointdangerMin = row.Cells["clmPointReasonMin"].Value?.ToString() ?? "";

                        pointreasonSum = row.Cells["clmPointReasonSum"].Value?.ToString() ?? "";
                        pointdangerSum = row.Cells["clmPointDangerSum"].Value?.ToString() ?? "";
                        pointreason = row.Cells["clmPointReason"].Value?.ToString() ?? "";
                        pointdanger = row.Cells["clmPointDanger"].Value?.ToString() ?? "";
                        bigo1 = row.Cells["clmBigo1"].Value?.ToString() ?? "";
                        bigo2 = row.Cells["clmBigo2"].Value?.ToString() ?? "";
                        bigo3 = row.Cells["clmBigo3"].Value?.ToString() ?? "";
                        memo = row.Cells["clmMemo"].Value?.ToString() ?? "";
                        fileurl = row.Cells["clmFileUrl"].Value?.ToString() ?? "";
                        answer = row.Cells["clmAnswer"].Value?.ToString() ?? "";
                        code = row.Cells["clmCode"].Value?.ToString() ?? "";
                        shownedanswer = row.Cells["clmShownedAnswer"].Value?.ToString() ?? "";
                        odx = Convert.ToInt32(row.Cells["clmOdx"].Value);
                        enabled = row.Cells["clmEnabled"].Value?.ToString() ?? "0";

                        r = Biz.Instance.SEALZONE_SaupjaDateSave(con, tran, _SaupjaNum, date, idx, grpno, name1, name2, name3, question, content, answerY, goY, answerN, goN, answerU, goU,
                            shownedlabel, iscritical, pointreasonY, pointreasonN, pointreasonU, pointreasonWeight, pointreasonMax, pointreasonMin, pointdangerY, pointdangerN, pointdangerU,
                            pointdangerWeight, pointdangerMax, pointdangerMin, pointreasonSum, pointdangerSum, pointreason, pointdanger, bigo1, bigo2, bigo3, memo, fileurl, answer, code, shownedanswer, odx, enabled);

                        if (r < 0)
                        {
                            cnt = 0;
                            tran.Rollback();
                            con.Close();
                            return;
                        }
                        else
                        {
                            cnt++;
                            _Worker.ReportProgress(cnt);
                        }
                    }
                    else
                        cnt = 0;
                });

                if (con.State != ConnectionState.Open)
                    return;

                tran.Commit();
                con.Close();

                //날짜 데이터 리프레쉬
                cboDate.SelectedIndexChanged -= cboDate_SelectedIndexChanged;

                DataTable dt = Biz.Instance.SEALZONE_SaupjaDateList(_SaupjaNum);

                args.Result = dt;
            };
            _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                waitingBar.Text = args.ProgressPercentage.ToString() + "/" + grdPyeongga.RowCount.ToString();
            };
            _Worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    cboDate.DataSource = (DataTable)args.Result;
                    cboDate.DisplayMember = "Date";
                    cboDate.Text = date;

                    Biz.Show(this, "저장에 성공했습니다.");

                    cboDate.SelectedIndexChanged += cboDate_SelectedIndexChanged;
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

        async void SetSaupjaList()
        {
            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = grdSaupja.Parent;
            _Worker = new AbortableBackgroundWorker();

            _Worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                string year = cboYear.Text;

                string searchtext = txtSaupjaName.Text.Trim();

                DataTable dt = Biz.Instance.SEALZONE_SaupjaList(year, searchtext);

                args.Result = dt;
            };
            _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
            };
            _Worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    grdSaupja.DataSource = (DataTable)args.Result;
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

        public UC_SealzoneDangerPyeongga()
        {
            InitializeComponent();

            Biz.Instance.SetGridViewOption(grdSaupja);
            Biz.Instance.SetGridViewOption(this.grdPyeongga);
            Biz.Instance.SetDropDownList(cboYear);
            Biz.Instance.SetDropDownList(cboDate);

            toolWindow1.AutoHideSize = new Size(500, 200);
            //this.radDock1.CloseWindow(toolWindow2);
            this.radDock1.AutoHideWindow(toolWindow1);
            radDock1.TabStripsLayout.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            //this.radDock1.DisplayWindow(toolWindow2);
            toolTabStrip1.SizeInfo.AbsoluteSize = new Size(500, 0);

            //사업장명 색상
            RadPageViewStripElement stripElement = (RadPageViewStripElement)pvMain.ViewElement;
            stripElement.Items[0].DrawFill = true;
            //stripElement.Items[0].BackColor = ColorTranslator.FromHtml("#91c930");
            stripElement.Items[0].BackColor = Color.FromArgb(0, 100, 0);
            stripElement.Items[0].ForeColor = Color.White;
            stripElement.Items[0].Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            stripElement.Items[0].GradientStyle = Telerik.WinControls.GradientStyles.Solid;

            for (int i = DateTime.Now.Year; i >= 2009; i--)
            {
                cboYear.Items.Add(i.ToString());
            }

            cboYear.SelectedIndex = 0;

            DataTable dtAnswer = new DataTable();
            dtAnswer.Columns.Add("Answer", typeof(string));
            dtAnswer.Rows.Add(new object[] { "1.예" });
            dtAnswer.Rows.Add(new object[] { "2.아니오" });
            dtAnswer.Rows.Add(new object[] { "3.모름" });

            GridViewComboBoxColumn colAnswer = grdPyeongga.Columns["clmAnswer"] as GridViewComboBoxColumn;
            colAnswer.DisplayMember = "Answer";
            colAnswer.DataSource = dtAnswer;

            grdPyeongga.EnableGrouping = true;
            grdPyeongga.ShowGroupPanel = false;
            grdPyeongga.GroupDescriptors.Clear();
            grdPyeongga.GroupDescriptors.Add("clmName1", ListSortDirection.Ascending);
            grdPyeongga.GroupDescriptors.Add("clmName2", ListSortDirection.Descending);

            grdPyeongga.AutoExpandGroups = true;

            Biz.Instance.DHCenterChanged += Instance_DHCenterChanged;

            //if(Biz.Instance.UserID != "2009112")
            {
                grdPyeongga.Columns["clmEnabled"].IsVisible = false;
                grdPyeongga.Columns["clmCode"].IsVisible = false;
                grdPyeongga.Columns["clmIdx"].IsVisible = false;
            }
        }

        //센터 변경에 의한 사업장 선태 내용 초기화
        private void Instance_DHCenterChanged(object sender, EventArgs e)
        {
            _SaupjaNum = "";
            _SaupjaName = "";
            _SaupID = -1;

            grdSaupja.DataSource = null;
            cboDate.DataSource = null;
            pvpSaupja.Text = "사업장선택";
        }

        ~UC_SealzoneDangerPyeongga()
        {
            if (_Worker != null && _Worker.IsBusy)
            {
                _Worker.CancelAsync();
                _Worker.Abort();
            }

            if (_Worker != null)
                _Worker.Dispose();
        }

        private void UC_SealzoneDangerPyeongga_Load(object sender, EventArgs e)
        {

        }

        private void radDock1_TransactionCommitted(object sender, Telerik.WinControls.UI.Docking.RadDockTransactionEventArgs e)
        {
            foreach (DockWindow window in e.Transaction.AssociatedWindows)
            {
                if (window.DockTabStrip != null)
                {
                    window.DockTabStrip.Font = new Font(this.Font.FontFamily, 11, FontStyle.Regular);
                }
            }
        }

        private void pvMain_SelectedPageChanging(object sender, Telerik.WinControls.UI.RadPageViewCancelEventArgs e)
        {
            if (e.Page == pvpSaupja)
            {
                radDock1.ShowAutoHidePopup(toolWindow1);
                e.Cancel = true;
            }
        }

        private void txtSaupjaName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (_Worker != null && _Worker.IsBusy)
                {
                    Biz.Show(this, "현재 진행중인 작업이 있어 잠시 후에 진행해 주시기 바랍니다.");
                    return;
                }
                SetSaupjaList();
            }
        }

        private void btnSaupjaSearch_Click(object sender, EventArgs e)
        {
            if (_Worker != null && _Worker.IsBusy)
            {
                Biz.Show(this, "현재 진행중인 작업이 있어 잠시 후에 진행해 주시기 바랍니다.");
                return;
            }
            SetSaupjaList();
        }

        private void grdSaupja_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column.Name == "clmSelect")
            {
                _SaupID = Convert.ToInt32(e.Row.Cells["clmSaupID"].Value);
                _SaupjaNum = e.Row.Cells["clmSaupjanum"].Value.ToString();
                _Year = e.Row.Cells["clmYear"].Value.ToString();
                _SaupjaName = e.Row.Cells["clmSaupjaName"].Value.ToString();

                pvpSaupja.Text = e.Row.Cells["clmSaupjaName"].Value.ToString();

                this.radDock1.AutoHideWindow(toolWindow1);

                SetDateList();
            }

        }

        private void grdSaupja_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            _SaupID = Convert.ToInt32(e.Row.Cells["clmSaupID"].Value);
            _SaupjaNum = e.Row.Cells["clmSaupjanum"].Value.ToString();
            _Year = e.Row.Cells["clmYear"].Value.ToString();
            _SaupjaName = e.Row.Cells["clmSaupjaName"].Value.ToString();

            pvpSaupja.Text = e.Row.Cells["clmSaupjaName"].Value.ToString();

            this.radDock1.AutoHideWindow(toolWindow1);

            SetDateList();
        }

        private void cboYear_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            _Year = cboYear.Text;
        }

        private void cboDate_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (cboDate.SelectedIndex < 0)
                return;

            try
            {
                _Date = Convert.ToDateTime(cboDate.Text).ToString("yyyy-MM-dd");
            }
            catch
            {
                Biz.Show(this, "날짜 형식에 맞지않습니다.");
                return;
            }

            txtDate.Text = _Date;

            SetDate();
        }

        private void cboDate_DataBindingComplete(object sender, ListBindingCompleteEventArgs e)
        {
            ////조사일자가 한개라도 없으면 연도를 기준으로 코드 데이터를 보여준다
            //if (_Year != "" && cboDate.Items.Count < 1)
            //{
            //    _Date = "";
            //    SetDate();
            //}
        }

        private void grdPyeongga_CellValueChanged(object sender, GridViewCellEventArgs e)
        {
            var v = e.Value?.ToString() ?? "";
            if (v == "")
                return;

            v = v.Substring(0, 1);

            var goY = e.Row.Cells["clmGoY"].Value;
            var goN = e.Row.Cells["clmGoN"].Value;
            var goU = e.Row.Cells["clmGoU"].Value;

            string grp = "";

            if (v == "1")//예
            {
                var row = grdPyeongga.Rows.Where(r => r.Cells["clmIdx"].Value.ToString() == goY.ToString());

                if (row != null && row.Count() > 0)
                {

                }
            }
            else if (v == "2")//아니오
            {

            }
            else if (v == "3")//모름
            {

            }

            SetValue();
        }

        private void grdPyeongga_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            grdPyeongga.Rows.AsParallel().ForEach(row =>
            {
                string value = row.Cells["clmAnswer"].Value?.ToString() ?? "";
                if (value != "")
                    row.Cells["clmAnswerValue"].Value = value.Substring(0, 1);
            });
        }

        private void grdPyeongga_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.Column.Name == "clmAnswer")
            {
                if ((e.Row.Cells["clmEnabled"].Value?.ToString() ?? "1") == "1")
                {
                    e.CellElement.Enabled = true;
                }
                else
                {
                    if ((e.CellElement.Value?.ToString() ?? "") != "")
                        e.CellElement.Value = ""; //속도문제로 하면 안됨
                    e.CellElement.Enabled = false;
                }

            }
            else
                e.CellElement.Enabled = true;

            if (e.Column.Name == "clmQuestion")
            {
                if ((e.Row.Cells["clmEnabled"].Value?.ToString() ?? "1") == "1")
                {
                    e.CellElement.BackColor = Color.White;
                }
                else
                {
                    e.CellElement.BackColor = Color.LightGray;
                }
            }
            else
                e.CellElement.BackColor = Color.White;

            if (e.Column.Name == "clmShownedLabel")
            {
                if (((e.Row.Cells["clmAnswer"].Value?.ToString() ?? "0") == "" ? "0" : (e.Row.Cells["clmAnswer"].Value?.ToString() ?? "0")) == (e.Row.Cells["clmShownedAnswer"].Value?.ToString()))
                    e.CellElement.ForeColor = Color.Black;
                else
                    e.CellElement.ForeColor = Color.White;
            }
            else
                e.CellElement.ForeColor = Color.Black;
        }
        //그루핑
        private void btnGrouping_Click(object sender, EventArgs e)
        {
            grdPyeongga.ShowGroupPanel = false;
            grdPyeongga.GroupDescriptors.Clear();
            grdPyeongga.GroupDescriptors.Add("clmName1", ListSortDirection.Ascending);
            grdPyeongga.GroupDescriptors.Add("clmName2", ListSortDirection.Descending);

            grdPyeongga.AutoExpandGroups = true;
        }
        //펼치기
        private void btnExtend_Click(object sender, EventArgs e)
        {
            grdPyeongga.GroupDescriptors.Clear();
        }

        private void grdPyeongga_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewTableHeaderRowInfo || e.Row is GridViewFilteringRowInfo || e.Row is GridViewHierarchyRowInfo)
                return;

            if (e.Column == null)
                return;

            if (e.Column.Name == "clmExample")
            {
                string bigo1 = e.Row.Cells["clmBigo1"].Value?.ToString() ?? "";
                string bigo2 = e.Row.Cells["clmBigo2"].Value?.ToString() ?? "";
                string url = e.Row.Cells["clmFileUrl"].Value?.ToString() ?? "";
                frmDangerSealzoneExample f = new frmDangerSealzoneExample(bigo1, bigo2, url);
                f.StartPosition = FormStartPosition.CenterParent;
                f.ShowDialog(this);
                f.Dispose();
            }
        }

        //엑셀 출력
        private void btnExcel_Click(object sender, EventArgs e)
        {
            Biz.ExportGridView(grdPyeongga);
        }

        //저장하기
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_Worker != null && _Worker.IsBusy)
            {
                Biz.Show(this, "진행중인 작업이 있어 진행할수 없습니다.\r\n작업이 종료된 후에 다시 시도해주십시오.");
                return;
            }

            try
            {
                Convert.ToDateTime(txtDate.Text);
            }
            catch
            {
                DialogResult result = Biz.Show(this, "올바른 날짜 형식이 아닙니다.조사일을 오늘로 정하고 계속 하시겠습니까?", "경고", MessageBoxButtons.YesNo);

                if (result != DialogResult.Yes)
                    return;

                txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }

            SaveData();
        }

        private void grdPyeongga_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            var editor = e.ActiveEditor;
            if (editor != null && editor is RadDropDownListEditor)
            {
                RadDropDownListEditor dropDown = (RadDropDownListEditor)editor;
                dropDown.ValueChanged -= DropDown_ValueChanged;
                dropDown.ValueChanged += DropDown_ValueChanged;
                //    RadDropDownListEditorElement element = (RadDropDownListEditorElement)dropDown.EditorElement;

            }
        }

        private void DropDown_ValueChanged(object sender, EventArgs e)
        {
            grdPyeongga.EndEdit();
        }

        //초기화
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (_SaupjaNum == "")
            {
                Biz.Show(this, "사업장을 선택해 주십시오.");
                return;
            }

            _Date = "";

            SetDate();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if(_SaupjaNum == "")
            {
                Biz.Show(this, "사업장을 선택해 주십시오.");
                return;
            }

            try
            {
                Convert.ToDateTime(txtDate.Text);
            }
            catch
            {
                Biz.Show(this, "올바른 날짜를 선택해 주십시오.");
                return;
            }

            DialogResult result = Biz.Show(this, txtDate.Text + " 데이터를 삭제하시겠습니까?", "경고", MessageBoxButtons.YesNo);

            if(result != DialogResult.Yes)
            {
                return;
            }

            DelDate();
        }
    }
}
