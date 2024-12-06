
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using System.Runtime.InteropServices;
using Telerik.WinControls.UI.Docking;
using NBOGUN.Properties;
using System.Xml.Linq;
using Telerik.WinControls;
using static NBOGUN.KihaMail;
using Telerik.WinControls.VirtualKeyboard;
using static System.Windows.Forms.AxHost;
using Telerik.Windows.Diagrams.Core;

namespace NBOGUN
{
    public partial class UC_EmotionLabor : UserControl
    {
        //string url = "https://docs.google.com/forms/d/e/1FAIpQLScXswgZt087WXx72McBQA8RTlPH0tIm6NPMM0omPohvqtY8yg/viewform?usp=pp_url&entry.1553186741=";
        const string _Url = "https://docs.google.com/forms/d/e/1FAIpQLScXswgZt087WXx72McBQA8RTlPH0tIm6NPMM0omPohvqtY8yg/viewform?usp=pp_url&entry.1553186741=";

        RadWaitingBar _wbSaupja;
        AbortableBackgroundWorker _workerSaupja;
        DataTable _DTSaupja;
        DataTable _DTSaupjaBuseo;

        int _SaupID = -1;
        string _SaupjaNum = "";
        string _Year = "";
        string _SaupjaName = "";

        [Flags]
        enum WorkerGubun { Buseo = 0, Person = 1, Phone = 2, PersonDel, ReportReady, All = Buseo | Person | ReportReady };
        WorkerGubun _WorkerGubun = WorkerGubun.All;

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

        RadRadioButton GetQ1(string Name, string Buseo)
        {
            RadRadioButton rdo = new RadRadioButton();
            rdo.Name = Name;
            rdo.Text = Buseo;
            rdo.Font = this.Font;
            rdo.ThemeName = this.office2013DarkTheme1.ThemeName;
            rdo.AutoSize = false;
            rdo.Size = new Size(180, 26);
            return rdo;
        }

        async void SetSaupjaList()
        {
            _wbSaupja.AssociatedControl = grdSaupja;
            _wbSaupja.StartWaiting();
            _wbSaupja.Text = "";

            await Task.Run(() => System.Threading.Thread.Sleep(Biz._ThreadTime));

            _workerSaupja = new AbortableBackgroundWorker();
            _workerSaupja.WorkerSupportsCancellation = true;
            _workerSaupja.WorkerReportsProgress = true;
            _workerSaupja.DoWork += _workerSaupja_DoWork;
            _workerSaupja.RunWorkerCompleted += _workerSaupja_RunWorkerCompleted;
            _workerSaupja.RunWorkerAsync();
        }

        async void RunWorker()
        {
            _wbSaupja.AssociatedControl = this;
            //if (_WorkerGubun == WorkerGubun.All)
            //    _wbSaupja.AssociatedControl = documentWindow4;
            //else if (_WorkerGubun == WorkerGubun.Buseo)
            //    _wbSaupja.AssociatedControl = pvpBuseo;

            _wbSaupja.StartWaiting();
            _wbSaupja.Text = "";

            await Task.Run(() => System.Threading.Thread.Sleep(Biz._ThreadTime));

            _workerSaupja = new AbortableBackgroundWorker();
            _workerSaupja.WorkerSupportsCancellation = true;
            _workerSaupja.WorkerReportsProgress = true;
            _workerSaupja.DoWork += _MainWorker_DoWork;
            _workerSaupja.ProgressChanged += _MainWorker_ProgressChanged;
            _workerSaupja.RunWorkerCompleted += _MainWorker_RunWorkerCompleted;
            _workerSaupja.RunWorkerAsync();
        }

        //class MyBehavior : BaseGridBehavior
        //{
        //    public override bool ProcessKeyDown(KeyEventArgs keys)
        //    {
        //        if (keys.KeyData == Keys.Enter && this.GridControl.IsInEditMode)
        //        {
        //            this.GridControl.GridNavigator.SelectNextColumn();
        //        }
        //        return true;
        //    }

        //}
        private void _MainWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (_WorkerGubun == WorkerGubun.ReportReady)
            {
                if (e.ProgressPercentage == 1)
                {
                    _wbSaupja.Text = "기존 데이터 삭제완료";
                }
                else if (e.ProgressPercentage == 2)
                {
                    _wbSaupja.Text = "기초 정보 저장";
                }

                else if (e.ProgressPercentage == 3)
                {
                    _wbSaupja.Text = "감정노동 업무 수행 현황 저장";
                }
                else if (e.ProgressPercentage == 4)
                {
                    _wbSaupja.Text = "감정노동 수준 총괄 평가 결과 저장";
                }
                else if (e.ProgressPercentage == 5)
                {
                    _wbSaupja.Text = "부서별 감정노동 평가결과 위험군 저장";
                }
                return;
            }
            if (e.ProgressPercentage == 1)
            {
            }
            else if (e.ProgressPercentage == 2)
            {
                _wbSaupja.Text = "부서 데이터 세팅";

                grdBuseo.DataSource = _DTSaupjaBuseo;

                cboBuseo.DisplayMember = "BuseoName";
                cboBuseo.ValueMember = "SeqNO";
                cboBuseo.DataSource = _DTSaupjaBuseo.Copy();

                pnG1.Controls.Clear();

                for (int i = 0; i < _DTSaupjaBuseo.Rows.Count; i++)
                {
                    pnG1.Controls.Add(GetQ1("rdoG1_" + _DTSaupjaBuseo.Rows[i]["SeqNO"].ToString(), _DTSaupjaBuseo.Rows[i]["BuseoName"].ToString()));
                }
            }
            else if (e.ProgressPercentage < 0)
            {
                Biz.Show(this, e.UserState.ToString());
                return;
            }
            else if (e.ProgressPercentage == 100)
            {
                _wbSaupja.Text = e.UserState.ToString();
                System.Threading.Thread.Sleep(100);
            }
            else if (e.ProgressPercentage == 101)
            {
                Biz.Show(this, "삭제 성공했습니다.", "알림");
            }
            else if (_WorkerGubun == WorkerGubun.ReportReady)
            {

            }

            if (_WorkerGubun == WorkerGubun.Person || _WorkerGubun == WorkerGubun.All || _WorkerGubun == WorkerGubun.PersonDel)
            {
                if(e.UserState is DataTable)
                {
                    System.Threading.Thread.Sleep(100);
                    grdPerson.DataSource = e.UserState as DataTable;
                }
            }

            if (_WorkerGubun == WorkerGubun.All && e.ProgressPercentage == 56)
            {
                System.Threading.Thread.Sleep(100);
                grdSMSSaupja.DataSource = e.UserState as DataTable;
            }
        }

