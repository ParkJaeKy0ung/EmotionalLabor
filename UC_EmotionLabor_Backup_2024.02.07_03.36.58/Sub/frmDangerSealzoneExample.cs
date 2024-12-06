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
    public partial class frmDangerSealzoneExample : RadForm
    {
        string _Bigo1;
        string _Bigo2;
        string _Url;

        public frmDangerSealzoneExample(string Bigo1, string Bigo2, string Url)
        {
            InitializeComponent();

            _Bigo1 = Bigo1;
            _Bigo2 = Bigo2;
            _Url = Url;
        }

        private void frmDangerSealzoneExample_Load(object sender, EventArgs e)
        {
            txtBigo1.Text = _Bigo1;
            txtBigo2.Text = _Bigo2;

            if(_Url != "")
            {
                pictureBox1.LoadAsync("https://ohis.kiha21.or.kr/FileUpload/BOGUN/" + _Url);
            }
            //string url = "https://ohis.kiha21.or.kr/FileUpload/BOGUN/" + uploadfilename;

            this.pictureBox1.Focus();
        }

        private void txtBigo1_Enter(object sender, EventArgs e)
        {
            this.Focus();
            this.pictureBox1.Focus();
        }

        private void txtBigo2_Enter(object sender, EventArgs e)
        {
            this.pictureBox1.Focus();
        }
    }
}
