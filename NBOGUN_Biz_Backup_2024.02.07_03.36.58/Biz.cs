using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Telerik.WinControls;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.UI;
using Convert = System.Convert;
using Excel = Microsoft.Office.Interop.Excel;

namespace NBOGUN
{
    public delegate void DelegateChangeYearmon();

    //public delegate void DelegateChangeSaupjaID(string SaupjaNum, int SaupID, string SaupjaName);
    public partial class Biz
    {
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        public event EventHandler SaupIDChanged;
        public event EventHandler DHCenterChanged;
        protected void OnDHCenterChanged()
        { DHCenterChanged?.Invoke(this, EventArgs.Empty); }
        
        protected void OnSaupIDChanged()
        {
            EventArgs args = new EventArgs();
            
            SaupIDChanged?.Invoke(this, args);
        }

        private DataTable _CenterGumsaCode;
        private DataTable _YearmonStaff;//해당 작업연월의 센터직원 리스트
        private DataTable _YearmonSaupja;
        private DataTable _DTAllCenterList;//전센터 리스트

        private DataTable _TableEduType;//사업장안전보건교육종류
        private DataTable _TableEduBangbup;//사업장안전보건교육방법
        private DataTable _TableEduSize;//사업장안전보건교육구분
        private DataTable _TableEduItemGubun;//자료제공구분
        private DataTable _TableEduItemType;//보급형태
        private DataTable _TableSimpleInja;//간이측정인자
        private DataTable _TableChkjnDanwee;//측정단위
        int _CurrentSaupID;
        private object _lock;
        //private NotifyPropertyChanged(string propertyName)
        //{
        //    //Raise PropertyChanged event
        //}
        //public event PropertyChangedEventHandler PropertyChanged;
        //public int CurrentSaupID
        //{
        //    get
        //    {
        //        return _CurrentSaupID;
        //    }
        //    set
        //    {
        //        lock (_lock)
        //        {
        //            //The property changed event will get fired whenever
        //            //the value changes. The subscriber will do work if the value is
        //            //1. This way you can keep your business logic outside of the setter
        //            if (value != _CurrentSaupID)
        //            {
        //                _CurrentSaupID = value;
        //                NotifyPropertyChanged("MyProperty");
        //            }
        //        }
        //    }
        //}
        /// <summary>
        /// 사업장안전보건교육종류
        /// </summary>
        public DataTable CodeEduType
        {
            get
            {
                if (_TableEduType == null)
                    _TableEduType = Biz.Instance.GetDHCodeList(Biz.CodeGroupName.사업장안전보건교육종류);
                return _TableEduType.Copy();
            }
            set => _TableEduType = value;
        }
        /// <summary>
        /// 사업장안전보건교육방법
        /// </summary>
        public DataTable CodeEduBangbup
        {
            get
            {
                if (_TableEduBangbup == null)
                    _TableEduBangbup = Biz.Instance.GetDHCodeList(Biz.CodeGroupName.사업장안전보건교육방법);
                return _TableEduBangbup.Copy();
            }
            set => _TableEduBangbup = value;
        }
        /// <summary>
        /// 사업장안전보건교육구분
        /// </summary>
        public DataTable CodeEduSize
        {
            get
            {
                if (_TableEduSize == null)
                    _TableEduSize = Biz.Instance.GetDHCodeList(Biz.CodeGroupName.사업장안전보건교육구분);
                return _TableEduSize.Copy();
            }
            set => _TableEduSize = value;
        }

        /// <summary>
        /// 자료제공구분
        /// </summary>
        public DataTable CodeEduItemGubun
        {
            get
            {
                if (_TableEduItemGubun == null)
                    _TableEduItemGubun = Biz.Instance.GetDHCodeList(Biz.CodeGroupName.자료제공구분);
                return _TableEduItemGubun.Copy();
            }
            set => _TableEduItemGubun = value;
        }
        /// <summary>
        /// 보급형태
        /// </summary>
        public DataTable CodeEduItemType
        {
            get
            {
                if (_TableEduItemType == null)
                    _TableEduItemType = Biz.Instance.GetDHCodeList(Biz.CodeGroupName.보급형태);
                return _TableEduItemType.Copy();
            }
            set => _TableEduItemType = value;
        }

        DataTable _TableChkjnExclusion;
        public DataTable CodeChkjnExclusion
        {
            get
            {
                if (_TableChkjnExclusion == null)
                    _TableChkjnExclusion = Biz.Instance.GetDHCodeList(Biz.CodeGroupName.측정제외사유);
                return _TableChkjnExclusion.Copy();
            }
            set => _TableChkjnExclusion = value;
        }

        DataTable _TableChkjnWorkType;
        public DataTable CodeChkjnWorkType
        {
            get
            {
                if (_TableChkjnWorkType == null)
                    _TableChkjnWorkType = Biz.Instance.GetDHCodeList(Biz.CodeGroupName.근로형태조);
                return _TableChkjnWorkType.Copy();
            }
            set => _TableChkjnWorkType = value;
        }

        DataTable _TableChkjnWorkTypeChange;
        public DataTable CodeChkjnWorkTypeChange
        {
            get
            {
                if (_TableChkjnWorkTypeChange == null)
                    _TableChkjnWorkTypeChange = Biz.Instance.GetDHCodeList(Biz.CodeGroupName.근로형태조);
                return _TableChkjnWorkTypeChange.Copy();
            }
            set => _TableChkjnWorkTypeChange = value;
        }

        DataTable _TableChkjnSYGubun;
        public DataTable CodeChkjnSYGubun
        {
            get
            {
                if (_TableChkjnSYGubun == null)
                    _TableChkjnSYGubun = Biz.Instance.GetDHCodeList(Biz.CodeGroupName.사용구분);
                return _TableChkjnSYGubun.Copy();
            }
            set => _TableChkjnSYGubun = value;
        }

        DataTable _TableChkjnResultType;
        public DataTable CodeChkjnResultType
        {
            get
            {
                if (_TableChkjnResultType == null)
                    _TableChkjnResultType = Biz.Instance.GetDHCodeList(Biz.CodeGroupName.결과유형);
                return _TableChkjnResultType.Copy();
            }
            set => _TableChkjnResultType = value;
        }

        DataTable _TableChkjnResultOver;
        public DataTable CodeChkjnResultOver
        {
            get
            {
                if (_TableChkjnResultOver == null)
                    _TableChkjnResultOver = Biz.Instance.GetDHCodeList(Biz.CodeGroupName.초과율);
                return _TableChkjnResultOver.Copy();
            }
            set => _TableChkjnResultOver = value;
        }

        public DataTable CodeSimpleInja
        {
            get
            {
                if (_TableSimpleInja != null)
                    return _TableSimpleInja.Copy();
                else
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add("Name", typeof(string));
                    dataTable.Columns.Add("Code", typeof(string));
                    return dataTable;
                }
            }
            set => _TableSimpleInja = value;
        }
        public DataTable CodeChkjnDanwee { get => _TableChkjnDanwee.Copy(); set => _TableChkjnDanwee = value; }

        public DataTable YearmonStaff
        {
            set => _YearmonStaff = value;
        }

        public DataTable YearmonStaffDr
        {
            get
            {
                return _YearmonStaff.Select("DHJikjong = 'DH1601'")?.CopyToDataTable();
            }
        }

        public DataTable YearmonStaffNr
        {
            get
            {
                return _YearmonStaff.Select("DHJikjong = 'DH1602'")?.CopyToDataTable();
            }
        }

        public DataTable YearmonStaffHr
        {
            get
            {
                return _YearmonStaff.Select("DHJikjong = 'DH1603'")?.CopyToDataTable();
            }
        }