        private void _MainWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_WorkerGubun == WorkerGubun.ReportReady && e.Cancelled == false)
            {
                //출력준비가 정상적으로 완료되었을때 
                string url = "https://ohis.kiha21.or.kr/mis/RD/BOGUN_Report_Html5.aspx?RPTNAME=NBOGUN_EMOTIONALLABOR.mrd&PARAM=%5b" + Biz.Instance.DHCenter
                   + "%5d%5b" + _SaupID.ToString() + "%5d%5b" + _Year + "%5d&PAGEORDER=%5b%5d";


                Biz.PrintEdge(url);
            }

            _wbSaupja.StopWaiting();

            btnPersonSave.Visible = true;
        }

        private void _MainWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (_WorkerGubun == WorkerGubun.All)
            {
                #region 구식
                //DataTable dt = Biz.Instance.EmotionLaborSaupjaJosaDateList(_SaupID);
                //_workerSaupja.ReportProgress(1, dt);
                #endregion

                if (this._SaupID < 1)
                    return;

                try
                {
                    //SetJosaDate(_Year);

                    DataTable dt = Biz.Instance.EmotionLaborSaupjaSMSList(_SaupID);
                    _workerSaupja.ReportProgress(56, dt);
                    //   btnPersonSave.Visible = true;
                    //DataTable dt = Biz.Instance.EmotionLaborPersonList(_SaupID, josadate);

                    //grdPerson.DataSource = dt;

                    //_DTSaupjaBuseo = Biz.Instance.EmotionLaborSaupjaBuseoList(_SaupID, josadate);

                    //grdBuseo.DataSource = _DTSaupjaBuseo;

                    //cboBuseo.DisplayMember = "BuseoName";
                    //cboBuseo.ValueMember = "SeqNO";
                    //cboBuseo.DataSource = _DTSaupjaBuseo.Copy();
                }
                catch
                {

                }
            }

            if (_WorkerGubun == WorkerGubun.Buseo || _WorkerGubun == WorkerGubun.All)
            {
                _DTSaupjaBuseo = Biz.Instance.EmotionLaborSaupjaBuseoList(_SaupID, _Year);

                _workerSaupja.ReportProgress(2, _DTSaupjaBuseo);
            }

           
            if (_WorkerGubun == WorkerGubun.PersonDel)
            {
                var selectedRow = from row in grdPerson.Rows
                                  where row.Cells["clmSelect"].Value != null && row.Cells["clmSelect"].Value.ToString().ToLower() == "true"
                                  select row;

                if (selectedRow?.Count() > 0)
                {
                    SqlConnection con = Biz.Instance.Connection;
                    SqlTransaction tran = con.BeginTransaction();

                    int r;
                    foreach (var row in selectedRow)
                    {
                        r = Biz.Instance.EmotionLaborPersonDel(con, tran, _SaupID, _Year, Convert.ToInt32(row.Cells["clmSeqNO"].Value));

                        if (r < 0)
                        {
                            tran.Rollback();
                            con.Close();
                            _workerSaupja.ReportProgress(-1, "근로자 삭제에 실패했습니다.");
                            _workerSaupja.CancelAsync();
                            return;
                        }
                        else
                        {
                            _workerSaupja.ReportProgress(100, row.Cells["clmName"].Value.ToString());
                        }
                    }
                    tran.Commit();
                    con.Close();

                    _workerSaupja.ReportProgress(101);

                    //근로자 리스트 리프레쉬
                }
            }

            if (_WorkerGubun == WorkerGubun.Person || _WorkerGubun == WorkerGubun.All || _WorkerGubun == WorkerGubun.PersonDel)
            {
                DataTable dt = Biz.Instance.EmotionLaborPersonList(_SaupID, _Year);

                _workerSaupja.ReportProgress(55, dt);
            }

            if (_WorkerGubun == WorkerGubun.ReportReady)
            {
                //출력 준비
                int r = Biz.Instance.EMOTIONALLABOR_RPT_Del(_SaupID, _Year);
                System.Threading.Thread.Sleep(200);
                if (r >= 0)
                    _workerSaupja.ReportProgress(1);
                else
                {
                    _workerSaupja.CancelAsync();
                    return;
                }

                r = Biz.Instance.EMOTIONALLABOR_RPT_00Save(_SaupID, _Year, _SaupjaNum, txtPrintJosaDate.Text, Biz.Instance.UserName);
                System.Threading.Thread.Sleep(200);
                if (r > 0)
                    _workerSaupja.ReportProgress(2);
                else
                {
                    _workerSaupja.CancelAsync();
                    return;
                }

                r = Biz.Instance.EMOTIONALLABOR_RPT_01Save(_SaupID, _Year);
                System.Threading.Thread.Sleep(200);
                if (r > 0)
                    _workerSaupja.ReportProgress(3);
                else
                {
                    _workerSaupja.CancelAsync();
                    return;
                }

                r = Biz.Instance.EMOTIONALLABOR_RPT_02Save(_SaupID, _Year);
                System.Threading.Thread.Sleep(200);
                if (r > 0)
                    _workerSaupja.ReportProgress(4);
                else
                {
                    _workerSaupja.CancelAsync();
                    return;
                }

                r = Biz.Instance.EMOTIONALLABOR_RPT_03Save(_SaupID, _Year);
                System.Threading.Thread.Sleep(200);
                if (r > 0)
                    _workerSaupja.ReportProgress(5);
                else
                {
                    _workerSaupja.CancelAsync();
                    return;
                }
            }
        }

        private void _workerSaupja_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            grdSaupja.DataSource = _DTSaupja;

            _wbSaupja.StopWaiting();
        }

        private void _workerSaupja_DoWork(object sender, DoWorkEventArgs e)
        {
            string year = cboYear.Text;

            string searchtext = txtSaupjaName.Text.Trim();

            _DTSaupja = Biz.Instance.EmotionLaborSaupjaList(year, searchtext);
        }

        public UC_EmotionLabor()
        {
            InitializeComponent();

            LogManager.WriteLog(this);

            //초기 페이지 세팅
            pvMain.SelectedPage = pvpPerson;

            Biz.Instance.SetGridViewOption(grdSaupja);
            Biz.Instance.SetGridViewOption(grdPerson);
            Biz.Instance.SetGridViewOption(grdPersonDetail);
            Biz.Instance.SetGridViewOption(grdBuseo);
            Biz.Instance.SetGridViewOption(grdSMSSaupja);
            Biz.Instance.SetGridViewOption(grdSMSPerson);
            Biz.Instance.SetGridViewOption(grdSMSPersonHistory);
            Biz.Instance.SetGridViewOption(grdQRJosaDate);
            Biz.Instance.SetGridViewOption(grdQRHistory);
            Biz.Instance.SetGridViewOption(grdGoogle);

            Biz.Instance.SetDropDownList(cboBuseo);

            grdPersonDetail.Columns["clmValue1"].HeaderText = "전혀" + Environment.NewLine + "그렇지" + Environment.NewLine + "않다";
            grdPersonDetail.Columns["clmValue2"].HeaderText = "그렇지" + Environment.NewLine + "않다";
            grdPersonDetail.Columns["clmValue3"].HeaderText = "그렇다";
            grdPersonDetail.Columns["clmValue4"].HeaderText = "매우" + Environment.NewLine + "그렇다";
            grdPersonDetail.Columns["clmPosition"].HeaderText = "응답" + Environment.NewLine + "위치";
            grdPersonDetail.Columns["clmPoint"].HeaderText = "실제" + Environment.NewLine + "점수";

            //this.radDock1.AutoHideWindow(toolWindow1);
            //toolWindow1.DockState = Telerik.WinControls.UI.Docking.DockState.Hidden;
            toolWindow1.AutoHideSize = new Size(500, 200);
            //this.radDock1.CloseWindow(toolWindow2);
            this.radDock1.AutoHideWindow(toolWindow1);
            radDock1.TabStripsLayout.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            //this.radDock1.DisplayWindow(toolWindow2);
            toolTabStrip1.SizeInfo.AbsoluteSize = new Size(500, 0);

            //toolWindow1.DockState = Telerik.WinControls.UI.Docking.DockState.Hidden;

            for (int i = DateTime.Now.Year + 1; i >= 2009; i--)
            {
                cboYear.Items.Add(i.ToString());
            }

            btnGDate1.PerformClick();

            cboYear.Text = DateTime.Now.Year.ToString();

            _wbSaupja = Biz.Instance.SetWaitingBar();

            //사업장명 색상
            RadPageViewStripElement stripElement = (RadPageViewStripElement)pvMain.ViewElement;
            stripElement.Items[0].DrawFill = true;
            //stripElement.Items[0].BackColor = ColorTranslator.FromHtml("#91c930");
            stripElement.Items[0].BackColor = Color.FromArgb(0, 100, 0);
            stripElement.Items[0].ForeColor = Color.White;
            stripElement.Items[0].Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            stripElement.Items[0].GradientStyle = Telerik.WinControls.GradientStyles.Solid;

            txtSMSPersonYear.Text = DateTime.Now.Year.ToString();
            txtQRJosaDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dtpQRJosaDate.Value = DateTime.Now;
            dtpJosaDate.Value = DateTime.Now;

            grdSMSPersonHistory.Size = new Size(634, 646);

            grdQRHistory.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            grdSMSPerson.Anchor = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left;
            grdSMSPersonHistory.Anchor = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
            txtGSearch.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            //Biz.Instance.DHCenterChanged += Instance_DHCenterChanged;
            //grdPersonDetail.GridBehavior = new MyBehavior();
        }

        ~UC_EmotionLabor()
        {
            if (_workerSaupja != null && _workerSaupja.IsBusy == true)
            {
                _workerSaupja.Abort();
                _workerSaupja.Dispose();
            }
        }
        private void UC_EmotionLabor_Load(object sender, EventArgs e)
        {
            //toolWindow1.DockState = Telerik.WinControls.UI.Docking.DockState.AutoHide;
            //webView21.CoreWebView2InitializationCompleted += WebView21_CoreWebView2InitializationCompleted;

            //await InitializeAsync();
        }

        private void documentTabStrip1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void txtSaupjaName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SetSaupjaList();
            }
        }

        private void btnSaupjaSearch_Click(object sender, EventArgs e)
        {
            SetSaupjaList();
        }

        private void cboYear_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {

            }
            catch
            {

            }
        }

        private void grdSaupja_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewTableHeaderRowInfo || e.Row is GridViewFilteringRowInfo)
                return;

            if (e.Column.Name == "clmSelect")
            {
                _SaupID = Convert.ToInt32(e.Row.Cells["clmSaupID"].Value);
                _SaupjaNum = e.Row.Cells["clmSaupjanum"].Value.ToString();
                _Year = e.Row.Cells["clmYear"].Value.ToString();
                _SaupjaName = e.Row.Cells["clmSaupjaName"].Value.ToString();

                if (e.Row.Cells["clmGUID"].Value?.ToString() == "")
                {
                    DataTable dtGUID = Biz.Instance.EMOTIONALLABOR_SaupjaGUID(_SaupID, _Year, _SaupjaNum);

                    e.Row.Cells["clmGUID"].Value = dtGUID.Rows[0]["GUID"].ToString();

                    //string shorturl = Biz.ShortUrl(_Url + dtGUID.Rows[0]["GUID"].ToString());

                    //txtUrl.Text = shorturl;
                }
                else
                {
                    //string shorturl = Biz.ShortUrl(_Url + e.Row.Cells["clmGUID"].Value?.ToString());

                    //txtUrl.Text = shorturl;
                }

                pvpSaupja.Text = e.Row.Cells["clmSaupjaName"].Value.ToString() + " (" + _Year + ")";

                txtPrintJosaDate.Text = e.Row.Cells["clmJosaDate"].Value.ToString();

                this.radDock1.AutoHideWindow(toolWindow1);
                //this.radDock1.AutoHideWindow(toolWindow2);
                ClearPersonData();

                _WorkerGubun = WorkerGubun.All;

                RunWorker();
            }
        }

        private void grdSaupja_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewTableHeaderRowInfo || e.Row is GridViewFilteringRowInfo)
                return;

            _SaupID = Convert.ToInt32(e.Row.Cells["clmSaupID"].Value);
            _SaupjaNum = e.Row.Cells["clmSaupjanum"].Value.ToString();
            _Year = e.Row.Cells["clmYear"].Value.ToString();
            _SaupjaName = e.Row.Cells["clmSaupjaName"].Value.ToString();

            if (e.Row.Cells["clmGUID"].Value?.ToString() == "")
            {
                DataTable dtGUID = Biz.Instance.EMOTIONALLABOR_SaupjaGUID(_SaupID, _Year, _SaupjaNum);

                e.Row.Cells["clmGUID"].Value = dtGUID.Rows[0]["GUID"].ToString();

                //string shorturl = Biz.ShortUrl(_Url + dtGUID.Rows[0]["GUID"].ToString());

                //txtUrl.Text = shorturl;
            }

            pvpSaupja.Text = e.Row.Cells["clmSaupjaName"].Value.ToString() + " (" + _Year + ")";
            txtPrintJosaDate.Text = e.Row.Cells["clmJosaDate"].Value.ToString();
            //this.radDock1.CloseWindow(toolWindow1);
            this.radDock1.AutoHideWindow(toolWindow1);

            _WorkerGubun = WorkerGubun.All;

            RunWorker();
        }

        private void pvMain_SelectedPageChanging(object sender, RadPageViewCancelEventArgs e)
        {
            if (e.Page == pvpSaupja)
            {
                radDock1.ShowAutoHidePopup(toolWindow1);
                e.Cancel = true;
            }
        }

        #region Buseo
        private void grdBuseo_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewTableHeaderRowInfo || e.Row is GridViewFilteringRowInfo)
                return;

            if (e.Column.Name == "clmDel")
            {
                grdBuseo.Rows.Remove(e.Row);

                _DTSaupjaBuseo.AcceptChanges();
            }
        }

        private void grdBuseo_CreateRow(object sender, GridViewCreateRowEventArgs e)
        {
            e.RowInfo.Cells["clmDel"].Value = Resources.add_remove;
        }

        private void btnBuseoClear_Click(object sender, EventArgs e)
        {
            _DTSaupjaBuseo.Rows.Clear();
            _DTSaupjaBuseo.AcceptChanges();
            grdBuseo.DataSource = _DTSaupjaBuseo;
        }

        private void btnBuseoAdd_Click(object sender, EventArgs e)
        {
            if (_DTSaupjaBuseo != null)
            {
                var max = _DTSaupjaBuseo.Compute("MAX([SeqNO])", string.Empty);

                int minLavel = Convert.ToInt32((max == null || max.ToString() == "") ? 0 : max) + 1;

                DataRow row = _DTSaupjaBuseo.NewRow();
                row["SeqNO"] = minLavel;
                row["BuseoName"] = "";
                _DTSaupjaBuseo.Rows.Add(row);
            }
        }

        private void btnBuseoRefresh_Click(object sender, EventArgs e)
        {
            _WorkerGubun = WorkerGubun.Buseo;

            RunWorker();
        }

        private void btnBuseoSave_Click(object sender, EventArgs e)
        {
            if (_SaupID < 0)
            {
                Biz.Show(this, "사업장을 선택해 주십시오.", "경고");
                return;
            }

            int r;

            SqlConnection con = Biz.Instance.Connection;
            SqlTransaction tran = con.BeginTransaction();

            r = Biz.Instance.EmotionLaborSaupjaBuseoDel(con, tran, _SaupID, _Year);

            if (r < 0)
            {
                tran.Rollback();

                con.Close();

                Biz.Show(this, "저장전 부서 초기화에 실패했습니다.", "오류");

                return;
            }

            int seqno = -1;

            for (int i = 0; i < grdBuseo.Rows.Count; i++)
            {
                try
                {
                    seqno = Convert.ToInt32(grdBuseo.Rows[i].Cells["clmSeqNO"].Value);
                }
                catch
                {
                    seqno = -1;
                }

                r = Biz.Instance.EmotionLaborSaupjaBuseoSave(con, tran, _SaupID, _Year, seqno, grdBuseo.Rows[i].Cells["clmBuseoName"].Value.ToString().Trim(),
                        grdBuseo.Rows[i].Cells["clmGubun"].Value.ToString().Trim(),
                        grdBuseo.Rows[i].Cells["clmClient"].Value.ToString().Trim(),
                        grdBuseo.Rows[i].Cells["clmHire"].Value.ToString().Trim(),
                        grdBuseo.Rows[i].Cells["clmInwon"].Value.ToString().Trim(),
                        grdBuseo.Rows[i].Cells["clmUpmu"].Value.ToString().Trim()
                    );

                if (r < 0)
                {
                    tran.Rollback();

                    con.Close();

                    Biz.Show(this, "부서 초기화후 저장에 실패했습니다.", "오류");

                    return;
                }
            }

            tran.Commit();

            con.Close();

            _DTSaupjaBuseo = Biz.Instance.EmotionLaborSaupjaBuseoList(_SaupID, _Year);

            cboBuseo.DataSource = _DTSaupjaBuseo.Copy();

            pnG1.Controls.Clear();

            for (int i = 0; i < _DTSaupjaBuseo.Rows.Count; i++)
            {
                pnG1.Controls.Add(GetQ1("rdoG1_" + (i + 1).ToString(), _DTSaupjaBuseo.Rows[i]["BuseoName"].ToString()));
            }

            Biz.Show(this, "저장에 성공했습니다.", "알림");
        }

        void SetJosaDate(string josadate)
        {
            ClearPersonData();

            //DataTable dt = Biz.Instance.EmotionLaborPersonList(_SaupID, josadate);

            //grdPerson.DataSource = dt;

            //_DTSaupjaBuseo = Biz.Instance.EmotionLaborSaupjaBuseoList(_SaupID, josadate);

            //grdBuseo.DataSource = _DTSaupjaBuseo;

            //cboBuseo.DisplayMember = "BuseoName";
            //cboBuseo.ValueMember = "SeqNO";
            //cboBuseo.DataSource = _DTSaupjaBuseo.Copy();

            //pnG1.Controls.Clear();

            //for (int i = 0; i < _DTSaupjaBuseo.Rows.Count; i++)
            //{
            //    pnG1.Controls.Add(GetQ1("rdoG1_" + (i + 1).ToString(), _DTSaupjaBuseo.Rows[i]["BuseoName"].ToString()));
            //}
        }
        #endregion

        #region Person
        private void btnPersonDelChecked_Click(object sender, EventArgs e)
        {
            if (_SaupID < 1)
            {
                Biz.Show(this, "사업장을 선택해 주십시오", "경고");
                return;
            }

            int delCnt = 0;

            bool isSelect = false;

            List<GridViewRowInfo> rows = new List<GridViewRowInfo>();

            for (int i = 0; i < grdPerson.Rows.Count; i++)
            {
                var select = grdPerson.Rows[i].Cells["clmSelect"].Value;

                try
                {
                    isSelect = Convert.ToBoolean(select);
                }
                catch
                {
                    isSelect = false;
                }

                if (isSelect)
                {
                    rows.Add(grdPerson.Rows[i]);
                    delCnt = delCnt + 1;
                }

            }

            if (delCnt == 0)
                return;

            DialogResult dialogResult = Biz.Show(delCnt.ToString() + "명의 근로자를 삭제하시겠습니까?", "주의", MessageBoxButtons.OKCancel);

            if (dialogResult != DialogResult.OK)
            {
                return;
            }

            this._wbSaupja.Text = "";
            _WorkerGubun = WorkerGubun.PersonDel;

            RunWorker();
            //선택한 근로자의 SeqNO
            //PersonDelAsync();
        }

        private void btnPersonRefresh_Click(object sender, EventArgs e)
        {
            if (_SaupID < 0)
                return;

            _WorkerGubun = WorkerGubun.Person;

            RunWorker();
        }
        private void btnPersonAdd_Click(object sender, EventArgs e)
        {
            ClearPersonData();
        }

        void ClearPersonData()
        {
            //txtPersonSeqNO.ReadOnly = false;
            txtPersonSeqNO.Text = "";
            txtPersonSeqNO.Text = "-1";
            //txtPersonSeqNO.ReadOnly = true;
            txtPersonName.Text = "";
            rdoM.CheckState = CheckState.Checked;
            //txtPersonJosaDate.Text = cboPersonJosaDate.Text;

            for (int i = 0; i < pnG1.Controls.Count; i++)
            {
                if (pnG1.Controls[i] is RadRadioButton)
                {
                    ((RadRadioButton)pnG1.Controls[i]).IsChecked = false;
                }
            }

            for (int i = 0; i < grdPersonDetail.Rows.Count; i++)
            {
                grdPersonDetail.Rows[i].Cells["clmPosition"].Value = "";
                grdPersonDetail.Rows[i].Cells["clmPoint"].Value = 0;
            }

            DataTable dt = Biz.Instance.EmotionLaborPersonDetail(_SaupID, _Year, -1);

            grdPersonDetail.DataSource = dt;

            grdPerson.CurrentRow = null;
        }

        private void grdPersonDetail_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            RadTextBoxEditor editor = e.ActiveEditor as RadTextBoxEditor;

            if (editor != null)
            {
                _SelectedPostionRowInfo = e.Row;

                editor.ValueChanging -= Editor_ValueChanging;
                editor.ValueChanging += Editor_ValueChanging;
            }
        }

        GridViewRowInfo _SelectedPostionRowInfo;
        int GetG1()
        {
            RadRadioButton rdo;

            for (int i = 0; i < pnG1.Controls.Count; i++)
            {
                if (pnG1.Controls[i] is RadRadioButton)
                {
                    rdo = pnG1.Controls[i] as RadRadioButton;

                    if (rdo.IsChecked == true)
                        return Convert.ToInt32(rdo.Name.Replace("rdoG1_", ""));
                }
            }

            return 0;
        }

        private void Editor_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            if (_SelectedPostionRowInfo != null)
            {
                try
                {
                    _SelectedPostionRowInfo.Cells["clmPoint"].Value = _SelectedPostionRowInfo.Cells["clmValue" + e.NewValue.ToString()].Value;
                }
                catch
                {
                    _SelectedPostionRowInfo.Cells["clmPoint"].Value = 0;
                }

            }
            //_SelectedPostionEditor
            //e.NewValue
        }

        private void btnPersonDel_Click(object sender, EventArgs e)
        {
            if (_SaupID < 1)
            {
                Biz.Show(this, "사업장을 선택해 주십시오", "경고");
                return;
            }

            DialogResult res;
            int seqno = -1;
            int r = -1;

            try
            {
                seqno = Convert.ToInt32(txtPersonSeqNO.Text);
            }
            catch
            {
                res = Biz.Show(this, "올바른 일련번호가 아닙니다. 근로자 내용을 초기화하시겠습니까?", "경고", MessageBoxButtons.OKCancel);

                if (res == DialogResult.OK)
                {
                    ClearPersonData();
                }
                else
                    return;
            }

            res = Biz.Show(this, "근로자 정보를 삭제하시겠습니까?", "경고", MessageBoxButtons.OKCancel);

            if (res != DialogResult.OK)
                return;

            SqlConnection con = Biz.Instance.Connection;
            SqlTransaction tran = con.BeginTransaction();

            r = Biz.Instance.EmotionLaborPersonDel(con, tran, _SaupID, _Year, seqno);

            if (r < 0)
            {
                tran.Rollback();
                con.Close();
                Biz.Show(this, "근로자 삭제에 실패했습니다.", "경고");
                return;
            }
            else
            {
                grdPerson.Rows.Remove(grdPerson.CurrentRow);
            }

            tran.Commit();
            con.Close();

            ClearPersonData();
        }

        private void btnPersonSave_Click(object sender, EventArgs e)
        {
            if (_SaupID < 1)
            {
                Biz.Show(this, "사업장을 선택해 주십시오", "경고");
                return;
            }

            int seqno = -1;
            int r = -1;

            try
            {
                seqno = Convert.ToInt32(txtPersonSeqNO.Text);
            }
            catch
            {
            }

            int g1 = GetG1();

            if (g1 == 0)
            {
                Biz.Show(this, "부서를 선택해 주십시오.", "경고");
                return;
            }

            if (!rdoM.IsChecked && !rdoW.IsChecked)
            {
                Biz.Show(this, "성별을 선택해 주십시오", "경고");
                return;
            }

            SqlConnection con = Biz.Instance.Connection;
            SqlTransaction tran = con.BeginTransaction();

            string name = txtPersonName.Text.Trim();
            string sex = rdoM.IsChecked == true ? "남" : "여";

            DataTable dtSeqNO = Biz.Instance.EmotionLaborPersonSave(con, tran, _SaupID, _SaupjaNum, _Year, seqno, name, sex, g1);

            if (dtSeqNO == null || dtSeqNO.Rows.Count != 1)
            {
                Biz.Show(this, "일련번호 생성에 실패했습니다.", "오류");
                tran.Rollback();
                con.Close();
                return;
            }
            else
            {
                seqno = Convert.ToInt32(dtSeqNO.Rows[0]["SeqNO"]);
            }

            string code = "";
            int point;
            for (int i = 0; i < grdPersonDetail.Rows.Count; i++)
            {
                code = grdPersonDetail.Rows[i].Cells["clmCode"].Value.ToString();
                try
                {
                    point = Convert.ToInt32(grdPersonDetail.Rows[i].Cells["clmPosition"].Value);
                }
                catch
                {
                    point = 0;
                }

                r = Biz.Instance.EmotionLaborPersonDetailSave(con, tran, _SaupID, _Year, seqno, code, point.ToString());

                if (r < 0)
                {
                    tran.Rollback();
                    con.Close();

                    Biz.Show(code + " 마킹 저장에 실패했습니다", "오류");

                    return;
                }
            }

            tran.Commit();
            con.Close();

            var selectedRow = from row in grdPerson.Rows
                              where Convert.ToInt32(row.Cells["clmSeqNO"].Value) == seqno
                              select row;

            if (selectedRow?.Count() > 0)
            {
                ((GridViewDataRowInfo)selectedRow.First()).IsSelected = true;
                ((GridViewDataRowInfo)selectedRow.First()).Cells["clmBuseo"].Value = dtSeqNO.Rows[0]["Buseo"].ToString();
                ((GridViewDataRowInfo)selectedRow.First()).Cells["clmBuseoName"].Value = ((RadRadioButton)pnG1.Controls["rdoG1_" + ((GridViewDataRowInfo)selectedRow.First()).Cells["clmBuseo"].Value.ToString()]).Text;
            }
            else
            {
                grdPerson.Rows.Add(false, seqno, name, sex, dtSeqNO.Rows[0]["BuseoName"].ToString(), dtSeqNO.Rows[0]["Buseo"].ToString());
                grdPerson.Rows[grdPerson.RowCount - 1].IsSelected = true;
                txtPersonSeqNO.Text = seqno.ToString();
            }

            Biz.Show(this, "저장에 성공했습니다.", "알림");
            return;
        }

        async void PersonDelAsync()
        {
            int delCnt = 0;

            bool isSelect = false;

            List<GridViewRowInfo> rows = new List<GridViewRowInfo>();

            for (int i = 0; i < grdPerson.Rows.Count; i++)
            {
                var select = grdPerson.Rows[i].Cells["clmSelect"].Value;

                try
                {
                    isSelect = Convert.ToBoolean(select);
                }
                catch
                {
                    isSelect = false;
                }

                if (isSelect)
                {
                    rows.Add(grdPerson.Rows[i]);
                    delCnt = delCnt + 1;
                }

            }

            if (delCnt == 0)
                return;

            DialogResult dialogResult = Biz.Show(delCnt.ToString() + "명의 근로자를 삭제하시겠습니까?", "주의", MessageBoxButtons.OKCancel);

            if (dialogResult != DialogResult.OK)
            {
                return;
            }

            _wbSaupja.AssociatedControl = this;
            _wbSaupja.StartWaiting();
            _wbSaupja.Text = "삭제 준비중";

            await Task.Run(() => System.Threading.Thread.Sleep(100));

            //_workerPersonDel = new BackgroundWorker();
            //_workerPersonDel.WorkerSupportsCancellation = true;
            //_workerPersonDel.WorkerReportsProgress = true;
            //_workerPersonDel.DoWork += _workerPersonDel_DoWork;
            //_workerPersonDel.ProgressChanged += _workerPersonDel_ProgressChanged;
            //_workerPersonDel.RunWorkerCompleted += _workerPersonDel_RunWorkerCompleted;
            //_workerPersonDel.RunWorkerAsync(rows);
        }

        private void grdPerson_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewTableHeaderRowInfo)
            {
                if (e.Column.Name == "clmSelect")
                {
                    if (e.Row is GridViewTableHeaderRowInfo && e.Column.Name == "clmSelect")
                    {
                        bool isCheck = false;

                        //string name = ((Telerik.WinControls.UI.GridViewGroupRowInfo)e.Row).Group.Header;
                        GridDataView grd = grdPerson.MasterTemplate.DataView as GridDataView;

                        if (grd.ItemCount == grdPerson.Rows.Count)
                        {
                            for (int i = 0; i < grdPerson.Rows.Count; i++)
                            {
                                if (Convert.ToString(grdPerson.Rows[i].Cells["clmSelect"].Value).ToLower() == "true")
                                {
                                    isCheck = true;
                                    break;
                                }
                            }

                            //갯수가 같다면 필터링 없음
                            for (int i = 0; i < grdPerson.Rows.Count; i++)
                            {
                                if (((GridViewGroupRowInfo)grdPerson.Rows[i].Parent == null || (GridViewGroupRowInfo)grdPerson.Rows[i].Parent != null &&
                                    ((GridViewGroupRowInfo)grdPerson.Rows[i].Parent).IsExpanded == true) && grdPerson.Rows[i].Height < 1)
                                    grdPerson.Rows[i].Cells["clmSelect"].Value = !isCheck;
                                else
                                    grdPerson.Rows[i].Cells["clmSelect"].Value = false;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < grd.Count; i++)
                            {
                                if (Convert.ToString(grd[i].Cells["clmSelect"].Value).ToLower() == "true")
                                {
                                    isCheck = true;
                                    break;
                                }
                            }

                            for (int i = 0; i < grdPerson.Rows.Count; i++)
                            {
                                grdPerson.Rows[i].Cells["clmSelect"].Value = false;
                            }

                            //갯수가 다르다면 필터링중
                            foreach (GridViewRowInfo row in grd.Indexer.Items)
                            {
                                row.Cells["clmSelect"].Value = !isCheck;
                            }
                        }

                        return;
                    }
                }
            }

            if (e.Row is GridViewFilteringRowInfo) //|| e.Row is GridViewFilteringRowInfo)
            {
                return;
            }

            if (_SaupID < 0)
                return;

            if (e.Column.Name == "clmSelect")
                return;

            int seqno = -1;
            int buseo = -1;

            try
            {
                seqno = Convert.ToInt32(e.Row.Cells["clmSeqNO"].Value);

                txtPersonSeqNO.Text = seqno.ToString();
            }
            catch
            {

            }

            try
            {
                buseo = Convert.ToInt32(e.Row.Cells["clmBuseo"].Value);
            }
            catch
            {

            }

            try
            {
                ((RadRadioButton)pnG1.Controls["rdoG1_" + buseo.ToString()]).IsChecked = true;
            }
            catch
            {
            }

            txtPersonName.Text = e.Row.Cells["clmName"].Value.ToString();
            rdoM.CheckState = e.Row.Cells["clmSex"].Value?.ToString() == "남" ? CheckState.Checked : CheckState.Unchecked;
            rdoW.CheckState = e.Row.Cells["clmSex"].Value?.ToString() == "여" ? CheckState.Checked : CheckState.Unchecked;

            DataTable dt = Biz.Instance.EmotionLaborPersonDetail(_SaupID, _Year, seqno);

            grdPersonDetail.DataSource = dt;
        }

        #endregion


        private void grdPersonDetail_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewTableHeaderRowInfo)
                return;

            if (e.Column.Name.Contains("clmValue"))
            {
                e.Row.Cells["clmPoint"].Value = e.Row.Cells[e.ColumnIndex].Value;//점수
                e.Row.Cells["clmPosition"].Value = e.Column.Name.Substring(8, 1);// 위치
            }
        }

        private void btnPersonBuseoChange_Click(object sender, EventArgs e)
        {
            if (_SaupID < 0)
                return;

            var selectedRow = from row in grdPerson.Rows
                              where row.Cells["clmSelect"].Value != null && row.Cells["clmSelect"].Value.ToString().ToLower() == "true"
                              select row;

            if (selectedRow?.Count() > 0)
            {
                int r;
                SqlConnection con = Biz.Instance.Connection;
                SqlTransaction tran = con.BeginTransaction();

                foreach (var row in selectedRow)
                {
                    r = Biz.Instance.EmotionLaborPersonBuseoChange(con, tran, _SaupID, _Year, Convert.ToInt32(row.Cells["clmSeqNO"].Value), Convert.ToInt32(cboBuseo.SelectedValue));

                    if (r < 0)
                    {
                        tran.Rollback();
                        con.Close();

                        Biz.Show(this, "변경 중 오류가 발생했습니다.", "경고");
                        return;
                    }
                }

                foreach (var row in selectedRow)
                {
                    row.Cells["clmBuseoName"].Value = cboBuseo.Text;
                    row.Cells["clmBuseo"].Value = cboBuseo.SelectedValue;
                }

                tran.Commit();
                con.Close();

            }
            else
            {
                Biz.Show(this, "선택된 근로자가 없습니다.", "경고");
                return;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (_SaupID < 0)
            {
                Biz.Show(this, "사업장을 선택해 주십시오.", "경고");
                return;
            }

            if (_workerSaupja != null && _workerSaupja.IsBusy == true)
            {
                Biz.Show(this, "진행중인 프로세스가 있어 출력할 수 없습니다.", "알림");
                return;
            }

            _WorkerGubun = WorkerGubun.ReportReady;

            RunWorker();




            //NBOGUN_EMOTIONALLABOR.
            //string url = Biz.
        }

        private void grdBuseo_Click(object sender, EventArgs e)
        {

        }

        private void cboPersonJosaDate_PopupClosed(object sender, RadPopupClosedEventArgs args)
        {

        }

        private void grdSaupja_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (grdSaupja.CurrentRow != null)
                {
                    _SaupID = Convert.ToInt32(grdSaupja.CurrentRow.Cells["clmSaupID"].Value);
                    _SaupjaNum = grdSaupja.CurrentRow.Cells["clmSaupjanum"].Value.ToString();
                    _Year = grdSaupja.CurrentRow.Cells["clmYear"].Value.ToString();

                    pvpSaupja.Text = grdSaupja.CurrentRow.Cells["clmSaupjaName"].Value.ToString() + " (" + _Year + ")";

                    //this.radDock1.CloseWindow(toolWindow1);
                    this.radDock1.AutoHideWindow(toolWindow1);

                    _WorkerGubun = WorkerGubun.All;

                    RunWorker();
                }
            }
        }

        private void btnSMSSave_Click(object sender, EventArgs e)
        {
            if (txtSMS.Text.Trim() == "")
            {
                Biz.Show(this, "내용을 입력해 주십시오.", "경고");
                return;
            }

            //if (txtSMS.Text.Contains(txtUrl.Text) == false)
            //{
            //    Biz.Show(this, "상단의 URL의 우측 버튼을 클릭하여 해당 URL을 문자에 포함 시켜야 합니다.", "경고");
            //    return;
            //}

            int r = Biz.Instance.EmotionLaborSaupjaSMSSave(_SaupID, txtSMS.Text.Trim());

            if (r < 0)
            {
                Biz.Show(this, Biz.Instance.Error + "저장에 실패했습니다.", "오류");
                return;
            }

            grdSMSSaupja.DataSource = Biz.Instance.EmotionLaborSaupjaSMSList(_SaupID);
        }


        private void btnUrlCopy_Click(object sender, EventArgs e)
        {
            try
            {
                //Clipboard.SetText(txtUrl.Text);
            }
            catch
            {

            }
            //string url = Biz.ShortUrl("https://docs.google.com/forms/d/e/1FAIpQLSfK7dJHazFFQZuEnw5KAMNv0pTQIBiH_xDiSQenrF10HQrMHA/viewform?usp=sf_link");

            //Biz.UrlStart(url);
        }

        private void grdSMSCenter_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewTableHeaderRowInfo)
            {
                return;
            }

            if (e.Column.Name == "clmDel")
            {
                int seqno;

                try
                {
                    seqno = Convert.ToInt32(e.Row.Cells["clmSeqNO"].Value);
                }
                catch
                {
                    Biz.Show(this, "일련번호를 찾을 수 없어 삭제 할 수 없습니다.", "알림");
                    return;
                }
                int r = Biz.Instance.EMOTIONALLABOR_SaupjaSMSDel(_SaupID, seqno);

                if (r < 0)
                {
                    Biz.Show(this, "삭제중 오류가 발생하여 삭제 할 수 없습니다.", "알림");
                    return;
                }
                else if (r < 1)
                {
                    Biz.Show(this, "삭제 할 데이터가 없습니다.", "알림");
                    return;
                }
                else
                    grdSMSSaupja.DataSource = Biz.Instance.EmotionLaborSaupjaSMSList(_SaupID);
            }
        }

        private void grdSMSCenter_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewTableHeaderRowInfo)
            {
                return;
            }

            txtSMS.Text = e.Row.Cells["clmSMS"].Value.ToString();
        }

        //엑셀 내용의 근로자를 붙여넣기
        async private void btnSMSExcelPaste_Click(object sender, EventArgs e)
        {
            if (_Year == "")
            {
                Biz.Show(this, "연도를 선택해 주십시오.", "알림");
                return;
            }

            if (_SaupID < 1)
            {
                Biz.Show(this, "사업장을 선택해 주십시오.", "알림");
                return;
            }

            string str = Clipboard.GetText();

            if (str == "")
            {
                Biz.Show(this, "붙여넣기 할 내용이 없습니다.", "알림");

                return;
            }

            string[] rows = str.Split(System.Environment.NewLine.ToCharArray());
            string[] cols;

            string name = "";
            string phone = "";
            string buseo = "";
            string date = "";

            int r = -1;

            int Cnt = 0;

            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                SqlConnection con = Biz.Instance.Connection;
                SqlTransaction tran = con.BeginTransaction();
                //System.Collections.Generic.List l = new System.Collections.Generic.List();
                Parallel.ForEach(rows, row =>
                {
                    cols = row.Split('\t');
                    if (cols.Length > 1)
                    {
                        name = cols[0].Replace("\t", "").Replace("\r\n", "");
                        phone = cols[1].Replace("\t", "").Replace("\r\n", "");
                        buseo = cols[2].Replace("\t", "").Replace("\r\n", "");

                        if (cols.Length == 4)
                        {
                            date = cols[3].Replace("\t", "").Replace("\r\n", "");
                            try
                            {
                                date = Convert.ToDateTime(date).ToString("yyyy-MM-dd");
                            }
                            catch
                            {

                            }
                        }

                        r = Biz.Instance.EMOTIONALLABOR_SMSPersonSave(con, tran, _SaupjaNum, name, phone, buseo, date);

                        //r = Biz.Instance.JikmustressGoogleSuveySave(con, tran, phone, name, buseo, _SaupjaNum, date);

                        if (r < 0)
                        {
                            Cnt = -1;

                            tran.Rollback();

                            con.Close();

                            //Biz.Show(this, "연락처 저장에 실패했습니다.", "오류");

                            return;
                        }
                        else
                            worker.ReportProgress(-1, name + " : " + phone);

                        Cnt++;
                    }

                });

                tran.Commit();

                con.Close();

                //args.Result = "성공";
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                try
                {
                    if (args.ProgressPercentage < 0)
                    {
                        waitingBar.Text = args.UserState?.ToString();
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog(ex.Message);
                }

            };
            worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (Cnt < 0)
                {
                    Biz.Show(this, "연락처 저장에 실패했습니다.", "오류");
                }
                else
                {
                    Biz.Show(this, Cnt.ToString() + "건의 데이터를 저장하는데 성공했습니다.");
                }

                waitingBar.StopWaiting();
            };
            waitingBar.StartWaiting();
            await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
            BeginInvoke(new MethodInvoker(worker.RunWorkerAsync));
        }

        private void btnSMSHistoryLoad_Click(object sender, EventArgs e)
        {

        }

        async void SetSaupjaSMSList()
        {
            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                DataTable mTableSite = Biz.Instance.EmotionLaborSaupjaSMSList(_SaupID);

                args.Result = mTableSite;
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                try
                {
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog(ex.Message);
                }

            };
            worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    grdSMSSaupja.DataSource = (DataTable)args.Result;
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

        private void btnSMSSaupja_Click(object sender, EventArgs e)
        {
            SetSaupjaSMSList();
        }

        public class GUIDList
        {
            public string JosaDate;
            public string GUID;
        }

        //문자발송
        async private void btnSMSPersonSend_Click(object sender, EventArgs e)
        {
            if (_SaupID < 1)
            {
                Biz.Show(this, "사업장을 선택해 주십시오.", "알림");
                return;
            }

            var rows = grdSMSPerson.Rows.Where(x => x.Cells["clmIsSelect"].Value != null && (x.Cells["clmIsSelect"].Value.ToString().ToLower() == "on" || x.Cells["clmIsSelect"].Value.ToString().ToLower() == "true"));

            if (rows.Count() < 1)
            {
                Biz.Show(this, "근로자를 선택해 주십시오.");
                return;
            }

            string sms = "";

            //string josadate = dtpJosaDate.Value.ToString("yyyy-MM-dd");

            //DataTable dtGUID;
            //string josaguid;

            List<GUIDList> list = new List<GUIDList>();
            var guidrow = from row in rows
                          group row by row.Cells["clmJosaDate"].Value.ToString() into g
                          select g;

            guidrow.AsParallel().ForEach(row => {
                GUIDList gUIDList = new GUIDList();
                gUIDList.JosaDate = row.Key.ToString();
                gUIDList.GUID = Biz.Instance.EMOTIONALLABOR_SaupjaGUID(_SaupID, gUIDList.JosaDate, _SaupjaNum).Rows[0]["GUID"].ToString();
                list.Add(gUIDList);
            });

            int r = -1;
            string url = "https://docs.google.com/forms/d/e/1FAIpQLScXswgZt087WXx72McBQA8RTlPH0tIm6NPMM0omPohvqtY8yg/viewform?usp=pp_url&entry.1553186741=";

            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            int Cnt = 0;
            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                //문구 가져오기
                sms = Biz.Instance.EmotionLaborSaupjaSMSList(_SaupID, "1").Rows[0]["SMS"].ToString();

                //GUID 가져오기
                //dtGUID = Biz.Instance.EMOTIONALLABOR_SaupjaGUID(_SaupID, josadate, _SaupjaNum);

                //josaguid = dtGUID.Rows[0]["GUID"].ToString();
                //url = sms + Environment.NewLine + Environment.NewLine + url + josaguid;

                SqlConnection con = Biz.Instance.Connection;
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlTransaction tran = con.BeginTransaction();

                rows.AsParallel().ForEach(row => {
                    string name = row.Cells["clmName"].Value.ToString();
                    string phone = row.Cells["clmPhone"].Value.ToString();
                    string buseo = row.Cells["clmBuseo"].Value.ToString();
                    string josadate = row.Cells["clmJosaDate"].Value.ToString();
                    GUIDList guid = list.FindLast(item => item.JosaDate == josadate);
                    string url1 = sms + Environment.NewLine + Environment.NewLine + url + guid.GUID;
                    r = Biz.Instance.MSG_Send(DateTime.Now.AddMinutes(1).ToString("yyyy-MM-dd hh:mm:ss"), phone, Biz.Instance.CenterPhone,
                        "대한산업보건협회 감정노동평가 설문", url1);

                    //if (con.State != ConnectionState.Open)
                    //    return;
                    //else 
                    if (r < 0)
                    {
                        Cnt = -1;
                        tran.Rollback();
                        con.Close();
                        return;
                    }
                    else
                        Cnt++;

                    r = Biz.Instance.EMOTIONALLABOR_SMSHistorySave(con, tran, phone, _SaupID, _SaupjaNum, _SaupjaName, name, buseo, url1, josadate);

                    //if (con.State != ConnectionState.Open)
                    //    return;
                    //else 
                    if (r < 0)
                    {
                        //Cnt = -1;
                        tran.Rollback();
                        con.Close();
                        return;
                    }

                    worker.ReportProgress(-1, name + " : " + phone);
                });

                tran.Commit();
                con.Close();
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                try
                {
                    if (args.ProgressPercentage < 0)
                    {
                        waitingBar.Text = args.UserState?.ToString();
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog(ex.Message);
                }

            };
            worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                waitingBar.StopWaiting();
                if (Cnt > 0)
                {
                    Biz.Show(this, Cnt.ToString() + "명 발송에 성공했습니다.");
                }
            };
            waitingBar.StartWaiting();
            await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
            BeginInvoke(new MethodInvoker(worker.RunWorkerAsync));
        }
        //추가 근로자 저장
        async private void btnSMSPersonSave_Click(object sender, EventArgs e)
        {
            if (_Year == "")
            {
                Biz.Show(this, "연도를 선택해 주십시오.", "알림");
                return;
            }

            if (_SaupID < 1)
            {
                Biz.Show(this, "사업장을 선택해 주십시오.", "알림");
                return;
            }

            string str = Clipboard.GetText();

            if (str == "")
            {
                Biz.Show(this, "붙여넣기 할 내용이 없습니다.", "알림");

                return;
            }

            string name = txtSMSName.Text.Trim();
            string phone = txtSMSPhone.Text.Trim().Replace("-", "");
            string buseo = txtSMSBuseo.Text.Trim();
            string date = dtpJosaDate.Value.ToString("yyyy-MM-dd");

            int r = -1;

            int Cnt = 0;

            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                SqlConnection con = Biz.Instance.Connection;
                SqlTransaction tran = con.BeginTransaction();
                //System.Collections.Generic.List l = new System.Collections.Generic.List();
                r = Biz.Instance.EMOTIONALLABOR_SMSPersonSave(con, tran, _SaupjaNum, name, phone, buseo, date);

                tran.Commit();

                con.Close();

                DataTable dt = Biz.Instance.EMOTIONALLABOR_SMSPersonList(_SaupjaNum, "");

                args.Result = dt;
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                try
                {
                    if (args.ProgressPercentage < 0)
                    {
                        waitingBar.Text = args.UserState?.ToString();
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog(ex.Message);
                }

            };
            worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (Cnt < 0)
                {
                    Biz.Show(this, "연락처 저장에 실패했습니다.", "오류");
                }
                else
                {
                    Biz.Show(this, "연락처 저장에 성공했습니다.");
                }

                grdSMSPerson.DataSource = args.Result;

                waitingBar.StopWaiting();
            };
            waitingBar.StartWaiting();
            await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
            BeginInvoke(new MethodInvoker(worker.RunWorkerAsync));
        }
        //문자발송근로자 리스트
        async private void btnSMSPersonList_Click(object sender, EventArgs e)
        {
            if (_Year == "")
            {
                Biz.Show(this, "연도를 선택해 주십시오.", "알림");
                return;
            }

            if (_SaupID < 1)
            {
                Biz.Show(this, "사업장을 선택해 주십시오.", "알림");
                return;
            }

            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                DataTable dt = Biz.Instance.EMOTIONALLABOR_SMSPersonList(_SaupjaNum, "");

                args.Result = dt;
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                try
                {
                    if (args.ProgressPercentage < 0)
                    {
                        waitingBar.Text = args.UserState?.ToString();
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog(ex.Message);
                }

            };
            worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result is DataTable)
                {
                    grdSMSPerson.DataSource = (DataTable)args.Result;
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

        async private void btnSMSPersonSearch_Click(object sender, EventArgs e)
        {
            if (_Year == "")
            {
                Biz.Show(this, "연도를 선택해 주십시오.", "알림");
                return;
            }

            if (_SaupID < 1)
            {
                Biz.Show(this, "사업장을 선택해 주십시오.", "알림");
                return;
            }

            string year;
            try
            {
                year = Convert.ToInt32(txtSMSPersonYear.Text.Trim()).ToString();
            }
            catch
            {
                year = DateTime.Now.Year.ToString();
                txtSMSPersonYear.Text = year;
            }

            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                DataTable dt = Biz.Instance.EMOTIONALLABOR_SMSPersonList(_SaupjaNum, year);

                args.Result = dt;
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                try
                {
                    if (args.ProgressPercentage < 0)
                    {
                        waitingBar.Text = args.UserState?.ToString();
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog(ex.Message);
                }

            };
            worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result is DataTable)
                {
                    grdSMSPerson.DataSource = (DataTable)args.Result;
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
        //문자발송근로자 삭제
        async private void btnSMSPersonDel_Click(object sender, EventArgs e)
        {
            if (_Year == "")
            {
                Biz.Show(this, "연도를 선택해 주십시오.", "알림");
                return;
            }

            if (_SaupID < 1)
            {
                Biz.Show(this, "사업장을 선택해 주십시오.", "알림");
                return;
            }

            string year;
            try
            {
                year = Convert.ToInt32(txtSMSPersonYear.Text.Trim()).ToString();
            }
            catch
            {
                year = DateTime.Now.Year.ToString();
                txtSMSPersonYear.Text = year;
            }
            int Cnt = 0;
            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                SqlConnection con = Biz.Instance.Connection;
                SqlTransaction tran = con.BeginTransaction();
                string phone = "";
                string josadate = "";
                int r;
                string name = "";
                var rows = grdSMSPerson.Rows.Where(x => x.Cells["clmIsSelect"].Value != null && (x.Cells["clmIsSelect"].Value.ToString().ToLower() == "on" || x.Cells["clmIsSelect"].Value.ToString().ToLower() == "true"));

                Parallel.ForEach(rows, row =>
                {
                    name = row.Cells["clmName"].Value.ToString();
                    phone = row.Cells["clmPhone"].Value.ToString();
                    josadate = row.Cells["clmJosaDate"].Value.ToString();

                    r = Biz.Instance.EMOTIONALLABOR_SMSPersonDel(con, tran, _SaupjaNum, phone, josadate);

                    if (r < 0)
                    {
                        Cnt = -1;
                        tran.Rollback();
                        con.Close();
                        return;
                    }
                    else
                    {
                        worker.ReportProgress(-1, name + " : " + phone);
                        Cnt++;
                    }
                });

                tran.Commit();
                con.Close();
                //args.Result = dt;
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                try
                {
                    if (args.ProgressPercentage < 0)
                    {
                        waitingBar.Text = args.UserState?.ToString();
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog(ex.Message);
                }
            };
            worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (Cnt >= 0)
                {
                    btnSMSPersonList.PerformClick();
                }
                else
                {
                    Biz.Show(this, "데이터를 삭제하는데 실패했습니다.");
                }

                waitingBar.StopWaiting();
            };
            waitingBar.StartWaiting();
            await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
            BeginInvoke(new MethodInvoker(worker.RunWorkerAsync));
        }
        //근로자 조사일변경
        async private void btnSMSPersonJosaDateChange_Click(object sender, EventArgs e)
        {
            if (_SaupID < 1)
            {
                Biz.Show(this, "사업장을 선택해 주십시오.", "알림");
                return;
            }

            var rows = grdSMSPerson.Rows.Where(x => x.Cells["clmIsSelect"].Value != null && (x.Cells["clmIsSelect"].Value.ToString().ToLower() == "true" || x.Cells["clmIsSelect"].Value.ToString().ToLower() == "on"));

            if (rows.Count() < 1)
            {
                Biz.Show(this, "근로자를 선택해 주십시오.");
                return;
            }

            int Cnt = 0;
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
                string phone = "";
                string originaljosadate = "";
                string josadate = dtpJosaDate.Value.ToString("yyyy-MM-dd");
                int r;
                string name = "";

                Parallel.ForEach(rows, row =>
                {
                    if (row.Cells["clmIsSelect"].Value != null && (row.Cells["clmIsSelect"].Value.ToString().ToLower() == "on" || row.Cells["clmIsSelect"].Value.ToString().ToLower() == "true"))
                    {
                        name = row.Cells["clmName"].Value.ToString();
                        phone = row.Cells["clmPhone"].Value.ToString();
                        originaljosadate = row.Cells["clmOriginalJosaDate"].Value.ToString();
                        //josadate = row.Cells["clmJosaDate"].Value.ToString();

                        r = Biz.Instance.EMOTIONALLABOR_SMSPersonJosaDateChange(con, tran, _SaupjaNum, phone, originaljosadate, josadate);

                        if (con.State != ConnectionState.Open)
                            return;
                        else if (r < 0)
                        {
                            Cnt = -1;
                            tran.Rollback();
                            con.Close();
                            return;
                        }
                        else
                        {
                            worker.ReportProgress(-1, name + " : " + phone);
                            Cnt++;
                        }
                    }

                });
                if (con.State != ConnectionState.Open)
                    return;
                else
                {
                    tran.Commit();
                    con.Close();
                }

                //args.Result = dt;
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                try
                {
                    if (args.ProgressPercentage < 0)
                    {
                        waitingBar.Text = args.UserState?.ToString();
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog(ex.Message);
                }

            };
            worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                waitingBar.StopWaiting();

                if (Cnt >= 0)
                {
                    btnSMSPersonList.PerformClick();
                }
                else
                {
                    Biz.Show(this, "데이터를 변경하는데 실패했습니다.");
                }


            };
            waitingBar.StartWaiting();
            await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
            BeginInvoke(new MethodInvoker(worker.RunWorkerAsync));


        }

        async private void grdSMSPerson_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewFilteringRowInfo)
                return;
            if (e.Row is GridViewTableHeaderRowInfo)
            {
                if (e.Column.Name == "clmIsSelect")
                {
                    var rows = grdSMSPerson.Rows.Where(row => row.Cells["clmIsSelect"].Value != null
                        && (row.Cells["clmIsSelect"].Value.ToString().ToLower() == "true" || row.Cells["clmIsSelect"].Value.ToString().ToLower() == "on"));

                    if (rows.Count() > 0)
                        grdSMSPerson.Rows.ForEach(row => row.Cells["clmIsSelect"].Value = false);
                    else
                        grdSMSPerson.Rows.ForEach(row => row.Cells["clmIsSelect"].Value = true);

                    return;
                }
                return;
            }
            if (e.Column.Name == "clmIsSelect")
            {
                return;
            }

            //EMOTIONALLABOR_SMSPersonHistoryList
            if (_SaupID < 1)
            {
                Biz.Show(this, "사업장을 선택해 주십시오.", "알림");
                return;
            }

            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                string phone = e.Row.Cells["clmPhone"].Value.ToString().Replace("-", "");

                DataTable dt = Biz.Instance.EMOTIONALLABOR_SMSPersonHistoryList(phone);

                args.Result = dt;
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                try
                {
                    if (args.ProgressPercentage < 0)
                    {
                        waitingBar.Text = args.UserState?.ToString();
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog(ex.Message);
                }

            };
            worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                waitingBar.StopWaiting();

                if (args.Result != null && args.Result is DataTable)
                {
                    grdSMSPersonHistory.DataSource = (DataTable)args.Result;
                }
                else
                {
                    Biz.Show(this, "데이터를 불러오는데 실패했습니다.");
                }
            };
            waitingBar.StartWaiting();
            await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
            BeginInvoke(new MethodInvoker(worker.RunWorkerAsync));
        }

        private void grdSMSPerson_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            //for (int i = 0; i < grdSMSPerson.Rows.Count; i++)
            //{
            //    grdSMSPerson.Rows[i].Cells["clmJosaDate"].Value = grdSMSPerson.Rows[i].Cells["clmOriginalJosaDate"].Value;
            //}
            grdSMSPerson.Rows.AsParallel().ForEach(row => {
                row.Cells["clmJosaDate"].Value = row.Cells["clmOriginalJosaDate"].Value;
            });

            //Parallel.ForEach(grdSMSPerson.Rows, row => { row.Cells["clmJosaDate"].Value = row.Cells["clmOriginalJosaDate"].Value; });
        }

        private void dtpQRJosaDate_Closing(object sender, RadPopupClosingEventArgs args)
        {
            txtQRJosaDate.Text = dtpQRJosaDate.Value.ToString("yyyy-MM-dd");
        }

        async private void grdQRJosaDate_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (_SaupID < 1)
            {
                Biz.Show(this, "사업장을 선택해 주십시오.", "알림");
                return;
            }

            string josadate = "";
            try
            {
                josadate = Convert.ToDateTime(e.Row.Cells["clmJosaDate"].Value).ToString("yyyy-MM-dd");
            }
            catch
            {
                Biz.Show(this, "올바른 조사일이 아닙니다");
                return;
            }

            string guid = "";

            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                DataTable dt = Biz.Instance.EMOTIONALLABOR_SaupjaGUID(this._SaupID, josadate, _SaupjaNum);

                if (dt != null && dt.Rows.Count == 1)
                {
                    guid = dt.Rows[0]["GUID"].ToString();
                }
                else
                {
                    worker.ReportProgress(-1, "GUID 생성에 실패했습니다.");
                    return;
                }
                args.Result = guid;
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                try
                {
                    if (args.ProgressPercentage < 0)
                    {
                        waitingBar.Text = args.UserState?.ToString();
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog(ex.Message);
                }
            };
            worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                waitingBar.StopWaiting();

                if (args.Result != null && args.Result is string)
                {
                    string qr = _Url + guid;
                    qrJosaDate.Value = qr;
                    txtQRLink.Text = qr;
                    txtQRJosaDate.Text = josadate;
                    dtpJosaDate.Value = Convert.ToDateTime(josadate);
                }
                else
                {
                    Biz.Show(this, "데이터를 불러오는데 실패했습니다.");
                }
            };

            waitingBar.StartWaiting();
            await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
            BeginInvoke(new MethodInvoker(worker.RunWorkerAsync));
        }

        async private void btnQRJosaDateRefresh_Click(object sender, EventArgs e)
        {
            if (_SaupID < 1)
            {
                Biz.Show(this, "사업장을 선택해 주십시오.", "알림");
                return;
            }

            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                DataTable dt = Biz.Instance.EMOTIONALLABOR_QRJosaDateList(this._SaupjaNum);

                args.Result = dt;
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                try
                {
                    if (args.ProgressPercentage < 0)
                    {
                        waitingBar.Text = args.UserState?.ToString();
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog(ex.Message);
                }

            };
            worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                waitingBar.StopWaiting();

                if (args.Result != null && args.Result is DataTable)
                {
                    grdQRJosaDate.DataSource = (DataTable)args.Result;
                }
                else
                {
                    Biz.Show(this, "데이터를 불러오는데 실패했습니다.");
                }
            };
            waitingBar.StartWaiting();
            await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
            BeginInvoke(new MethodInvoker(worker.RunWorkerAsync));
        }

        private void btnQRGenerate_Click(object sender, EventArgs e)
        {
            if (this._SaupjaNum == "")
            {
                Biz.Show(this, "사업장을 선택해 주십시오.");
                return;
            }

            string josadate = "";
            try
            {
                josadate = Convert.ToDateTime(txtQRJosaDate.Text).ToString("yyyy-MM-dd");
            }
            catch
            {
                josadate = DateTime.Now.ToString("yyyy-MM-dd");
                txtQRJosaDate.Text = josadate;
            }

            DataTable dt = Biz.Instance.EMOTIONALLABOR_SaupjaGUID(_SaupID, josadate, _SaupjaNum);
            string guid = "";
            if (dt != null && dt.Rows.Count == 1)
            {
                guid = dt.Rows[0]["GUID"].ToString();
            }
            else
            {
                Biz.Show(this, "GUID 생성에 실패했습니다.");
                return;
            }

            string qr = _Url + guid;
            qrJosaDate.Value = qr;
            txtQRLink.Text = qr;
        }

        private void btnQRDamdangRefresh_Click(object sender, EventArgs e)
        {
            Sub.frmSaupjaUpmuDamdang f = new Sub.frmSaupjaUpmuDamdang();
            f.SaupID = _SaupID;
            f.Yearmon = "";
            f.StartPosition = FormStartPosition.CenterParent;
            if(f.ShowDialog(this) == DialogResult.OK)
            {
                txtQRName.Text = f.DamdangName;
                txtQREMail.Text = f.DamdangEMail;
            }
            f.Dispose();
            //RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            //waitingBar.AssociatedControl = pvpQR;
            //AbortableBackgroundWorker worker = new AbortableBackgroundWorker();
            //worker.WorkerSupportsCancellation = true;
            //worker.WorkerReportsProgress = true;

            //worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            //{
            //    try
            //    {
            //        DataTable dt = Biz.Instance.SaupjaUpmuDamdangList(_SaupID, "");

            //        args.Result = dt;
            //    }
            //    catch (Exception ex)
            //    {
            //        LogManager.WriteLog(ex.Message);
            //    }
            //};
            //worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            //{
            //    if (args.ProgressPercentage < 0)
            //        waitingBar.Text = (args.UserState?.ToString() ?? "") + "/" + grdBuseo.Rows.Count.ToString();
            //};
            //worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            //{
            //    if (args.Result != null && args.Result is DataTable)
            //    {
            //        grdQRDamdang.DataSource = (DataTable)args.Result;
            //    }
            //    else
            //        Biz.Show(this, "불러오기에 실패했습니다.", "알림");
            //    waitingBar.StopWaiting();
            //};
            //waitingBar.StartWaiting();
            //await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
            //BeginInvoke(new MethodInvoker(worker.RunWorkerAsync));

        }

        async private void btnQREMailSend_Click(object sender, EventArgs e)
        {
            if (this._SaupjaNum == "")
            {
                Biz.Show(this, "사업장을 선택해 주십시오.");
                return;
            }

            if (txtQRName.Text.Trim() == "")
            {
                Biz.Show(this, "메일 수신자 이름이 없습니다");
                return;
            }

            if (txtQREMail.Text.Trim() == "")
            {
                Biz.Show(this, "메일 수신자 주소가 없습니다");
                return;
            }
            string josadate = "";
            try
            {
                josadate = Convert.ToDateTime(txtQRJosaDate.Text).ToString("yyyy-MM-dd");
            }
            catch
            {
                josadate = DateTime.Now.ToString("yyyy-MM-dd");
                txtQRJosaDate.Text = josadate;
            }


            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = pvpQR;
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                try
                {
                    if ((qrJosaDate.Value?.ToString() ?? "") == "")
                    {
                        Biz.Show(this, "");
                    }

                    DataTable dt = Biz.Instance.EMOTIONALLABOR_SaupjaGUID(_SaupID, josadate, _SaupjaNum);
                    string guid = "";

                    if (dt != null && dt.Rows.Count == 1)
                    {
                        guid = dt.Rows[0]["GUID"].ToString();
                    }
                    else
                    {
                        args.Result = "GUID 생성에 실패했습니다.";
                        return;
                    }

                    worker.ReportProgress(-1, "QR 코드 생성");

                    //"https://docs.google.com/forms/d/e/1FAIpQLSeJOYEyIZvmYRcYGJ9dqKpQW6jUVksAZx0lq1DrOD3Q-HavRw/viewform?usp=pp_url&entry.2098741794=" + guid;
                    string qr = _Url + guid;
                    qrJosaDate.Value = qr;

                    worker.ReportProgress(0, qr);

                    Guid localGUID = Guid.NewGuid();

                    string localfileguid = localGUID.ToString();

                    try
                    {
                        worker.ReportProgress(-1, "QR 코드 이미지 로컬 저장");
                        qrJosaDate.ExportToImage(@"C:\Kiha\Bogun\Temp\" + localfileguid + ".png", new Size(500, 500));
                    }
                    catch
                    {

                    }

                    string rdurl = "";

                    try
                    {
                        string[] p1 = new string[1];
                        p1[0] = guid;

                        rdurl = Biz.ReportHtml5("JIKMUSTRESS_QR", p1);
                    }
                    catch
                    {
                        rdurl = "";
                    }

                    string body = "<div>"
                        + "<p > 안녕하세요. 대한산업보건협회 " + Biz.Instance.DHCenterName + "입니다. <br/><br/>"
                        + "</p>";

                    body += //"<a href = \"" + rdurl + "\"> " + "<font color=red size=4> 직무스트레스 QR 링크 </font><br/>" + " </a><br/>"
                           "<br/>항상 최선을 다하는 대한산업보건협회가 되도록 하겠습니다.<br/>감사합니다.<br/>"
                           + "</p></div>";

                    worker.ReportProgress(-1, "QR 코드 이미지 파일 업로드 저장");
                    Biz.Instance.FileUpload("/BOGUN_JIKMUSTRESS_QR", @"C:\Kiha\Bogun\Temp\" + localfileguid + ".Png", guid + ".png");
                    worker.ReportProgress(-1, "QR 코드 메일 발송 준비");
                    //메일 발송
                    KihaMail m = new KihaMail();
                    m.linkNm = "보건관리";
                    m.categoryNm = "보건관리";
                    m.content = body;
                    m.title = Biz.Instance.DHCenterName + " 감정노동평가 설문 QR코드 (기준 설문일 : " + Convert.ToDateTime(josadate).ToString("yyyy년 MM월 dd일") + ")";
                    m.sendType = "D";
                    m.sendInfo = Biz.Instance.UserEmailID + " " + Biz.Instance.UserName;
                    string[] receiveMailAddresses;
                    receiveMailAddresses = new string[1] { txtQREMail.Text.Trim() + " " + txtQRName.Text.Trim() };
                    m.ReceiveAddresses = receiveMailAddresses;
                    m.userId = Biz.Instance.UserID;
                    string[] filelist = new string[1];
                    filelist[0] = @"C:\Kiha\Bogun\Temp\" + localfileguid + ".Png";
                    MailResult mr = m.SendMail(filelist);
                    if (mr == null)
                    {
                        args.Result = "메일 발송시 오류가 발생했습니다.";
                        //worker.ReportProgress(-1, "메일 발송시 오류가 발생했습니다.");
                    }
                    else if(mr.Result.ToString().ToLower() != "success")
                    {
                        args.Result = mr.Result.ToString();                        
                    }

                    SqlConnection con = Biz.Instance.Connection;
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    SqlTransaction tran = con.BeginTransaction();

                    int r = Biz.Instance.EMOTIONALLABOR_SendEMailHistorySave(con, tran, _SaupID, josadate, Biz.Instance.UserName, txtQRName.Text.Trim(), txtQREMail.Text.Trim(), body, mr.MailId);

                    if (r < 0)
                    {
                        tran.Commit();
                        con.Close();
                        args.Result = "메일 발송시 오류가 발생했습니다.";
                        return;
                    }

                    tran.Commit();
                    con.Close();

                    DataTable dtHistory = Biz.Instance.EMOTIONALLABOR_SendEMailHistoryList(_SaupID);
                    args.Result = dtHistory;
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog(ex.Message);
                }
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                if (args.ProgressPercentage < 0)
                    waitingBar.Text = (args.UserState?.ToString() ?? "") + "/" + grdBuseo.Rows.Count.ToString();
                else if (args.ProgressPercentage == 0)
                {
                    txtQRLink.Text = args.UserState?.ToString() ?? "";
                }
            };
            worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result != null && args.Result.ToString() != "")
                {
                    Biz.Show(this, args.Result?.ToString() ?? "", "알림");
                }
                else if(args.Result != null && args.Result is DataTable)
                {
                    grdQRHistory.DataSource = args.Result;
                    Biz.Show(this, "메일 발송에 성공했습니다.", "알림");
                }
                else
                {
                    Biz.Show(this, "메일 발송에 성공했습니다.", "알림");
                }

                waitingBar.StopWaiting();
            };
            waitingBar.StartWaiting();
            await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
            BeginInvoke(new MethodInvoker(worker.RunWorkerAsync));
        }

        private void grdQRDamdang_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewTableHeaderRowInfo)
                return;

            txtQRName.Text = e.Row.Cells["clmName"].Value.ToString();
            txtQREMail.Text = e.Row.Cells["clmEMail"].Value.ToString();

        }

        private void grdQRDamdang_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewTableHeaderRowInfo)
                return;

            if (e.Column.Name == "clmSelect")
            {
                txtQRName.Text = e.Row.Cells["clmName"].Value.ToString();
                txtQREMail.Text = e.Row.Cells["clmEMail"].Value.ToString();
            }
        }

        private void btnQRCopy_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(txtQRLink.Text.Trim());
            }
            catch
            {

            }
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

        private void grdPersonDetail_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.Row is GridViewTableHeaderRowInfo)
                return;

            string value = e.Row.Cells["clmPosition"].Value?.ToString().Trim() ?? "";

            if (e.Column.Name == "clmValue1" && value == "1")
                e.CellElement.BackColor = Color.LightBlue;
            //else
            //    e.CellElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
            else if (e.Column.Name == "clmValue2" && value == "2")
                e.CellElement.BackColor = Color.LightBlue;
            //else
            //    e.CellElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
            else if (e.Column.Name == "clmValue3" && value == "3")
                e.CellElement.BackColor = Color.LightBlue;
            //else
            //    e.CellElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
            else if (e.Column.Name == "clmValue4" && value == "4")
                e.CellElement.BackColor = Color.LightBlue;
            else
                e.CellElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
        }

        private void btnGDate1_Click(object sender, EventArgs e)
        {
            txtGDate1.Text = DateTime.Now.ToString("yyyy-01-01");
            txtGDate2.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void btnGDate2_Click(object sender, EventArgs e)
        {
            txtGDate1.Text = DateTime.Now.AddYears(-1).ToString("yyyy-01-01");
            txtGDate2.Text = DateTime.Now.AddYears(-1).ToString("yyyy-12-31");
        }

        private void btnGSearch_Click(object sender, EventArgs e)
        {
            GoogleSurveySearch();
        }

        async private void GoogleSurveySearch()
        {
            string sdate = "";
            string edate = "";
            string searchtext = "";

            try
            {
                sdate = Convert.ToDateTime(txtGDate1.Text).ToString("yyyy-MM-dd");
                edate = Convert.ToDateTime(txtGDate2.Text).ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message);

                Biz.Show("검색 날짜 형식이 올바르지 않습니다.", "알림");

                return;
            }

            if (txtGSearch.Text.Trim() == "")
            {
                Biz.Show("검색 내용이 비어 있으면 안됩니다.", "알림");

                return;
            }

            searchtext = txtGSearch.Text.Trim();

            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                DataTable dt = Biz.Instance.EMOTIONALLABOR_GoogleSurveyHistoryList(sdate, edate, searchtext);
                args.Result = dt;
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                try
                {
                    if (args.ProgressPercentage < 0)
                    {
                        waitingBar.Text = args.UserState?.ToString();
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog(ex.Message);
                }

            };
            worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result == null)
                {
                    Biz.Show(this, "데이터를 불러오는데 실패했습니다.", "오류");
                }
                else
                {
                    grdGoogle.DataSource = args.Result;
                }

                waitingBar.StopWaiting();
            };
            waitingBar.StartWaiting();
            await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
            BeginInvoke(new MethodInvoker(worker.RunWorkerAsync));
        }

        private void grdGoogle_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewTableHeaderRowInfo || e.Row is GridViewFilteringRowInfo)
                return;

            if (e.Column.Name == "clmSave")
            {
                string reply = e.Row.Cells["clmProcParameter"].Value.ToString();

                string guid = "";

                List<string> list = new List<string>();

                try
                {
                    guid = reply.Replace("UP_EMOTIONALLABOR_SurveySave", "").Split('\'')[1];
                    string phone = reply.Replace("UP_EMOTIONALLABOR_SurveySave", "").Replace("-", "").Trim().Split('\'')[3];
                    string name = reply.Replace("UP_EMOTIONALLABOR_SurveySave", "").Replace("-", "").Trim().Split('\'')[5];
                    string sex = reply.Replace("UP_EMOTIONALLABOR_SurveySave", "").Replace("-", "").Trim().Split('\'')[7];
                    string buseo = reply.Replace("UP_EMOTIONALLABOR_SurveySave", "").Replace("-", "").Trim().Split('\'')[9];

                    string a01 = reply.Replace("UP_EMOTIONALLABOR_SurveySave", "").Replace("-", "").Trim().Split('\'')[11];
                    string a02 = reply.Replace("UP_EMOTIONALLABOR_SurveySaved", "").Replace("-", "").Trim().Split('\'')[13];
                    string b03 = reply.Replace("UP_EMOTIONALLABOR_SurveySaved", "").Replace("-", "").Trim().Split('\'')[15];
                    string b04 = reply.Replace("UP_EMOTIONALLABOR_SurveySaved", "").Replace("-", "").Trim().Split('\'')[17];
                    string b05 = reply.Replace("UP_EMOTIONALLABOR_SurveySaved", "").Replace("-", "").Trim().Split('\'')[19];
                    string c06 = reply.Replace("UP_EMOTIONALLABOR_SurveySaved", "").Replace("-", "").Trim().Split('\'')[21];
                    string c07 = reply.Replace("UP_EMOTIONALLABOR_SurveySaved", "").Replace("-", "").Trim().Split('\'')[23];
                    string d08 = reply.Replace("UP_EMOTIONALLABOR_SurveySaved", "").Replace("-", "").Trim().Split('\'')[25];
                    string d09 = reply.Replace("UP_EMOTIONALLABOR_SurveySaved", "").Replace("-", "").Trim().Split('\'')[27];
                    string d10 = reply.Replace("UP_EMOTIONALLABOR_SurveySaved", "").Replace("-", "").Trim().Split('\'')[29];
                    string d11 = reply.Replace("UP_EMOTIONALLABOR_SurveySaved", "").Replace("-", "").Trim().Split('\'')[31];

                    list.Add(guid);
                    list.Add(phone);
                    list.Add(name);
                    list.Add(sex);
                    list.Add(buseo);

                    list.Add(a01);
                    list.Add(a02);
                    list.Add(b03);
                    list.Add(b04);
                    list.Add(b05);
                    list.Add(c06);
                    list.Add(c07);
                    list.Add(d08);
                    list.Add(d09);
                    list.Add(d10);
                    list.Add(d11);

                    GoogleSurveySave(list);
                }
                catch
                {
                    Biz.Show("연락처 정보를 가져올 수 없습니다.", "알림");
                    return;
                }

                //GoogleSurveySave(phone);
            }
        }

        async void GoogleSurveySave(List<string> str)
        {
            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                SqlConnection con = Biz.Instance.Connection;
                SqlTransaction tran = con.BeginTransaction();

                int r = Biz.Instance.EMOTIONALLABOR_SurveySave(con, tran, str[0], str[1], str[2], str[3], str[4], str[5], str[6], str[7], str[8], str[9], str[10], str[11]
                    , str[12], str[13], str[14], str[15]);

                if (r < 0)
                {
                    tran.Rollback();
                    con.Close();
                    return;
                }

                tran.Commit();
                con.Close();

                args.Result = "성공";
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                try
                {
                    if (args.ProgressPercentage < 0)
                    {
                        waitingBar.Text = args.UserState?.ToString();
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog(ex.Message);
                }

            };
            worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result == null)
                {
                    Biz.Show(this, "데이터를 불러오는데 실패했습니다.", "오류");
                }
                else
                {
                    Biz.Show(this, "데이터를 불러오는데 성공했습니다.", "오류");
                }

                waitingBar.StopWaiting();
            };
            waitingBar.StartWaiting();
            await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
            BeginInvoke(new MethodInvoker(worker.RunWorkerAsync));
        }

        private void txtGSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                GoogleSurveySearch();
            }
        }

        async private void btnSMSPersonNotAnswer_Click(object sender, EventArgs e)
        {
            if(_SaupID < 0)
            {
                Biz.Show(this, "사업장을 선택해 주십시오");
                return;
            }

            string year = "";

            try
            {
                year = Convert.ToInt32(this.txtSMSPersonYear.Text).ToString();

                if(year.Length != 4)
                {
                    Biz.Show(this, "올바른 연도를 입력해 주십시오.");
                    return;
                }
            }
            catch
            {
                txtSMSPersonYear.Text = DateTime.Now.Year.ToString();
                year = txtSMSPersonYear.Text;
            }

            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                DataTable dtd = Biz.Instance.EMOTIONALLABOR_PersonNotAnswerList(_SaupjaNum, year);

                args.Result = dtd;
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                try
                {
                    if (args.ProgressPercentage < 0)
                    {
                        waitingBar.Text = args.UserState?.ToString();
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog(ex.Message);
                }

            };
            worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if(args.Result != null && args.Result is DataTable)
                {
                    grdSMSPerson.DataSource = args.Result;
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

        async private void btnExcel_Click(object sender, EventArgs e)
        {
            if (_SaupID < 0)
            {
                Biz.Show(this, "사업장을 선택해 주십시오");
                return;
            }

            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                DataTable dtd = Biz.Instance.EMOTIONALLABOR_Excel(_SaupID, _Year, "");

                args.Result = dtd;
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                try
                {
                    if (args.ProgressPercentage < 0)
                    {
                        waitingBar.Text = args.UserState?.ToString();
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog(ex.Message);
                }

            };
            worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    Biz.Instance.SaveExcel((DataTable)args.Result);
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

        async private void btnQRHistoryRefresh_Click(object sender, EventArgs e)
        {
            if (_SaupID < 0)
            {
                Biz.Show(this, "사업장을 선택해 주십시오");
                return;
            }

            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                DataTable dtd = Biz.Instance.EMOTIONALLABOR_SendEMailHistoryList(_SaupID);

                args.Result = dtd;
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                try
                {
                    if (args.ProgressPercentage < 0)
                    {
                        waitingBar.Text = args.UserState?.ToString();
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog(ex.Message);
                }

            };
            worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    grdQRHistory.DataSource = args.Result;
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

        private void grdQRHistory_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            grdQRHistory.Columns["clmEMailID"].IsVisible = true;
            var rows = grdQRHistory.Rows.Where(row => row.Cells["clmResult"].Value?.ToString() == "발송됨");

            SqlConnection con = Biz.Instance.Connection;
            if (con.State != ConnectionState.Open)
                con.Open();
            SqlTransaction tran = con.BeginTransaction();

            rows.AsParallel().ForEach(row => {
                var emailid = row.Cells["clmEMailID"].Value?.ToString() ?? "";
                if(emailid != "")
                {
                    MailInfo m = KihaMail.GetMailInfo(emailid);
                    if(m.Result != "NO_MSG_ID")
                    {
                        if (m.ReceiveAddresses[0].ReadYN == "Y")
                        {
                            int r = Biz.Instance.EMOTIONALLABOR_SendEMailHistoryUpdate(con, tran, emailid, "열어봄");
                            if (r < 0)
                            {
                                tran.Rollback();
                                con.Close();
                                return;
                            }
                            else
                            {
                                row.Cells["clmResult"].Value = "열어봄";
                            }
                        }
                        else if (m.ReceiveAddresses[0].Result == "F")
                        {
                            int r = Biz.Instance.EMOTIONALLABOR_SendEMailHistoryUpdate(con, tran, emailid, "발송실패");
                            if (r < 0)
                            {
                                tran.Rollback();
                                con.Close();
                                return;
                            }
                            else
                            {
                                row.Cells["clmResult"].Value = "발송실패";
                            }
                        }
                    }
                    
                }
                    
            });

            tran.Commit();
            con.Close();
        }
    }
}
