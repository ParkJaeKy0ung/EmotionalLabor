using NBOGUN.Sub;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Telerik.WinControls.VirtualKeyboard;
using Telerik.Windows.Diagrams.Core;

namespace NBOGUN
{
    public partial class UC_SaupjaWeewonhoi : UserControl
    {
        string _Year;
        int _SaupID;
        DataTable mTableWeewonhoi;
        AbortableBackgroundWorker _Worker;
        public int SaupID
        {
            get { return _SaupID; }
            set { _SaupID = value; }
        }

        async public void RefreshData()
        {
            if (_Worker != null && _Worker.IsBusy)
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
                mTableWeewonhoi = Biz.Instance.WeewonhoiList(_SaupID, year, (cboYear.Text == "전체" ? "2" : "1"));

                if (mTableWeewonhoi != null)
                {
                    args.Result = mTableWeewonhoi;
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
                    grdWeewonhoi.DataSource = mTableWeewonhoi;
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

        public UC_SaupjaWeewonhoi()
        {
            InitializeComponent();

            Biz.Instance.SetGridViewOption(grdWeewonhoi);
            Biz.Instance.SetDropDownList(cboYear);

            //clmGubun 위원회구분
            GridViewComboBoxColumn col1 = grdWeewonhoi.Columns["clmGubun"] as GridViewComboBoxColumn;
            col1.DisplayMember = "Name";
            col1.ValueMember = "Code";
            col1.DataSource = Biz.Instance.GetDHCodeList("위원회구분");

            GridViewComboBoxColumn col2 = grdWeewonhoi.Columns["clmTypeGubun"] as GridViewComboBoxColumn;
            col2.DisplayMember = "Name";
            col2.ValueMember = "Code";
            col2.DataSource = Biz.Instance.GetDHCodeList("위원회종류");

            GridViewComboBoxColumn col3 = grdWeewonhoi.Columns["clmAttendanceGubun"] as GridViewComboBoxColumn;
            col3.DisplayMember = "Name";
            col3.ValueMember = "Code";
            col3.DataSource = Biz.Instance.GetDHCodeList("위원회참석구분");

            GridViewComboBoxColumn col4 = grdWeewonhoi.Columns["clmBunki"] as GridViewComboBoxColumn;
            if (col4 != null)
            {
                DataTable dtBunki = new DataTable();
                dtBunki.Columns.Add("Bunki");
                DataRow rowBunki = dtBunki.NewRow();
                rowBunki["Bunki"] = "1";
                dtBunki.Rows.Add(rowBunki);
                rowBunki = dtBunki.NewRow();
                rowBunki["Bunki"] = "2";
                dtBunki.Rows.Add(rowBunki);
                rowBunki = dtBunki.NewRow();
                rowBunki["Bunki"] = "3";
                dtBunki.Rows.Add(rowBunki);
                rowBunki = dtBunki.NewRow();
                rowBunki["Bunki"] = "4";
                dtBunki.Rows.Add(rowBunki);
                rowBunki = dtBunki.NewRow();
                rowBunki["Bunki"] = "";
                dtBunki.Rows.Add(rowBunki);

                col4.DisplayMember = "Bunki";
                col4.DataSource = dtBunki;
            }

            cboYear.Items.Add("전체");

            for (int i = DateTime.Now.Year; i > 2019; i--)
            {
                //cboExcelYear.Items.Add(i.ToString());
                cboYear.Items.Add(i.ToString());
            }

            cboYear.SelectedIndex = 0;

            Biz.Instance.SaupIDChanged += Instance_SaupIDChanged;
        }

        private void Instance_SaupIDChanged(object sender, EventArgs e)
        {
            _SaupID = NBOGUN.Biz.Instance.CurrentSaupID;

            RefreshData();// Biz.Show(this, "바뀜");
        }

        ~UC_SaupjaWeewonhoi()
        {
            if (_Worker != null && _Worker.IsBusy)
            {
                _Worker.Abort();
                _Worker.Dispose();
            }
        }

        async private void grdWeewonhoi_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.Row is GridViewTableHeaderRowInfo)
                return;

            string uploadfilename = e.Row.Cells["clmUploadFileName"].Value?.ToString() ?? "";
            string date = DateTime.TryParse((e.Row.Cells["clmDate"].Value?.ToString() ?? ""), out DateTime dt) == true? (e.Row.Cells["clmDate"].Value?.ToString() ?? "") : "";
            string gubun = e.Row.Cells["clmGubun"].Value?.ToString() ?? "";
            string opinion = e.Row.Cells["clmOpinion"].Value?.ToString() ?? "";
            string typegubun = e.Row.Cells["clmTypeGubun"].Value?.ToString() ?? "";
            string attendanceGubun = e.Row.Cells["clmAttendanceGubun"].Value?.ToString() ?? "";
            string attendancedr = e.Row.Cells["clmAttendanceDr"].Value?.ToString() ?? "";
            string attendancenr = e.Row.Cells["clmAttendanceNr"].Value?.ToString() ?? "";
            string attendancehr = e.Row.Cells["clmAttendanceHr"].Value?.ToString() ?? "";
            string isdaesang = bool.TryParse((e.Row.Cells["clmIsDaesang"].Value?.ToString() ?? ""), out bool result) == true ? "1" : "0";
            string bunki = e.Row.Cells["clmBunki"].Value?.ToString() ?? "";
            int r;

            if (e.Column.Name == "clmAttendanceDrName" || e.Column.Name == "clmAttendanceNrName" || e.Column.Name == "clmAttendanceHrName")
            {//frmDamdang
                if ((e.Row.Cells["clmDate"].Value?.ToString() ?? "") == "")
                {
                    Biz.Show(this, "개최일을 올바르게 입력해 주십시오", "오류");
                    return;
                }
                Sub.frmDamdang f = new frmDamdang(e.Row.Cells["clmDate"].Value.ToString().Substring(0, 7));
                f.StartPosition = FormStartPosition.CenterParent;

                if (f.ShowDialog() == DialogResult.OK)
                {
                    e.Row.Cells["clmAttendanceDr"].Value = f.DamdangDr;
                    e.Row.Cells["clmAttendanceDrName"].Value = f.DamdangDrName;
                    e.Row.Cells["clmAttendanceNr"].Value = f.DamdangNr;
                    e.Row.Cells["clmAttendanceNrName"].Value = f.DamdangNrName;
                    e.Row.Cells["clmAttendanceHr"].Value = f.DamdangHr;
                    e.Row.Cells["clmAttendanceHrName"].Value = f.DamdangHrName;
                }
                f.Dispose();
            }
            else if (e.Column.Name == "clmFileDel")
            {
                string url = "https://ohis.kiha21.or.kr/FileUpload/BOGUN_Weewonhoi/C" + Biz.Instance.DHCenter + "_" + _SaupID.ToString() + "_" + date + "_" + uploadfilename;

                //기존 파일 삭제
                if (uploadfilename != "")
                {
                    // 기존 파일 삭제
                    if (Biz.URLExists(url))
                    {
                        Biz.Instance.FileDelete("/BOGUN_Weewonhoi", "C" + Biz.Instance.DHCenter + "_" + _SaupID.ToString() + "_" + date + "_" + uploadfilename);
                        System.Threading.Thread.Sleep(500);
                        if (Biz.URLExists(url))
                        {
                            Biz.Show(this, "기존 파일 삭제에 실패했습니다");
                            return;
                        }
                        else
                        {
                            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
                            waitingBar.AssociatedControl = this;
                            _Worker = new AbortableBackgroundWorker();

                            _Worker.DoWork += delegate (object obj, DoWorkEventArgs args)
                            {
                                SqlConnection con = Biz.Instance.Connection;
                                if (con.State != ConnectionState.Open)
                                    con.Open();
                                SqlTransaction tran = con.BeginTransaction();

                                r = Biz.Instance.WeewonhoiSave(con, tran, _SaupID, date, gubun, opinion, typegubun, attendanceGubun, attendancedr, attendancenr, attendancehr, isdaesang, "", bunki);

                                if (r < 0)
                                {
                                    tran.Rollback();
                                    con.Close();

                                    return;
                                }
                                else
                                {
                                    tran.Commit();
                                    con.Close();
                                    args.Result = e.Row;
                                }
                            };
                            _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
                            {
                            };
                            _Worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
                            {
                                if (args.Result != null && args.Result is GridViewRowInfo)
                                {
                                    ((GridViewRowInfo)args.Result).Cells["clmUploadFileName"].Value = "";
                                    ((GridViewRowInfo)args.Result).Cells["clmUploadFileNameOld"].Value = "";
                                }
                                else
                                {
                                    Biz.Show(this, "파일 삭제 후 저장에 실패했습니다.");
                                }

                                waitingBar.StopWaiting();
                            };

                            waitingBar.StartWaiting();
                            await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
                            BeginInvoke(new MethodInvoker(_Worker.RunWorkerAsync));
                        }
                    }
                }
            }
            else if (e.Column.Name == "clmFile")
            {
                DialogResult res = Biz.Show(this, "파일을 선택하면 바로 해당내역이 저장됩니다. 계속 진행하시겠습니까?", "알림", MessageBoxButtons.OKCancel);

                if (res != DialogResult.OK)
                    return;

                if (date == "")
                {
                    Biz.Show(this, "파일업로드를 위해선 회의일자를 올바르게 입력해 주십시오.", "알림");
                    return;
                }

                OpenFileDialog di = new OpenFileDialog();

                di.Multiselect = false;

                if (di.ShowDialog() == DialogResult.OK)
                {
                    string url = "https://ohis.kiha21.or.kr/FileUpload/BOGUN_Weewonhoi/C" + Biz.Instance.DHCenter + "_" + _SaupID.ToString() + "_" + date + "_" + uploadfilename;

                    //기존 파일 삭제
                    if (uploadfilename != "")
                    {
                        // 기존 파일 삭제
                        if (Biz.URLExists(url))
                        {
                            //파일 삭제
                            Biz.Instance.FileDelete("/BOGUN_Weewonhoi", "C" + Biz.Instance.DHCenter + "_" + _SaupID.ToString() + "_" + date + "_" + uploadfilename);

                            if (Biz.URLExists(url))
                            {
                                Biz.Show(this, "기존 파일 삭제에 실패했습니다");
                                return;
                            }
                        }
                    }

                    uploadfilename = di.FileName;
                    //업로드된 파일이 다를 경우

                    string ftpFolder = "/BOGUN_Weewonhoi";//앞에 / 꼭 붙여야함

                    string pfileName = uploadfilename.Substring(uploadfilename.LastIndexOf("\\") + 1, uploadfilename.Length - uploadfilename.LastIndexOf("\\") - 1);
                    string realfilename = "C" + Biz.Instance.DHCenter + "_" + _SaupID.ToString() + "_" + date + "_" + pfileName;

                    Biz.Instance.FileUpload(ftpFolder, uploadfilename, realfilename);

                    string fullPath = "https://ohis.kiha21.or.kr/FileUpload/BOGUN_Weewonhoi/" + realfilename;

                    WebRequest webRequest = WebRequest.Create(fullPath);
                    webRequest.Timeout = 10000; // miliseconds
                    webRequest.Method = "HEAD";

                    HttpWebResponse response = null;

                    System.Threading.Thread.Sleep(1000);

                    if (Biz.URLExists(fullPath))
                    {
                        RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
                        waitingBar.AssociatedControl = this;
                        _Worker = new AbortableBackgroundWorker();

                        _Worker.DoWork += delegate (object obj, DoWorkEventArgs args)
                        {
                            SqlConnection con = Biz.Instance.Connection;
                            if (con.State != ConnectionState.Open)
                                con.Open();
                            SqlTransaction tran = con.BeginTransaction();

                            r = Biz.Instance.WeewonhoiSave(con, tran, _SaupID, date, gubun, opinion, typegubun, attendanceGubun, attendancedr, attendancenr, attendancehr, isdaesang, pfileName, bunki);

                            if (r < 0)
                            {
                                tran.Rollback();
                                con.Close();

                                return;
                            }
                            else
                            {
                                tran.Commit();
                                con.Close();
                                args.Result = e.Row;
                            }
                        };
                        _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
                        {
                        };
                        _Worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
                        {
                            if (args.Result != null && args.Result is GridViewRowInfo)
                            {
                                ((GridViewRowInfo)args.Result).Cells["clmUploadFileName"].Value = pfileName;
                                ((GridViewRowInfo)args.Result).Cells["clmUploadFileNameOld"].Value = pfileName;
                            }
                            else
                            {
                                Biz.Show(this, "파일 삭제 후 저장에 실패했습니다.");
                            }

                            waitingBar.StopWaiting();
                        };

                        waitingBar.StartWaiting();
                        await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
                        BeginInvoke(new MethodInvoker(_Worker.RunWorkerAsync));
                    }
                    else
                    {
                        Biz.Show(this, "파일 업로드를 실패했습니다.", "오류");
                        return;
                    }

                }
                else
                {
                    e.Row.Cells["clmUploadFileName"].Value = "";
                }

                di.Dispose();
            }
            else if (e.Column.Name == "clmUploadFileName")
            {
                if ((e.Row.Cells["clmUploadFileName"].Value?.ToString().Trim() ?? "") == "")
                    return;

                if (System.IO.File.Exists(e.Row.Cells["clmUploadFileName"].Value.ToString().Trim()) == true)
                {
                    System.Diagnostics.Process.Start(e.Row.Cells["clmUploadFileName"].Value.ToString().Trim());
                }

                date = e.Row.Cells["clmDate"].Value.ToString();
                string pfileName = e.Row.Cells["clmUploadFileName"].Value.ToString();

                string realfilename = "C" + Biz.Instance.DHCenter + "_" + _SaupID.ToString() + "_" + date.ToString() + "_" + pfileName;

                string url = "https://ohis.kiha21.or.kr/FileUpload/BOGUN_Weewonhoi/" + realfilename;

                System.Diagnostics.Process.Start(url);
            }
        }

