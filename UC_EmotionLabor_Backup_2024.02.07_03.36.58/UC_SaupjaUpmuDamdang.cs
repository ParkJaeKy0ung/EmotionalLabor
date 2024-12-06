using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;
using Telerik.WinControls.UI;
using System.Data.SqlClient;
using Telerik.WinControls.Keyboard;

namespace NBOGUN
{
    public partial class UC_SaupjaUpmuDamdang : UserControl
    {
        public event EventHandler<GridViewRowInfo> Selected;

        public enum ViewGubun { Selection, Manage };

        ViewGubun _ViewGubun = ViewGubun.Manage;

        string _SelectedName = "";
        string _SelectedTel = "";
        string _SelectedEMail = "";

        int _SaupID;
        public int SaupID
        {
            set { _SaupID = value; }
        }

        string _Yearmon = "";
        public string Yearmon
        {
            set  { _Yearmon = value; txtYearmon.Text = value; }
        }

        public ViewGubun ViewType { get => _ViewGubun; set { _ViewGubun = value;
                if (_ViewGubun == ViewGubun.Selection)
                {
                    btnUpmuDamdangAdd.Visible = false;
                    btnUpmuDamdangCopy.Visible = false;
                    btnUpmuDamdangSave.Visible = false;

                    grdUpmuDamdang.Columns["clmDel"].IsVisible = false;
                    grdUpmuDamdang.Columns["clmSelect"].IsVisible = true;
                    grdUpmuDamdang.AllowEditRow = false;
                }
            } }

        public UC_SaupjaUpmuDamdang()
        {
            InitializeComponent();

            Biz.Instance.SetGridViewOption(grdUpmuDamdang);

            if (Biz.Instance.Yearmon != "")
            {
                txtYearmon.Text = Biz.Instance.Yearmon;
            }

            GridViewComboBoxColumn col = grdUpmuDamdang.Columns["clmCode"] as GridViewComboBoxColumn;

            col.DisplayMember = "CodeName";
            col.ValueMember = "Code";
            col.DataSource = Biz.Instance.GetUpmuDamdang;
        }

        async public void RefreshData()
        {
            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();

            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                DataTable dt4 = Biz.Instance.RPT_SaupjaCardUpmuDamdangList(_SaupID, txtYearmon.Text);
                args.Result = dt4;
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
            };
            worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    grdUpmuDamdang.DataSource = (DataTable)args.Result;
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

        private void btnEduItemRefresh_Click(object sender, EventArgs e)
        {
            if (_SaupID < 0)
            {
                Biz.Show(this, "사업장을 선택해 주십시오");
                return;
            }

            _Yearmon = txtYearmon.Text;

            try
            {
                _Yearmon = Convert.ToDateTime(txtYearmon.Text + "-01").ToString("yyyy-MM");
            }
            catch
            {
                txtYearmon.Text = DateTime.Now.ToString("yyyy-MM");
                _Yearmon = txtYearmon.Text;
            }

            RefreshData();
        }

        private void btnUpmuDamdangAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = ((DataTable)grdUpmuDamdang.DataSource);

            DataRow row = dt.NewRow();
            row["Code"] = "DH2701";
            row["CodeName"] = "보건";
            row["IsSelect"] = "1";
            dt.Rows.InsertAt(row, 0);
        }

        private void btnUpmuDamdangCopy_Click(object sender, EventArgs e)
        {
            int c1 = 0, c2 = 0;

            for (int i = 0; i < grdUpmuDamdang.Rows.Count; i++)
            {
                if (grdUpmuDamdang.Rows[i].Cells["clmCode"].Value.ToString() == "DH2701")
                    c1++;

                if (grdUpmuDamdang.Rows[i].Cells["clmCode"].Value.ToString() == "DH2702")
                    c2++;
            }

            if (c1 != 1 || c2 != 1)
                return;

            //clmName
            //clmSosok
            //clmTel
            //clmDirectNumber
            //clmEMail

            string clmName1 = grdUpmuDamdang.Rows[0].Cells["clmName"].Value.ToString().Trim();
            string clmSosok1 = grdUpmuDamdang.Rows[0].Cells["clmSosok"].Value.ToString().Trim();
            string clmTel1 = grdUpmuDamdang.Rows[0].Cells["clmTel"].Value.ToString().Trim();
            string clmDirectNumber1 = grdUpmuDamdang.Rows[0].Cells["clmDirectNumber"].Value.ToString().Trim();
            string clmEMail1 = grdUpmuDamdang.Rows[0].Cells["clmEMail"].Value.ToString().Trim();

            string clmName2 = grdUpmuDamdang.Rows[1].Cells["clmName"].Value.ToString().Trim();
            string clmSosok2 = grdUpmuDamdang.Rows[1].Cells["clmSosok"].Value.ToString().Trim();
            string clmTel2 = grdUpmuDamdang.Rows[1].Cells["clmTel"].Value.ToString().Trim();
            string clmDirectNumber2 = grdUpmuDamdang.Rows[1].Cells["clmDirectNumber"].Value.ToString().Trim();
            string clmEMail2 = grdUpmuDamdang.Rows[1].Cells["clmEMail"].Value.ToString().Trim();

            bool emt1 = false, emt2 = false;
            if (clmName1 == "" && clmSosok1 == "" && clmTel1 == "" && clmDirectNumber1 == "" && clmEMail1 == "")
                emt1 = true;

            if (clmName2 == "" && clmSosok2 == "" && clmTel2 == "" && clmDirectNumber2 == "" && clmEMail2 == "")
                emt2 = true;

            if ((emt1 == false && emt2 == false) || (emt1 == true && emt2 == true))
            {
                Biz.Show("보건/안전 담당자중 복사 대상 담당자의 내용이 전부 비어있어야 합니다.", "경고");
                return;
            }


            if (emt1 == false)
            {
                grdUpmuDamdang.Rows[1].Cells["clmName"].Value = clmName1;
                grdUpmuDamdang.Rows[1].Cells["clmSosok"].Value = clmSosok1;
                grdUpmuDamdang.Rows[1].Cells["clmTel"].Value = clmTel1;
                grdUpmuDamdang.Rows[1].Cells["clmDirectNumber"].Value = clmDirectNumber1;
                grdUpmuDamdang.Rows[1].Cells["clmEMail"].Value = clmEMail1;
            }
            else
            {
                grdUpmuDamdang.Rows[0].Cells["clmName"].Value = clmName2;
                grdUpmuDamdang.Rows[0].Cells["clmSosok"].Value = clmSosok2;
                grdUpmuDamdang.Rows[0].Cells["clmTel"].Value = clmTel2;
                grdUpmuDamdang.Rows[0].Cells["clmDirectNumber"].Value = clmDirectNumber2;
                grdUpmuDamdang.Rows[0].Cells["clmEMail"].Value = clmEMail2;
            }
        }

