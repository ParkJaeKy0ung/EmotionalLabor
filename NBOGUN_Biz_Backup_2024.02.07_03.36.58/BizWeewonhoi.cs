using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace NBOGUN
{
    public partial class Biz
    {
        public DataTable WeewonhoiList(int SaupID, string Yearmon, string Gubun)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_WeewonhoiList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[4];
                    p[0] = new SqlParameter("Center", SqlDbType.Char);
                    p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                    p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
                    p[3] = new SqlParameter("Gubun", SqlDbType.Char);

                    p[0].Value = DHCenter;
                    p[1].Value = SaupID;
                    p[2].Value = Yearmon;
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
                        _Error = ex.Message;
                        LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
                    }
                    return dt;
                }

            }
        }

        public int WeewonhoiDel(SqlConnection con, SqlTransaction tran, int SaupID, string Date)
        {
            int r = -1;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_WeewonhoiDel";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("Date", SqlDbType.Char);
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
                _Error = ex.Message;
                LogManager.WriteLog(ex.Message);
            }

            return r;
        }

        public int WeewonhoiSave(SqlConnection con, SqlTransaction tran, int SaupID, string Date, string Gubun, string Opinion, string TypeGubun, string AttendanceGubun,
            string AttendanceDr, string AttendanceNr, string AttendanceHr, string IsDaesang, string UploadFileName, string Bunki)
        {
            int r = -1;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_WeewonhoiAdd";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[13];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("Date", SqlDbType.Char);
            p[2].Value = Date;
            p[3] = new SqlParameter("Gubun", SqlDbType.Char);
            p[3].Value = Gubun;
            p[4] = new SqlParameter("Opinion", SqlDbType.VarChar);
            p[4].Value = Opinion;
            p[5] = new SqlParameter("TypeGubun", SqlDbType.Char);
            p[5].Value = TypeGubun;
            p[6] = new SqlParameter("AttendanceGubun", SqlDbType.Char);
            p[6].Value = AttendanceGubun;
            p[7] = new SqlParameter("AttendanceDr", SqlDbType.Char);
            p[7].Value = AttendanceDr;
            p[8] = new SqlParameter("AttendanceNr", SqlDbType.Char);
            p[8].Value = AttendanceNr;
            p[9] = new SqlParameter("AttendanceHr", SqlDbType.Char);
            p[9].Value = AttendanceHr;
            p[10] = new SqlParameter("IsDaesang", SqlDbType.Char);
            p[10].Value = IsDaesang;
            p[11] = new SqlParameter("UploadFileName", SqlDbType.VarChar);
            p[11].Value = UploadFileName;
            p[12] = new SqlParameter("Bunki", SqlDbType.VarChar);
            p[12].Value = Bunki;

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
                LogManager.WriteLog(ex.Message);
            }

            return r;
        }
    }
}
