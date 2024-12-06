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
        public DataTable SaupjaContractInwon(int SaupID, string Yearmon)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                lock (lockObject)
                {
                    dt = new DataTable();
                    con.Open();
                    SqlCommand com = con.CreateCommand();
                    com.CommandText = "UP_NBOGUN_SaupjaContractInwon";
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Center"></param>
        /// <param name="Gubun">Gubun = '3' 당월 사업장, Gubun = '6' 당해연도 전체 사업장 리스트(해약사업장 포함), '7' 타부서 실적</param>
        /// <param name="SaupjaName"></param>
        /// <returns></returns>
        public DataTable CenterSaupjaList(string Center, string Gubun = "5", string SaupjaName = "")
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_SaupjaList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[4];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = Center;
                p[1] = new SqlParameter("Yearmon", SqlDbType.Char);
                p[1].Value = Yearmon;
                p[2] = new SqlParameter("Gubun", SqlDbType.Char);
                p[2].Value = Gubun;
                p[3] = new SqlParameter("SaupjaName", SqlDbType.VarChar);
                p[3].Value = SaupjaName;

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

        public DataTable CenterSaupjaList(string Center, string Yearmon, string Gubun, string SaupjaName)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_SaupjaList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[4];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[0].Value = Center;
                p[1] = new SqlParameter("Yearmon", SqlDbType.Char);
                p[1].Value = Yearmon;
                p[2] = new SqlParameter("Gubun", SqlDbType.Char);
                p[2].Value = Gubun;
                p[3] = new SqlParameter("SaupjaName", SqlDbType.VarChar);
                p[3].Value = SaupjaName;

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

        public DataTable SaupjaNumList(int SaupID)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_SaupjaNumList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);

                p[0].Value = _DHCenter;
                p[1].Value = SaupID;

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

        public DataTable SaupjaYearmonList(int SaupID)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_SaupjaYearmonList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);

                p[0].Value = _DHCenter;
                p[1].Value = SaupID;

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

        //UP_NBOGUN_SaupjaUpmuManagerList '00', 4196, '2022-05', '3'
        public DataTable SaupjaUpmuManagerList(int SaupID, string Yearmon)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_SaupjaUpmuManagerList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[4];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[2] = new SqlParameter("Yearmon", SqlDbType.Char);
                p[3] = new SqlParameter("Gubun", SqlDbType.Char);

                p[0].Value = _DHCenter;
                p[1].Value = SaupID;
                p[2].Value = Yearmon;
                p[3].Value = "3";

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

        //UP_NBOGUN_SaupjaUpmuDamdangList '00', 4196, '2022-05' 
        public DataTable SaupjaUpmuDamdangList(int SaupID, string Yearmon)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_SaupjaUpmuDamdangList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[2] = new SqlParameter("Yearmon", SqlDbType.Char);

                p[0].Value = _DHCenter;
                p[1].Value = SaupID;
                p[2].Value = Yearmon;

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

        //안전보건관리책임자
        public DataTable SaupjaDH2703List(int SaupID, string Yearmon)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_SaupjaDH2703";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[2] = new SqlParameter("Yearmon", SqlDbType.Char);

                p[0].Value = _DHCenter;
                p[1].Value = SaupID;
                p[2].Value = Yearmon;

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

        public DataTable SaupjaUpmuManagerFileList(int SaupID)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_SaupjaUpmuManagerFileList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);

                p[0].Value = _DHCenter;
                p[1].Value = SaupID;

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
        /// 업무담당자 저장 전 해당 작업연월의 내용 삭제
        /// </summary>
        /// <param name="con"></param>
        /// <param name="tran"></param>
        /// <param name="Center"></param>
        /// <param name="SaupID"></param>
        /// <param name="Yearmon"></param>
        /// <returns></returns>
        
        public int SaupjaUpmuDamdangDel(SqlConnection con, SqlTransaction tran, int SaupID, string Yearmon)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_SaupjaUpmuDamdangDel";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[2] = new SqlParameter("Yearmon", SqlDbType.Char);

            p[0].Value = _DHCenter;
            p[1].Value = SaupID;
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
                _Error = ex.Message;
                LogManager.WriteLog("오류 함수 : " + System.Reflection.MethodBase.GetCurrentMethod() + " : " + ex.Message);
            }

            return r;
        }

        //UP_NBOGUN_SaupjaUpmuDamdangAdd
        public int SaupjaUpmuDamdangSave(SqlConnection con, SqlTransaction tran, int SaupID, string Yearmon, string Gubun, string Name, string Sosok, string Tel, string DirectNumber, string EMail, string IsSelect)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_SaupjaUpmuDamdangAdd";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[10];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = _DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("@Yearmon", SqlDbType.Char);
            p[2].Value = Yearmon;
            p[3] = new SqlParameter("@Gubun", SqlDbType.Char);
            p[3].Value = Gubun;
            p[4] = new SqlParameter("@Name", SqlDbType.Char);
            p[4].Value = Name;
            p[5] = new SqlParameter("@Sosok", SqlDbType.Char);
            p[5].Value = Sosok;
            p[6] = new SqlParameter("@Tel", SqlDbType.Char);
            p[6].Value = Tel;
            p[7] = new SqlParameter("@DirectNumber", SqlDbType.Char);
            p[7].Value = DirectNumber;
            p[8] = new SqlParameter("@Email", SqlDbType.Char);
            p[8].Value = EMail;
            p[9] = new SqlParameter("@IsSelect", SqlDbType.VarChar);
            p[9].Value = IsSelect;

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
        //UP_NBOGUN_SaupjaUpmuManagerAdd
        public int SaupjaUpmuManagerSave(SqlConnection con, SqlTransaction tran, int SaupID, string Yearmon, string Gubun, string Name, string SeonimDate, string License, 
            string SchoolCareer, string Career, string EduGubun, string EduStart, string EduEnd, string UploadFileName, string UploadFileUrl)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_SaupjaUpmuManagerAdd";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[14];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = _DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("@Yearmon", SqlDbType.Char);
            p[2].Value = Yearmon;
            p[3] = new SqlParameter("Gubun", SqlDbType.Char);
            p[3].Value = Gubun;
            p[4] = new SqlParameter("Name", SqlDbType.VarChar);
            p[4].Value = Name;
            p[5] = new SqlParameter("SeonimDate", SqlDbType.Char);
            p[5].Value = SeonimDate;
            p[6] = new SqlParameter("@License", SqlDbType.VarChar);
            p[6].Value = License;
            p[7] = new SqlParameter("@SchoolCareer", SqlDbType.VarChar);
            p[7].Value = SchoolCareer;
            p[8] = new SqlParameter("@Career", SqlDbType.VarChar);
            p[8].Value = Career;
            p[9] = new SqlParameter("@EduGubun", SqlDbType.VarChar);
            p[9].Value = EduGubun;
            p[10] = new SqlParameter("@EduStart", SqlDbType.VarChar);
            p[10].Value = EduStart;
            p[11] = new SqlParameter("@EduEnd", SqlDbType.VarChar);
            p[11].Value = EduEnd;
            p[12] = new SqlParameter("@UploadFileName", SqlDbType.VarChar);
            p[12].Value = UploadFileName;//UploadFileUrl
            p[13] = new SqlParameter("UploadFileUrl", SqlDbType.VarChar);
            p[13].Value = UploadFileUrl;//UploadFileUrl

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

        //UP_NBOGUN_RPT_SaupjaCardUpmuDamdangList
        public DataTable RPT_SaupjaCardUpmuDamdangList(int SaupID, string Year)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_RPT_SaupjaCardUpmuDamdangList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[2] = new SqlParameter("Year", SqlDbType.Char);

                p[0].Value = _DHCenter;
                p[1].Value = SaupID;
                p[2].Value = Year;
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
        //UP_NBOGUN_RPT_SaupjaCardUpmuManagerList
        public DataTable RPT_SaupjaCardUpmuManagerList(int SaupID, string Year)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_RPT_SaupjaCardUpmuManagerList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[2] = new SqlParameter("Year", SqlDbType.Char);

                p[0].Value = _DHCenter;
                p[1].Value = SaupID;
                p[2].Value = Year;
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

        public int RPT_SaupjaCardUpmuDamdangDel(SqlConnection con, SqlTransaction tran, int SaupID, string Year)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_RPT_SaupjaCardUpmuDamdangDel";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[2] = new SqlParameter("Year", SqlDbType.Char);

            p[0].Value = _DHCenter;
            p[1].Value = SaupID;
            p[2].Value = Year;

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

        public int RPT_SaupjaCardUpmuManagerDel(SqlConnection con, SqlTransaction tran, int SaupID, string Year)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_RPT_SaupjaCardUpmuManagerDel";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[2] = new SqlParameter("Year", SqlDbType.Char);

            p[0].Value = _DHCenter;
            p[1].Value = SaupID;
            p[2].Value = Year;

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

        //UP_NBOGUN_RPT_SaupjaCardUpmuDamdangSave
        public int RPT_SaupjaCardUpmuDamdangSave(SqlConnection con, SqlTransaction tran, int SaupID, string Yearmon, string Gubun, string Name, string Sosok, string Tel, string DirectNumber, string EMail, string IsSelect)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_RPT_SaupjaCardUpmuDamdangSave";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[10];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = _DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("@Yearmon", SqlDbType.Char);
            p[2].Value = Yearmon;
            p[3] = new SqlParameter("@Gubun", SqlDbType.Char);
            p[3].Value = Gubun;
            p[4] = new SqlParameter("@Name", SqlDbType.Char);
            p[4].Value = Name;
            p[5] = new SqlParameter("@Sosok", SqlDbType.Char);
            p[5].Value = Sosok;
            p[6] = new SqlParameter("@Tel", SqlDbType.Char);
            p[6].Value = Tel;
            p[7] = new SqlParameter("@DirectNumber", SqlDbType.Char);
            p[7].Value = DirectNumber;
            p[8] = new SqlParameter("@Email", SqlDbType.Char);
            p[8].Value = EMail;
            p[9] = new SqlParameter("@IsSelect", SqlDbType.VarChar);
            p[9].Value = IsSelect;

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

        public int RPT_SaupjaCardUpmuManagerSave(SqlConnection con, SqlTransaction tran, int SaupID, string Yearmon, string Gubun, string Name, string SeonimDate, string License,
            string SchoolCareer, string Career, string EduGubun, string EduStart, string EduEnd, string UploadFileName, string UploadFileUrl)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_RPT_SaupjaCardUpmuManagerSave";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[14];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = _DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("@Yearmon", SqlDbType.Char);
            p[2].Value = Yearmon;
            p[3] = new SqlParameter("Gubun", SqlDbType.Char);
            p[3].Value = Gubun;
            p[4] = new SqlParameter("Name", SqlDbType.VarChar);
            p[4].Value = Name;
            p[5] = new SqlParameter("SeonimDate", SqlDbType.Char);
            p[5].Value = SeonimDate;
            p[6] = new SqlParameter("@License", SqlDbType.VarChar);
            p[6].Value = License;
            p[7] = new SqlParameter("@SchoolCareer", SqlDbType.VarChar);
            p[7].Value = SchoolCareer;
            p[8] = new SqlParameter("@Career", SqlDbType.VarChar);
            p[8].Value = Career;
            p[9] = new SqlParameter("@EduGubun", SqlDbType.VarChar);
            p[9].Value = EduGubun;
            p[10] = new SqlParameter("@EduStart", SqlDbType.VarChar);
            p[10].Value = EduStart;
            p[11] = new SqlParameter("@EduEnd", SqlDbType.VarChar);
            p[11].Value = EduEnd;
            p[12] = new SqlParameter("@UploadFileName", SqlDbType.VarChar);
            p[12].Value = UploadFileName;//UploadFileUrl
            p[13] = new SqlParameter("UploadFileUrl", SqlDbType.VarChar);
            p[13].Value = UploadFileUrl;//UploadFileUrl

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

        //UP_NBOGUN_RPT_SaupjaCardGongjungList
        public DataTable RPT_SaupjaCard04List(int SaupID, string Yearmon)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_RPT_SaupjaCardZ04";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[2] = new SqlParameter("Yearmon", SqlDbType.VarChar);

                p[0].Value = _DHCenter;
                p[1].Value = SaupID;
                p[2].Value = Yearmon;

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

        public DataTable RPT_SaupjaCardGongjungList(int SaupID, string Year)
        {
            DataTable dt;

            using (SqlConnection con = new SqlConnection(_Connection))
            {
                dt = new DataTable();
                con.Open();
                SqlCommand com = con.CreateCommand();
                com.CommandText = "UP_NBOGUN_RPT_SaupjaCardGongjungList";
                com.CommandType = CommandType.StoredProcedure;

                SqlParameter[] p = new SqlParameter[3];
                p[0] = new SqlParameter("Center", SqlDbType.Char);
                p[1] = new SqlParameter("SaupID", SqlDbType.Int);
                p[2] = new SqlParameter("Year", SqlDbType.VarChar);

                p[0].Value = _DHCenter;
                p[1].Value = SaupID;
                p[2].Value = Year;

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

        public int RPT_SaupjaCardGongjungDel(SqlConnection con, SqlTransaction tran, int SaupID, string Year)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_RPT_SaupjaCardGongjungDel";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[2] = new SqlParameter("Year", SqlDbType.Char);

            p[0].Value = _DHCenter;
            p[1].Value = SaupID;
            p[2].Value = Year;

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

        
        public int RPT_SaupjaCardGongjungSave(SqlConnection con, SqlTransaction tran, int SaupID, string Year, string GJName)
        {
            int r;

            SqlCommand com = con.CreateCommand();
            com.Transaction = tran;
            com.CommandText = "UP_NBOGUN_RPT_SaupjaCardGongjungSave";
            com.CommandType = CommandType.StoredProcedure;

            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("Center", SqlDbType.Char);
            p[0].Value = _DHCenter;
            p[1] = new SqlParameter("SaupID", SqlDbType.Int);
            p[1].Value = SaupID;
            p[2] = new SqlParameter("Year", SqlDbType.Char);
            p[2].Value = Year;
            p[3] = new SqlParameter("GJName", SqlDbType.NVarChar);
            p[3].Value = GJName;

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
}
