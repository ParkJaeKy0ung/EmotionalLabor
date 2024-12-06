using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Policy;
using Telerik.WinControls.Extensions;

namespace NBOGUN
{
    partial class Biz
    {
        public DataTable UP_NBOGUN_VisitJakupEnvSelbiList(int SaupID, string VisitDate, string Visitor, int SiteNO, int GJNO)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_VisitJakupEnvSelbiList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[6];
                    p[0] = new SqlParameter("Center", SqlDbType.Char);
                    p[0].Value = DHCenter;
                    p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                    p[1].Value = SaupID;
                    p[2] = new SqlParameter("VisitDate", SqlDbType.Char);
                    p[2].Value = VisitDate;
                    p[3] = new SqlParameter("Visitor", SqlDbType.Char);
                    p[3].Value = Visitor;
                    p[4] = new SqlParameter("SiteNO", SqlDbType.Int);
                    p[4].Value = SiteNO;
                    p[5] = new SqlParameter("GJNO", SqlDbType.Int);
                    p[5].Value = GJNO;

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

        public int VisitJakupEnvSelbiDel(SqlConnection con, SqlTransaction tran, int SaupID, string VisitDate, string Visitor, int SiteNO, int GJNO)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_VisitJakupEnvSelbiDel";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[6];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("VisitDate", SqlDbType.Char);
            p[2].Value = VisitDate;
            p[3] = new SqlParameter("Visitor", SqlDbType.Char);
            p[3].Value = Visitor;
            p[4] = new SqlParameter("SiteNO", SqlDbType.Int);
            p[4].Value = SiteNO;
            p[5] = new SqlParameter("GJNO", SqlDbType.Int);
            p[5].Value = GJNO;

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


        public int VisitJakupEnvSelbiSave(SqlConnection con, SqlTransaction tran, int SaupID, string VisitDate, string Visitor, int SiteNO, int GJNO, int SeqNO,
            string SeolbiName, string IsDaesang, string IsExcept, string GumsaKikwan, string GumsaDate)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_VisitJakupEnvSelbiAdd";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[12];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("VisitDate", SqlDbType.Char);
            p[2].Value = VisitDate;
            p[3] = new SqlParameter("Visitor", SqlDbType.Char);
            p[3].Value = Visitor;
            p[4] = new SqlParameter("SiteNO", SqlDbType.Int);
            p[4].Value = SiteNO;
            p[5] = new SqlParameter("GJNO", SqlDbType.Int);
            p[5].Value = GJNO;
            p[6] = new SqlParameter("SeqNO", SqlDbType.Int);
            p[6].Value = SeqNO;
            p[7] = new SqlParameter("SeolbiName", SqlDbType.VarChar);
            p[7].Value = SeolbiName;
            p[8] = new SqlParameter("IsDaesang", SqlDbType.Char);
            p[8].Value = IsDaesang;
            p[9] = new SqlParameter("IsExcept", SqlDbType.Char);
            p[9].Value = IsExcept;
            p[10] = new SqlParameter("GumsaKikwan", SqlDbType.VarChar);
            p[10].Value = GumsaKikwan;
            p[11] = new SqlParameter("GumsaDate", SqlDbType.VarChar);
            p[11].Value = GumsaDate;

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
