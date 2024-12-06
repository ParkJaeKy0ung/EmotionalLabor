using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;

namespace NBOGUN.Sub
{
    public partial class frmGongjung : RadForm
    {
        public enum ViewGubun { 복수공정선택, 단일공정선택 };
        private ViewGubun _ViewGubun = ViewGubun.복수공정선택;

        int _SaupID;
        string _VisitDate;

        string _GJs = "";
        string _GJNames = "";

        string _SiteName = "";
        int _SiteNO;

        int _GJNO;
        string _GJName = "";

        public string GJs { get => _GJs; set => _GJs = value; }
        public string GJNames { get => _GJNames; set => _GJNames = value; }
        public string SiteName { get => _SiteName; set => _SiteName = value; }
        public int SiteNO { get => _SiteNO; set => _SiteNO = value; }
        public int GJNO { get => _GJNO; set => _GJNO = value; }
        public string GJName { get => _GJName; set => _GJName = value; }

        void SetJakupResultGrouping()
        {
            grdGJ.EnableGrouping = true;
            grdGJ.ShowGroupPanel = false;

            GroupDescriptor groupSite = new GroupDescriptor();
            groupSite.GroupNames.Add("clmSiteName", ListSortDirection.Descending);
            //GroupDescriptor groupGJ = new GroupDescriptor();
            //groupGJ.GroupNames.Add("clmGJName", ListSortDirection.Descending);
            //GroupDescriptor groupPos = new GroupDescriptor();
            //groupPos.GroupNames.Add("clmPosName", ListSortDirection.Descending);

            grdGJ.GroupDescriptors.Clear();
            grdGJ.GroupDescriptors.Add(groupSite);
            //grdGJ.GroupDescriptors.Add(groupGJ);
            //grdGJ.GroupDescriptors.Add(groupPos);
            grdGJ.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            grdGJ.AutoExpandGroups = true;
        }

        public frmGongjung()
        {
            InitializeComponent();

            Biz.Instance.SetGridViewOption(grdGJ);

            SetJakupResultGrouping();
        }

        public frmGongjung(int SaupID, string VisitDate, string GJs, ViewGubun viewGubun = ViewGubun.복수공정선택) : this()
        {
            _SaupID = SaupID;
            _VisitDate = VisitDate;
            //공정 체크박스를 위해 만듦
            _GJs = GJs;

            _ViewGubun = viewGubun;

            switch (_ViewGubun)
            {
                case ViewGubun.복수공정선택:
                    grdGJ.Columns["clmIsSelect"].IsVisible = true;
                    break;
                case ViewGubun.단일공정선택:
                    grdGJ.Columns["clmIsSelect"].IsVisible = false;
                    break;
            }
        }

        private void frmGongjung_Load(object sender, EventArgs e)
        {

        }

        async private void frmGongjung_Shown(object sender, EventArgs e)
        {
            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = grdGJ;
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                DataTable dt = Biz.Instance.JakupGongjungList(_SaupID, _VisitDate, -1, "3");
                worker.ReportProgress(0, dt);
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
                try
                {
                    if (args.UserState != null && args.UserState is DataTable)
                    {
                        grdGJ.DataSource = args.UserState as DataTable;
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
                    //this.cboJido.Text = "";
                    //grdInwon.DataSource = args.Result as DataTable;
                }
                waitingBar.StopWaiting();
            };
            waitingBar.StartWaiting();
            await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
            BeginInvoke(new MethodInvoker(worker.RunWorkerAsync));
        }

