using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using System.Runtime.CompilerServices;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace NBOGUN
{
    partial class Biz
    {
        public Font FontDefault = new System.Drawing.Font("맑은 고딕", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        public Font FontStrong = new System.Drawing.Font("맑은 고딕", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        public static void SetTextBox(RadTextBox txt)
        {
            if (txt != null)
            {
                txt.TextChanged += Txt_TextChanged;
            }

        }
        public static Image Base64ToImage(string base64String)
        {
            // Convert base 64 string to byte[]
            byte[] imageBytes;// = Convert.FromBase64String(base64String);

            try
            {
                imageBytes = Convert.FromBase64String(base64String);

                // Convert byte[] to Image
            }
            catch
            {
                base64String += "=";

                try
                {
                    imageBytes = Convert.FromBase64String(base64String);
                }
                catch
                {
                    return null;
                }
            }

            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                Image image = Image.FromStream(ms, true);
                return image;
            }
        }
        public static void SetTextBox(RadMaskedEditBox txt)
        {
            if (txt != null)
            {
                txt.TextChanged += Txt_TextChanged;
            }

        }

        private static void Txt_TextChanged(object sender, EventArgs e)
        {
            RadTextBox txt = sender as RadTextBox;
            if (txt != null)
            {
                txt.Text = ConvertFullWidthToHalfWidth(txt.Text);
            }
            else if (sender is RadMaskedEditBox)
            {
                RadMaskedEditBox txte = sender as RadMaskedEditBox;
                if (txte != null)
                {
                    txte.Text = ConvertFullWidthToHalfWidth(txte.Text);
                }
            }

        }

        static string ConvertFullWidthToHalfWidth(string input)
        {
            var sb = new StringBuilder();
            foreach (char c in input)
            {
                if (c >= 0xFF01 && c <= 0xFF5E) // 전각문자 범위: 0xFF01 ~ 0xFF5E
                {
                    char convertedChar = (char)(c - 0xFEE0); // 반각문자로 변환
                    sb.Append(convertedChar);
                }
                else if (c == 0x3000) // 전각 공백 문자
                {
                    sb.Append(' '); // 반각 공백 문자로 변환
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        BackgroundWorker _WorkerFileUpload;
        public BackgroundWorker WorkerFileUpload
        {
            get { return _WorkerFileUpload; }
        }

        public string UploadStatus = "";
        public int UploadPercentage = 0;

        public static Color ButtonColor = Color.FromArgb(87, 139, 141);

        /// <summary>
        /// 파일 업로드
        /// </summary>
        /// <param name="TargetFolder">업로드될 폴더</param>
        /// <param name="LocalFileFullPath">파일의 로컬 경로</param>
        /// <param name="TargetFileName">업로드 된 후 서버에 저장될 파일명(확장포함). 공란으로 하면 로컬 파일명이 된다.</param>
        /// <returns></returns>
        public string FileUpload(string TargetFolder, string LocalFileFullPath, string TargetFileName = "")
        {
            if(_WorkerFileUpload != null && _WorkerFileUpload.IsBusy)
            {
                return "Upload";
            }

            _WorkerFileUpload = new BackgroundWorker();

            //string uri = "ftp://121.65.230.204:2011/" + TargetFolder + "/" + Path.GetFileName(LocalFileFullPath);

            _WorkerFileUpload.WorkerReportsProgress = true;
            _WorkerFileUpload.DoWork += _WorkerFileUpload_DoWork;
            _WorkerFileUpload.ProgressChanged += _WorkerFileUpload_ProgressChanged;
            _WorkerFileUpload.RunWorkerCompleted += _WorkerFileUpload_RunWorkerCompleted;
            _WorkerFileUpload.RunWorkerAsync(new string []{ TargetFolder, LocalFileFullPath, TargetFileName });

            return "";
        }

        public bool FileDelete(string TargetFolder, string FileName)
        {
            string uri;

            try
            {
                uri = "ftp://121.65.230.204:2011/" + TargetFolder + "/" + FileName;

                FtpWebRequest ftpWebRequest = WebRequest.Create(uri) as FtpWebRequest;

                ftpWebRequest.Credentials = new NetworkCredential("kiha", "health");
                ftpWebRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                ftpWebRequest.Proxy = null;
                ftpWebRequest.UseBinary = false;
                ftpWebRequest.UsePassive = true;
                ftpWebRequest.KeepAlive = false;

                //FtpWebResponse ftpWebResponse = ftpWebRequest.GetResponse() as FtpWebResponse;

                using (FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse())
                {
                    //return ftpWebResponse.StatusDescription;
                }
            }
            catch (WebException e)
            {
                String status = ((FtpWebResponse)e.Response).StatusDescription;

                LogManager.WriteLog(status);

                return false;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                LogManager.WriteLog(error);

                return false;
            }

            return true;
        }

        public static string EncodingUTF8(string str)
        {
            var encoding = Encoding.GetEncoding("UTF-8");//2014-06-26 추가 RD파라메터 한글 인식문제로 변경

            string report = HttpUtility.UrlEncode(str, encoding);

            return report;
        }

        public bool FileCopy(string Center, string FileName1, string FileName2)
        {
            string TargetFolder = "/BOGUN_Jido/C" + Center;
            
            string mfileName = FileName1;
            
            string uri = "ftp://121.65.230.204:2011/" + TargetFolder + "/" + mfileName;
            string uri_folder = "ftp://121.65.230.204:2011/" + TargetFolder + "/";


            FtpWebRequest reqFTP;

            // Create FtpWebRequest object from the Uri provided
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

            // Provide the WebPermission Credintials
            reqFTP.Credentials = new NetworkCredential("kiha", "health");

            // By default KeepAlive is true, where the control connection is not closed
            // after a command is executed.
            reqFTP.KeepAlive = false;

            // Specify the command to be executed.
            FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);//기존 파일
                //request.Credentials = new NetworkCredential(userName, password);

                Stream responseStream = response.GetResponseStream();
                UploadForCopy(uri_folder + FileName2, ToByteArray(responseStream), "kiha", "health");//새로운 파일
                responseStream.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 내부적으로 파일 복사하기 위해 사용
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public Byte[] ToByteArray(Stream stream)
        {
            MemoryStream ms = new MemoryStream();
            byte[] chunk = new byte[4096];
            int bytesRead;
            while ((bytesRead = stream.Read(chunk, 0, chunk.Length)) > 0)
            {
                ms.Write(chunk, 0, bytesRead);
            }

            return ms.ToArray();
        }

        private bool UploadForCopy(string FileName, byte[] Image, string FtpUsername, string FtpPassword)
        {
            try
            {
                System.Net.FtpWebRequest clsRequest = (System.Net.FtpWebRequest)System.Net.WebRequest.Create(FileName);
                clsRequest.Credentials = new System.Net.NetworkCredential(FtpUsername, FtpPassword);
                clsRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
                System.IO.Stream clsStream = clsRequest.GetRequestStream();
                clsStream.Write(Image, 0, Image.Length);

                clsStream.Close();
                clsStream.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void _WorkerFileUpload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UploadStatus = "";
            _WorkerFileUpload.Dispose();
            _WorkerFileUpload = null;
        }

        private void _WorkerFileUpload_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            UploadStatus = "업로드 중";
            UploadPercentage = e.ProgressPercentage;
        }

        private void _WorkerFileUpload_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] arr = e.Argument as string[];

            UploadStatus = "업로드 시작";
            _WorkerFileUpload.ReportProgress(0);

            string TargetFolder = arr[0];// "/BOGUN_Jido/C" + _DHCenter;
            string LocalFileFullPath = arr[1];// e.Argument.ToString();
            string FileName = Path.GetFileName(LocalFileFullPath);
            if (arr[2] != "")
            {
                FileName = arr[2];
            }
            //string Extension = ""; //확장자
            long totalSize = 0;
            long currentSize = 0;

            
            //Extension = LocalFileFullPath.Substring(LocalFileFullPath.LastIndexOf('.'));
            FileInfo fileInf = new FileInfo(LocalFileFullPath);

            string uri;

            uri = "ftp://121.65.230.204:2011/" + TargetFolder + "/" + FileName;

            //if(DirectoryExists("ftp://192.168.100.2:2011//" + mfolder) == false)
            //    MakeDirectory("ftp://192.168.100.2:2011//" + mfolder);

            FtpWebRequest reqFTP;

            // Create FtpWebRequest object from the Uri provided
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

            // Provide the WebPermission Credintials
            reqFTP.Credentials = new NetworkCredential("kiha", "health");

            // By default KeepAlive is true, where the control connection is not closed
            // after a command is executed.
            reqFTP.KeepAlive = false;

            // Specify the command to be executed.
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            // Specify the data transfer type.
            reqFTP.UseBinary = true;
            reqFTP.UsePassive = true;

            // Notify the server about the size of the uploaded file
            reqFTP.ContentLength = fileInf.Length;
            totalSize = fileInf.Length;//총 사이즈 계산

            // The buffer size is set to 2kb
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;

            // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
            FileStream fs = fileInf.OpenRead();

            if (fs.Length < 1)//만약 0바이트 파일같은 경우는 올리지 않는다
            {
                // memberWorker.ReportProgress(-1, grdFileList.Rows[i].Cells["clmFileUploadSeq"].Value.ToString());
                // continue;
            }


            // Stream to which the file to be upload is written
            Stream strm = reqFTP.GetRequestStream();

            // Read from the file stream 2kb at a time
            contentLen = fs.Read(buff, 0, buffLength);

            // Till Stream content ends
            while (contentLen != 0)
            {
                //System.Threading.Thread.Sleep(1);
                // Write Content from the file stream to the FTP Upload Stream
                strm.Write(buff, 0, contentLen);
                contentLen = fs.Read(buff, 0, buffLength);

                currentSize += contentLen;

                //int percentage = 0;

                //if (contentLen > 0)
                int percentage = (int)((float)currentSize / (float)totalSize * 100);

                //UploadPercentage = percentage;
                _WorkerFileUpload.ReportProgress(percentage);
                if(percentage > 10)
                {

                }
            }
            _WorkerFileUpload.ReportProgress(100);

            // Close the file stream and the Request Stream
            strm.Close();
            fs.Close();
        }

        /// <summary>
        /// 날짜 형식이 아닐 경우 공란을 리턴
        /// </summary>
        /// <param name="Date"></param>
        /// <returns></returns>
        public string GetDateTime(string Date)
        {
            try
            {
                return Convert.ToDateTime(Date).ToString("yyyy-MM-dd");
            }
            catch
            {
                return "";
            }
        }

        public static int? GetNumber(string value)
        {

            string strTmp = Regex.Replace(value, @"\D", "");
            if (strTmp.Trim() == "")
                return null;

            int nTmp = int.Parse(strTmp);
            return nTmp;
        }

        /// <summary>
        /// 나이 계산
        /// </summary>
        /// <param name="BirthDay">주민 번호 앞 7자리 ex) 7903051 </param>
        /// <param name="StandarDay">나이 계산할 기준일자 ex) 2020-01-05 </param>
        /// <returns></returns>
        public int GetAgeFromDate(string BirthDay, string StandarDay = "")
        {
            if (StandarDay == "")
                StandarDay = DateTime.Now.ToString("yyyy-MM-dd");

            if (BirthDay.Replace("-", "").Replace("_", "").Length < 6)
                return -1;

            string year = "19";

            try
            {
                Convert.ToInt32(BirthDay.Replace("-", "").Replace("_", ""));

                TimeSpan span = new TimeSpan();
                span = Convert.ToDateTime(StandarDay) - Convert.ToDateTime("19" + BirthDay.Substring(0, 2) + "-" + BirthDay.Substring(2, 2) + "-" + BirthDay.Substring(4, 2));
                if (span.Days / 365 > 90)
                    year = "20";

                if (BirthDay.Replace("-", "").Replace("_", "").Length == 6)
                {
                    span = Convert.ToDateTime(StandarDay) - Convert.ToDateTime(year + BirthDay.Substring(0, 2) + "-" + BirthDay.Substring(2, 2) + "-" + BirthDay.Substring(4, 2));

                    return span.Days / 365;
                }
            }
            catch
            {
                return -1;
            }

            string j1 = BirthDay.Replace("-", "").Replace("_", "");
            string b = "";
            TimeSpan s = new TimeSpan();

            if (j1.Substring(6, 1) == "1" || j1.Substring(6, 1) == "2" || j1.Substring(6, 1) == "5" || j1.Substring(6, 1) == "6")
            {
                b = "19" + j1.Substring(0, 2) + "-" + j1.Substring(2, 2) + "-" + j1.Substring(4, 2);

                s = Convert.ToDateTime(StandarDay) - Convert.ToDateTime(b);
            }
            else if (j1.Substring(6, 1) == "3" || j1.Substring(6, 1) == "4" || j1.Substring(6, 1) == "7" || j1.Substring(6, 1) == "8")
            {
                b = "20" + j1.Substring(0, 2) + "-" + j1.Substring(2, 2) + "-" + j1.Substring(4, 2);

                s = Convert.ToDateTime(StandarDay) - Convert.ToDateTime(b);
            }
            else
            {
                b = year + j1.Substring(0, 2) + "-" + j1.Substring(2, 2) + "-" + j1.Substring(4, 2);

                s = Convert.ToDateTime(BirthDay) - Convert.ToDateTime(b);
            }
            //txtPersonAge.Text = (s.Days / 365).ToString() + "세";

            return s.Days / 365;
        }

        public DataSet COMMON_Organization()
        {
            DataSet dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataSet();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_COMMON_Organization";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] para = new SqlParameter[0];

                com.Parameters.AddRange(para);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, para);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }
        #region 면허
        DataTable _DTLicense;

        public DataTable GetLicense
        {
            get
            {
                if (_DTLicense == null)
                    SetLicense();

                return _DTLicense;
            }
        }

        public DataTable SetLicense()
        {
            using (SqlConnection con = new SqlConnection(_Connection))
            {
                _DTLicense = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_BOGUN_DHLicenseList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("Year", SqlDbType.Char);
                //UserID = "1992052";
                p[0].Value = Yearmon == null ? DateTime.Now.Year.ToString() : Yearmon.Substring(0, 4);

                com.Parameters.AddRange(p);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(_DTLicense);
                return _DTLicense;
            }
        }

        #endregion

        #region 업무담당코드
        public DataTable GetUpmuDamdang
        {
            get
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Code", typeof(string));
                dt.Columns.Add("CodeName", typeof(string));

                DataRow row;
                row = dt.NewRow();
                row["Code"] = "DH2701";
                row["CodeName"] = "보건";
                dt.Rows.Add(row);

                row = dt.NewRow();
                row["Code"] = "DH2702";
                row["CodeName"] = "안전";
                dt.Rows.Add(row);               

                return dt;
            }
        }
        #endregion

        #region 센터 코드
        DataTable _DTBogunCenter;
        public DataTable GetBogunCenter
        {
            get
            {
                if(_DTBogunCenter == null)
                {
                    SetBogunCenter();
                }

                return _DTBogunCenter.Copy();
            }
        }
        public DataTable SetBogunCenter()
        {
            using (SqlConnection con = new SqlConnection(_Connection))
            {
                _DTBogunCenter = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_CenterList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("Yearmon", SqlDbType.Char);
                //UserID = "1992052";
                p[0].Value = Yearmon == null ? "" : Yearmon;

                com.Parameters.AddRange(p);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(_DTBogunCenter);
                return _DTBogunCenter;
            }
        }
        #endregion

        #region 검진구분 코드
        DataTable _DTGumjinGubun;

        public DataTable GetGumjinGubunList
        {
            get
            {
                if (_DTGumjinGubun == null)
                {
                    SetCodeGumjinGubun();
                }

                return _DTGumjinGubun.Copy();
            }
        }

        public void SetCodeGumjinGubun()
        {
            using (SqlConnection con = new SqlConnection(_Connection))
            {
                _DTGumjinGubun = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_GumjinGubunList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] para = new SqlParameter[0];

                com.Parameters.AddRange(para);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, para);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(_DTGumjinGubun);
            }
        }
        #endregion

        #region 측정단위 코드
        DataTable _DTChkjnDanwee;

        public DataTable GetCodeChkjnDanwee
        {
            get
            {
                if (_DTChkjnDanwee == null)
                {
                    _DTChkjnDanwee = Biz.Instance.GetDHCodeList("측정단위");
                }

                return _DTChkjnDanwee.Copy();
            }
        }
        #endregion

        #region 측정단위 코드
        DataTable _DTChkjnResultGubun;

        public DataTable GetCodeChkjnResultGubun
        {
            get
            {
                if (_DTChkjnResultGubun == null)
                {
                    _DTChkjnResultGubun = new DataTable();
                    _DTChkjnResultGubun.Columns.Add("Name", typeof(string));
                    DataRow dr = _DTChkjnResultGubun.NewRow();
                    dr["Name"] = "TWAppm";
                    _DTChkjnResultGubun.Rows.Add(dr);
                    dr = _DTChkjnResultGubun.NewRow();
                    dr["Name"] = "TWAmg";
                    _DTChkjnResultGubun.Rows.Add(dr);
                    dr = _DTChkjnResultGubun.NewRow();
                    dr["Name"] = "STELppm";
                    _DTChkjnResultGubun.Rows.Add(dr);
                    dr = _DTChkjnResultGubun.NewRow();
                    dr["Name"] = "STELmg";
                    _DTChkjnResultGubun.Rows.Add(dr);
                    dr = _DTChkjnResultGubun.NewRow();
                    dr["Name"] = "EtcStd";
                    _DTChkjnResultGubun.Rows.Add(dr);
                }

                return _DTChkjnResultGubun.Copy();
            }
        }
        #endregion


        #region 교육구분 코드
        DataTable _DTEduGubun;

        public DataTable GetCodeEduGubun
        {
            get
            {
                if (_DTEduGubun == null)
                {
                    SetCodeEduGubun();
                }

                return _DTEduGubun;
            }
        }

        public void SetCodeEduGubun()
        {
            _DTEduGubun = new DataTable();
            _DTEduGubun.Columns.Add("Name", typeof(string));
            DataRow row;

            row = _DTEduGubun.NewRow();
            row["Name"] = "협회";
            _DTEduGubun.Rows.Add(row);
            row = _DTEduGubun.NewRow();
            row["Name"] = "자체";
            _DTEduGubun.Rows.Add(row);

            row = _DTEduGubun.NewRow();
            row["Name"] = "외부";
            _DTEduGubun.Rows.Add(row);
        }
        #endregion

        #region 교육 방법 코드
        DataTable _DTEduBangbup;

        public DataTable GetCodeEduBangbup
        {
            get
            {
                if (_DTEduBangbup == null)
                {
                    _DTEduBangbup = Biz.Instance.GetDHCodeList("사업장안전보건교육방법");
                }

                return _DTEduBangbup.Copy();
            }
        }
        #endregion

        #region 교육 강사 코드
        DataTable _DTEduTeacher;

        public DataTable GetCodeEduTeacher
        {
            get
            {
                if (_DTEduTeacher == null)
                {
                    _DTEduTeacher = Biz.Instance.GetDHCodeList("보건교육실시계획강사");
                }

                return _DTEduTeacher.Copy();
            }
        }
        #endregion

        #region 안전보건관리책임자 부재 사유 코드
        DataTable _DTAnjunCause;

        public DataTable GetAnjunCause
        {
            get
            {
                if (_DTAnjunCause == null)
                {
                    _DTAnjunCause = Biz.Instance.GetDHCodeList("안전보건관리책임자부재사유");
                }

                return _DTAnjunCause.Copy();
            }
        }
        #endregion

        #region 교육물품 코드
        DataTable _DTEduItem;

        public DataTable GetCodeEduItem
        {
            get
            {
                if (_DTEduItem == null)
                {
                    SetCodeEduItem();
                }

                return _DTEduItem.Copy();
            }
        }

        public void SetCodeEduItem()
        {
            if (_DTEduItem == null)
            {
                _DTEduItem = Biz.Instance.PlanEduItemCode("", "");
            }
        }
        #endregion

        #region 교육물품 보급형태
        DataTable _DTEduItemType;

        public DataTable GetCodeEduItemType
        {
            get
            {
                if (_DTEduItemType == null)
                {
                    SetCodeEduItemType();
                }

                return _DTEduItemType.Copy();
            }
        }

        public void SetCodeEduItemType()
        {
            if (_DTEduItemType == null)
            {
                _DTEduItemType = Biz.Instance.GetDHCodeList("보급형태");
            }
        }
        #endregion

        DataTable _DTBalhaengGubun;

        public DataTable GetBalhaengGubunList
        {
            get
            {
                if(_DTBalhaengGubun == null)
                {
                    _DTBalhaengGubun = Biz.Instance.GetDHCodeList("발행구분");
                }

                return _DTBalhaengGubun.Copy();
            }
        }

        DataTable _TableJidoSangtae;

        public DataTable Code_JidoSangtae
        {
            get
            {
                if (_TableJidoSangtae == null)
                {
                    _TableJidoSangtae = Biz.Instance.GetDHCodeList("지도상태");
                }

                return _TableJidoSangtae.Copy();
            }
        }

        public static bool URLExists(string url)
        {
            bool result = false;

            WebRequest webRequest;
            HttpWebResponse response = null;

            try
            {
                webRequest = WebRequest.Create(url);
                webRequest.Timeout = 1200; // miliseconds
                webRequest.Method = "HEAD";

                response = (HttpWebResponse)webRequest.GetResponse();
                result = true;
            }
            catch (WebException webException)
            {
                LogManager.WriteLog("URLExists : " + webException.Message);
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }

            return result;
        }

        //public static bool URLDelete();
        public static string ShortURL(string longurl)
        {
            StringBuilder sb = new StringBuilder();

            //base
            sb.Append(@"http://api.bit.ly/v3/shorten?login=o_7ufhlrkhs9&apiKey=R_73aba01adeec40b0a1c746d48e944086&longUrl=" + HttpUtility.UrlEncode(longurl) + "&format=txt");
            //server page
            //sb.Append(HttpUtility.UrlEncode(longurl));

            // Make Short URL
            try
            {
                WebClient client = new WebClient();
                string shorturl = client.DownloadString(new Uri(sb.ToString()));

                return shorturl.Replace("\n", "");
            }
            catch
            {
                return longurl;
            }

        }

        public static bool FTPDelete(string folder, string fileName)
        {
            string uri;

            try
            {
                try
                {
                    uri = "ftp://" + "121.65.230.204" + ":2011/" + folder + "/" + fileName;

                    //uri = k; Bogun 폴더에서의 삭제는 되고 하위 폴더는 삭제가 안됨 확인 필요
                }
                catch
                {
                    uri = "ftp://192.168.100.11:2011/" + folder + "/" + fileName;
                }

                FtpWebRequest ftpWebRequest = WebRequest.Create(uri) as FtpWebRequest;

                ftpWebRequest.Credentials = new NetworkCredential("kiha", "health");
                ftpWebRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                ftpWebRequest.Proxy = null;
                ftpWebRequest.UseBinary = false;
                ftpWebRequest.UsePassive = true;
                ftpWebRequest.KeepAlive = false;

                //FtpWebResponse ftpWebResponse = ftpWebRequest.GetResponse() as FtpWebResponse;

                using (FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse())
                {
                    //return ftpWebResponse.StatusDescription;
                }
            }
            catch (WebException e)
            {
                try
                {
                    FtpWebRequest requestFileDelete = (FtpWebRequest)WebRequest.Create("ftp://192.168.101.14:2011//" + folder + "/" + fileName);
                    requestFileDelete.Credentials = new NetworkCredential("kiha", "health");
                    requestFileDelete.Method = WebRequestMethods.Ftp.DeleteFile;
                }
                catch
                {

                }
                String status = ((FtpWebResponse)e.Response).StatusDescription;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(ex.Message);
                return false;
            }

            return true;
        }

        //static AbortableBackgroundWorker _WorkerUpload;
        //public static string mfolder = "";
        //public static string mfileFullpath = "";
        //public static string mUploadIP = "";
        //public static string mfileName = "";
        //public static int UploadPercentage = 0;
        //public static int UploadPercentage = 0;
        //public static void FTPUpload(string folder1, string fileFullpath1, string fileName1, string port)
        //{
        //    _WorkerUpload = new AbortableBackgroundWorker();

        //    //mfolder = folder1;
        //    //mfileFullpath = fileFullpath1;
        //    //mfileName = fileName1;
        //    //mPort = port;

        //    _WorkerUpload.DoWork += _WorkerUpload_DoWork;
        //    _WorkerUpload.RunWorkerAsync();
        //}

        //private static void _WorkerUpload_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    long totalSize = 0;
        //    long currentSize = 0;

        //    string extension = ""; //확장자
        //    extension = mfileFullpath.Substring(mfileFullpath.LastIndexOf('.'));
        //    FileInfo fileInf = new FileInfo(mfileFullpath);

        //    string uri;

        //    try
        //    {
        //        IPAddress.Parse(mUploadIP);

        //        uri = "ftp://" + mUploadIP + ":2011//" + mfolder + "/" + mfileName + extension;
        //    }
        //    catch
        //    {
        //        uri = "ftp://192.168.100.2:2011//" + mfolder + "/" + mfileName + extension;
        //    }

        //    //if(DirectoryExists("ftp://192.168.100.2:2011//" + mfolder) == false)
        //    //    MakeDirectory("ftp://192.168.100.2:2011//" + mfolder);

        //    FtpWebRequest reqFTP;

        //    // Create FtpWebRequest object from the Uri provided
        //    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

        //    // Provide the WebPermission Credintials
        //    reqFTP.Credentials = new NetworkCredential("kiha", "health");

        //    // By default KeepAlive is true, where the control connection is not closed
        //    // after a command is executed.
        //    reqFTP.KeepAlive = false;

        //    // Specify the command to be executed.
        //    reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

        //    // Specify the data transfer type.
        //    reqFTP.UseBinary = true;
        //    reqFTP.UsePassive = true;

        //    // Notify the server about the size of the uploaded file
        //    reqFTP.ContentLength = fileInf.Length;
        //    totalSize = fileInf.Length;//총 사이즈 계산

        //    // The buffer size is set to 2kb
        //    int buffLength = 2048;
        //    byte[] buff = new byte[buffLength];
        //    int contentLen;

        //    // Opens a file stream (System.IO.FileStream) to read the file to be uploaded
        //    FileStream fs = fileInf.OpenRead();

        //    if (fs.Length < 1)//만약 0바이트 파일같은 경우는 올리지 않는다
        //    {
        //        // memberWorker.ReportProgress(-1, grdFileList.Rows[i].Cells["clmFileUploadSeq"].Value.ToString());
        //        // continue;
        //    }


        //    // Stream to which the file to be upload is written
        //    Stream strm = reqFTP.GetRequestStream();

        //    // Read from the file stream 2kb at a time
        //    contentLen = fs.Read(buff, 0, buffLength);

        //    // Till Stream content ends
        //    while (contentLen != 0)
        //    {
        //        // Write Content from the file stream to the FTP Upload Stream
        //        strm.Write(buff, 0, contentLen);
        //        contentLen = fs.Read(buff, 0, buffLength);

        //        currentSize += contentLen;

        //        int percentage = 0;

        //        if (contentLen > 0)
        //            percentage = (int)((float)currentSize / (float)totalSize * 100);
        //        else
        //            percentage = 100;

        //        UploadPercentage = percentage;
        //        memberWorker.ReportProgress(percentage);
        //    }

        //    // Close the file stream and the Request Stream
        //    strm.Close();
        //    fs.Close();
        //}

        /// <summary>
        /// 이미지 리사이즈
        /// </summary>
        /// <param name="imgToResize"></param>
        /// <param name="size">new Size(16,16)</param>
        /// <returns>Image</returns>
        public static Image ResizeImage(Image imgToResize, Size size)
        {
            try
            {
                return (Image)(new Bitmap(imgToResize, size));
            }
            catch
            {
                return null;
            }
        }

        public static byte[] ImageToByteArray(Image image)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        public static bool CheckingSpecialText(string txt)
        {
            string str = @"[~!@\#$%^&*\()\=+|\\/:;?""<>',]";
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(str);
            return rex.IsMatch(txt);
        }

        public static string ReportHtml5(string ReportName, string[] Parameters)
        {
            string add = "", para = "";

            for (int i = 0; i < Parameters.Length; i++)
            {
                para += "[" + Parameters[i] + "]";
            }

            //var encoding = Encoding.GetEncoding("euc-kr");
            var encoding = Encoding.GetEncoding("UTF-8");//2014-06-26 추가 RD파라메터 한글 인식문제로 변경

            string report = HttpUtility.UrlEncode(ReportName, encoding);

            string pa = HttpUtility.UrlEncode(para, encoding);//2014-06-26 추가

            add += "https://ohis.kiha21.or.kr/mis/RD/BOGUN_Report_Html5.aspx?RPTNAME=" + report + ".mrd&PARAM=" + pa + "&PAGEORDER=%5b%5d";

            return add;
        }

        public static string ReportHtml5(string ReportName, string[] Parameters, string PageOrder)
        {
            string add = "", para = "";

            for (int i = 0; i < Parameters.Length; i++)
            {
                para += "[" + Parameters[i] + "]";
            }

            //var encoding = Encoding.GetEncoding("euc-kr");
            var encoding = Encoding.GetEncoding("UTF-8");//2014-06-26 추가 RD파라메터 한글 인식문제로 변경

            string report = HttpUtility.UrlEncode(ReportName, encoding);

            string pa = HttpUtility.UrlEncode(para, encoding);//2014-06-26 추가

            add += "https://ohis.kiha21.or.kr/mis/RD/BOGUN_Report_Html5.aspx?RPTNAME=" + report + ".mrd&PARAM=" + pa + "&PAGEORDER=%5b" + PageOrder + "%5d";

            return add;
        }

        public static void PrintEdge(string Url)
        {
            try
            {
                Process.Start("microsoft-edge:" + Url);
            }
            catch
            {
                
            }

            try
            {
                Clipboard.SetText(Url);
            }
            catch
            {

            }
        }

        RadWaitingBar _waitingBar;

        public RadWaitingBar SetWaitingBar()
        {
            _waitingBar = new RadWaitingBar();
            _waitingBar.ThemeName = "VisualStudio2012LightTheme";
            _waitingBar.WaitingStyle = Telerik.WinControls.Enumerations.WaitingBarStyles.DotsRing;
            _waitingBar.WaitingSpeed = 5;
            _waitingBar.Size = new System.Drawing.Size(400, 200);
            //_wbSaupja.AssociatedControl = grdSaupja;
            _waitingBar.ShowText = true;
            _waitingBar.Text = "";
            _waitingBar.KeyPress += _waitingBar_KeyPress;
            return _waitingBar;
        }

        private void _waitingBar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                _waitingBar.StopWaiting();
        }

        public static bool IsValidEmail(string email)
        {
            bool valid = Regex.IsMatch(email, @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?");
            return valid;
        }

        public static bool IsValidTel(string tel)
        {
            tel = tel.Replace("-", "");

            if (tel.Trim().Length != 10 && tel.Trim().Length != 11)
                return false;

            bool valid = Regex.IsMatch(tel, @"01{1}[016789]{1}[0-9]{7,8}");
            return valid;
        }

        #region ExportGridView
        static RadWaitingBar radWaitingBar;
        [DllImport("shcore.dll")]
        static extern int SetProcessDpiAwareness(_Process_DPI_Awareness value);

        enum _Process_DPI_Awareness
        {
            Process_DPI_Unaware = 0,
            Process_System_DPI_Aware = 1,
            Process_Per_Monitor_DPI_Aware = 2
        }
        public static void ExportGridView(RadGridView grd)
        {
            SetProcessDpiAwareness(_Process_DPI_Awareness.Process_DPI_Unaware);

            try
            {
                string excelName = Guid.NewGuid().ToString();

                if (Directory.Exists(@"c:\kiha\bogun\temp") == false)
                {
                    Directory.CreateDirectory(@"c:\kiha\bogun\temp");
                }

                if (Directory.Exists(@"c:\kiha\bogun\temp") == true)
                {
                    excelName = @"c:\kiha\bogun\temp\" + excelName + ".xlsx";
                }
                else
                    excelName = @"..\..\" + excelName + ".xlsx";

                //그리드의 엑셀 변환
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    Telerik.WinControls.Export.GridViewSpreadExport exporter = new Telerik.WinControls.Export.GridViewSpreadExport(grd);
                    //exporter.CellFormatting += spreadExporter_CellFormatting;
                    Telerik.WinControls.Export.SpreadExportRenderer renderer = new Telerik.WinControls.Export.SpreadExportRenderer();
                    exporter.ExportChildRowsGrouped = true;
                    exporter.ExportHierarchy = true;
                    exporter.ExportVisualSettings = true;
                    exporter.FreezePinnedColumns = true;
                    //exporter.ExportGroupedColumns = true;
                    exporter.HiddenColumnOption = Telerik.WinControls.UI.Export.HiddenOption.DoNotExport;
                    exporter.ChildViewExportMode = Telerik.WinControls.UI.Export.ChildViewExportMode.ExportAllViews;
                    exporter.RunExport(ms, renderer);

                    using (System.IO.FileStream fileStream = new System.IO.FileStream(excelName, FileMode.Create, FileAccess.Write))
                    {
                        ms.WriteTo(fileStream);
                    }
                }

                Process process = new Process();

                process.StartInfo.FileName = excelName;

                process.Start();
            }
            catch
            {
                Random rnd = new Random(DateTime.Now.Millisecond);
                string na = rnd.Next(100000, 999999).ToString();

                string exportFile = @"..\..\" + na + ".xlsx";
                _exportFile = exportFile;
                radWaitingBar = new RadWaitingBar();
                radWaitingBar = new RadWaitingBar();
                radWaitingBar.ThemeName = "Office2019Light";
                radWaitingBar.WaitingStyle = Telerik.WinControls.Enumerations.WaitingBarStyles.DotsRing;
                radWaitingBar.WaitingSpeed = 25;
                radWaitingBar.Size = new System.Drawing.Size(500, 300);
                radWaitingBar.Text = "";
                radWaitingBar.StartWaiting();
                radWaitingBar.AssociatedControl = grd;

                ////그리드의 엑셀 변환
                ms = new System.IO.MemoryStream();

                Telerik.WinControls.Export.GridViewSpreadExport exporter = new Telerik.WinControls.Export.GridViewSpreadExport(grd);
                //exporter.CellFormatting += spreadExporter_CellFormatting;
                Telerik.WinControls.Export.SpreadExportRenderer renderer = new Telerik.WinControls.Export.SpreadExportRenderer();
                exporter.ExportChildRowsGrouped = true;
                exporter.ExportHierarchy = true;
                exporter.ExportVisualSettings = true;
                exporter.FreezePinnedColumns = true;
                exporter.ExportGroupedColumns = true;

                exporter.AsyncExportProgressChanged += Exporter_AsyncExportProgressChanged;
                exporter.AsyncExportCompleted += Exporter_AsyncExportCompleted;
                exporter.RunExportAsync(ms, renderer);
            }
            

        }

        static string _exportFile = "";
        static System.IO.MemoryStream ms;

        private static void Exporter_AsyncExportCompleted(object sender, AsyncCompletedEventArgs e)
        {
            using (System.IO.FileStream fileStream = new System.IO.FileStream(_exportFile, FileMode.Create, FileAccess.Write))
            {
                ms.WriteTo(fileStream);
            }

            ms.Close();

            radWaitingBar.StopWaiting();

            Process process = new Process();

            process.StartInfo.FileName = _exportFile;

            process.Start();
        }

        private static void Exporter_AsyncExportProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            radWaitingBar.Text = "엑셀 변환 중 : " + e.ProgressPercentage.ToString() + "/100";
            radWaitingBar.WaitingIndicators[0].Text = "엑셀 변환 중 : " + e.ProgressPercentage.ToString() + "/100";
            radWaitingBar.Update();
            Application.DoEvents();
        }
        #endregion


        public static Image GetOrientation(Image image)
        {
            int orientation = 0;

            try
            {
                // EXIF 정보를 가져옵니다.
                foreach (PropertyItem propItem in image.PropertyItems)
                {
                    if (propItem.Id == 0x0112) // Orientation
                    {
                        orientation = propItem.Value[0];
                        break;
                    }
                }
            }
            catch
            {
                // 예외 처리
            }

            switch (orientation)
            {
                case 1: // No rotation required.
                    return image;

                case 2: // Flip horizontally.
                    image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    break;

                case 3: // Rotate 180 degrees.
                    image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;

                case 4: // Flip vertically.
                    image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    break;

                case 5: // Rotate 90 degrees clockwise and flip vertically.
                    image.RotateFlip(RotateFlipType.Rotate90FlipY);
                    break;

                case 6: // Rotate 90 degrees clockwise.
                    image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;

                case 7: // Rotate 270 degrees clockwise and flip vertically.
                    image.RotateFlip(RotateFlipType.Rotate270FlipY);
                    break;

                case 8: // Rotate 270 degrees clockwise.
                    image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
            }
            image.RemovePropertyItem(274);
            return image;
        }
        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out string pszPath);
        private static class KnownFolder
        {
            public static readonly Guid Downloads = new Guid("374DE290-123F-4565-9164-39C4925E467B");
        }
        public static string DownloadFolder
        {
            get
            {
                string mFileDownLoadPath = "";
                SHGetKnownFolderPath(KnownFolder.Downloads, 0, IntPtr.Zero, out mFileDownLoadPath);
                return mFileDownLoadPath;
            }
        }
    }
}
