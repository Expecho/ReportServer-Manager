using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ReportingServerManager.Logic;
using ReportingServerManager.Logic.ReportModel;

namespace ReportingServerManager.Forms
{
    public partial class FormDependantItems : Form
    {
        private readonly Controller controller;
        private readonly string existingModelPath;
        private readonly string newModelSMDL;

        private IList<ReportTestResult> testResults;

        private IList<ReportTestResult> TestResults
        {
            set { 
                testResults = value;

                dgvResults.Rows.Clear();

                foreach (var testResult in testResults)
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(dgvResults);

                    row.Cells[0].Value = testResult.ReportName;
                    row.Cells[1].Value = testResult.PassedTest;

                    dgvResults.Rows.Add(row);
                }
            }
        }

        public FormDependantItems(Controller controller, string existingModelPath, string newModelSMDL)
        {
            this.controller = controller;
            this.existingModelPath = existingModelPath;
            this.newModelSMDL = newModelSMDL;

            InitializeComponent();
        }

        private void FormDependantItemsLoad(object sender, EventArgs e)
        {
            CheckModelForCompatibility(controller, existingModelPath, newModelSMDL);
        }

        private void CheckModelForCompatibility(Controller controller, string existingModelPath, string newModelSMDL)
        {
            Show();

            DialogResult = DialogResult.Yes;

            var isCompatibleModel = true;

            var dependantItems = controller.ListDependantItems(existingModelPath);

            var validator = new ReportModelValidator(newModelSMDL);

            var tmpResults = new List<ReportTestResult>();


            foreach (var reportItem in dependantItems)
            {
                // Load report 
                var bytes = controller.GetReport(reportItem.Path);
                var reportRDL = Encoding.UTF8.GetString(bytes);

                if (reportRDL[0] == 65279)
                    reportRDL = reportRDL.Substring(1); // Step by Byte-order mark which confuses the xml-parser

                // Check it against the model:
                var result = new ReportTestResult
                                 {
                                     ReportName = reportItem.Path + "/" + reportItem.Name,
                                     PassedTest = validator.ValidateModelForReport(reportRDL)
                                 };

                if (result.PassedTest == false)
                    isCompatibleModel = false;

                tmpResults.Add(result);

                TestResults = tmpResults;
                Application.DoEvents();
            }

            if (isCompatibleModel == false)
            {
                MessageBox.Show("This model seem to have some compatibility issues with existing reports.", "Compatibility warning", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("The new model seems to be compatible with existing reports. ", "", MessageBoxButtons.OK);
            }
        }

        private void FormDependantItemsResize(object sender, EventArgs e)
        {
            dgvResults.Width = Width - 50;
            dgvResults.Height = Height - 130;

            btnClose.Top = Height + 199 - 277;
            btnClose.Left = Width + 393 - 498;
        }

        private void BtnCloseClick(object sender, EventArgs e)
        {
            Close();
        }

    }

    public class ReportTestResult
    {
        public string ReportName { get; set; }
        public bool PassedTest { get; set; }
    }
}