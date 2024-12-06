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
    public partial class frmSaupjaUpmuDamdang : RadForm
    {
        int _SaupID;
        string _Yearmon;

        string _DamdangName;
        string _DamdangTel;
        string _DamdangEMail;


        public frmSaupjaUpmuDamdang()
        {
            InitializeComponent();
        }

        public int SaupID { get => _SaupID; set => _SaupID = value; }
        public string Yearmon { get => _Yearmon; set => _Yearmon = value; }
        public string DamdangName { get => _DamdangName; set => _DamdangName = value; }
        public string DamdangTel { get => _DamdangTel; set => _DamdangTel = value; }
        public string DamdangEMail { get => _DamdangEMail; set => _DamdangEMail = value; }

        private void frmSaupjaUpmuDamdang_Load(object sender, EventArgs e)
        {
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();

            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                UC_SaupjaUpmuDamdang uC_EduItem = new UC_SaupjaUpmuDamdang();

                args.Result = uC_EduItem;
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
            };
            worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is UC_SaupjaUpmuDamdang)
                {
                    UC_SaupjaUpmuDamdang con = ((UC_SaupjaUpmuDamdang)args.Result);
                    con.SaupID = _SaupID;
                    con.Yearmon = _Yearmon;
                    con.Selected += Con_Selected;
                    con.ViewType = UC_SaupjaUpmuDamdang.ViewGubun.Selection;
                    this.Controls.Add(con);
                    con.Dock = DockStyle.Fill;
                    con.RefreshData();
                }
            };

            BeginInvoke(new MethodInvoker(worker.RunWorkerAsync));
        }

        private void Con_Selected(object sender, GridViewRowInfo e)
        {
            _DamdangName = e.Cells["clmName"].Value?.ToString();
            _DamdangTel = e.Cells["clmTel"].Value?.ToString();
            _DamdangEMail = e.Cells["clmEMail"].Value?.ToString();

            this.DialogResult = DialogResult.OK;    

            this.Close();
        }
    }
}
