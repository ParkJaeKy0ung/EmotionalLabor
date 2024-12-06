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
        public int RPT_ItemDel(SqlConnection con, SqlTransaction tran, string Yearmon)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_RPT_ItemDel";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("Yearmon", SqlDbType.Char);
            p[1].Value = Yearmon;
            p[2] = new SqlParameter("UserID", SqlDbType.Char);
            p[2].Value = UserID;

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
        public int RPT_ItemSave(SqlConnection con, SqlTransaction tran, string Yearmon, int SaupID, string SaupjaNum)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_RPT_ItemAdd";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("Yearmon", SqlDbType.Char);
            p[1].Value = Yearmon;
            p[2] = new SqlParameter("SaupID", SqlDbType.Int);
            p[2].Value = SaupID;
            p[3] = new SqlParameter("SaupjaNum", SqlDbType.Char);
            p[3].Value = SaupjaNum;
            p[4] = new SqlParameter("UserID", SqlDbType.Char);
            p[4].Value = UserID;

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
