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
        public int SangdamHealth1Add(SqlConnection con, SqlTransaction tran,int SaupID, string pVisitDate, string pVisitor, string pProgram, string pSDate, string pEDate,
            int Odx = -1, string KikwanName = "", string KikwanIdxs = "", string SeqNOs = "", int Inwon = 0, string Subject = "")
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_AnjunBogunEducationDel";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[13];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("VisitDate", SqlDbType.Char);
            p[2].Value = pVisitDate;
            p[3] = new SqlParameter("Visitor", SqlDbType.Char);
            p[3].Value = pVisitor;
            p[4] = new SqlParameter("Program", SqlDbType.VarChar);
            p[4].Value = pProgram;
            p[5] = new SqlParameter("SDate", SqlDbType.VarChar);
            p[5].Value = pSDate;
            p[6] = new SqlParameter("EDate", SqlDbType.VarChar);
            p[6].Value = pEDate;
            p[7] = new SqlParameter("Odx", SqlDbType.Int);
            p[7].Value = Odx;
            p[8] = new SqlParameter("KikwanName", SqlDbType.VarChar);
            p[8].Value = KikwanName;
            p[9] = new SqlParameter("KikwanIdxs", SqlDbType.VarChar);
            p[9].Value = KikwanIdxs;
            p[10] = new SqlParameter("SeqNOs", SqlDbType.VarChar);
            p[10].Value = SeqNOs;
            p[11] = new SqlParameter("Inwon", SqlDbType.Int);
            p[11].Value = Inwon;
            p[12] = new SqlParameter("Subject", SqlDbType.VarChar);
            p[12].Value = Subject;

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

        public DataTable SangdamHealthList(int SaupID, string pVisitDate, string pVisitor, string Gubun)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_SangdamHealthList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[5];
                    p[0] = new SqlParameter("Center", SqlDbType.Char);
                    p[0].Value = DHCenter;
                    p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                    p[1].Value = SaupID;
                    p[2] = new SqlParameter("VisitDate", SqlDbType.VarChar);
                    p[2].Value = pVisitDate;
                    p[3] = new SqlParameter("Visitor", SqlDbType.VarChar);
                    p[3].Value = pVisitor;
                    p[4] = new SqlParameter("Gubun", SqlDbType.VarChar);
                    p[4].Value = Gubun;

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
    }
}
