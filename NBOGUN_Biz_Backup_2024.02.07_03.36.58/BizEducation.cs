using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Web.UI.WebControls;

namespace NBOGUN
{
    partial class Biz
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SaupID"></param>
        /// <param name="VisitDate"></param>
        /// <param name="Visitor"></param>
        /// <param name="Gubun">"1" : 둘다, "2" : 3개년, "3" : 방문일</param>
        /// <returns></returns>
        public DataSet AnjunBogunEducationList(int SaupID, string VisitDate, string Visitor, string Gubun)
        {
            DataSet dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataSet();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_AnjunBogunEducationList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[5];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = DHCenter;
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[1].Value = SaupID;
                p[2] = new SqlParameter("VisitDate", SqlDbType.VarChar);
                p[2].Value = VisitDate;
                p[3] = new SqlParameter("Visitor", SqlDbType.VarChar);
                p[3].Value = Visitor;
                p[4] = new SqlParameter("Gubun", SqlDbType.Char);
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
                    _Error = ex.Message;
                    LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
                }

                return dt;
            }
        }

        public DataSet AnjunBogunEducationItemList(int SaupID, string VisitDate, string Visitor, string Gubun = "1")
        {
            DataSet dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataSet();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_AnjunBogunEducationItemList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[5];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = DHCenter;
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[1].Value = SaupID;
                p[2] = new SqlParameter("VisitDate", SqlDbType.VarChar);
                p[2].Value = VisitDate;
                p[3] = new SqlParameter("Visitor", SqlDbType.VarChar);
                p[3].Value = Visitor;
                p[4] = new SqlParameter("Gubun", SqlDbType.Char);
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
                    LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
                }

                return dt;
            }
        }

        /// <summary>
        /// 교육실시내역 삭제
        /// </summary>
        /// <param name="con"></param>
        /// <param name="tran"></param>
        /// <param name="SaupID"></param>
        /// <param name="VisitDate"></param>
        /// <param name="Visitor"></param>
        /// <param name="Gubun">2019-05-17	'1'방문일만 지우고 '2' 는 당해연도 전체 삭제</param>
        /// <returns></returns>
        public int AnjunBogunEducationDel(SqlConnection con, SqlTransaction tran, int SaupID, string VisitDate, string Visitor, string Gubun)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_AnjunBogunEducationDel";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("VisitDate", SqlDbType.VarChar);
            p[2].Value = VisitDate;
            p[3] = new SqlParameter("Visitor", SqlDbType.VarChar);
            p[3].Value = Visitor;
            p[4] = new SqlParameter("Gubun", SqlDbType.Char);
            p[4].Value = Gubun;

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

        public int AnjunBogunEducationSave(SqlConnection con, SqlTransaction tran, int SaupID, string pEduDate, int pOdx, string pEduType, string pEduBangbup, string pEduSize, string pContent, int pDaesangInwon,
            int pAttandInwon, string pLocation, string pSilsija, string pVisitDate, string pVisitor, string pIsSilsi, string EduEndDate)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_AnjunBogunEducationAdd";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[16];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("EduDate", SqlDbType.Char);
            p[2].Value = pEduDate;
            p[3] = new SqlParameter("Odx", SqlDbType.Int);
            p[3].Value = pOdx;
            p[4] = new SqlParameter("EduType", SqlDbType.Char);
            p[4].Value = pEduType;
            p[5] = new SqlParameter("EduBangbup", SqlDbType.Char);
            p[5].Value = pEduBangbup;
            p[6] = new SqlParameter("EduSize", SqlDbType.Char);
            p[6].Value = pEduSize;
            p[7] = new SqlParameter("Content", SqlDbType.VarChar);
            p[7].Value = pContent;
            p[8] = new SqlParameter("DaesangInwon", SqlDbType.Int);
            p[8].Value = pDaesangInwon;
            p[9] = new SqlParameter("AttandInwon", SqlDbType.Int);
            p[9].Value = pAttandInwon;
            p[10] = new SqlParameter("Location", SqlDbType.VarChar);
            p[10].Value = pLocation;
            p[11] = new SqlParameter("Silsija", SqlDbType.VarChar);
            p[11].Value = pSilsija;
            p[12] = new SqlParameter("VisitDate", SqlDbType.VarChar);
            p[12].Value = pVisitDate;
            p[13] = new SqlParameter("Visitor", SqlDbType.VarChar);
            p[13].Value = pVisitor;
            p[14] = new SqlParameter("IsSilsi", SqlDbType.VarChar);
            p[14].Value = pIsSilsi;
            p[15] = new SqlParameter("EduEndDate", SqlDbType.VarChar);
            p[15].Value = EduEndDate;

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

        public int AnjunBogunEducationItemDel(SqlConnection con, SqlTransaction tran,int SaupID, string Year)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_AnjunBogunEducationItemDel";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = Center;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("VisitDate", SqlDbType.VarChar);
            p[2].Value = Year;
            p[3] = new SqlParameter("Visitor", SqlDbType.VarChar);
            p[3].Value = "";

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

        public int AnjunBogunEducationItemSave(SqlConnection con, SqlTransaction tran, int SaupID, string pItemDate, int pOdx, string pGubun, string pItemName, string pItemCode, int pCnt,
            string pVisitDate, string pVisitor, string pItemType, string IsUsage)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_AnjunBogunEducationItemAdd";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[12];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = Center;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("ItemDate", SqlDbType.Char);
            p[2].Value = pItemDate;
            p[3] = new SqlParameter("Odx", SqlDbType.Int);
            p[3].Value = pOdx;
            p[4] = new SqlParameter("Gubun", SqlDbType.Char);
            p[4].Value = pGubun;
            p[5] = new SqlParameter("ItemName", SqlDbType.Char);
            p[5].Value = pItemName;
            p[6] = new SqlParameter("ItemCode", SqlDbType.Char);
            p[6].Value = pItemCode;
            p[7] = new SqlParameter("Cnt", SqlDbType.Int);
            p[7].Value = pCnt;
            p[8] = new SqlParameter("VisitDate", SqlDbType.VarChar);
            p[8].Value = pVisitDate;
            p[9] = new SqlParameter("Visitor", SqlDbType.VarChar);
            p[9].Value = pVisitor;
            p[10] = new SqlParameter("ItemType", SqlDbType.VarChar);
            p[10].Value = pItemType;
            p[11] = new SqlParameter("IsUsage", SqlDbType.VarChar);
            p[11].Value = IsUsage;

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

