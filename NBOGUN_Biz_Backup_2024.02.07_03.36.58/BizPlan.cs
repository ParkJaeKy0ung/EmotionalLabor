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
        public int PlanEduItemAdd(SqlConnection con, SqlTransaction tran, int SaupID, string SincheongYearmon, string Yearmon, string Code, string ItemType, string Name)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_PlanEduItemAdd";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[7];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = _DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("SincheongYearmon", SqlDbType.Char);
            p[2].Value = SincheongYearmon;
            p[3] = new SqlParameter("Yearmon", SqlDbType.Char);
            p[3].Value = Yearmon;
            p[4] = new SqlParameter("Code", SqlDbType.VarChar);
            p[4].Value = Code;
            p[5] = new SqlParameter("ItemType", SqlDbType.VarChar);
            p[5].Value = ItemType;
            p[6] = new SqlParameter("Name", SqlDbType.VarChar);
            p[6].Value = Name;

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
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        //UP_NBOGUN_PlanEduItemDel
        public int PlanEduItemDel(SqlConnection con, SqlTransaction tran, int SaupID, string SincheongYearmon)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_PlanEduItemDel";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = _DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("SincheongYearmon", SqlDbType.Char);
            p[2].Value = SincheongYearmon;

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
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }
        public int PlanEduSilsiAdd(SqlConnection con, SqlTransaction tran, int SaupID, string SincheongYearmon, string Yearmon, string Gubun, string Subject, string EduBangbup, string Teacher)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_PlanEduSilsiIdxAdd";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[8];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = _DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("SincheongYearmon", SqlDbType.Char);
            p[2].Value = SincheongYearmon;
            p[3] = new SqlParameter("Yearmon", SqlDbType.Char);
            p[3].Value = Yearmon;
            p[4] = new SqlParameter("Gubun", SqlDbType.VarChar);
            p[4].Value = Gubun;
            p[5] = new SqlParameter("Subject", SqlDbType.VarChar);
            p[5].Value = Subject;
            p[6] = new SqlParameter("EduBangbup", SqlDbType.VarChar);
            p[6].Value = EduBangbup;
            p[7] = new SqlParameter("Teacher", SqlDbType.VarChar);
            p[7].Value = Teacher;

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
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public int PlanTargetAdd(SqlConnection con, SqlTransaction tran, int SaupID, string SincheongYearmon, int Odx, string Code, string Name, string Target)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_PlanTargetAdd";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[7];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = _DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
            p[2].Value = SincheongYearmon;
            p[3] = new SqlParameter("Odx", SqlDbType.Int);
            p[3].Value = Odx;
            p[4] = new SqlParameter("Code", SqlDbType.Char);
            p[4].Value = Code;
            p[5] = new SqlParameter("Name", SqlDbType.VarChar);
            p[5].Value = Name;
            p[6] = new SqlParameter("Target", SqlDbType.VarChar);
            p[6].Value = Target;

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
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }
            

            return r;
        }

        public int PlanTargetDel(SqlConnection con, SqlTransaction tran, int SaupID, string SincheongYearmon)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_PlanTargetDel";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = _DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
            p[2].Value = SincheongYearmon;

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
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }
            

            return r;
        }

        public int PlanVisitAdd(SqlConnection con, SqlTransaction tran,int SaupID, string SincheongYearmon, string DHJikjong, string M01, string M02, string M03, 
            string M04, string M05, string M06, string M07, string M08, string M09, string M10, string M11, string M12)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_PlanVisitAdd";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[16];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = _DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("SincheongYearmon", SqlDbType.Char);
            p[2].Value = SincheongYearmon;
            p[3] = new SqlParameter("DHJikjong", SqlDbType.Char);
            p[3].Value = DHJikjong;
            p[4] = new SqlParameter("M01", SqlDbType.Char);
            p[4].Value = M01;
            p[5] = new SqlParameter("M02", SqlDbType.Char);
            p[5].Value = M02;
            p[6] = new SqlParameter("M03", SqlDbType.Char);
            p[6].Value = M03;
            p[7] = new SqlParameter("M04", SqlDbType.Char);
            p[7].Value = M04;
            p[8] = new SqlParameter("M05", SqlDbType.Char);
            p[8].Value = M05;
            p[9] = new SqlParameter("M06", SqlDbType.Char);
            p[9].Value = M06;
            p[10] = new SqlParameter("M07", SqlDbType.Char);
            p[10].Value = M07;
            p[11] = new SqlParameter("M08", SqlDbType.Char);
            p[11].Value = M08;
            p[12] = new SqlParameter("M09", SqlDbType.Char);
            p[12].Value = M09;
            p[13] = new SqlParameter("M10", SqlDbType.Char);
            p[13].Value = M10;
            p[14] = new SqlParameter("M11", SqlDbType.Char);
            p[14].Value = M11;
            p[15] = new SqlParameter("M12", SqlDbType.Char);
            p[15].Value = M12;

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
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public int PlanUpmuAdd(SqlConnection con, SqlTransaction tran, int SaupID, string SincheongYearmon, string DHJikjong, string Code,
            string IsDaesang, string M01, string M02, string M03, string M04, string M05, string M06, string M07, string M08, string M09, string M10, string M11, string M12, string Bigo)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_PlanUpmuAdd";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[19];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = _DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("SincheongYearmon", SqlDbType.Char);
            p[2].Value = SincheongYearmon;
            p[3] = new SqlParameter("DHJikjong", SqlDbType.Char);
            p[3].Value = DHJikjong;
            p[4] = new SqlParameter("Code", SqlDbType.Char);
            p[4].Value = Code;
            p[5] = new SqlParameter("IsDaesang", SqlDbType.VarChar);
            p[5].Value = IsDaesang;
            p[6] = new SqlParameter("M01", SqlDbType.Char);
            p[6].Value = M01;
            p[7] = new SqlParameter("M02", SqlDbType.Char);
            p[7].Value = M02;
            p[8] = new SqlParameter("M03", SqlDbType.Char);
            p[8].Value = M03;
            p[9] = new SqlParameter("M04", SqlDbType.Char);
            p[9].Value = M04;
            p[10] = new SqlParameter("M05", SqlDbType.Char);
            p[10].Value = M05;
            p[11] = new SqlParameter("M06", SqlDbType.Char);
            p[11].Value = M06;
            p[12] = new SqlParameter("M07", SqlDbType.Char);
            p[12].Value = M07;
            p[13] = new SqlParameter("M08", SqlDbType.Char);
            p[13].Value = M08;
            p[14] = new SqlParameter("M09", SqlDbType.Char);
            p[14].Value = M09;
            p[15] = new SqlParameter("M10", SqlDbType.Char);
            p[15].Value = M10;
            p[16] = new SqlParameter("M11", SqlDbType.Char);
            p[16].Value = M11;
            p[17] = new SqlParameter("M12", SqlDbType.Char);
            p[17].Value = M12;
            p[18] = new SqlParameter("Bigo", SqlDbType.VarChar);
            p[18].Value = Bigo;

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
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public int Plan00STANDAdd(SqlConnection con, SqlTransaction tran, int SaupID, string pYearmon, string pSTANDDate, string pSTANDTarget, string pSTANDPerson1,
            string pSTANDPerson2, string pSTANDPerson3, string pSTANDPerson4, string pSTANDPerson5, string pSTANDPerson6,
            string pSTANDPerson7, string pSTANDBunki1, string pSTANDBunki2, string pSTANDBunki3, string pSTANDBunki4,
            string pSTANDIMonth, string pSTANDIInwon, string pSTANDTMonth, string pSTANDTInwon, string STANDUpmu01, string STANDUpmu02, string STANDUpmu03,
            string STANDUpmu04, string STANDUpmu05, string STANDUpmu06, string STANDUpmu07, string STANDUpmu08, string STANDUpmu09, string STANDUpmu10, string STANDUpmu11
            , string STANDUpmu12, string STANDUpmu13, string STANDUpmu14, string STANDUpmu15, string STANDUpmu16, string STANDUpmu17, string STANDUpmu18, string STANDUpmu19
            , string STANDUpmu20, string STANDUpmu21, string STANDUpmu22, string STANDUpmu23, string STANDUpmu24, string STANDUpmu25, string STANDUpmu26, string STANDUpmu27
            , string STANDUpmu28, string STANDUpmu29, string STANDUpmu30, string STANDUpmu31, string STANDUpmu32, string STANDUpmu33,
            string STANDGubun)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_Plan00STANDAdd";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[54];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = _DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
            p[2].Value = pYearmon;
            p[3] = new SqlParameter("STANDDate", SqlDbType.VarChar);
            p[3].Value = pSTANDDate;
            p[4] = new SqlParameter("STANDTarget", SqlDbType.VarChar);
            p[4].Value = pSTANDTarget;
            p[5] = new SqlParameter("STANDPerson1", SqlDbType.VarChar);
            p[5].Value = pSTANDPerson1;
            p[6] = new SqlParameter("STANDPerson2", SqlDbType.VarChar);
            p[6].Value = pSTANDPerson2;
            p[7] = new SqlParameter("STANDPerson3", SqlDbType.VarChar);
            p[7].Value = pSTANDPerson3;
            p[8] = new SqlParameter("STANDPerson4", SqlDbType.VarChar);
            p[8].Value = pSTANDPerson4;
            p[9] = new SqlParameter("STANDPerson5", SqlDbType.VarChar);
            p[9].Value = pSTANDPerson5;
            p[10] = new SqlParameter("STANDPerson6", SqlDbType.VarChar);
            p[10].Value = pSTANDPerson6;
            p[11] = new SqlParameter("STANDPerson7", SqlDbType.VarChar);
            p[11].Value = pSTANDPerson7;
            p[12] = new SqlParameter("STANDBunki1", SqlDbType.VarChar);
            p[12].Value = pSTANDBunki1;
            p[13] = new SqlParameter("STANDBunki2", SqlDbType.VarChar);
            p[13].Value = pSTANDBunki2;
            p[14] = new SqlParameter("STANDBunki3", SqlDbType.VarChar);
            p[14].Value = pSTANDBunki3;
            p[15] = new SqlParameter("STANDBunki4", SqlDbType.VarChar);
            p[15].Value = pSTANDBunki4;
            p[16] = new SqlParameter("STANDIMonth", SqlDbType.VarChar);
            p[16].Value = pSTANDIMonth;
            p[17] = new SqlParameter("STANDIInwon", SqlDbType.VarChar);
            p[17].Value = pSTANDIInwon;
            p[18] = new SqlParameter("STANDTMonth", SqlDbType.VarChar);
            p[18].Value = pSTANDTMonth;
            p[19] = new SqlParameter("STANDTInwon", SqlDbType.VarChar);
            p[19].Value = pSTANDTInwon;
            p[20] = new SqlParameter("STANDUpmu01", SqlDbType.VarChar);
            p[20].Value = STANDUpmu01;
            p[21] = new SqlParameter("STANDUpmu02", SqlDbType.VarChar);
            p[21].Value = STANDUpmu02;
            p[22] = new SqlParameter("STANDUpmu03", SqlDbType.VarChar);
            p[22].Value = STANDUpmu03;
            p[23] = new SqlParameter("STANDUpmu04", SqlDbType.VarChar);
            p[23].Value = STANDUpmu04;
            p[24] = new SqlParameter("STANDUpmu05", SqlDbType.VarChar);
            p[24].Value = STANDUpmu05;
            p[25] = new SqlParameter("STANDUpmu06", SqlDbType.VarChar);
            p[25].Value = STANDUpmu06;
            p[26] = new SqlParameter("STANDUpmu07", SqlDbType.VarChar);
            p[26].Value = STANDUpmu07;
            p[27] = new SqlParameter("STANDUpmu08", SqlDbType.VarChar);
            p[27].Value = STANDUpmu08;
            p[28] = new SqlParameter("STANDUpmu09", SqlDbType.VarChar);
            p[28].Value = STANDUpmu09;
            p[29] = new SqlParameter("STANDUpmu10", SqlDbType.VarChar);
            p[29].Value = STANDUpmu10;
            p[30] = new SqlParameter("STANDUpmu11", SqlDbType.VarChar);
            p[30].Value = STANDUpmu11;
            p[31] = new SqlParameter("STANDUpmu12", SqlDbType.VarChar);
            p[31].Value = STANDUpmu12;
            p[32] = new SqlParameter("STANDUpmu13", SqlDbType.VarChar);
            p[32].Value = STANDUpmu13;
            p[33] = new SqlParameter("STANDUpmu14", SqlDbType.VarChar);
            p[33].Value = STANDUpmu14;
            p[34] = new SqlParameter("STANDUpmu15", SqlDbType.VarChar);
            p[34].Value = STANDUpmu15;
            p[35] = new SqlParameter("STANDUpmu16", SqlDbType.VarChar);
            p[35].Value = STANDUpmu16;
            p[36] = new SqlParameter("STANDUpmu17", SqlDbType.VarChar);
            p[36].Value = STANDUpmu17;
            p[37] = new SqlParameter("STANDUpmu18", SqlDbType.VarChar);
            p[37].Value = STANDUpmu18;
            p[38] = new SqlParameter("STANDUpmu19", SqlDbType.VarChar);
            p[38].Value = STANDUpmu19;
            p[39] = new SqlParameter("STANDUpmu20", SqlDbType.VarChar);
            p[39].Value = STANDUpmu20;
            p[40] = new SqlParameter("STANDUpmu21", SqlDbType.VarChar);
            p[40].Value = STANDUpmu21;
            p[41] = new SqlParameter("STANDUpmu22", SqlDbType.VarChar);
            p[41].Value = STANDUpmu22;
            p[42] = new SqlParameter("STANDUpmu23", SqlDbType.VarChar);
            p[42].Value = STANDUpmu23;
            p[43] = new SqlParameter("STANDUpmu24", SqlDbType.VarChar);
            p[43].Value = STANDUpmu24;
            p[44] = new SqlParameter("STANDUpmu25", SqlDbType.VarChar);
            p[44].Value = STANDUpmu25;
            p[45] = new SqlParameter("STANDUpmu26", SqlDbType.VarChar);
            p[45].Value = STANDUpmu26;
            p[46] = new SqlParameter("STANDUpmu27", SqlDbType.VarChar);
            p[46].Value = STANDUpmu27;
            p[47] = new SqlParameter("STANDUpmu28", SqlDbType.VarChar);
            p[47].Value = STANDUpmu28;
            p[48] = new SqlParameter("STANDUpmu29", SqlDbType.VarChar);
            p[48].Value = STANDUpmu29;
            p[49] = new SqlParameter("STANDUpmu30", SqlDbType.VarChar);
            p[49].Value = STANDUpmu30;
            p[50] = new SqlParameter("STANDUpmu31", SqlDbType.VarChar);
            p[50].Value = STANDUpmu31;
            p[51] = new SqlParameter("STANDUpmu32", SqlDbType.VarChar);
            p[51].Value = STANDUpmu32;
            p[52] = new SqlParameter("STANDUpmu33", SqlDbType.VarChar);
            p[52].Value = STANDUpmu33;
            p[53] = new SqlParameter("STANDGubun", SqlDbType.VarChar);
            p[53].Value = STANDGubun;

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
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public int RPT_Plan10Del(SqlConnection con, SqlTransaction tran, int SaupID, string Yearmon)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_RPT_Plan10Del";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = _DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
            p[2].Value = Yearmon;

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
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public int RPT_Plan11Del(SqlConnection con, SqlTransaction tran, int SaupID, string Yearmon)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_RPT_Plan11Del";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = _DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
            p[2].Value = Yearmon;

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
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        //UP_NBOGUN_PlanEduSilsiDel
        public int PlanEduSilsiDel(SqlConnection con, SqlTransaction tran, int SaupID, string Yearmon)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_PlanEduSilsiDel";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = _DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("SincheongYearmon", SqlDbType.Char);
            p[2].Value = Yearmon;

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
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public int RPT_Plan10Add(SqlConnection con, SqlTransaction tran, int SaupID, string Yearmon, string YearGubun, string SDate, string EDate, string SaupjaNum,
            string GJName, string MaterialName, string Kikwan)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_RPT_Plan10Add";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[10];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = _DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
            p[2].Value = Yearmon;
            p[3] = new SqlParameter("YearGubun", SqlDbType.VarChar);
            p[3].Value = YearGubun;
            p[4] = new SqlParameter("SDate", SqlDbType.VarChar);
            p[4].Value = SDate;
            p[5] = new SqlParameter("EDate", SqlDbType.VarChar);
            p[5].Value = EDate;
            p[6] = new SqlParameter("SaupjaNum", SqlDbType.VarChar);
            p[6].Value = SaupjaNum;
            p[7] = new SqlParameter("GJName", SqlDbType.VarChar);
            p[7].Value = GJName;
            p[8] = new SqlParameter("MaterialName", SqlDbType.VarChar);
            p[8].Value = MaterialName;
            p[9] = new SqlParameter("Kikwan", SqlDbType.VarChar);
            p[9].Value = Kikwan;

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
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        public int RPT_Plan11Add(SqlConnection con, SqlTransaction tran, int SaupID, string Yearmon, string YearGubun, int JuminCnt, int TD1Cnt,
            int TDNCnt, int TC1Cnt, int TCNCnt,  int TRCnt, int TUCnt, int ID2Cnt, int IC2Cnt, string Kikwan)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_RPT_Plan11Add";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[14];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = _DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
            p[2].Value = Yearmon;
            p[3] = new SqlParameter("YearGubun", SqlDbType.VarChar);
            p[3].Value = YearGubun;
            p[4] = new SqlParameter("JuminCnt", SqlDbType.Int);
            p[4].Value = JuminCnt;
            p[5] = new SqlParameter("TD1Cnt", SqlDbType.Int);
            p[5].Value = TD1Cnt;
            p[6] = new SqlParameter("TDNCnt", SqlDbType.Int);
            p[6].Value = TDNCnt;
            p[7] = new SqlParameter("TC1Cnt", SqlDbType.Int);
            p[7].Value = TC1Cnt;
            p[8] = new SqlParameter("TCNCnt", SqlDbType.Int);
            p[8].Value = TCNCnt;
            p[9] = new SqlParameter("TRCnt", SqlDbType.Int);
            p[9].Value = TRCnt;
            p[10] = new SqlParameter("TUCnt", SqlDbType.Int);
            p[10].Value = TUCnt;
            p[11] = new SqlParameter("ID2Cnt", SqlDbType.Int);
            p[11].Value = ID2Cnt;
            p[12] = new SqlParameter("IC2Cnt", SqlDbType.Int);
            p[12].Value = IC2Cnt;
            p[13] = new SqlParameter("Kikwan", SqlDbType.VarChar);
            p[13].Value = Kikwan;

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
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }
        //PlanAcceptAdd
        public DataTable PlanAcceptAdd(string pCenter, string pSaupjaNum, string pYearmon, string pSincheongDate, string pSincheongSabun,
            string pAcceptDate, string pBuseojang, string pPart1, string pPart1Comment, string pPart2, string pPart2Comment, string pPart2Check)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_PlanAcceptAdd";
                    com.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] p = new SqlParameter[12];
                    p[0] = new SqlParameter("Center", SqlDbType.Char);
                    p[0].Value = Center;
                    p[1] = new SqlParameter("Yearmon", SqlDbType.Char);
                    p[1].Value = pYearmon;
                    p[2] = new SqlParameter("SaupjaNum", SqlDbType.Char);
                    p[2].Value = pSaupjaNum;
                    p[3] = new SqlParameter("SincheongDate", SqlDbType.VarChar);
                    p[3].Value = pSincheongDate;
                    p[4] = new SqlParameter("SincheongSabun", SqlDbType.VarChar);
                    p[4].Value = pSincheongSabun;
                    p[5] = new SqlParameter("AcceptDate", SqlDbType.VarChar);
                    p[5].Value = pAcceptDate;
                    p[6] = new SqlParameter("Buseojang", SqlDbType.VarChar);
                    p[6].Value = pBuseojang;
                    p[7] = new SqlParameter("Part1", SqlDbType.VarChar);
                    p[7].Value = pPart1;
                    p[8] = new SqlParameter("Part1Comment", SqlDbType.VarChar);
                    p[8].Value = pPart1Comment;
                    p[9] = new SqlParameter("Part2", SqlDbType.VarChar);
                    p[9].Value = pPart2;
                    p[10] = new SqlParameter("Part2Comment", SqlDbType.VarChar);
                    p[10].Value = pPart2Comment;
                    p[11] = new SqlParameter("Part2Check", SqlDbType.VarChar);
                    p[11].Value = pPart2Check;

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
        public int PlanBuseojangAdd(int SaupID, string Yearmon, string SaupjaNum, string AcceptDate, string Buseojang)
        {
            int r;

            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_PlanBuseojangAdd";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[8];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = _DHCenter;
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[1].Value = SaupID;
                p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
                p[2].Value = Yearmon;
                p[3] = new SqlParameter("SaupjaNum", SqlDbType.VarChar);
                p[3].Value = SaupjaNum;
                p[4] = new SqlParameter("SincheongDate", SqlDbType.VarChar);
                p[4].Value = DateTime.Now.ToString("yyyy-MM-dd");
                p[5] = new SqlParameter("SincheongSabun", SqlDbType.VarChar);
                p[5].Value = UserID;
                p[6] = new SqlParameter("AcceptDate", SqlDbType.VarChar);
                p[6].Value = AcceptDate;
                p[7] = new SqlParameter("Buseojang", SqlDbType.VarChar);
                p[7].Value = Buseojang;

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
                    LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " +  ex.Message);
                }

                return r;
            }
        }
        public int PlanBuseojangDel(string pCenter, string pSaupjaNum, string pYearmon)
        {
            int r;

            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_PlanBuseojangDel";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = pCenter;
                p[1] = new SqlParameter("SaupjaNum", SqlDbType.Char);
                p[1].Value = pSaupjaNum;
                p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
                p[2].Value = pYearmon;

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
                    LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
                }

                return r;
            }
        }
        public int PlanJungjeomAdd(SqlConnection con, SqlTransaction tran, int SaupID, string Yearmon, string Code, string Month, string Comment)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_PlanJungjeomAdd";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[6];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = _DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
            p[2].Value = Yearmon;
            p[3] = new SqlParameter("Code", SqlDbType.Char);
            p[3].Value = Code;
            p[4] = new SqlParameter("Month", SqlDbType.Char);
            p[4].Value = Month;
            p[5] = new SqlParameter("Comment", SqlDbType.VarChar);
            p[5].Value = Comment;

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
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }
        //UP_NBOGUN_PlanBuseojangAdd

        //UP_NBOGUN_RPT_Plan00
        public SqlDataReader RPT_Plan00Reader(int SaupID, string Yearmon)
        {
            SqlDataReader dt;
            SqlConnection con = new SqlConnection(_Connection);
            //dt = new DataTable();
            con.Open();
            SqlCommand com = con.CreateCommand();
            com.CommandText = "UP_NBOGUN_RPT_Plan00";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = _DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
            p[2].Value = Yearmon;

            com.Parameters.AddRange(p);

            //서버로그 남기기기
            WriteServerLog(com.CommandText, p);

            dt = com.ExecuteReader(CommandBehavior.CloseConnection);

            return dt;
        }

        /// <summary>
        /// 연도별 일반검진 초기화
        /// </summary>
        /// <param name="SaupID"></param>
        /// <param name="Yearmon"></param>
        /// <returns></returns>
        public DataTable RPT_Plan11Init(int SaupID, string Yearmon)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_RPT_Plan11Init";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = _DHCenter;
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[1].Value = SaupID;
                p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
                p[2].Value = Yearmon;

                com.Parameters.AddRange(p);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable RPT_Plan00(int SaupID, string Yearmon)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_RPT_Plan00";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = _DHCenter;
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[1].Value = SaupID;
                p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
                p[2].Value = Yearmon;

                com.Parameters.AddRange(p);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable RPT_Plan13(int SaupID, string Yearmon)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_RPT_Plan13";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = _DHCenter;
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[1].Value = SaupID;
                p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
                p[2].Value = Yearmon;

                com.Parameters.AddRange(p);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable RPT_Plan10(int SaupID, string Yearmon)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_RPT_Plan10";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = _DHCenter;
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[1].Value = SaupID;
                p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
                p[2].Value = Yearmon;

                com.Parameters.AddRange(p);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable RPT_Plan11(int SaupID, string Yearmon)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_RPT_Plan11";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = _DHCenter;
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[1].Value = SaupID;
                p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
                p[2].Value = Yearmon;

                com.Parameters.AddRange(p);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable SaupjaJunggeomList(string Yearmon, string SaupjaNum)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_SaupjaJunggeomList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = _DHCenter;
                p[1] = new SqlParameter("Yearmon", SqlDbType.Char);
                p[1].Value = Yearmon;
                p[2] = new SqlParameter("SaupjaNum", SqlDbType.Char);
                p[2].Value = SaupjaNum;

                com.Parameters.AddRange(p);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable PlanJungjeomList(int SaupID, string Yearmon, string Code)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_PlanJungjeomList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[4];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = _DHCenter;
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[1].Value = SaupID;
                p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
                p[2].Value = Yearmon;
                p[3] = new SqlParameter("Code", SqlDbType.VarChar);
                p[3].Value = Code;

                com.Parameters.AddRange(p);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }
        //
        public DataSet PlanUpmuList(int SaupID, string Yearmon, string DHJikjong)
        {
            DataSet dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataSet();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_PlanUpmuList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[5];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = _DHCenter;
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[1].Value = SaupID;
                p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
                p[2].Value = Yearmon;
                p[3] = new SqlParameter("DHJikjong", SqlDbType.VarChar);
                p[3].Value = DHJikjong;
                p[4] = new SqlParameter("Gubun", SqlDbType.Char);
                p[4].Value = Yearmon.Substring(5, 2) == "00" ? "2" : "1";

                com.Parameters.AddRange(p);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }
        //UP_NBOGUN_PlanEduItemCodeList
        public DataTable PlanEduItemCode(string Year, string Gubun)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_PlanEduItemCodeList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("Year", SqlDbType.VarChar);
                p[0].Value = Year;
                p[1] = new SqlParameter("Gubun", SqlDbType.VarChar);
                p[1].Value = Gubun;

                com.Parameters.AddRange(p);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }

        //UP_NBOGUN_PlanTargetList
        public DataTable PlanTargetList(int SaupID, string Yearmon)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_PlanTargetList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = DHCenter;
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[1].Value = SaupID;
                p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
                p[2].Value = Yearmon;

                com.Parameters.AddRange(p);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }

        //UP_NBOGUN_PlanBuseojangList
        public DataTable PlanBuseojangList(string SaupjaNum, string Yearmon)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_PlanBuseojangList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = DHCenter;
                p[1] = new SqlParameter("SaupjaNum", SqlDbType.Char);
                p[1].Value = SaupjaNum;
                p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
                p[2].Value = Yearmon;

                com.Parameters.AddRange(p);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable PlanEduSilsiList(int SaupID, string Yearmon)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_PlanEduList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = DHCenter;
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[1].Value = SaupID;
                p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
                p[2].Value = Yearmon;

                com.Parameters.AddRange(p);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }
        public DataTable PlanEduItemList(int SaupID, string Yearmon)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_PlanEduItemList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = DHCenter;
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[1].Value = SaupID;
                p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
                p[2].Value = Yearmon;

                com.Parameters.AddRange(p);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable PlanVisitList(int SaupID, string Yearmon)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_PlanVisitList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = DHCenter;
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[1].Value = SaupID;
                p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
                p[2].Value = Yearmon;

                com.Parameters.AddRange(p);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }

        //UP_NBOGUN_PlanVisitList
        public DataTable PlanYearmonList(int SaupID)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_PlanYearmonList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = DHCenter;
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[1].Value = SaupID;

                com.Parameters.AddRange(p);

                //서버로그 남기기기
                WriteServerLog(com.CommandText, p);

                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}
