using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RSS_Report_Retrievers
{
    public enum ItemTypes
    {
        Folder = 0,
        Datasource = 1,
        Report = 2,
        model = 3,
        Unknown = 4

    }

    public enum ViewItems
    {
        All = 0,
        Datasources = 1,
        Folders = 2
    }

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