        private void frmGongjung_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_ViewGubun == ViewGubun.복수공정선택)
            {
                DialogResult resultDialog = Biz.Show(this, "공정을 변경하시겠습니까?", "알림", MessageBoxButtons.YesNoCancel);

                if (resultDialog == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }

                this.DialogResult = resultDialog;

                //_GJNames = "";
                if (resultDialog == DialogResult.Yes)
                {
                    SetGonjung();
                }
                if (this.DialogResult == DialogResult.No)
                {
                    e.Cancel = false;
                }
            }
        }

        private void SetGonjung()
        {
            string result = "";
            string resultno = "";

            var groupedGJs = grdGJ.Rows.Where(x => x.Cells["clmSelect"].Value != null && (bool)x.Cells["clmSelect"].Value)
                    .GroupBy(x => x.Cells["clmSiteNo"].Value.ToString() + "%" + x.Cells["clmSiteName"].Value.ToString());

            foreach (var siteGroup in groupedGJs)
            {
                string _SiteName = "[" + siteGroup.Key.Substring(siteGroup.Key.IndexOf("%") + 1) + "]";
                string _SiteNO = siteGroup.Key.Substring(0, siteGroup.Key.IndexOf("%"));

                result += _SiteName;
                _GJNames = "";
                var rows = grdGJ.Rows.Where(x => x.Cells["clmSelect"].Value != null
                    && (bool)x.Cells["clmSelect"].Value && x.Cells["clmSiteNo"].Value.ToString() == siteGroup.Key.Substring(0, siteGroup.Key.IndexOf("%")));
                foreach (GridViewDataRowInfo row in rows)
                {
                    string siteno = row.Cells["clmSiteNO"].Value.ToString().PadLeft(3, '0');
                    string gjno = row.Cells["clmGJNO"].Value.ToString().PadLeft(3, '0');

                    resultno += (resultno == "" ? "" : ",") + siteno + "-" + gjno.ToString();

                    _GJs += siteno.ToString() + "-" + gjno.ToString() + "/";

                    _GJNames += (_GJNames == "" ? "" : ",") + row.Cells["clmGJName"].Value.ToString();
                }

                result += _GJNames;
            }

            _GJNames = result;
            _GJs = resultno;
        }

        private void grdGJ_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            if (e.Column.Name == "clmSelect")
            {
                RadCheckBoxEditor _Checkeditor = e.ActiveEditor as RadCheckBoxEditor;
                _Checkeditor.ValueChanged -= new EventHandler(editor_ValueChanged);
                _Checkeditor.ValueChanged += new EventHandler(editor_ValueChanged);
            }
        }

        void editor_ValueChanged(object sender, EventArgs e)
        {
            grdGJ.EndEdit();
        }

        private void grdGJ_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            //기존 공정이 있을경우 해당 공정을 체크함
            if (_GJs != "")
            {
                var list = _GJs.Split(',').Where(x => x.ToString() != "");

                if (list != null)
                {
                    foreach (var item in list)
                    {
                        grdGJ.Rows.Where(x => x.Cells["clmSiteNO"].Value.ToString().PadLeft(3, '0') == item.Split('-')[0] && x.Cells["clmGJNO"].Value.ToString().PadLeft(3, '0') == item.Split('-')[1]).FirstOrDefault().Cells["clmSelect"].Value = true;
                    }
                }
                //string siteno = row.Cells["clmSiteNO"].Value.ToString().PadLeft(3, '0');
                //string gjno = row.Cells["clmGJNO"].Value.ToString().PadLeft(3, '0');

                //var groupedGJs = grdGJ.Rows.Where(x => x.Cells["clmSelect"].Value != null && (bool)x.Cells["clmSelect"].Value)
                //        .GroupBy(x => x.Cells["clmSiteNo"].Value.ToString() + "%" + x.Cells["clmSiteName"].Value.ToString());
            }
        }

        private void grdGJ_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewTableHeaderRowInfo || e.Row is GridViewHierarchyRowInfo || e.Row is GridViewFilteringRowInfo)
                return;

            if (_ViewGubun == ViewGubun.단일공정선택)
            {
                _SiteNO = Convert.ToInt16(e.Row.Cells["clmSiteNO"].Value);
                _SiteName = e.Row.Cells["clmSiteName"].Value?.ToString() ?? "";

                _GJNO = Convert.ToInt16(e.Row.Cells["clmGJNO"].Value);
                _GJName = e.Row.Cells["clmGJName"].Value?.ToString() ?? "";
            }
            else if (_ViewGubun == ViewGubun.복수공정선택)
            {
                e.Row.Cells["clmSelect"].Value = true;

                SetGonjung();
            }

            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
    }
}