        public DataTable YearmonSaupja
        {
            set => _YearmonSaupja = value;
            get
            {
                return _YearmonSaupja;
            }
        }

        public DataTable CenterGumsaCode
        {
            get => _CenterGumsaCode.Copy();
            set => _CenterGumsaCode = value;
        }

        private string _TitleContext = "";
        public string TitleContext
        {
            get => _TitleContext;
            set
            {
                if (_TitleContext != value)
                    SetProperty(ref _TitleContext, value);

                _TitleContext = value;
            }
        }

        BackgroundWorker _workerBackground;

        public void RunBackWork()
        {
            if (_workerBackground == null)
            {
                _workerBackground = new BackgroundWorker();
                _workerBackground.WorkerSupportsCancellation = true;
                _workerBackground.WorkerReportsProgress = true;
                _workerBackground.DoWork += _workerBackground_DoWork;
                _workerBackground.ProgressChanged += _workerBackground_ProgressChanged;
                _workerBackground.RunWorkerCompleted += _workerBackground_RunWorkerCompleted;
                _workerBackground.RunWorkerAsync();
            }

            if (_workerBackground.IsBusy)
                return;

            _workerBackground.RunWorkerAsync();
        }

        private void _workerBackground_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void _workerBackground_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void _workerBackground_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {

            }
        }

        #region DHCenter Changed
        public event PropertyChangedEventHandler PropertyChanged;

        private void SetProperty<T>(ref T field, T value, [CallerMemberName] string name = "")
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                var handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(name));
                }
            }

            field = value;
            var handler1 = PropertyChanged;
            if (handler1 != null)
            {
                handler1(this, new PropertyChangedEventArgs(name));
            }
        }
        public string DHCenter
        {
            get { return _DHCenter; }
            set
            {
                if (_DHCenter != value)
                    SetProperty(ref _DHCenter, value);
                OnDHCenterChanged();
                _DHCenter = value;
                if (_changeYearmon != null)
                    _changeYearmon();
            }
        }
        //int _CurrentSaupID;
        public int  CurrentSaupID
        {
            get { return _CurrentSaupID; }
            set
            {
                _CurrentSaupID = value;

                OnSaupIDChanged();
            }
        }

        protected virtual void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        #endregion


        static public DelegateChangeYearmon _changeYearmon;

        public static DelegateChangeYearmon ChangeYearmon
        {
            get
            {
                return _changeYearmon;
            }
            set
            {
                _changeYearmon = value;
            }
        }

        string _UserID = "2009112";
        string _UserName = "";
        static string _Center = "00";
        static string _DHCenter = "00";
        static string _DHCenterName = "본부";
        static string _Yearmon = "";
        static string _DHJikjong = "DH1601";
        static string _DHJikjongName = "의사";
        static string _BuseoSection = "L100";//L100, S400
        static string _UserPhone = "";//로그인한 유저 연락처
        static string _UserEmailID = "";//

        string _ProcedureString;//Log_Function에 저장용
        string _ProcedureStringForServer;//Log_Function에 저장용

        //string _ThemeName = "Office2019Light";
        // lock문에 사용될 객체
        private static object lockObject = new object();

        public string UserEmailID
        {
            get { return _UserEmailID; }
            set { _UserEmailID = value; }
        }

        public string UserPhone
        {
            get { return _UserPhone; }
            set { _UserPhone = value; }
        }

        public string CenterPhone
        {
            get
            {
                DataTable dt = GetCenterList();

                var row = dt.Select("Center = '" + Biz.Instance.DHCenter + "'");

                if (row != null && row.Length > 0)
                {
                    return dt.Rows[0]["Phone1"].ToString() + "-" + dt.Rows[0]["Phone2"].ToString() + "-" + dt.Rows[0]["Phone3"].ToString();
                }
                else
                    return "";
            }
        }
        /// <summary>
        /// 보건관리 프로그램 권한 여부
        /// </summary>
        public bool AuthBogun
        {
            get
            {
                if (_BuseoSection == "L100" || _BuseoSection == "S400" || _BuseoSection == "C000")
                    return true;
                else
                    return false;
            }
        }
        public string BuseoSection
        {
            get { return _BuseoSection; }
            set { _BuseoSection = value; }
        }

        public string Center
        {
            get { return _Center; }
            set { _Center = value; }
        }

        public string DHJikjong
        {
            get { return _DHJikjong; }
            set { _DHJikjong = value; }
        }

        public string DHJikjongName
        {
            set
            {
                _DHJikjongName = value;
            }
            get { return _DHJikjongName; }
        }



        public string DHCenterName
        {
            get { return _DHCenterName; }
            set { _DHCenterName = value; }
        }

        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public string Yearmon
        {
            get
            {
                if (_Yearmon == "")
                    _Yearmon = DateTime.Now.ToString("yyyy-MM");

                return _Yearmon;
            }
            set
            {
                _Yearmon = value;

                if(_changeYearmon != null)
                    _changeYearmon();
            }
        }

        public enum CodeGroupName
        {
            자료제공구분,
            /// <summary>
            /// 건강증진계약구분
            /// </summary>
            건강증진계약구분,
            /// <summary>
            /// 건물소유여부
            /// </summary>
            건물소유여부,
            /// <summary>
            /// 교대제
            /// </summary>
            교대제,
            /// <summary>
            /// 교육방법
            /// </summary>
            교육방법,
            /// <summary>
            /// 국고계약종류
            /// </summary>
            국고계약종류,
            /// <summary>
            /// 
            /// </summary>
            국고구분,
            /// <summary>
            /// 
            /// </summary>
            근골유해요인조사구분,
            /// <summary>
            /// 
            /// </summary>
            노동조합,
            /// <summary>
            /// 
            /// </summary>
            뇌심입력구분,
            /// <summary>
            /// 
            /// </summary>
            담당구분,
            /// <summary>
            /// 
            /// </summary>
            물질단위,
            /// <summary>
            /// 
            /// </summary>
            보건관리계약구분,
            /// <summary>
            /// 
            /// </summary>
            보건관리기록구분,
            /// <summary>
            /// 
            /// </summary>
            보건관리자구분,
            /// <summary>
            /// 
            /// </summary>
            보건관리직종,
            /// <summary>
            /// 
            /// </summary>
            보건담당자월상태,
            /// <summary>
            /// 
            /// </summary>
            보고서,
            /// <summary>
            /// 
            /// </summary>
            보급형태,
            측정단위,
            측정제외사유,
            근로형태조, 사용구분, 결과유형, 초과율,
            근로형태교대,
            보건간이측정인자,
            /// <summary>
            /// 
            /// </summary>
            사업장규모,
            /// <summary>
            /// 
            /// </summary>
            사업장안전보건관리규정,
            /// <summary>
            /// 
            /// </summary>
            사업장안전보건교육구분,
            /// <summary>
            /// 
            /// </summary>
            사업장안전보건교육방법,
            /// <summary>
            /// 
            /// </summary>
            사업장안전보건교육종류,
            /// <summary>
            /// 
            /// </summary>
            사업장위치,
            /// <summary>
            /// 
            /// </summary>
            생산방식,
            /// <summary>
            /// 
            /// </summary>
            성별,
            /// <summary>
            /// 
            /// </summary>
            실시구분,
            /// <summary>
            /// 
            /// </summary>
            위원회구분,
            /// <summary>
            /// 
            /// </summary>
            위원회종류,
            /// <summary>
            /// 
            /// </summary>
            위원회참석구분,
            /// <summary>
            /// 
            /// </summary>
            위험물질구분,
            /// <summary>
            /// 
            /// </summary>
            유해물질구분,
            /// <summary>
            /// 
            /// </summary>
            자체점검결과,
            /// <summary>
            /// 
            /// </summary>
            자체코드,
            /// <summary>
            /// 
            /// </summary>
            재해구분,
            /// <summary>
            /// 
            /// </summary>
            평가구분,
            /// <summary>
            /// 
            /// </summary>
            해약사유,
            /// <summary>
            /// 
            /// </summary>
            혈당구분,
            /// <summary>
            /// 
            /// </summary>
            인력부서,
            /// <summary>
            /// 
            /// </summary>
            신규계약사유,
            보건관리자격면허,
            발행구분
        }

        RadGridView _Grd;

        private string _Error = "";

        public string Error
        { get { return _Error; } set { _Error = value; } }

        private const string _Connection = "Data Source=121.65.230.197,1147\\Saup;Initial Catalog=OHIS2015;User ID=kiha;Password=health;Timeout=360";

        public string ConnectionString
        { get { return _Connection; } }

        public string Query
        {
            get => _ProcedureString;
        }

        public SqlConnection Connection
        {
            get
            {
                SqlConnection con = new SqlConnection(_Connection);
                con.Open();
                return con;
            }
        }

        private static readonly Biz _Biz = new Biz();
        public static Biz Instance
        { get { return _Biz; } }

        private Biz()
        {
            _DTControlLevel1.Columns.Add("Name", typeof(string));
            _DTControlLevel1.Columns.Add("Level", typeof(int));
            _DTControlLevel1.Columns.Add("Text", typeof(string));

            _DTControlLevel2.Columns.Add("Name", typeof(string));
            _DTControlLevel2.Columns.Add("Level", typeof(int));
            _DTControlLevel2.Columns.Add("Text", typeof(string));
            _DTControlLevel2.Columns.Add("PName", typeof(string));
        }
        public enum Browser { IE, Chrome, Edge };
        public static void UrlStart(string Url, Browser Browser = Browser.Edge)
        {
            Process pro;

            switch (Browser)
            {
                case Browser.Edge:
                    pro = Process.Start("microsoft-edge:" + Url);
                    break;
                case Browser.Chrome:
                    pro = Process.Start("chrome.exe", Url);
                    break;
                case Browser.IE:
                default:
                    pro = Process.Start("IExplore.exe", Url);
                    break;
            }

            // How to bring it to front.
            try
            {
                IntPtr WinPtr = pro.MainWindowHandle;

                // Verify there is a mainWindow
                if (WinPtr == IntPtr.Zero)
                {
                    //System.Windows.MessageBox.Show("No Windows");
                    return;
                }
                else
                {
                    //bring it to front.
                    SetForegroundWindow(WinPtr);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("UrlStart : " + Url + Environment.NewLine + ex.Message);
            }

        }

        public bool SaveExcel(System.Data.DataTable orgTable)
        {
            if (orgTable == null)
            {
                return false;
            }

            //Form Progress = new Kiha21.BogunApp.Progress();
            //Progress.Show();

            Microsoft.Office.Interop.Excel.Application objExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook objWorkBook;
            Excel.Worksheet objWorkSheet;

            objWorkBook = objExcel.Workbooks.Add(Type.Missing);
            objWorkSheet = (Excel.Worksheet)objWorkBook.Worksheets[1];
            //objWorkSheet.Name = "시험중";
            //objWorkSheet = (Excel.Worksheet)objWorkBook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing); // Sheet추가
            for (int i = 0; i < orgTable.Columns.Count; i++)
            {
                if (orgTable.Columns[i].DataType.Name == "String")
                {
                    //Microsoft.Office.Interop.Excel.Range objRangeH = (Microsoft.Office.Interop.Excel.Range)objWorkSheet.Cells[1, i + 1];
                    Microsoft.Office.Interop.Excel.Range objRangeH = (Microsoft.Office.Interop.Excel.Range)objWorkSheet.Cells[1, i + 1];
                    objRangeH.EntireColumn.NumberFormatLocal = "@";
                }

                objWorkSheet.Cells[1, i + 1] = orgTable.Columns[i].ColumnName.ToString();
            }

            Object[,] dataRow = new Object[orgTable.Rows.Count, orgTable.Columns.Count];
            for (int i = 0; i < orgTable.Rows.Count; i++)
            {
                for (int j = 0; j < orgTable.Columns.Count; j++)
                {
                    dataRow[i, j] = orgTable.Rows[i][j];
                }
            }

            Microsoft.Office.Interop.Excel.Range r1 = objWorkSheet.Cells[2, 1];
            Microsoft.Office.Interop.Excel.Range r2 = objWorkSheet.Cells[orgTable.Rows.Count + 1, orgTable.Columns.Count];
            Microsoft.Office.Interop.Excel.Range objRange =
                objWorkSheet.get_Range(r1, r2);
            //(Microsoft.Office.Interop.Excel.Range)objWorkSheet.get_Range(objWorkSheet.Cells[2, 1], objWorkSheet.Cells[orgTable.Rows.Count + 1, orgTable.Columns.Count]);
            objRange.Value2 = dataRow;
            objWorkSheet.Cells.Columns.AutoFit();
            objWorkSheet.Cells.Rows.AutoFit();
            objExcel.Visible = true;
            //Progress.Close();
            return true;
        }

        public bool SaveExcel(System.Data.DataSet orgTable)
        {
            if (orgTable == null)
            {
                return false;
            }

            Microsoft.Office.Interop.Excel.Application objExcel = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Workbook objWorkBook;

            objWorkBook = objExcel.Workbooks.Add(Type.Missing);

            for (int i = 0; i < orgTable.Tables.Count; i++)
            {
                objWorkBook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }

            int tableIndex = 1;

            foreach (System.Data.DataTable tmpTable in orgTable.Tables)
            {
                Excel.Worksheet objWorkSheet;

                objWorkSheet = (Excel.Worksheet)objWorkBook.Worksheets[tableIndex];
                objWorkSheet.Name = tmpTable.TableName;
                //objWorkSheet = (Excel.Worksheet)objWorkBook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing); // Sheet추가
                for (int i = 0; i < tmpTable.Columns.Count; i++)
                {
                    //if (tmpTable.Columns[i].DataType.Name == "String")
                    //{
                    Microsoft.Office.Interop.Excel.Range objRangeH = (Microsoft.Office.Interop.Excel.Range)objWorkSheet.Cells[1, i + 1];
                    objRangeH.EntireColumn.NumberFormatLocal = "@";
                    //}

                    objWorkSheet.Cells[1, i + 1] = tmpTable.Columns[i].ColumnName.ToString();
                }

                Object[,] dataRow = new Object[tmpTable.Rows.Count, tmpTable.Columns.Count];
                for (int i = 0; i < tmpTable.Rows.Count; i++)
                {
                    for (int j = 0; j < tmpTable.Columns.Count; j++)
                    {
                        dataRow[i, j] = tmpTable.Rows[i][j];
                    }
                }

                Microsoft.Office.Interop.Excel.Range objRange;
                Microsoft.Office.Interop.Excel.Range r1;
                Microsoft.Office.Interop.Excel.Range r2;
                try
                {
                    r1 = objWorkSheet.Cells[2, 1];
                    r2 = objWorkSheet.Cells[tmpTable.Rows.Count + 1, tmpTable.Columns.Count];
                    objRange = objWorkSheet.get_Range(r1, r2);
                    //objRange = (Microsoft.Office.Interop.Excel.Range)objWorkSheet.get_Range(objWorkSheet.Cells[2, 1], objWorkSheet.Cells[tmpTable.Rows.Count + 1, tmpTable.Columns.Count]);
                }
                catch
                {
                    return false;
                }

                objRange.Value2 = dataRow;
                objWorkSheet.Cells.Columns.AutoFit();
                tableIndex += 1;
            }

            objExcel.Visible = true;

            return true;
        }

        private void SetProcedureString(string ProcedureName, SqlParameter[] SqlParameters)
        {
            //디버깅을 위해 코드 추가
            _ProcedureString = ProcedureName;
            _ProcedureStringForServer = ProcedureName;

            string pString = "";
            string pStringServer = "";

            for (int i = 0; i < SqlParameters.Length; i++)
            {
                if (pString != "")
                {
                    pString += ",";
                    pStringServer += ",";
                }

                if (SqlParameters[i] == null)
                    continue;

                if (SqlParameters[i].SqlDbType == SqlDbType.Char || SqlParameters[i].SqlDbType == SqlDbType.VarChar || SqlParameters[i].SqlDbType == SqlDbType.Date
                    || SqlParameters[i].SqlDbType == SqlDbType.DateTime || SqlParameters[i].SqlDbType == SqlDbType.Text || SqlParameters[i].SqlDbType == SqlDbType.NChar
                    || SqlParameters[i].SqlDbType == SqlDbType.NText || SqlParameters[i].SqlDbType == SqlDbType.NVarChar)
                {
                    pString += " '" + (SqlParameters[i].Value == null ? "" : SqlParameters[i].Value.ToString()) + "'";
                    pStringServer += " ''" + (SqlParameters[i].Value == null ? "" : SqlParameters[i].Value.ToString()) + "''";
                }
                else
                {
                    pString += " " + (SqlParameters[i].Value == null ? "" : SqlParameters[i].Value.ToString());
                    pStringServer += " " + (SqlParameters[i].Value == null ? "" : SqlParameters[i].Value.ToString());
                }
            }

            _ProcedureString += pString;
            _ProcedureStringForServer += pStringServer;

            LogManager.WriteLog(_ProcedureString);
            //ProcedureAdd(this.mProcedureString);
        }

        //외부 아이피
        string GetExternalIpReg()
        {
            try
            {
                string whatIsMyIp = "http://www.whatismyip.com/automation/n09230945.asp";
                WebClient wc = new WebClient();
                UTF8Encoding utf8 = new UTF8Encoding();
                string requestHtml = "";

                requestHtml = utf8.GetString(wc.DownloadData(whatIsMyIp));

                IPAddress externalIp = null;

                externalIp = IPAddress.Parse(requestHtml);

                return externalIp.ToString();
            }
            catch
            {
                return "";
            }
        }

        int WriteServerLog(String ProcName, SqlParameter[] param)
        {
            using (SqlConnection cn = new SqlConnection(_Connection))
            {
                if (cn.State == ConnectionState.Closed)
                    cn.Open();

                SqlCommand cmd = cn.CreateCommand();
                String ProcCommand = "";

                if (param != null)
                {
                    foreach (SqlParameter p in param)
                    {
                        ProcCommand += "[" + p.ParameterName + "=" + p.Value + "]";
                    }
                }

                SetProcedureString(ProcName, param);

                cmd.CommandType = CommandType.Text;
#pragma warning disable CS0618 // 형식 또는 멤버는 사용되지 않습니다.
                cmd.CommandText = "SET ARITHABORT ON insert System_ServerLog (Center, Sabun, ProcName, ProcParameter, HostName, InternalIP, ExternalIP) values ('" +
                                                Center + "', '" +
                                                UserID + "', '" +
                                                ProcName + "','" +
                                                //ProcCommand + "', '" +
                                                _ProcedureStringForServer + "', '" +
                                                Dns.GetHostName().ToString() + "', '" +
                                                Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString() + "','" +
                                                GetExternalIpReg() + "')";
#pragma warning restore CS0618 // 형식 또는 멤버는 사용되지 않습니다.

                int rValue = 0;

                try
                {
                    rValue = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    _Error = ex.Message;

                    cmd.Parameters.Clear();
                    cmd = null;
                    //RadMessageBox.Show(e.Message + cmd.CommandText, "서버로그 오류", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                    {
                        cn.Close();
                    }
                }
                //cmd.Parameters.Clear();
                return rValue;
            }

            //Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();

        }

        public static DialogResult Show(string Content, string Caption = "알림", MessageBoxButtons Buttons = MessageBoxButtons.OK, string Theme = "VisualStudio2012Light", int FontSize = 13)
        {
            //Noto Serif KR
            //RadMessageBox.Instance.FormElement.TitleBar.Font = new System.Drawing.Font("맑은 고딕", FontSize);
            RadMessageBox.Instance.FormElement.TitleBar.Font = new System.Drawing.Font("맑은 고딕", FontSize);

            RadMessageLocalizationProvider.CurrentProvider = new RadMessageBoxLocalization();

            // I added this additional check for safety, if Telerik modifies the name of the control.
            if (RadMessageBox.Instance.Controls.ContainsKey("radLabel1"))
            {
                RadMessageBox.Instance.Controls["radLabel1"].Font = new System.Drawing.Font("맑은 고딕", FontSize, FontStyle.Regular);
                //RadMessageBox.Instance.Controls["radLabel1"].Font = new System.Drawing.Font("맑은 고딕", FontSize, FontStyle.Regular);

                foreach (Control obj in RadMessageBox.Instance.Controls)
                {
                    obj.Font = new System.Drawing.Font("맑은 고딕", FontSize, FontStyle.Regular);

                    obj.Margin = new System.Windows.Forms.Padding(4);
                    obj.Padding = new System.Windows.Forms.Padding(4);
                }
                RadMessageBox.Instance.ButtonSize = new Size(70, 32);
                RadMessageBox.Instance.Controls["radButton1"].Text = "확인11";
                //RadMessageBox.Instance.Controls["radButton1"].Padding = new System.Windows.Forms.Padding(5);
            }

            RadMessageBox.ThemeName = Theme;
            //MessageBox.Show("Message Text", "Header", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
            //RadMessageLocalizationProvider.CurrentProvider = new RadMessageBoxLocalization();
            //text = "<html><span style=\"font-size:10pt;font-family: 맑은 고딕\">" + text + "</span>";
            //caption = "<html><span style=\"font-size:10pt;font-family: 맑은 고딕\">" + caption + "</span>";
            return RadMessageBox.Show(Content, Caption, Buttons);
        }

        public static DialogResult Show(IWin32Window win, string Content, string Caption = "알림", MessageBoxButtons Buttons = MessageBoxButtons.OK, string Theme = "VisualStudio2012Light", int FontSize = 13)
        {
            //Noto Serif KR
            //RadMessageBox.Instance.FormElement.TitleBar.Font = new System.Drawing.Font("맑은 고딕", FontSize);
            RadMessageBox.Instance.FormElement.TitleBar.Font = new System.Drawing.Font("맑은 고딕", FontSize);

            RadMessageLocalizationProvider.CurrentProvider = new RadMessageBoxLocalization();

            // I added this additional check for safety, if Telerik modifies the name of the control.
            if (RadMessageBox.Instance.Controls.ContainsKey("radLabel1"))
            {
                RadMessageBox.Instance.Controls["radLabel1"].Font = new System.Drawing.Font("맑은 고딕", FontSize, FontStyle.Regular);
                //RadMessageBox.Instance.Controls["radLabel1"].Font = new System.Drawing.Font("맑은 고딕", FontSize, FontStyle.Regular);

                foreach (Control obj in RadMessageBox.Instance.Controls)
                {
                    obj.Font = new System.Drawing.Font("맑은 고딕", FontSize, FontStyle.Regular);

                    obj.Margin = new System.Windows.Forms.Padding(4);
                    obj.Padding = new System.Windows.Forms.Padding(4);
                }
                RadMessageBox.Instance.ButtonSize = new Size(70, 32);
                RadMessageBox.Instance.Controls["radButton1"].Text = "확인11";
                //RadMessageBox.Instance.Controls["radButton1"].Padding = new System.Windows.Forms.Padding(5);
            }

            RadMessageBox.ThemeName = Theme;
            //MessageBox.Show("Message Text", "Header", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
            //RadMessageLocalizationProvider.CurrentProvider = new RadMessageBoxLocalization();
            //text = "<html><span style=\"font-size:10pt;font-family: 맑은 고딕\">" + text + "</span>";
            //caption = "<html><span style=\"font-size:10pt;font-family: 맑은 고딕\">" + caption + "</span>";
            return RadMessageBox.Show(win, Content, Caption, Buttons);
        }

        #region 컨트롤 옵션
        public static int _RowHeight = 30;
        public static int _ThreadTime = 30;

        //public event PropertyChangedEventHandler PropertyChanged;

        public void SetDropDownList(RadDropDownList pCbo)
        {
            pCbo.DropDownListElement.ListElement.Font = pCbo.Font;
            //pCbo.DropDownListElement.ListElement.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            if (pCbo.Font.Size == 14)
                pCbo.ListElement.ItemHeight = 30; //2020-09-09    바꿈
            else
                pCbo.ListElement.ItemHeight = 28; //2020-09-09    바꿈
            //pCbo.ListElement.ItemHeight = Biz._RowHeight;
        }

        public void SetDateTimePicker(RadDateTimePicker pCbo)
        {
            pCbo.DateTimePickerElement.Font = pCbo.Font;

            pCbo.DateTimePickerElement.TextBoxElement.Font = pCbo.Font;

            foreach (var el in pCbo.DateTimePickerElement.Children)
            {
                if (el is Telerik.WinControls.UI.StackLayoutElement)
                {
                    Telerik.WinControls.UI.StackLayoutElement a = el as Telerik.WinControls.UI.StackLayoutElement;

                    if (a != null)
                    {
                        ((RadCheckBoxElement)a.Children[0]).Font = pCbo.Font;
                        ((RadMaskedEditBoxElement)a.Children[1]).Font = pCbo.Font;
                        ((RadDateTimePickerArrowButtonElement)a.Children[2]).Font = pCbo.Font;
                    }
                }
            }
        }

        public void SetGridViewOption(RadGridView pGrd)
        {
            _Grd = pGrd;

            pGrd.AllowAddNewRow = false;
            pGrd.AllowDeleteRow = false;
            pGrd.EnableGrouping = false;
            pGrd.ShowRowHeaderColumn = false;
            pGrd.ImeMode = ImeMode.Hangul;
            pGrd.ThemeName = "VisualStudio2012Light";
            pGrd.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            pGrd.DataBindingComplete += PGrd_DataBindingComplete;
            //pGrd.CreateRowInfo += PGrd_CreateRowInfo;
            pGrd.CreateRow += PGrd_CreateRow;
            pGrd.CreateRowInfo += PGrd_CreateRowInfo;
            pGrd.CellEditorInitialized += PGrd_CellEditorInitialized;
            pGrd.CellFormatting += PGrd_CellFormatting;
            pGrd.ContextMenuOpening += PGrd_ContextMenuOpening;
            pGrd.ViewCellFormatting += PGrd_ViewCellFormatting;
        }

        private void PGrd_CreateRowInfo(object sender, GridViewCreateRowInfoEventArgs e)
        {
            e.RowInfo.MinHeight = Biz._RowHeight;
        }

        private void PGrd_CreateRow(object sender, GridViewCreateRowEventArgs e)
        {
            e.RowInfo.MinHeight = Biz._RowHeight;
        }

        private void PGrd_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is GridHeaderCellElement)
            {
                //e.CellElement.TextOrientation = Orientation.Vertical;
                ((GridViewTableHeaderRowInfo)e.CellElement.RowInfo).MinHeight = Biz._RowHeight;
            }
        }

        private void PGrd_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {

        }

        //private void PGrd_CreateRowInfo(object sender, GridViewCreateRowInfoEventArgs e)
        //{
        //    e.RowInfo.MinHeight = Biz._RowHeight;
        //}


        private void SetGridViewColumn(object pObj)
        {
            RadGridView grd = pObj as RadGridView;

            if (grd == null)
                return;


            for (int i = 0; i < grd.Columns.Count; i++)
            {
                if (grd.Columns[i].HeaderText.Contains("clm") == false)
                    continue;

                if (Biz.Instance.SettingVisibleclm == "Y")
                {
                    grd.Columns[i].IsVisible = true;
                }
                else
                    grd.Columns[i].IsVisible = false;
            }

            grd.CurrentRow = null;

        }

        private void PGrd_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            this.SetGridViewColumn(sender);
            ((RadGridView)sender).CurrentRow = null;
        }

        private void PGrd_CellEditorInitialized(object sender, GridViewCellEventArgs e)
        {
            var editor = e.ActiveEditor;
            if (editor != null && editor is RadDropDownListEditor)
            {
                RadDropDownListEditor dropDown = (RadDropDownListEditor)editor;

                RadDropDownListEditorElement element = (RadDropDownListEditorElement)dropDown.EditorElement;
                element.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
                element.ListElement.Font = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
                element.ItemHeight = 36;

            }
            else if (editor != null && editor is RadTextBoxEditor)
            {
                RadTextBoxEditor el = editor as RadTextBoxEditor;
                if (el != null)
                {

                    el.EditorElement.MinSize = new Size(0, Biz._RowHeight - 2);
                    RadTextBoxEditorElement ele = el.EditorElement as RadTextBoxEditorElement;
                    ele.Font = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
                }
            }
        }

        private void PGrd_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.Row.IsCurrent == true)
            {
                if (!(e.Column is GridViewCommandColumn))
                    e.CellElement.Font = new Font(((Telerik.WinControls.UI.GridDataCellElement)sender).Font.FontFamily.Name, ((Telerik.WinControls.UI.GridDataCellElement)sender).Font.Size, FontStyle.Bold, GraphicsUnit.Point, ((byte)(129)));
                //((Telerik.WinControls.UI.MasterGridViewTemplate)((Telerik.WinControls.UI.GridDataCellElement)sender).DataColumnInfo.OwnerTemplate).Owner
                //((Telerik.WinControls.UI.MasterGridViewTemplate)((Telerik.WinControls.UI.GridDataCellElement)sender).DataColumnInfo.OwnerTemplate).Owner

            }
            else
                e.CellElement.Font = new Font(((Telerik.WinControls.UI.GridDataCellElement)sender).Font.FontFamily.Name, ((Telerik.WinControls.UI.GridDataCellElement)sender).Font.Size, FontStyle.Regular, GraphicsUnit.Point, ((byte)(129)));
        }
        #endregion

        #region 로컬세팅
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        XmlReaderSettings SettingForSetting()
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.CheckCharacters = false;
            settings.IgnoreProcessingInstructions = false;
            settings.Async = true;
            return settings;
        }

        string SettingLocation = @"C:\Kiha\Setting.xml";

        public string SettingJikmuStressOrder
        {
            get
            {
                StringBuilder value = new StringBuilder();

                GetPrivateProfileString("BOGUN", "JikmuStressOrder", "", value, 32, SettingLocation);

                return value?.ToString() ?? "New";
            }

            set
            {
                WritePrivateProfileString("BOGUN", "JikmuStressOrder", value.ToString(), SettingLocation);
            }
        }

        public int SettingSelectedLawPage
        {
            get
            {
                StringBuilder value = new StringBuilder();

                GetPrivateProfileString("BOGUN", "SelectedLawPage", "", value, 32, SettingLocation);

                if (value.ToString() == "")
                    return 0;
                else
                    return Convert.ToInt32(value.ToString());
            }

            set
            {
                WritePrivateProfileString("BOGUN", "SelectedLawPage", value.ToString(), SettingLocation);
            }
        }

        public int SettingUploadFileSize
        {
            get
            {
                StringBuilder value = new StringBuilder();

                GetPrivateProfileString("BOGUN", "UploadFileSize", "", value, 32, SettingLocation);

                if ((value?.ToString() ?? "") == "")
                    return 3;
                else
                {
                    int v;

                    try
                    {
                        v = Convert.ToInt32(value.ToString());
                    }
                    catch
                    {
                        v = 3;
                    }

                    if (v > 3)
                        v = 3;
                }
                return Convert.ToInt32(value.ToString());
            }

            set
            {
                WritePrivateProfileString("BOGUN", "UploadFileSize", value.ToString(), SettingLocation);
            }
        }

        public string SettingUserID
        {
            get
            {
                StringBuilder value = new StringBuilder();

                GetPrivateProfileString("BOGUN", "UserID", "", value, 32, SettingLocation);

                return value?.ToString();
            }

            set
            {
                WritePrivateProfileString("BOGUN", "UserID", value.ToString(), SettingLocation);
            }
        }

        /// <summary>
        /// 그리드뷰의 clm필드를 볼지 여부
        /// </summary>
        public string SettingVisibleclm
        {
            get
            {
                StringBuilder value = new StringBuilder();

                GetPrivateProfileString("BOGUN", "Visibleclm", "", value, 32, SettingLocation);

                return ((value?.ToString() ?? "Y") == "" ? "Y" : (value?.ToString() ?? "Y"));
            }

            set
            {
                WritePrivateProfileString("BOGUN", "Visibleclm", value.ToString(), SettingLocation);
            }
        }
        /// <summary>
        /// 상태보고서 연간 출력시 의사 체크 여부
        /// </summary>
        public bool SettingReportSangtaeDH1601IsChecked
        {
            get
            {
                StringBuilder value = new StringBuilder();

                GetPrivateProfileString("BOGUN", "ReportSangtaeDH1601IsChecked", "", value, 32, SettingLocation);

                return ((value?.ToString() ?? "false").ToLower() == "true" ? true : false);
            }

            set
            {
                WritePrivateProfileString("BOGUN", "ReportSangtaeDH1601IsChecked", value.ToString(), SettingLocation);
            }
        }

        /// <summary>
        /// 상태보고서 연간 출력시 간호사 체크 여부
        /// </summary>
        public bool SettingReportSangtaeDH1602IsChecked
        {
            get
            {
                StringBuilder value = new StringBuilder();

                GetPrivateProfileString("BOGUN", "ReportSangtaeDH1602IsChecked", "", value, 32, SettingLocation);

                return ((value?.ToString() ?? "false").ToLower() == "true" ? true : false);
            }

            set
            {
                WritePrivateProfileString("BOGUN", "ReportSangtaeDH1602IsChecked", value.ToString(), SettingLocation);
            }
        }

        /// <summary>
        /// 상태보고서 연간 출력시 위생사 체크 여부
        /// </summary>
        public bool SettingReportSangtaeDH1603IsChecked
        {
            get
            {
                StringBuilder value = new StringBuilder();

                GetPrivateProfileString("BOGUN", "ReportSangtaeDH1603IsChecked", "", value, 32, SettingLocation);

                return ((value?.ToString() ?? "false").ToLower() == "true" ? true : false);
            }

            set
            {
                WritePrivateProfileString("BOGUN", "ReportSangtaeDH1603IsChecked", value.ToString(), SettingLocation);
            }
        }

        /// <summary>
        /// 상태보고서 연간 출력시 상태보고서 체크 여부
        /// </summary>
        public bool SettingReportSangtae1IsChecked
        {
            get
            {
                StringBuilder value = new StringBuilder();

                GetPrivateProfileString("BOGUN", "ReportSangtae1IsChecked", "", value, 32, SettingLocation);

                return ((value?.ToString() ?? "false").ToLower() == "true" ? true : false);
            }

            set
            {
                WritePrivateProfileString("BOGUN", "ReportSangtae1IsChecked", value.ToString(), SettingLocation);
            }
        }

        public bool SettingReportWeewonhoi1IsChecked
        {
            get
            {
                StringBuilder value = new StringBuilder();

                GetPrivateProfileString("BOGUN", "ReportWeewonhoi1IsChecked", "", value, 32, SettingLocation);

                return ((value?.ToString() ?? "false").ToLower() == "true" ? true : false);
            }

            set
            {
                WritePrivateProfileString("BOGUN", "ReportWeewonhoi1IsChecked", value.ToString(), SettingLocation);
            }
        }

        public bool SettingReportWeewonhoi2IsChecked
        {
            get
            {
                StringBuilder value = new StringBuilder();

                GetPrivateProfileString("BOGUN", "ReportWeewonhoi2IsChecked", "", value, 32, SettingLocation);

                return ((value?.ToString() ?? "false").ToLower() == "true" ? true : false);
            }

            set
            {
                WritePrivateProfileString("BOGUN", "ReportWeewonhoi2IsChecked", value.ToString(), SettingLocation);
            }
        }

        /// <summary>
        /// 상태보고서 연간 출력시 상태보고서 체크 여부
        /// </summary>
        public bool SettingReportSangtae2IsChecked
        {
            get
            {
                StringBuilder value = new StringBuilder();

                GetPrivateProfileString("BOGUN", "ReportSangtae2IsChecked", "", value, 32, SettingLocation);

                return ((value?.ToString() ?? "false").ToLower() == "true" ? true : false);
            }

            set
            {
                WritePrivateProfileString("BOGUN", "ReportSangtae2IsChecked", value.ToString(), SettingLocation);
            }
        }

        /// <summary>
        /// 상태보고서 연간 출력시 상태보고서 체크 여부
        /// </summary>
        public bool SettingReportSangtae3IsChecked
        {
            get
            {
                StringBuilder value = new StringBuilder();

                GetPrivateProfileString("BOGUN", "ReportSangtae3IsChecked", "", value, 32, SettingLocation);

                return ((value?.ToString() ?? "false").ToLower() == "true" ? true : false);
            }

            set
            {
                WritePrivateProfileString("BOGUN", "ReportSangtae3IsChecked", value.ToString(), SettingLocation);
            }
        }

        public bool SettingReportSangtae4IsChecked
        {
            get
            {
                StringBuilder value = new StringBuilder();

                GetPrivateProfileString("BOGUN", "ReportSangtae4IsChecked", "", value, 32, SettingLocation);

                return ((value?.ToString() ?? "false").ToLower() == "true" ? true : false);
            }

            set
            {
                WritePrivateProfileString("BOGUN", "ReportSangtae4IsChecked", value.ToString(), SettingLocation);
            }
        }
        //SangtaeAccept
        public bool SettingReportSangtaeAccept
        {
            get
            {
                StringBuilder value = new StringBuilder();

                GetPrivateProfileString("BOGUN", "ReportSangtaeAccept", "", value, 32, SettingLocation);

                return ((value?.ToString() ?? "false").ToLower() == "true" ? true : false);
            }

            set
            {
                WritePrivateProfileString("BOGUN", "ReportSangtaeAccept", value.ToString(), SettingLocation);
            }
        }

        public string SettingJidoCodeViewType
        {
            get
            {
                StringBuilder value = new StringBuilder();

                GetPrivateProfileString("BOGUN", "JidoCodeViewType", "1", value, 32, SettingLocation);

                return (value?.ToString() ?? "1");
            }

            set
            {
                WritePrivateProfileString("BOGUN", "JidoCodeViewType", value.ToString(), SettingLocation);
            }
        }
        #endregion

        #region TreeView Control
        DataTable _DTControlLevel1 = new DataTable();
        DataTable _DTControlLevel2 = new DataTable();
        DataTable _DTControlLevel3 = new DataTable();


        public DataTable ControlLevel1
        {
            get { return _DTControlLevel1; }
        }

        public void SetTreeView(Control con, int Level)
        {
            if (con == null)
                return;

            DataRow row;

            if (Level == 1)
            {
                foreach (var co in ((RadTabbedFormControl)con).Tabs)
                {
                    row = _DTControlLevel1.NewRow(); row["Name"] = co.Name; row["Level"] = 1; row["Text"] = co.Text;

                    _DTControlLevel1.Rows.Add(row);

                    if (co.Controls.Count > 0)
                        SetTreeView(co, 2);
                }
                //var tab = ((RadTabbedFormControl)con).Tabs.ToList();


                //tab.ToList().ForEach(t => { row = _DTControlLevel1.NewRow(); row["Name"] = t.Name; row["Level"] = 1; row["Text"] = t.Text; });
            }
            else if (Level == 2)
            {
                foreach (var co in con.Controls)
                {
                    if (co is UserControl)
                    {
                        SetTreeView(((UserControl)co), 2);
                    }
                    else
                    {
                        if (co is RadControl)
                        {
                            row = _DTControlLevel2.NewRow(); row["Name"] = ((RadControl)co).Name; row["Level"] = 1; row["Text"] = ((RadControl)co).Text; row["PName"] = con.Name;
                            _DTControlLevel2.Rows.Add(row);
                            //SetTreeView(con, 3);
                        }
                    }
                }
            }
            //else if (Level == 3)
            //{
            //    if (_DTControlLevel3.Columns.Contains("Name") == false)
            //        _DTControlLevel3.Columns.Add("Name", typeof(string));
            //    if (_DTControlLevel3.Columns.Contains("Level") == false)
            //        _DTControlLevel3.Columns.Add("Level", typeof(int));
            //    if (_DTControlLevel3.Columns.Contains("Text") == false)
            //        _DTControlLevel3.Columns.Add("Text", typeof(string));

            //    foreach (var co in con.Controls)
            //    {
            //        if (co is UserControl)
            //        {
            //            SetTreeView(con, 3);
            //        }
            //        else
            //        {
            //            if (co is RadControl)
            //            {
            //                row = _DTControlLevel3.NewRow(); row["Name"] = ((RadControl)co).Name; row["Level"] = 1; row["Text"] = ((RadControl)co).Text;
            //                _DTControlLevel3.Rows.Add(row);
            //                SetTreeView(con, 4);
            //            }
            //        }
            //    }
            //}
        }
        #endregion


        void SetQueryLog()
        {
            //this.mQuery = helper.ProcedureString;

            //LogManager.WriteLog(" " + mQuery + " ");

            //if (helper.TranYN == false)
            //{
            //    if (helper.ErrorMessage.Trim() != "")
            //        LogManager.WriteLog(" " + helper.ErrorMessage + " ");

            //    this.Error = helper.ErrorMessage;
            //}
            //else
            //{
            //    if (helper.TransactaionErrorMessage == null || helper.TransactaionErrorMessage.Count == 0)
            //        this.Error = "";
            //    else
            //        this.Error = helper.TransactaionErrorMessage[0];
            //}
        }

        public DataTable AlarmList(string pGubun)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_AlarmList";
                com.CommandType = CommandType.StoredProcedure;
                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("Gubun", SqlDbType.Char);
                p[0].Value = pGubun;
                com.Parameters.AddRange(p);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }

        /// <summary>
        /// 전센터 리스트
        /// </summary>
        /// <returns></returns>
        public DataTable GetCenterList()
        {
            if (_DTAllCenterList == null || _DTAllCenterList.Rows.Count < 1)
            {
                //DataTable result = new DataTable();
                _DTAllCenterList = new DataTable();

                using (SqlConnection con = new SqlConnection(_Connection))
                {
                    try
                    {
                        con.Open();
                        SqlCommand com = con.CreateCommand();
                        com.CommandText = "UP_NBOGUN_WPF_CenterList";
                        com.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] p = new SqlParameter[1];
                        p[0] = new SqlParameter("Center", SqlDbType.VarChar);
                        p[0].Value = _DHCenter;

                        com.Parameters.AddRange(p);

                        //서버로그 남기기기
                        WriteServerLog(com.CommandText, p);

                        SqlDataAdapter adapter = new SqlDataAdapter(com);

                        adapter.Fill(_DTAllCenterList);
                    }
                    catch (Exception ex)
                    {
                        LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
                    }
                }

                return _DTAllCenterList;
            }
            else
                return _DTAllCenterList;

        }

        public DataTable GetStaffList(string DHJikjong)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_StaffList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[3];
                    p[0] = new SqlParameter("Center", SqlDbType.Char);
                    p[0].Value = _DHCenter;
                    p[1] = new SqlParameter("Yearmon", SqlDbType.Char);
                    p[1].Value = _Yearmon;
                    p[2] = new SqlParameter("DHJikjong", SqlDbType.VarChar);
                    p[2].Value = DHJikjong;
                    com.Parameters.AddRange(p);

                    //서버로그 남기기기
                    WriteServerLog(com.CommandText, p);

                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    try
                    {
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        _Error = ex.Message;
                        LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
                    }
                    return dt;
                }
            }
        }

        public DataTable GetStaffList(string Yearmon, string DHJikjong)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_StaffList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[3];
                    p[0] = new SqlParameter("Center", SqlDbType.Char);
                    p[0].Value = _DHCenter;
                    p[1] = new SqlParameter("Yearmon", SqlDbType.Char);
                    p[1].Value = Yearmon;
                    p[2] = new SqlParameter("DHJikjong", SqlDbType.VarChar);
                    p[2].Value = DHJikjong;
                    com.Parameters.AddRange(p);

                    //서버로그 남기기기
                    WriteServerLog(com.CommandText, p);

                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    try
                    {
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        _Error = ex.Message;
                        LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
                    }
                    return dt;
                }
            }
        }

        /// <summary>
        /// 테스트하는 기능에 해당 사번이 있는 지 여부
        /// </summary>
        /// <param name="TestName"></param>
        /// <param name="Sabun"></param>
        /// <returns></returns>
        public DataTable TestCheck(string TestName, string Sabun)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_TesterCheck";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[2];
                    p[0] = new SqlParameter("TestName", SqlDbType.VarChar);
                    p[0].Value = TestName;
                    p[1] = new SqlParameter("Sabun", SqlDbType.VarChar);
                    p[1].Value = Sabun;
                    com.Parameters.AddRange(p);

                    //서버로그 남기기기
                    WriteServerLog(com.CommandText, p);

                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    try
                    {
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        _Error = ex.Message;
                        LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
                    }
                    return dt;
                }
            }
        }

        /// <summary>
        /// 사업장 검색
        /// </summary>
        /// <param name="SaupjaName"></param>
        /// <param name="Gubun">'1' 센터 전체 검색사업장, '2' 보건관리 실적사업장</param>
        /// <returns></returns>
        public DataTable SaupjaSearch(string SaupjaName, string Gubun)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_SaupjaSearchList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[4];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = DHCenter;
                p[1] = new SqlParameter("Yearmon", SqlDbType.Char);
                p[1].Value = "";
                p[2] = new SqlParameter("SaupjaName", SqlDbType.Char);
                p[2].Value = SaupjaName;
                p[3] = new SqlParameter("Gubun", SqlDbType.Char);
                p[3].Value = Gubun;

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);

                com.Parameters.AddRange(p);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable GetDamdangSaupjaList(string Sabun, string Gubun, string Center = "")
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_DamdangSaupjaList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[4];
                p[0] = new SqlParameter("Center", SqlDbType.VarChar);
                p[0].Value = Center == "" ? _DHCenter : Center;
                p[1] = new SqlParameter("Sabun", SqlDbType.VarChar);
                p[1].Value = Sabun;
                p[2] = new SqlParameter("Yearmon", SqlDbType.VarChar);
                p[2].Value = _Yearmon;
                p[3] = new SqlParameter("Gubun", SqlDbType.Char);
                p[3].Value = Gubun;

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);

                com.Parameters.AddRange(p);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }

        //UP_NBOGUN_JaehaejaList '00', 4196, '2021'
        public DataTable GetJaehaejaList(int SaupID, string Year)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_JaehaejaList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = _DHCenter;
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[1].Value = SaupID;
                p[2] = new SqlParameter("Year", SqlDbType.VarChar);
                p[2].Value = Year;

                com.Parameters.AddRange(p);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }

        //UP_NBOGUN_AnjunBogunEducationList '00', 4196, '2021-11-16', '2009112', '4'
        public DataTable GetEducationList(int SaupID, string VisitDate, string Visitor, string Gubun)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_AnjunBogunEducationList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[5];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = _DHCenter;
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[1].Value = SaupID;
                p[2] = new SqlParameter("VisitDate", SqlDbType.VarChar);
                p[2].Value = VisitDate;
                p[3] = new SqlParameter("Visitor", SqlDbType.VarChar);
                p[3].Value = Visitor;
                p[4] = new SqlParameter("Gubun", SqlDbType.VarChar);
                p[4].Value = Gubun;

                com.Parameters.AddRange(p);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }

        // UP_NBOGUN_VisitList '00', 4196, '2021-12', '40'
        public DataTable GetVisitList(int SaupID, string Gubun)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_VisitList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[4];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = _DHCenter;
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[1].Value = SaupID;
                p[2] = new SqlParameter("Yearmon", SqlDbType.VarChar);
                p[2].Value = _Yearmon;
                p[3] = new SqlParameter("Gubun", SqlDbType.VarChar);
                p[3].Value = Gubun;

                com.Parameters.AddRange(p);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable GetContractDateList(string SaupjaNum, string Date = "")
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_ContractList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = _DHCenter;
                p[1] = new SqlParameter("SaupjaNum", SqlDbType.Char);
                p[1].Value = SaupjaNum;
                p[2] = new SqlParameter("Date", SqlDbType.VarChar);
                p[2].Value = Date;

                com.Parameters.AddRange(p);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable GetContractDateList(string Center, string SaupjaNum, string Date = "")
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_ContractList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = Center;
                p[1] = new SqlParameter("SaupjaNum", SqlDbType.Char);
                p[1].Value = SaupjaNum;
                p[2] = new SqlParameter("Date", SqlDbType.VarChar);
                p[2].Value = Date;

                com.Parameters.AddRange(p);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable GetDHCodeList(string GroupName)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_DHCodeList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter("Name", SqlDbType.Char);
                para[0].Value = GroupName.ToString();

                com.Parameters.AddRange(para);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, para);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SaupID"></param>
        /// <param name="Yearmon"></param>
        /// <param name="Gubun">"3" : 방문일만 (VisitDate)</param>
        /// <returns></returns>
        public DataTable GetVisitDateList(int SaupID, string Yearmon, string Gubun)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_VisitList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[4];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = _DHCenter;
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[1].Value = SaupID;
                p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
                p[2].Value = Yearmon;
                p[3] = new SqlParameter("Gubun", SqlDbType.Char);
                p[3].Value = Gubun;

                com.Parameters.AddRange(p);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                try
                {
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
                }

                return dt;
            }
        }

        public DataTable GetVisitDateList(string DHCenter, int SaupID, string Yearmon, string Gubun)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_VisitList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[4];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = DHCenter;
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[1].Value = SaupID;
                p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
                p[2].Value = Yearmon;
                p[3] = new SqlParameter("Gubun", SqlDbType.Char);
                p[3].Value = Gubun;

                com.Parameters.AddRange(p);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                try
                {
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
                }

                return dt;
            }
        }

        public DataTable GetDHCodeList(CodeGroupName GroupName)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_DHCodeList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter("Name", SqlDbType.Char);
                para[0].Value = GroupName.ToString();

                com.Parameters.AddRange(para);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, para);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                try
                {
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
                }
                return dt;
            }
        }

        /// <summary>
        /// 마감 내역
        /// </summary>
        /// <param name="Gubun">"1" : 2개년, "2" : 당월</param>
        /// <returns></returns>
        public DataTable GetWeolboList(string Gubun = "1")
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_WeolboList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter("Center", SqlDbType.Char);
                para[1] = new SqlParameter("Yearmon", SqlDbType.Char);
                para[2] = new SqlParameter("Gubun", SqlDbType.Char);

                para[0].Value = _DHCenter;
                para[1].Value = _Yearmon;
                para[2].Value = Gubun;

                com.Parameters.AddRange(para);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable GetWeolboList(string Yearmon, string Gubun = "1")
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_WeolboList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter("Center", SqlDbType.Char);
                para[1] = new SqlParameter("Yearmon", SqlDbType.Char);
                para[2] = new SqlParameter("Gubun", SqlDbType.Char);

                para[0].Value = _DHCenter;
                para[1].Value = Yearmon;
                para[2].Value = Gubun;

                com.Parameters.AddRange(para);
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }

        public int MonthCarSave(string CarName)
        {
            //int r;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "NBOGUN_MonthCarSave";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[1] = new SqlParameter("Yearmon", SqlDbType.Char);
                p[2] = new SqlParameter("CarName", SqlDbType.VarChar);

                p[0].Value = _DHCenter;
                p[1].Value = _Yearmon;
                p[2].Value = CarName;

                com.Parameters.AddRange(p);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);

                int r = com.ExecuteNonQuery();

                return r;
            }
        }

        public DataTable MonthCarList()
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "NBOGUN_MonthCarList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[1] = new SqlParameter("Yearmon", SqlDbType.Char);

                p[0].Value = Center;
                p[1].Value = Yearmon;

                com.Parameters.AddRange(p);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }


        public DataTable GetBuseoCode()
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_BuseoCode";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("Center", SqlDbType.Char);

                p[0].Value = Center;

                com.Parameters.AddRange(p);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }

        /// <summary>
        /// 보건관리코드리스트
        /// </summary>
        /// <param name="Code">BH : 보호구</param>
        /// <returns></returns>
        public DataTable CodeList(string Code)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_CodeList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("Code", SqlDbType.Char);
                p[0].Value = Code;

                com.Parameters.AddRange(p);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}
