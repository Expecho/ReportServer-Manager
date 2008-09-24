using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RSS_Report_Retrievers.Classes;
using RSS_Report_Retrievers.Classes.ReportModel;

namespace RSS_Report_Retrievers.Forms
{
    public partial class FormDependantItems : Form
    {

        private IList<ReportTestResult> testResults;

        private IList<ReportTestResult> TestResults
        {
            get { return testResults; }
            set { 
                testResults = value;

                dgvResults.Rows.Clear();

                foreach (ReportTestResult tr in this.testResults)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(this.dgvResults);

                    row.Cells[0].Value = tr.ReportName;
                    row.Cells[1].Value = tr.PassedTest;

                    dgvResults.Rows.Add(row);
                }
            }
        }

        public FormDependantItems()
        {
            InitializeComponent();
        }

        private void FormDependantItems_Load(object sender, EventArgs e)
        {
        }

        public void CheckModelForCompatibility(IController rs, string existingModelPath, string newModelSMDL)
        {
            this.Show();

            this.DialogResult = DialogResult.Yes;

            bool isCompatible = true;

            List<ReportItemDTO> dependantItems = rs.ListDependantItems(existingModelPath);

            ReportModelValidator validator = new ReportModelValidator(newModelSMDL);

            List<ReportTestResult> tmpResults = new List<ReportTestResult>();


            foreach (ReportItemDTO reportItem in dependantItems)
            {
                // Load report 
                byte[] bytes = rs.GetReport(reportItem.Path);
                string reportRDL = Encoding.UTF8.GetString(bytes);

                if (reportRDL[0] == 65279)
                    reportRDL = reportRDL.Substring(1); // Step by Byte-order mark which confuses the xml-parser

                // Check it against the model:
                ReportTestResult result = new ReportTestResult();
                result.ReportName = reportItem.Name;

                result.PassedTest = validator.ValidateModelForReport(reportRDL);

                if (result.PassedTest == false)
                    isCompatible = false;

                tmpResults.Add(result);

                // update GUI
                TestResults = tmpResults;
                Application.DoEvents();
            }

            if (isCompatible == false)
            {
                this.DialogResult = MessageBox.Show("This model seem to have some compatibility issues, would you like to replace it anyway?", "Compatibility warning", MessageBoxButtons.YesNo);
            }

            this.Close();
        }

        private string GetModelDefinition(IRSFacade rsInstance, string path)
        {
            byte[] bytes = rsInstance.GetModelDefinition(path);

            System.Text.UTF8Encoding enc = new UTF8Encoding();

            return enc.GetString(bytes);
        }

        private string GetReportDefinition(IRSFacade rsInstance, string path)
        {
            byte[] bytes = rsInstance.GetReportDefinition(path);

            System.Text.UTF8Encoding enc = new UTF8Encoding();

            return enc.GetString(bytes);
        }

        private List<ReportItemDTO> LoadDependantItems(IRSFacade rsInstance, string modelPath)
        {
            if(rsInstance is RS2005Facade)
            {
                return ((rsInstance as RS2005Facade).ListDependantItems(modelPath));
            }
            else if (rsInstance is RS2005SharePointFacade)
            {
                return ((rsInstance as RS2005SharePointFacade).ListDependantItems(modelPath));
            }
            else
            {
                return new List<ReportItemDTO>();
            }
        }

        private void MockTestResults()
        {
            List<ReportTestResult> list = new List<ReportTestResult>();

            ReportTestResult res = new ReportTestResult();

            res.PassedTest = true;
            res.ReportName = "David";

            ReportTestResult res2 = new ReportTestResult();

            res2.PassedTest = false;
            res2.ReportName = "David2";

            list.Add(res);
            list.Add(res2);

            TestResults = list;
        }

    }

    public class ReportTestResult
    {
        private string reportName;

        public string ReportName
        {
            get { return reportName; }
            set { reportName = value; }
        }
        private bool passedTest;

        public bool PassedTest
        {
            get { return passedTest; }
            set { passedTest = value; }
        }
    }
}