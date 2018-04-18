using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Collector
{
    public class LogWriter
    {
        private string m_exePath = string.Empty;
        private string mk_dir = "logs\\" + Environment.UserName;
        public LogWriter(string logMessage)
        {
            LogWrite(logMessage);
        }
        public void LogWrite(string logMessage)
        {
            m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            try
            {
                Directory.CreateDirectory(mk_dir);
                using (StreamWriter w = File.AppendText(m_exePath + "\\"+ mk_dir +"\\log.txt"))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception ex)
            {
                new LogWriter(ex.Message);
            }
        }

        public void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.Write("| :");
                txtWriter.WriteLine(" {0}", logMessage);
            }
            catch (Exception ex)
            {
                new LogWriter(ex.Message);
            }
        }
    }
}
