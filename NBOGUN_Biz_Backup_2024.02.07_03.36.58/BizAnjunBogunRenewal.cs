using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBOGUN
{
    //안전보건관리규정 제개정
    partial class Biz
    {
        /// <summary>
        /// 연도별 안전보건관리규정재개정일
        /// </summary>
        /// <param name="CurrentSaupID">통합번호</param>
        /// <param name="Yearmon">기준일</param>
        /// <param name="pGubun">"1" 당해연도, "2" 기준연도 기준 포함 모든 과거 기록</param>
        /// <returns></returns>
        public DataTable AnjunBogunRenewalList(int CurrentSaupID, string Yearmon, string pGubun = "3")
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_AnjunBogunRenewalList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[4];
                    p[0] = new SqlParameter("Center", SqlDbType.Char);
                    p[0].Value = DHCenter;
                    p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                    p[1].Value = CurrentSaupID;
                    p[2] = new SqlParameter("@Yearmon", SqlDbType.Char);
                    p[2].Value = Yearmon;
                    p[3] = new SqlParameter("Gubun", SqlDbType.Char);
                    p[3].Value = pGubun;

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

        public int AnjunBogunRenewalDel(SqlConnection con, SqlTransaction tran, int SaupID, string pYear)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_AnjunBogunRenewalDel";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("Year", SqlDbType.VarChar);
            p[2].Value = pYear;

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

        public int AnjunBogunRenewalAdd(SqlConnection con, SqlTransaction tran, int SaupID, string pDate, string pGubun, string pContent)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_AnjunBogunRenewalAdd";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("Date", SqlDbType.Char);
            p[2].Value = pDate;
            p[3] = new SqlParameter("Gubun", SqlDbType.Char);
            p[3].Value = pGubun;
            p[4] = new SqlParameter("Content", SqlDbType.VarChar);
            p[4].Value = pContent;

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
    }
}
