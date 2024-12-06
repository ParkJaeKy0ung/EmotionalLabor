using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using DataTable = System.Data.DataTable;

namespace NBOGUN
{
    partial class Biz
    {
        public DataTable EmotionLaborSaupjaList(string Year, string SearchText)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_EMOTIONALLABOR_SaupjaList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[1] = new SqlParameter("Year", SqlDbType.Char);
                p[2] = new SqlParameter("SaupjaName", SqlDbType.VarChar);

                p[0].Value = DHCenter;
                p[1].Value = Year;
                p[2].Value = SearchText;

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
        public DataTable EmotionLaborSaupjaBuseoList(int SaupID, string Year)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_EMOTIONALLABOR_SaupjaBuseoList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[2] = new SqlParameter("Year", SqlDbType.VarChar);

                p[0].Value = _DHCenter;
                p[1].Value = SaupID;
                p[2].Value = Year;

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
        public DataTable EmotionLaborSaupjaJosaDateList(int SaupID)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_EMOTIONALLABOR_SaupjaJosaDateList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[1] = new SqlParameter("SaupID", SqlDbType.Char);

                p[0].Value = _DHCenter;
                p[1].Value = SaupID;

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
        //UP_EMOTIONALLABOR_PersonList
        public DataTable EmotionLaborPersonList(int SaupID, string Year)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_EMOTIONALLABOR_PersonList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[2] = new SqlParameter("Year", SqlDbType.Char);

                p[0].Value = _DHCenter;
                p[1].Value = SaupID;
                p[2].Value = Year;

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

        public DataTable EmotionLaborPersonDetail(int SaupID, string Year, int SeqNO)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_EMOTIONALLABOR_PersonDetailList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[4];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[2] = new SqlParameter("Year", SqlDbType.Char);
                p[3] = new SqlParameter("SeqNO", SqlDbType.Int);

                p[0].Value = _DHCenter;
                p[1].Value = SaupID;
                p[2].Value = Year;
                p[3].Value = SeqNO;

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

        public DataTable EMOTIONALLABOR_SaupjaGUID(int SaupID, string Year, string Saupjanum = "")
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_EMOTIONALLABOR_SaupjaGUID";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[4];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[2] = new SqlParameter("Year", SqlDbType.VarChar);
                p[3] = new SqlParameter("Saupjanum", SqlDbType.VarChar);

                p[0].Value = _DHCenter;
                p[1].Value = SaupID;
                p[2].Value = Year;
                p[3].Value = Saupjanum;

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

        //UP_EMOTIONALLABOR_SaupjaSMSDel
        public int EMOTIONALLABOR_SaupjaSMSDel(int SaupID, int SeqNO)
        {
            int r;

            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_EMOTIONALLABOR_SaupjaSMSDel";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[2] = new SqlParameter("SeqNO", SqlDbType.Int);

                p[0].Value = _DHCenter;
                p[1].Value = SaupID;
                p[2].Value = SeqNO;

                com.Parameters.AddRange(p);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);

                try
                {
                    r = com.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    r = -1;
                    LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
                }

