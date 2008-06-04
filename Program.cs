using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RSS_Report_Retrievers
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // TODO: Log this error someway
            try
            {
                Application.Run(new FormSSRSExplorer());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r\n" + e.StackTrace, "Unhandled exception in Rss Explorer");
            }
        }
    }
}