        async private void btnUpmuDamdangSave_Click(object sender, EventArgs e)
        {
            //사업실적 체크
            int r;
            string gubun = "";//code
            string name = "";
            string sosok = "";
            string tel = "";
            string directnumber = "";
            string email = "", isselect = "";

            _Yearmon = txtYearmon.Text;

            try
            {
                _Yearmon = Convert.ToDateTime(txtYearmon.Text + "-01").ToString("yyyy-MM");
            }
            catch
            {
                txtYearmon.Text = DateTime.Now.ToString("yyyy-MM");
                _Yearmon = txtYearmon.Text; 
            }

            RadWaitingBar waitingBar = Biz.Instance.SetWaitingBar();
            waitingBar.AssociatedControl = this;
            AbortableBackgroundWorker worker = new AbortableBackgroundWorker();

            worker.DoWork += delegate (object obj, DoWorkEventArgs args)
            {
                SqlConnection con = Biz.Instance.Connection;
                SqlTransaction tran = con.BeginTransaction();

                r = Biz.Instance.SaupjaUpmuDamdangDel(con, tran, _SaupID, _Yearmon);
                if (r < 0)
                {
                    tran.Rollback();
                    con.Close();
                    Biz.Show("업무 담당자 초기화에 실패했습니다.", "오류");
                    return;
                }

                for (int i = 0; i < grdUpmuDamdang.Rows.Count; i++)
                {
                    gubun = grdUpmuDamdang.Rows[i].Cells["clmCode"].Value?.ToString();
                    name = grdUpmuDamdang.Rows[i].Cells["clmName"].Value?.ToString() ?? "";
                    sosok = grdUpmuDamdang.Rows[i].Cells["clmSosok"].Value?.ToString() ?? "";
                    tel = grdUpmuDamdang.Rows[i].Cells["clmTel"].Value?.ToString() ?? "";
                    directnumber = grdUpmuDamdang.Rows[i].Cells["clmDirectNumber"].Value?.ToString() ?? "";
                    email = grdUpmuDamdang.Rows[i].Cells["clmEmail"].Value?.ToString() ?? "";
                    string check = grdUpmuDamdang.Rows[i].Cells["clmIsSelect"].Value?.ToString().ToLower();

                    if (check == "true" || check == "on")
                        isselect = "1";
                    else
                        isselect = "0";

                    r = Biz.Instance.SaupjaUpmuDamdangSave(con, tran, _SaupID, _Yearmon, gubun, name, sosok, tel, directnumber, email, isselect);

                    if (r < 0)
                    {
                        tran.Rollback();
                        con.Close();
                        //Biz.Show("업무 담당자 저장에 실패했습니다.", "오류");
                        return;
                    }
                }

                tran.Commit();
                con.Close();

                DataTable dt4 = Biz.Instance.RPT_SaupjaCardUpmuDamdangList(_SaupID, _Yearmon);
                args.Result = dt4;
            };
            worker.ProgressChanged += delegate (object obj, ProgressChangedEventArgs args)
            {
            };
            worker.RunWorkerCompleted += delegate (object obj, RunWorkerCompletedEventArgs args)
            {
                if (args.Result != null && args.Result is DataTable)
                {
                    grdUpmuDamdang.DataSource = (DataTable)args.Result;
                }
                else
                {
                    Biz.Show(this, "업무 담당자 저장에 실패했습니다.");
                }

                waitingBar.StopWaiting();
            };

            waitingBar.StartWaiting();
            await Task.Run(() => { System.Threading.Thread.Sleep(Biz._ThreadTime); });
            BeginInvoke(new MethodInvoker(worker.RunWorkerAsync));
        }

        private void grdUpmuDamdang_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewTableHeaderRowInfo)
                return;

            if (e.Column.Name == "clmDel")
            {
                int c = 0;
                string code = e.Row.Cells["clmCode"].Value.ToString();

                for (int i = 0; i < grdUpmuDamdang.Rows.Count; i++)
                {
                    if (grdUpmuDamdang.Rows[i].Cells["clmCode"].Value.ToString() == code)
                        c++;
                }

                if (c < 2)
                    return;

                grdUpmuDamdang.Rows.Remove(e.Row);
            }
            else if (e.Column.Name == "clmSelect")
            {
                if (Selected != null)
                    Selected(this, e.Row);
            }
        }

        private void grdUpmuDamdang_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row is GridViewTableHeaderRowInfo)
                return;

            if (Selected != null)
                Selected(this, e.Row);
        }
    }
}