                return r;
            }
        }

        //UP_EMOTIONALLABOR_SurveySave
        public int EMOTIONALLABOR_SurveySave(SqlConnection con, SqlTransaction tran, string GUID, string Phone, string Name, string Sex, string Buseo
            , string A01, string A02, string B03, string B04, string B05, string C06, string C07, string D08, string D09, string D10, string D11)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_EMOTIONALLABOR_SurveySave";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[16];
            p[0] = new SqlParameter("GUID", SqlDbType.VarChar);
            p[1] = new SqlParameter("Phone", SqlDbType.VarChar);
            p[2] = new SqlParameter("Name", SqlDbType.VarChar);
            p[3] = new SqlParameter("Sex", SqlDbType.VarChar);
            p[4] = new SqlParameter("Buseo", SqlDbType.VarChar);
            p[5] = new SqlParameter("A01", SqlDbType.VarChar);
            p[6] = new SqlParameter("A02", SqlDbType.VarChar);
            p[7] = new SqlParameter("B03", SqlDbType.VarChar);
            p[8] = new SqlParameter("B04", SqlDbType.VarChar);
            p[9] = new SqlParameter("B05", SqlDbType.VarChar);
            p[10] = new SqlParameter("C06", SqlDbType.VarChar);
            p[11] = new SqlParameter("C07", SqlDbType.VarChar);
            p[12] = new SqlParameter("D08", SqlDbType.VarChar);
            p[13] = new SqlParameter("D09", SqlDbType.VarChar);
            p[14] = new SqlParameter("D10", SqlDbType.VarChar);
            p[15] = new SqlParameter("D11", SqlDbType.VarChar);

            p[0].Value = GUID;
            p[1].Value = Phone;
            p[2].Value = Name;
            p[3].Value = Sex;
            p[4].Value = Buseo;
            p[5].Value = A01;
            p[6].Value = A02;
            p[7].Value = B03;
            p[8].Value = B04;
            p[9].Value = B05;
            p[10].Value = C06;
            p[11].Value = C07;
            p[12].Value = D08;
            p[13].Value = D09;
            p[14].Value = D10;
            p[15].Value = D11;

            com.Parameters.AddRange(p);

            //서버로그 남기기기
            WriteServerLog(com.CommandText, p);

            try
            {
                r = com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                r = -1;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }


        /// <summary>
        /// 문자 발송 내역 저장
        /// </summary>
        /// <param name="con"></param>
        /// <param name="tran"></param>
        /// <param name="Phone"></param>
        /// <param name="SaupID"></param>
        /// <param name="SaupjaNum"></param>
        /// <param name="SaupjaName"></param>
        /// <param name="Name"></param>
        /// <param name="Buseo"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public int EMOTIONALLABOR_SMSHistorySave(SqlConnection con, SqlTransaction tran, string Phone, int SaupID, string SaupjaNum, string SaupjaName, string Name, string Buseo, string Content, string JosaDate)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_EMOTIONALLABOR_SMSPersonHistorySave";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[9];
            p[0] = new SqlParameter("Phone", SqlDbType.VarChar);
            p[1] = new SqlParameter("Center", SqlDbType.Char);
            p[2] = new SqlParameter("SaupID", SqlDbType.Int);
            p[3] = new SqlParameter("SaupjaNum", SqlDbType.Char);
            p[4] = new SqlParameter("SaupjaName", SqlDbType.NVarChar);
            p[5] = new SqlParameter("Name", SqlDbType.NVarChar);
            p[6] = new SqlParameter("Buseo", SqlDbType.NVarChar);
            p[7] = new SqlParameter("Content", SqlDbType.NVarChar);
            p[8] = new SqlParameter("JosaDate", SqlDbType.NVarChar);

            p[0].Value = Phone;
            p[1].Value = DHCenter;
            p[2].Value = SaupID;
            p[3].Value = SaupjaNum;
            p[4].Value = SaupjaName;
            p[5].Value = Name;
            p[6].Value = Buseo;
            p[7].Value = Content;
            p[8].Value = JosaDate;

            com.Parameters.AddRange(p);

            //서버로그 남기기기
            WriteServerLog(com.CommandText, p);

            try
            {
                r = com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                r = -1;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        //UP_EMOTIONALLABOR_SMSPersonJosaDateChange
        public int EMOTIONALLABOR_SMSPersonJosaDateChange(SqlConnection con, SqlTransaction tran, string Saupjanum, string Phone, string JosaDate, string TargetJosaDate)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_EMOTIONALLABOR_SMSPersonJosaDateChange";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[1] = new SqlParameter("Saupjanum", SqlDbType.Char);
            p[2] = new SqlParameter("Phone", SqlDbType.VarChar);
            p[3] = new SqlParameter("JosaDate", SqlDbType.VarChar);
            p[4] = new SqlParameter("TargetJosaDate", SqlDbType.VarChar);

            p[0].Value = DHCenter;
            p[1].Value = Saupjanum;
            p[2].Value = Phone;
            p[3].Value = JosaDate;
            p[4].Value = TargetJosaDate;

            com.Parameters.AddRange(p);

            //서버로그 남기기기
            WriteServerLog(com.CommandText, p);

            try
            {
                r = com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                r = -1;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        /// <summary>
        /// 문자 발송대상 근로자 저장하기
        /// </summary>
        /// <param name="con"></param>
        /// <param name="tran"></param>
        /// <param name="Phone"></param>
        /// <param name="Name"></param>
        /// <param name="Buseo"></param>
        /// <param name="SaupjaNum"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        public int EMOTIONALLABOR_SMSPersonSave(SqlConnection con, SqlTransaction tran, string Saupjanum, string Name, string Phone, string Buseo, string JosaDate)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_EMOTIONALLABOR_SMSPersonSave";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[6];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[1] = new SqlParameter("Saupjanum", SqlDbType.Char);
            p[2] = new SqlParameter("Name", SqlDbType.VarChar);
            p[3] = new SqlParameter("Phone", SqlDbType.VarChar);
            p[4] = new SqlParameter("Buseo", SqlDbType.VarChar);
            p[5] = new SqlParameter("JosaDate", SqlDbType.VarChar);

            p[0].Value = DHCenter;
            p[1].Value = Saupjanum;
            p[2].Value = Name;
            p[3].Value = Phone;
            p[4].Value = Buseo;
            p[5].Value = JosaDate;

            com.Parameters.AddRange(p);

            //서버로그 남기기기
            WriteServerLog(com.CommandText, p);

            try
            {
                r = com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                r = -1;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public int EMOTIONALLABOR_SMSPersonDel(SqlConnection con, SqlTransaction tran, string Saupjanum, string Phone, string JosaDate)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_EMOTIONALLABOR_SMSPersonDel";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[1] = new SqlParameter("Saupjanum", SqlDbType.Char);
            p[2] = new SqlParameter("Phone", SqlDbType.VarChar);
            p[3] = new SqlParameter("JosaDate", SqlDbType.VarChar);

            p[0].Value = DHCenter;
            p[1].Value = Saupjanum;
            p[2].Value = Phone;
            p[3].Value = JosaDate;

            com.Parameters.AddRange(p);

            //서버로그 남기기기
            WriteServerLog(com.CommandText, p);

            try
            {
                r = com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                r = -1;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public int EMOTIONALLABORGoogleSuveySave(SqlConnection con, SqlTransaction tran, string Phone, string Name, string Buseo, string SaupjaNum, string Date)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_GOOGLE_SurveyPersonAdd";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[6];
            p[0] = new SqlParameter("Phone", SqlDbType.VarChar);
            p[1] = new SqlParameter("Name", SqlDbType.VarChar);
            p[2] = new SqlParameter("Buseo", SqlDbType.VarChar);
            p[3] = new SqlParameter("Center", SqlDbType.Char);
            p[4] = new SqlParameter("SaupjaNum", SqlDbType.Char);
            p[5] = new SqlParameter("Date", SqlDbType.VarChar);

            p[0].Value = Phone;
            p[1].Value = Name;
            p[2].Value = Buseo;
            p[3].Value = _DHCenter;
            p[4].Value = SaupjaNum;
            p[5].Value = Date;

            com.Parameters.AddRange(p);

            //서버로그 남기기기
            WriteServerLog(com.CommandText, p);

            try
            {
                r = com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                r = -1;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public int EmotionLaborSaupjaSMSSave(int SaupID, string SMS)
        {
            int r;

            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_EMOTIONALLABOR_SaupjaSMSSave";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[4];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[2] = new SqlParameter("SMS", SqlDbType.VarChar);
                p[3] = new SqlParameter("Sabun", SqlDbType.Char);

                p[0].Value = _DHCenter;
                p[1].Value = SaupID;
                p[2].Value = SMS;
                p[3].Value = _UserID;

                com.Parameters.AddRange(p);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);

                try
                {
                    r = com.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    r = -1;
                    LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
                }

                return r;
            }
        }


        //UP_EMOTIONALLABOR_PersonNotAnswerList
        public DataTable EMOTIONALLABOR_PersonNotAnswerList(string SaupjaNum, string Year)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_EMOTIONALLABOR_PersonNotAnswerList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[3];
                    p[0] = new SqlParameter("Center", SqlDbType.VarChar);
                    p[0].Value = DHCenter;
                    p[1] = new SqlParameter("SaupjaNum", SqlDbType.VarChar);
                    p[1].Value = SaupjaNum;
                    p[2] = new SqlParameter("Year", SqlDbType.VarChar);
                    p[2].Value = Year;


                    com.Parameters.AddRange(p);

                    //서버로그 남기기기
                    WriteServerLog(com.CommandText, p);

                    try
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(com);
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        _Error = ex.Message;
                        LogManager.WriteLog(ex.Message);
                    }

                    return dt;
                }
            }
        }

        public DataTable EMOTIONALLABOR_SendEMailHistoryList(int SaupID)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_EMOTIONALLABOR_SendEMailHistoryList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[2];
                    p[0] = new SqlParameter("Center", SqlDbType.VarChar);
                    p[0].Value = DHCenter;
                    p[1] = new SqlParameter("SaupID", SqlDbType.VarChar);
                    p[1].Value = SaupID;

                    com.Parameters.AddRange(p);

                    //서버로그 남기기기
                    WriteServerLog(com.CommandText, p);

                    try
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(com);
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        _Error = ex.Message;
                        LogManager.WriteLog(ex.Message);
                    }

                    return dt;
                }
            }
        }

        public int EMOTIONALLABOR_SendEMailHistorySave(SqlConnection con, SqlTransaction tran, int SaupID, string JosaDate, string Name, string ReceiveName, string ReceiveEMail,
            string Content, string EMailID)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_EMOTIONALLABOR_SendEMailHistorySave";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[9];

            p[0] = new SqlParameter("Sabun", SqlDbType.Char);
            p[1] = new SqlParameter("Center", SqlDbType.Char);
            p[2] = new SqlParameter("SaupID", SqlDbType.Int);
            p[3] = new SqlParameter("JosaDate", SqlDbType.Char);
            p[4] = new SqlParameter("Name", SqlDbType.VarChar);
            p[5] = new SqlParameter("ReceiveName", SqlDbType.VarChar);
            p[6] = new SqlParameter("ReceiveEMail", SqlDbType.VarChar);
            p[7] = new SqlParameter("Content", SqlDbType.VarChar);
            p[8] = new SqlParameter("EMailID", SqlDbType.VarChar);

            p[0].Value = UserID;
            p[1].Value = DHCenter;
            p[2].Value = SaupID;
            p[3].Value = JosaDate;
            p[4].Value = Name;
            p[5].Value = ReceiveName;
            p[6].Value = ReceiveEMail;
            p[7].Value = Content;
            p[8].Value = EMailID;

            com.Parameters.AddRange(p);

            //서버로그 남기기기
            WriteServerLog(com.CommandText, p);

            try
            {
                r = com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                r = -1;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }


        public int EMOTIONALLABOR_SendEMailHistoryUpdate(SqlConnection con, SqlTransaction tran, string EMailID, string Result)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_EMOTIONALLABOR_SendEMailHistoryUpdate";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[2];

            p[0] = new SqlParameter("EMailID", SqlDbType.VarChar);
            p[1] = new SqlParameter("Result", SqlDbType.VarChar);

            p[0].Value = EMailID;
            p[1].Value = Result;

            com.Parameters.AddRange(p);

            //서버로그 남기기기
            WriteServerLog(com.CommandText, p);

            try
            {
                r = com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                r = -1;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public DataTable EMOTIONALLABOR_Excel(int SaupID, string JosaDate, string Gubun)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_EMOTIONALLABOR_Excel";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[4];
                    p[0] = new SqlParameter("Center", SqlDbType.VarChar);
                    p[0].Value = DHCenter;
                    p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                    p[1].Value = SaupID;
                    p[2] = new SqlParameter("JosaDate", SqlDbType.VarChar);
                    p[2].Value = JosaDate;
                    p[3] = new SqlParameter("Gubun", SqlDbType.VarChar);
                    p[3].Value = Gubun;

                    com.Parameters.AddRange(p);

                    //서버로그 남기기기
                    WriteServerLog(com.CommandText, p);

                    try
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(com);
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        _Error = ex.Message;
                        LogManager.WriteLog(ex.Message);
                    }

                    return dt;
                }
            }
        }

        public DataTable EMOTIONALLABOR_SMSPersonHistoryList(string Phone)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_EMOTIONALLABOR_SMSPersonHistoryList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[1];
                    p[0] = new SqlParameter("Phone", SqlDbType.VarChar);
                    p[0].Value = Phone;

                    com.Parameters.AddRange(p);

                    //서버로그 남기기기
                    WriteServerLog(com.CommandText, p);

                    try
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(com);
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        _Error = ex.Message;
                        LogManager.WriteLog(ex.Message);
                    }

                    return dt;
                }
            }
        }

        public DataTable EMOTIONALLABOR_GoogleSurveyHistoryList(string SDate, string EDate, string SearchText)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_EMOTIONALLABOR_GoogleSurveyHistory";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[3];
                    p[0] = new SqlParameter("SDate", SqlDbType.VarChar);
                    p[0].Value = SDate;
                    p[1] = new SqlParameter("EDate", SqlDbType.VarChar);
                    p[1].Value = EDate;
                    p[2] = new SqlParameter("SearchText", SqlDbType.VarChar);
                    p[2].Value = SearchText;

                    com.Parameters.AddRange(p);

                    //서버로그 남기기기
                    WriteServerLog(com.CommandText, p);

                    try
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(com);
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        _Error = ex.Message;
                        LogManager.WriteLog(ex.Message);
                    }

                    return dt;
                }
            }
        }

        /// <summary>
        /// 사업장의 생성 조사일 리스트
        /// </summary>
        /// <param name="SaupjaNum"></param>
        /// <returns></returns>
        public DataTable EMOTIONALLABOR_QRJosaDateList(string SaupjaNum)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_EMOTIONALLABOR_QRJosaDateList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[2];
                    p[0] = new SqlParameter("Center", SqlDbType.Char);
                    p[0].Value = DHCenter;
                    p[1] = new SqlParameter("SaupjaNum", SqlDbType.Char);
                    p[1].Value = SaupjaNum;

                    com.Parameters.AddRange(p);

                    //서버로그 남기기기
                    WriteServerLog(com.CommandText, p);

                    try
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(com);
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        _Error = ex.Message;
                        LogManager.WriteLog(ex.Message);
                    }

                    return dt;
                }
            }
        }

        public DataTable EMOTIONALLABOR_SMSPersonList(string Saupjanum, string Year)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_EMOTIONALLABOR_SMSPersonList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[3];
                    p[0] = new SqlParameter("Center", SqlDbType.Char);
                    p[0].Value = DHCenter;
                    p[1] = new SqlParameter("Saupjanum", SqlDbType.Char);
                    p[1].Value = Saupjanum;
                    p[2] = new SqlParameter("Year", SqlDbType.VarChar);
                    p[2].Value = Year;

                    com.Parameters.AddRange(p);

                    //서버로그 남기기기
                    WriteServerLog(com.CommandText, p);

                    try
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(com);
                        adapter.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        _Error = ex.Message;
                        LogManager.WriteLog(ex.Message);
                    }

                    return dt;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SaupID"></param>
        /// <param name="Gubun">'1':현재 사용하는 한개, '' 공란일땐 전체</param>
        /// <returns></returns>
        public DataTable EmotionLaborSaupjaSMSList(int SaupID, string Gubun = "")
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_EMOTIONALLABOR_SaupjaSMSList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[2] = new SqlParameter("Gubun", SqlDbType.VarChar);

                p[0].Value = _DHCenter;
                p[1].Value = SaupID;
                p[2].Value = Gubun;

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

        public int EmotionLaborSaupjaBuseoDel(SqlConnection con, SqlTransaction tran, int SaupID, string Year)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_EMOTIONALLABOR_SaupjaBuseoDel";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[2] = new SqlParameter("Year", SqlDbType.Char);

            p[0].Value = _DHCenter;
            p[1].Value = SaupID;
            p[2].Value = Year;

            com.Parameters.AddRange(p);

            //서버로그 남기기기
            WriteServerLog(com.CommandText, p);

            try
            {
                r = com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                r = -1;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }
        //UP_JIKMUSTRESS_PyeonggaReportCreateTable



        public int EmotionLaborSaupjaBuseoSave(SqlConnection con, SqlTransaction tran, int SaupID, string Year, int SeqNO, string BuseoName, string Gubun, string Client, string Hire, string Inwon, string Upmu)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_EMOTIONALLABOR_SaupjaBuseoSave";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[10];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[2] = new SqlParameter("Year", SqlDbType.Char);
            p[3] = new SqlParameter("SeqNO", SqlDbType.Int);
            p[4] = new SqlParameter("BuseoName", SqlDbType.VarChar);
            p[5] = new SqlParameter("Gubun", SqlDbType.VarChar);
            p[6] = new SqlParameter("Client", SqlDbType.VarChar);
            p[7] = new SqlParameter("Hire", SqlDbType.VarChar);
            p[8] = new SqlParameter("Inwon", SqlDbType.VarChar);
            p[9] = new SqlParameter("Upmu", SqlDbType.VarChar);

            p[0].Value = _DHCenter;
            p[1].Value = SaupID;
            p[2].Value = Year;
            p[3].Value = SeqNO;
            p[4].Value = BuseoName;
            p[5].Value = Gubun;
            p[6].Value = Client;
            p[7].Value = Hire;
            p[8].Value = Inwon;
            p[9].Value = Upmu;

            com.Parameters.AddRange(p);

            //서버로그 남기기기
            WriteServerLog(com.CommandText, p);

            try
            {
                r = com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                r = -1;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        #region 출력하기전에 하는 기본 세팅
        /// <summary>
        /// 출력하기전에 하는 기본세팅 - 초기화
        /// </summary>
        /// <param name="con"></param>
        /// <param name="tran"></param>
        /// <param name="SaupID"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public int EmotionLabor_RPT_Del(SqlConnection con, SqlTransaction tran, int SaupID, string Year)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_EMOTIONALLABOR_RPT_Del";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[2] = new SqlParameter("Year", SqlDbType.Char);

            p[0].Value = _DHCenter;
            p[1].Value = SaupID;
            p[2].Value = Year;

            com.Parameters.AddRange(p);

            //서버로그 남기기기
            WriteServerLog(com.CommandText, p);

            try
            {
                r = com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                r = -1;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public int EmotionLabor_RPT_00Save(SqlConnection con, SqlTransaction tran, int SaupID, string Year, string SaupjaNum, string JosaDate, string Josaja)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_EMOTIONALLABOR_RPT_00Save";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[6];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[2] = new SqlParameter("Year", SqlDbType.Char);
            p[3] = new SqlParameter("SaupjaNum", SqlDbType.Char);
            p[4] = new SqlParameter("JosaDate", SqlDbType.VarChar);
            p[5] = new SqlParameter("Josaja", SqlDbType.VarChar);

            p[0].Value = _DHCenter;
            p[1].Value = SaupID;
            p[2].Value = Year;
            p[3].Value = SaupjaNum;
            p[4].Value = JosaDate;
            p[5].Value = Josaja;

            com.Parameters.AddRange(p);

            //서버로그 남기기기
            WriteServerLog(com.CommandText, p);

            try
            {
                r = com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                r = -1;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }
        #endregion


        public int EmotionLaborPersonDel(SqlConnection con, SqlTransaction tran, int SaupID, string Year, int SeqNO)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_EMOTIONALLABOR_PersonDel";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[2] = new SqlParameter("Year", SqlDbType.Char);
            p[3] = new SqlParameter("SeqNO", SqlDbType.Int);

            p[0].Value = _DHCenter;
            p[1].Value = SaupID;
            p[2].Value = Year;
            p[3].Value = SeqNO;

            com.Parameters.AddRange(p);

            //서버로그 남기기기
            WriteServerLog(com.CommandText, p);

            try
            {
                r = com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                r = -1;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public DataTable EmotionLaborPersonSave(SqlConnection con, SqlTransaction tran, int SaupID, string SaupjaNum, string Year, int SeqNO, string Name, string Sex, int Buseo)
        {
            DataTable dt = new DataTable();

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_EMOTIONALLABOR_PersonSave";
            com.CommandType = CommandType.StoredProcedure;
           
            SqlParameter[] p = new SqlParameter[8];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[2] = new SqlParameter("SaupjaNum", SqlDbType.Char);
            p[3] = new SqlParameter("Year", SqlDbType.Char);
            p[4] = new SqlParameter("SeqNO", SqlDbType.Int);
            p[5] = new SqlParameter("Name", SqlDbType.VarChar);
            p[6] = new SqlParameter("Sex", SqlDbType.Char);
            p[7] = new SqlParameter("Buseo", SqlDbType.Int);

            p[0].Value = _DHCenter;
            p[1].Value = SaupID;
            p[2].Value = SaupjaNum;
            p[3].Value = Year;
            p[4].Value = SeqNO;
            p[5].Value = Name;
            p[6].Value = Sex;
            p[7].Value = Buseo;

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

        public int EmotionLaborPersonDetailSave(SqlConnection con, SqlTransaction tran, int SaupID, string Year, int SeqNO, string Code, string Position)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_EMOTIONALLABOR_PersonDetailSave";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[6];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[2] = new SqlParameter("Year", SqlDbType.Char);
            p[3] = new SqlParameter("SeqNO", SqlDbType.Int);
            p[4] = new SqlParameter("Code", SqlDbType.Char);
            p[5] = new SqlParameter("Position", SqlDbType.Char);

            p[0].Value = _DHCenter;
            p[1].Value = SaupID;
            p[2].Value = Year;
            p[3].Value = SeqNO;
            p[4].Value = Code;
            p[5].Value = Position;

            com.Parameters.AddRange(p);

            //서버로그 남기기기
            WriteServerLog(com.CommandText, p);

            try
            {
                r = com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                r = -1;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public int EmotionLaborPersonBuseoChange(SqlConnection con, SqlTransaction tran, int SaupID, string Year, int SeqNO, int Buseo)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_EMOTIONALLABOR_PersonBuseoChange";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[2] = new SqlParameter("Year", SqlDbType.Char);
            p[3] = new SqlParameter("SeqNO", SqlDbType.Int);
            p[4] = new SqlParameter("Buseo", SqlDbType.Int);

            p[0].Value = _DHCenter;
            p[1].Value = SaupID;
            p[2].Value = Year;
            p[3].Value = SeqNO;
            p[4].Value = Buseo;

            com.Parameters.AddRange(p);

            //서버로그 남기기기
            WriteServerLog(com.CommandText, p);

            try
            {
                r = com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                r = -1;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public int EMOTIONALLABOR_RPT_Del(int SaupID, string Year)
        {
            int r;

            //DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                //dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_EMOTIONALLABOR_RPT_Del";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[2] = new SqlParameter("Year", SqlDbType.Char);

                p[0].Value = _DHCenter;
                p[1].Value = SaupID;
                p[2].Value = Year;

                com.Parameters.AddRange(p);

                try
                {
                    r = com.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    r = -1;
                    LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
                }

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);


                return r;
            }
        }

        public int EMOTIONALLABOR_RPT_00Save(int SaupID, string Year, string SaupjaNum, string JosaDate, string Josaja)
        {
            int r;

            //DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                //dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_EMOTIONALLABOR_RPT_00Save";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[6];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[2] = new SqlParameter("Year", SqlDbType.Char);
                p[3] = new SqlParameter("SaupjaNum", SqlDbType.Char);
                p[4] = new SqlParameter("JosaDate", SqlDbType.VarChar);
                p[5] = new SqlParameter("Josaja", SqlDbType.VarChar);

                p[0].Value = _DHCenter;
                p[1].Value = SaupID;
                p[2].Value = Year;
                p[3].Value = SaupjaNum;
                p[4].Value = JosaDate;
                p[5].Value = Josaja;

                com.Parameters.AddRange(p);

                try
                {
                    r = com.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    r = -1;
                    LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
                }

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);


                return r;
            }
        }

        public int EMOTIONALLABOR_RPT_01Save(int SaupID, string Year)
        {
            int r;

            //DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                //dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_EMOTIONALLABOR_RPT_01Save";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[2] = new SqlParameter("Year", SqlDbType.Char);

                p[0].Value = _DHCenter;
                p[1].Value = SaupID;
                p[2].Value = Year;

                com.Parameters.AddRange(p);

                try
                {
                    r = com.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    r = -1;
                    LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
                }

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);


                return r;
            }
        }

        public int EMOTIONALLABOR_RPT_02Save(int SaupID, string Year)
        {
            int r;

            //DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                //dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_EMOTIONALLABOR_RPT_02Save";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[2] = new SqlParameter("Year", SqlDbType.Char);

                p[0].Value = _DHCenter;
                p[1].Value = SaupID;
                p[2].Value = Year;

                com.Parameters.AddRange(p);

                try
                {
                    r = com.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    r = -1;
                    LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
                }

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);


                return r;
            }
        }

        public int EMOTIONALLABOR_RPT_03Save(int SaupID, string Year)
        {
            int r;

            //DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                //dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_EMOTIONALLABOR_RPT_03Save";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[2] = new SqlParameter("Year", SqlDbType.Char);

                p[0].Value = _DHCenter;
                p[1].Value = SaupID;
                p[2].Value = Year;

                com.Parameters.AddRange(p);

                try
                {
                    r = com.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    r = -1;
                    LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
                }

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);


                return r;
            }
        }
    }
}
