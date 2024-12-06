using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBOGUN
{
    partial class Biz
    {
        /// <summary>
        /// 상태보고서 검진 판정 개수
        /// </summary>
        /// <param name="SaupID"></param>
        /// <param name="VisitDate"></param>
        /// <param name="Gubun"> '1' : 보건관리 데이터를 기반으로 산출된 해당연도 판정별 인원
		///		                 '2' : 상태보고서에 수기입력한 데이터가 있을 경우 해당 데이터 표시
		///						       1번 데이터가 없을 경우 보건관리 데이터를 기반으로 산출된 해당연도 판정별 인원
		///						 '3' : 직전 방문일의 상태보고서에 수기입력한 데이터가 있을 경우 해당 데이터 표시
		///						       1번 데이터가 없을 경우 보건관리 데이터를 기반으로 산출된 해당연도 판정별 인원</param>
        /// <returns></returns>
        public DataTable SangtaeGumjinDataList(int SaupID, string VisitDate, string Gubun)
        {
            lock (lockObject)
            {
                DataTable dt;
                using (SqlConnection con = new SqlConnection(_Connection))
                {

                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_SangtaeGumjinDataList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[4];
                    p[0] = new SqlParameter("Center", SqlDbType.Char);
                    p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                    p[2] = new SqlParameter("VisitDate", SqlDbType.VarChar);
                    p[3] = new SqlParameter("Gubun", SqlDbType.VarChar);

                    p[0].Value = DHCenter;
                    p[1].Value = SaupID;
                    p[2].Value = VisitDate;
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
                        Error = ex.Message;
                        LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
                    }
                    return dt;
                }
            }
        }
        //UP_NBOGUN_SangtaeGumjinDataSave
        public DataTable SangtaeGumjinDataSave(SqlConnection con, SqlTransaction tran, int SaupID, string VisitDate, string Visitor, string D1, string D2, string DN, string C1, string C2, string CN)
        {
            DataTable r = new DataTable();

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_SangtaeGumjinDataSave ";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[10];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("VisitDate", SqlDbType.Char);
            p[2].Value = VisitDate;
            p[3] = new SqlParameter("Visitor", SqlDbType.Char);
            p[3].Value = Visitor;
            p[4] = new SqlParameter("D1", SqlDbType.VarChar);
            p[4].Value = D1;
            p[5] = new SqlParameter("D2", SqlDbType.VarChar);
            p[5].Value = D2;
            p[6] = new SqlParameter("DN", SqlDbType.VarChar);
            p[6].Value = DN;
            p[7] = new SqlParameter("C1", SqlDbType.VarChar);
            p[7].Value = C1;
            p[8] = new SqlParameter("C2", SqlDbType.VarChar);
            p[8].Value = C2;
            p[9] = new SqlParameter("CN", SqlDbType.VarChar);
            p[9].Value = CN;


            com.Parameters.AddRange(p);

            //서버로그 남기기기
            WriteServerLog(com.CommandText, p);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            try
            {
                adapter.Fill(r);
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                r = null;

                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public DataTable VisitDateSignInquire(int SaupID, string VisitDate, string Visitor, string Gubun)
        {
            lock (lockObject)
            {
                DataTable dt;

                using (SqlConnection con = new SqlConnection(_Connection))
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_VisitDateSignInqure";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[5];
                    p[0] = new SqlParameter("Center", SqlDbType.Char);
                    p[0].Value = DHCenter;
                    p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                    p[1].Value = SaupID;
                    p[2] = new SqlParameter("VisitDate", SqlDbType.Char);
                    p[2].Value = VisitDate;
                    p[3] = new SqlParameter("Visitor", SqlDbType.Char);
                    p[3].Value = Visitor;
                    p[4] = new SqlParameter("Gubun", SqlDbType.VarChar);
                    p[4].Value = Gubun;

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

        public SqlDataReader SangtaeFKita1(int SaupID, string VisitDate)
        {
            SqlDataReader r;
            SqlConnection con = new SqlConnection(_Connection);
            con.Open();
            SqlCommand com = con.CreateCommand();

            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("VisitDate", SqlDbType.Char);
            p[2].Value = VisitDate;

            com.CommandText = "UP_NBOGUN_SangtaeFKita1";
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddRange(p);

            //서버로그 남기기기
            WriteServerLog(com.CommandText, p);
            try
            {
                r = com.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                r = null;
                LogManager.WriteLog(ex.Message);
            }

            return r;
        }

        public SqlDataReader SangtaeFKita2(int SaupID, string VisitDate, string Visitor)
        {
            SqlDataReader r;
            SqlConnection con = new SqlConnection(_Connection);
            con.Open();
            SqlCommand com = con.CreateCommand();

            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("VisitDate", SqlDbType.Char);
            p[2].Value = VisitDate;
            p[3] = new SqlParameter("Visitor", SqlDbType.Char);
            p[3].Value = VisitDate;

            com.CommandText = "UP_NBOGUN_SangtaeFKita2";
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddRange(p);

            //서버로그 남기기기
            WriteServerLog(com.CommandText, p);
            try
            {
                r = com.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                r = null;
                LogManager.WriteLog(ex.Message);
            }

            return r;
        }

        public SqlDataReader SangtaeFKita3(int SaupID, string VisitDate, string Visitor)
        {
            SqlDataReader r;
            SqlConnection con = new SqlConnection(_Connection);
            con.Open();
            SqlCommand com = con.CreateCommand();

            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("VisitDate", SqlDbType.Char);
            p[2].Value = VisitDate;
            p[3] = new SqlParameter("Visitor", SqlDbType.Char);
            p[3].Value = VisitDate;

            com.CommandText = "UP_NBOGUN_SangtaeFKita3";
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddRange(p);

            //서버로그 남기기기
            WriteServerLog(com.CommandText, p);
            try
            {
                r = com.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                r = null;
                LogManager.WriteLog(ex.Message);
            }

            return r;
        }

        public SqlDataReader SangtaeIKita1(int SaupID, string VisitDate)
        {
            SqlDataReader r;
            SqlConnection con = new SqlConnection(_Connection);
            con.Open();
            SqlCommand com = con.CreateCommand();

            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("VisitDate", SqlDbType.Char);
            p[2].Value = VisitDate;

            com.CommandText = "UP_NBOGUN_SangtaeIKita1";
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddRange(p);

            //서버로그 남기기기
            WriteServerLog(com.CommandText, p);
            try
            {
                r = com.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                r = null;
                LogManager.WriteLog(ex.Message);
            }

            return r;
        }

        public SqlDataReader SangtaeJKita1(int SaupID, string VisitDate, string Visitor)
        {
            SqlDataReader r;
            SqlConnection con = new SqlConnection(_Connection);
            con.Open();
            SqlCommand com = con.CreateCommand();

            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("VisitDate", SqlDbType.Char);
            p[2].Value = VisitDate;
            p[3] = new SqlParameter("Visitor", SqlDbType.Char);
            p[3].Value = Visitor;

            com.CommandText = "UP_NBOGUN_SangtaeJKita1";
            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddRange(p);

            //서버로그 남기기기
            WriteServerLog(com.CommandText, p);
            try
            {
                r = com.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                r = null;
                LogManager.WriteLog(ex.Message);
            }

            return r;
        }
    }
}
