using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBOGUN
{
    public partial class Biz
    {
        /// <summary>
        /// 사업장 관리카드의 해당없음
        /// </summary>
        /// <param name="con"></param>
        /// <param name="tran"></param>
        /// <param name="SaupID"></param>
        /// <param name="Yearmon"></param>
        /// <param name="Daesang15">15.위험물질 공란이면 기존값 유지</param>
        /// <param name="Daesang16">16.유해물질 공란이면 기존값 유지</param>
        /// <param name="Daesang14">14.국소배기안전검사 공란이면 기존값 유지</param>
        /// <returns></returns>
        public int MonthDetailDataAdd(SqlConnection con, SqlTransaction tran, int SaupID, string Yearmon, string Daesang15, string Daesang16, string Daesang14)
        {
            int r = -1;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_MonthDetailDataAdd";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[6];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
            p[2].Value = Yearmon;
            p[3] = new SqlParameter("Daesang15", SqlDbType.VarChar);
            p[3].Value = Daesang15;
            p[4] = new SqlParameter("Daesang16", SqlDbType.VarChar);
            p[4].Value = Daesang16;
            p[5] = new SqlParameter("Daesang14", SqlDbType.VarChar);
            p[5].Value = Daesang14;

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

                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        /// <summary>
        /// 해당 사업장의 사업장 관리카드 해당없음 리스트
        /// </summary>
        /// <param name="SaupID"></param>
        /// <param name="Year"></param>
        /// <returns></returns>
        public DataTable MonthDetailDataList(int SaupID, string Yearmon)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_MonthDetailDataList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[3];
                    p[0] = new SqlParameter("Center", SqlDbType.VarChar);
                    p[0].Value = DHCenter;
                    p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                    p[1].Value = SaupID;
                    p[2] = new SqlParameter("Yearmon", SqlDbType.VarChar);
                    p[2].Value = Yearmon;


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

        public DataTable RPT_SaupjaCardZ18(int SaupID, string Yearmon)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_RPT_SaupjaCardZ18";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[3];
                    p[0] = new SqlParameter("Center", SqlDbType.VarChar);
                    p[0].Value = DHCenter;
                    p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                    p[1].Value = SaupID;
                    p[2] = new SqlParameter("Yearmon", SqlDbType.VarChar);
                    p[2].Value = Yearmon;


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

        public DataTable RPT_SaupjaCardBagiList(int SaupID, string VisitDate)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_RPT_SaupjaCardBagiList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[3];
                    p[0] = new SqlParameter("Center", SqlDbType.VarChar);
                    p[0].Value = DHCenter;
                    p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                    p[1].Value = SaupID;
                    p[2] = new SqlParameter("VisitDate", SqlDbType.VarChar);
                    p[2].Value = VisitDate;


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

        public int RPT_SaupjaCardBagiSave(SqlConnection con, SqlTransaction tran, int SaupID, string Year, string SeolbiName, int Cnt, string GumsaJugi, string GumsaKikwan
            , string Year1, string Date1, string Year2, string Date2, string Year3, string Date3, string Year4, string Date4)
        {
            int r = -1;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_RPT_SaupjaCardBagiSave";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[15];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("Year", SqlDbType.Char);
            p[2].Value = Year;
            p[3] = new SqlParameter("SeolbiName", SqlDbType.VarChar);
            p[3].Value = SeolbiName;
            p[4] = new SqlParameter("Cnt", SqlDbType.Int);
            p[4].Value = Cnt;
            p[5] = new SqlParameter("GumsaJugi", SqlDbType.VarChar);
            p[5].Value = GumsaJugi;
            p[6] = new SqlParameter("GumsaKikwan", SqlDbType.VarChar);
            p[6].Value = GumsaKikwan;
            p[7] = new SqlParameter("Year1", SqlDbType.VarChar);
            p[7].Value = Year1;
            p[8] = new SqlParameter("Date1", SqlDbType.VarChar);
            p[8].Value = Date1;
            p[9] = new SqlParameter("Year2", SqlDbType.VarChar);
            p[9].Value = Year2;
            p[10] = new SqlParameter("Date2", SqlDbType.VarChar);
            p[10].Value = Date2;
            p[11] = new SqlParameter("Year3", SqlDbType.VarChar);
            p[11].Value = Year3;
            p[12] = new SqlParameter("Date3", SqlDbType.VarChar);
            p[12].Value = Date3;
            p[13] = new SqlParameter("Year4", SqlDbType.VarChar);
            p[13].Value = Year4;
            p[14] = new SqlParameter("Date4", SqlDbType.VarChar);
            p[14].Value = Date4;

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

                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public int RPT_SaupjaCardBagiDel(SqlConnection con, SqlTransaction tran, int SaupID, string Year, string IsDaesang)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_RPT_SaupjaCardBagiDel";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("Year", SqlDbType.Char);
            p[2].Value = Year;
            p[3] = new SqlParameter("IsDaesang", SqlDbType.Char);
            p[3].Value = IsDaesang;

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
    }
}
