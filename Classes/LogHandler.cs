using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace RSS_Report_Retrievers.Classes
{
    class LogHandler
    {
        public static void WriteLogEntry(Exception ex)
        {
            using(StreamWriter sw = new StreamWriter("RSSExplorer.log", true))
            {
                while(ex!=null)
                {
                    sw.WriteLine(string.Format("{0} : {1}", DateTime.Now.ToString("dd-MM-yyyy HH:mm"), ex.Message));
                    sw.WriteLine(string.Format("{0} : {1}", DateTime.Now.ToString("dd-MM-yyyy HH:mm"), ex.StackTrace));

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
            using (StreamWriter sw = new StreamWriter("RSSExplorer.log", true))
            {
                sw.WriteLine(string.Format("{0} : {1}", DateTime.Now.ToString("dd-MM-yyyy HH:mm"), message));
            }
        }
    }
}
