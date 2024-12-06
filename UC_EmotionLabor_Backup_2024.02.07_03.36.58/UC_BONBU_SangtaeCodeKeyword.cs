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

namespace NBOGUN
{
    public partial class UC_BONBU_SangtaeCodeKeyword : UserControl
    {
        RadWaitingBar _WaitingBar;

        public UC_BONBU_SangtaeCodeKeyword()
        {
            InitializeComponent();

            Biz.Instance.SetGridViewOption(grdCode);
        }

        public RadWaitingBar WaitingBar { get => _WaitingBar; set => _WaitingBar = value; }

        private void UC_BONBU_SangtaeCodeKeyword_Load(object sender, EventArgs e)
        {

        }
    }
}
