using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;


namespace NBOGUN
{
    public partial class Biz
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SaupID"></param>
        /// <param name="Year">공란이면 전체</param>
        /// <returns></returns>
        public DataTable JaehaejaList(int SaupID, string Year, int SeqNO = -1)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_JaehaejaList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[4];
                    p[0] = new SqlParameter("Center", SqlDbType.Char);
                    p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                    p[2] = new SqlParameter("Year", SqlDbType.VarChar);
                    p[3] = new SqlParameter("SeqNO", SqlDbType.Int);

                    p[0].Value = DHCenter;
                    p[1].Value = SaupID;
                    p[2].Value = Year;
                    p[3].Value = SeqNO;

                    com.Parameters.AddRange(p);

                    //서버로그 남기기기
                    WriteServerLog(com.CommandText, p);

                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        public DataTable SangtaeJaehaeja(int SaupID, string VisitDate)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_SangtaeJaehaeja";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[3];
                    p[0] = new SqlParameter("Center", SqlDbType.Char);
                    p[0].Value = DHCenter;
                    p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                    p[1].Value = SaupID;
                    p[2] = new SqlParameter("VisitDate", SqlDbType.Char);
                    p[2].Value = VisitDate;

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

        DataTable _TableJaehae;

        public DataTable CODE_Jaehae()
        {
            if(_TableJaehae == null)
            {
                using (SqlConnection con = new SqlConnection(_Connection))
                {
                    lock (lockObject)
                    {
                        _TableJaehae = new DataTable();

                        con.Open();
                        SqlCommand com = con.CreateCommand();
                        com.CommandText = "UP_NBOGUN_JaehaeCodeList";
                        com.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] p = new SqlParameter[0];

                        com.Parameters.AddRange(p);

                        //서버로그 남기기기
                        WriteServerLog(com.CommandText, p);

                        SqlDataAdapter adapter = new SqlDataAdapter(com);
                        try
                        {
                            adapter.Fill(_TableJaehae);
                        }
                        catch (Exception ex)
                        {
                            _Error = ex.Message;
                            LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
                        }
                    }
                }
            }

            return _TableJaehae;
        }

        //상태보고서 첨부 파일 업로드 전 삭제
        public DataTable JaehaejaDataTableAdd(SqlConnection con, SqlTransaction tran, int SaupID, string pJumin, string pJaehaeDate, string pName, string pJaehaeGubun, string pJaehaeGubun1, string pSincheongDate, string pSeunginDate,
            string pDisease, string pCause, string pResult, int pSeqNO, string pUploadFileName, string pResolve, string pReturnDate, string SingoDate
            , string RetireDate, string IsPyeongga)
        {
            DataTable r = new DataTable();

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_JaehaejaDataTableAdd";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[18];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("Jumin", SqlDbType.Char);
            p[2].Value = pJumin;
            p[3] = new SqlParameter("JaehaeDate", SqlDbType.Char);
            p[3].Value = pJaehaeDate;
            p[4] = new SqlParameter("Name", SqlDbType.VarChar);
            p[4].Value = pName;
            p[5] = new SqlParameter("JaehaeGubun", SqlDbType.Char);
            p[5].Value = pJaehaeGubun;
            p[6] = new SqlParameter("JaehaeGubun1", SqlDbType.VarChar);
            p[6].Value = pJaehaeGubun1;
            p[7] = new SqlParameter("SincheongDate", SqlDbType.Char);
            p[7].Value = pSincheongDate;
            p[8] = new SqlParameter("SeunginDate", SqlDbType.Char);
            p[8].Value = pSeunginDate;
            p[9] = new SqlParameter("Disease", SqlDbType.VarChar);
            p[9].Value = pDisease;
            p[10] = new SqlParameter("Cause", SqlDbType.VarChar);
            p[10].Value = pCause;
            p[11] = new SqlParameter("Result", SqlDbType.VarChar);
            p[11].Value = pResult;
            p[12] = new SqlParameter("SeqNO", SqlDbType.Int);
            p[12].Value = pSeqNO;
            p[13] = new SqlParameter("UploadFileName", SqlDbType.VarChar);
            p[13].Value = pUploadFileName;
            p[14] = new SqlParameter("Resolve", SqlDbType.Char);
            p[14].Value = pResolve;
            p[15] = new SqlParameter("ReturnDate", SqlDbType.Char);
            p[15].Value = pReturnDate;
            p[16] = new SqlParameter("SingoDate", SqlDbType.VarChar);
            p[16].Value = SingoDate;
            p[17] = new SqlParameter("RetireDate", SqlDbType.VarChar);
            p[17].Value = RetireDate;
            p[18] = new SqlParameter("IsPyeongga", SqlDbType.VarChar);
            p[18].Value = IsPyeongga;

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
                _Error = ex.Message;

                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }
        public int JaehaejaSave(SqlConnection con, SqlTransaction tran, int SaupID, string pJumin, string pJaehaeDate, string pName, string pJaehaeGubun, string pJaehaeGubun1, string pSincheongDate, string pSeunginDate,
            string pDisease, string pCause, string pResult, int pSeqNO, string pUploadFileName, string pResolve, string pReturnDate, string SingoDate
            , string RetireDate, string IsPyeongga)
        {
            int r = -1;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_JaehaejaAdd";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[19];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("Jumin", SqlDbType.Char);
            p[2].Value = pJumin;
            p[3] = new SqlParameter("JaehaeDate", SqlDbType.Char);
            p[3].Value = pJaehaeDate;
            p[4] = new SqlParameter("Name", SqlDbType.VarChar);
            p[4].Value = pName;
            p[5] = new SqlParameter("JaehaeGubun", SqlDbType.Char);
            p[5].Value = pJaehaeGubun;
            p[6] = new SqlParameter("JaehaeGubun1", SqlDbType.VarChar);
            p[6].Value = pJaehaeGubun1;
            p[7] = new SqlParameter("SincheongDate", SqlDbType.Char);
            p[7].Value = pSincheongDate;
            p[8] = new SqlParameter("SeunginDate", SqlDbType.Char);
            p[8].Value = pSeunginDate;
            p[9] = new SqlParameter("Disease", SqlDbType.VarChar);
            p[9].Value = pDisease;
            p[10] = new SqlParameter("Cause", SqlDbType.VarChar);
            p[10].Value = pCause;
            p[11] = new SqlParameter("Result", SqlDbType.VarChar);
            p[11].Value = pResult;
            p[12] = new SqlParameter("SeqNO", SqlDbType.Int);
            p[12].Value = pSeqNO;
            p[13] = new SqlParameter("UploadFileName", SqlDbType.VarChar);
            p[13].Value = pUploadFileName;
            p[14] = new SqlParameter("Resolve", SqlDbType.VarChar);
            p[14].Value = pResolve;
            p[15] = new SqlParameter("ReturnDate", SqlDbType.Char);
            p[15].Value = pReturnDate;
            p[16] = new SqlParameter("SingoDate", SqlDbType.VarChar);
            p[16].Value = SingoDate;
            p[17] = new SqlParameter("RetireDate", SqlDbType.VarChar);
            p[17].Value = RetireDate;
            p[18] = new SqlParameter("IsPyeongga", SqlDbType.VarChar);
            p[18].Value = IsPyeongga;

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

        public int JaehaejaDel(SqlConnection con, SqlTransaction tran, int SaupID, int pSeqNO, string pYear)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_JaehaejaDel";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("SeqNO", SqlDbType.Int);
            p[2].Value = pSeqNO;
            p[3] = new SqlParameter("Year", SqlDbType.Char);
            p[3].Value = pYear;

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
