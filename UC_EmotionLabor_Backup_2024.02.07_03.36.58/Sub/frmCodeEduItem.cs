using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace NBOGUN.Sub
{
    public partial class frmCodeEduItem : RadForm
    {
        string _Code;
        string _ItemName;
        string _ItemType;

        public string Code
        {
            get => _Code;
        }

        public string ItemName
        {
            get => _ItemName;
        }

        public string ItemType
        {
            get => _ItemType;
        }


        public frmCodeEduItem()
        {
            InitializeComponent();

            Biz.Instance.SetGridViewOption(grdEduItem);
        }

        public frmCodeEduItem(string Year) : this()
        {
            txtEduItemYear.Text = Year;
        }

        private void frmCodeEduItem_Load(object sender, EventArgs e)
        {
            btnEduItemList.PerformClick();
        }

        private void btnEduItemList_Click(object sender, EventArgs e)
        {
            string year = DateTime.Now.Year.ToString();

            try
            {
                year = Convert.ToInt32(txtEduItemYear.Text).ToString();
            }
            catch
            {
                txtEduItemYear.Text = year;
            }

            btnEduItemList.Enabled = false;

            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();
            RadWaitingBar bar = Biz.Instance.SetWaitingBar();
            bar.AssociatedControl = grdEduItem;
            bar.StartWaiting();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            worker.DoWork += delegate (object workersender, DoWorkEventArgs workere)
            {
                DataTable dt = Biz.Instance.BONBU_EduItemList(year);
                worker.ReportProgress(0, dt);
            };

            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {


                if (args.ProgressPercentage < 0)
                {
                    return;
                }

                grdEduItem.DataSource = args.UserState as DataTable;
            };

            worker.RunWorkerCompleted += delegate (object workersender, RunWorkerCompletedEventArgs workere) {
                btnEduItemList.Enabled = true;
                bar.StopWaiting();
                if (workere.Result != null && workere.Result is DataTable)
                {
                }
            };

            BeginInvoke(new MethodInvoker(worker.RunWorkerAsync));
        }

        private void grdEduItem_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewTableHeaderRowInfo || e.Row is GridViewFilteringRowInfo)
                return;

            if (e.Column.Name == "clmOK")
            {
                this.DialogResult = DialogResult.OK;

                _Code = e.Row.Cells["clmCode"].Value.ToString();
                _ItemName = e.Row.Cells["clmName"].Value.ToString();
                _ItemType = e.Row.Cells["clmItemType"].Value.ToString();

                this.Close();
            }
        }

        private void grdEduItem_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewTableHeaderRowInfo || e.Row is GridViewFilteringRowInfo)
                return;

            this.DialogResult = DialogResult.OK;

            _Code = e.Row.Cells["clmCode"].Value.ToString();
            _ItemName = e.Row.Cells["clmName"].Value.ToString();
            _ItemType = e.Row.Cells["clmItemType"].Value.ToString();

            this.Close();
        }
    }
}
