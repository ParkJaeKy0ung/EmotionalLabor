using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBOGUN
{
    partial class Biz
    {
        /// <summary>
        /// 문자 보내기
        /// </summary>
        /// <param name="senddate">발송시간 (yyyy-MM-dd hh:mm:ss)</param>
        /// <param name="phone">문자 받는 연락처</param>
        /// <param name="callbackphone">문자 보내는 연락처</param>
        /// <param name="subject">제목</param>
        /// <param name="MSG">메세지</param>
        /// <param name="saup">DH</param>
        /// <param name="catrgoryindex">1</param>
        /// <param name="bigo"></param>
        /// <returns></returns>
        public int MSG_Send(string senddate, string phone, string callbackphone, string subject, string MSG, string saup = "DH", string catrgoryindex = "1", string bigo = "")
        {
            using (SqlConnection con = new SqlConnection(_Connection))
            {
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "[external].[dbo].[UP_MSG_Send]";
                com.CommandType = CommandType.StoredProcedure;

                System.Data.SqlClient.SqlParameter[] param ={
                    new  System.Data.SqlClient.SqlParameter("@SUBJECT", System.Data.SqlDbType.VarChar),
                    new  System.Data.SqlClient.SqlParameter("@SENDDATE", System.Data.SqlDbType.VarChar),
                    new  System.Data.SqlClient.SqlParameter("@PHONE", System.Data.SqlDbType.VarChar),
                    new  System.Data.SqlClient.SqlParameter("@CALLBACKPHONE", System.Data.SqlDbType.VarChar),
                    new  System.Data.SqlClient.SqlParameter("@MESSAGE", System.Data.SqlDbType.VarChar),
                    new  System.Data.SqlClient.SqlParameter("@CENTER", System.Data.SqlDbType.Char),
                    new  System.Data.SqlClient.SqlParameter("@BUSEO", System.Data.SqlDbType.Char),
                    new  System.Data.SqlClient.SqlParameter("@SABUN", System.Data.SqlDbType.Char),
                    new  System.Data.SqlClient.SqlParameter("@SAUPGUBUN", System.Data.SqlDbType.Char),
                    new  System.Data.SqlClient.SqlParameter("@CategoryIndex", System.Data.SqlDbType.VarChar),
                    new  System.Data.SqlClient.SqlParameter("@Bigo", System.Data.SqlDbType.VarChar),
                };

                param[0].Value = subject;
                param[1].Value = senddate;
                param[2].Value = phone;
                param[3].Value = callbackphone;
                param[4].Value = MSG;
                param[5].Value = _DHCenter;
                param[6].Value = _BuseoSection;
                param[7].Value = _UserID;
                param[8].Value = saup;
                param[9].Value = catrgoryindex;
                param[10].Value = bigo;

                com.Parameters.AddRange(param);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, param);

                int r = com.ExecuteNonQuery();

                return r;
            }
        }
    }
}
