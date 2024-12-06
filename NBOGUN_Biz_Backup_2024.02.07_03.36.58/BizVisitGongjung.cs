using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NBOGUN
{
    public partial class Biz
    {
        //UP_NBOGUN_JakupSaupSave
        public int JakupSaupSave(SqlConnection con, SqlTransaction tran, int SaupID, string VisitDate, string SaupjaName, string SaupDate)
        {
            int r = -1;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_JakupSaupSave";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("VisitDate", SqlDbType.Char);
            p[2].Value = VisitDate;
            p[3] = new SqlParameter("SaupjaName", SqlDbType.VarChar);
            p[3].Value = SaupjaName;
            p[4] = new SqlParameter("SaupDate", SqlDbType.Char);
            p[4].Value = SaupDate;

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
        public DataTable VisitChkjnDateInquire(int SaupID, string VisitDate)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_JakupSaupList";
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

        public DataTable MethodCodeList()
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_HYG_CodeMethodList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[1];
                    p[0] = new SqlParameter("Gubun", SqlDbType.Char);
                    p[0].Value = "";

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

        public DataTable JakupPositionList(int SaupID, string pVisitDate, int pSiteNO, int pGJNO, string Gubun = "1")
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_JakupPositionList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[6];
                    p[0] = new SqlParameter("Center", SqlDbType.Char);
                    p[0].Value = DHCenter;
                    p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                    p[1].Value = SaupID;
                    p[2] = new SqlParameter("VisitDate", SqlDbType.Char);
                    p[2].Value = pVisitDate;
                    p[3] = new SqlParameter("SiteNO", SqlDbType.Int);
                    p[3].Value = pSiteNO;
                    p[4] = new SqlParameter("GJNO", SqlDbType.Int);
                    p[4].Value = pGJNO;
                    p[5] = new SqlParameter("Gubun", SqlDbType.Char);
                    p[5].Value = Gubun;

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

        public DataTable JakupSayongList(int SaupID, string pVisitDate, int pSiteNO, int pGJNO)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_JakupSayongList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[5];
                    p[0] = new SqlParameter("Center", SqlDbType.Char);
                    p[0].Value = DHCenter;
                    p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                    p[1].Value = SaupID;
                    p[2] = new SqlParameter("VisitDate", SqlDbType.Char);
                    p[2].Value = pVisitDate;
                    p[3] = new SqlParameter("SiteNO", SqlDbType.Int);
                    p[3].Value = pSiteNO;
                    p[4] = new SqlParameter("GJNO", SqlDbType.Int);
                    p[4].Value = pGJNO;

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

        public DataTable JakupResultList(int SaupID, string pVisitDate, int pSiteNO, int pGJNO, int pPosNO)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_JakupResultList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[6];
                    p[0] = new SqlParameter("Center", SqlDbType.Char);
                    p[0].Value = DHCenter;
                    p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                    p[1].Value = SaupID;
                    p[2] = new SqlParameter("VisitDate", SqlDbType.Char);
                    p[2].Value = pVisitDate;
                    p[3] = new SqlParameter("SiteNO", SqlDbType.Int);
                    p[3].Value = pSiteNO;
                    p[4] = new SqlParameter("GJNO", SqlDbType.Int);
                    p[4].Value = pGJNO;
                    p[5] = new SqlParameter("PosNO", SqlDbType.Int);
                    p[5].Value = pPosNO;

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

        public DataTable JakupSiteAdd(SqlConnection con, SqlTransaction tran, int SaupID, string VisitDate, int pSiteNO, int pOdx, string pSiteName, string pSiteMemo, string pIsVisible)
        {
            DataTable dt = new DataTable();

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_JakupSiteAdd";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[9];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("VisitDate", SqlDbType.Char);
            p[2].Value = VisitDate;
            p[3] = new SqlParameter("SiteNO", SqlDbType.Int);
            p[3].Value = pSiteNO;
            p[4] = new SqlParameter("Odx", SqlDbType.Int);
            p[4].Value = pOdx;
            p[5] = new SqlParameter("SiteName", SqlDbType.VarChar);
            p[5].Value = pSiteName;
            p[6] = new SqlParameter("SiteMemo", SqlDbType.VarChar);
            p[6].Value = pSiteMemo;
            p[7] = new SqlParameter("UserID", SqlDbType.Char);
            p[7].Value = Biz.Instance.UserID;
            p[8] = new SqlParameter("IsVisible", SqlDbType.Char);
            p[8].Value = pIsVisible;

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

        public int JakupSiteDel(SqlConnection con, SqlTransaction tran, int SaupID, string VisitDate, int pSiteNO)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_JakupSiteDel";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("VisitDate", SqlDbType.Char);
            p[2].Value = VisitDate;
            p[3] = new SqlParameter("SiteNO", SqlDbType.Int);
            p[3].Value = pSiteNO;

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
                LogManager.WriteLog(ex.Message);
            }

            return r;
        }

        public DataTable JakupSiteJidoCntList(int SaupID, string VisitDate, int SiteNO)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_JakupSiteJidoCntList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[4];
                    p[0] = new SqlParameter("Center", SqlDbType.Char);
                    p[0].Value = DHCenter;
                    p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                    p[1].Value = SaupID;
                    p[2] = new SqlParameter("VisitDate", SqlDbType.Char);
                    p[2].Value = VisitDate;
                    p[3] = new SqlParameter("SiteNO", SqlDbType.Int);
                    p[3].Value = SiteNO;

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

        public int VisitBohoguDel(SqlConnection con, SqlTransaction tran, int SaupID, string VisitDate, string Visitor, int SiteNO, int GJNO)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_VisitJakupEnvBohoguDel";
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

            r = com.ExecuteNonQuery();

            return r;
        }

        public int VisitBohoguSave(SqlConnection con, SqlTransaction tran, int SaupID, string pVisitDate, string pVisitor, int pSiteNO, int pGJNO, string pCode, int pPosNO, int pCnt, string pIsSA, string pGubun1, string pGubun2, string pGubun3,
            string pChange, int pSeqNO, string pPassNO)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_VisitJakupEnvBohoguAdd";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[16];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("VisitDate", SqlDbType.Char);
            p[2].Value = pVisitDate;
            p[3] = new SqlParameter("Visitor", SqlDbType.Char);
            p[3].Value = pVisitor;
            p[4] = new SqlParameter("SiteNO", SqlDbType.Int);
            p[4].Value = pSiteNO;
            p[5] = new SqlParameter("GJNO", SqlDbType.Int);
            p[5].Value = pGJNO;
            p[6] = new SqlParameter("Code", SqlDbType.Char);
            p[6].Value = pCode;
            p[7] = new SqlParameter("PosNO", SqlDbType.Int);
            p[7].Value = pPosNO;
            p[8] = new SqlParameter("Cnt", SqlDbType.Int);
            p[8].Value = pCnt;
            p[9] = new SqlParameter("IsSA", SqlDbType.Char);
            p[9].Value = pIsSA;
            p[10] = new SqlParameter("Gubun1", SqlDbType.VarChar);
            p[10].Value = pGubun1;
            p[11] = new SqlParameter("Gubun2", SqlDbType.VarChar);
            p[11].Value = pGubun2;
            p[12] = new SqlParameter("Gubun3", SqlDbType.VarChar);
            p[12].Value = pGubun3;
            p[13] = new SqlParameter("Change", SqlDbType.VarChar);
            p[13].Value = pChange;
            p[14] = new SqlParameter("SeqNO", SqlDbType.Int);
            p[14].Value = pSeqNO;
            p[15] = new SqlParameter("PassNO", SqlDbType.VarChar);
            p[15].Value = pPassNO;

            com.Parameters.AddRange(p);

            //서버로그 남기기기
            WriteServerLog(com.CommandText, p);

            r = com.ExecuteNonQuery();

            return r;
        }

        public DataTable VisitBohoguList(int SaupID, string VisitDate, int SiteNO)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_VisitBohoguList";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[4];
                    p[0] = new SqlParameter("Center", SqlDbType.Char);
                    p[0].Value = DHCenter;
                    p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                    p[1].Value = SaupID;
                    p[2] = new SqlParameter("VisitDate", SqlDbType.Char);
                    p[2].Value = VisitDate;
                    p[3] = new SqlParameter("SiteNO", SqlDbType.Int);
                    p[3].Value = SiteNO;

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

        public DataTable JakupSiteList(int SaupID, string VisitDate)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_JakupSiteList";
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
        //UP_NBOGUN_JakupGongjungList
        public DataTable JakupGongjungList(int SaupID, string VisitDate, int SiteNO, string Gubun)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_JakupGongjungList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[5];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = DHCenter;
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[1].Value = SaupID;
                p[2] = new SqlParameter("VisitDate", SqlDbType.Char);
                p[2].Value = VisitDate;
                p[3] = new SqlParameter("SiteNO", SqlDbType.Int);
                p[3].Value = SiteNO;
                p[4] = new SqlParameter("Gubun", SqlDbType.Char);
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

        public DataTable JakupResultForVisitDate(int SaupID, string VisitDate)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_JakupResultForVisitDate";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter("Center", SqlDbType.Char);
                para[0].Value = DHCenter;
                para[1] = new SqlParameter("SaupID", SqlDbType.Int);
                para[1].Value = SaupID;
                para[2] = new SqlParameter("VisitDate", SqlDbType.Char);
                para[2].Value = VisitDate;

                com.Parameters.AddRange(para);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, para);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable JakupSayongForVisitDate(int SaupID, string VisitDate)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_JakupSayongListForVisitDate";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] para = new SqlParameter[3];
                para[0] = new SqlParameter("Center", SqlDbType.Char);
                para[0].Value = DHCenter;
                para[1] = new SqlParameter("SaupID", SqlDbType.Int);
                para[1].Value = SaupID;
                para[2] = new SqlParameter("VisitDate", SqlDbType.Char);
                para[2].Value = VisitDate;

                com.Parameters.AddRange(para);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, para);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable RPT_JakupCheck(int SaupID, string VisitDate, string Visitor, string Gubun)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_RPT_JakupCheck";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] para = new SqlParameter[5];
                para[0] = new SqlParameter("Center", SqlDbType.Char);
                para[0].Value = DHCenter;
                para[1] = new SqlParameter("SaupID", SqlDbType.Int);
                para[1].Value = SaupID;
                para[2] = new SqlParameter("VisitDate", SqlDbType.Char);
                para[2].Value = VisitDate;
                para[3] = new SqlParameter("Visitor", SqlDbType.Char);
                para[3].Value = Visitor;
                para[4] = new SqlParameter("Gubun", SqlDbType.Char);
                para[4].Value = Gubun;

                com.Parameters.AddRange(para);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, para);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable RPT_JakupCheckList(int SaupID, string VisitDate, string Visitor)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_RPT_JakupCheckList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] para = new SqlParameter[4];
                para[0] = new SqlParameter("Center", SqlDbType.Char);
                para[0].Value = DHCenter;
                para[1] = new SqlParameter("SaupID", SqlDbType.Int);
                para[1].Value = SaupID;
                para[2] = new SqlParameter("VisitDate", SqlDbType.Char);
                para[2].Value = VisitDate;
                para[3] = new SqlParameter("Visitor", SqlDbType.Char);
                para[3].Value = Visitor;

                com.Parameters.AddRange(para);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, para);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }

        public int RPT_JakupCheckDel(SqlConnection con, SqlTransaction tran, int SaupID, string VisitDate, string Visitor)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_RPT_JakupCheckDel";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("VisitDate", SqlDbType.Char);
            p[2].Value = VisitDate;
            p[3] = new SqlParameter("Visitor", SqlDbType.Char);
            p[3].Value = Visitor;

            com.Parameters.AddRange(p);

            //서버로그 남기기기
            WriteServerLog(com.CommandText, p);

            r = com.ExecuteNonQuery();

            return r;
        }

        public int RPT_JakupCheckSave(SqlConnection con, SqlTransaction tran, int SaupID, string VisitDate, string Visitor, int SiteNO, int GJNO, string GJName, string CodeGroup,
            string Name, string Result, string TableName, string Code)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_RPT_JakupCheckSave";
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
            p[6] = new SqlParameter("GJName", SqlDbType.VarChar);
            p[6].Value = GJName;
            p[7] = new SqlParameter("CodeGroup", SqlDbType.VarChar);
            p[7].Value = CodeGroup;
            p[8] = new SqlParameter("Name", SqlDbType.VarChar);
            p[8].Value = Name;
            p[9] = new SqlParameter("Result", SqlDbType.VarChar);
            p[9].Value = Result;
            p[10] = new SqlParameter("TableName", SqlDbType.VarChar);
            p[10].Value = TableName;
            p[11] = new SqlParameter("Code", SqlDbType.VarChar);
            p[11].Value = Code;

            com.Parameters.AddRange(p);

            //서버로그 남기기기
            WriteServerLog(com.CommandText, p);

            r = com.ExecuteNonQuery();

            return r;
        }
    }
}
