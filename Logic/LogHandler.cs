using System;
using System.IO;

namespace ReportingServerManager.Logic
{
    class LogHandler
    {
        public static void WriteLogEntry(Exception ex)
        {
            using (var sw = new StreamWriter("ReportingServerManager.log", true))
            {
                while(ex!=null)
                {
                    sw.WriteLine("{0} : {1}", DateTime.Now.ToString("dd-MM-yyyy HH:mm"), ex.Message);
                    sw.WriteLine("{0} : {1}", DateTime.Now.ToString("dd-MM-yyyy HH:mm"), ex.StackTrace);

                    ex = ex.InnerException; 
                }
            }
        }

        public static void WriteLogEntry(Exception ex, string message)
        {
            WriteLogEntry(message); 
            WriteLogEntry(ex); 
        }

        public static void WriteLogEntry(string message)
        {
            using (var sw = new StreamWriter("RSSExplorer.log", true))
            {
                sw.WriteLine("{0} : {1}", DateTime.Now.ToString("dd-MM-yyyy HH:mm"), message);
            }
        }
    }
}
