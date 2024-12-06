using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI.Docking;

namespace NBOGUN
{
    public partial class UC_DockBasic : UserControl
    {
        public UC_DockBasic()
        {
            InitializeComponent();
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
    }
}
