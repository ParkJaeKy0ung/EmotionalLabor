using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace NBOGUN
{
    public class MailResult
    {
        public string ReceiveEmail { get; set; }
        public string Result { get; set; }
        public string MailId { get; set; }
    }
    public class MailReceive
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        /// <summary>
        /// 발송결과 Y, N
        /// </summary>
        public string Result { get; set; }

        public string ReadYN { get; set; }

        public string ReadDate { get; set; }
    }

    public class MailFile
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string OriginName { get; set; }
    }
    public class MailInfo
    {
        string _Id = "";
        MailReceive[] _ReceiveAddresses;
        MailFile[] _MailFile;

        public MailFile[] Files { get; set; }
        string _ReadYN = "";
        public string ReadYN { get => _ReadYN; set => _ReadYN = value; }

        /// <summary>
        /// 메일발송 제목
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 메일 Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 메일 발송 내용
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 수신자 정보
        /// </summary>
        public MailReceive[] ReceiveAddresses { get => _ReceiveAddresses; set => _ReceiveAddresses = value; }

        /// <summary>
        /// 메일 전송 시간
        /// </summary>
        public string SendDate { get; set; }

        public string Result { get; set; }


        /// <summary>
        /// 전송
        /// </summary>
        public string SenderName { get; set; }

        public string SenderMail { get; set; }

        public int FilesCnt { get; set; }


        public MailInfo(string Id)
        {
            _Id = Id;
        }
    }

    public class KihaMail
    {
        string _title = "";
        string _content = "";
        string _sendInfo = "";
        string _rcvInfo = "";
        string _sendDate = "";
        string _sendType = "";
        string _categoryNm = "";
        string _linkNm = "";
        string _memo = "";

        string _files0 = "";
        string _files1 = "";
        string _files2 = "";
        string _files3 = "";
        string _files4 = "";
        string _fileYn = "";
        string _userId = "";
        string[] _ReceiveAddress;
        string[] _Files;
        string _Error = "";
        /// <summary>
        /// 전송 후 오류
        /// </summary>
        public string Error { get => _Error; }

        //public static MailInfo GetMailInfo(string Id)
        //{
        //    MailInfo info = new MailInfo(Id);

        //    var v = (JObject)JsonConvert.DeserializeObject(Id);

        //    if (v != null)
        //    {
        //        string result = v["result"].ToString();

        //        var v1 = (JObject)JsonConvert.DeserializeObject(result);

        //        string id = v1["msgId"].ToString();

        //        KihaMail kihaMail = new KihaMail();
        //        string data = kihaMail.MailInfo(id);

        //        return info;
        //    }

        //    return null;
        //}

        List<string> _FileName = new List<string>();

        public string fileYn { get => _fileYn; set => _fileYn = value; }

        public void FileAdd(string FileName)
        {
            string[] filenames;

            if (FileName.Contains(";") == true)
            {
                filenames = FileName.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                _FileName.AddRange(filenames);

                while (_FileName.Count > 5)
                {
                    _FileName.RemoveAt(0);
                }
            }
            else
            {
                if (_FileName.Count > 4)
                {
                    _FileName.RemoveAt(0);
                }

                _FileName.Add(FileName);

                if (_FileName.Count > 0)
                    _files0 = _FileName[0];

                if (_FileName.Count > 1)
                    _files1 = _FileName[1];

                if (_FileName.Count > 2)
                    _files2 = _FileName[2];

                if (_FileName.Count > 3)
                    _files3 = _FileName[3];

                if (_FileName.Count > 4)
                    _files4 = _FileName[4];
            }

            _fileYn = "Y";
        }

        byte[][] _bytes;
        HttpContent[] _fileByteContent;

        /// <summary>
        /// <strong>단체 메일 리스트</strong> <br/>
        /// 형태 : (이메일주소 이름)<br/>
        /// 이메일 주소와 이름 띄어쓰기 구분<br/>
        /// 수신자별, 구분<br/>
        /// 수신자 이메일주소, 이름 각각 50자<br/>
        /// </summary>        
        public string[] ReceiveAddresses
        {
            get => _ReceiveAddress;
            set
            {
                for (int i = 0; i < value.Length; i++)
                {
                    rcvInfo += value[i].Trim() + (i == value.Length - 1 ? "" : ",");
                }
            }
        }

        MailResult[] _MailResult;

        /// <summary>
        /// <strong>첨부파일 리스트</strong>
        /// </summary>
        public string[] Files
        {
            get
            {
                return _Files;
            }
            set
            {
                _MailResult = new MailResult[value.Length];
                _fileByteContent = new ByteArrayContent[value.Length];
                string filename = "";
                _bytes = new byte[value.Length][];
                for (int i = 0; i < value.Length; i++)
                {
                    if (File.Exists(value[i]) == false)
                        continue;

                    filename = value[i].Substring(value[i].LastIndexOf(@"\") + 1, value[i].Length - value[i].LastIndexOf(@"\") - 1);
                    var hackedFileName = new string(Encoding.UTF8.GetBytes(filename).Select(b => (char)b).ToArray());
                    _bytes[i] = File.ReadAllBytes(value[i]);
                    _fileByteContent[i] = new ByteArrayContent(_bytes[i], 0, _bytes[i].Length);

                    _fileByteContent[i].Headers.Add("Content-Disposition", $"form-data; name=\"files" + i.ToString() + $"\"; filename=\"{hackedFileName}\"");
                }
            }
        }
        public void FileAdd(string[] FileNams)
        {
            _FileName.AddRange(FileNams);

            while (_FileName.Count > 5)
            {
                _FileName.RemoveAt(0);
            }

            if (_FileName.Count > 0)
                _files0 = _FileName[0];

            if (_FileName.Count > 1)
                _files1 = _FileName[1];

            if (_FileName.Count > 2)
                _files2 = _FileName[2];

            if (_FileName.Count > 3)
                _files3 = _FileName[3];

            if (_FileName.Count > 4)
                _files4 = _FileName[4];

            _fileYn = "Y";
        }

        public void FileDelete(int Index = -1)
        {
            if (Index < 0)
                _FileName.Clear();
            else
                _FileName.RemoveAt(Index);

            files0 = "";
            files1 = "";
            files2 = "";
            files3 = "";
            files4 = "";

            if (_FileName.Count > 0)
                _files0 = _FileName[0];

            if (_FileName.Count > 1)
                _files1 = _FileName[1];

            if (_FileName.Count > 2)
                _files2 = _FileName[2];

            if (_FileName.Count > 3)
                _files3 = _FileName[3];

            if (_FileName.Count > 4)
                _files4 = _FileName[4];

            if (_FileName.Count == 0)
                _fileYn = "";
        }

        public string files0 { get => _files0; set => _files0 = value; }
        public string files1 { get => _files1; set => _files1 = value; }
        public string files2 { get => _files2; set => _files2 = value; }
        public string files3 { get => _files3; set => _files3 = value; }
        public string files4 { get => _files4; set => _files4 = value; }

        public string userId { get => _userId; set => _userId = value; }

        /// <summary>
        /// 메일 발송 제목
        /// </summary>
        public string title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
            }
        }

        /// <summary>
        /// 메일 발송 내욕
        /// </summary>
        public string content
        {
            get
            {
                return _content;
            }

            set
            {
                _content = value;
            }
        }

        /// <summary>
        /// 발송자 정보 : (이메일주소 이름) <br/>
        /// 이메일 주소와 이름 띄어쓰기로 구분 <br/>
        /// 발신자 이메일주소, 이름 각각 50자 <br/>
        /// 발신자 이메일 : 필수 <br/>
        /// </summary>
        public string sendInfo
        {
            get
            {
                return _sendInfo;
            }

            set
            {
                _sendInfo = value;
            }
        }

        /// <summary>
        /// 수신자 정보 : (이메일주소 이름) <br/>
        /// 이메일 주소와 이름 띄어쓰기 구분 <br/>
        ///수신자별, 구분 <br/>
        ///수신자 이메일주소, 이름 각각 50자 <br/>
        ///수신자 이메일 : 필수
        /// </summary>
        public string rcvInfo
        {
            get
            {
                return _rcvInfo;
            }

            set
            {
                _rcvInfo = value;
            }
        }

        /// <summary>
        /// 메일 전송 시간(YYYYMMDDHH24MISS) 기본값 : 현재시간
        /// </summary>
        public string sendDate
        {
            get
            {
                return _sendDate;
            }

            set
            {
                _sendDate = value;
            }
        }

        /// <summary>
        /// D : 즉시전송(기본값) R : 예약전송
        /// </summary>
        public string sendType
        {
            get
            {
                return _sendType;
            }

            set
            {
                _sendType = value;
            }
        }

        /// <summary>
        /// 메일의 분류 값 <para>※ 메일의 분류용도 <br/> 
        /// ※ 각 분류는 등록되어있어야 메일전송 가능
        /// </summary>
        public string categoryNm
        {
            get
            {
                return _categoryNm;
            }

            set
            {
                _categoryNm = value;
            }
        }

        /// <summary>
        /// 각 시스템별 고유 구분 값(연계시스템 코드)
        /// <para>
        /// ※ 메일 전송 요청시 시스템별 구분용도 
        /// </para>
        /// ※ 각 시스템은 등록되어있어야 메일전송 가능
        /// </summary>             

        public string linkNm
        {
            get
            {
                return _linkNm;
            }

            set
            {
                _linkNm = value;
            }
        }

        /// <summary>
        /// 사용 하고 싶은 값
        /// </summary>
        public string memo
        {
            get
            {
                return _memo;
            }

            set
            {
                _memo = value;
            }
        }

        public static MailInfo GetMailInfo(string MailId)
        {
            try
            {
                MailInfo info = new MailInfo(MailId);

                //입력할 데이터 형식
                data d = new data();
                d.msgId = MailId;

                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://send.kiha21.or.kr/api/mailDetail.do");
                ////var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://emstest.tigensoft.co.kr:81/api/sendMailFile.do");
                //httpWebRequest.ContentType = "text/json";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                ////httpWebRequest.Method = "PUT";//메일 전송은 성공 파일 전송은 실패

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(d);

                    json = "{\"data\" : " +
                        json + "}";

                    streamWriter.Write(json);
                }
                try
                {
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var responseText = streamReader.ReadToEnd();

                        var v1 = ((JObject)JsonConvert.DeserializeObject(responseText))["result"];

                        if (v1 != null)
                        {
                            if (v1["result"].ToString() == "")
                            {

                            }
                            info.Id = v1["msgId"] == null ? "" : v1["msgId"].ToString();
                            //info.CategoryName = v1["categoryNm"].ToString();
                            info.Content = v1["emsContent"] == null ? "" : v1["emsContent"].ToString();
                            //info.ReceiveAddresses = "";
                            info.SendDate = v1["sendDate"] == null ? "" : v1["sendDate"].ToString().Substring(0, 4) + "-" + v1["sendDate"].ToString().Substring(4, 2) + "-" + v1["sendDate"].ToString().Substring(6, 2) + " " +
                                v1["sendDate"].ToString().Substring(8, 2) + ":" + v1["sendDate"].ToString().Substring(10, 2) + ":" + v1["sendDate"].ToString().Substring(12, 2);// Convert.ToDateTime(v1["sendDate"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                            info.SenderName = v1["senderNm"] == null ? "" : v1["senderNm"].ToString();
                            info.SenderMail = v1["senderMail"] == null ? "" : v1["senderMail"].ToString();
                            info.Result = v1["result"] == null ? "" : v1["result"].ToString();
                            info.Title = v1["emsTitle"] == null ? "" : v1["emsTitle"].ToString();
                            //info.ReadYN = v1["readYn"] == null ? "" : v1["readYn"].ToString();
                            var v2 = v1["fileList"] == null ? "" : v1["fileList"].ToString();

                            if (v2 != null && v2 != "")
                            {
                                var v3 = ((JArray)JsonConvert.DeserializeObject(v1["fileList"].ToString())).ToList();

                                if (v3 != null)
                                {
                                    MailFile[] mf = new MailFile[v3.Count];

                                    for (int i = 0; i < v3.Count; i++)
                                    {
                                        mf[i] = new MailFile();
                                        //var v4 = ((JObject)JsonConvert.DeserializeObject(v3[i].ToString()))["filePath"];
                                        mf[i].Path = ((JObject)JsonConvert.DeserializeObject(v3[i].ToString()))["filePath"].ToString();
                                        mf[i].OriginName = ((JObject)JsonConvert.DeserializeObject(v3[i].ToString()))["originalFileNm"].ToString();
                                        mf[i].Name = ((JObject)JsonConvert.DeserializeObject(v3[i].ToString()))["fileNm"].ToString();
                                    }

                                    info.Files = mf;
                                }
                            }

                            var rcv = v1["rcvList"] == null ? "" : v1["rcvList"].ToString();

                            if (rcv != null && rcv != "")
                            {
                                var v3 = ((JArray)JsonConvert.DeserializeObject(v1["rcvList"].ToString())).ToList();

                                if (v3 != null)
                                {
                                    MailReceive[] mf = new MailReceive[v3.Count];

                                    for (int i = 0; i < v3.Count; i++)
                                    {
                                        mf[i] = new MailReceive();
                                        //var v4 = ((JObject)JsonConvert.DeserializeObject(v3[i].ToString()))["filePath"];
                                        mf[i].Mail = ((JObject)JsonConvert.DeserializeObject(v3[i].ToString()))["rcvMail"].ToString();
                                        mf[i].Name = ((JObject)JsonConvert.DeserializeObject(v3[i].ToString()))["rcvNm"].ToString();
                                        mf[i].ReadDate = ((JObject)JsonConvert.DeserializeObject(v3[i].ToString()))["readDate"].ToString();
                                        mf[i].ReadYN = ((JObject)JsonConvert.DeserializeObject(v3[i].ToString()))["readYn"].ToString();
                                        mf[i].Result = ((JObject)JsonConvert.DeserializeObject(v3[i].ToString()))["resultCd"].ToString();
                                    }

                                    info.ReceiveAddresses = mf;
                                }
                            }
                        }

                        #region 메일 ID 파싱
                        //var v = ((JObject)JsonConvert.DeserializeObject(stream));

                        //if (v != null)
                        //{
                        //    string JsonResult = v["result"].ToString();

                        //    var v1 = (JObject)JsonConvert.DeserializeObject(JsonResult);

                        //    //mailresult.Result = v1["result"].ToString();
                        //    //mailresult.MailId = v1["msgId"].ToString();
                        //}
                        #endregion
                        //Now you have your response.
                        //or false depending on information in the response
                        return info;
                    }
                }
                catch (Exception ex)
                {
                    //return "실패" + Environment.NewLine + ex.Message;
                    LogManager.WriteLog(ex.Message);
                    return null;
                }
            }
            catch
            {
                return new MailInfo("");
            }

        }
        string _FileUploadUrl = "https://send.kiha21.or.kr/api/sendMailFile.do";
        string _Url = "https://send.kiha21.or.kr/api/sendMail.do";
        //string _Url = "http://114.202.2.185:8888/api/sendMail.do";

        public MailResult SendMail(params string[] FileNames)
        {
            MailResult mailresult;

            if (FileNames.Length < 1)
                mailresult = SendMailWithNotFile();
            else
                mailresult = SendMailWithFiles(FileNames).Result;

            return mailresult;
        }
        public MailResult SendMailWithNotFile()
        {
            MailResult mr = new MailResult();

            if (_title == "")
            {
                mr.Result = "Title 입력 필요";
                return mr;
            }

            if (_content == "")
            {
                mr.Result = "내용 입력 필요";
                return mr;
            }

            if (_sendInfo == "")
            {
                mr.Result = "수신자 입력 필요";
                return mr;
            }

            if (_rcvInfo == "")
            {
                mr.Result = "발신자 입력 필요";
                return mr;
            }

            //http://114.202.2.185:8888/ 
            //http://emstest.tigensoft.co.kr:81 
            //http://114.202.2.185:8888/loginForm.do 
            //var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://114.202.2.185:8888/api/sendMail.do");
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(_Url);
            //var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://emstest.tigensoft.co.kr:81/api/sendMailFile.do");
            //http://EMS서버/api/sendMailFile.do
            httpWebRequest.ContentType = "application/json";
            //httpWebRequest.ContentType = "multipart/form-data";
            httpWebRequest.Method = "POST";
            //httpWebRequest.Method = "PUT";//메일 전송은 성공 파일 전송은 실패

            //httpWebRequest.Accept = "Accept=application/json";
            //httpWebRequest.SendChunked = false;
            //httpWebRequest.ContentLength = serializedObject.Length;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(this);

                //json = "{ \"" + Environment.NewLine +
                //    "title : "                    
                //    "}";
                json = "{\"data\" : " +
                    json + "}";

                //json = "{\"data\" : {"
                //    + "\"title\":\"" + _title + "\""
                //    + "\"content\":\"" + _content + "\""
                //    + "\"senderInfo\":\"" + _sendInfo + "\""
                //    + "\"rcvInfo\":\"" + _rcvInfo + "\""
                //    + "\"sendDate\":\"" + _sendDate + "\""
                //    + "\"sendType\":\"" + _sendType + "\""
                //    + "\"categoryNm\":\"" + _categoryNm + "\""
                //    + "\"linkNm\":\"" + _linkNm + "\""
                //    + "}}";

                //json = "{data : {"
                //    + "title:" + _title  
                //    + "content:" + _content
                //    + "senderInfo:" + _sendInfo
                //    + "rcvInfo:" + _rcvInfo 
                //    + "sendDate:" + _sendDate
                //    + "sendType:" + _sendType 
                //    + "categoryNm:" + _categoryNm
                //    + "linkNm:" + _linkNm 
                //    + "}}";
                streamWriter.Write(json);
            }
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();

                    //JsonConvert.DeserializeObject(responseText);

                    #region 메일 ID 파싱
                    var v = ((JObject)JsonConvert.DeserializeObject(responseText));

                    if (v != null)
                    {
                        string JsonResult = v["result"].ToString();

                        var v1 = (JObject)JsonConvert.DeserializeObject(JsonResult);

                        mr.Result = v1["result"].ToString();
                        mr.MailId = v1["msgId"].ToString();
                    }
                    #endregion

                    //Now you have your response.
                    //or false depending on information in the response
                    return mr;
                }
            }
            catch (Exception ex)
            {
                mr.Result = "실패\r\n" + ex.Message;

                return mr;
            }
        }

        public class data
        {
            public string msgId { get; set; }
        }

        

        public async Task<MailResult> SendMailWithFiles(string[] FileNames)
        {
            string result = "";
            string filename = "";
            MailResult mailresult = new MailResult();

            // Définition des variables qui seront envoyés
            HttpContent stringContent_title = new StringContent(_title);
            HttpContent stringContent_content = new StringContent(_content);
            HttpContent stringContent_sendInfo = new StringContent(_sendInfo);
            HttpContent stringContent_rcvInfo = new StringContent(_rcvInfo);
            HttpContent stringContent_sendDate = new StringContent(_sendDate);
            HttpContent stringContent_sendType = new StringContent("D");
            HttpContent stringContent_categoryNm = new StringContent(_categoryNm);
            HttpContent stringContent_linkNm = new StringContent(_linkNm);
            HttpContent stringContent_memo = new StringContent(_memo);
            HttpContent stringContent_userid = new StringContent(_userId);
            HttpContent stringContent_fileYn = new StringContent(FileNames.Length > 0 ? "Y" : "N");

            byte[] bytes0;
            HttpContent fileStreamContent0 = null;
            byte[] bytes1;
            HttpContent fileStreamContent1 = null;
            byte[] bytes2;
            HttpContent fileStreamContent2 = null;
            byte[] bytes3;
            HttpContent fileStreamContent3 = null;
            byte[] bytes4;
            HttpContent fileStreamContent4 = null;

            if (FileNames.Length > 0 && File.Exists(FileNames[0]))
            {
                bytes0 = File.ReadAllBytes(FileNames[0]);
                fileStreamContent0 = new ByteArrayContent(bytes0, 0, bytes0.Length);
            }

            if (FileNames.Length > 1 && File.Exists(FileNames[1]))
            {
                bytes1 = File.ReadAllBytes(FileNames[1]);
                fileStreamContent1 = new ByteArrayContent(bytes1, 0, bytes1.Length);
            }

            if (FileNames.Length > 2 && File.Exists(FileNames[2]))
            {
                bytes2 = File.ReadAllBytes(FileNames[2]);
                fileStreamContent2 = new ByteArrayContent(bytes2, 0, bytes2.Length);
            }

            if (FileNames.Length > 3 && File.Exists(FileNames[3]))
            {
                bytes3 = File.ReadAllBytes(FileNames[3]);
                fileStreamContent3 = new ByteArrayContent(bytes3, 0, bytes3.Length);
            }

            if (FileNames.Length > 4 && File.Exists(FileNames[4]))
            {
                bytes4 = File.ReadAllBytes(FileNames[4]);
                fileStreamContent4 = new ByteArrayContent(bytes4, 0, bytes4.Length);
            }

            //new ByteArrayContent(fileBytes);

            using (var client = new HttpClient())
            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(stringContent_title, "title"); // Le paramètre P1 aura la valeur contenue dans param1String
                formData.Add(stringContent_content, "content"); // Le parmaètre P2 aura la valeur contenue dans param2String
                formData.Add(stringContent_sendInfo, "sendInfo");
                formData.Add(stringContent_rcvInfo, "rcvInfo");
                formData.Add(stringContent_sendDate, "sendDate");//, DateTime.Now.ToString("yyyyMMddHHmmss")
                formData.Add(stringContent_sendType, "sendType");
                formData.Add(stringContent_categoryNm, "categoryNm");
                formData.Add(stringContent_linkNm, "linkNm");
                formData.Add(stringContent_userid, "userId");
                try
                {
                    formData.Add(stringContent_memo, "memo");
                }
                catch (Exception ex)
                {
                    string err11 = ex.Message;
                }
                try
                {
                    formData.Add(stringContent_fileYn, "fileYn");
                }
                catch (Exception ex)
                {
                    string err11 = ex.Message;
                }

                if (fileStreamContent0 != null)
                {
                    try
                    {
                        filename = FileNames[0].Substring(FileNames[0].LastIndexOf(@"\") + 1, FileNames[0].Length - FileNames[0].LastIndexOf(@"\") - 1);

                        var hackedFileName = new string(Encoding.UTF8.GetBytes(filename).Select(b => (char)b).ToArray());
                        fileStreamContent0.Headers.Add("Content-Disposition", $"form-data; name=\"files0\"; filename=\"{hackedFileName}\"");

                        formData.Add(fileStreamContent0, "files0");
                    }
                    catch (Exception ex)
                    {
                        mailresult.Result = "fileStreamContent0 오류\r\n" + ex.Message;

                        return mailresult;
                    }
                }

                if (fileStreamContent1 != null)
                {
                    try
                    {
                        filename = FileNames[1].Substring(FileNames[1].LastIndexOf(@"\") + 1, FileNames[1].Length - FileNames[1].LastIndexOf(@"\") - 1);

                        var hackedFileName = new string(Encoding.UTF8.GetBytes(filename).Select(b => (char)b).ToArray());
                        fileStreamContent1.Headers.Add("Content-Disposition", $"form-data; name=\"files1\"; filename=\"{hackedFileName}\"");


                        formData.Add(fileStreamContent1, "files1");
                    }
                    catch (Exception ex)
                    {
                        mailresult.Result = "fileStreamContent1 오류\r\n" + ex.Message;

                        return mailresult;
                    }
                }

                if (fileStreamContent2 != null)
                {
                    try
                    {
                        filename = FileNames[2].Substring(FileNames[2].LastIndexOf(@"\") + 1, FileNames[2].Length - FileNames[2].LastIndexOf(@"\") - 1);

                        var hackedFileName = new string(Encoding.UTF8.GetBytes(filename).Select(b => (char)b).ToArray());
                        fileStreamContent2.Headers.Add("Content-Disposition", $"form-data; name=\"files2\"; filename=\"{hackedFileName}\"");

                        formData.Add(fileStreamContent2, "files2");
                    }
                    catch (Exception ex)
                    {
                        mailresult.Result = "fileStreamContent2 오류\r\n" + ex.Message;

                        return mailresult;
                    }
                }

                if (fileStreamContent3 != null)
                {
                    try
                    {
                        filename = FileNames[3].Substring(FileNames[3].LastIndexOf(@"\") + 1, FileNames[3].Length - FileNames[3].LastIndexOf(@"\") - 1);

                        var hackedFileName = new string(Encoding.UTF8.GetBytes(filename).Select(b => (char)b).ToArray());
                        fileStreamContent3.Headers.Add("Content-Disposition", $"form-data; name=\"files3\"; filename=\"{hackedFileName}\"");

                        formData.Add(fileStreamContent3, "files3");
                    }
                    catch (Exception ex)
                    {
                        mailresult.Result = "fileStreamContent3 오류\r\n" + ex.Message;

                        return mailresult;
                    }
                }

                if (fileStreamContent4 != null)
                {
                    try
                    {
                        filename = FileNames[4].Substring(FileNames[4].LastIndexOf(@"\") + 1, FileNames[4].Length - FileNames[4].LastIndexOf(@"\") - 1);

                        var hackedFileName = new string(Encoding.UTF8.GetBytes(filename).Select(b => (char)b).ToArray());
                        fileStreamContent4.Headers.Add("Content-Disposition", $"form-data; name=\"files4\"; filename=\"{hackedFileName}\"");

                        formData.Add(fileStreamContent4, "files4");
                    }
                    catch (Exception ex)
                    {
                        mailresult.Result = "fileStreamContent4 오류\r\n" + ex.Message;

                        return mailresult;
                    }
                }

                //실행
                try
                {
                    var response = client.PostAsync(_FileUploadUrl, formData).Result;
                    ///client.
                    result = response.ToString();

                    if (!response.IsSuccessStatusCode)
                    {
                        result = "응답오류";
                        return null;
                    }

                    string stream = await response.Content.ReadAsStringAsync();

                    #region 메일 ID 파싱
                    var v = ((JObject)JsonConvert.DeserializeObject(stream));

                    if (v != null)
                    {
                        string JsonResult = v["result"].ToString();

                        var v1 = (JObject)JsonConvert.DeserializeObject(JsonResult);

                        mailresult.Result = v1["result"].ToString();
                        mailresult.MailId = v1["msgId"].ToString();
                    }
                    #endregion
                    return mailresult;

                    //return await response.Content.ReadAsStreamAsync();
                }
                catch (Exception Error)
                {
                    result = Error.Message;
                    return null;
                }
                //finally
                //{
                //    client.CancelPendingRequests();
                //}

                //return awa result;
            }
        }
    }
}
