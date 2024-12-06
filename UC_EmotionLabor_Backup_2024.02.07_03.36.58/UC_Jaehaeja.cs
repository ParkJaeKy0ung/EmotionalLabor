using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.Export;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace NBOGUN
{
    public partial class UC_Jaehaeja : UserControl
    {
        int _SaupID;

        public int SaupID
        {
            get { return _SaupID; } set { _SaupID = value; }
        }

        public void RefreshData()
        {
            btnJaehaejaSearch.PerformClick();
        }

        DataTable mTableJaehaeja;

        public UC_Jaehaeja()
        {
            InitializeComponent();

            Biz.Instance.SetGridViewOption(grdJaehaeja);

            for (int i = DateTime.Now.Year; i > 2019; i--)
            {
                //cboExcelYear.Items.Add(i.ToString());
                cboJaehaejaYear.Items.Add(i.ToString());
            }
        }

        async private void btnJaehaejaSearch_Click(object sender, EventArgs e)
        {
            string year = cboJaehaejaYear.Text;
            try
            {
                year = Convert.ToDateTime(year + "-01-01").Year.ToString();
            }
            catch
            {
                year = DateTime.Now.Year.ToString();
            }

            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();

            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                mTableJaehaeja = Biz.Instance.JaehaejaList(_SaupID, year);

                if (mTableJaehaeja != null)
                {
                    args.Result = mTableJaehaeja;
                }
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
            };
            worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    //DataTable dt = (DataTable)args.Result;
                    grdJaehaeja.DataSource = mTableJaehaeja;
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

        private void btnJaehaejaAdd_Click(object sender, EventArgs e)
        {
            //재해자 추가
            //재해자 저장
            if (mTableJaehaeja == null)
            {
                Biz.Show(this.ParentForm, "재해자 정보를 불러오지 못했습니다." + Environment.NewLine + "프로그램을 재시작해주시기 바랍니다.", "오류");
                return;
            }

            SqlConnection con = Biz.Instance.Connection;
            if (con.State != ConnectionState.Open) con.Open();
            SqlTransaction tran = con.BeginTransaction();

            DataTable r;

            r = Biz.Instance.JaehaejaDataTableAdd(con, tran, _SaupID, "", DateTime.Now.ToString("yyyy-MM-dd"), "", "", "", "", "", "", "", "", -1, "", "", "____-__-__", "", "", "");

            if (r == null || r.Rows.Count != 1)
            {
                tran.Rollback();
                con.Close();
                Biz.Show(this.ParentForm, "저장에 실패했습니다");
                return;
            }
            else
            {
                tran.Commit();
                con.Close();
            }

            DataRow row = mTableJaehaeja.NewRow();
            row["SeqNO"] = r.Rows[0]["SeqNO"];
            row["JaehaeDate"] = DateTime.Now.ToString("yyyy-MM-dd");
            mTableJaehaeja.Rows.InsertAt(row, 0);
            this.mTableJaehaeja.AcceptChanges();
            grdJaehaeja.DataSource = mTableJaehaeja;
        }

        private void btnJaehaejaDel_Click(object sender, EventArgs e)
        {
            //재해자 삭제
            if (grdJaehaeja.SelectedRows == null || grdJaehaeja.SelectedRows.Count == 0)
            {
                Biz.Show(this.ParentForm, "재해자를 선택해 주십시오", "알림");
                return;
            }

            string uploadfilenameold = grdJaehaeja.SelectedRows[0].Cells["clmUploadFileNameOld"].Value?.ToString().Trim() ?? "";
            //string url = "";
            if (uploadfilenameold == "")
            {
                grdJaehaeja.Rows.Remove(grdJaehaeja.SelectedRows[0]);
                this.mTableJaehaeja.AcceptChanges();
                return;
            }
            else
            {
                Biz.Show(this.ParentForm, "첨부 파일이 있는 경우 개별 삭제를 진행해 주십시오.");
                //return;
            }


            #region 재해자 개별 삭제
            int seqno = 0;
            try
            {
                seqno = Convert.ToInt32(grdJaehaeja.SelectedRows[0].Cells["clmSeqNO"].Value);
            }
            catch
            {

            }

            int r;
            string jaehaejaname = grdJaehaeja.SelectedRows[0].Cells["clmName"].Value.ToString();
            string jaehaedate = grdJaehaeja.SelectedRows[0].Cells["clmJaehaeDate"].Value.ToString();

            if (seqno < 1)//저장되지 않은 재해자
            {
                grdJaehaeja.Rows.Remove(grdJaehaeja.SelectedRows[0]);
                this.mTableJaehaeja.AcceptChanges();
                return;
            }

            DialogResult res = Biz.Show(this, jaehaejaname + " (" + jaehaedate + ") 기록을 삭제 하시겠습니까?", "경고", MessageBoxButtons.OKCancel);

            if (res != DialogResult.OK)
            {
                return;
            }

            //string uploadfilenameold = grdJaehaeja.SelectedRows[0].Cells["clmUploadFileNameOld"].Value?.ToString() ?? "";

            if (uploadfilenameold != "")
            {
                string url = "https://ohis.kiha21.or.kr/FileUpload/BOGUN_Jaehaeja/C" + Biz.Instance.DHCenter + "_" + _SaupID.ToString() + "_" + seqno.ToString() + "_" + uploadfilenameold;
                if (Biz.URLExists(url))
                {
                    //파일 삭제

                    Biz.FTPDelete("/BOGUN_Jaehaeja/", "C" + Biz.Instance.DHCenter + "_" + _SaupID.ToString() + "_" + seqno.ToString() + "_" + uploadfilenameold);

                    System.Threading.Thread.Sleep(500);

                    if (Biz.URLExists(url))
                    {
                        Biz.Show(this, "기존 파일 삭제에 실패했습니다");
                        return;
                    }
                }
            }

            SqlConnection con = Biz.Instance.Connection;
            if (con.State != ConnectionState.Open) con.Open();
            SqlTransaction tran = con.BeginTransaction();

            r = Biz.Instance.JaehaejaDel(con, tran, _SaupID, seqno, "");

            if (r < 0)
            {
                tran.Rollback();
                con.Close();
                Biz.Show(this, "삭제중 오류가 발생했습니다", "알림");
                return;
            }
            else
            {
                tran.Commit();
                con.Close();
            }

            grdJaehaeja.Rows.Remove(grdJaehaeja.SelectedRows[0]);
            this.mTableJaehaeja.AcceptChanges();
            #endregion
        }

        private void btnJaehaejaExcel_Click(object sender, EventArgs e)
        {
            if (_SaupID < 0)
            {
                Biz.Show(this, "사업장을 선택해 주십시오.");
                return;
            }

            //연도별 엑셀
            //DataSet ds = Biz.Instance.Excel_JaehaejaListForYear();

            //for (int i = 0; i < ds.Tables.Count; i++)
            //{
            //    if (ds.Tables[i].Rows.Count == 0)
            //    {
            //        ds.Tables.Remove(ds.Tables[i]);
            //        i--;
            //    }
            //    else
            //    {
            //        ds.Tables[i].TableName = ds.Tables[i].Rows[i]["Year"].ToString() + "년도";
            //    }
            //}

            string excelName = Guid.NewGuid().ToString();

            Biz.ExportGridView(grdJaehaeja);
        }

        private void btnJaehaejaSave_Click(object sender, EventArgs e)
        {
            if (_SaupID < 0)
            {
                Biz.Show(this, "사업장을 선택해 주십시오");
                return;
            }

            DialogResult result = Biz.Show(this, cboJaehaejaYear.Text + "년도 기록을 삭제 후 화면에 있는 데이터로 저장 합니다.\r\n계속 하시겠습니까?", "경고", MessageBoxButtons.YesNo);

            if (result != DialogResult.Yes)
                return;

            SaveJaehaeja();
        }
        void SaveJaehaeja()
        {
            int r;
            string jumin = "";
            string jaehaedate = "";
            string name = "";
            string jaehaegubun = "";
            string jaehaegubun1 = "";
            string sincheongdate = "";
            string seunindate = "";
            string disease = "";
            string cause = "";
            string result = "";
            int seqno;
           // string uploadfilename = "";
           // string uploadfilenameold = "";
            //string url = "";
            string resolve = "";
            string pfileName = "", returndate = "";
            string singodate = "";
            string retiredate = "";//퇴사일
            string pFullfileName = "", IsPyeongga = "";
            string year = cboJaehaejaYear.Text;
            
            try
            {
                year = Convert.ToDateTime(year + "-01-01").Year.ToString();
            }
            catch
            {
                year = DateTime.Now.Year.ToString();
            }

            SqlConnection con = Biz.Instance.Connection;
            if (con.State != ConnectionState.Open) con.Open();
            SqlTransaction tran = con.BeginTransaction();

            int ir = Biz.Instance.JaehaejaDel(con, tran, _SaupID, -1, year);

            if (ir < 0)
            {
                tran.Rollback();
                con.Close();
                Biz.Show(this, "저장중 초기화에 실패했습니다.", "오류");
                return;
            }

            for (int i = 0; i < grdJaehaeja.Rows.Count; i++)
            {
                name = grdJaehaeja.Rows[i].Cells["clmName"].Value.ToString();
                jaehaedate = grdJaehaeja.Rows[i].Cells["clmJaehaeDate"].Value.ToString();
                jaehaegubun1 = grdJaehaeja.Rows[i].Cells["clmJaehaegubun1"].Value.ToString();

                jumin = grdJaehaeja.Rows[i].Cells["clmJumin"].Value.ToString();
                jaehaegubun = grdJaehaeja.Rows[i].Cells["clmjaehaegubun"].Value.ToString();
                sincheongdate = grdJaehaeja.Rows[i].Cells["clmSincheongDate"].Value.ToString();
                seunindate = grdJaehaeja.Rows[i].Cells["clmSeunginDate"].Value.ToString();
                disease = grdJaehaeja.Rows[i].Cells["clmdisease"].Value.ToString();
                cause = grdJaehaeja.Rows[i].Cells["clmcause"].Value.ToString();
                result = grdJaehaeja.Rows[i].Cells["clmresult"].Value.ToString();
                jaehaegubun1 = grdJaehaeja.Rows[i].Cells["clmjaehaegubun1"].Value.ToString();

                returndate = grdJaehaeja.Rows[i].Cells["clmReturnDate"].Value == null ? "" : grdJaehaeja.Rows[i].Cells["clmReturnDate"].Value.ToString();//복귀일
                singodate = grdJaehaeja.Rows[i].Cells["clmSingoDate"].Value == null ? "" : grdJaehaeja.Rows[i].Cells["clmSingoDate"].Value.ToString();//신고일
                IsPyeongga = grdJaehaeja.Rows[i].Cells["clmIsPyeongga"].Value?.ToString() ?? "";

                seqno = 0;
                try
                {
                    seqno = Convert.ToInt32(grdJaehaeja.Rows[i].Cells["clmSeqNO"].Value);
                }
                catch
                {

                }

                pfileName = grdJaehaeja.Rows[i].Cells["clmUploadFileName"].Value?.ToString() ?? "";
                resolve = grdJaehaeja.Rows[i].Cells["clmResolve"].Value?.ToString() ?? "";//원인 분석및 대책
                retiredate = grdJaehaeja.Rows[i].Cells["clmRetireDate"].Value?.ToString() ?? "";
                IsPyeongga = grdJaehaeja.Rows[i].Cells["clmIsPyeongga"].Value?.ToString() ?? "";
                r = Biz.Instance.JaehaejaSave(con, tran, _SaupID, jumin, jaehaedate, name, jaehaegubun, jaehaegubun1, sincheongdate, seunindate, disease, cause, result, seqno, pfileName, resolve, returndate, singodate, retiredate, IsPyeongga);

                if (r < 1)
                {
                    tran.Rollback();
                    con.Close();
                    Biz.Show(this, "저장중 오류가 발생했습니다", "경고");
                    return;
                }
            }

            tran.Commit();
            con.Close();
            Biz.Show(this, "저장에 성공했습니다.", "알림");
        }

        private void grdJaehaeja_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.Row is GridViewTableHeaderRowInfo)
                return;

            string year = cboJaehaejaYear.Text;
            try
            {
                year = Convert.ToDateTime(cboJaehaejaYear.Text + "-01-01").Year.ToString();
            }
            catch
            {
                year = DateTime.Now.Year.ToString();
            }

            if (e.Column.Name == "clmJaehaeGubunName")
            {
                Sub.frmCodeJaehaeja f = new Sub.frmCodeJaehaeja();
                f.StartPosition = FormStartPosition.CenterParent;
                f.Owner = this.ParentForm;
                if (f.ShowDialog() == DialogResult.OK)
                {
                    e.Row.Cells["clmJaehaeGubun1"].Value = f.Code;
                    e.Row.Cells["clmJaehaeGubunName"].Value = f.CodeName;
                }
                f.Dispose();
            }
            else if (e.Column.Name == "clmJaehaejaDel")
            {
                #region 재해자 개별 삭제
                int seqno = 0;
                try
                {
                    seqno = Convert.ToInt32(grdJaehaeja.SelectedRows[0].Cells["clmSeqNO"].Value);
                }
                catch
                {

                }

                int r;
                string jaehaejaname = e.Row.Cells["clmName"].Value.ToString();
                string jaehaedate = e.Row.Cells["clmJaehaeDate"].Value.ToString();

                if (seqno < 1)//저장되지 않은 재해자
                {
                    grdJaehaeja.Rows.Remove(grdJaehaeja.SelectedRows[0]);
                    this.mTableJaehaeja.AcceptChanges();
                    return;
                }

                DialogResult res = Biz.Show(this, jaehaejaname + " (" + jaehaedate + ") 기록을 삭제 하시겠습니까?", "경고", MessageBoxButtons.OKCancel);

                if (res != DialogResult.OK)
                {
                    return;
                }

                string uploadfilenameold = e.Row.Cells["clmUploadFileNameOld"].Value?.ToString() ?? "";

                if (uploadfilenameold != "")
                {
                    string url = "https://ohis.kiha21.or.kr/FileUpload/BOGUN_Jaehaeja/C" + Biz.Instance.DHCenter + "_" + _SaupID.ToString() + "_" + seqno.ToString() + "_" + uploadfilenameold;
                    if (Biz.URLExists(url))
                    {
                        //파일 삭제

                        Biz.FTPDelete("/BOGUN_Jaehaeja/", "C" + Biz.Instance.DHCenter + "_" + _SaupID.ToString() + "_" + seqno.ToString() + "_" + uploadfilenameold);

                        System.Threading.Thread.Sleep(500);

                        if (Biz.URLExists(url))
                        {
                            Biz.Show(this, "기존 파일 삭제에 실패했습니다");
                            return;
                        }
                    }
                }

                SqlConnection con = Biz.Instance.Connection;
                if (con.State != ConnectionState.Open) con.Open();
                SqlTransaction tran = con.BeginTransaction();

                r = Biz.Instance.JaehaejaDel(con, tran, _SaupID, seqno, year);

                if (r < 0)
                {
                    tran.Rollback();
                    con.Close();
                    Biz.Show(this, "삭제중 오류가 발생했습니다", "알림");
                    return;
                }
                else
                {
                    tran.Commit();
                    con.Close();
                }

                grdJaehaeja.Rows.Remove(grdJaehaeja.SelectedRows[0]);
                this.mTableJaehaeja.AcceptChanges();
                #endregion
            }
            else if (e.Column.Name == "clmFile")
            {
                if (Biz.Instance.UploadStatus != "")
                {
                    Biz.Show(this.ParentForm, Biz.Instance.UploadStatus);
                    return;
                }

                DialogResult res = Biz.Show(this, "파일을 선택하면 바로 해당내역이 저장됩니다. 계속 진행하시겠습니까?", "알림", MessageBoxButtons.OKCancel);

                if (res != DialogResult.OK)
                    return;

                OpenFileDialog di = new OpenFileDialog();

                di.Multiselect = false;

                if (di.ShowDialog() == DialogResult.OK)
                {
                    string jumin = e.Row.Cells["clmJumin"].Value.ToString();
                    string jaehaedate = e.Row.Cells["clmJaehaeDate"].Value.ToString();
                    string name = e.Row.Cells["clmName"].Value.ToString();
                    string jaehaegubun = e.Row.Cells["clmjaehaegubun"].ToString();
                    string jaehaegubun1 = e.Row.Cells["clmJaehaegubun1"].Value.ToString();
                    string sincheongdate = e.Row.Cells["clmSincheongDate"].Value.ToString();
                    string seunindate = e.Row.Cells["clmSeunginDate"].Value.ToString();
                    string disease = e.Row.Cells["clmdisease"].Value.ToString();
                    string cause = e.Row.Cells["clmcause"].Value.ToString();
                    string result = e.Row.Cells["clmresult"].Value.ToString();
                    string uploadfilenameold = e.Row.Cells["clmUploadFileNameOld"].Value?.ToString() ?? "";
                    string uploadfilename = e.Row.Cells["clmUploadFileName"].Value?.ToString() ?? "";
                    int seqno = 0;
                    string isPyeongga = e.Row.Cells["clmIsPyeongga"].Value?.ToString() ?? "";
                    try
                    {
                        seqno = Convert.ToInt32(e.Row.Cells["clmSeqNO"].Value);
                    }
                    catch
                    {

                    }
                    string returndate = e.Row.Cells["clmReturnDate"].Value == null ? "" : e.Row.Cells["clmReturnDate"].Value.ToString();
                    string resolve = e.Row.Cells["clmResolve"].Value == null ? "" : e.Row.Cells["clmResolve"].Value.ToString();
                    string singodate = e.Row.Cells["clmSingoDate"].Value?.ToString() ?? "";
                    string retiredate = e.Row.Cells["clmRetireDate"].Value?.ToString() ?? "";

                    RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
                    waitingBar.Text = "";
                    waitingBar.AssociatedControl = this;
                    waitingBar.StartWaiting();

                    AbortableBackgroundWorker worker = new AbortableBackgroundWorker();
                    worker.DoWork += delegate (object obj, DoWorkEventArgs args)
                    {
                        SqlConnection con = Biz.Instance.Connection;
                        if (con.State != ConnectionState.Open) con.Open();
                        SqlTransaction tran = con.BeginTransaction();

                        worker.ReportProgress(-1, "저장 준비중");
                        uploadfilename = di.FileName;
                        string pfileName = uploadfilename.Substring(uploadfilename.LastIndexOf("\\") + 1, uploadfilename.Length - uploadfilename.LastIndexOf("\\") - 1);

                        DataTable dt = Biz.Instance.JaehaejaDataTableAdd(con, tran, _SaupID, jumin.Trim(), jaehaedate, name, jaehaegubun, jaehaegubun1, sincheongdate, seunindate, disease, cause, result, seqno, pfileName
                            , resolve, returndate, singodate, retiredate, isPyeongga);

                        if (dt == null || dt.Rows.Count != 1)
                        {
                            tran.Rollback();
                            con.Close();
                            args.Result = "근로자 저장에 실패했습니다.";
                            worker.CancelAsync();
                            worker.Abort();
                            return;
                        }
                        else
                        {
                            worker.ReportProgress(-1, "저장 성공");

                            tran.Commit();
                            con.Close();
                            seqno = Convert.ToInt16(dt.Rows[0]["SeqNO"]);
                        }

                        string url = "https://ohis.kiha21.or.kr/FileUpload/BOGUN_Jaehaeja/C" + Biz.Instance.DHCenter + "_" + _SaupID.ToString() + "_" + seqno.ToString() + "_" + uploadfilenameold;

                        //기존 파일 삭제
                        if (Biz.URLExists(url))
                        {
                            worker.ReportProgress(-1, "기존 파일 삭제 시도");
                            //파일 삭제
                            Biz.FTPDelete("/BOGUN_Jaehaeja/", "C" + Biz.Instance.DHCenter + "_" + _SaupID.ToString() + "_" + seqno.ToString() + "_" + uploadfilenameold);

                            System.Threading.Thread.Sleep(500);

                            if (Biz.URLExists(url))
                            {
                                args.Result = "근로자 저장은 성공했지만 기존 파일 삭제에 실패했습니다";
                                worker.CancelAsync();
                                worker.Abort();
                                return;
                            }
                        }

                        worker.ReportProgress(-1, "신규 파일 업로드 시도");

                        ////신규 파일 저장
                        string ftpFolder = "/BOGUN_Jaehaeja";

                        //string pfileName = uploadfilename.Substring(uploadfilename.LastIndexOf("\\") + 1, uploadfilename.Length - uploadfilename.LastIndexOf("\\") - 1);

                        string realfilename = "C" + Biz.Instance.DHCenter + "_" + _SaupID.ToString() + "_" + seqno.ToString() + "_" + pfileName;

                        //if (Biz.Instance.WorkerFileUpload != null && Biz.Instance.WorkerFileUpload.IsBusy == true)
                        //{
                        //    args.Result = "파일 업로드 작업이 진행중입니다.";
                        //    worker.CancelAsync();
                        //    return;
                        //}

                        Biz.Instance.FileUpload(ftpFolder, uploadfilename, realfilename);

                        int ration = 0;
                        //int timer = 0;
                        while (ration <= 100)
                        {
                            //ration = ;
                            worker.ReportProgress(Biz.Instance.UploadPercentage);
                            System.Threading.Thread.Sleep(1);

                            //timer++;

                            //if (timer > 200000)
                            //    break;

                            if (Biz.Instance.UploadPercentage == 100)
                            {
                                break;
                            }

                        }

                        //업로드가 완료되면 Url을 업데이트 한다.
                        url = "https://ohis.kiha21.or.kr/FileUpload/BOGUN_Jaehaeja/" + realfilename;

                        DataTable dtJaehaeja = Biz.Instance.JaehaejaList(_SaupID, "", seqno);

                        if (dtJaehaeja != null && dtJaehaeja.Rows.Count == 1)
                        {
                            worker.ReportProgress(1000, dt.Rows[0]["UploadFileName"]);
                        }
                        else
                        {
                            worker.ReportProgress(-1, "올바른 일련번호를 불러올수 없습니다.");
                        }

                        args.Result = url;//경로전송하여 파일이 정상적으로 있는지 확인
                    };

                    worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
                    {
                        if (args.ProgressPercentage < 0)
                            waitingBar.Text = args.UserState?.ToString();
                        else if (args.ProgressPercentage <= 100)
                            waitingBar.Text = args.ProgressPercentage.ToString() + "/100";
                        else if (args.ProgressPercentage == 1000)
                        {
                            e.Row.Cells["clmUploadFileName"].Value = args.UserState?.ToString() ?? "";
                            e.Row.Cells["clmUploadFileNameOld"].Value = args.UserState?.ToString() ?? "";
                        }
                    };

                    worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
                    {
                        waitingBar.StopWaiting();

                        if (args.Cancelled == true)
                        {
                            Biz.Show(args.Result?.ToString());
                            return;
                        }
                        else
                        {
                            string url = args.Result?.ToString() ?? "";
                            if (url == "" || !Biz.URLExists(url))
                            {
                                Biz.Show(this.ParentForm, "업로드된 파일을 확인 할 수 없습니다.");
                            }
                        }
                    };

                    BeginInvoke(new MethodInvoker(worker.RunWorkerAsync));
                }

                di.Dispose();
            }
        }

        private void grdJaehaeja_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.Column.Name == "clmJaehaeDate" || e.Column.Name == "clmSincheongDate" || e.Column.Name == "clmSeunginDate")
            {
                string date = e.Row.Cells[e.Column.Name].Value?.ToString().Replace("-", "") ?? "";

                if (date.Length == 8)
                {
                    e.Row.Cells[e.Column.Name].Value = date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2);
                }
            }
        }

        private void grdJaehaeja_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            if (e.Column.Name == "clmSaupjaName")
            {
                try
                {
                    int inwon = Convert.ToInt32(e.Row.Cells["clmContractInwon"].Value);

                    if (inwon >= 100)
                        e.CellElement.ForeColor = Color.Blue;
                    else
                        e.CellElement.ForeColor = Color.Black;
                }
                catch
                {
                    e.CellElement.ForeColor = Color.Black;
                }
            }
            else
                e.CellElement.ForeColor = Color.Black;
        }
    }
}
