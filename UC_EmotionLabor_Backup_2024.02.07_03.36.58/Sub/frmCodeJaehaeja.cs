using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace NBOGUN.Sub
{
    public partial class frmCodeJaehaeja : Telerik.WinControls.UI.RadForm
    {
        bool mIsOk = false;

        string mCode;
        string mCodeName;

        DataTable mTableCode;

        public string Code
        { get { return mCode; } }

        public string CodeName
        { get { return mCodeName; } }

        public frmCodeJaehaeja()
        {
            InitializeComponent();

            Biz.Instance.SetGridViewOption(grd1);
            Biz.Instance.SetGridViewOption(grd2);
            Biz.Instance.SetGridViewOption(grd3);
            Biz.Instance.SetGridViewOption(grd4);
            Biz.Instance.SetGridViewOption(grd5);
            Biz.Instance.SetGridViewOption(grd6);
        }

        private void frmCodeJaehaeja_Load(object sender, EventArgs e)
        {
            mTableCode = Biz.Instance.CODE_Jaehae();

            DataRow[] row = mTableCode.Select("Gubun1 = '질병재해' AND Gubun2 = '사망'");

            if (row != null && row.Length > 0)
                grd1.DataSource = row.CopyToDataTable();
            else
                grd1.DataSource = null;

            row = mTableCode.Select("Gubun1 = '질병재해' AND Gubun2 = '직업병'");

            if (row != null && row.Length > 0)
                grd2.DataSource = row.CopyToDataTable();
            else
                grd2.DataSource = null;

            row = mTableCode.Select("Gubun1 = '질병재해' AND Gubun2 = '작업관련성질병'");

            if (row != null && row.Length > 0)
                grd3.DataSource = row.CopyToDataTable();
            else
                grd3.DataSource = null;

            row = mTableCode.Select("Gubun1 = '업무상사고' AND Gubun2 = '사망'");

            if (row != null && row.Length > 0)
                grd4.DataSource = row.CopyToDataTable();
            else
                grd4.DataSource = null;

            row = mTableCode.Select("Gubun1 = '업무상사고' AND Gubun2 = '부상'");

            if (row != null && row.Length > 0)
                grd5.DataSource = row.CopyToDataTable();
            else
                grd5.DataSource = null;

            row = mTableCode.Select("Gubun1 = '기타재해' AND Gubun2 = '기타'");

            if (row != null && row.Length > 0)
                grd6.DataSource = row.CopyToDataTable();
            else
                grd6.DataSource = null;
        }

        private void grd1_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.Row is GridViewTableHeaderRowInfo)
                return;

            if (e.Column.Name == "clmName")
            {
                mCode = e.Row.Cells["clmCode"].Value.ToString();

                DataRow[] row = mTableCode.Select("Code = '" + mCode + "'");

                if (row != null && row.Length == 1)
                {
                    mCodeName = row[0]["Gubun1"].ToString() + "/" + row[0]["Gubun2"].ToString() + "/" + row[0]["Gubun3"].ToString();
                    mIsOk = true;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void grd1_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.Row is GridViewTableHeaderRowInfo)
                return;

            if (e.Column.Name == "clmSelect")
            {
                mCode = e.Row.Cells["clmCode"].Value.ToString();

                DataRow[] row = mTableCode.Select("Code = '" + mCode + "'");

                if (row != null && row.Length == 1)
                {
                    mCodeName = row[0]["Gubun1"].ToString() + "/" + row[0]["Gubun2"].ToString() + "/" + row[0]["Gubun3"].ToString();
                    mIsOk = true;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void grd4_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is GridHeaderCellElement)
            {
                e.CellElement.ForeColor = Color.Red;
            }
        }
    }
}
