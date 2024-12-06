using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBOGUN
{
    public partial class Biz
    {
        public int SEALZONE_SaupjaDateDel(SqlConnection con, SqlTransaction tran, string SaupjaNum, string Date)
        {
            int r = -1;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_SEALZONE_SaupjaDateDel";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[3];

            p[0] = new SqlParameter("Center", SqlDbType.VarChar);
            p[1] = new SqlParameter("SaupjaNum", SqlDbType.VarChar);
            p[2] = new SqlParameter("Date", SqlDbType.VarChar);

            p[0].Value = _DHCenter;
            p[1].Value = SaupjaNum;
            p[2].Value = Date;

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

        //UP_SEALZONE_SaupjaDate
        public int SEALZONE_SaupjaDateSave(SqlConnection con, SqlTransaction tran, string SaupjaNum, string Date, int Idx, string GrpNO, string Name1, string Name2, string Name3, string Question, string Content,
            string AnswerY, string GoY, string AnswerN, string GoN, string AnswerU, string GoU, string ShownedLabel, string IsCritical, string PointReasonY, string PointReasonN, string PointReasonU, string PointReasonWeight
            , string PointReasonMax, string PointReasonMin, string PointDangerY, string PointDangerN, string PointDangerU, string PointDangerWeight, string PointDangerMax, string PointDangerMin, string PointReasonSum
            , string PointDangerSum, string PointReason, string PointDanger, string Bigo1, string Bigo2, string Bigo3, string Memo, string FileUrl, string Answer, string Code, string ShownedAnswer, int Odx,
            string Enabled)
        {
            int r = -1;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_SEALZONE_SaupjaDateSave";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[44];

            p[0] = new SqlParameter("Center", SqlDbType.VarChar);
            p[1] = new SqlParameter("SaupjaNum", SqlDbType.VarChar);
            p[2] = new SqlParameter("Date", SqlDbType.VarChar);
            p[3] = new SqlParameter("Idx", SqlDbType.Int);
            p[4] = new SqlParameter("GrpNO", SqlDbType.VarChar);
            p[5] = new SqlParameter("Name1", SqlDbType.NVarChar);
            p[6] = new SqlParameter("Name2", SqlDbType.NVarChar);
            p[7] = new SqlParameter("Name3", SqlDbType.NVarChar);
            p[8] = new SqlParameter("Question", SqlDbType.NVarChar);
            p[9] = new SqlParameter("Content", SqlDbType.NVarChar);
            p[10] = new SqlParameter("AnswerY", SqlDbType.NVarChar);
            p[11] = new SqlParameter("GoY", SqlDbType.VarChar);
            p[12] = new SqlParameter("AnswerN", SqlDbType.NVarChar);
            p[13] = new SqlParameter("GoN", SqlDbType.VarChar);
            p[14] = new SqlParameter("AnswerU", SqlDbType.NVarChar);
            p[15] = new SqlParameter("GoU", SqlDbType.VarChar);
            p[16] = new SqlParameter("ShownedLabel", SqlDbType.NVarChar);
            p[17] = new SqlParameter("IsCritical", SqlDbType.NVarChar);
            p[18] = new SqlParameter("PointReasonY", SqlDbType.VarChar);
            p[19] = new SqlParameter("PointReasonN", SqlDbType.VarChar);
            p[20] = new SqlParameter("PointReasonU", SqlDbType.VarChar);
            p[21] = new SqlParameter("PointReasonWeight", SqlDbType.VarChar);
            p[22] = new SqlParameter("PointReasonMax", SqlDbType.VarChar);
            p[23] = new SqlParameter("PointReasonMin", SqlDbType.VarChar);
            p[24] = new SqlParameter("PointDangerY", SqlDbType.VarChar);
            p[25] = new SqlParameter("PointDangerN", SqlDbType.VarChar);
            p[26] = new SqlParameter("PointDangerU", SqlDbType.VarChar);
            p[27] = new SqlParameter("PointDangerWeight", SqlDbType.VarChar);
            p[28] = new SqlParameter("PointDangerMax", SqlDbType.VarChar);
            p[29] = new SqlParameter("PointDangerMin", SqlDbType.VarChar);
            p[30] = new SqlParameter("PointReasonSum", SqlDbType.VarChar);
            p[31] = new SqlParameter("PointDangerSum", SqlDbType.VarChar);
            p[32] = new SqlParameter("PointReason", SqlDbType.VarChar);
            p[33] = new SqlParameter("PointDanger", SqlDbType.VarChar);
            p[34] = new SqlParameter("Bigo1", SqlDbType.NVarChar);
            p[35] = new SqlParameter("Bigo2", SqlDbType.NVarChar);
            p[36] = new SqlParameter("Bigo3", SqlDbType.NVarChar);
            p[37] = new SqlParameter("Memo", SqlDbType.NVarChar);
            p[38] = new SqlParameter("FileUrl", SqlDbType.VarChar);
            p[39] = new SqlParameter("Answer", SqlDbType.VarChar);
            p[40] = new SqlParameter("Code", SqlDbType.VarChar);
            p[41] = new SqlParameter("ShownedAnswer", SqlDbType.VarChar);
            p[42] = new SqlParameter("Odx", SqlDbType.Int);
            p[43] = new SqlParameter("Enabled", SqlDbType.VarChar);

            p[0].Value = _DHCenter;
            p[1].Value = SaupjaNum;
            p[2].Value = Date;
            p[3].Value = Idx;
            p[4].Value = GrpNO;
            p[5].Value = Name1;
            p[6].Value = Name2;
            p[7].Value = Name3;
            p[8].Value = Question;
            p[9].Value = Content;
            p[10].Value = AnswerY;
            p[11].Value = GoY;
            p[12].Value = AnswerN;
            p[13].Value = GoN;
            p[14].Value = AnswerU;
            p[15].Value = GoU;
            p[16].Value = ShownedLabel;
            p[17].Value = IsCritical;
            p[18].Value = PointReasonY;
            p[19].Value = PointReasonN;
            p[20].Value = PointReasonU;
            p[21].Value = PointReasonWeight;
            p[22].Value = PointReasonMax;
            p[23].Value = PointReasonMin;
            p[24].Value = PointDangerY;
            p[25].Value = PointDangerN;
            p[26].Value = PointDangerU;
            p[27].Value = PointDangerWeight;
            p[28].Value = PointDangerMax;
            p[29].Value = PointDangerMin;
            p[30].Value = PointReasonSum;
            p[31].Value = PointDangerSum;
            p[32].Value = PointReason;
            p[33].Value = PointDanger;
            p[34].Value = Bigo1;
            p[35].Value = Bigo2;
            p[36].Value = Bigo3;
            p[37].Value = Memo;
            p[38].Value = FileUrl;
            p[39].Value = Answer;
            p[40].Value = Code;
            p[41].Value = ShownedAnswer;
            p[42].Value = Odx;
            p[43].Value = Enabled;

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

        public DataTable SEALZONE_SaupjaDate(string SaupjaNum, string Date)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_SEALZONE_SaupjaDate";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[3];
                    p[0] = new SqlParameter("Center", SqlDbType.Char);
                    p[1] = new SqlParameter("SaupjaNum", SqlDbType.Char);
                    p[2] = new SqlParameter("Date", SqlDbType.Char);

                    p[0].Value = DHCenter;
                    p[1].Value = SaupjaNum;
                    p[2].Value = Date;

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


        public DataTable SEALZONE_SaupjaDateList(string SaupjaNum)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_SEALZONE_SaupjaDateList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[2];
                    p[0] = new SqlParameter("Center", SqlDbType.Char);
                    p[1] = new SqlParameter("SaupjaNum", SqlDbType.Char);

                    p[0].Value = DHCenter;
                    p[1].Value = SaupjaNum;

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


        public DataTable SEALZONE_SaupjaList(string Year, string SaupjaName)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_SEALZONE_SaupjaList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[3];
                    p[0] = new SqlParameter("Center", SqlDbType.Char);
                    p[1] = new SqlParameter("Year", SqlDbType.Char);
                    p[2] = new SqlParameter("SaupjaName", SqlDbType.VarChar);

                    p[0].Value = DHCenter;
                    p[1].Value = Year;
                    p[2].Value = SaupjaName;

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
    }
}
