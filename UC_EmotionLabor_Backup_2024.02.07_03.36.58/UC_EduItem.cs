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
    public partial class UC_EduItem : UserControl
    {
        int _SaupID;
        string _VisitDate = DateTime.Now.ToString("yyyy-MM-dd");

        public string VisitDate
        {
            set => _VisitDate = value;
        }

        DataTable mTableEduItem;

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

        async public void RefreshData()
        {
            //기존 데이터 삭제하기
            string yearitem = cboEduItemYear.Text;
            if (_VisitDate != "")
            {
                //방문일을 선택했고 검색연도가 같다면 방문일을 기준으로 검색
                if (_VisitDate.Substring(0, 4) == yearitem.Substring(0, 4))
                    yearitem = _VisitDate;
                else
                {
                    //방문일을 선택했고 검색연도가 다르다면 검색연도를 기준으로 검색
                }
            }
            else if (_VisitDate == "" && yearitem == DateTime.Now.Year.ToString())
            {
                //방문일을 선택 안했지만 검색연도가 올해라면 오늘을 기준으로 검색
                yearitem = DateTime.Now.ToString("yyyy-MM-dd");
            }

            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();

            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                DataSet dsItem = Biz.Instance.AnjunBogunEducationItemList(_SaupID, yearitem, "", "4");

                if (dsItem.Tables.Count == 1)
                {
                    mTableEduItem = dsItem.Tables[0];
                }

                if (mTableEduItem != null)
                {
                    args.Result = mTableEduItem;
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
                    grdEduItem.DataSource = mTableEduItem;
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
        public UC_EduItem()
        {
            InitializeComponent();

            Biz.Instance.SetGridViewOption(grdEduItem);

            Biz.Instance.SetDropDownList(cboEduItemYear);

            lblTitle.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            GridViewComboBoxColumn col4 = grdEduItem.Columns["clmGubun"] as GridViewComboBoxColumn;
            col4.DisplayMember = "Name";
            col4.ValueMember = "Code";
            col4.DataSource = Biz.Instance.GetCodeEduGubun;
            //col4.DataSource = Biz.This().DHCodeList("자료제공구분");//

            GridViewComboBoxColumn col5 = grdEduItem.Columns["clmItemType"] as GridViewComboBoxColumn;
            col5.DisplayMember = "Name";
            col5.ValueMember = "Code";
            col5.DataSource = Biz.Instance.GetCodeEduItemType;//
            //col5.DataSource = Biz.This().DHCodeList("보급형태");//

            DataTable dtIsUsage = new DataTable();
            dtIsUsage.Columns.Add("Name", typeof(string));
            DataRow row = dtIsUsage.NewRow();
            row[0] = "";
            dtIsUsage.Rows.Add(row);
            row = dtIsUsage.NewRow();
            row[0] = "○";
            dtIsUsage.Rows.Add(row);
            row = dtIsUsage.NewRow();
            row[0] = "Ⅹ";
            dtIsUsage.Rows.Add(row);

            GridViewComboBoxColumn colIsUsage = grdEduItem.Columns["clmIsUsage"] as GridViewComboBoxColumn;
            colIsUsage.DisplayMember = "Name";
            colIsUsage.ValueMember = "Name";
            colIsUsage.DataSource = dtIsUsage;

            for (int i = DateTime.Now.Year; i > 2019; i--)
            {
                //cboExcelYear.Items.Add(i.ToString());
                cboEduItemYear.Items.Add(i.ToString());
            }

            cboEduItemYear.SelectedIndex = 0;

        }
        public UC_EduItem(int SaupID, string VisitDate) :this ()
        {
            
        }

        private void btnEduItemAdd_Click(object sender, EventArgs e)
        {
            //추가
            DataRow row = this.mTableEduItem.NewRow();
            //row["Odx"] = KihaConvert.ToInt(mTableEduItem.Compute("Max(Odx)", string.Empty)) + 1;
            //DH5301
            row["Gubun"] = "DH5301";
            row["Odx"] = -1;
            row["ItemDate"] = _VisitDate;
            row["VisitDate"] = _VisitDate;
            row["Visitor"] = "";
            row["VisitorName"] = "";
            this.mTableEduItem.Rows.InsertAt(row, 0);
            this.mTableEduItem.AcceptChanges();
        }

        private void btnEduItemDel_Click(object sender, EventArgs e)
        {
            if (mTableEduItem == null || grdEduItem.SelectedRows.Count == 0 || grdEduItem.SelectedRows[0] == null)
                return;

            this.grdEduItem.Rows.Remove(grdEduItem.SelectedRows[0]);

            mTableEduItem.AcceptChanges();
        }

        private void btnEduItemRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void btnEduItemExcel_Click(object sender, EventArgs e)
        {
            if (_SaupID < 0)
            {
                Biz.Show(this, "사업장을 선택해 주십시오.");
                return;
            }

            if (grdEduItem.DataSource == null)
            {
                Biz.Show(this, "엑셀전환을 할 수 없습니다.");
                return;
            }

            Biz.Instance.SaveExcel((DataTable)grdEduItem.DataSource);
        }

        async private void btnEduItemReport_Click(object sender, EventArgs e)
        {
            string yearmon = cboEduItemYear.Text.Trim();
            //int saupid = -1;

            int r = -1;

            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();

            SqlConnection con = Biz.Instance.Connection;
            if (con.State != ConnectionState.Open)
                con.Open();
            SqlTransaction tran = con.BeginTransaction();

            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                r = Biz.Instance.RPT_ItemDel(con, tran, yearmon);

                if (r < 0)
                    return;

                r = Biz.Instance.RPT_ItemSave(con, tran, yearmon, _SaupID, _Saupjanum);

                if (r < 0)
                    return;


                args.Result = "성공";
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
            };
            worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null)
                {
                    string[] p = new string[] { Biz.Instance.DHCenter, yearmon, Biz.Instance.UserID };

                    string url = Biz.ReportHtml5("NBOGUN_AnjunBogunEducationItem", p);

                    try
                    {
                        Clipboard.SetText(url);
                    }
                    catch
                    {

                    }

                    Biz.UrlStart(url, Biz.Browser.Edge);
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

        //저장
        async private void btnEduItemSave_Click(object sender, EventArgs e)
        {
            string year = cboEduItemYear.Text;
            try
            {
                year = Convert.ToInt32(cboEduItemYear.Text).ToString();
            }
            catch
            {
                cboEduItemYear.Text = DateTime.Now.Year.ToString();
                year = cboEduItemYear.Text;
            }

            DialogResult result = Biz.Show(this, year + "년도 기록을 초기화 후 저장합니다. 계속 하시겠습니까?", "알림", MessageBoxButtons.OKCancel);

            if (result != DialogResult.OK)
                return;

            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                SqlConnection conitem = Biz.Instance.Connection;
                if (conitem.State != ConnectionState.Open)
                    conitem.Open();
                SqlTransaction tranitem = conitem.BeginTransaction();

                int ritem = Biz.Instance.AnjunBogunEducationItemDel(conitem, tranitem,_SaupID, year);

                if (ritem < 0)
                {
                    tranitem.Rollback();
                    conitem.Close();

                    worker.ReportProgress(-1, "기존 데이터 삭제에 실패했습니다.");
                    args.Cancel = true;
                    worker.CancelAsync();
                    return;
                }
                //신규 데이터 저장하기 
                string itemdate = "";
                string gubun = "";
                string itemName = "";
                string itemCode;
                int cnt;
                string visitdate = "";
                string visitro = "";
                //int odx;
                string itemtype = "";
                string isUsage = "";
                int r;

                grdEduItem.Rows.AsParallel().ForEach(row => { 
                    try
                    {
                        itemdate = Convert.ToDateTime(row.Cells["clmItemDate"].Value?.ToString()).ToString("yyyy-MM-dd");
                        cnt = Convert.ToInt32(row.Cells["clmCnt"].Value);
                        visitdate = (row.Cells["clmVisitDate"].Value?.ToString() ?? "");
                        visitro = (row.Cells["clmVisitor"].Value?.ToString() ?? "");

                        int.TryParse(row.Cells["clmOdx"].Value?.ToString() ?? "", out cnt);

                        gubun = (row.Cells["clmGubun"].Value?.ToString() ?? "");
                        itemName = (row.Cells["clmItemName"].Value?.ToString() ?? "");
                        itemCode = (row.Cells["clmItemCode"].Value?.ToString() ?? "");

                        itemtype = (row.Cells["clmItemType"].Value?.ToString() ?? "");

                        isUsage = row.Cells["clmIsUsage"].Value?.ToString() ?? "";

                        r = Biz.Instance.AnjunBogunEducationItemSave(conitem, tranitem, _SaupID, itemdate, -1, gubun, itemName, itemCode, cnt, visitdate, visitro, itemtype, isUsage);

                        if (r < 0)
                        {
                            tranitem.Rollback();
                            conitem.Close();

                            worker.ReportProgress(-1, "기존 데이터 저장에 실패했습니다.");
                            args.Cancel = true;
                            worker.CancelAsync();
                            return;
                        }
                        else
                        {
                            worker.ReportProgress(-1, itemdate + " 저장 성공");
                            System.Threading.Thread.Sleep(10);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogManager.WriteLog(ex.Message);
                    }
                });
                //for (int i = 0; i < grdEduItem.Rows.Count; i++)
                //{
                //    itemdate = DateTime.TryParse(grdEduItem.Rows[i].Cells["clmItemDate"].Value?.ToString(), out result.ToString("yyyy-MM-dd") => itemdate);
                //    visitdate = (grdEduItem.Rows[i].Cells["clmVisitDate"].Value?.ToString() ?? "");
                //    visitro = (grdEduItem.Rows[i].Cells["clmVisitor"].Value?.ToString() ?? "");
                //    cnt = KihaConvert.ToInt(grdEduItem.Rows[i].Cells["clmCnt"].Value);

                //    if (KihaConvert.ToDate(itemdate) == "" || cnt < 1)
                //        continue;

                //    gubun = (grdEduItem.Rows[i].Cells["clmGubun"].Value?.ToString() ?? "");
                //    itemName = (grdEduItem.Rows[i].Cells["clmItemName"].Value?.ToString() ?? "");
                //    itemCode = (grdEduItem.Rows[i].Cells["clmItemCode"].Value?.ToString() ?? "");

                //    odx = KihaConvert.ToInt(grdEduItem.Rows[i].Cells["clmOdx"].Value);
                //    itemtype = (grdEduItem.Rows[i].Cells["clmItemType"].Value?.ToString() ?? "");

                //    isUsage = grdEduItem.Rows[i].Cells["clmIsUsage"].Value?.ToString() ?? "";

                //    r = Biz.Instance.AnjunBogunEducationItemSave(conitem, tranitem, _SaupID, itemdate, -1, gubun, itemName, itemCode, cnt, visitdate, visitro, itemtype, isUsage);

                //    if (r < 0)
                //    {
                //        tranitem.Rollback();
                //        conitem.Close();

                //        worker.ReportProgress(-1, "기존 데이터 저장에 실패했습니다.");
                //        args.Cancel = true;
                //        worker.CancelAsync();
                //        return;
                //    }
                //    else
                //    {
                //        worker.ReportProgress(-1, itemdate + " 저장 성공");
                //        System.Threading.Thread.Sleep(10);
                //    }
                //}

                tranitem.Commit();
                conitem.Close();

                //데이터 저장되었는지 새로고침
                DataSet dsEduItemS = Biz.Instance.AnjunBogunEducationItemList(_SaupID, _VisitDate, "", "4");

                if (dsEduItemS.Tables.Count == 1)
                {
                    mTableEduItem = dsEduItemS.Tables[0];

                    worker.ReportProgress(-1, "자료제공내역 불러오기");
                    args.Result = mTableEduItem;
                    System.Threading.Thread.Sleep(10);
                }
                else
                {
                    args.Cancel = true;
                    worker.CancelAsync();
                }
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                if (args.ProgressPercentage < 0)
                {
                    waitingBar.Text = args.UserState.ToString();

                    return;
                }
            };
            worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    //DataTable dt = (DataTable)args.Result;
                    grdEduItem.DataSource = mTableEduItem;
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

        private void grdEduItem_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewTableHeaderRowInfo || e.Row is GridViewFilteringRowInfo)
                return;

            if (e.Column.Name != "clmFind")
            {
                return;
            }

            string year = DateTime.Now.Year.ToString();

            try
            {
                year = Convert.ToInt32(e.Row.Cells["clmItemDate"].Value.ToString().Substring(0, 4)).ToString();
            }
            catch
            {

            }

            Sub.frmCodeEduItem frmEduItem = new Sub.frmCodeEduItem(year);
            frmEduItem.StartPosition = FormStartPosition.CenterParent;
            if (frmEduItem.ShowDialog() == DialogResult.OK)
            {
                e.Row.Cells["clmItemName"].Value = frmEduItem.ItemName;
                e.Row.Cells["clmItemCode"].Value = frmEduItem.Code;
                e.Row.Cells["clmItemType"].Value = frmEduItem.ItemType;
            }
            frmEduItem.Dispose();
        }

        private void grdEduItem_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewTableHeaderRowInfo || e.Row is GridViewFilteringRowInfo)
                return;

            if (e.Column.Name == "clmItemCode")
            {
                string code = e.Value.ToString().Replace("_", "");

                DataTable dt = Biz.Instance.PlanEduItemCode("", "");

                DataRow[] rows = dt.Select("Code = '" + code + "'");

                if (rows != null && rows.Length == 1)
                {
                    //clmItemName
                    e.Row.Cells["clmItemName"].Value = rows[0]["Name"];
                    e.Row.Cells["clmItemType"].Value = rows[0]["ItemType"];
                }
            }
        }

        private void grdEduItem_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.Row.Cells["clmDataGubun"].Value != null && e.Row.Cells["clmDataGubun"].Value.ToString() == "Planed")
            {
                e.CellElement.BackColor = Color.FromArgb(208, 182, 164);
            }
            else if (e.Row.Cells["clmDataGubun"].Value != null && e.Row.Cells["clmDataGubun"].Value.ToString() == "Saved")
            {
                e.CellElement.BackColor = Color.FromArgb(214, 216, 201);
            }
            else
            {
                e.CellElement.BackColor = Color.White;
            }
        }
    }
}
