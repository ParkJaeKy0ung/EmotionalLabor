using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Xml;
using Telerik.WinControls.UI;

namespace NBOGUN
{
    partial class Biz
    {
        public int BONBU_KikawnPyeonggaCode2Del(SqlConnection con, SqlTransaction tran, int Idx)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_BONBU_KikawnPyeonggaCode2Del";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("Idx", SqlDbType.Int);

            p[0].Value = Idx;

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
                Error = ex.Message;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public int BONBU_KikawnPyeonggaCode2Save(SqlConnection con, SqlTransaction tran, int Idx, string Code)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_BONBU_KikawnPyeonggaCode2Save";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("Idx", SqlDbType.Int);
            p[1] = new SqlParameter("Code", SqlDbType.VarChar);

            p[0].Value = Idx;
            p[1].Value = Code;

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
                Error = ex.Message;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public DataTable BONBU_KikawnPyeonggaCode2List(int Idx)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_BONBU_KikawnPyeonggaCode2List";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[1];
                    p[0] = new SqlParameter("Idx", SqlDbType.VarChar);
                    p[0].Value = Idx;

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
                        Error = ex.Message;
                        LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
                    }
                    return dt;
                }

            }
        }


        public int BONBU_KikawnPyeonggaCode1Del(SqlConnection con, SqlTransaction tran, int Idx)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_BONBU_KikawnPyeonggaCode1Del";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("Idx", SqlDbType.Int);

            p[0].Value = Idx;

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
                Error = ex.Message;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public int BONBU_KikawnPyeonggaCode1Save(SqlConnection con, SqlTransaction tran, int Idx, string Name, int Odx = 0)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_BONBU_KikawnPyeonggaCode1Save";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("Idx", SqlDbType.Int);
            p[1] = new SqlParameter("Name", SqlDbType.VarChar);
            p[2] = new SqlParameter("Odx", SqlDbType.Int);

            p[0].Value = Idx;
            p[1].Value = Name;
            p[2].Value = Odx;

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
                Error = ex.Message;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public DataSet BONBU_KikawnPyeonggaCode1List(string Gubun = "1")
        {
            DataSet dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataSet();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_BONBU_KikawnPyeonggaCode1List";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[1];
                    p[0] = new SqlParameter("Gubun", SqlDbType.VarChar);
                    p[0].Value = Gubun;

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
                        Error = ex.Message;
                        LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
                    }
                    return dt;
                }
            }
        }

        public DataTable BONBU_EduItemList(string Year)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_BONBU_EduItemList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[1];
                    p[0] = new SqlParameter("Year", SqlDbType.Char);
                    p[0].Value = Year;

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

        public int BONBU_CODE_JidoKeywordSave(SqlConnection con, SqlTransaction tran, string Code, string Keyword)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_CODE_JidoKeywordSave";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("Code", SqlDbType.Char);
            p[1] = new SqlParameter("Keyword", SqlDbType.VarChar);

            p[0].Value = Code;
            p[1].Value = Keyword;

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
                Error = ex.Message;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public DataTable BONBU_CODE_JidoKeywordList()
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_CODE_JidoKeywordList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[0];

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

        public DataTable BONBU_300SaupjaList(string Yearmon)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_BONBU_300SaupjaList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[1];
                    p[0] = new SqlParameter("Yearmon", SqlDbType.Char);
                    p[0].Value = Yearmon;

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
        /// 지정 승인
        /// </summary>
        /// <param name="con"></param>
        /// <param name="tran"></param>
        /// <param name="pCenter"></param>
        /// <param name="pYearmon"></param>
        /// <param name="pSincheongDate"></param>
        /// <returns></returns>
        public int BONBU_JijungAccept(SqlConnection con, SqlTransaction tran, string pCenter, string pYearmon, string pSincheongDate)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_JijungAccept";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[1] = new SqlParameter("Yearmon", SqlDbType.Char);
            p[2] = new SqlParameter("SincheongDate", SqlDbType.Char);
            p[3] = new SqlParameter("Bonbu", SqlDbType.Char);

            p[0].Value = pCenter;
            p[1].Value = pYearmon;
            p[2].Value = pSincheongDate;
            p[3].Value = UserID;

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
                _Error = ex.Message;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }


        public int BONBU_CodeHoodDel(SqlConnection con, SqlTransaction tran, string Code)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_BONBU_CodeHoodDel";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@Code", SqlDbType.Char);

            p[0].Value = Code;

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
                _Error = ex.Message;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public DataTable BONBU_CodeSealZone()
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_SEALZONE_CodeList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[0];

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

        public DataTable BONBU_OverVisitList(string Year)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_BONBU_OverVisitList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[1];
                    p[0] = new SqlParameter("Year", SqlDbType.Char);
                    p[0].Value = Year;

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

        public DataTable BONBU_CodeSealZoneSave(SqlConnection con, SqlTransaction tran, int Idx, string GrpNO, string Name1, string Name2, string Name3, string Question, string Content,
            string AnswerY, string GoY, string AnswerN, string GoN, string AnswerU, string GoU, string ShownedLabel, string IsCritical, string PointReasonY, string PointReasonN, string PointReasonU, string PointReasonWeight
            , string PointReasonMax, string PointReasonMin, string PointDangerY, string PointDangerN, string PointDangerU, string PointDangerWeight, string PointDangerMax, string PointDangerMin, string PointReasonSum
            , string PointDangerSum, string PointReason, string PointDanger, string Bigo1, string Bigo2, string Bigo3, string Memo, string FileUrl, string Code, string ShownedAnswer)
        {
            DataTable r = new DataTable();

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_SEALZONE_CodeSave";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[38];

            p[0] = new SqlParameter("Idx", SqlDbType.Int);
            p[1] = new SqlParameter("GrpNO", SqlDbType.VarChar);
            p[2] = new SqlParameter("Name1", SqlDbType.NVarChar);
            p[3] = new SqlParameter("Name2", SqlDbType.NVarChar);
            p[4] = new SqlParameter("Name3", SqlDbType.NVarChar);
            p[5] = new SqlParameter("Question", SqlDbType.NVarChar);
            p[6] = new SqlParameter("Content", SqlDbType.NVarChar);
            p[7] = new SqlParameter("AnswerY", SqlDbType.NVarChar);
            p[8] = new SqlParameter("GoY", SqlDbType.VarChar);
            p[9] = new SqlParameter("AnswerN", SqlDbType.NVarChar);
            p[10] = new SqlParameter("GoN", SqlDbType.VarChar);
            p[11] = new SqlParameter("AnswerU", SqlDbType.NVarChar);
            p[12] = new SqlParameter("GoU", SqlDbType.VarChar);
            p[13] = new SqlParameter("ShownedLabel", SqlDbType.NVarChar);
            p[14] = new SqlParameter("IsCritical", SqlDbType.NVarChar);
            p[15] = new SqlParameter("PointReasonY", SqlDbType.VarChar);
            p[16] = new SqlParameter("PointReasonN", SqlDbType.VarChar);
            p[17] = new SqlParameter("PointReasonU", SqlDbType.VarChar);
            p[18] = new SqlParameter("PointReasonWeight", SqlDbType.VarChar);
            p[19] = new SqlParameter("PointReasonMax", SqlDbType.VarChar);
            p[20] = new SqlParameter("PointReasonMin", SqlDbType.VarChar);
            p[21] = new SqlParameter("PointDangerY", SqlDbType.VarChar);
            p[22] = new SqlParameter("PointDangerN", SqlDbType.VarChar);
            p[23] = new SqlParameter("PointDangerU", SqlDbType.VarChar);
            p[24] = new SqlParameter("PointDangerWeight", SqlDbType.VarChar);
            p[25] = new SqlParameter("PointDangerMax", SqlDbType.VarChar);
            p[26] = new SqlParameter("PointDangerMin", SqlDbType.VarChar);
            p[27] = new SqlParameter("PointReasonSum", SqlDbType.VarChar);
            p[28] = new SqlParameter("PointDangerSum", SqlDbType.VarChar);
            p[29] = new SqlParameter("PointReason", SqlDbType.VarChar);
            p[30] = new SqlParameter("PointDanger", SqlDbType.VarChar);
            p[31] = new SqlParameter("Bigo1", SqlDbType.NVarChar);
            p[32] = new SqlParameter("Bigo2", SqlDbType.NVarChar);
            p[33] = new SqlParameter("Bigo3", SqlDbType.NVarChar);
            p[34] = new SqlParameter("Memo", SqlDbType.NVarChar);
            p[35] = new SqlParameter("FileUrl", SqlDbType.VarChar);
            p[36] = new SqlParameter("Code", SqlDbType.VarChar);
            p[37] = new SqlParameter("ShownedAnswer", SqlDbType.VarChar);

            p[0].Value = Idx;
            p[1].Value = GrpNO;
            p[2].Value = Name1;
            p[3].Value = Name2;
            p[4].Value = Name3;
            p[5].Value = Question;
            p[6].Value = Content;
            p[7].Value = AnswerY;
            p[8].Value = GoY;
            p[9].Value = AnswerN;
            p[10].Value = GoN;
            p[11].Value = AnswerU;
            p[12].Value = GoU;
            p[13].Value = ShownedLabel;
            p[14].Value = IsCritical;
            p[15].Value = PointReasonY;
            p[16].Value = PointReasonN;
            p[17].Value = PointReasonU;
            p[18].Value = PointReasonWeight;
            p[19].Value = PointReasonMax;
            p[20].Value = PointReasonMin;
            p[21].Value = PointDangerY;
            p[22].Value = PointDangerN;
            p[23].Value = PointDangerU;
            p[24].Value = PointDangerWeight;
            p[25].Value = PointDangerMax;
            p[26].Value = PointDangerMin;
            p[27].Value = PointReasonSum;
            p[28].Value = PointDangerSum;
            p[29].Value = PointReason;
            p[30].Value = PointDanger;
            p[31].Value = Bigo1;
            p[32].Value = Bigo2;
            p[33].Value = Bigo3;
            p[34].Value = Memo;
            p[35].Value = FileUrl;
            p[36].Value = Code;
            p[37].Value = ShownedAnswer;

            com.Parameters.AddRange(p);

            //서버로그 남기기기

            WriteServerLog(com.CommandText, p);

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(r);
            }
            catch (Exception ex)
            {
                r = null;
                _Error = ex.Message;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public int BONBU_CodeSealZoneDel(SqlConnection con, SqlTransaction tran, int Idx)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_SEALZONE_CodeDel";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("Idx", SqlDbType.Int);

            p[0].Value = Idx;

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
                _Error = ex.Message;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public int BONBU_SignSync(SqlConnection con, SqlTransaction tran, string Center, int SaupID, string VisitDate, string Visitor)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_BONBU_SignSync";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[2] = new SqlParameter("VisitDate", SqlDbType.Char);
            p[3] = new SqlParameter("Visitor", SqlDbType.Char);
            p[0].Value = Center;
            p[1].Value = SaupID;
            p[2].Value = VisitDate;
            p[3].Value = Visitor;

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
                _Error = ex.Message;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public DataTable BONBU_SignSyncVisitDate(string Center, int SaupID, string Year)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_BONBU_SignSyncVisitDate";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[3];
                    p[0] = new SqlParameter("Center", SqlDbType.Char);
                    p[1] = new SqlParameter("SaupId", SqlDbType.Int);
                    p[2] = new SqlParameter("Year", SqlDbType.Char);
                    p[0].Value = Center;
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
                        _Error = ex.Message;
                        LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
                    }
                    return dt;
                }

            }
        }


        public DataTable BONBU_CodeHoodCnt(string Code)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_BONBU_CodeHoodCnt";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[1];
                    p[0] = new SqlParameter("@Code", SqlDbType.Char);

                    p[0].Value = Code;

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

        public int BONBU_CodeHoodSave(SqlConnection con, SqlTransaction tran, string Code, string GroupName, string Name, string WindSpeed)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_BONBU_CodeHoodSave";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("Code", SqlDbType.VarChar);
            p[1] = new SqlParameter("GroupName", SqlDbType.VarChar);
            p[2] = new SqlParameter("Name", SqlDbType.VarChar);
            p[3] = new SqlParameter("WindSpeed", SqlDbType.VarChar);

            p[0].Value = Code;
            p[1].Value = GroupName;
            p[2].Value = Name;
            p[3].Value = WindSpeed;

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
                _Error = ex.Message;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public int BONBU_JijungDel(SqlConnection con, SqlTransaction tran, string pCenter, string pYearmon, string pSincheongDate)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_JijungDel";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[1] = new SqlParameter("Yearmon", SqlDbType.Char);
            p[2] = new SqlParameter("SincheongDate", SqlDbType.Char);
            p[3] = new SqlParameter("Bonbu", SqlDbType.Char);

            p[0].Value = pCenter;
            p[1].Value = pYearmon;
            p[2].Value = pSincheongDate;
            p[3].Value = UserID;

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
                _Error = ex.Message;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public DataTable BONBU_JijungSTANDList(string pCenter, string pYearmon, string pGubun)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_JijungSTANDList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[3];
                    p[0] = new SqlParameter("Center", SqlDbType.Char);
                    p[1] = new SqlParameter("Yearmon", SqlDbType.Char);
                    p[2] = new SqlParameter("Gubun", SqlDbType.Char);

                    p[0].Value = pCenter;
                    p[1].Value = pYearmon;
                    p[2].Value = pGubun;

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

        public int BONBU_SangtaeCodeDel(SqlConnection con, SqlTransaction tran, string Code)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_SangtaeCodeDetailDel";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("Code", SqlDbType.VarChar);

            p[0].Value = Code;

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
                _Error = ex.Message;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public int BONBU_SangtaeCodeSave(SqlConnection con, SqlTransaction tran, string Code, string JidoCode)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_SangtaeCodeDetailSave";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("Code", SqlDbType.VarChar);
            p[1] = new SqlParameter("JidoCode", SqlDbType.VarChar);

            p[0].Value = Code;
            p[1].Value = JidoCode;

            com.Parameters.AddRange(p);

            //서버로그 남기기기

            WriteServerLog(com.CommandText, p);

            try
            {
                r = com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                _Error = ex.Message;
                r = -1;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public DataSet CODE_Sangtae(string Gubun = "")
        {
            DataSet dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataSet();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_SangtaeCodeList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[1];
                    p[0] = new SqlParameter("Gubun", SqlDbType.VarChar);
                    p[0].Value = Gubun;

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

        public DataTable BONBU_SangtaeJido(string Code)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_SangtaeCodeDetailList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[1];
                    p[0] = new SqlParameter("Code", SqlDbType.VarChar);
                    p[0].Value = Code;

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

        public int SaupjaUpmuManagerFileDel(SqlConnection con, SqlTransaction tran, string Center = "", int SaupID = -1, string UploadFileName = "")
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_SaupjaUpmuManagerFileDel";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("Center", SqlDbType.VarChar);
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[2] = new SqlParameter("UploadFileName", SqlDbType.VarChar);

            p[0].Value = Center;
            p[1].Value = SaupID;
            p[2].Value = UploadFileName;

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
                _Error = ex.Message;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        //UP_NBOGUN_SaupjaUpmuDamdangAdd
        public int SaupjaUpmuManagerFileSave(SqlConnection con, SqlTransaction tran,string Center, int SaupID, string Yearmon, string Gubun, string LocalFileName, string UploadFileName, string UploadFileUrl)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_SaupjaUpmuManagerFileSave";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[7];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = Center;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("@Yearmon", SqlDbType.Char);
            p[2].Value = Yearmon;
            p[3] = new SqlParameter("@Gubun", SqlDbType.Char);
            p[3].Value = Gubun;
            p[4] = new SqlParameter("LocalFileName", SqlDbType.Char);
            p[4].Value = LocalFileName;
            p[5] = new SqlParameter("@UploadFileName", SqlDbType.Char);
            p[5].Value = UploadFileName;
            p[6] = new SqlParameter("@UploadFileUrl", SqlDbType.Char);
            p[6].Value = UploadFileUrl;

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
                _Error = ex.Message;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        //UP_NBOGUN_OtherBuseoSaupja
        public DataTable BONBU_SangdamPersonCntList(string Year)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_BONBU_SangdamPersonCntList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("Year", SqlDbType.Char);
                p[0].Value = Year;

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

        //타부서 실적 연계 관리번호 기준
        public DataTable BONBU_SiljukOtherBuseo(string Year)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_BONBU_SiljukOtherBuseo";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[1];
                p[0] = new SqlParameter("Year", SqlDbType.Char);
                p[0].Value = Year;

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
        //

        /// <summary>
        /// 사업장 만족도 조사를 보내기 위해 해당작업연월의 사업장 담당자 정보를 보여줌
        /// </summary>
        /// <param name="Year"></param>
        /// <returns></returns>
        public DataTable BONBU_BogunDamdangList(string Centers, string Yearmon)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_BONBU_BogunDamdangList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("Centers", SqlDbType.VarChar);
                p[0].Value = Centers;
                p[1] = new SqlParameter("Yearmon", SqlDbType.VarChar);
                p[1].Value = Yearmon;

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

        //UP_NBOGUN_BONBU_BogunDamdangSMSHistory
        public DataTable BONBU_BogunDamdangSMSHistory(string Centers, string SDate, string EDate)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_BONBU_BogunDamdangSMSHistory";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Centers", SqlDbType.VarChar);
                p[0].Value = Centers;
                p[1] = new SqlParameter("SDate", SqlDbType.Char);
                p[1].Value = SDate;
                p[2] = new SqlParameter("EDate", SqlDbType.Char);
                p[2].Value = EDate;

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

        //UP_NBOGUN_BONBU_SangtaeSeunginTimeList
        public DataTable BONBU_SangtaeSeunginTimeList(string Center, int Cut, string SYearmon, string EYearmon)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_BONBU_SangtaeSeunginTimeList";
                com.CommandType = CommandType.StoredProcedure;
                
                SqlParameter[] p = new SqlParameter[4];
                p[0] = new SqlParameter("Center", SqlDbType.VarChar);
                p[0].Value = Center;
                p[1] = new SqlParameter("Cut", SqlDbType.Int);
                p[1].Value = Cut;
                p[2] = new SqlParameter("SYearmon", SqlDbType.VarChar);
                p[2].Value = SYearmon;
                p[3] = new SqlParameter("EYearmon", SqlDbType.VarChar);
                p[3].Value = EYearmon;

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
        async public Task<DataTable> BONBU_SangtaeSeunginTimeListAsync(string Center, int Cut, string SYearmon, string EYearmon)
        {
            using (SqlConnection connection = new SqlConnection(Biz.Instance.ConnectionString))
            {
                DataTable dt = new DataTable();
                //dt.Columns.Add("CenterName", typeof(string));
                //dt.Columns.Add("Date", typeof(string));
                //dt.Columns.Add("Center", typeof(string));
                //dt.Columns.Add("DHJikjong", typeof(string));
                //dt.Columns.Add("SaupID", typeof(int));
                //dt.Columns.Add("Visitor", typeof(string));
                //dt.Columns.Add("SaupjaName", typeof(string));
                //dt.Columns.Add("VisitorName", typeof(string));
                //dt.Columns.Add("SignSysDate", typeof(string));
                //dt.Columns.Add("Part1Name", typeof(string));
                //dt.Columns.Add("Part1SysDate", typeof(string));
                //dt.Columns.Add("Part2Name", typeof(string));
                //dt.Columns.Add("Part2SysDate", typeof(string));
                //dt.Columns.Add("BuseojangName", typeof(string));
                //dt.Columns.Add("BuseojangSysDate", typeof(string));
                //dt.Columns.Add("DateDif", typeof(int));
                //dt.Columns.Add("NextName", typeof(string));
                //dt.Columns.Add("NextJikchk", typeof(string));
                //dt.Columns.Add("DHJikjongName", typeof(string));

                await connection.OpenAsync();

                using (SqlCommand com = new SqlCommand("UP_NBOGUN_BONBU_SangtaeSeunginTimeList", connection))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    SqlParameter[] p = new SqlParameter[4];
                    p[0] = new SqlParameter("Center", SqlDbType.VarChar);
                    p[0].Value = Center;
                    p[1] = new SqlParameter("Cut", SqlDbType.Int);
                    p[1].Value = Cut;
                    p[2] = new SqlParameter("SYearmon", SqlDbType.VarChar);
                    p[2].Value = SYearmon;
                    p[3] = new SqlParameter("EYearmon", SqlDbType.VarChar);
                    p[3].Value = EYearmon;

                    com.Parameters.AddRange(p);

                    // The reader needs to be executed with the SequentialAccess behavior to enable network streaming
                    // Otherwise ReadAsync will buffer the entire Xml Document into memory which can cause scalability issues or even OutOfMemoryExceptions
                    using (SqlDataReader reader = await com.ExecuteReaderAsync(CommandBehavior.SequentialAccess))
                    {
                        if (reader.HasRows)
                        {
                            DataTable schemaTable = reader.GetSchemaTable();
                            foreach (DataRow row in schemaTable.Rows)
                            {
                                string colName = row.Field<string>("ColumnName");
                                Type t = row.Field<Type>("DataType");
                                dt.Columns.Add(colName, t);
                            }
                            while (await reader.ReadAsync())
                            {
                                var newRow = dt.Rows.Add();
                                foreach (DataColumn col in dt.Columns)
                                {
                                    newRow[col.ColumnName] = reader[col.ColumnName];
                                }
                            }
                        }
                    }
                }

                return dt;
            }
        }

        //계약서 내용 변경
        public int BONBU_ContractChange(SqlConnection con, SqlTransaction tran, string Center, int SaupID, string Date, int Inwon, int SamuM, int SamuW, int SaengsanM, int SaengsanW,
            int PersonAmount, int CheongguAmount)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_BONBU_ContractChange";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[11];
            p[0] = new SqlParameter("Center", SqlDbType.VarChar);
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[2] = new SqlParameter("Date", SqlDbType.Char);
            p[3] = new SqlParameter("Inwon", SqlDbType.Int);
            p[4] = new SqlParameter("SamuM", SqlDbType.Int);
            p[5] = new SqlParameter("SamuW", SqlDbType.Int);
            p[6] = new SqlParameter("SaengsanM", SqlDbType.Int);
            p[7] = new SqlParameter("SaengsanW", SqlDbType.Int);
            p[8] = new SqlParameter("PersonAmount", SqlDbType.Int);
            p[9] = new SqlParameter("CheongguAmount", SqlDbType.Int);
            p[10] = new SqlParameter("ChangeUserID", SqlDbType.Char);

            p[0].Value = Center;
            p[1].Value = SaupID;
            p[2].Value = Date;
            p[3].Value = Inwon;
            p[4].Value = SamuM;
            p[5].Value = SamuW;
            p[6].Value = SaengsanM;
            p[7].Value = SaengsanW;
            p[8].Value = PersonAmount;
            p[9].Value = CheongguAmount;
            p[10].Value = UserID;

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

        public int BONBU_SangtaeChange(SqlConnection con, SqlTransaction tran, string Center, int SaupID, string Date, string Visitor, 
            string NextName, string NextJichk, string DHJikjongName)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_BONBU_SangtaeChange";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[8];
            p[0] = new SqlParameter("Center", SqlDbType.VarChar);
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[2] = new SqlParameter("Date", SqlDbType.Char);
            p[3] = new SqlParameter("Visitor", SqlDbType.Char);
            p[4] = new SqlParameter("NextName", SqlDbType.VarChar);
            p[5] = new SqlParameter("NextJikchk", SqlDbType.VarChar);
            p[6] = new SqlParameter("DHJikjongName", SqlDbType.VarChar);
            p[7] = new SqlParameter("UserID", SqlDbType.Char);

            p[0].Value = Center;
            p[1].Value = SaupID;
            p[2].Value = Date;
            p[3].Value = Visitor;
            p[4].Value = NextName;
            p[5].Value = NextJichk;
            p[6].Value = DHJikjongName;
            p[7].Value = UserID;

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

        public int BONBU_BogunDamdangSMSSave(SqlConnection con, SqlTransaction tran, string Center, int SaupID , string SaupjaNum, string Yearmon, string Name, string Sosok, string Tel, string SMS)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_BONBU_BogunDamdangSMSSave";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[8];
            p[0] = new SqlParameter("Center", SqlDbType.VarChar);
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[2] = new SqlParameter("SaupjaNum", SqlDbType.Char);
            p[3] = new SqlParameter("Yearmon", SqlDbType.Char);
            p[4] = new SqlParameter("Name", SqlDbType.VarChar);
            p[5] = new SqlParameter("Sosok", SqlDbType.VarChar);
            p[6] = new SqlParameter("Tel", SqlDbType.VarChar);
            p[7] = new SqlParameter("SMS", SqlDbType.VarChar);

            p[0].Value = Center;
            p[1].Value = SaupID;
            p[2].Value = SaupjaNum;
            p[3].Value = Yearmon;
            p[4].Value = Name;
            p[5].Value = Sosok;
            p[6].Value = Tel;
            p[7].Value = SMS;

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


        public int BONBU_CodeLibraryUpdate(SqlConnection con, SqlTransaction tran, string Code, string Bigo, string IsUsage)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_BONBU_CodeLibraryUpdate";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("Code", SqlDbType.VarChar);
            p[1] = new SqlParameter("Bigo", SqlDbType.VarChar);
            p[2] = new SqlParameter("IsUsage", SqlDbType.VarChar);

            p[0].Value = Code;
            p[1].Value = Bigo;
            p[2].Value = IsUsage;

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
}
