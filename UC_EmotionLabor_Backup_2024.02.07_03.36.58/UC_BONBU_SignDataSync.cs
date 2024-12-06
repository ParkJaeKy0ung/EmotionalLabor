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
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Docking;
using Telerik.Windows.Diagrams.Core;
using static System.Net.Mime.MediaTypeNames;

namespace NBOGUN
{
    public partial class UC_BONBU_SignDataSync : UserControl
    {
        AbortableBackgroundWorker _Worker;
        int _SaupID;
        string _SaupjaNum;
        string _Year;
        string _SaupjaName;
        string _Center;

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
                DataTable dt = Biz.Instance.BONBU_SignSyncVisitDate(_Center, _SaupID, _Year);
                args.Result = dt;
            };
            _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
            };
            _Worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    grdVisit.DataSource = (DataTable)args.Result;
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

        async void SetSaupjaList()
        {
            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            _Worker = new AbortableBackgroundWorker();

            _Worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                string year = cboYear.Text;

                string searchtext = txtSaupjaName.Text.Trim();

                DataTable dt = Biz.Instance.SaupjaSearch(searchtext, "1");

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

        public UC_BONBU_SignDataSync()
        {
            InitializeComponent();

            Biz.Instance.SetGridViewOption(grdSaupja);
            Biz.Instance.SetGridViewOption(grdVisit);

            Biz.Instance.SetDropDownList(cboYear);

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

            for (int i = DateTime.Now.Year + 1; i >= 2009; i--)
            {
                cboYear.Items.Add(i.ToString());
            }

            cboYear.Text = DateTime.Now.Year.ToString();

            Biz.Instance.DHCenterChanged += Instance_DHCenterChanged;
        }

        private void Instance_DHCenterChanged(object sender, EventArgs e)
        {
            _Center = Biz.Instance.DHCenter;

            //Biz.Show(this, _Center);
        }

        private void UC_BONBU_SignDataSync_Load(object sender, EventArgs e)
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

        private void cboYear_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            _Year = cboYear.Text;
        }

        private void grdSaupja_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Column.Name == "clmSelect")
            {
                _SaupID = Convert.ToInt32(e.Row.Cells["clmSaupID"].Value);
                _SaupjaNum = e.Row.Cells["clmSaupjanum"].Value.ToString();
                _Center = e.Row.Cells["clmCenter"].Value?.ToString();
                _SaupjaName = e.Row.Cells["clmSaupjaName"].Value.ToString();

                pvpSaupja.Text = e.Row.Cells["clmSaupjaName"].Value.ToString() + " (" + _Year + ")";

                this.radDock1.AutoHideWindow(toolWindow1);

                RefreshData();
            }

        }

        private void grdSaupja_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            _SaupID = Convert.ToInt32(e.Row.Cells["clmSaupID"].Value);
            _SaupjaNum = e.Row.Cells["clmSaupjanum"].Value.ToString();
            _Center = e.Row.Cells["clmCenter"].Value?.ToString();
            _SaupjaName = e.Row.Cells["clmSaupjaName"].Value.ToString();

            pvpSaupja.Text = e.Row.Cells["clmSaupjaName"].Value.ToString() + " (" + _Year + ")";

            this.radDock1.AutoHideWindow(toolWindow1);

            RefreshData();
        }

        private void grdVisit_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            waitingBar.StartWaiting();
            grdVisit.Rows.AsParallel().ForEach(row => {
                string v1 = row.Cells["clmRPTVisitor"].Value?.ToString() ?? "";
                string v2 = row.Cells["clmRPTDamdang"].Value?.ToString() ?? "";
                string v3 = row.Cells["clmSignVisitor"].Value?.ToString() ?? "";
                string v4 = row.Cells["clmSignDamdang"].Value?.ToString() ?? "";

                if(v1 != "")
                    row.Cells["clmImgRPTVisitor"].Value  = Biz.Base64ToImage(row.Cells["clmRPTVisitor"].Value?.ToString() ?? "");
                if (v2 != "")
                    row.Cells["clmImgRPTDamdang"].Value  = Biz.Base64ToImage(row.Cells["clmRPTDamdang"].Value?.ToString() ?? "");
                if (v3 != "")
                    row.Cells["clmImgSignVisitor"].Value = Biz.Base64ToImage(row.Cells["clmSignVisitor"].Value?.ToString() ?? "");
                if (v4 != "")
                    row.Cells["clmImgSignDamdang"].Value = Biz.Base64ToImage(row.Cells["clmSignDamdang"].Value?.ToString() ?? "");
            });
            waitingBar.StopWaiting();
        }

        async private void grdVisit_CellClick(object sender, GridViewCellEventArgs e)
        {
            if(e.Row is GridViewTableHeaderRowInfo || e.Row is GridViewFilteringRowInfo) {
                return;
            }

            if(e.Column.Name == "clmSync")
            {
                RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
                waitingBar.AssociatedControl = this;
                _Worker = new AbortableBackgroundWorker();

                _Worker.DoWork += delegate (object obj, DoWorkEventArgs args)
                {
                    int r;
                    SqlConnection con = Biz.Instance.Connection;
                    if (con.State != ConnectionState.Open) con.Open();
                    SqlTransaction tran = con.BeginTransaction();
                    r = Biz.Instance.BONBU_SignSync(con, tran, _Center, _SaupID, e.Row.Cells["clmVisitDate"].Value.ToString(), e.Row.Cells["clmVisitor"].Value.ToString());
                    if(r < 0)
                    {
                        tran.Rollback();
                        con.Close();
                        return;
                    }
                    tran.Commit();
                    con.Close();
                    DataTable dt = Biz.Instance.BONBU_SignSyncVisitDate(_Center, _SaupID, _Year);
                    args.Result = dt;
                };
                _Worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
                {
                };
                _Worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
                {
                    if (args.Result != null && args.Result is DataTable)
                    {
                        grdVisit.DataSource = (DataTable)args.Result;
                        Biz.Show(this, "동기화에 성공했습니다.");
                    }
                    else
                    {
                        Biz.Show(this, "동기화에 실패했습니다.");
                    }

                    waitingBar.StopWaiting();
                };

                waitingBar.StartWaiting();
                await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
                BeginInvoke(new MethodInvoker(_Worker.RunWorkerAsync));
            }
        }

        private void pvMain_SelectedPageChanging(object sender, RadPageViewCancelEventArgs e)
        {
            if (e.Page == pvpSaupja)
            {
                radDock1.ShowAutoHidePopup(toolWindow1);
                e.Cancel = true;
            }
        }
    }
}
