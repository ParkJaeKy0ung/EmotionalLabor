using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NBOGUN
{
    public class LogManager
    {
        //에러 로그 위치
        const string logName = @"C:\Kiha\Log_Function";

        public static string WriteLog(object CallForm)
        {
            //while(IsStop == false)
            //{
            //    //CallerMemberNameAttribute a = new CallerMemberNameAttribute();

            //}
            //return "";
            string logContent = "";
            try
            {
                StackFrame frame = new StackFrame(1);

                MethodBase callerMethod = frame.GetMethod();

                string name = callerMethod.Name;

                ParameterInfo[] p = callerMethod.GetParameters();

                string para = "";

                for (int i = 0; i < p.Length; i++)
                {
                    if (para != "")
                        para += ",";

                    para += p[i].ParameterType.Name + " " + p[i].Name;
                }

                logContent = name + "(" + para + ")";

                WriteEventLogEntry(((Form)CallForm).Name, logContent);

            }
            catch
            {

            }
            return logContent;
        }

        //변수 기록
        public static string WriteLog(string pName, string pValue)
        {
            string logContent = "";
            try
            {
                WriteEventLogEntry(pName, pValue);
            }
            catch
            {

            }
            return logContent;
        }

        public static string WriteLog(string pValue)
        {
            string logContent = "";
            try
            {
                WriteEventLogEntry("사용자 로그", " " + pValue + " ");
            }
            catch
            {

            }
            return logContent;
        }

        private static void WriteEventLogEntry(string ModuleName, string FunctionName)
        {
            string center = Biz.Instance.Center;
            string sabun = Biz.Instance.UserID;

            string sysDate = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt");
            string procName = ModuleName;
            string procParameter = FunctionName;
            string hostName = Dns.GetHostName().ToString();

            string InternalIP = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
            string ExternalIP = "";
            string date = DateTime.Now.ToString("yyyy-MM-dd");

            string log = center + "|" + sabun + "|" + sysDate + "|" + ModuleName + "|"
                + FunctionName + "|" + hostName + "|" + InternalIP + "|" + ExternalIP + "|" + date;

            string fileName = logName + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

            StreamWriter writer = File.AppendText(fileName);
            writer.WriteLine(log);
            writer.Close();
        }
    }
}