        private void grdWeewonhoi_CellValueChanged(object sender, GridViewCellEventArgs e)
        {
            if (e.Column.Name == "clmDate")
            {
                string date = e.Row.Cells["clmDate"].Value == null ? "" : e.Row.Cells["clmDate"].Value.ToString();

                if (date.Replace("-", "").Replace("_", "").Trim() == "")
                    return;
                else
                {
                    date = date.Substring(5, 2).Replace("_", "");

                    try
                    {
                        int m = Convert.ToInt16(date);

                        if (m == 1 || m == 2 || m == 3)
                            e.Row.Cells["clmBunki"].Value = 1;
                        else if (m == 4 || m == 5 || m == 6)
                            e.Row.Cells["clmBunki"].Value = 2;
                        else if (m == 7 || m == 8 || m == 7)
                            e.Row.Cells["clmBunki"].Value = 3;
                        else if (m == 10 || m == 11 || m == 12)
                            e.Row.Cells["clmBunki"].Value = 4;
                        else
                            e.Row.Cells["clmBunki"].Value = "";
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void grdWeewonhoi_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < grdWeewonhoi.Rows.Count; i++)
            {
                grdWeewonhoi.Rows[i].Cells["clmUploadFileName"].Value = grdWeewonhoi.Rows[i].Cells["clmUploadFileNameOld"].Value;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //재해자 추가
            //재해자 저장


            //Biz.This().BeginTran();

            //DataTable r;

            //r = Biz.This().JaehaejaAdd("", DateTime.Now.ToString("yyyy-MM-dd"), "", "", "", "", "", "", "", "", -1, "");

            //if (r == null || r.Rows.Count != 1)
            //{
            //    Biz.This().RollBackTran();
            //    Biz.Show(this, "저장중 오류가 발생했습니다", "경고");
            //    return;
            //}

            //Biz.This().EndTran();

            DataRow row = mTableWeewonhoi.NewRow();
            //row["SeqNO"] = r.Rows[0]["SeqNO"];
            //row["JaehaeDate"] = r.Rows[0]["JaehaeDate"];
            mTableWeewonhoi.Rows.InsertAt(row, 0);
            //this.mTableWeewonhoi.AcceptChanges();
            grdWeewonhoi.DataSource = mTableWeewonhoi;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            //재해자 삭제
            if (grdWeewonhoi.SelectedRows == null || grdWeewonhoi.SelectedRows.Count == 0)
                return;

            string uploadfilenameold = grdWeewonhoi.SelectedRows[0].Cells["clmUploadFileNameOld"].Value.ToString().Trim();
            string url = "";
            string date = grdWeewonhoi.SelectedRows[0].Cells["clmDate"].Value?.ToString() ?? "";

            if (uploadfilenameold == "")
            {
                grdWeewonhoi.Rows.Remove(grdWeewonhoi.SelectedRows[0]);

                //this.mTableWeewonhoi.AcceptChanges();

                return;
            }

            if (uploadfilenameold != "")
            {
                DialogResult result = Biz.Show(this, "업로드된 회의록이 있습니다. 회의록이 존재하는 데이터는 바로 삭제 후 바로 저장이 됩니다.", "경고", MessageBoxButtons.OKCancel);

                if (result == DialogResult.Cancel)
                    return;

                url = "https://ohis.kiha21.or.kr/FileUpload/BOGUN_Weewonhoi/C" + Biz.Instance.DHCenter + "_" + _SaupID.ToString() + "_" + date.ToString() + "_" + uploadfilenameold;

                // 기존 파일 삭제
                if (Biz.URLExists(url))
                {
                    Biz.Instance.FileDelete("/BOGUN_Weewonhoi", "C" + Biz.Instance.DHCenter + "_" + _SaupID.ToString() + "_" + date.ToString() + "_" + uploadfilenameold);
                    System.Threading.Thread.Sleep(500);
                    if (Biz.URLExists(url))
                    {
                        Biz.Show(this, "기존 파일 삭제에 실패했습니다");
                        return;
                    }
                }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Biz.ExportGridView(this.grdWeewonhoi);
        }

        async private void btnSave_Click(object sender, EventArgs e)
        {
            if (mTableWeewonhoi == null)
                return;

            int r;
            string date = "";

            string uploadfilename = "";
            //string uploadfilenameold = "";
            //string url = "";
            string pfileName = "";
            //string pFullfileName = "";

            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            _Worker = new AbortableBackgroundWorker();

            _Worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
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

                SqlConnection con = Biz.Instance.Connection;
                if (con.State != ConnectionState.Open)
                    con.Open();
                SqlTransaction tran = con.BeginTransaction();

                r = Biz.Instance.WeewonhoiDel(con, tran, _SaupID, _Year);

                if (r < 0)
                {
                    tran.Rollback();
                    con.Close();

                    return;
                }

                string gubun;
                string opinion;
                string typegubun;
                string attendanceGubun;
                string attendancedr;
                string attendancenr;
                string attendancehr;
                string isdaesang, bunki = "";

                grdWeewonhoi.Rows.AsParallel().ForEach(row =>
                {
                    uploadfilename = row.Cells["clmUploadFileName"].Value?.ToString() ?? "";
                    date = row.Cells["clmDate"].Value?.ToString() ?? "";
                    gubun = row.Cells["clmGubun"].Value?.ToString().Trim() ?? "";
                    opinion = row.Cells["clmOpinion"].Value?.ToString().Trim() ?? "";
                    typegubun = row.Cells["clmTypeGubun"].Value?.ToString().Trim() ?? "";
                    attendanceGubun = row.Cells["clmAttendanceGubun"].Value?.ToString().Trim() ?? "";
                    attendancedr = row.Cells["clmAttendanceDr"].Value?.ToString().Trim() ?? "";
                    attendancenr = row.Cells["clmAttendanceNr"].Value?.ToString().Trim() ?? "";
                    attendancehr = row.Cells["clmAttendanceHr"].Value?.ToString().Trim() ?? "";
                    isdaesang = bool.TryParse((row.Cells["clmIsDaesang"].Value?.ToString() ?? ""), out bool result) == true ? "1" : "0"; ;
                    bunki = row.Cells["clmBunki"].Value?.ToString().Trim() ?? "";

                    r = Biz.Instance.WeewonhoiSave(con, tran, _SaupID, date, gubun, opinion, typegubun, attendanceGubun, attendancedr, attendancenr, attendancehr, isdaesang, pfileName, bunki);

                    if (r < 0)
                    {
                        tran.Rollback();
                        con.Close();

                        return;
                    }
                    else
                    {
                        tran.Commit();
                        con.Close();
                    }
                });
                mTableWeewonhoi = Biz.Instance.WeewonhoiList(_SaupID, year, (cboYear.Text == "전체" ? "2" : "1"));

                if (mTableWeewonhoi != null)
                {
                    args.Result = mTableWeewonhoi;
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
                    grdWeewonhoi.DataSource = mTableWeewonhoi;
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

        private void cboYear_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            _Year = cboYear.Text;
        }
    }
}
