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
        Unknown = 3
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
            Application.Run(new FormSSRSExplorer());
        }
    }
}